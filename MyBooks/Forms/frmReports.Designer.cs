namespace MyBooks
{
    partial class frmReports
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReports));
			this.tblReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dsMain = new MyBooks.dsMain();
			this.dtFrom = new System.Windows.Forms.DateTimePicker();
			this.rptView = new Microsoft.Reporting.WinForms.ReportViewer();
			this.label1 = new System.Windows.Forms.Label();
			this.dtTo = new System.Windows.Forms.DateTimePicker();
			this.btnPrintClose = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.optDay = new System.Windows.Forms.RadioButton();
			this.optWeek = new System.Windows.Forms.RadioButton();
			this.optMonth = new System.Windows.Forms.RadioButton();
			this.optYear = new System.Windows.Forms.RadioButton();
			this.optPeriod = new System.Windows.Forms.RadioButton();
			this.lblFrom = new System.Windows.Forms.Label();
			this.lblTo = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.tblReportBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsMain)).BeginInit();
			this.SuspendLayout();
			// 
			// tblReportBindingSource
			// 
			this.tblReportBindingSource.DataMember = "tblReport";
			this.tblReportBindingSource.DataSource = this.dsMain;
			// 
			// dsMain
			// 
			this.dsMain.DataSetName = "dsMain";
			this.dsMain.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// dtFrom
			// 
			this.dtFrom.CustomFormat = "MMMM";
			this.dtFrom.Location = new System.Drawing.Point(88, 34);
			this.dtFrom.Name = "dtFrom";
			this.dtFrom.Size = new System.Drawing.Size(200, 20);
			this.dtFrom.TabIndex = 7;
			this.dtFrom.ValueChanged += new System.EventHandler(this.dtFrom_ValueChanged);
			// 
			// rptView
			// 
			this.rptView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			reportDataSource1.Name = "dsMain_tblReport";
			reportDataSource1.Value = this.tblReportBindingSource;
			this.rptView.LocalReport.DataSources.Add(reportDataSource1);
			this.rptView.LocalReport.ReportEmbeddedResource = "MyBooks.rptReport.rdl";
			this.rptView.Location = new System.Drawing.Point(12, 60);
			this.rptView.Name = "rptView";
			this.rptView.Size = new System.Drawing.Size(750, 446);
			this.rptView.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Отчет за";
			// 
			// dtTo
			// 
			this.dtTo.Location = new System.Drawing.Point(327, 34);
			this.dtTo.Name = "dtTo";
			this.dtTo.Size = new System.Drawing.Size(200, 20);
			this.dtTo.TabIndex = 9;
			this.dtTo.Visible = false;
			this.dtTo.ValueChanged += new System.EventHandler(this.dtTo_ValueChanged);
			// 
			// btnPrintClose
			// 
			this.btnPrintClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrintClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnPrintClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.btnPrintClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.btnPrintClose.Location = new System.Drawing.Point(516, 514);
			this.btnPrintClose.Name = "btnPrintClose";
			this.btnPrintClose.Size = new System.Drawing.Size(165, 23);
			this.btnPrintClose.TabIndex = 11;
			this.btnPrintClose.Text = "Распечатать и закрыть";
			this.btnPrintClose.UseVisualStyleBackColor = true;
			this.btnPrintClose.Click += new System.EventHandler(this.btnPrintClose_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.btnClose.Location = new System.Drawing.Point(687, 514);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 12;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// optDay
			// 
			this.optDay.AutoSize = true;
			this.optDay.Checked = true;
			this.optDay.Location = new System.Drawing.Point(70, 12);
			this.optDay.Name = "optDay";
			this.optDay.Size = new System.Drawing.Size(49, 17);
			this.optDay.TabIndex = 1;
			this.optDay.TabStop = true;
			this.optDay.Text = "день";
			this.optDay.UseVisualStyleBackColor = true;
			this.optDay.CheckedChanged += new System.EventHandler(this.optDay_CheckedChanged);
			// 
			// optWeek
			// 
			this.optWeek.AutoSize = true;
			this.optWeek.Location = new System.Drawing.Point(125, 12);
			this.optWeek.Name = "optWeek";
			this.optWeek.Size = new System.Drawing.Size(63, 17);
			this.optWeek.TabIndex = 2;
			this.optWeek.Text = "неделю";
			this.optWeek.UseVisualStyleBackColor = true;
			this.optWeek.CheckedChanged += new System.EventHandler(this.optWeek_CheckedChanged);
			// 
			// optMonth
			// 
			this.optMonth.AutoSize = true;
			this.optMonth.Location = new System.Drawing.Point(194, 12);
			this.optMonth.Name = "optMonth";
			this.optMonth.Size = new System.Drawing.Size(57, 17);
			this.optMonth.TabIndex = 3;
			this.optMonth.TabStop = true;
			this.optMonth.Text = "месяц";
			this.optMonth.UseVisualStyleBackColor = true;
			this.optMonth.CheckedChanged += new System.EventHandler(this.optMonth_CheckedChanged);
			// 
			// optYear
			// 
			this.optYear.AutoSize = true;
			this.optYear.Location = new System.Drawing.Point(257, 12);
			this.optYear.Name = "optYear";
			this.optYear.Size = new System.Drawing.Size(42, 17);
			this.optYear.TabIndex = 4;
			this.optYear.TabStop = true;
			this.optYear.Text = "год";
			this.optYear.UseVisualStyleBackColor = true;
			this.optYear.CheckedChanged += new System.EventHandler(this.optYear_CheckedChanged);
			// 
			// optPeriod
			// 
			this.optPeriod.AutoSize = true;
			this.optPeriod.Location = new System.Drawing.Point(305, 12);
			this.optPeriod.Name = "optPeriod";
			this.optPeriod.Size = new System.Drawing.Size(138, 17);
			this.optPeriod.TabIndex = 5;
			this.optPeriod.TabStop = true;
			this.optPeriod.Text = "произвольный период";
			this.optPeriod.UseVisualStyleBackColor = true;
			this.optPeriod.CheckedChanged += new System.EventHandler(this.optPeriod_CheckedChanged);
			// 
			// lblFrom
			// 
			this.lblFrom.Location = new System.Drawing.Point(9, 38);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(73, 13);
			this.lblFrom.TabIndex = 6;
			this.lblFrom.Text = "День :";
			this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblTo
			// 
			this.lblTo.AutoSize = true;
			this.lblTo.Location = new System.Drawing.Point(294, 38);
			this.lblTo.Name = "lblTo";
			this.lblTo.Size = new System.Drawing.Size(27, 13);
			this.lblTo.TabIndex = 8;
			this.lblTo.Text = "По :";
			this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblTo.Visible = false;
			// 
			// frmReports
			// 
			this.AcceptButton = this.btnPrintClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(774, 549);
			this.Controls.Add(this.lblTo);
			this.Controls.Add(this.lblFrom);
			this.Controls.Add(this.optPeriod);
			this.Controls.Add(this.optYear);
			this.Controls.Add(this.optMonth);
			this.Controls.Add(this.optWeek);
			this.Controls.Add(this.optDay);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnPrintClose);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rptView);
			this.Controls.Add(this.dtTo);
			this.Controls.Add(this.dtFrom);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(790, 490);
			this.Name = "frmReports";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Отчет";
			this.Load += new System.EventHandler(this.frmReports_Load);
			((System.ComponentModel.ISupportInitialize)(this.tblReportBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtFrom;
        private Microsoft.Reporting.WinForms.ReportViewer rptView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.BindingSource tblReportBindingSource;
        private dsMain dsMain;
        private System.Windows.Forms.Button btnPrintClose;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton optDay;
        private System.Windows.Forms.RadioButton optWeek;
        private System.Windows.Forms.RadioButton optMonth;
        private System.Windows.Forms.RadioButton optYear;
        private System.Windows.Forms.RadioButton optPeriod;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
    }
}