using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace win_util_tool
{
    public class HotKeyForm : Form{
        const int  HOTKEY_ID   = 1;
        const int  WM_HOTKEY   = 0x0312;
        const uint MOD_CONTROL = 0x0002;
        const uint MOD_SHIFT   = 0x0004;
        const uint MOD_NOREPEAT = 0x4000;


        [DllImport("user32.dll")] extern static int RegisterHotKey(IntPtr hWnd, int id, uint modKey, uint key);
        [DllImport("user32.dll")] extern static int UnregisterHotKey(IntPtr HWnd, int id);


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // hotkeyformの表示を抑制
            this.Visible         = false;
            this.ShowInTaskbar   = false;
            this.Opacity         = 0;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState     = FormWindowState.Minimized;
            this.Hide();

            // Ctrl+Shift+S でWndProcが呼ばれる。
            if (RegisterHotKey(this.Handle, HOTKEY_ID, MOD_CONTROL | MOD_SHIFT | MOD_NOREPEAT, (uint)Keys.C) == 0) {
                MessageBox.Show("ホットキーの取得に失敗しました。","エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                UnregisterHotKey(this.Handle, HOTKEY_ID);

            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                Invoke((MethodInvoker)(async () => {
                    SendKeys.SendWait("^{c}");
                    await Task.Delay(300);

                    var form = new Form1(Clipboard.GetText());
                    form.Show();
                    form.Activate();
                }));
            }
            base.WndProc(ref m);
        }
    }
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HotKeyForm());
        }
    }
}
