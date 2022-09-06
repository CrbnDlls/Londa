namespace Bat
{
    partial class FormWorkSheetEdit
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
            this.labelGrn = new System.Windows.Forms.Label();
            this.textBoxSumm = new System.Windows.Forms.TextBox();
            this.labelAutoSumm = new System.Windows.Forms.Label();
            this.textBoxMinuteS = new System.Windows.Forms.TextBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelTime2 = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.textBoxHourS = new System.Windows.Forms.TextBox();
            this.comboBoxWorker = new System.Windows.Forms.ComboBox();
            this.labelWorker = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnIDN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SvHourS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SvMinuteS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SvHourE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SvMinuteE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SVCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Summ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mistakes = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Client = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Master = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxDay = new System.Windows.Forms.TextBox();
            this.textBoxNum = new System.Windows.Forms.TextBox();
            this.labelNumSheet = new System.Windows.Forms.Label();
            this.labelSummlbl = new System.Windows.Forms.Label();
            this.labelSumm = new System.Windows.Forms.Label();
            this.comboBoxSalon = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxMonth = new System.Windows.Forms.TextBox();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.labelSlash1 = new System.Windows.Forms.Label();
            this.labelSlash2 = new System.Windows.Forms.Label();
            this.textBoxMinuteE = new System.Windows.Forms.TextBox();
            this.textBoxHourE = new System.Windows.Forms.TextBox();
            this.labelDots1 = new System.Windows.Forms.Label();
            this.labelDoubleDots = new System.Windows.Forms.Label();
            this.labelSalon = new System.Windows.Forms.Label();
            this.listBoxMessage = new System.Windows.Forms.ListBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 9;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.5F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.5F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.5F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.5F));
            this.tableLayoutPanelMain.Controls.Add(this.labelGrn, 8, 6);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxSumm, 2, 6);
            this.tableLayoutPanelMain.Controls.Add(this.labelAutoSumm, 1, 7);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxMinuteS, 4, 4);
            this.tableLayoutPanelMain.Controls.Add(this.labelDate, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelTime2, 5, 4);
            this.tableLayoutPanelMain.Controls.Add(this.labelTime, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxHourS, 2, 4);
            this.tableLayoutPanelMain.Controls.Add(this.comboBoxWorker, 2, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelWorker, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.dataGridView1, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxDay, 2, 2);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxNum, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelNumSheet, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelSummlbl, 1, 6);
            this.tableLayoutPanelMain.Controls.Add(this.labelSumm, 2, 7);
            this.tableLayoutPanelMain.Controls.Add(this.comboBoxSalon, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonSave, 0, 6);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxMonth, 6, 2);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxYear, 8, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelSlash1, 5, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelSlash2, 7, 2);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxMinuteE, 8, 4);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxHourE, 6, 4);
            this.tableLayoutPanelMain.Controls.Add(this.labelDots1, 3, 4);
            this.tableLayoutPanelMain.Controls.Add(this.labelDoubleDots, 7, 4);
            this.tableLayoutPanelMain.Controls.Add(this.labelSalon, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.listBoxMessage, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonExit, 8, 7);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 8;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(969, 474);
            this.tableLayoutPanelMain.TabIndex = 14;
            // 
            // labelGrn
            // 
            this.labelGrn.AutoSize = true;
            this.labelGrn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelGrn.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGrn.Location = new System.Drawing.Point(838, 434);
            this.labelGrn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelGrn.Name = "labelGrn";
            this.labelGrn.Size = new System.Drawing.Size(129, 16);
            this.labelGrn.TabIndex = 32;
            this.labelGrn.Text = "грн.";
            // 
            // textBoxSumm
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxSumm, 6);
            this.textBoxSumm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSumm.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSumm.Location = new System.Drawing.Point(382, 428);
            this.textBoxSumm.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSumm.Name = "textBoxSumm";
            this.textBoxSumm.Size = new System.Drawing.Size(452, 23);
            this.textBoxSumm.TabIndex = 31;
            this.textBoxSumm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSumm.TextChanged += new System.EventHandler(this.textBoxSumm_TextChanged);
            // 
            // labelAutoSumm
            // 
            this.labelAutoSumm.AutoSize = true;
            this.labelAutoSumm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelAutoSumm.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAutoSumm.Location = new System.Drawing.Point(270, 458);
            this.labelAutoSumm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAutoSumm.Name = "labelAutoSumm";
            this.labelAutoSumm.Size = new System.Drawing.Size(108, 16);
            this.labelAutoSumm.TabIndex = 30;
            this.labelAutoSumm.Text = "АвтоСумма";
            // 
            // textBoxMinuteS
            // 
            this.textBoxMinuteS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMinuteS.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxMinuteS.Location = new System.Drawing.Point(534, 98);
            this.textBoxMinuteS.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMinuteS.Name = "textBoxMinuteS";
            this.textBoxMinuteS.Size = new System.Drawing.Size(126, 23);
            this.textBoxMinuteS.TabIndex = 7;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelDate.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDate.Location = new System.Drawing.Point(270, 48);
            this.labelDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(108, 24);
            this.labelDate.TabIndex = 3;
            this.labelDate.Text = "Дата (дд/мм/гггг)";
            // 
            // labelTime2
            // 
            this.labelTime2.AutoSize = true;
            this.labelTime2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelTime2.Location = new System.Drawing.Point(664, 96);
            this.labelTime2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTime2.Name = "labelTime2";
            this.labelTime2.Size = new System.Drawing.Size(18, 24);
            this.labelTime2.TabIndex = 9;
            this.labelTime2.Text = "по";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelTime.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTime.Location = new System.Drawing.Point(270, 104);
            this.labelTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(108, 16);
            this.labelTime.TabIndex = 8;
            this.labelTime.Text = "Время            с";
            // 
            // textBoxHourS
            // 
            this.textBoxHourS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxHourS.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxHourS.Location = new System.Drawing.Point(382, 98);
            this.textBoxHourS.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxHourS.Name = "textBoxHourS";
            this.textBoxHourS.Size = new System.Drawing.Size(126, 23);
            this.textBoxHourS.TabIndex = 6;
            // 
            // comboBoxWorker
            // 
            this.comboBoxWorker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxWorker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tableLayoutPanelMain.SetColumnSpan(this.comboBoxWorker, 7);
            this.comboBoxWorker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxWorker.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxWorker.FormattingEnabled = true;
            this.comboBoxWorker.Location = new System.Drawing.Point(382, 74);
            this.comboBoxWorker.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxWorker.Name = "comboBoxWorker";
            this.comboBoxWorker.Size = new System.Drawing.Size(585, 24);
            this.comboBoxWorker.TabIndex = 5;
            // 
            // labelWorker
            // 
            this.labelWorker.AutoSize = true;
            this.labelWorker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelWorker.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWorker.Location = new System.Drawing.Point(270, 72);
            this.labelWorker.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelWorker.Name = "labelWorker";
            this.labelWorker.Size = new System.Drawing.Size(108, 24);
            this.labelWorker.TabIndex = 7;
            this.labelWorker.Text = "Ф.И.О. мастера";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIDN,
            this.ClientNum,
            this.SvHourS,
            this.SvMinuteS,
            this.SvHourE,
            this.SvMinuteE,
            this.SVCode,
            this.Discount,
            this.Quantity,
            this.Summ,
            this.Mistakes,
            this.Client,
            this.Master,
            this.DDATE});
            this.tableLayoutPanelMain.SetColumnSpan(this.dataGridView1, 8);
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(270, 122);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(697, 302);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // ColumnIDN
            // 
            this.ColumnIDN.HeaderText = "IDN";
            this.ColumnIDN.Name = "ColumnIDN";
            this.ColumnIDN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnIDN.Visible = false;
            // 
            // ClientNum
            // 
            this.ClientNum.HeaderText = "Номер клиента";
            this.ClientNum.Name = "ClientNum";
            this.ClientNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SvHourS
            // 
            this.SvHourS.HeaderText = "Время начала услуги (часы)";
            this.SvHourS.Name = "SvHourS";
            this.SvHourS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SvMinuteS
            // 
            this.SvMinuteS.HeaderText = "Время начала услуги (минуты)";
            this.SvMinuteS.Name = "SvMinuteS";
            this.SvMinuteS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SvHourE
            // 
            this.SvHourE.HeaderText = "Время окончания услуги (часы)";
            this.SvHourE.Name = "SvHourE";
            this.SvHourE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SvMinuteE
            // 
            this.SvMinuteE.HeaderText = "Время окончания услуги (минуты)";
            this.SvMinuteE.Name = "SvMinuteE";
            this.SvMinuteE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SVCode
            // 
            this.SVCode.HeaderText = "Код услуги";
            this.SVCode.Name = "SVCode";
            this.SVCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SVCode.ToolTipText = "Код услуги";
            // 
            // Discount
            // 
            this.Discount.HeaderText = "Скидка %";
            this.Discount.Name = "Discount";
            this.Discount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Кол-во";
            this.Quantity.Name = "Quantity";
            this.Quantity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Quantity.ToolTipText = "Количество";
            // 
            // Summ
            // 
            this.Summ.HeaderText = "Сумма в грн.";
            this.Summ.Name = "Summ";
            this.Summ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Mistakes
            // 
            this.Mistakes.HeaderText = "Ошибка";
            this.Mistakes.Name = "Mistakes";
            // 
            // Client
            // 
            this.Client.HeaderText = "ФИО Клиента";
            this.Client.Name = "Client";
            this.Client.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Client.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Master
            // 
            this.Master.HeaderText = "Фамилия на талоне";
            this.Master.Name = "Master";
            // 
            // DDATE
            // 
            this.DDATE.HeaderText = "Дата на талоне";
            this.DDATE.Name = "DDATE";
            // 
            // textBoxDay
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxDay, 3);
            this.textBoxDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDay.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDay.Location = new System.Drawing.Point(382, 50);
            this.textBoxDay.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDay.Name = "textBoxDay";
            this.textBoxDay.Size = new System.Drawing.Size(278, 23);
            this.textBoxDay.TabIndex = 2;
            this.textBoxDay.Text = "дд";
            this.textBoxDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxNum
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.textBoxNum, 7);
            this.textBoxNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNum.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNum.Location = new System.Drawing.Point(382, 26);
            this.textBoxNum.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNum.Name = "textBoxNum";
            this.textBoxNum.Size = new System.Drawing.Size(585, 23);
            this.textBoxNum.TabIndex = 1;
            // 
            // labelNumSheet
            // 
            this.labelNumSheet.AutoSize = true;
            this.labelNumSheet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelNumSheet.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNumSheet.Location = new System.Drawing.Point(270, 32);
            this.labelNumSheet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNumSheet.Name = "labelNumSheet";
            this.labelNumSheet.Size = new System.Drawing.Size(108, 16);
            this.labelNumSheet.TabIndex = 2;
            this.labelNumSheet.Text = "Номер листа";
            // 
            // labelSummlbl
            // 
            this.labelSummlbl.AutoSize = true;
            this.labelSummlbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelSummlbl.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSummlbl.Location = new System.Drawing.Point(270, 434);
            this.labelSummlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSummlbl.Name = "labelSummlbl";
            this.labelSummlbl.Size = new System.Drawing.Size(108, 16);
            this.labelSummlbl.TabIndex = 13;
            this.labelSummlbl.Text = "Общая Сумма";
            // 
            // labelSumm
            // 
            this.labelSumm.AutoSize = true;
            this.tableLayoutPanelMain.SetColumnSpan(this.labelSumm, 6);
            this.labelSumm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelSumm.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSumm.Location = new System.Drawing.Point(382, 458);
            this.labelSumm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSumm.Name = "labelSumm";
            this.labelSumm.Size = new System.Drawing.Size(452, 16);
            this.labelSumm.TabIndex = 12;
            this.labelSumm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSalon
            // 
            this.comboBoxSalon.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxSalon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tableLayoutPanelMain.SetColumnSpan(this.comboBoxSalon, 7);
            this.comboBoxSalon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSalon.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSalon.FormattingEnabled = true;
            this.comboBoxSalon.Location = new System.Drawing.Point(382, 2);
            this.comboBoxSalon.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxSalon.Name = "comboBoxSalon";
            this.comboBoxSalon.Size = new System.Drawing.Size(585, 24);
            this.comboBoxSalon.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSave.Location = new System.Drawing.Point(2, 428);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(264, 20);
            this.buttonSave.TabIndex = 19;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxMonth
            // 
            this.textBoxMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMonth.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxMonth.Location = new System.Drawing.Point(686, 50);
            this.textBoxMonth.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMonth.Name = "textBoxMonth";
            this.textBoxMonth.Size = new System.Drawing.Size(126, 23);
            this.textBoxMonth.TabIndex = 3;
            this.textBoxMonth.Text = "мм";
            this.textBoxMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxYear
            // 
            this.textBoxYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxYear.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxYear.Location = new System.Drawing.Point(838, 50);
            this.textBoxYear.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(129, 23);
            this.textBoxYear.TabIndex = 4;
            this.textBoxYear.Text = "гггг";
            this.textBoxYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelSlash1
            // 
            this.labelSlash1.AutoSize = true;
            this.labelSlash1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelSlash1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSlash1.Location = new System.Drawing.Point(664, 56);
            this.labelSlash1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSlash1.Name = "labelSlash1";
            this.labelSlash1.Size = new System.Drawing.Size(18, 16);
            this.labelSlash1.TabIndex = 23;
            this.labelSlash1.Text = "/";
            // 
            // labelSlash2
            // 
            this.labelSlash2.AutoSize = true;
            this.labelSlash2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelSlash2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSlash2.Location = new System.Drawing.Point(816, 56);
            this.labelSlash2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSlash2.Name = "labelSlash2";
            this.labelSlash2.Size = new System.Drawing.Size(18, 16);
            this.labelSlash2.TabIndex = 24;
            this.labelSlash2.Text = "/";
            // 
            // textBoxMinuteE
            // 
            this.textBoxMinuteE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMinuteE.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxMinuteE.Location = new System.Drawing.Point(838, 98);
            this.textBoxMinuteE.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMinuteE.Name = "textBoxMinuteE";
            this.textBoxMinuteE.Size = new System.Drawing.Size(129, 23);
            this.textBoxMinuteE.TabIndex = 9;
            // 
            // textBoxHourE
            // 
            this.textBoxHourE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxHourE.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxHourE.Location = new System.Drawing.Point(686, 98);
            this.textBoxHourE.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxHourE.Name = "textBoxHourE";
            this.textBoxHourE.Size = new System.Drawing.Size(126, 23);
            this.textBoxHourE.TabIndex = 8;
            // 
            // labelDots1
            // 
            this.labelDots1.AutoSize = true;
            this.labelDots1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelDots1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDots1.Location = new System.Drawing.Point(512, 104);
            this.labelDots1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDots1.Name = "labelDots1";
            this.labelDots1.Size = new System.Drawing.Size(18, 16);
            this.labelDots1.TabIndex = 27;
            this.labelDots1.Text = ":";
            // 
            // labelDoubleDots
            // 
            this.labelDoubleDots.AutoSize = true;
            this.labelDoubleDots.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelDoubleDots.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDoubleDots.Location = new System.Drawing.Point(816, 104);
            this.labelDoubleDots.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDoubleDots.Name = "labelDoubleDots";
            this.labelDoubleDots.Size = new System.Drawing.Size(18, 16);
            this.labelDoubleDots.TabIndex = 28;
            this.labelDoubleDots.Text = ":";
            // 
            // labelSalon
            // 
            this.labelSalon.AutoSize = true;
            this.labelSalon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelSalon.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSalon.Location = new System.Drawing.Point(270, 8);
            this.labelSalon.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSalon.Name = "labelSalon";
            this.labelSalon.Size = new System.Drawing.Size(108, 16);
            this.labelSalon.TabIndex = 17;
            this.labelSalon.Text = "Салон";
            // 
            // listBoxMessage
            // 
            this.listBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxMessage.FormattingEnabled = true;
            this.listBoxMessage.HorizontalScrollbar = true;
            this.listBoxMessage.ItemHeight = 16;
            this.listBoxMessage.Location = new System.Drawing.Point(2, 2);
            this.listBoxMessage.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxMessage.Name = "listBoxMessage";
            this.tableLayoutPanelMain.SetRowSpan(this.listBoxMessage, 6);
            this.listBoxMessage.Size = new System.Drawing.Size(264, 420);
            this.listBoxMessage.TabIndex = 29;
            this.listBoxMessage.Layout += new System.Windows.Forms.LayoutEventHandler(this.listBoxMessage_Layout);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Location = new System.Drawing.Point(838, 452);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(128, 20);
            this.buttonExit.TabIndex = 20;
            this.buttonExit.Text = "Выйти";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // FormWorkSheetEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 474);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormWorkSheetEdit";
            this.Text = "Проверка рабочего листа";
            this.Load += new System.EventHandler(this.FormWorkSheetEdit_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormWorkSheetEdit_FormClosed);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TextBox textBoxMinuteS;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelTime2;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.TextBox textBoxHourS;
        private System.Windows.Forms.ComboBox comboBoxWorker;
        private System.Windows.Forms.Label labelWorker;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBoxDay;
        private System.Windows.Forms.TextBox textBoxNum;
        private System.Windows.Forms.Label labelNumSheet;
        private System.Windows.Forms.Label labelSummlbl;
        private System.Windows.Forms.ComboBox comboBoxSalon;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.TextBox textBoxMonth;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label labelSlash1;
        private System.Windows.Forms.Label labelSlash2;
        private System.Windows.Forms.TextBox textBoxMinuteE;
        private System.Windows.Forms.TextBox textBoxHourE;
        private System.Windows.Forms.Label labelDots1;
        private System.Windows.Forms.Label labelDoubleDots;
        private System.Windows.Forms.Label labelSalon;
        private System.Windows.Forms.ListBox listBoxMessage;
        private System.Windows.Forms.Label labelSumm;
        private System.Windows.Forms.TextBox textBoxSumm;
        private System.Windows.Forms.Label labelAutoSumm;
        private System.Windows.Forms.Label labelGrn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIDN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn SvHourS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SvMinuteS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SvHourE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SvMinuteE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SVCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Summ;
        private System.Windows.Forms.DataGridViewComboBoxColumn Mistakes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client;
        private System.Windows.Forms.DataGridViewTextBoxColumn Master;
        private System.Windows.Forms.DataGridViewTextBoxColumn DDATE;
    }
}