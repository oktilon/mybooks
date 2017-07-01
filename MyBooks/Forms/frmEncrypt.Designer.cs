namespace MyBooks
{
    partial class frmEncrypt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEncrypt));
            this.lblSrc = new System.Windows.Forms.Label();
            this.txSrc = new System.Windows.Forms.TextBox();
            this.lblDst = new System.Windows.Forms.Label();
            this.txDst = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSrc
            // 
            this.lblSrc.AutoSize = true;
            this.lblSrc.Location = new System.Drawing.Point(12, 15);
            this.lblSrc.Name = "lblSrc";
            this.lblSrc.Size = new System.Drawing.Size(89, 13);
            this.lblSrc.TabIndex = 0;
            this.lblSrc.Text = "Исходный текст";
            // 
            // txSrc
            // 
            this.txSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txSrc.Location = new System.Drawing.Point(107, 12);
            this.txSrc.Name = "txSrc";
            this.txSrc.Size = new System.Drawing.Size(322, 20);
            this.txSrc.TabIndex = 1;
            this.txSrc.TextChanged += new System.EventHandler(this.txSrc_TextChanged);
            // 
            // lblDst
            // 
            this.lblDst.AutoSize = true;
            this.lblDst.Location = new System.Drawing.Point(12, 41);
            this.lblDst.Name = "lblDst";
            this.lblDst.Size = new System.Drawing.Size(80, 13);
            this.lblDst.TabIndex = 0;
            this.lblDst.Text = "Шифрованный";
            // 
            // txDst
            // 
            this.txDst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txDst.Location = new System.Drawing.Point(107, 38);
            this.txDst.Name = "txDst";
            this.txDst.ReadOnly = true;
            this.txDst.Size = new System.Drawing.Size(322, 20);
            this.txDst.TabIndex = 1;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(343, 65);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(86, 23);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Закрыть";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // cmdCopy
            // 
            this.cmdCopy.Location = new System.Drawing.Point(107, 64);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(86, 23);
            this.cmdCopy.TabIndex = 2;
            this.cmdCopy.Text = "Копировать";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // frmEncrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(441, 100);
            this.Controls.Add(this.cmdCopy);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.txDst);
            this.Controls.Add(this.lblDst);
            this.Controls.Add(this.txSrc);
            this.Controls.Add(this.lblSrc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEncrypt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Encrypt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSrc;
        private System.Windows.Forms.TextBox txSrc;
        private System.Windows.Forms.Label lblDst;
        private System.Windows.Forms.TextBox txDst;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdCopy;
    }
}