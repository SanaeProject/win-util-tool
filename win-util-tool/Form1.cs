using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace win_util_tool
{
    public partial class Form1 : Form
    {
        private readonly Translator translator;
        private readonly Wiktionary wikitionary = new Wiktionary();
        private string searchUrl = "https://www.google.com/search?q=";

        public Form1(string text)
        {
            InitializeComponent();

            try
            {
                translator = new Translator();
            }
            catch (Exception e)
            {
                translator = null;
                MessageBox.Show(e.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            this.Shown += (s,e)=> { 
                search.Text = text; 
                result.Select(); 
                render(text); 
            };
        }

        public async void render(string text)
        {
            string translated = null;
            string html = null;
            searchUrl = "https://www.google.com/search?q=" + Uri.EscapeDataString(text);

            if (text.Length < 100)
            {
                try
                {
                    translated = await translator.TranslateAsync(text);
                }
                catch (Exception ex)
                {
                    translated = "[翻訳失敗]";
                }
            }

            try
            {
                html = await wikitionary.search(text);
            }
            catch (Exception ex)
            {
                html = "<html><body>検索失敗</body></html>";
            }

            if (result != null && !result.IsDisposed && result.IsHandleCreated)
            {
                result.Invoke((MethodInvoker)(() =>
                {
                    if (translated != null)
                        result.Text = translated;
                }));
            }

            if (webResult != null && !webResult.IsDisposed && webResult.IsHandleCreated)
            {
                webResult.Invoke((MethodInvoker)(() =>
                {
                    webResult.DocumentText = html;
                }));
            }
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            // 入力が変更されたときにタイマーをリセット
            if (timer1.Enabled)
                timer1.Stop();

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            render(search.Text);
            timer1.Stop();
        }

        private void searchLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.searchUrl);
            this.Close();
        }

        private void result_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T)
            {
                Invoke((MethodInvoker)(async () => {
                    result.Text = await translator.TranslateAsync(search.Text);
                }));
            }
            else if (e.KeyCode == Keys.Enter)
            {
                // Enterキーで検索リンクを開く
                System.Diagnostics.Process.Start(this.searchUrl);
                this.Close();
            }
        }
    }
    public class Wiktionary {
        private readonly HttpClient httpClient = new HttpClient();

        public Wiktionary() {}

        public async Task<string> search(string text) {
            try
            {
                string html = await httpClient.GetStringAsync("https://" + Translator.DetectLang(text).ToLower() + ".wiktionary.org/wiki/" + text);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                var targetNode = doc.DocumentNode.SelectSingleNode("//div[@id='bodyContent']");
                string extractedHtml = targetNode?.OuterHtml ?? "<p>要素が見つかりませんでした。</p>";

                return extractedHtml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
    public class Translator
    {
        private readonly string apiKey;
        private static readonly string endpoint = "https://api-free.deepl.com/v2/translate";

        public Translator()
        {
            try
            {
                this.apiKey = Environment.GetEnvironmentVariable("DEEPL_API_KEY");

                if (string.IsNullOrEmpty(apiKey))
                    throw new Exception("APIキーが設定されていません。");
            }
            catch (Exception e)
            {
                throw new Exception("DeepL APIキーの取得に失敗しました。\n" + e);
            }
        }

        public static string DetectLang(string text)
        {
            foreach (char c in text)
            {
                if ((c >= '\u3040' && c <= '\u309F') || // ひらがな  
                    (c >= '\u30A0' && c <= '\u30FF') || // カタカナ  
                    (c >= '\u4E00' && c <= '\u9FFF') || // 漢字  
                    (c >= '\uFF66' && c <= '\uFF9F'))   // 半角カタカナ  
                {
                    return "JA";
                }
            }
            return "EN";
        }

        public async Task<string> TranslateAsync(string text)
        {
            string targetLang = DetectLang(text) == "JA" ? "EN" : "JA"; // 翻訳元が日本語のとき英語へ、英語のときは日本語へ

            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("auth_key", apiKey),
                new KeyValuePair<string, string>("text", text),
                new KeyValuePair<string, string>("target_lang", targetLang)
            });

            var response = await client.PostAsync(endpoint, content);
            var json = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(json);
            var translated = doc.RootElement
                .GetProperty("translations")[0]
                .GetProperty("text")
                .GetString();

            return translated;
        }
    }
}
