namespace Bat
{
    partial class FormFilterConstructor
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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDeleteRow = new System.Windows.Forms.Button();
            this.buttonAddRow = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.dataGridViewFilter = new System.Windows.Forms.DataGridView();
            this.ColumnANDOR = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnSkobkaOpen = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnColName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnEqual = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSkobkaClose = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilter)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.buttonDeleteRow, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonAddRow, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonSave, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.buttonExit, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.dataGridViewFilter, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(604, 249);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // buttonDeleteRow
            // 
            this.buttonDeleteRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDeleteRow.Location = new System.Drawing.Point(304, 2);
            this.buttonDeleteRow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonDeleteRow.Name = "buttonDeleteRow";
            this.buttonDeleteRow.Size = new System.Drawing.Size(298, 28);
            this.buttonDeleteRow.TabIndex = 1;
            this.buttonDeleteRow.Text = "Удалить строку фильтра";
            this.buttonDeleteRow.UseVisualStyleBackColor = true;
            this.buttonDeleteRow.Click += new System.EventHandler(this.buttonDeleteRow_Click);
            // 
            // buttonAddRow
            // 
            this.buttonAddRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAddRow.Location = new System.Drawing.Point(2, 2);
            this.buttonAddRow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonAddRow.Name = "buttonAddRow";
            this.buttonAddRow.Size = new System.Drawing.Size(298, 28);
            this.buttonAddRow.TabIndex = 0;
            this.buttonAddRow.Text = "Добавить строку фильтра";
            this.buttonAddRow.UseVisualStyleBackColor = true;
            this.buttonAddRow.Click += new System.EventHandler(this.buttonAddRow_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(2, 219);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(298, 28);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Применить фильтр";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonExit.Location = new System.Drawing.Point(304, 219);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(298, 28);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Закрыть";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // dataGridViewFilter
            // 
            this.dataGridViewFilter.AllowUserToAddRows = false;
            this.dataGridViewFilter.AllowUserToDeleteRows = false;
            this.dataGridViewFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFilter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnANDOR,
            this.ColumnSkobkaOpen,
            this.ColumnColName,
            this.ColumnEqual,
            this.ColumnValue,
            this.ColumnSkobkaClose,
            this.ColumnDataType});
            this.tableLayoutPanelMain.SetColumnSpan(this.dataGridViewFilter, 2);
            this.dataGridViewFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFilter.Location = new System.Drawing.Point(2, 34);
            this.dataGridViewFilter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridViewFilter.MultiSelect = false;
            this.dataGridViewFilter.Name = "dataGridViewFilter";
            this.dataGridViewFilter.RowTemplate.Height = 24;
            this.dataGridViewFilter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFilter.Size = new System.Drawing.Size(600, 181);
            this.dataGridViewFilter.TabIndex = 4;
            this.dataGridViewFilter.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFilter_CellValueChanged);
            // 
            // ColumnANDOR
            // 
            this.ColumnANDOR.HeaderText = "И / ИЛИ";
            this.ColumnANDOR.Items.AddRange(new object[] {
            "И",
            "ИЛИ"});
            this.ColumnANDOR.Name = "ColumnANDOR";
            // 
            // ColumnSkobkaOpen
            // 
            this.ColumnSkobkaOpen.HeaderText = "(";
            this.ColumnSkobkaOpen.Items.AddRange(new object[] {
            "",
            "("});
            this.ColumnSkobkaOpen.Name = "ColumnSkobkaOpen";
            // 
            // ColumnColName
            // 
            this.ColumnColName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnColName.HeaderText = "Колонка";
            this.ColumnColName.Name = "ColumnColName";
            this.ColumnColName.Width = 56;
            // 
            // ColumnEqual
            // 
            this.ColumnEqual.HeaderText = "Равенство";
            this.ColumnEqual.Items.AddRange(new object[] {
            "равно",
            "не равно",
            "похоже",
            "не похоже",
            "больше",
            "меньше",
            "больше равно",
            "меньше равно",
            "пустое значение",
            "не пустое значение"});
            this.ColumnEqual.Name = "ColumnEqual";
            // 
            // ColumnValue
            // 
            this.ColumnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnValue.HeaderText = "Значение";
            this.ColumnValue.MinimumWidth = 100;
            this.ColumnValue.Name = "ColumnValue";
            // 
            // ColumnSkobkaClose
            // 
            this.ColumnSkobkaClose.HeaderText = ")";
            this.ColumnSkobkaClose.Items.AddRange(new object[] {
            "",
            ")"});
            this.ColumnSkobkaClose.Name = "ColumnSkobkaClose";
            // 
            // ColumnDataType
            // 
            this.ColumnDataType.HeaderText = "Тип Данных";
            this.ColumnDataType.Name = "ColumnDataType";
            this.ColumnDataType.Visible = false;
            // 
            // FormFilterConstructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 249);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormFilterConstructor";
            this.Text = "Фильтр";
            this.tableLayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Button buttonDeleteRow;
        private System.Windows.Forms.Button buttonAddRow;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.DataGridView dataGridViewFilter;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnANDOR;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnSkobkaOpen;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnColName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnEqual;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnSkobkaClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDataType;

    }
}