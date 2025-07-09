namespace win_util_tool
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.search = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.result = new System.Windows.Forms.TextBox();
            this.webResult = new System.Windows.Forms.TextBox();
            this.searchLink = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // search
            // 
            this.search.Dock = System.Windows.Forms.DockStyle.Top;
            this.search.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.search.Location = new System.Drawing.Point(0, 0);
            this.search.Multiline = true;
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(508, 65);
            this.search.TabIndex = 0;
            this.search.TabStop = false;
            this.search.TextChanged += new System.EventHandler(this.search_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.splitContainer1.Location = new System.Drawing.Point(0, 65);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.result);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webResult);
            this.splitContainer1.Size = new System.Drawing.Size(508, 353);
            this.splitContainer1.SplitterDistance = 168;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // result
            // 
            this.result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.result.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.result.Location = new System.Drawing.Point(0, 0);
            this.result.Multiline = true;
            this.result.Name = "result";
            this.result.ReadOnly = true;
            this.result.Size = new System.Drawing.Size(168, 353);
            this.result.TabIndex = 0;
            this.result.KeyDown += new System.Windows.Forms.KeyEventHandler(this.result_KeyDown);
            // 
            // webResult
            // 
            this.webResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webResult.Location = new System.Drawing.Point(0, 0);
            this.webResult.MinimumSize = new System.Drawing.Size(20, 20);
            this.webResult.Multiline = true;
            this.webResult.Name = "webResult";
            this.webResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.webResult.Size = new System.Drawing.Size(336, 353);
            this.webResult.TabIndex = 0;
            // 
            // searchLink
            // 
            this.searchLink.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchLink.Font = new System.Drawing.Font("MS UI Gothic", 13F);
            this.searchLink.Location = new System.Drawing.Point(0, 418);
            this.searchLink.Name = "searchLink";
            this.searchLink.Size = new System.Drawing.Size(508, 32);
            this.searchLink.TabIndex = 2;
            this.searchLink.TabStop = true;
            this.searchLink.Text = "Google検索";
            this.searchLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.searchLink_LinkClicked);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.searchLink);
            this.Controls.Add(this.search);
            this.Name = "Form1";
            this.Opacity = 0.8D;
            this.Text = "GetInfo";
            this.TopMost = true;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox result;
        private System.Windows.Forms.TextBox webResult;
        private System.Windows.Forms.LinkLabel searchLink;
        private System.Windows.Forms.Timer timer1;
    }
}

