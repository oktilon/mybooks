namespace MyBooks
{
    partial class frmBill
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBill));
            this.tblBillBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsMain = new MyBooks.dsMain();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.cmdSave = new System.Windows.Forms.Button();
            this.gridBill = new SourceGrid.Grid();
            this.textCash = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblBack = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.rptView = new Microsoft.Reporting.WinForms.ReportViewer();
            this.chkReportViewer = new System.Windows.Forms.CheckBox();
            this.cmdAddItem = new System.Windows.Forms.Button();
            this.cntxtCmdAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddCarrier = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddTab = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEditCustomer = new System.Windows.Forms.Button();
            this.tmrDblClk = new System.Windows.Forms.Timer(this.components);
            this.SplitV = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.tblBillBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMain)).BeginInit();
            this.cntxtCmdAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitV)).BeginInit();
            this.SplitV.Panel1.SuspendLayout();
            this.SplitV.Panel2.SuspendLayout();
            this.SplitV.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblBillBindingSource
            // 
            this.tblBillBindingSource.DataMember = "tblBill";
            this.tblBillBindingSource.DataSource = this.dsMain;
            // 
            // dsMain
            // 
            this.dsMain.DataSetName = "dsMain";
            this.dsMain.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Tabs
            // 
            this.Tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tabs.Location = new System.Drawing.Point(7, 3);
            this.Tabs.Multiline = true;
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(534, 415);
            this.Tabs.TabIndex = 0;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cmdSave.Location = new System.Drawing.Point(645, 429);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(149, 23);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "Сохранить и закрыть";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // gridBill
            // 
            this.gridBill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBill.BackColor = System.Drawing.SystemColors.Control;
            this.gridBill.EnableSort = true;
            this.gridBill.Location = new System.Drawing.Point(3, 3);
            this.gridBill.Name = "gridBill";
            this.gridBill.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridBill.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.gridBill.Size = new System.Drawing.Size(316, 387);
            this.gridBill.TabIndex = 1;
            this.gridBill.TabStop = true;
            this.gridBill.Tag = "bill";
            this.gridBill.ToolTipText = "";
            // 
            // textCash
            // 
            this.textCash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textCash.Location = new System.Drawing.Point(176, 396);
            this.textCash.Name = "textCash";
            this.textCash.Size = new System.Drawing.Size(70, 22);
            this.textCash.TabIndex = 5;
            this.textCash.Text = "0";
            this.textCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textCash.TextChanged += new System.EventHandler(this.textCash_TextChanged);
            this.textCash.Enter += new System.EventHandler(this.textCash_Enter);
            this.textCash.MouseHover += new System.EventHandler(this.textCash_MouseHover);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 401);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ИТОГО :";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotal.ForeColor = System.Drawing.Color.Navy;
            this.lblTotal.Location = new System.Drawing.Point(64, 396);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(70, 22);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "0,00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBack
            // 
            this.lblBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBack.ForeColor = System.Drawing.Color.Maroon;
            this.lblBack.Location = new System.Drawing.Point(252, 396);
            this.lblBack.Name = "lblBack";
            this.lblBack.Size = new System.Drawing.Size(70, 22);
            this.lblBack.TabIndex = 6;
            this.lblBack.Text = "-";
            this.lblBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 401);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Нал:";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdCancel.ForeColor = System.Drawing.Color.Maroon;
            this.cmdCancel.Location = new System.Drawing.Point(800, 429);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 23);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "Отмена";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPrint.ForeColor = System.Drawing.Color.Purple;
            this.cmdPrint.Location = new System.Drawing.Point(12, 429);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(82, 23);
            this.cmdPrint.TabIndex = 7;
            this.cmdPrint.Text = "Печать чека";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // rptView
            // 
            this.rptView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "dsMain_tblBill";
            reportDataSource1.Value = this.tblBillBindingSource;
            this.rptView.LocalReport.DataSources.Add(reportDataSource1);
            this.rptView.LocalReport.ReportEmbeddedResource = "MyBooks.rptBill.rdlc";
            this.rptView.Location = new System.Drawing.Point(7, 3);
            this.rptView.Name = "rptView";
            this.rptView.Size = new System.Drawing.Size(534, 411);
            this.rptView.TabIndex = 8;
            this.rptView.Visible = false;
            // 
            // chkReportViewer
            // 
            this.chkReportViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkReportViewer.AutoSize = true;
            this.chkReportViewer.Location = new System.Drawing.Point(100, 433);
            this.chkReportViewer.Name = "chkReportViewer";
            this.chkReportViewer.Size = new System.Drawing.Size(133, 17);
            this.chkReportViewer.TabIndex = 9;
            this.chkReportViewer.Text = "Показать накладную";
            this.chkReportViewer.UseVisualStyleBackColor = true;
            this.chkReportViewer.CheckedChanged += new System.EventHandler(this.chkReportViewer_CheckedChanged);
            // 
            // cmdAddItem
            // 
            this.cmdAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddItem.Image = global::MyBooks.Properties.Resources.Add16;
            this.cmdAddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdAddItem.Location = new System.Drawing.Point(513, 429);
            this.cmdAddItem.Name = "cmdAddItem";
            this.cmdAddItem.Size = new System.Drawing.Size(83, 23);
            this.cmdAddItem.TabIndex = 11;
            this.cmdAddItem.Text = "Добавить";
            this.cmdAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAddItem.UseVisualStyleBackColor = true;
            this.cmdAddItem.Click += new System.EventHandler(this.cmdAddItem_Click);
            // 
            // cntxtCmdAdd
            // 
            this.cntxtCmdAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddItem,
            this.mnuAddPrint,
            this.mnuAddCarrier,
            this.mnuAddTab});
            this.cntxtCmdAdd.Name = "cntxtCmdAdd";
            this.cntxtCmdAdd.Size = new System.Drawing.Size(162, 92);
            // 
            // mnuAddItem
            // 
            this.mnuAddItem.Image = global::MyBooks.Properties.Resources.add_24;
            this.mnuAddItem.Name = "mnuAddItem";
            this.mnuAddItem.Size = new System.Drawing.Size(161, 22);
            this.mnuAddItem.Text = "Товар";
            this.mnuAddItem.Click += new System.EventHandler(this.mnuAddItem_Click);
            // 
            // mnuAddPrint
            // 
            this.mnuAddPrint.Image = global::MyBooks.Properties.Resources.Ink16;
            this.mnuAddPrint.Name = "mnuAddPrint";
            this.mnuAddPrint.Size = new System.Drawing.Size(161, 22);
            this.mnuAddPrint.Text = "Печать";
            this.mnuAddPrint.Click += new System.EventHandler(this.mnuAddPrint_Click);
            // 
            // mnuAddCarrier
            // 
            this.mnuAddCarrier.Image = global::MyBooks.Properties.Resources.documents16;
            this.mnuAddCarrier.Name = "mnuAddCarrier";
            this.mnuAddCarrier.Size = new System.Drawing.Size(161, 22);
            this.mnuAddCarrier.Text = "Бумагу (печать)";
            this.mnuAddCarrier.Click += new System.EventHandler(this.mnuAddCarrier_Click);
            // 
            // mnuAddTab
            // 
            this.mnuAddTab.Image = global::MyBooks.Properties.Resources.Tab16;
            this.mnuAddTab.Name = "mnuAddTab";
            this.mnuAddTab.Size = new System.Drawing.Size(161, 22);
            this.mnuAddTab.Text = "Закладку";
            this.mnuAddTab.Click += new System.EventHandler(this.mnuAddTab_Click);
            // 
            // cmdEditCustomer
            // 
            this.cmdEditCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdEditCustomer.Image = global::MyBooks.Properties.Resources.boss_16;
            this.cmdEditCustomer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdEditCustomer.Location = new System.Drawing.Point(239, 429);
            this.cmdEditCustomer.Name = "cmdEditCustomer";
            this.cmdEditCustomer.Size = new System.Drawing.Size(97, 23);
            this.cmdEditCustomer.TabIndex = 11;
            this.cmdEditCustomer.Text = "Покупатель";
            this.cmdEditCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEditCustomer.UseVisualStyleBackColor = true;
            this.cmdEditCustomer.Visible = false;
            this.cmdEditCustomer.Click += new System.EventHandler(this.cmdEditCustomer_Click);
            // 
            // tmrDblClk
            // 
            this.tmrDblClk.Interval = 1000;
            this.tmrDblClk.Tick += new System.EventHandler(this.tmrDblClk_Tick);
            // 
            // SplitV
            // 
            this.SplitV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitV.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitV.Location = new System.Drawing.Point(5, 5);
            this.SplitV.Name = "SplitV";
            // 
            // SplitV.Panel1
            // 
            this.SplitV.Panel1.Controls.Add(this.rptView);
            this.SplitV.Panel1.Controls.Add(this.Tabs);
            // 
            // SplitV.Panel2
            // 
            this.SplitV.Panel2.Controls.Add(this.gridBill);
            this.SplitV.Panel2.Controls.Add(this.lblBack);
            this.SplitV.Panel2.Controls.Add(this.textCash);
            this.SplitV.Panel2.Controls.Add(this.label1);
            this.SplitV.Panel2.Controls.Add(this.label3);
            this.SplitV.Panel2.Controls.Add(this.lblTotal);
            this.SplitV.Size = new System.Drawing.Size(870, 418);
            this.SplitV.SplitterDistance = 544;
            this.SplitV.TabIndex = 12;
            // 
            // frmBill
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(880, 459);
            this.Controls.Add(this.SplitV);
            this.Controls.Add(this.cmdAddItem);
            this.Controls.Add(this.cmdEditCustomer);
            this.Controls.Add(this.chkReportViewer);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(722, 328);
            this.Name = "frmBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Чек";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBill_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBill_FormClosed);
            this.Load += new System.EventHandler(this.frmBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblBillBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMain)).EndInit();
            this.cntxtCmdAdd.ResumeLayout(false);
            this.SplitV.Panel1.ResumeLayout(false);
            this.SplitV.Panel2.ResumeLayout(false);
            this.SplitV.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitV)).EndInit();
            this.SplitV.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private SourceGrid.Grid gridBill;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.TextBox textCash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblBack;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdPrint;
        private Microsoft.Reporting.WinForms.ReportViewer rptView;
        private System.Windows.Forms.BindingSource tblBillBindingSource;
        private dsMain dsMain;
        private System.Windows.Forms.CheckBox chkReportViewer;
        private System.Windows.Forms.Button cmdAddItem;
        private System.Windows.Forms.ContextMenuStrip cntxtCmdAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuAddTab;
        private System.Windows.Forms.ToolStripMenuItem mnuAddItem;
        private System.Windows.Forms.Button cmdEditCustomer;
        private System.Windows.Forms.Timer tmrDblClk;
		private System.Windows.Forms.SplitContainer SplitV;
		private System.Windows.Forms.ToolStripMenuItem mnuAddPrint;
		private System.Windows.Forms.ToolStripMenuItem mnuAddCarrier;
    }
}

