namespace MyBooks
{
    partial class frmMyOrder
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
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSupplier = new System.Windows.Forms.ComboBox();
            this.btnSupEd = new System.Windows.Forms.Button();
            this.btnSupAdd = new System.Windows.Forms.Button();
            this.grpCat = new System.Windows.Forms.GroupBox();
            this.btnCatDel = new System.Windows.Forms.Button();
            this.btnCatEd = new System.Windows.Forms.Button();
            this.btnCatAdd = new System.Windows.Forms.Button();
            this.gridCat = new SourceGrid.Grid();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.grpOrder = new System.Windows.Forms.GroupBox();
            this.gridOrder = new SourceGrid.Grid();
            this.split = new System.Windows.Forms.SplitContainer();
            this.tt = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.lstFind = new System.Windows.Forms.ListBox();
            this.grpCat.SuspendLayout();
            this.grpOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdCancel.ForeColor = System.Drawing.Color.Maroon;
            this.cmdCancel.Location = new System.Drawing.Point(1098, 345);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 23);
            this.cmdCancel.TabIndex = 11;
            this.cmdCancel.Text = "Отмена";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cmdOk.Location = new System.Drawing.Point(1024, 345);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(68, 23);
            this.cmdOk.TabIndex = 10;
            this.cmdOk.Text = "ОК";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Поставщик :";
            // 
            // cbSupplier
            // 
            this.cbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSupplier.FormattingEnabled = true;
            this.cbSupplier.Location = new System.Drawing.Point(90, 10);
            this.cbSupplier.Name = "cbSupplier";
            this.cbSupplier.Size = new System.Drawing.Size(183, 21);
            this.cbSupplier.TabIndex = 13;
            this.tt.SetToolTip(this.cbSupplier, "Поставщик заказа");
            this.cbSupplier.SelectedIndexChanged += new System.EventHandler(this.cbSupplier_SelectedIndexChanged);
            // 
            // btnSupEd
            // 
            this.btnSupEd.Image = global::MyBooks.Properties.Resources.pencil_24;
            this.btnSupEd.Location = new System.Drawing.Point(284, 10);
            this.btnSupEd.Name = "btnSupEd";
            this.btnSupEd.Size = new System.Drawing.Size(25, 25);
            this.btnSupEd.TabIndex = 14;
            this.tt.SetToolTip(this.btnSupEd, "Праить данные поставщика");
            this.btnSupEd.UseVisualStyleBackColor = true;
            this.btnSupEd.Click += new System.EventHandler(this.btnSupEd_Click);
            // 
            // btnSupAdd
            // 
            this.btnSupAdd.Image = global::MyBooks.Properties.Resources.add_24;
            this.btnSupAdd.Location = new System.Drawing.Point(315, 10);
            this.btnSupAdd.Name = "btnSupAdd";
            this.btnSupAdd.Size = new System.Drawing.Size(25, 25);
            this.btnSupAdd.TabIndex = 14;
            this.tt.SetToolTip(this.btnSupAdd, "Новый поставщик");
            this.btnSupAdd.UseVisualStyleBackColor = true;
            this.btnSupAdd.Click += new System.EventHandler(this.btnSupAdd_Click);
            // 
            // grpCat
            // 
            this.grpCat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCat.Controls.Add(this.btnCatDel);
            this.grpCat.Controls.Add(this.btnCatEd);
            this.grpCat.Controls.Add(this.btnCatAdd);
            this.grpCat.Controls.Add(this.gridCat);
            this.grpCat.Location = new System.Drawing.Point(0, 0);
            this.grpCat.Name = "grpCat";
            this.grpCat.Size = new System.Drawing.Size(446, 298);
            this.grpCat.TabIndex = 15;
            this.grpCat.TabStop = false;
            this.grpCat.Text = "Каталог";
            // 
            // btnCatDel
            // 
            this.btnCatDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCatDel.Image = global::MyBooks.Properties.Resources.close_24;
            this.btnCatDel.Location = new System.Drawing.Point(68, 267);
            this.btnCatDel.Name = "btnCatDel";
            this.btnCatDel.Size = new System.Drawing.Size(25, 25);
            this.btnCatDel.TabIndex = 16;
            this.tt.SetToolTip(this.btnCatDel, "Удалить товар из каталога");
            this.btnCatDel.UseVisualStyleBackColor = true;
            this.btnCatDel.Click += new System.EventHandler(this.btnCatDel_Click);
            // 
            // btnCatEd
            // 
            this.btnCatEd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCatEd.Image = global::MyBooks.Properties.Resources.pencil_24;
            this.btnCatEd.Location = new System.Drawing.Point(37, 267);
            this.btnCatEd.Name = "btnCatEd";
            this.btnCatEd.Size = new System.Drawing.Size(25, 25);
            this.btnCatEd.TabIndex = 16;
            this.tt.SetToolTip(this.btnCatEd, "Изменить товар");
            this.btnCatEd.UseVisualStyleBackColor = true;
            this.btnCatEd.Click += new System.EventHandler(this.btnCatEd_Click);
            // 
            // btnCatAdd
            // 
            this.btnCatAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCatAdd.Image = global::MyBooks.Properties.Resources.add_24;
            this.btnCatAdd.Location = new System.Drawing.Point(6, 267);
            this.btnCatAdd.Name = "btnCatAdd";
            this.btnCatAdd.Size = new System.Drawing.Size(25, 25);
            this.btnCatAdd.TabIndex = 15;
            this.tt.SetToolTip(this.btnCatAdd, "Новый товар");
            this.btnCatAdd.UseVisualStyleBackColor = true;
            this.btnCatAdd.Click += new System.EventHandler(this.btnCatAdd_Click);
            // 
            // gridCat
            // 
            this.gridCat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridCat.EnableSort = true;
            this.gridCat.Location = new System.Drawing.Point(6, 19);
            this.gridCat.Name = "gridCat";
            this.gridCat.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridCat.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridCat.Size = new System.Drawing.Size(434, 242);
            this.gridCat.TabIndex = 0;
            this.gridCat.TabStop = true;
            this.gridCat.ToolTipText = "";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MyBooks.Properties.Resources.right;
            this.btnAdd.Location = new System.Drawing.Point(4, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(25, 25);
            this.btnAdd.TabIndex = 15;
            this.tt.SetToolTip(this.btnAdd, "Заказать товар");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Image = global::MyBooks.Properties.Resources.trashcan_delete_17;
            this.btnDel.Location = new System.Drawing.Point(3, 50);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(25, 25);
            this.btnDel.TabIndex = 15;
            this.tt.SetToolTip(this.btnDel, "Убрать из заказа");
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // grpOrder
            // 
            this.grpOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOrder.Controls.Add(this.gridOrder);
            this.grpOrder.Controls.Add(this.btnAdd);
            this.grpOrder.Controls.Add(this.btnDel);
            this.grpOrder.Location = new System.Drawing.Point(0, 0);
            this.grpOrder.Name = "grpOrder";
            this.grpOrder.Size = new System.Drawing.Size(1154, 298);
            this.grpOrder.TabIndex = 16;
            this.grpOrder.TabStop = false;
            this.grpOrder.Text = "Состав заказа";
            // 
            // gridOrder
            // 
            this.gridOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridOrder.EnableSort = true;
            this.gridOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridOrder.Location = new System.Drawing.Point(34, 19);
            this.gridOrder.Name = "gridOrder";
            this.gridOrder.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridOrder.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridOrder.Size = new System.Drawing.Size(1114, 273);
            this.gridOrder.TabIndex = 0;
            this.gridOrder.TabStop = true;
            this.gridOrder.ToolTipText = "";
            // 
            // split
            // 
            this.split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.split.Location = new System.Drawing.Point(12, 41);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.grpCat);
            this.split.Panel1Collapsed = true;
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.grpOrder);
            this.split.Size = new System.Drawing.Size(1154, 298);
            this.split.SplitterDistance = 449;
            this.split.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(476, 348);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Итого:";
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.Location = new System.Drawing.Point(522, 345);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(95, 20);
            this.txtTotal.TabIndex = 20;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtName.Location = new System.Drawing.Point(985, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(181, 20);
            this.txtName.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(880, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Название заказа:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(346, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Добавить :";
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.Location = new System.Drawing.Point(415, 15);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(459, 20);
            this.txtFind.TabIndex = 23;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            this.txtFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyUp);
            // 
            // lstFind
            // 
            this.lstFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFind.FormattingEnabled = true;
            this.lstFind.Location = new System.Drawing.Point(415, 35);
            this.lstFind.Name = "lstFind";
            this.lstFind.Size = new System.Drawing.Size(459, 225);
            this.lstFind.TabIndex = 16;
            this.lstFind.Visible = false;
            this.lstFind.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstFind_MouseClick);
            this.lstFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstFind_KeyUp);
            // 
            // frmMyOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 380);
            this.Controls.Add(this.lstFind);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSupAdd);
            this.Controls.Add(this.btnSupEd);
            this.Controls.Add(this.cbSupplier);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.split);
            this.Name = "frmMyOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Параметры заказа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMyOrder_FormClosing);
            this.Load += new System.EventHandler(this.frmMyOrder_Load);
            this.grpCat.ResumeLayout(false);
            this.grpOrder.ResumeLayout(false);
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSupplier;
        private System.Windows.Forms.Button btnSupEd;
        private System.Windows.Forms.Button btnSupAdd;
        private System.Windows.Forms.GroupBox grpCat;
        private System.Windows.Forms.Button btnCatDel;
        private System.Windows.Forms.Button btnCatEd;
        private System.Windows.Forms.Button btnCatAdd;
        private SourceGrid.Grid gridCat;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.GroupBox grpOrder;
        private SourceGrid.Grid gridOrder;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.ToolTip tt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.ListBox lstFind;
    }
}