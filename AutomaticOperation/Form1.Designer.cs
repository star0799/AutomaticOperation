namespace AutomaticOperation
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tbShow = new System.Windows.Forms.TextBox();
            this.btClear = new System.Windows.Forms.Button();
            this.bttest = new System.Windows.Forms.Button();
            this.tbtest = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.webBrowser1.Location = new System.Drawing.Point(0, 115);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(1070, 614);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted_1);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // tbShow
            // 
            this.tbShow.Location = new System.Drawing.Point(125, 12);
            this.tbShow.Multiline = true;
            this.tbShow.Name = "tbShow";
            this.tbShow.ReadOnly = true;
            this.tbShow.Size = new System.Drawing.Size(259, 97);
            this.tbShow.TabIndex = 7;
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(12, 77);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 9;
            this.btClear.Text = "清除";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // bttest
            // 
            this.bttest.Location = new System.Drawing.Point(12, 28);
            this.bttest.Name = "bttest";
            this.bttest.Size = new System.Drawing.Size(75, 23);
            this.bttest.TabIndex = 10;
            this.bttest.Text = "一鍵審核";
            this.bttest.UseVisualStyleBackColor = true;
            this.bttest.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbtest
            // 
            this.tbtest.Location = new System.Drawing.Point(390, 12);
            this.tbtest.Multiline = true;
            this.tbtest.Name = "tbtest";
            this.tbtest.Size = new System.Drawing.Size(407, 97);
            this.tbtest.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 729);
            this.Controls.Add(this.tbtest);
            this.Controls.Add(this.bttest);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.tbShow);
            this.Controls.Add(this.webBrowser1);
            this.Name = "Form1";
            this.Text = "自動審核var2.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox tbShow;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button bttest;
        private System.Windows.Forms.TextBox tbtest;
    }
}

