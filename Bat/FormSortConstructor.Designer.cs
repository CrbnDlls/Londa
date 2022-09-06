namespace Bat
{
    partial class FormSortConstructor
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
            this.dataGridViewSort = new System.Windows.Forms.DataGridView();
            this.ColumnSortColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnDirection = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSort)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.buttonAddRow, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonDeleteRow, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonSave, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.buttonExit, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.dataGridViewSort, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(438, 255);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // buttonDeleteRow
            // 
            this.buttonDeleteRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDeleteRow.Location = new System.Drawing.Point(222, 3);
            this.buttonDeleteRow.Name = "buttonDeleteRow";
            this.buttonDeleteRow.Size = new System.Drawing.Size(213, 34);
            this.buttonDeleteRow.TabIndex = 0;
            this.buttonDeleteRow.Text = "Удалить строку сортировки";
            this.buttonDeleteRow.UseVisualStyleBackColor = true;
            this.buttonDeleteRow.Click += new System.EventHandler(this.buttonDeleteRow_Click);
            // 
            // buttonAddRow
            // 
            this.buttonAddRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAddRow.Location = new System.Drawing.Point(3, 3);
            this.buttonAddRow.Name = "buttonAddRow";
            this.buttonAddRow.Size = new System.Drawing.Size(213, 34);
            this.buttonAddRow.TabIndex = 1;
            this.buttonAddRow.Text = "Добавить строку сортировки";
            this.buttonAddRow.UseVisualStyleBackColor = true;
            this.buttonAddRow.Click += new System.EventHandler(this.buttonAddRow_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(3, 218);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(213, 34);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Применить сортировку";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonExit.Location = new System.Drawing.Point(222, 218);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(213, 34);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Закрыть";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // dataGridViewSort
            // 
            this.dataGridViewSort.AllowUserToAddRows = false;
            this.dataGridViewSort.AllowUserToDeleteRows = false;
            this.dataGridViewSort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSort.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSortColumn,
            this.ColumnDirection});
            this.tableLayoutPanelMain.SetColumnSpan(this.dataGridViewSort, 2);
            this.dataGridViewSort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSort.Location = new System.Drawing.Point(3, 43);
            this.dataGridViewSort.MultiSelect = false;
            this.dataGridViewSort.Name = "dataGridViewSort";
            this.dataGridViewSort.RowTemplate.Height = 24;
            this.dataGridViewSort.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSort.Size = new System.Drawing.Size(432, 169);
            this.dataGridViewSort.TabIndex = 4;
            // 
            // ColumnSortColumn
            // 
            this.ColumnSortColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnSortColumn.HeaderText = "Колонка";
            this.ColumnSortColumn.Name = "ColumnSortColumn";
            // 
            // ColumnDirection
            // 
            this.ColumnDirection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnDirection.HeaderText = "Направление";
            this.ColumnDirection.Name = "ColumnDirection";
            this.ColumnDirection.Width = 103;
            // 
            // FormSortConstructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 255);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "FormSortConstructor";
            this.Text = "Сортировка";
            this.tableLayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Button buttonDeleteRow;
        private System.Windows.Forms.Button buttonAddRow;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.DataGridView dataGridViewSort;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnSortColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnDirection;
    }
}