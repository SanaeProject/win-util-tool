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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.formWrapper = new System.Windows.Forms.SplitContainer();
            this.search = new System.Windows.Forms.TextBox();
            this.resultWrapper = new System.Windows.Forms.SplitContainer();
            this.result = new System.Windows.Forms.TextBox();
            this.webResult = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.formWrapper)).BeginInit();
            this.formWrapper.Panel1.SuspendLayout();
            this.formWrapper.Panel2.SuspendLayout();
            this.formWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultWrapper)).BeginInit();
            this.resultWrapper.Panel1.SuspendLayout();
            this.resultWrapper.Panel2.SuspendLayout();
            this.resultWrapper.SuspendLayout();
            this.SuspendLayout();
            // 
            // formWrapper
            // 
            resources.ApplyResources(this.formWrapper, "formWrapper");
            this.formWrapper.Name = "formWrapper";
            // 
            // formWrapper.Panel1
            // 
            resources.ApplyResources(this.formWrapper.Panel1, "formWrapper.Panel1");
            this.formWrapper.Panel1.Controls.Add(this.search);
            // 
            // formWrapper.Panel2
            // 
            resources.ApplyResources(this.formWrapper.Panel2, "formWrapper.Panel2");
            this.formWrapper.Panel2.Controls.Add(this.resultWrapper);
            // 
            // search
            // 
            resources.ApplyResources(this.search, "search");
            this.search.Name = "search";
            this.search.TabStop = false;
            this.search.TextChanged += new System.EventHandler(this.search_TextChanged);
            // 
            // resultWrapper
            // 
            resources.ApplyResources(this.resultWrapper, "resultWrapper");
            this.resultWrapper.BackColor = System.Drawing.SystemColors.Control;
            this.resultWrapper.Name = "resultWrapper";
            // 
            // resultWrapper.Panel1
            // 
            resources.ApplyResources(this.resultWrapper.Panel1, "resultWrapper.Panel1");
            this.resultWrapper.Panel1.Controls.Add(this.result);
            // 
            // resultWrapper.Panel2
            // 
            resources.ApplyResources(this.resultWrapper.Panel2, "resultWrapper.Panel2");
            this.resultWrapper.Panel2.Controls.Add(this.webResult);
            this.resultWrapper.TabStop = false;
            // 
            // result
            // 
            resources.ApplyResources(this.result, "result");
            this.result.BackColor = System.Drawing.SystemColors.Window;
            this.result.Name = "result";
            this.result.ReadOnly = true;
            this.result.KeyDown += new System.Windows.Forms.KeyEventHandler(this.result_KeyDown);
            // 
            // webResult
            // 
            resources.ApplyResources(this.webResult, "webResult");
            this.webResult.Name = "webResult";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.formWrapper);
            this.Name = "Form1";
            this.Opacity = 0.8D;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.formWrapper.Panel1.ResumeLayout(false);
            this.formWrapper.Panel1.PerformLayout();
            this.formWrapper.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.formWrapper)).EndInit();
            this.formWrapper.ResumeLayout(false);
            this.resultWrapper.Panel1.ResumeLayout(false);
            this.resultWrapper.Panel1.PerformLayout();
            this.resultWrapper.Panel2.ResumeLayout(false);
            this.resultWrapper.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultWrapper)).EndInit();
            this.resultWrapper.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer formWrapper;
        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.SplitContainer resultWrapper;
        private System.Windows.Forms.TextBox result;
        private System.Windows.Forms.TextBox webResult;
    }
}

