namespace MyBooks
{
    partial class frmPrice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrice));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblShort = new System.Windows.Forms.Label();
            this.txtShort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.imgl = new System.Windows.Forms.ImageList(this.components);
            this.cbIcon = new System.Windows.Forms.ComboBox();
            this.chkUse = new System.Windows.Forms.CheckBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.grid = new SourceGrid.Grid();
            this.split = new System.Windows.Forms.SplitContainer();
            this.split2 = new System.Windows.Forms.SplitContainer();
            this.lblTop = new System.Windows.Forms.Label();
            this.gridTop = new SourceGrid.Grid();
            this.cmdBase = new System.Windows.Forms.Button();
            this.cmdMin = new System.Windows.Forms.Button();
            this.gridBot = new SourceGrid.Grid();
            this.lblBot = new System.Windows.Forms.Label();
            this.lblBase = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.cmdDelPrice = new System.Windows.Forms.Button();
            this.cmdAddPrice = new System.Windows.Forms.Button();
            this.lblNameUa = new System.Windows.Forms.Label();
            this.txtNameUa = new System.Windows.Forms.TextBox();
            this.lblArt = new System.Windows.Forms.Label();
            this.txtArt = new System.Windows.Forms.TextBox();
            this.lblTypeCap = new System.Windows.Forms.Label();
            this.lblFmt = new System.Windows.Forms.Label();
            this.cmdFmt = new System.Windows.Forms.Button();
            this.lblDen = new System.Windows.Forms.Label();
            this.cmdDen = new System.Windows.Forms.Button();
            this.lblSurf = new System.Windows.Forms.Label();
            this.cmdSurf = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split2)).BeginInit();
            this.split2.Panel1.SuspendLayout();
            this.split2.Panel2.SuspendLayout();
            this.split2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(5, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(36, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Назв.";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(47, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(478, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblShort
            // 
            this.lblShort.AutoSize = true;
            this.lblShort.Location = new System.Drawing.Point(5, 45);
            this.lblShort.Name = "lblShort";
            this.lblShort.Size = new System.Drawing.Size(43, 13);
            this.lblShort.TabIndex = 2;
            this.lblShort.Text = "Кратко";
            // 
            // txtShort
            // 
            this.txtShort.Location = new System.Drawing.Point(54, 41);
            this.txtShort.Name = "txtShort";
            this.txtShort.Size = new System.Drawing.Size(185, 20);
            this.txtShort.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Цена по точкам:";
            // 
            // imgl
            // 
            this.imgl.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgl.ImageSize = new System.Drawing.Size(16, 16);
            this.imgl.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cbIcon
            // 
            this.cbIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbIcon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIcon.FormattingEnabled = true;
            this.cbIcon.ItemHeight = 20;
            this.cbIcon.Location = new System.Drawing.Point(74, 420);
            this.cbIcon.Name = "cbIcon";
            this.cbIcon.Size = new System.Drawing.Size(49, 26);
            this.cbIcon.TabIndex = 22;
            this.cbIcon.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbIcon_DrawItem);
            // 
            // chkUse
            // 
            this.chkUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUse.AutoSize = true;
            this.chkUse.Location = new System.Drawing.Point(531, 14);
            this.chkUse.Name = "chkUse";
            this.chkUse.Size = new System.Drawing.Size(103, 17);
            this.chkUse.TabIndex = 10;
            this.chkUse.Text = "Используемый";
            this.chkUse.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdCancel.ForeColor = System.Drawing.Color.Maroon;
            this.cmdCancel.Location = new System.Drawing.Point(558, 420);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 23);
            this.cmdCancel.TabIndex = 24;
            this.cmdCancel.Text = "Отмена";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cmdOk.Location = new System.Drawing.Point(484, 420);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(68, 23);
            this.cmdOk.TabIndex = 23;
            this.cmdOk.Text = "ОК";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid.EnableSort = true;
            this.grid.FixedColumns = 1;
            this.grid.FixedRows = 1;
            this.grid.Location = new System.Drawing.Point(0, 16);
            this.grid.Name = "grid";
            this.grid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.grid.Size = new System.Drawing.Size(350, 275);
            this.grid.TabIndex = 1;
            this.grid.TabStop = true;
            this.grid.ToolTipText = "";
            // 
            // split
            // 
            this.split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.split.Location = new System.Drawing.Point(5, 120);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.label4);
            this.split.Panel1.Controls.Add(this.grid);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.split2);
            this.split.Size = new System.Drawing.Size(629, 294);
            this.split.SplitterDistance = 350;
            this.split.TabIndex = 19;
            // 
            // split2
            // 
            this.split2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.split2.Location = new System.Drawing.Point(0, 0);
            this.split2.Name = "split2";
            this.split2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split2.Panel1
            // 
            this.split2.Panel1.Controls.Add(this.lblTop);
            this.split2.Panel1.Controls.Add(this.gridTop);
            // 
            // split2.Panel2
            // 
            this.split2.Panel2.Controls.Add(this.cmdBase);
            this.split2.Panel2.Controls.Add(this.cmdMin);
            this.split2.Panel2.Controls.Add(this.gridBot);
            this.split2.Panel2.Controls.Add(this.lblBot);
            this.split2.Panel2.Controls.Add(this.lblBase);
            this.split2.Panel2.Controls.Add(this.lblMin);
            this.split2.Size = new System.Drawing.Size(272, 291);
            this.split2.SplitterDistance = 130;
            this.split2.TabIndex = 0;
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Location = new System.Drawing.Point(0, 0);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(39, 13);
            this.lblTop.TabIndex = 0;
            this.lblTop.Text = "lblTop:";
            // 
            // gridTop
            // 
            this.gridTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridTop.EnableSort = true;
            this.gridTop.FixedColumns = 1;
            this.gridTop.FixedRows = 1;
            this.gridTop.Location = new System.Drawing.Point(0, 16);
            this.gridTop.Name = "gridTop";
            this.gridTop.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridTop.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridTop.Size = new System.Drawing.Size(272, 111);
            this.gridTop.TabIndex = 1;
            this.gridTop.TabStop = true;
            this.gridTop.ToolTipText = "";
            // 
            // cmdBase
            // 
            this.cmdBase.Location = new System.Drawing.Point(157, 16);
            this.cmdBase.Name = "cmdBase";
            this.cmdBase.Size = new System.Drawing.Size(75, 23);
            this.cmdBase.TabIndex = 4;
            this.cmdBase.Text = "BASE";
            this.cmdBase.UseVisualStyleBackColor = true;
            this.cmdBase.Click += new System.EventHandler(this.cmdBase_Click);
            // 
            // cmdMin
            // 
            this.cmdMin.Location = new System.Drawing.Point(37, 16);
            this.cmdMin.Name = "cmdMin";
            this.cmdMin.Size = new System.Drawing.Size(75, 23);
            this.cmdMin.TabIndex = 2;
            this.cmdMin.Text = "MIN";
            this.cmdMin.UseVisualStyleBackColor = true;
            this.cmdMin.Click += new System.EventHandler(this.cmdMin_Click);
            // 
            // gridBot
            // 
            this.gridBot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridBot.EnableSort = true;
            this.gridBot.Location = new System.Drawing.Point(0, 45);
            this.gridBot.Name = "gridBot";
            this.gridBot.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridBot.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridBot.Size = new System.Drawing.Size(272, 114);
            this.gridBot.TabIndex = 5;
            this.gridBot.TabStop = true;
            this.gridBot.ToolTipText = "";
            this.gridBot.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridUnits_MouseClick);
            // 
            // lblBot
            // 
            this.lblBot.AutoSize = true;
            this.lblBot.Location = new System.Drawing.Point(0, 0);
            this.lblBot.Name = "lblBot";
            this.lblBot.Size = new System.Drawing.Size(53, 13);
            this.lblBot.TabIndex = 0;
            this.lblBot.Text = "lblBottom:";
            // 
            // lblBase
            // 
            this.lblBase.AutoSize = true;
            this.lblBase.Location = new System.Drawing.Point(121, 21);
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size(30, 13);
            this.lblBase.TabIndex = 3;
            this.lblBase.Text = "Осн.";
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(0, 21);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(31, 13);
            this.lblMin.TabIndex = 1;
            this.lblMin.Text = "Мин.";
            // 
            // cmdDelPrice
            // 
            this.cmdDelPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDelPrice.Image = global::MyBooks.Properties.Resources.close_24;
            this.cmdDelPrice.Location = new System.Drawing.Point(43, 420);
            this.cmdDelPrice.Name = "cmdDelPrice";
            this.cmdDelPrice.Size = new System.Drawing.Size(25, 25);
            this.cmdDelPrice.TabIndex = 21;
            this.cmdDelPrice.UseVisualStyleBackColor = true;
            this.cmdDelPrice.Click += new System.EventHandler(this.cmdDelPrice_Click);
            // 
            // cmdAddPrice
            // 
            this.cmdAddPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAddPrice.Image = global::MyBooks.Properties.Resources.add_24;
            this.cmdAddPrice.Location = new System.Drawing.Point(12, 420);
            this.cmdAddPrice.Name = "cmdAddPrice";
            this.cmdAddPrice.Size = new System.Drawing.Size(25, 25);
            this.cmdAddPrice.TabIndex = 20;
            this.cmdAddPrice.UseVisualStyleBackColor = true;
            this.cmdAddPrice.Click += new System.EventHandler(this.cmdAddPrice_Click);
            // 
            // lblNameUa
            // 
            this.lblNameUa.AutoSize = true;
            this.lblNameUa.Location = new System.Drawing.Point(5, 70);
            this.lblNameUa.Name = "lblNameUa";
            this.lblNameUa.Size = new System.Drawing.Size(69, 13);
            this.lblNameUa.TabIndex = 6;
            this.lblNameUa.Text = "В чеке (Укр)";
            // 
            // txtNameUa
            // 
            this.txtNameUa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameUa.Location = new System.Drawing.Point(80, 67);
            this.txtNameUa.Name = "txtNameUa";
            this.txtNameUa.Size = new System.Drawing.Size(546, 20);
            this.txtNameUa.TabIndex = 7;
            // 
            // lblArt
            // 
            this.lblArt.AutoSize = true;
            this.lblArt.Location = new System.Drawing.Point(245, 45);
            this.lblArt.Name = "lblArt";
            this.lblArt.Size = new System.Drawing.Size(48, 13);
            this.lblArt.TabIndex = 4;
            this.lblArt.Text = "Артикул";
            // 
            // txtArt
            // 
            this.txtArt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArt.Location = new System.Drawing.Point(299, 41);
            this.txtArt.Name = "txtArt";
            this.txtArt.Size = new System.Drawing.Size(327, 20);
            this.txtArt.TabIndex = 5;
            // 
            // lblTypeCap
            // 
            this.lblTypeCap.AutoSize = true;
            this.lblTypeCap.Location = new System.Drawing.Point(5, 96);
            this.lblTypeCap.Name = "lblTypeCap";
            this.lblTypeCap.Size = new System.Drawing.Size(26, 13);
            this.lblTypeCap.TabIndex = 11;
            this.lblTypeCap.Text = "Тип";
            // 
            // lblFmt
            // 
            this.lblFmt.AutoSize = true;
            this.lblFmt.Location = new System.Drawing.Point(127, 96);
            this.lblFmt.Name = "lblFmt";
            this.lblFmt.Size = new System.Drawing.Size(49, 13);
            this.lblFmt.TabIndex = 13;
            this.lblFmt.Text = "Формат";
            // 
            // cmdFmt
            // 
            this.cmdFmt.Location = new System.Drawing.Point(182, 91);
            this.cmdFmt.Name = "cmdFmt";
            this.cmdFmt.Size = new System.Drawing.Size(75, 23);
            this.cmdFmt.TabIndex = 14;
            this.cmdFmt.Text = "FMT";
            this.cmdFmt.UseVisualStyleBackColor = true;
            this.cmdFmt.Click += new System.EventHandler(this.cmdFmt_Click);
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Location = new System.Drawing.Point(263, 96);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(41, 13);
            this.lblDen.TabIndex = 15;
            this.lblDen.Text = "Плотн.";
            this.lblDen.Visible = false;
            // 
            // cmdDen
            // 
            this.cmdDen.Location = new System.Drawing.Point(313, 91);
            this.cmdDen.Name = "cmdDen";
            this.cmdDen.Size = new System.Drawing.Size(75, 23);
            this.cmdDen.TabIndex = 16;
            this.cmdDen.Text = "DENS";
            this.cmdDen.UseVisualStyleBackColor = true;
            this.cmdDen.Visible = false;
            this.cmdDen.Click += new System.EventHandler(this.cmdDen_Click);
            // 
            // lblSurf
            // 
            this.lblSurf.AutoSize = true;
            this.lblSurf.Location = new System.Drawing.Point(391, 96);
            this.lblSurf.Name = "lblSurf";
            this.lblSurf.Size = new System.Drawing.Size(47, 13);
            this.lblSurf.TabIndex = 17;
            this.lblSurf.Text = "Поверх.";
            this.lblSurf.Visible = false;
            // 
            // cmdSurf
            // 
            this.cmdSurf.Location = new System.Drawing.Point(444, 91);
            this.cmdSurf.Name = "cmdSurf";
            this.cmdSurf.Size = new System.Drawing.Size(82, 23);
            this.cmdSurf.TabIndex = 18;
            this.cmdSurf.Text = "SURF";
            this.cmdSurf.UseVisualStyleBackColor = true;
            this.cmdSurf.Visible = false;
            this.cmdSurf.Click += new System.EventHandler(this.cmdSurf_Click);
            // 
            // lblType
            // 
            this.lblType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblType.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblType.Location = new System.Drawing.Point(44, 92);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(75, 20);
            this.lblType.TabIndex = 12;
            this.lblType.Text = "Type name";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cmdSurf);
            this.Controls.Add(this.cmdDen);
            this.Controls.Add(this.cmdFmt);
            this.Controls.Add(this.lblSurf);
            this.Controls.Add(this.lblDen);
            this.Controls.Add(this.lblFmt);
            this.Controls.Add(this.txtArt);
            this.Controls.Add(this.cmdDelPrice);
            this.Controls.Add(this.split);
            this.Controls.Add(this.cmdAddPrice);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.chkUse);
            this.Controls.Add(this.cbIcon);
            this.Controls.Add(this.txtShort);
            this.Controls.Add(this.txtNameUa);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblArt);
            this.Controls.Add(this.lblShort);
            this.Controls.Add(this.lblTypeCap);
            this.Controls.Add(this.lblNameUa);
            this.Controls.Add(this.lblName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(654, 494);
            this.Name = "frmPrice";
            this.Text = "Товар/услуга";
            this.Load += new System.EventHandler(this.frmPrice_Load);
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel1.PerformLayout();
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.split2.Panel1.ResumeLayout(false);
            this.split2.Panel1.PerformLayout();
            this.split2.Panel2.ResumeLayout(false);
            this.split2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split2)).EndInit();
            this.split2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblShort;
        private System.Windows.Forms.TextBox txtShort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbIcon;
        private System.Windows.Forms.CheckBox chkUse;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.ImageList imgl;
        private SourceGrid.Grid grid;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.Label lblTop;
        private SourceGrid.Grid gridTop;
        private System.Windows.Forms.SplitContainer split2;
        private System.Windows.Forms.Label lblBot;
		private System.Windows.Forms.Label lblMin;
		private System.Windows.Forms.Label lblNameUa;
		private System.Windows.Forms.TextBox txtNameUa;
		private System.Windows.Forms.Button cmdDelPrice;
		private System.Windows.Forms.Button cmdAddPrice;
		private SourceGrid.Grid gridBot;
		private System.Windows.Forms.Label lblBase;
		private System.Windows.Forms.Button cmdMin;
		private System.Windows.Forms.Button cmdBase;
		private System.Windows.Forms.Label lblArt;
		private System.Windows.Forms.TextBox txtArt;
		private System.Windows.Forms.Label lblTypeCap;
		private System.Windows.Forms.Label lblFmt;
		private System.Windows.Forms.Button cmdFmt;
		private System.Windows.Forms.Label lblDen;
		private System.Windows.Forms.Button cmdDen;
		private System.Windows.Forms.Label lblSurf;
		private System.Windows.Forms.Button cmdSurf;
		private System.Windows.Forms.Label lblType;
    }
}