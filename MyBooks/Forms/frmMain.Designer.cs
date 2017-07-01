namespace MyBooks
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.icoNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnNew = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolBase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExit = new System.Windows.Forms.ToolStripMenuItem();
            this.stStrip = new System.Windows.Forms.StatusStrip();
            this.stStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.stStripProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.gridLst = new SourceGrid.Grid();
            this.btnReport = new System.Windows.Forms.Button();
            this.dtCurrent = new System.Windows.Forms.DateTimePicker();
            this.btnPrice = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.stStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // icoNotify
            // 
            this.icoNotify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.icoNotify.BalloonTipText = "Чеков за сегодня - 0шт.\r\nНовый чек - F12";
            this.icoNotify.BalloonTipTitle = "База чеков";
            this.icoNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("icoNotify.Icon")));
            this.icoNotify.Text = "База чеков";
            this.icoNotify.Visible = true;
            this.icoNotify.MouseClick += new System.Windows.Forms.MouseEventHandler(this.icoNotify_MouseClick);
            // 
            // btnNew
            // 
            this.btnNew.Image = global::MyBooks.Properties.Resources.BillNew32;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(12, 53);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(146, 42);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "Новый чек (F12)";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBase});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(394, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolBase
            // 
            this.toolBase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolExit});
            this.toolBase.Name = "toolBase";
            this.toolBase.Size = new System.Drawing.Size(43, 20);
            this.toolBase.Text = "База";
            // 
            // toolExit
            // 
            this.toolExit.Image = global::MyBooks.Properties.Resources.Exit16;
            this.toolExit.Name = "toolExit";
            this.toolExit.ShortcutKeyDisplayString = "Alt+F4";
            this.toolExit.Size = new System.Drawing.Size(150, 22);
            this.toolExit.Text = "Выход";
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // stStrip
            // 
            this.stStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stStripLabel,
            this.stStripProgress});
            this.stStrip.Location = new System.Drawing.Point(0, 292);
            this.stStrip.Name = "stStrip";
            this.stStrip.Size = new System.Drawing.Size(394, 22);
            this.stStrip.TabIndex = 2;
            this.stStrip.Text = "statusStrip1";
            // 
            // stStripLabel
            // 
            this.stStripLabel.Name = "stStripLabel";
            this.stStripLabel.Size = new System.Drawing.Size(277, 17);
            this.stStripLabel.Spring = true;
            this.stStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stStripProgress
            // 
            this.stStripProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stStripProgress.Name = "stStripProgress";
            this.stStripProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // gridLst
            // 
            this.gridLst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLst.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridLst.EnableSort = true;
            this.gridLst.Location = new System.Drawing.Point(164, 27);
            this.gridLst.Name = "gridLst";
            this.gridLst.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridLst.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.gridLst.Size = new System.Drawing.Size(218, 262);
            this.gridLst.TabIndex = 3;
            this.gridLst.TabStop = true;
            this.gridLst.Tag = "lst";
            this.gridLst.ToolTipText = "";
            // 
            // btnReport
            // 
            this.btnReport.Image = global::MyBooks.Properties.Resources.Report32;
            this.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.Location = new System.Drawing.Point(12, 101);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(146, 42);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "Отчет";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // dtCurrent
            // 
            this.dtCurrent.Location = new System.Drawing.Point(12, 27);
            this.dtCurrent.Name = "dtCurrent";
            this.dtCurrent.Size = new System.Drawing.Size(146, 20);
            this.dtCurrent.TabIndex = 4;
            this.dtCurrent.ValueChanged += new System.EventHandler(this.dtCurrent_ValueChanged);
            // 
            // btnPrice
            // 
            this.btnPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrice.Image = global::MyBooks.Properties.Resources.Coins32;
            this.btnPrice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrice.Location = new System.Drawing.Point(12, 199);
            this.btnPrice.Name = "btnPrice";
            this.btnPrice.Size = new System.Drawing.Size(146, 42);
            this.btnPrice.TabIndex = 0;
            this.btnPrice.Text = "Прайс";
            this.btnPrice.UseVisualStyleBackColor = true;
            this.btnPrice.Click += new System.EventHandler(this.btnPrice_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Image = global::MyBooks.Properties.Resources.Exit32;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(12, 247);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(146, 42);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOrders.Image = global::MyBooks.Properties.Resources.Pack32;
            this.btnOrders.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrders.Location = new System.Drawing.Point(12, 151);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(146, 42);
            this.btnOrders.TabIndex = 0;
            this.btnOrders.Text = "Заказы";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(394, 314);
            this.Controls.Add(this.dtCurrent);
            this.Controls.Add(this.gridLst);
            this.Controls.Add(this.stStrip);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.btnPrice);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(410, 350);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "База";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.stStrip.ResumeLayout(false);
            this.stStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon icoNotify;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolBase;
        private System.Windows.Forms.ToolStripMenuItem toolExit;
        private System.Windows.Forms.StatusStrip stStrip;
        private System.Windows.Forms.ToolStripStatusLabel stStripLabel;
        public System.Windows.Forms.ToolStripProgressBar stStripProgress;
        private SourceGrid.Grid gridLst;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.DateTimePicker dtCurrent;
        private System.Windows.Forms.Button btnPrice;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnOrders;
    }
}