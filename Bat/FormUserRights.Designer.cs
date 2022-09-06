namespace Bat
{
    partial class FormUserRights
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
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.ColumnIDN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMenus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSELECT = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnINSERT = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnUPDATE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnDELETE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewSettings = new System.Windows.Forms.DataGridView();
            this.ColumnIDNS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToAddRows = false;
            this.dataGridViewMain.AllowUserToDeleteRows = false;
            this.dataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIDN,
            this.ColumnMenus,
            this.ColumnSELECT,
            this.ColumnINSERT,
            this.ColumnUPDATE,
            this.ColumnDELETE});
            this.dataGridViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMain.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.RowTemplate.Height = 24;
            this.dataGridViewMain.Size = new System.Drawing.Size(670, 465);
            this.dataGridViewMain.TabIndex = 0;
            // 
            // ColumnIDN
            // 
            this.ColumnIDN.HeaderText = "IDN";
            this.ColumnIDN.Name = "ColumnIDN";
            this.ColumnIDN.Visible = false;
            // 
            // ColumnMenus
            // 
            this.ColumnMenus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnMenus.HeaderText = "Меню";
            this.ColumnMenus.Name = "ColumnMenus";
            this.ColumnMenus.ReadOnly = true;
            // 
            // ColumnSELECT
            // 
            this.ColumnSELECT.HeaderText = "SELECT";
            this.ColumnSELECT.Name = "ColumnSELECT";
            // 
            // ColumnINSERT
            // 
            this.ColumnINSERT.HeaderText = "INSERT";
            this.ColumnINSERT.Name = "ColumnINSERT";
            // 
            // ColumnUPDATE
            // 
            this.ColumnUPDATE.HeaderText = "UPDATE";
            this.ColumnUPDATE.Name = "ColumnUPDATE";
            // 
            // ColumnDELETE
            // 
            this.ColumnDELETE.HeaderText = "DELETE";
            this.ColumnDELETE.Name = "ColumnDELETE";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.buttonExit, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSave, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControlMain, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(690, 546);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // buttonExit
            // 
            this.buttonExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonExit.Location = new System.Drawing.Point(348, 509);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(339, 34);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Закрыть";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(3, 509);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(339, 34);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // tabControlMain
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControlMain, 2);
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(3, 3);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(684, 500);
            this.tabControlMain.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewMain);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(676, 471);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Разрешения на таблицы";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewSettings);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(676, 471);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Настройки";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewSettings
            // 
            this.dataGridViewSettings.AllowUserToAddRows = false;
            this.dataGridViewSettings.AllowUserToDeleteRows = false;
            this.dataGridViewSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIDNS,
            this.ColumnName,
            this.ColumnValue});
            this.dataGridViewSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSettings.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewSettings.Name = "dataGridViewSettings";
            this.dataGridViewSettings.RowTemplate.Height = 24;
            this.dataGridViewSettings.Size = new System.Drawing.Size(670, 465);
            this.dataGridViewSettings.TabIndex = 0;
            // 
            // ColumnIDNS
            // 
            this.ColumnIDNS.HeaderText = "IDN";
            this.ColumnIDNS.Name = "ColumnIDNS";
            this.ColumnIDNS.Visible = false;
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnName.HeaderText = "Настройка";
            this.ColumnName.Name = "ColumnName";
            // 
            // ColumnValue
            // 
            this.ColumnValue.HeaderText = "Значение";
            this.ColumnValue.Name = "ColumnValue";
            // 
            // FormUserRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 546);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormUserRights";
            this.Text = "Права пользователя";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSettings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIDN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMenus;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSELECT;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnINSERT;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnUPDATE;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnDELETE;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridViewSettings;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIDNS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnValue;
    }
}