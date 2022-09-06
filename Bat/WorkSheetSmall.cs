using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Xml;
using System.IO;

namespace Bat
{
    public partial class FormWorkSheetSmall : Form
    {
        
        private OracleConnection conn;
        
        private DataTable workers_tbl;
        private DataTable salons_tbl;
        private DataTable mistakes_tbl;
        bool AutoSumm;
        string wrkshPath;

        public FormWorkSheetSmall(OracleConnection baseConn)
        {
            InitializeComponent();
            dataGridView1.Rows.Add(21);
            OracleCommand cmd = new OracleCommand("SELECT s.FNAME, s.FSTNAME, s.FATHNAME, s.PRENAME, s.IDN, l.SNAME, LONDA.workingornot(s.DROPDATE) FROM LONDA.STAFF s, LONDA.PROFESSIONS p, LONDA.SALONS l WHERE s.IDN <> 0 AND s.PROF = p.IDN AND p.ALWRKSH = 0 AND s.MAINSALON = l.IDN", baseConn);
            DataTable Data = Functions.GetData(cmd);
            if (Data != null)
            {
                workers_tbl = new DataTable("Workers");
                workers_tbl.Columns.Add("ID");
                workers_tbl.Columns.Add("NAME");
                for (int z = 0; z < Data.Rows.Count; z++)
                {
                    string temp = Data.Rows[z].ItemArray[0].ToString() + " " + Data.Rows[z].ItemArray[1].ToString() + " " + Data.Rows[z].ItemArray[2].ToString();

                    if (Data.Rows[z].ItemArray[3] != DBNull.Value)
                    {
                        temp = temp + " (" + Data.Rows[z].ItemArray[3].ToString() + ")";
                    }
                    temp = temp + " | " + Data.Rows[z].ItemArray[5].ToString() + " | " + Data.Rows[z].ItemArray[6].ToString();
                    DataRow row = workers_tbl.NewRow();
                    row["NAME"] = temp;
                    row["ID"] = Data.Rows[z].ItemArray[0];
                    workers_tbl.Rows.Add(row);
                }
                comboBoxWorker.DataSource = workers_tbl;
                comboBoxWorker.DisplayMember = "NAME";
                Data.Dispose();
            }
            else
            {
                MessageBox.Show("Невозможно отобразить данные", "Ошибка", MessageBoxButtons.OK);
                this.Dispose();
            }



            cmd.CommandText = "SELECT IDN, SNAME FROM LONDA.SALONS WHERE IDN <> 0";
            salons_tbl = Functions.GetData(cmd);
            if (salons_tbl != null)
            {
                salons_tbl.Columns[0].ColumnName = "ID";
                salons_tbl.Columns[1].ColumnName = "NAME";
                comboBoxSalon.DataSource = salons_tbl;
                comboBoxSalon.DisplayMember = "NAME";
            }
            else
            {
                MessageBox.Show("Невозможно отобразить данные", "Ошибка", MessageBoxButtons.OK);
                this.Dispose();
            }

            cmd.CommandText = "SELECT VAL FROM LONDA.USER_SETTINGS s, SYS.USER$ u, LONDA.SETTINGS_NAME n WHERE s.LONDAUSER = u.USER# AND u.NAME = USER AND s.SETTING = n.IDN AND n.SNAME = 'Автоматически суммировать при добавлении рабочего листа'";
            Data = Functions.GetData(cmd);
            if (Data != null)
            {
                if (Data.Rows.Count == 1)
                {
                    
                    if (Data.Rows[0].ItemArray[0].ToString() == "Y")
                    {
                        AutoSumm = true;
                    }
                    else
                    {
                        AutoSumm = false;
                    }
                }
                else
                {
                    AutoSumm = false;
                }
                
            }
            else
            {
                MessageBox.Show("Невозможно отобразить данные", "Ошибка", MessageBoxButtons.OK);
                this.Dispose();
            }

            cmd.CommandText = "SELECT IDN, MNAME FROM LONDA.WMISTAKES";
            mistakes_tbl = Functions.GetData(cmd);
            if (mistakes_tbl != null)
            {

                mistakes_tbl.Columns[0].ColumnName = "IDN";
                mistakes_tbl.Columns[1].ColumnName = "MNAME";

                DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dataGridView1.Columns["Mistakes"];
                col.DataSource = mistakes_tbl;
                col.DisplayMember = "MNAME";
                col.ValueMember = "IDN";

                col.MinimumWidth = 200;

                foreach (DataGridViewRow dtrow in dataGridView1.Rows)
                {
                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dtrow.Cells["Mistakes"];
                    cell.Value = mistakes_tbl.Rows[0].ItemArray[0];
                }
            }
            else
            {
                MessageBox.Show("Невозможно отобразить данные", "Ошибка", MessageBoxButtons.OK);
                this.Dispose();
            }

            XmlDocument WrkShXml = new XmlDocument();
            wrkshPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WorkSheets";
            if (Directory.Exists(wrkshPath))
            {
                if (!File.Exists(wrkshPath + "\\wrksh.xml"))
                {

                    using (XmlWriter writer = XmlWriter.Create(wrkshPath + "\\wrksh.xml"))
                    {
                        // Write XML data.
                        writer.WriteStartElement("wrksh");
                        writer.WriteEndElement();
                        writer.Flush();
                    }
                }
                else
                {
                    WrkShXml.Load(wrkshPath + "\\wrksh.xml");
                    XmlNodeList nodelst = WrkShXml.SelectNodes("/wrksh/WORKSHEET");
                    foreach (XmlNode node in nodelst)
                    {
                        treeViewWorkSheets.Nodes.Add(node.Attributes["SHEETNUMBER"].Value, node.Attributes["SALON"].Value + " " + node.Attributes["DATE"].Value.Substring(0, 2) + "/" + node.Attributes["DATE"].Value.Substring(2, 2) + "/" + node.Attributes["DATE"].Value.Substring(4, 4) + " " + node.Attributes["SHEETNUMBER"].Value + " " + node.Attributes["FIO"].Value + " " + node.Attributes["SUMM"].Value + " грн.");
                    }

                }
            }
            else
            {
                Directory.CreateDirectory(wrkshPath);
                using (XmlWriter writer = XmlWriter.Create(wrkshPath + "\\wrksh.xml"))
                {
                    // Write XML data.
                    writer.WriteStartElement("wrksh");
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            baseConn.Close();
            
            conn = baseConn;

         }

        private void buttonAddSave_Click(object sender, EventArgs e)
        {
            switch (buttonAddSave.Text)
            {
                case "Добавить":
                    
                    treeViewWorkSheets.Nodes.Add("Новый рабочий лист");
                    treeViewWorkSheets.SelectedNode = treeViewWorkSheets.Nodes[treeViewWorkSheets.Nodes.Count - 1];
                    textBoxNum.Text = "";
                    comboBoxWorker.SelectedIndex = -1;
                    textBoxSumm.Text = "0";

                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(21);
                    foreach (DataGridViewRow dtrow in dataGridView1.Rows)
                    {
                        DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dtrow.Cells["Mistakes"];
                        cell.Value = mistakes_tbl.Rows[0].ItemArray[0];

                    }
                    comboBoxSalon.Enabled = true;
                    textBoxNum.Enabled = true;
                    textBoxDay.Enabled = true;
                    textBoxMonth.Enabled = true;
                    textBoxYear.Enabled = true;
                    comboBoxWorker.Enabled = true;
                    textBoxHourS.Enabled = true;
                    textBoxMinuteS.Enabled = true;
                    textBoxHourE.Enabled = true;
                    textBoxMinuteE.Enabled = true;
                    textBoxSumm.Enabled = true;
                    dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
                    treeViewWorkSheets.Enabled = false;
                    buttonChange.Enabled = false;
                    buttonAddToBase.Enabled = false;
                    buttonAddSave.Text = "Сохранить";
                    buttonDelete.Text = "Отменить";
                    break;
                case "Сохранить":
                    string[] error = check_fields();
                    if (error[0] == "Ok")
                    {
                        XmlDocument worksheets = new XmlDocument();
                        worksheets.Load(wrkshPath + "\\wrksh.xml");
                        XmlNode node = worksheets.DocumentElement;
                        XmlElement element = worksheets.CreateElement("WORKSHEET");
                       // element.SetAttribute("SALON", GetSalon("Num"));
                        element.SetAttribute("SALON", comboBoxSalon.Text);
                        element.SetAttribute("SHEETNUMBER", textBoxNum.Text);
                        element.SetAttribute("DATE", textBoxDay.Text + textBoxMonth.Text + textBoxYear.Text);
                        element.SetAttribute("FIO", comboBoxWorker.Text);
                        element.SetAttribute("TimeS", textBoxHourS.Text + textBoxMinuteS.Text);
                        element.SetAttribute("TimeE", textBoxHourE.Text + textBoxMinuteE.Text);
                        element.SetAttribute("SUMM", textBoxSumm.Text);
                        int rowNum = 1;
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value != null | dataGridView1.Rows[i].Cells[1].Value != null | dataGridView1.Rows[i].Cells[2].Value != null | dataGridView1.Rows[i].Cells[3].Value != null | dataGridView1.Rows[i].Cells[4].Value != null | dataGridView1.Rows[i].Cells[5].Value != null | dataGridView1.Rows[i].Cells[6].Value != null | dataGridView1.Rows[i].Cells[7].Value != null | dataGridView1.Rows[i].Cells[8].Value != null)
                            {
                                XmlElement rowLine = worksheets.CreateElement("ROWLINE");
                                rowLine.SetAttribute("ROWNUM", rowNum.ToString());
                                rowNum = rowNum + 1;
                                if (dataGridView1.Rows[i].Cells["ClientNum"].Value != null)
                                {
                                    rowLine.SetAttribute("ClientNum", dataGridView1.Rows[i].Cells["ClientNum"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("ClientNum", "");
                                }
                                if (dataGridView1.Rows[i].Cells["SvHourS"].Value != null)
                                {
                                    rowLine.SetAttribute("SvTimeS", dataGridView1.Rows[i].Cells["SvHourS"].Value.ToString() + dataGridView1.Rows[i].Cells["SvMinuteS"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("SvTimeS", "");
                                }
                                if (dataGridView1.Rows[i].Cells["SvHourE"].Value != null)
                                {
                                    rowLine.SetAttribute("SvTimeE", dataGridView1.Rows[i].Cells["SvHourE"].Value.ToString() + dataGridView1.Rows[i].Cells["SvMinuteE"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("SvTimeE", "");
                                }
                                if (dataGridView1.Rows[i].Cells["SVCode"].Value != null)
                                {
                                    rowLine.SetAttribute("SVCode", dataGridView1.Rows[i].Cells["SVCode"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("SVCode", "");
                                }
                                if (dataGridView1.Rows[i].Cells["Discount"].Value != null)
                                {
                                    rowLine.SetAttribute("Discount", dataGridView1.Rows[i].Cells["Discount"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("Discount", "0");
                                }
                                if (dataGridView1.Rows[i].Cells["Quantity"].Value != null)
                                {
                                    rowLine.SetAttribute("Quantity", dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("Quantity", "");
                                }
                                if (dataGridView1.Rows[i].Cells["Summ"].Value != null)
                                {
                                    rowLine.SetAttribute("Summ", dataGridView1.Rows[i].Cells["Summ"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("Summ", "");
                                }
                                rowLine.SetAttribute("WMistake", dataGridView1.Rows[i].Cells["Mistakes"].Value.ToString());
                                if (dataGridView1.Rows[i].Cells["Client"].Value != null)
                                {
                                    rowLine.SetAttribute("Client", dataGridView1.Rows[i].Cells["Client"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("Client", "");
                                }
                                if (dataGridView1.Rows[i].Cells["Master"].Value != null)
                                {
                                    rowLine.SetAttribute("Master", dataGridView1.Rows[i].Cells["Master"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("Master", "");
                                }
                                if (dataGridView1.Rows[i].Cells["DDATE"].Value != null)
                                {
                                    rowLine.SetAttribute("DDATE", dataGridView1.Rows[i].Cells["DDATE"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("DDATE", "");
                                }
                                element.AppendChild(rowLine);
                            }
                        }
                        node.AppendChild(element);
                        worksheets.Save(wrkshPath + "\\wrksh.xml");
                        treeViewWorkSheets.SelectedNode.Text = comboBoxSalon.Text + " " + textBoxDay.Text + "/" + textBoxMonth.Text + "/" + textBoxYear.Text + " " + textBoxNum.Text + " " + comboBoxWorker.Text + " " + textBoxSumm.Text + " грн.";
                        treeViewWorkSheets.SelectedNode.Name = textBoxNum.Text;
                        buttonAddSave.Text = "Добавить";
                        buttonDelete.Text = "Удалить";
                        comboBoxSalon.Enabled = false;
                        textBoxNum.Enabled = false;
                        textBoxDay.Enabled = false;
                        textBoxMonth.Enabled = false;
                        textBoxYear.Enabled = false;
                        comboBoxWorker.Enabled = false;
                        textBoxHourS.Enabled = false;
                        textBoxMinuteS.Enabled = false;
                        textBoxHourE.Enabled = false;
                        textBoxMinuteE.Enabled = false;
                        textBoxSumm.Enabled = false;
                        dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
                        treeViewWorkSheets.Enabled = true;
                        buttonChange.Enabled = true;
                        if (treeViewWorkSheets.Nodes.Count > 0)
                        {
                            buttonAddToBase.Enabled = true;
                        } 
                    }
                    else
                    {
                        ErrorForm err = new ErrorForm(error, "Ошибка заполнения данных");
                        err.ShowDialog();
                    }
                    break;
            }
            
        }

        private string GetFIO(string txt)
        {
            string line = null;
            if (txt == "Num")
            {
                for (int i = 0; i < workers_tbl.Rows.Count; i++)
                {
                    if (workers_tbl.Rows[i].ItemArray[1].ToString() == comboBoxWorker.Text)
                    {
                        line = workers_tbl.Rows[i].ItemArray[0].ToString();
                    }
                }

            }
            else
            {
                for (int i = 0; i < workers_tbl.Rows.Count; i++)
                {
                    if (workers_tbl.Rows[i].ItemArray[0].ToString() == txt)
                    {
                        line = workers_tbl.Rows[i].ItemArray[1].ToString();
                    }
                }
            }
            return line;
        }

        private string GetSalon(string txt)
        {
            string line = null;

            if (txt == "Num")
            {
                for (int i = 0; i < salons_tbl.Rows.Count; i++)
                {
                    if (salons_tbl.Rows[i].ItemArray[1].ToString() == comboBoxSalon.Text)
                    {
                        line = salons_tbl.Rows[i].ItemArray[0].ToString();
                    }
                }
            }
            else
            {
                for (int i = 0; i < salons_tbl.Rows.Count; i++)
                {
                    if (salons_tbl.Rows[i].ItemArray[0].ToString() == txt)
                    {
                        line = salons_tbl.Rows[i].ItemArray[1].ToString();
                    }
                }
            }
            return line;
        }

        private string[] check_fields()
        {
            string[] error = new string[1];
            string[] tmp = new string[1];
            int x = 0;
            Int32 result = 0, result1 = 0;
            /// Проверка названия салона
            for (int i = 0; i < salons_tbl.Rows.Count; i++)
            {
                if (comboBoxSalon.Text == salons_tbl.Rows[i].ItemArray[1].ToString())
                {
                    x = x + 1;
                }
            }
            if (x < 1)
            {
                /*error[0] = "Незначительная Ошибка";
                tmp[0] = "Название салона заполнено не верно";
                error = error.Concat(tmp).ToArray();*/
            }
            //Проверка номера листа
            if (!Int32.TryParse(textBoxNum.Text, out result))
            {
                error[0] = "Ошибка";
                tmp[0] = "Номер листа заполнен не верно";
                error = error.Concat(tmp).ToArray();
            }
            /// Проверка даты
            Int32 month, year, day;

            if ((!Int32.TryParse(textBoxMonth.Text, out month) || month > 12) | !Int32.TryParse(textBoxYear.Text, out year) | !Int32.TryParse(textBoxDay.Text, out day))
            {
                error[0] = "Ошибка";
                tmp[0] = "Дата заполнена не верно";
                error = error.Concat(tmp).ToArray();
            }
            else
            {
                if (day > DateTime.DaysInMonth(year, month))
                {
                    error[0] = "Ошибка";
                    tmp[0] = "В " + month + " месяце " + year + " года меньше " + day + " дня(ей)";
                    error = error.Concat(tmp).ToArray();
                }
            }
            /// Проверка имени сотрудника
            for (int i = 0; i < workers_tbl.Rows.Count; i++)
            {
                if (comboBoxWorker.Text == workers_tbl.Rows[i].ItemArray[0].ToString())
                {
                    x = x + 1;
                }
            }
            if (x < 1)
            {
                /*error[0] = "Незначительная Ошибка";
                tmp[0] = "Фамилия сотрудника заполнена не верно";
                error = error.Concat(tmp).ToArray();*/
            }
            /// Проверка часов начала и конца работы мастера
            Int32 hourS, minuteS, hourE, minuteE;
            if ((!Int32.TryParse(textBoxHourS.Text, out hourS) || hourS > 23) | (!Int32.TryParse(textBoxHourE.Text, out hourE) || hourE > 23) | (!Int32.TryParse(textBoxMinuteS.Text, out minuteS) || minuteS > 59) | (!Int32.TryParse(textBoxMinuteE.Text, out minuteE) || minuteE > 59))
            {
                error[0] = "Ошибка";
                tmp[0] = "Время начала или окончания робочего дня мастера введено не верно";
                error = error.Concat(tmp).ToArray();
            }
            else
            {
                if (hourE - hourS < 0 || (hourE - hourS == 0 & minuteE - minuteS < 0))
                {
                    /*error[0] = "Незначительная Ошибка";
                    tmp[0] = "Время начала позже времени окончания робочего дня мастера";
                    error = error.Concat(tmp).ToArray();*/
                }
            }

            // Cумма
            decimal summ;
            if (!decimal.TryParse(textBoxSumm.Text, out summ))
            {
                error[0] = "Ошибка";
                tmp[0] = "Не верно внесена общая сумма.";
                error = error.Concat(tmp).ToArray();
            }

            // Проверка данных таблицы

            Int32 hours = -1, minutes = -1, houre = -2, y = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells["ClientNum"].Value != null | dataGridView1.Rows[i].Cells["SvHourS"].Value != null | dataGridView1.Rows[i].Cells["SvMinuteS"].Value != null | dataGridView1.Rows[i].Cells["SvHourE"].Value != null | dataGridView1.Rows[i].Cells["SvMinuteE"].Value != null | dataGridView1.Rows[i].Cells["SVCode"].Value != null | dataGridView1.Rows[i].Cells["Summ"].Value != null)
                {
                    //Порядковый номер
                    if (dataGridView1.Rows[0].Cells["ClientNum"].Value == null)
                    {
                        error[0] = "Ошибка";
                        tmp[0] = "Не внесен номер клиента. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();
                    }
                    if (dataGridView1.Rows[i].Cells["ClientNum"].Value != null && !Int32.TryParse(dataGridView1.Rows[i].Cells["ClientNum"].Value.ToString(), out result))
                    {
                        error[0] = "Ошибка";
                        tmp[0] = "Внесен не верный номер клиента. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();
                    }
                    else
                    {
                        if (i > 0 & dataGridView1.Rows[i].Cells["ClientNum"].Value != null)
                        {
                            if (result - x < 0)
                            {
                                /*error[0] = "Незначительная Ошибка";
                                tmp[0] = "Внесен не верный номер клиента. Строка №" + (i + 1);
                                error = error.Concat(tmp).ToArray();*/
                            }
                            x = result;

                        }

                    }

                    //Время начала процедуры часы
                    if (dataGridView1.Rows[0].Cells["SvHourS"].Value == null)
                    {
                        /*error[0] = "Незначительная Ошибка";
                        tmp[0] = "Не внесен час начала процедуры. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();*/
                    }
                    if (dataGridView1.Rows[i].Cells["SvHourS"].Value != null && (!Int32.TryParse(dataGridView1.Rows[i].Cells["SvHourS"].Value.ToString(), out result) || result > 23))
                    {
                        error[0] = "Ошибка";
                        tmp[0] = "Внесен не верный час начала процедуры. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();
                    }
                    else
                    {
                        if (dataGridView1.Rows[i].Cells["SvHourS"].Value != null)
                        {
                            if (result - hourS < 0)
                            {
                                /*error[0] = "Незначительная Ошибка";
                                tmp[0] = "Час начала процедуры раньше часа начала работы. Строка №" + (i + 1);
                                error = error.Concat(tmp).ToArray();*/
                            }
                            else
                            {
                                if (houre == -1)
                                {
                                    /*error[0] = "Незначительная Ошибка";
                                    tmp[0] = "Не заполнено время конца процедуры. Строка №" + (i);
                                    error = error.Concat(tmp).ToArray();*/
                                }
                                houre = -1;
                                hours = result;
                            }

                        }
                    }
                    if (dataGridView1.Rows[i].Cells["SvHourS"].Value == null & dataGridView1.Rows[i].Cells["SvMinuteS"].Value != null)
                    {
                        /*error[0] = "Незначительная Ошибка";
                        tmp[0] = "Внесены минуты, но не внесены часы начала процедуры. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();*/
                    }

                    //Время начала процедуры минуты
                    if (dataGridView1.Rows[i].Cells["SvMinuteS"].Value != null && (!Int32.TryParse(dataGridView1.Rows[i].Cells["SvMinuteS"].Value.ToString(), out result1) || result1 > 59))
                    {
                        error[0] = "Ошибка";
                        tmp[0] = "Внесены не верные минуты начала процедуры. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();
                    }
                    else
                    {
                        if (dataGridView1.Rows[i].Cells["SvMinuteS"].Value != null)
                        {
                            if (result - hourS == 0 & result1 - minuteS < 0)
                            {
                                /*error[0] = "Незначительная Ошибка";
                                tmp[0] = "Минуты начала процедуры раньше минут начала работы. Строка №" + (i + 1);
                                error = error.Concat(tmp).ToArray();*/
                            }
                            else
                            {
                                minutes = result1;
                            }

                        }
                    }

                    //Время конца процедуры часы
                    if (dataGridView1.Rows[i].Cells["SvHourE"].Value != null && (!Int32.TryParse(dataGridView1.Rows[i].Cells["SvHourE"].Value.ToString(), out result) || result > 23))
                    {
                        error[0] = "Ошибка";
                        tmp[0] = "Внесен не верный час окончания процедуры. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();
                    }
                    else
                    {
                        if (dataGridView1.Rows[i].Cells["SvHourE"].Value != null)
                        {
                            if (hourE - result < 0)
                            {
                                /*error[0] = "Незначительная Ошибка";
                                tmp[0] = "Час окончания процедуры позже часа окончания работы. Строка №" + (i + 1);
                                error = error.Concat(tmp).ToArray();
                                houre = -2;*/
                            }
                            else
                            {
                                if (hours == -1)
                                {
                                    /*error[0] = "Незначительная Ошибка";
                                    tmp[0] = "Не заполнено время начала процедуры. Строка №" + (i + 1);
                                    error = error.Concat(tmp).ToArray();*/
                                }
                                if (hours != -1 && result - hours < 0)
                                {
                                    /*error[0] = "Незначительная Ошибка";
                                    tmp[0] = "Час окончания процедуры раньше часа начала. Строка №" + (i + 1);
                                    error = error.Concat(tmp).ToArray();
                                    houre = -2;*/
                                }
                                else
                                {
                                    houre = result;
                                }

                            }

                        }
                    }
                    if (dataGridView1.Rows[i].Cells["SvHourE"].Value == null & dataGridView1.Rows[i].Cells["SvMinuteE"].Value != null)
                    {
                        /*error[0] = "Незначительная Ошибка";
                        tmp[0] = "Внесены минуты, но не внесены часы окончания процедуры. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();*/
                    }
                    if (i == 20 & houre == -1)
                    {
                        /*error[0] = "Незначительная Ошибка";
                        tmp[0] = "Не внесены часы окончания процедуры. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();*/
                    }

                    //Время конца процедуры минуты
                    if (dataGridView1.Rows[i].Cells["SvMinuteE"].Value != null && (!Int32.TryParse(dataGridView1.Rows[i].Cells["SvMinuteE"].Value.ToString(), out result1) || result1 > 59))
                    {
                        error[0] = "Ошибка";
                        tmp[0] = "Внесены не верные минуты окончания процедуры. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();
                    }
                    else
                    {
                        if (dataGridView1.Rows[i].Cells["SvMinuteE"].Value != null)
                        {
                            if (hourE - result == 0 & minuteE - result1 < 0)
                            {
                                /*error[0] = "Незначительная Ошибка";
                                tmp[0] = "Минуты окончания процедуры позже минут окончания работы. Строка №" + (i + 1);
                                error = error.Concat(tmp).ToArray();*/
                            }
                            if (houre - hours == 0 & result1 - minutes < 0)
                            {
                                /*error[0] = "Незначительная Ошибка";
                                tmp[0] = "Минуты окончания процедуры раньше минут начала процедуры. Строка №" + (i + 1);
                                error = error.Concat(tmp).ToArray();*/
                            }
                            hours = -1;

                        }
                    }
                    //Код услуги !!!!!!!!!!!!!!!!!!!!!
                    if (dataGridView1.Rows[i].Cells["SVCode"].Value == null)
                    {
                        /*error[0] = "Незначительная Ошибка";
                        tmp[0] = "Не заполнен код услуги. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();*/
                    }
                    else
                    {
                        if (!Int32.TryParse(dataGridView1.Rows[i].Cells["SVCode"].Value.ToString(), out result))
                        {
                            error[0] = "Ошибка";
                            tmp[0] = "Не верно заполнен код услуги. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        //Необходимо добавить проверку по кодам 
                    }
                    // Скидка
                    int discount = 0;
                    if (dataGridView1.Rows[i].Cells["Discount"].Value != null && !int.TryParse(dataGridView1.Rows[i].Cells["Discount"].Value.ToString(), out discount))
                    {
                        error[0] = "Ошибка";
                        tmp[0] = "Не верно внесена скидка. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();
                    }
                    else
                    {
                        if (discount < 0 | discount > 100)
                        {
                            error[0] = "Ошибка";
                            tmp[0] = "Скидка должна быть в диапазоне от 0 до 100. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                    }
                    // Скидка
                    int quantity = 1;
                    if (dataGridView1.Rows[i].Cells["Quantity"].Value != null && !int.TryParse(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString(), out quantity))
                    {
                        error[0] = "Ошибка";
                        tmp[0] = "Не верно внесено количество. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();
                    }
                    else
                    {
                        if (quantity <= 0)
                        {
                            error[0] = "Ошибка";
                            tmp[0] = "Количество должно быть больше 0. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                    }
                    //Стоимость
                    decimal result2;
                    if (dataGridView1.Rows[i].Cells["Summ"].Value == null)
                    {
                        /*error[0] = "Незначительная Ошибка";
                        tmp[0] = "Не внесена стоимость услуги. Строка №" + (i + 1);
                        error = error.Concat(tmp).ToArray();*/
                    }
                    else
                    {
                        if (!decimal.TryParse(dataGridView1.Rows[i].Cells["Summ"].Value.ToString(), out result2))
                        {
                            error[0] = "Ошибка";
                            tmp[0] = "Не верно внесена стоимость услуги. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        //Можно добавить проверку соответствия код - стоимость услуги 
                    }
                    if (dataGridView1.Rows[i].Cells["DDATE"].Value != null & dataGridView1.Rows[i].Cells["DDATE"].Value.ToString() != "")
                    {
                        bool err = false;
                        int dresult;
                        if (dataGridView1.Rows[i].Cells["DDATE"].Value.ToString().Length != 10)
                        {
                            err = true;
                        }
                        else
                        {
                            if (Int32.TryParse(dataGridView1.Rows[i].Cells["DDATE"].Value.ToString().Substring(0, 2), out dresult))
                            {
                                if (dresult > 31)
                                {
                                    err = true;
                                }
                            }
                            else
                            {
                                err = true;
                            }
                            if (dataGridView1.Rows[i].Cells["DDATE"].Value.ToString().Substring(2, 1) != "/")
                            {
                                err = true;
                            }
                            if (Int32.TryParse(dataGridView1.Rows[i].Cells["DDATE"].Value.ToString().Substring(3, 2), out dresult))
                            {
                                if (dresult > 12)
                                {
                                    err = true;
                                }
                            }
                            else
                            {
                                err = true;
                            }
                            if (dataGridView1.Rows[i].Cells["DDATE"].Value.ToString().Substring(5, 1) != "/")
                            {
                                err = true;
                            }
                            if (!Int32.TryParse(dataGridView1.Rows[i].Cells["DDATE"].Value.ToString().Substring(6, 4), out dresult))
                            {
                                err = true;
                            }
                        }
                        if (err)
                        {
                            error[0] = "Ошибка";
                            tmp[0] = "Не верный формат даты. Формат даты должен быть дд/мм/гггг. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                    }
                }
                else
                {
                    y = y + 1;
                    if (y == 21)
                    {
                        /*error[0] = "Ошибка";
                        tmp[0] = "Пустой рабочий лист";
                        error = error.Concat(tmp).ToArray();*/
                    }
                    if (houre == -1)
                    {
                        /*error[0] = "Незначительная Ошибка";
                        tmp[0] = "Не внесены часы окончания процедуры. Строка №" + (i);
                        error = error.Concat(tmp).ToArray();*/
                        houre = -2;
                    }
                }

            }


            if (error[0] != "Ошибка")
            {
                error[0] = "Ok";
            }
            return error;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["SVCOde"].Index)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["SVCOde"].Value != null)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value = 1;
                }
            }
           
            
            if (e.ColumnIndex == dataGridView1.Columns["SvHourS"].Index | e.ColumnIndex == dataGridView1.Columns["SvHourE"].Index | e.ColumnIndex == dataGridView1.Columns["SvMinuteS"].Index | e.ColumnIndex == dataGridView1.Columns["SvMinuteE"].Index)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length == 1)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0" + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    }
                }
            }
            if (e.ColumnIndex == dataGridView1.Columns["Summ"].Index)
            {

                decimal result, summ = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Summ"].Value != null)
                    {
                        dataGridView1.Rows[i].Cells["Summ"].Value = dataGridView1.Rows[i].Cells["Summ"].Value.ToString().Replace(".", ",");
                        if (dataGridView1.Rows[i].Cells["Summ"].Value != null && decimal.TryParse(dataGridView1.Rows[i].Cells["Summ"].Value.ToString(), out result))
                        {
                            summ = summ + result;
                        }
                    }
                }
                if (AutoSumm)
                {
                    textBoxSumm.Text = summ.ToString();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            switch (buttonAddSave.Text)
            {
                case "Сохранить":
                    treeViewWorkSheets.SelectedNode.Remove();
                    buttonAddSave.Text = "Добавить";
                    comboBoxSalon.Enabled = false;
                    textBoxNum.Enabled = false;
                    textBoxDay.Enabled = false;
                    textBoxMonth.Enabled = false;
                    textBoxYear.Enabled = false;
                    comboBoxWorker.Enabled = false;
                    textBoxHourS.Enabled = false;
                    textBoxMinuteS.Enabled = false;
                    textBoxHourE.Enabled = false;
                    textBoxMinuteE.Enabled = false;
                    dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
                    treeViewWorkSheets.Enabled = true;
                    buttonChange.Enabled = true;
                    if (treeViewWorkSheets.Nodes.Count > 0)
                    {
                        buttonAddToBase.Enabled = true;
                    } 
                    buttonDelete.Text = "Удалить";
                    break;
                case "Добавить":
                    if (buttonChange.Text == "Сохранить")
                    {
                        comboBoxSalon.Enabled = false;
                        textBoxNum.Enabled = false;
                        textBoxDay.Enabled = false;
                        textBoxMonth.Enabled = false;
                        textBoxYear.Enabled = false;
                        comboBoxWorker.Enabled = false;
                        textBoxHourS.Enabled = false;
                        textBoxMinuteS.Enabled = false;
                        textBoxHourE.Enabled = false;
                        textBoxMinuteE.Enabled = false;
                        dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
                        treeViewWorkSheets.Enabled = true;
                        buttonChange.Enabled = true;
                        buttonAddSave.Enabled = true;
                        if (treeViewWorkSheets.Nodes.Count > 0)
                        {
                            buttonAddToBase.Enabled = true;
                        } 
                        buttonDelete.Text = "Удалить";
                        buttonChange.Text = "Изменить";
                    }
                    else
                    {
                        XmlDocument wrksh = new XmlDocument();
                        wrksh.Load(wrkshPath + "\\wrksh.xml");
                        XmlNode node = wrksh.SelectSingleNode("/wrksh");
                        node.RemoveChild(wrksh.SelectSingleNode("/wrksh/WORKSHEET[" + (treeViewWorkSheets.SelectedNode.Index + 1) + "]"));
                        wrksh.Save(wrkshPath + "\\wrksh.xml");
                        treeViewWorkSheets.SelectedNode.Remove();
                    }
                    break;
            }
            
        }

        private void treeViewWorkSheets_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text != "Новый рабочий лист")
            {
                XmlDocument worksheets = new XmlDocument();
                worksheets.Load(wrkshPath + "\\wrksh.xml");
                XmlNode node = worksheets.SelectSingleNode("/wrksh/WORKSHEET[" + (e.Node.Index + 1) + "]");
                comboBoxSalon.Text = node.Attributes["SALON"].Value;
                textBoxNum.Text = node.Attributes["SHEETNUMBER"].Value;
                textBoxDay.Text = node.Attributes["DATE"].Value.Substring(0,2);
                textBoxMonth.Text = node.Attributes["DATE"].Value.Substring(2, 2);
                textBoxYear.Text = node.Attributes["DATE"].Value.Substring(4, 4);
                comboBoxWorker.Text = node.Attributes["FIO"].Value;
                
                textBoxHourS.Text = node.Attributes["TimeS"].Value.Substring(0, 2);
                textBoxMinuteS.Text = node.Attributes["TimeS"].Value.Substring(2, 2);
                textBoxHourE.Text = node.Attributes["TimeE"].Value.Substring(0, 2);
                textBoxMinuteE.Text = node.Attributes["TimeE"].Value.Substring(2, 2);
                textBoxSumm.Text = node.Attributes["SUMM"].Value;
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(21);
                foreach (DataGridViewRow dtrow in dataGridView1.Rows)
                {
                    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dtrow.Cells["Mistakes"];
                    cell.Value = mistakes_tbl.Rows[0].ItemArray[0];

                }
                XmlNodeList nodelst = worksheets.SelectNodes("/wrksh/WORKSHEET[" + (e.Node.Index + 1) + "]/ROWLINE");
                for (int i = 0; i < nodelst.Count; i++)
                {
                    if (nodelst[i].Attributes["ClientNum"].Value != "")
                    {
                        dataGridView1.Rows[i].Cells["ClientNum"].Value = nodelst[i].Attributes["ClientNum"].Value;
                    }
                    if (nodelst[i].Attributes["SvTimeS"].Value != "")
                    {
                        dataGridView1.Rows[i].Cells["SvHourS"].Value = nodelst[i].Attributes["SvTimeS"].Value.Substring(0, 2);
                        dataGridView1.Rows[i].Cells["SvMinuteS"].Value = nodelst[i].Attributes["SvTimeS"].Value.Substring(2, 2);
                    }
                    if (nodelst[i].Attributes["SvTimeE"].Value != "")
                    {
                    dataGridView1.Rows[i].Cells["SvHourE"].Value = nodelst[i].Attributes["SvTimeE"].Value.Substring(0, 2);
                    dataGridView1.Rows[i].Cells["SvMinuteE"].Value = nodelst[i].Attributes["SvTimeE"].Value.Substring(2, 2);
                    }
                    dataGridView1.Rows[i].Cells["SVCode"].Value = nodelst[i].Attributes["SVCode"].Value;
                    dataGridView1.Rows[i].Cells["Discount"].Value = nodelst[i].Attributes["Discount"].Value;
                    dataGridView1.Rows[i].Cells["Quantity"].Value = nodelst[i].Attributes["Quantity"].Value;
                    dataGridView1.Rows[i].Cells["Summ"].Value = nodelst[i].Attributes["Summ"].Value;
                    dataGridView1.Rows[i].Cells["Mistakes"].Value = nodelst[i].Attributes["WMistake"].Value;
                    dataGridView1.Rows[i].Cells["Client"].Value = nodelst[i].Attributes["Client"].Value;
                    dataGridView1.Rows[i].Cells["Master"].Value = nodelst[i].Attributes["Master"].Value;
                    dataGridView1.Rows[i].Cells["DDATE"].Value = nodelst[i].Attributes["DDATE"].Value;
                }
                
            }
        }

        private void textBoxDay_Leave(object sender, EventArgs e)
        {
            if (textBoxDay.Text.Length == 1)
            {
                textBoxDay.Text = "0" + textBoxDay.Text;
            }
        }

        private void textBoxMonth_Leave(object sender, EventArgs e)
        {
            if (textBoxMonth.Text.Length == 1)
            {
                textBoxMonth.Text = "0" + textBoxMonth.Text;
            }
        }

        private void textBoxHourS_Leave(object sender, EventArgs e)
        {
            if (textBoxHourS.Text.Length == 1)
            {
                textBoxHourS.Text = "0" + textBoxHourS.Text;
            }
        }

        private void textBoxMinuteS_Leave(object sender, EventArgs e)
        {
            if (textBoxMinuteS.Text.Length == 1)
            {
                textBoxMinuteS.Text = "0" + textBoxMinuteS.Text;
            }
        }

        private void textBoxHourE_Leave(object sender, EventArgs e)
        {
            if (textBoxHourE.Text.Length == 1)
            {
                textBoxHourE.Text = "0" + textBoxHourE.Text;
            }
        }

        private void textBoxMinuteE_Leave(object sender, EventArgs e)
        {
            if (textBoxMinuteE.Text.Length == 1)
            {
                textBoxMinuteE.Text = "0" + textBoxMinuteE.Text;
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            switch (buttonChange.Text)
            {
                case "Изменить":
                    comboBoxSalon.Enabled = true;
                    textBoxNum.Enabled = true;
                    textBoxDay.Enabled = true;
                    textBoxMonth.Enabled = true;
                    textBoxYear.Enabled = true;
                    comboBoxWorker.Enabled = true;
                    textBoxHourS.Enabled = true;
                    textBoxMinuteS.Enabled = true;
                    textBoxHourE.Enabled = true;
                    textBoxMinuteE.Enabled = true;
                    textBoxSumm.Enabled = true;
                    dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
                    treeViewWorkSheets.Enabled = false;
                    buttonAddSave.Enabled = false;
                    buttonAddToBase.Enabled = false;
                    buttonDelete.Text = "Отменить";
                    buttonChange.Text = "Сохранить";
                    break;
                case "Сохранить":
                    string[] error = check_fields();
                    if (error[0] == "Ok")
                    {
                        XmlDocument worksheets = new XmlDocument();
                        worksheets.Load(wrkshPath + "\\wrksh.xml");
                        XmlNode node = worksheets.SelectSingleNode("/wrksh/WORKSHEET[" + (treeViewWorkSheets.SelectedNode.Index + 1) + "]");
                        node.Attributes["SALON"].Value = comboBoxSalon.Text;
                        node.Attributes["SHEETNUMBER"].Value = textBoxNum.Text;
                        node.Attributes["DATE"].Value = textBoxDay.Text + textBoxMonth.Text + textBoxYear.Text;
                        node.Attributes["FIO"].Value = comboBoxWorker.Text;
                        node.Attributes["TimeS"].Value = textBoxHourS.Text + textBoxMinuteS.Text;
                        node.Attributes["TimeE"].Value = textBoxHourE.Text + textBoxMinuteE.Text;
                        node.Attributes["SUMM"].Value = textBoxSumm.Text;
                        if (node.HasChildNodes)
                        {
                            XmlNodeList nodelist = worksheets.SelectNodes("/wrksh/WORKSHEET[" + (treeViewWorkSheets.SelectedNode.Index + 1) + "]/ROWLINE");
                            foreach (XmlNode delNode in nodelist)
                            {
                                node.RemoveChild(delNode);
                            }
                        }
                        int rowNum = 1;
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value != null | dataGridView1.Rows[i].Cells[1].Value != null | dataGridView1.Rows[i].Cells[2].Value != null | dataGridView1.Rows[i].Cells[3].Value != null | dataGridView1.Rows[i].Cells[4].Value != null | dataGridView1.Rows[i].Cells[5].Value != null | dataGridView1.Rows[i].Cells[6].Value != null | dataGridView1.Rows[i].Cells[7].Value != null | dataGridView1.Rows[i].Cells[8].Value != null)
                            {
                                XmlElement rowLine = worksheets.CreateElement("ROWLINE");
                                rowLine.SetAttribute("ROWNUM", rowNum.ToString());
                                rowNum = rowNum + 1;
                                if (dataGridView1.Rows[i].Cells["ClientNum"].Value != null)
                                {
                                    rowLine.SetAttribute("ClientNum", dataGridView1.Rows[i].Cells["ClientNum"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("ClientNum", "");
                                }
                                if (dataGridView1.Rows[i].Cells["SvHourS"].Value != null)
                                {
                                    rowLine.SetAttribute("SvTimeS", dataGridView1.Rows[i].Cells["SvHourS"].Value.ToString() + dataGridView1.Rows[i].Cells["SvMinuteS"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("SvTimeS", "");
                                }
                                if (dataGridView1.Rows[i].Cells["SvHourE"].Value != null)
                                {
                                    rowLine.SetAttribute("SvTimeE", dataGridView1.Rows[i].Cells["SvHourE"].Value.ToString() + dataGridView1.Rows[i].Cells["SvMinuteE"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("SvTimeE", "");
                                }
                                if (dataGridView1.Rows[i].Cells["SVCode"].Value != null)
                                {
                                    rowLine.SetAttribute("SVCode", dataGridView1.Rows[i].Cells["SVCode"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("SVCode", "");
                                }
                                if (dataGridView1.Rows[i].Cells["Discount"].Value != null)
                                {
                                    rowLine.SetAttribute("Discount", dataGridView1.Rows[i].Cells["Discount"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("Discount", "0");
                                }
                                if (dataGridView1.Rows[i].Cells["Quantity"].Value != null)
                                {
                                    rowLine.SetAttribute("Quantity", dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("Quantity", "");
                                }
                                if (dataGridView1.Rows[i].Cells["Summ"].Value != null)
                                {
                                    rowLine.SetAttribute("Summ", dataGridView1.Rows[i].Cells["Summ"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("Summ", "");
                                }
                                rowLine.SetAttribute("WMistake", dataGridView1.Rows[i].Cells["Mistakes"].Value.ToString());
                                if (dataGridView1.Rows[i].Cells["Client"].Value != null)
                                {
                                    rowLine.SetAttribute("Client", dataGridView1.Rows[i].Cells["Client"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("Client", "");
                                }
                                if (dataGridView1.Rows[i].Cells["Master"].Value != null)
                                {
                                    rowLine.SetAttribute("Master", dataGridView1.Rows[i].Cells["Master"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("Master", "");
                                }
                                if (dataGridView1.Rows[i].Cells["DDATE"].Value != null)
                                {
                                    rowLine.SetAttribute("DDATE", dataGridView1.Rows[i].Cells["DDATE"].Value.ToString());
                                }
                                else
                                {
                                    rowLine.SetAttribute("DDATE", "");
                                }
                                node.AppendChild(rowLine);
                            }
                        }
                        worksheets.Save(wrkshPath + "\\wrksh.xml");
                        treeViewWorkSheets.SelectedNode.Text = comboBoxSalon.Text + " " + textBoxDay.Text + "/" + textBoxMonth.Text + "/" + textBoxYear.Text + " " + textBoxNum.Text + " " + comboBoxWorker.Text + " " + textBoxSumm.Text + " грн.";
                        treeViewWorkSheets.SelectedNode.Name = textBoxNum.Text;
                        comboBoxSalon.Enabled = false;
                        textBoxNum.Enabled = false;
                        textBoxDay.Enabled = false;
                        textBoxMonth.Enabled = false;
                        textBoxYear.Enabled = false;
                        comboBoxWorker.Enabled = false;
                        textBoxHourS.Enabled = false;
                        textBoxMinuteS.Enabled = false;
                        textBoxHourE.Enabled = false;
                        textBoxMinuteE.Enabled = false;
                        textBoxSumm.Enabled = false;
                        dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
                        treeViewWorkSheets.Enabled = true;
                        buttonAddSave.Enabled = true;
                        if (treeViewWorkSheets.Nodes.Count > 0)
                        {
                            buttonAddToBase.Enabled = true;
                        }
                        buttonDelete.Text = "Удалить";
                        buttonChange.Text = "Изменить";
                    }
                    else
                    {
                        ErrorForm err = new ErrorForm(error, "Ошибка заполнения данных");
                        err.ShowDialog();
                    }
                    break;
            }
        }

        private void buttonAddToBase_Click(object sender, EventArgs e)
        {
            bool error = false;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter par;
            
            cmd.CommandText = "LONDA.SAVEWRKSH";
            par = cmd.CreateParameter();
            par.ParameterName = "IN_SALON";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_SHEETNUM";
            par.OracleType = OracleType.Number;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_DATE";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_WORKER";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_SDATE";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_FDATE";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_SUMM";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_NROW";
            par.OracleType = OracleType.Number;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_NUMCL";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_SDATE1";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_FDATE1";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_SCODE";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_DISC";
            par.OracleType = OracleType.Number;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_QUANTITY";
            par.OracleType = OracleType.Number;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_PRICE";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_MISTAKES";
            par.OracleType = OracleType.Number;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_CLIENT";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_DMASTER";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "IN_DDATE";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "ERRSHEET";
            par.OracleType = OracleType.NVarChar;
            par.Size = 4000;
            par.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par);
            XmlDocument xmlsheet = new XmlDocument();
            xmlsheet.Load(wrkshPath + "\\wrksh.xml");
            
                XmlNodeList nodelst = xmlsheet.SelectNodes("/wrksh/WORKSHEET");
                string[] SheetNum = new string[1];
                for (int i = 0; i < nodelst.Count; i++)
                {
                    if (nodelst[i].HasChildNodes)
                    {
                        XmlNodeList nodelst2 = nodelst[i].ChildNodes;

                        for (int y = 0; y < nodelst2.Count; y++)
                        {
                            cmd.Parameters["IN_SALON"].Value = nodelst[i].Attributes["SALON"].Value;
                            cmd.Parameters["IN_SHEETNUM"].Value = int.Parse(nodelst[i].Attributes["SHEETNUMBER"].Value);
                            cmd.Parameters["IN_DATE"].Value = nodelst[i].Attributes["DATE"].Value;
                            cmd.Parameters["IN_WORKER"].Value = nodelst[i].Attributes["FIO"].Value;
                            cmd.Parameters["IN_SDATE"].Value = nodelst[i].Attributes["TimeS"].Value;
                            cmd.Parameters["IN_FDATE"].Value = nodelst[i].Attributes["TimeE"].Value;
                            cmd.Parameters["IN_SUMM"].Value = nodelst[i].Attributes["SUMM"].Value;
                            cmd.Parameters["IN_NROW"].Value = int.Parse(nodelst2[y].Attributes["ROWNUM"].Value);
                            cmd.Parameters["IN_NUMCL"].Value = nodelst2[y].Attributes["ClientNum"].Value;
                            cmd.Parameters["IN_SDATE1"].Value = nodelst2[y].Attributes["SvTimeS"].Value;
                            cmd.Parameters["IN_FDATE1"].Value = nodelst2[y].Attributes["SvTimeE"].Value;
                            cmd.Parameters["IN_SCODE"].Value = nodelst2[y].Attributes["SVCode"].Value;
                            cmd.Parameters["IN_DISC"].Value = int.Parse(nodelst2[y].Attributes["Discount"].Value);
                            cmd.Parameters["IN_QUANTITY"].Value = int.Parse(nodelst2[y].Attributes["Quantity"].Value);
                            cmd.Parameters["IN_PRICE"].Value = nodelst2[y].Attributes["Summ"].Value;
                            cmd.Parameters["IN_MISTAKES"].Value = int.Parse(nodelst2[y].Attributes["WMistake"].Value);
                            cmd.Parameters["IN_CLIENT"].Value = nodelst2[y].Attributes["Client"].Value;
                            cmd.Parameters["IN_DMASTER"].Value = nodelst2[y].Attributes["Master"].Value;
                            cmd.Parameters["IN_DDATE"].Value = nodelst2[y].Attributes["DDATE"].Value;
                            if (!Functions.ExecuteNonQuery(cmd))
                            {
                                error = true;
                                break;
                            }

                        }
                    }
                    else
                    {
                        cmd.Parameters["IN_SALON"].Value = nodelst[i].Attributes["SALON"].Value;
                        cmd.Parameters["IN_SHEETNUM"].Value = int.Parse(nodelst[i].Attributes["SHEETNUMBER"].Value);
                        cmd.Parameters["IN_DATE"].Value = nodelst[i].Attributes["DATE"].Value;
                        cmd.Parameters["IN_WORKER"].Value = nodelst[i].Attributes["FIO"].Value;
                        cmd.Parameters["IN_SDATE"].Value = nodelst[i].Attributes["TimeS"].Value;
                        cmd.Parameters["IN_FDATE"].Value = nodelst[i].Attributes["TimeE"].Value;
                        cmd.Parameters["IN_SUMM"].Value = nodelst[i].Attributes["SUMM"].Value;
                        cmd.Parameters["IN_NROW"].Value = 1;
                        cmd.Parameters["IN_NUMCL"].Value = "";
                        cmd.Parameters["IN_SDATE1"].Value = "";
                        cmd.Parameters["IN_FDATE1"].Value = "";
                        cmd.Parameters["IN_SCODE"].Value = "";
                        cmd.Parameters["IN_DISC"].Value = 0;
                        cmd.Parameters["IN_QUANTITY"].Value = 0;
                        cmd.Parameters["IN_PRICE"].Value = "";
                        cmd.Parameters["IN_MISTAKES"].Value = 0;
                        cmd.Parameters["IN_CLIENT"].Value = "";
                        cmd.Parameters["IN_DMASTER"].Value = "";
                        cmd.Parameters["IN_DDATE"].Value = "";
                        if (!Functions.ExecuteNonQuery(cmd))
                        {
                            error = true;
                        }
                    }
                    int result;
                    if (!error && !int.TryParse(cmd.Parameters["ERRSHEET"].Value.ToString(), out result))
                    {
                        MessageBox.Show(cmd.Parameters["ERRSHEET"].Value.ToString(), "Ошибка");
                        error = true;
                    }
                    if (error)
                    {
                        OracleCommand cmd2 = new OracleCommand("ROLLBACK", conn);
                        if (!Functions.ExecuteNonQuery(cmd2))
                        {
                            MessageBox.Show("Неудалось провести откат", "Ошибка");
                        }
                    }
                    else
                    {
                        OracleCommand cmd2 = new OracleCommand("COMMIT", conn);
                        if (!Functions.ExecuteNonQuery(cmd2))
                        {
                            MessageBox.Show("Неудалось провести COMMIT", "Ошибка");
                        }
                        TreeNode trNode = treeViewWorkSheets.Nodes[nodelst[i].Attributes["SHEETNUMBER"].Value];
                        string[] tmpStr = { nodelst[i].Attributes["SHEETNUMBER"].Value };
                        SheetNum = SheetNum.Concat(tmpStr).ToArray();
                        xmlsheet.ChildNodes[1].RemoveChild(nodelst[i]);
                        treeViewWorkSheets.Nodes.Remove(trNode);

                    }
                }
                
                if (treeViewWorkSheets.Nodes.Count > 0)
                {
                    treeViewWorkSheets.SelectedNode = treeViewWorkSheets.Nodes[0];
                }
                else
                {
                    buttonChange.Enabled = false;
                    buttonAddToBase.Enabled = false;
                    comboBoxWorker.SelectedIndex = -1;
                    comboBoxSalon.SelectedIndex = -1;
                    comboBoxWorker.Text = "";
                    comboBoxSalon.Text = "";
                    textBoxNum.Text = "";
                    textBoxDay.Text = "";
                    textBoxMonth.Text = "";
                    textBoxYear.Text = "";
                    textBoxHourS.Text = "";
                    textBoxMinuteS.Text = "";
                    textBoxHourE.Text = "";
                    textBoxMinuteS.Text = "";
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(21);
                }
                xmlsheet.Save(wrkshPath + "\\wrksh.xml");
                string mesg = "";
                for (int line = 1; line < SheetNum.Count(); line++)
                {
                    if (line == SheetNum.Count() - 1)
                    {
                        mesg = mesg + " " + SheetNum[line];
                    }
                    else
                    {
                        mesg = mesg + " " + SheetNum[line] + ",";
                    }
                }
                MessageBox.Show("Рабочий(е) лист(ы):" + mesg + " - добавлен(ы) в базу", "Сообщение");
            
        }

        private void FormWorkSheetSmall_Load(object sender, EventArgs e)
        {
            if (treeViewWorkSheets.Nodes.Count > 0)
            {
                treeViewWorkSheets.SelectedNode = treeViewWorkSheets.Nodes[0];
            }
            else
            {
                buttonChange.Enabled = false;
                buttonAddToBase.Enabled = false;
            }
        }

        private void textBoxSumm_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSumm.Text.IndexOf(".") != -1)
            {
                string thc = textBoxSumm.Text.Replace(".", ",");
                textBoxSumm.Text = "";
                textBoxSumm.AppendText(thc);
            }
        }

       


    }
}
