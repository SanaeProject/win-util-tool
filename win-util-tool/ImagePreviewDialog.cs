using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace win_util_tool
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public static class ImagePreviewDialog
    {
        public static DialogResult Show(Bitmap bitmap, string title = "画像プレビュー")
        {
            if (bitmap == null)
            {
                MessageBox.Show("表示する画像がありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return DialogResult.Abort;
            }

            using (Form previewForm = new Form())
            {
                previewForm.Text = title;
                previewForm.StartPosition = FormStartPosition.CenterParent;
                previewForm.ClientSize = new Size(800, 600);

                PictureBox pictureBox = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = (Bitmap)bitmap.Clone()
                };

                previewForm.Controls.Add(pictureBox);

                Button closeButton = new Button
                {
                    Text = "閉じる",
                    Dock = DockStyle.Bottom,
                    Height = 40
                };
                closeButton.Click += (s, e) => previewForm.DialogResult = DialogResult.OK;
                previewForm.Controls.Add(closeButton);

                return previewForm.ShowDialog();
            }
        }
    }
}
