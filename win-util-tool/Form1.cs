using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Chrome;


namespace win_util_tool
{
    public partial class Form1 : Form
    {
        public static readonly HttpClient client      = new HttpClient();
        private static readonly Translator translator = new Translator(client);
        // private static readonly Wiktionary wikitionary = new Wiktionary(client);
        private string searchUrl = "https://www.google.com/search?q=";

        public Form1(string text)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Generate_an_icon_for;

            this.Shown += (s,e)=> { 
                search.Text = text; 
                result.Select(); 
                render(text); 
            };
        }

        public void render(string text)
        {
            searchUrl = "https://www.google.com/search?q=" + Uri.EscapeDataString(text);
            this.Invoke((MethodInvoker)(async () =>
            {
                string translated = string.Empty;

                if (text.Length < 100 && Translator.isValid)
                    translated = await translator.TranslateAsync(text);
                else if(text.Length > 100 && Translator.isValid)
                    translated = "100文字を超えているため翻訳を中止しました。\r\n続行するには\"T\"を押してください。";
                else
                    translated = "翻訳APIキーが設定されていません。\r\nシステム環境変数に\"DEEPL_API_KEY\"を設定してください。";

                if (!result.IsDisposed && !result.Disposing && translated != string.Empty)
                    result.Text = translated;

                string html = await GoogleWithWiki.search(text);
                string plainText = Regex.Replace(html, "<br.?>", "\r\n");
                plainText = Regex.Replace(plainText, "<.*?>", string.Empty);
                plainText = System.Net.WebUtility.HtmlDecode(plainText);

                if (!webResult.IsDisposed && !webResult.Disposing)
                {
                    webResult.Text = plainText;
                }
            }));
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
                // Tキーで翻訳を実行
                Invoke((MethodInvoker)(async () => {
                    if (!Translator.isValid)
                    {
                        MessageBox.Show("翻訳APIキーが設定されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                        
                    string translated = await translator.TranslateAsync(search.Text);

                    if (!result.IsDisposed && !result.Disposing)
                        result.Text = translated;
                }));
            }
            else if((e.Shift && e.KeyCode == Keys.Enter) || e.KeyCode == Keys.Escape)
            {
                // Shift+Enter または Escキーでアプリケーションを終了
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                // EnterキーでGoogle検索を開く
                System.Diagnostics.Process.Start(this.searchUrl);
                this.Close();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                Environment.Exit(0); // アプリケーションを終了
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            formWrapper.SplitterDistance = formWrapper.Height / 4;
            resultWrapper.SplitterDistance = resultWrapper.Width / 2;
        }
    }

    public static class Browser
    {
        private static OpenQA.Selenium.IWebDriver driver;
        private static readonly SemaphoreSlim semaphore  = new SemaphoreSlim(1, 1);

        public static OpenQA.Selenium.IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    driver = CreateDriver();
                }
                return driver;
            }
        }

        private static OpenQA.Selenium.IWebDriver CreateDriver()
        {
            try
            {
                var service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                var options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");

                options.AddArgument("--disable-blink-features=AutomationControlled");
                options.AddExcludedArgument("enable-automation");
                options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");

                return new ChromeDriver(service, options);
            }
            catch (Exception ex)
            {
                if (MessageBox.Show($"ブラウザの起動に失敗しました。\r\n{ex.Message}\r\n再試行しますか？", "エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                {
                    Environment.Exit(1); // アプリケーションを終了
                }

                return CreateDriver(); // 再帰的に再試行
            }
        }

        public static string GetHtml(string url)
        {
            Driver.Navigate().GoToUrl(url);
            return Driver.PageSource;
        }

        public static async Task<string> GetHtmlWithWait(string url, int waitTime = 3000)
        {
            await semaphore.WaitAsync();

            try
            {
                Driver.Navigate().GoToUrl(url);
                await Task.Delay(waitTime);
                return Driver.PageSource;
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static async Task<string> GetHtmlWithWait(string url, string xpath, int maxTry = 8)
        {
            await semaphore.WaitAsync();

            try
            {
                Driver.Navigate().GoToUrl(url);

                OpenQA.Selenium.IWebElement element = null;

                for (int i = 0; i < maxTry; i++)
                {
                    try
                    {
                        element = Driver.FindElement(OpenQA.Selenium.By.XPath(xpath));
                        if (element != null)
                            break;
                    }
                    catch (OpenQA.Selenium.NoSuchElementException){}

                    await Task.Delay(1000);
                }

                if (element == null)
                    throw new Exception($"指定されたXPath '{xpath}' が {maxTry} 回の待機後にも見つかりませんでした。");

                return element.GetAttribute("outerHTML");
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static async Task<string> GetHtmlsWithWait(string url, string xpath, int eleCount = 3,int maxTry = 8,string suffix = "\r\n\r\n")
        {
            await semaphore.WaitAsync();

            try
            {
                Driver.Navigate().GoToUrl(url);

                IReadOnlyCollection<OpenQA.Selenium.IWebElement> elements = null;

                for (int i = 0; i < maxTry; i++)
                {
                    try
                    {
                        elements = Driver.FindElements(OpenQA.Selenium.By.XPath(xpath));
                        if (elements != null && elements.Count > 0)
                            break;
                    }
                    catch (OpenQA.Selenium.NoSuchElementException) { }

                    await Task.Delay(1000);
                }

                if (elements == null || elements.Count == 0)
                    throw new Exception($"指定されたXPath '{xpath}' が {maxTry} 回の待機後にも見つかりませんでした。");

                return string.Join(suffix, elements
                    .Take(eleCount)
                    .Select(e => e.GetAttribute("outerHTML")));
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static void Dispose()
        {
            driver?.Quit();
            driver = null;
        }
    }
    public static class GoogleWithWiki
    {
        public static async Task<string> search(string text)
        {
            try
            {
                return await Browser.GetHtmlsWithWait("https://search.yahoo.co.jp/search?p=" + Uri.EscapeDataString(text) + " site:wikipedia.org", "//*[contains(@class, 'sw-Card__summary')]", 5, 3);
            }
            catch (HttpRequestException ex)
            {
                return "HttpRequestException: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "予期せぬエラー: " + ex.Message;
            }
        }
    }

    public class Wiktionary {
        readonly HttpClient client;

        public Wiktionary(HttpClient client) {
            this.client = client;
        }

        public async Task<string> search(string text) {
            try
            {
                string html = await client.GetStringAsync("https://" + Translator.DetectLang(text).ToLower() + ".wiktionary.org/wiki/" + text);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                var targetNode = doc.DocumentNode.SelectSingleNode("//div[@id='bodyContent']");
                string extractedHtml = targetNode?.OuterHtml ?? "<p>要素が見つかりませんでした。</p>";

                return extractedHtml;
            }
            catch (HttpRequestException ex)
            {
                return "HttpRequestException: "+ex.Message;
            }
            catch (Exception ex) {
                return "予期せぬエラー: " + ex.Message;
            }
        }
    }
    public class Translator
    {
        readonly HttpClient client;
        protected static readonly string apiKey   = Environment.GetEnvironmentVariable("DEEPL_API_KEY");
        protected static readonly string endpoint = "https://api-free.deepl.com/v2/translate";

        public static bool isValid {
            get
            {
                return apiKey != null;
            }
        }

        public Translator(HttpClient client) {
            this.client = client;
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
            if (apiKey == null)
                throw new UnauthorizedAccessException("APIキーが設定されていません。");

            string targetLang = DetectLang(text) == "JA" ? "EN" : "JA"; // 翻訳元が日本語のとき英語へ、英語のときは日本語
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("auth_key", apiKey),
                new KeyValuePair<string, string>("text", text),
                new KeyValuePair<string, string>("target_lang", targetLang)
            });

            string translated = null;
            try
            {
                var response = await client.PostAsync(endpoint, content);
                var json = await response.Content.ReadAsStringAsync();

                var doc = JsonDocument.Parse(json);
                translated = doc.RootElement
                    .GetProperty("translations")[0]
                    .GetProperty("text")
                    .GetString();
            }
            catch (HttpRequestException ex)
            {
                return "HTTPリクエストエラー: " + ex.Message;
            }
            catch (JsonException ex)
            {
                return "JSON解析エラー: " + ex.Message;
            }
            catch(Exception ex)
            {
                return "予期せぬエラー: " + ex.Message;
            }

            return translated;
        }
    }
}
