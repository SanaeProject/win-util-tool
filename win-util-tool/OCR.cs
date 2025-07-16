using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace win_util_tool
{
    public partial class OCR : Form
    {
        Point?     moving      = null;
        Rectangle? currentRect = null;

        public OCR()
        {
            InitializeComponent();
        }

        private void OCR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if(e.KeyCode == Keys.Enter)
            {
                string tessdata = Environment.GetEnvironmentVariable("TESSDATA_PATH");
                string lng      = "jpn+eng";

                if (tessdata == null)
                {
                    MessageBox.Show("TESSDATA_PATH環境変数が設定されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (
                    !Directory.Exists(tessdata) || 
                    !File.Exists(Path.Combine(tessdata, "jpn.traineddata")) ||
                    !File.Exists(Path.Combine(tessdata, "eng.traineddata")))
                {
                    MessageBox.Show("tessdataフォルダに言語データがありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Clipboard.SetText(OCRWithScreenShot(tessdata, lng).Replace("\n","\r\n"));
                SendKeys.SendWait("^+(c)"); // 検索
                this.Close();
            }
        }

        private void OCR_MouseDown(object sender, MouseEventArgs e)
        {
            moving      = new Point(e.X, e.Y);
            currentRect = null;
        }

        private void OCR_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving == null)
                return;

            int width  = e.X - moving.Value.X;
            int height = e.Y - moving.Value.Y;

            currentRect = new Rectangle(moving.Value.X, moving.Value.Y, width, height);

            this.Invalidate();
        }

        private void OCR_Paint(object sender, PaintEventArgs e)
        {
            if (currentRect.HasValue)
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(128, 0, 0, 255)))
                {
                    e.Graphics.FillRectangle(brush, currentRect.Value);
                }
            }
        }

        private void OCR_MouseUp(object sender, MouseEventArgs e)
        {
            moving = null;
        }

        private Bitmap getScreenShot()
        {
            Rectangle rect = currentRect.Value;
            Bitmap    bmp  = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(bmp))
                g.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);

            // ImagePreviewDialog.Show(bmp, "OCRプレビュー");

            return bmp;
        }

        private string OCRWithScreenShot(string tessdata,string lng)
        {
            this.Opacity = 0.0;
            Bitmap bmp = getScreenShot();
            try
            {
                using (var engine = new TesseractEngine(tessdata, lng, EngineMode.Default))
                {
                    using (var img = PixConverter.ToPix(bmp))
                    {
                        using (var page = engine.Process(img))
                        {
                            return page.GetText() ?? "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "エラー:" + ex.Message + "\r\n" + ex.InnerException?.Message;
            }
        }
    }
}
