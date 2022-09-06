namespace Bat
{
    partial class FormImport
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
            this.dataGridViewImportTable = new System.Windows.Forms.DataGridView();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.openFileDialogImport = new System.Windows.Forms.OpenFileDialog();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FSTNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FATHNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OLDNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BIRTHDAY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HIREDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STAGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAINSALON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImportTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.dataGridViewImportTable, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOpenFile, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonImport, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1066, 543);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // dataGridViewImportTable
            // 
            this.dataGridViewImportTable.AllowUserToAddRows = false;
            this.dataGridViewImportTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewImportTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.IDN,
            this.FNAME,
            this.FSTNAME,
            this.FATHNAME,
            this.OLDNAME,
            this.PROF,
            this.BIRTHDAY,
            this.HIREDATE,
            this.STAGE,
            this.MAINSALON});
            this.tableLayoutPanelMain.SetColumnSpan(this.dataGridViewImportTable, 2);
            this.dataGridViewImportTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewImportTable.Location = new System.Drawing.Point(3, 33);
            this.dataGridViewImportTable.Name = "dataGridViewImportTable";
            this.dataGridViewImportTable.RowTemplate.Height = 24;
            this.dataGridViewImportTable.Size = new System.Drawing.Size(1060, 507);
            this.dataGridViewImportTable.TabIndex = 0;
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOpenFile.Location = new System.Drawing.Point(3, 3);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(527, 24);
            this.buttonOpenFile.TabIndex = 1;
            this.buttonOpenFile.Text = "Открыть файл";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonImport.Location = new System.Drawing.Point(536, 3);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(527, 24);
            this.buttonImport.TabIndex = 2;
            this.buttonImport.Text = "Импорт таблицы";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // openFileDialogImport
            // 
            this.openFileDialogImport.FileName = "openFileDialog1";
            // 
            // Number
            // 
            this.Number.HeaderText = "№";
            this.Number.Name = "Number";
            // 
            // IDN
            // 
            this.IDN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IDN.HeaderText = "ID";
            this.IDN.Name = "IDN";
            this.IDN.Width = 46;
            // 
            // FNAME
            // 
            this.FNAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FNAME.HeaderText = "Фамилия";
            this.FNAME.Name = "FNAME";
            this.FNAME.Width = 95;
            // 
            // FSTNAME
            // 
            this.FSTNAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FSTNAME.HeaderText = "Имя";
            this.FSTNAME.Name = "FSTNAME";
            this.FSTNAME.Width = 60;
            // 
            // FATHNAME
            // 
            this.FATHNAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FATHNAME.HeaderText = "Отчество";
            this.FATHNAME.Name = "FATHNAME";
            this.FATHNAME.Width = 96;
            // 
            // OLDNAME
            // 
            this.OLDNAME.HeaderText = "Девичья фамилия";
            this.OLDNAME.Name = "OLDNAME";
            // 
            // PROF
            // 
            this.PROF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PROF.HeaderText = "Должность";
            this.PROF.Name = "PROF";
            this.PROF.Width = 106;
            // 
            // BIRTHDAY
            // 
            this.BIRTHDAY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BIRTHDAY.HeaderText = "Дата рождения";
            this.BIRTHDAY.Name = "BIRTHDAY";
            this.BIRTHDAY.Width = 124;
            // 
            // HIREDATE
            // 
            this.HIREDATE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.HIREDATE.HeaderText = "Дата принятия";
            this.HIREDATE.Name = "HIREDATE";
            this.HIREDATE.Width = 123;
            // 
            // STAGE
            // 
            this.STAGE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STAGE.HeaderText = "Звание";
            this.STAGE.Name = "STAGE";
            this.STAGE.Width = 81;
            // 
            // MAINSALON
            // 
            this.MAINSALON.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MAINSALON.HeaderText = "Основной салон";
            this.MAINSALON.Name = "MAINSALON";
            this.MAINSALON.Width = 129;
            // 
            // FormImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 543);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "FormImport";
            this.Text = "FormImport";
            this.tableLayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImportTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.DataGridView dataGridViewImportTable;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.OpenFileDialog openFileDialogImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDN;
        private System.Windows.Forms.DataGridViewTextBoxColumn FNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FSTNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FATHNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn OLDNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROF;
        private System.Windows.Forms.DataGridViewTextBoxColumn BIRTHDAY;
        private System.Windows.Forms.DataGridViewTextBoxColumn HIREDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STAGE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAINSALON;
    }
}