using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace Bat
{
    public partial class FormAddEdit : Form
    {
        private OracleConnection conn;
        private OracleCommand insUpdCommand;
        DataTable comboTextBoxes;
        private System.Windows.Forms.TableLayoutPanel mainPanel;
        DataGridViewRow Identifier;
        private int KEY;
        /// <summary>
        /// IDN номер для выбора строки
        /// </summary>
        [Description("IDN номер для выбора строки")]
        public int ItemNum
        {
            get { return KEY; }
        }
        public FormAddEdit(string formName, OracleConnection baseConn, DataTable colName, OracleCommand INSERTUPDATE, DataGridViewRow IDN)
        {
            InitializeComponent();
            Text = formName + " Добавить ; Изменить.";
            comboTextBoxes = colName;
            Identifier = IDN;
            mainPanel = new TableLayoutPanel();
            mainPanel.Location = new System.Drawing.Point(0, 0);
            mainPanel.RowCount = colName.Rows.Count + 1;
            mainPanel.ColumnCount = 3;
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
            
            for (int i = 0; i < colName.Rows.Count + 1; i++)
            {
                if (i == colName.Rows.Count)
                {
                    mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
                    Button buttonSave = new Button();
                    buttonSave.Text = "Сохранить";
                    mainPanel.Controls.Add(buttonSave, 0, i);
                    buttonSave.Dock = DockStyle.Fill;
                    buttonSave.Click += new EventHandler(buttonSave_Click);
                    Button buttonCancel = new Button();
                    buttonCancel.Text = "Отменить";
                    buttonCancel.Click += new EventHandler(buttonCancel_Click);

                    mainPanel.Controls.Add(buttonCancel, 1, i);
                    mainPanel.SetColumnSpan(buttonCancel, 2);
                    buttonCancel.Dock = DockStyle.Fill;
                }
                else
                {
                    mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / colName.Rows.Count));
                    Label tmpLbl = new Label();
                    tmpLbl.Width = 400;
                    tmpLbl.TextAlign = ContentAlignment.MiddleRight;
                    tmpLbl.Text = colName.Rows[i].ItemArray[0].ToString();
                    mainPanel.Controls.Add(tmpLbl, 0, i);
                    tmpLbl.Dock = DockStyle.Top;
                    //tmpLbl.Anchor = AnchorStyles.Right & AnchorStyles.Bottom;
                    Font tmpfont = new Font("Arial", (float)10.2, FontStyle.Regular);
                    tmpLbl.Font = tmpfont;
                    switch (colName.Rows[i].ItemArray[1].ToString().Substring(0, 7))
                    {
                        case "textbox":
                            TextBox tmptext = new TextBox();
                            mainPanel.Controls.Add(tmptext, 1, i);
                            mainPanel.SetColumnSpan(tmptext, 2);
                            tmptext.Dock = DockStyle.Fill;
                            break;
                        case "datepic":
                            DateTimePicker tmpdate = new DateTimePicker();
                            mainPanel.Controls.Add(tmpdate, 2, i);
                            tmpdate.Dock = DockStyle.Fill;
                            tmptext = new TextBox();
                            mainPanel.Controls.Add(tmptext, 1, i);
                            tmptext.Dock = DockStyle.Fill;
                            tmpdate.ValueChanged += new EventHandler(tmpdate_ValueChanged);
                            break;
                        case "combobo":
                            OracleCommand tmpcmd = new OracleCommand(colName.Rows[i].ItemArray[1].ToString().Substring(9), baseConn);
                                                       
                            DataTable tmp_tbl = Functions.GetData(tmpcmd);
                            tmp_tbl.Columns[0].ColumnName = "ID";
                            tmp_tbl.Columns[1].ColumnName = "NAME";
                            if (tmp_tbl != null)
                            {
                                ComboBox tmpBox = new ComboBox();
                                mainPanel.Controls.Add(tmpBox, 1, i);
                                mainPanel.SetColumnSpan(tmpBox, 2);
                                tmpBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                                tmpBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                                tmpBox.Dock = DockStyle.Fill;
                                tmpBox.DataSource = tmp_tbl;
                                tmpBox.DisplayMember = "NAME";
                                tmpBox.ValueMember = "ID";
                            }
                            else
                            {
                                MessageBox.Show("Невозможно отобразить данные", "Ошибка оракле ридер", MessageBoxButtons.OK);
                                this.Dispose();
                            }
                            break;
                    }

                }

            }
            
            mainPanel.Dock = DockStyle.Fill;
            this.Controls.Add(mainPanel);

            conn = baseConn;
            insUpdCommand = INSERTUPDATE;
            
        }
        void buttonSave_Click(object sender, EventArgs e)
        {
            if (Identifier != null)
            {
                insUpdCommand.Parameters[0].Value = Identifier.Cells[1].Value.ToString();
            }
            else
            {
                insUpdCommand.Parameters[0].Value = "0";
            }
            //throw new NotImplementedException();
            bool error = false;
            for (int j = 0; j < insUpdCommand.Parameters.Count - 2; j++)
            {
                switch (comboTextBoxes.Rows[j].ItemArray[1].ToString().Substring(0, 7))
                {
                    case "textbox":
                        TextBox tmptext = (TextBox)mainPanel.GetControlFromPosition(1, j);
                        insUpdCommand.Parameters[j + 1].Value = tmptext.Text;
                        break;
                    case "datepic":
                        tmptext = (TextBox)mainPanel.GetControlFromPosition(1, j);
                        insUpdCommand.Parameters[j + 1].Value = tmptext.Text;
                        break;
                    case "combobo":
                        ComboBox tmpcombo = (ComboBox)mainPanel.GetControlFromPosition(1, j);
                        if (tmpcombo.SelectedValue == null)
                        {
                            DataTable tmpTbl = (DataTable)tmpcombo.DataSource;
                            if (tmpcombo.Text == "")
                            {
                                error = true;
                                foreach (DataRow row in tmpTbl.Rows)
                                {
                                    if (row["NAME"].ToString() == "")
                                    {
                                        insUpdCommand.Parameters[j + 1].Value = row["ID"].ToString();
                                        error = false;
                                    }
                                }
                            }
                            else
                            {
                                DataRow[] tmprow = tmpTbl.Select("NAME = '" + tmpcombo.Text + "'");
                                if (tmprow.Count() == 0)
                                {
                                    error = true;
                                }
                                else
                                {
                                    insUpdCommand.Parameters[j + 1].Value = tmprow[0].ItemArray[0].ToString();
                                }
                            }

                        }
                        else
                        {
                            insUpdCommand.Parameters[j + 1].Value = tmpcombo.SelectedValue;
                        }
                        break;
                }
            }
            insUpdCommand.Connection = conn;

            if (!error)
            {
                if (!Functions.ExecuteNonQuery(insUpdCommand))
                {
                    MessageBox.Show("Сохранение не выполнено", "Ошибка");
                    error = true;
                }

            }
            else
            {
                MessageBox.Show("Необходимо выбрать значение из списка.", "Ошибка");
            }
            
            int result;

            if (error == false && !int.TryParse(insUpdCommand.Parameters[insUpdCommand.Parameters.Count - 1].Value.ToString(), out result))
            {
                MessageBox.Show(insUpdCommand.Parameters[insUpdCommand.Parameters.Count - 1].Value.ToString(), "Ошибка");
                error = true;
            }

            if (error == false)
            {

                KEY = int.Parse(insUpdCommand.Parameters[insUpdCommand.Parameters.Count - 1].Value.ToString());
                MessageBox.Show("Данные сохранены", "Сообщение");
                DialogResult = DialogResult.OK;
                this.Close();

            }
        }

        void tmpdate_ValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            DateTimePicker tempDatePicker = (DateTimePicker)sender;
            int y = mainPanel.GetRow(tempDatePicker);
            TextBox tempTextBox = (TextBox)mainPanel.GetControlFromPosition(1, y);
            string day, month;
            day = tempDatePicker.Value.Day.ToString();
            if (day.Length < 2)
            {
                day = "0" + day;
            }
            month = tempDatePicker.Value.Month.ToString();
            if (month.Length < 2)
            {
                month = "0" + month;
            }
            tempTextBox.Text = day + "/" + month + "/" + tempDatePicker.Value.Year.ToString();

                
        }

        void buttonCancel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.Close();
        }

        private void FormAddEdit_Shown(object sender, EventArgs e)
        {
            if (Identifier != null)
            {

                for (int i = 0; i < comboTextBoxes.Rows.Count; i++)
                {
                    switch (comboTextBoxes.Rows[i].ItemArray[1].ToString().Substring(0, 7))
                    {
                        case "textbox":
                            TextBox tmptext = (TextBox)mainPanel.GetControlFromPosition(1, i);
                            tmptext.Text = Identifier.Cells[i + 2].Value.ToString();

                            break;
                        case "datepic":
                            DateTimePicker tmpdate = (DateTimePicker)mainPanel.GetControlFromPosition(2, i);
                            tmptext = (TextBox)mainPanel.GetControlFromPosition(1, i);
                            tmptext.Text = Identifier.Cells[i + 2].Value.ToString();
                            if (tmptext.Text != "")
                            {
                                tmpdate.Value = new DateTime(int.Parse(tmptext.Text.Substring(6, 4)), int.Parse(tmptext.Text.Substring(3, 2)), int.Parse(tmptext.Text.Substring(0, 2)));
                            }
                            
                            break;
                        case "combobo":

                            ComboBox tmpBox = (ComboBox)mainPanel.GetControlFromPosition(1, i);
                            
                            //tmpBox.SelectedText = Identifier.Cells[i + 2].Value.ToString();
                            tmpBox.Text = Identifier.Cells[i + 2].Value.ToString();
                            break;
                    }
                    /*
                    "Фамилия", "Имя", "Отчество", "Девичья фамилия", "Профессия", "Звание", "Дата присвоения", "Дата приема", "День рождения", "Дата Увольнения", "Оформлен", "Дата Оформления"*/
                }
            }
        }


    }
}
