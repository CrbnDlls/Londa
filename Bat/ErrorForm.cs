using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bat
{
    /// <summary>
    /// Форма в которой отображается случившаяся ошибка
    /// </summary>
    class ErrorForm :Form
    {
        private Button buttonClose;
        private TableLayoutPanel tableLayoutPanel1;
        private ListBox listBoxErrorMessage;
        
        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="ErrorMessage">Сообщение, которое необходимо показать</param>
        public ErrorForm(string[] ErrorMessage, string Header)
        {
            InitializeComponent();
            listBoxErrorMessage.Items.AddRange(ErrorMessage);
            Text = Header;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.listBoxErrorMessage = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonClose.Location = new System.Drawing.Point(3, 372);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(1088, 24);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // listBoxErrorMessage
            // 
            this.listBoxErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxErrorMessage.ForeColor = System.Drawing.Color.Black;
            this.listBoxErrorMessage.FormattingEnabled = true;
            this.listBoxErrorMessage.ItemHeight = 25;
            this.listBoxErrorMessage.Location = new System.Drawing.Point(3, 3);
            this.listBoxErrorMessage.Name = "listBoxErrorMessage";
            this.listBoxErrorMessage.Size = new System.Drawing.Size(1088, 354);
            this.listBoxErrorMessage.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.listBoxErrorMessage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonClose, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1094, 399);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // ErrorForm
            // 
            this.AcceptButton = this.buttonClose;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(1094, 399);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorForm";
            this.ShowInTaskbar = false;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }
    }
}
