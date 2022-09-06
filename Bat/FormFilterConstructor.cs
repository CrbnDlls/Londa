using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bat
{
    public partial class FormFilterConstructor : Form
    {
        DataTable filterTbl, DataType;
        string Command;
        string[] StrFilter; 
        public DataTable FilterTable
        {
            get { return filterTbl; }
        }
        public string[] AddFilter
        {
            get { return StrFilter; }
        }
        public FormFilterConstructor(DataTable OraColumns, DataTable Filter, string[] StringFilter, string OraCommand)
        {
            InitializeComponent();
            DataGridViewComboBoxColumn comCol = (DataGridViewComboBoxColumn)dataGridViewFilter.Columns["ColumnColName"];
            StrFilter = StringFilter;
            DataType = OraColumns;
            Command = OraCommand;
            comCol.DataSource = OraColumns;
            comCol.DisplayMember = "Подписи";
            comCol.ValueMember = "OraColumns";
            filterTbl = Filter;
            if (Filter == null)
            {
                /*dataGridViewFilter.Rows.Add();
                DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[0].Cells[0];
                comboCell.Value = comboCell.Items[0];
                comboCell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[0].Cells[1];
                comboCell.Value = comboCell.Items[0];
                comboCell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[0].Cells[2];
                DataRowView row = (DataRowView)comboCell.Items[0];
                comboCell.Value = row[1];
                comboCell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[0].Cells[3];
                comboCell.Value = comboCell.Items[0];
                comboCell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[0].Cells[5];
                comboCell.Value = comboCell.Items[0];*/
                filterTbl = new DataTable();
                filterTbl.Columns.Add("AndOr");
                filterTbl.Columns.Add("SkobkaOpen");
                filterTbl.Columns.Add("OraCol");
                filterTbl.Columns.Add("Equals");
                filterTbl.Columns.Add("Value");
                filterTbl.Columns.Add("SkobkaClose");
                filterTbl.Columns.Add("DataType");
            }
            else
            {
                for (int h = 0; h < filterTbl.Rows.Count; h++)
                {
                    dataGridViewFilter.Rows.Add();
                    dataGridViewFilter.Rows[h].Cells[0].Value = filterTbl.Rows[h].ItemArray[0];
                    dataGridViewFilter.Rows[h].Cells[1].Value = filterTbl.Rows[h].ItemArray[1];
                    dataGridViewFilter.Rows[h].Cells[2].Value = filterTbl.Rows[h].ItemArray[2];
                    dataGridViewFilter.Rows[h].Cells[3].Value = filterTbl.Rows[h].ItemArray[3];
                    dataGridViewFilter.Rows[h].Cells[4].Value = filterTbl.Rows[h].ItemArray[4];
                    dataGridViewFilter.Rows[h].Cells[5].Value = filterTbl.Rows[h].ItemArray[5];
                }
                //dataGridViewFilter.DataSource = filterTbl;
            }
            
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            dataGridViewFilter.Rows.Add();
            DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[dataGridViewFilter.Rows.Count - 1].Cells[0];
            comboCell.Value = comboCell.Items[0];
            comboCell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[dataGridViewFilter.Rows.Count - 1].Cells[1];
            comboCell.Value = comboCell.Items[0];
            comboCell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[dataGridViewFilter.Rows.Count - 1].Cells[2];
            DataRowView row = (DataRowView)comboCell.Items[0];
            comboCell.Value = row[1];
            comboCell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[dataGridViewFilter.Rows.Count - 1].Cells[3];
            comboCell.Value = comboCell.Items[0];
            comboCell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[dataGridViewFilter.Rows.Count - 1].Cells[5];
            comboCell.Value = comboCell.Items[0];
        }

        private void buttonDeleteRow_Click(object sender, EventArgs e)
        {
            dataGridViewFilter.Rows.Remove(dataGridViewFilter.SelectedRows[0]);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            filterTbl.Rows.Clear();
            StrFilter[0] = "";
            StrFilter[1] = "";
            
            for (int a = 0; a < dataGridViewFilter.Rows.Count; a++)
            {
                DataRow row = filterTbl.NewRow();
                row[0] = dataGridViewFilter.Rows[a].Cells[0].Value;
                row[1] = dataGridViewFilter.Rows[a].Cells[1].Value;
                row[2] = dataGridViewFilter.Rows[a].Cells[2].Value;
                row[3] = dataGridViewFilter.Rows[a].Cells[3].Value;
                row[4] = dataGridViewFilter.Rows[a].Cells[4].Value;
                row[5] = dataGridViewFilter.Rows[a].Cells[5].Value;
                row[6] = dataGridViewFilter.Rows[a].Cells[6].Value;
                filterTbl.Rows.Add(row);
            }
            StrFilter = GetFilter(filterTbl, Command, DataType);
            DialogResult = DialogResult.OK;
        }

       /* public static string[] GetFilter(DataTable FilterTbl, string OraCommand, DataTable FilterColumns)
        {
            string line = "";
            string line2 = "";
            if (FilterTbl != null)
            {
                for (int a = 0; a < FilterTbl.Rows.Count; a++)
                {

                    switch (FilterTbl.Rows[a].ItemArray[3].ToString())
                    {
                        case "равно":
                            // Добавление AND; OR
                            if (line == "" | FilterTbl.Rows[a].ItemArray[0].ToString() == "И")
                            {
                                if (OraCommand.IndexOf("WHERE") == -1)
                                {
                                    line = line + " WHERE";
                                }
                                else
                                {
                                    line = line + " AND";
                                    line2 = line2 + " И";
                                }
                            }
                            else
                            {
                                line = line + " OR";
                                line2 = line2 + " ИЛИ";
                            }
                            // Добавление (
                            if (line == " OR" | line == " AND" | line == " WHERE" | FilterTbl.Rows[a].ItemArray[1].ToString() == "(")
                            {
                                line = line + " (";
                                line2 = line2 + " (";
                            }
                            // Добавление Названия колонки таблицы
                            line = line + " " + FilterTbl.Rows[a].ItemArray[2].ToString();
                            for (int b = 0; b < FilterColumns.Rows.Count; b++)
                            {
                                if (FilterColumns.Rows[b].ItemArray[1].ToString() == FilterTbl.Rows[a].ItemArray[2].ToString())
                                {
                                    line2 = line2 + " " + FilterColumns.Rows[b].ItemArray[0].ToString();
                                }
                            }
                            // Добавление =; <>; >; <; LIKE; NOT LIKE
                            line = line + " =";
                            line2 = line2 + " =";
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "date")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','dd/mm/yyyy')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "number")
                            {
                                line = line + " replace('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "', '.', ',')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "time")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','HH24:MI')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "")
                            {
                                line = line + " '" + FilterTbl.Rows[a].ItemArray[4].ToString() + "'";
                            }
                            line2 = line2 + " " + FilterTbl.Rows[a].ItemArray[4].ToString();
                            // Добавление )
                            if (a == (FilterTbl.Rows.Count - 1) | FilterTbl.Rows[a].ItemArray[5].ToString() == ")")
                            {
                                line = line + " )";
                                line2 = line2 + " )";
                            }
                            break;
                        case "не равно":
                            // Добавление AND; OR
                            if (line == "" | FilterTbl.Rows[a].ItemArray[0].ToString() == "И")
                            {
                                if (OraCommand.IndexOf("WHERE") == -1)
                                {
                                    line = line + " WHERE";
                                }
                                else
                                {
                                    line = line + " AND";
                                    line2 = line2 + " И";
                                }
                            }
                            else
                            {
                                line = line + " OR";
                                line2 = line2 + " ИЛИ";
                            }
                            // Добавление (
                            if (line == " OR" | line == " AND" | line == " WHERE" | FilterTbl.Rows[a].ItemArray[1].ToString() == "(")
                            {
                                line = line + " (";
                                line2 = line2 + " (";
                            }
                            // Добавление Названия колонки таблицы
                            line = line + " " + FilterTbl.Rows[a].ItemArray[2].ToString();
                            for (int b = 0; b < FilterColumns.Rows.Count; b++)
                            {
                                if (FilterColumns.Rows[b].ItemArray[1].ToString() == FilterTbl.Rows[a].ItemArray[2].ToString())
                                {
                                    line2 = line2 + " " + FilterColumns.Rows[b].ItemArray[0].ToString();
                                }
                            }
                            // Добавление =; <>; >; <; LIKE; NOT LIKE
                            line = line + " <>";
                            line2 = line2 + " <>";
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "date")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','dd/mm/yyyy')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "number")
                            {
                                line = line + " replace('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "', '.', ',')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "time")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','HH24:MI')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "")
                            {
                                line = line + " '" + FilterTbl.Rows[a].ItemArray[4].ToString() + "'";
                            }
                            line2 = line2 + " " + FilterTbl.Rows[a].ItemArray[4].ToString();
                            // Добавление )
                            if (a == (FilterTbl.Rows.Count - 1) | FilterTbl.Rows[a].ItemArray[5].ToString() == ")")
                            {
                                line = line + " )";
                                line2 = line2 + " )";
                            }
                            break;
                        case "больше":
                            // Добавление AND; OR
                            if (line == "" | FilterTbl.Rows[a].ItemArray[0].ToString() == "И")
                            {
                                if (OraCommand.IndexOf("WHERE") == -1)
                                {
                                    line = line + " WHERE";
                                }
                                else
                                {
                                    line = line + " AND";
                                    line2 = line2 + " И";
                                }
                            }
                            else
                            {
                                line = line + " OR";
                                line2 = line2 + " ИЛИ";
                            }
                            // Добавление (
                            if (line == " OR" | line == " AND" | line == " WHERE" | FilterTbl.Rows[a].ItemArray[1].ToString() == "(")
                            {
                                line = line + " (";
                                line2 = line2 + " (";
                            }
                            // Добавление Названия колонки таблицы
                            line = line + " " + FilterTbl.Rows[a].ItemArray[2].ToString();
                            for (int b = 0; b < FilterColumns.Rows.Count; b++)
                            {
                                if (FilterColumns.Rows[b].ItemArray[1].ToString() == FilterTbl.Rows[a].ItemArray[2].ToString())
                                {
                                    line2 = line2 + " " + FilterColumns.Rows[b].ItemArray[0].ToString();
                                }
                            }
                            // Добавление =; <>; >; <; LIKE; NOT LIKE
                            line = line + " >";
                            line2 = line2 + " >";
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "date")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','dd/mm/yyyy')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "number")
                            {
                                line = line + " replace('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "', '.', ',')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "time")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','HH24:MI')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "")
                            {
                                line = line + " '" + FilterTbl.Rows[a].ItemArray[4].ToString() + "'";
                            }
                            line2 = line2 + " " + FilterTbl.Rows[a].ItemArray[4].ToString();
                            // Добавление )
                            if (a == (FilterTbl.Rows.Count - 1) | FilterTbl.Rows[a].ItemArray[5].ToString() == ")")
                            {
                                line = line + " )";
                                line2 = line2 + " )";
                            }
                            break;
                        case "меньше":
                            // Добавление AND; OR
                            if (line == "" | FilterTbl.Rows[a].ItemArray[0].ToString() == "И")
                            {
                                if (OraCommand.IndexOf("WHERE") == -1)
                                {
                                    line = line + " WHERE";
                                }
                                else
                                {
                                    line = line + " AND";
                                    line2 = line2 + " И";
                                }
                            }
                            else
                            {
                                line = line + " OR";
                                line2 = line2 + " ИЛИ";
                            }
                            // Добавление (
                            if (line == " OR" | line == " AND" | line == " WHERE" | FilterTbl.Rows[a].ItemArray[1].ToString() == "(")
                            {
                                line = line + " (";
                                line2 = line2 + " (";
                            }
                            // Добавление Названия колонки таблицы
                            line = line + " " + FilterTbl.Rows[a].ItemArray[2].ToString();
                            for (int b = 0; b < FilterColumns.Rows.Count; b++)
                            {
                                if (FilterColumns.Rows[b].ItemArray[1].ToString() == FilterTbl.Rows[a].ItemArray[2].ToString())
                                {
                                    line2 = line2 + " " + FilterColumns.Rows[b].ItemArray[0].ToString();
                                }
                            }
                            // Добавление =; <>; >; <; LIKE; NOT LIKE
                            line = line + " <";
                            line2 = line2 + " <";
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "date")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','dd/mm/yyyy')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "number")
                            {
                                line = line + " replace('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "', '.', ',')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "time")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','HH24:MI')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "")
                            {
                                line = line + " '" + FilterTbl.Rows[a].ItemArray[4].ToString() + "'";
                            }
                            line2 = line2 + " " + FilterTbl.Rows[a].ItemArray[4].ToString();
                            // Добавление )
                            if (a == (FilterTbl.Rows.Count - 1) | FilterTbl.Rows[a].ItemArray[5].ToString() == ")")
                            {
                                line = line + " )";
                                line2 = line2 + " )";
                            }
                            break;
                        case "похоже":
                            // Добавление AND; OR
                            string ifDateOrTime = line;
                            string ifDateOrTime2 = line2;
                            bool ifDateTime = false;
                            if (line == "" | FilterTbl.Rows[a].ItemArray[0].ToString() == "И")
                            {
                                if (OraCommand.IndexOf("WHERE") == -1)
                                {
                                    line = line + " WHERE";
                                }
                                else
                                {
                                    line = line + " AND";
                                    line2 = line2 + " И";
                                }
                            }
                            else
                            {
                                line = line + " OR";
                                line2 = line2 + " ИЛИ";
                            }
                            // Добавление (
                            if (line == " OR" | line == " AND" | line == " WHERE" | FilterTbl.Rows[a].ItemArray[1].ToString() == "(")
                            {
                                line = line + " (";
                                line2 = line2 + " (";
                            }
                            // Добавление Названия колонки таблицы
                            line = line + " " + FilterTbl.Rows[a].ItemArray[2].ToString();
                            for (int b = 0; b < FilterColumns.Rows.Count; b++)
                            {
                                if (FilterColumns.Rows[b].ItemArray[1].ToString() == FilterTbl.Rows[a].ItemArray[2].ToString())
                                {
                                    line2 = line2 + " " + FilterColumns.Rows[b].ItemArray[0].ToString();
                                }
                            }
                            // Добавление =; <>; >; <; LIKE; NOT LIKE
                            line = line + " LIKE";
                            line2 = line2 + " похоже на";
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "date")
                            {
                                ifDateTime = true;
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "number")
                            {
                                line = line + " replace('%" + FilterTbl.Rows[a].ItemArray[4].ToString() + "%', '.', ',')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "time")
                            {
                                ifDateTime = true;
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "")
                            {
                                line = line + " '%" + FilterTbl.Rows[a].ItemArray[4].ToString() + "%'";
                            }
                            line2 = line2 + " " + FilterTbl.Rows[a].ItemArray[4].ToString();
                            // Добавление )
                            if (a == (FilterTbl.Rows.Count - 1) | FilterTbl.Rows[a].ItemArray[5].ToString() == ")")
                            {
                                line = line + " )";
                                line2 = line2 + " )";
                            }
                            if (ifDateTime)
                            {
                                if (a == (FilterTbl.Rows.Count - 1))
                                {
                                    line = ifDateOrTime + " )";
                                    line2 = ifDateOrTime2 + " )";
                                }
                                else
                                {
                                    line = ifDateOrTime;
                                    line2 = ifDateOrTime2;
                                }

                            }
                            break;
                        case "не похоже":
                            ifDateOrTime = line;
                            ifDateOrTime2 = line2;
                            ifDateTime = false;
                            // Добавление AND; OR

                            if (line == "" | FilterTbl.Rows[a].ItemArray[0].ToString() == "И")
                            {
                                if (OraCommand.IndexOf("WHERE") == -1)
                                {
                                    line = line + " WHERE";
                                }
                                else
                                {
                                    line = line + " AND";
                                    line2 = line2 + " И";
                                }
                            }
                            else
                            {
                                line = line + " OR";
                                line2 = line2 + " ИЛИ";
                            }
                            // Добавление (
                            if (line == " OR" | line == " AND" | line == " WHERE" | FilterTbl.Rows[a].ItemArray[1].ToString() == "(")
                            {
                                line = line + " (";
                                line2 = line2 + " (";
                            }
                            // Добавление Названия колонки таблицы
                            line = line + " " + FilterTbl.Rows[a].ItemArray[2].ToString();
                            for (int b = 0; b < FilterColumns.Rows.Count; b++)
                            {
                                if (FilterColumns.Rows[b].ItemArray[1].ToString() == FilterTbl.Rows[a].ItemArray[2].ToString())
                                {
                                    line2 = line2 + " " + FilterColumns.Rows[b].ItemArray[0].ToString();
                                }
                            }
                            // Добавление =; <>; >; <; LIKE; NOT LIKE
                            line = line + " NOT LIKE";
                            line2 = line2 + " не похоже";
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "date")
                            {
                                ifDateTime = true;
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "number")
                            {
                                line = line + " replace('%" + FilterTbl.Rows[a].ItemArray[4].ToString() + "%', '.', ',')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "time")
                            {
                                ifDateTime = true;
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "")
                            {
                                line = line + " '%" + FilterTbl.Rows[a].ItemArray[4].ToString() + "%'";
                            }
                            line2 = line2 + " " + FilterTbl.Rows[a].ItemArray[4].ToString();
                            // Добавление )
                            if (a == (FilterTbl.Rows.Count - 1) | FilterTbl.Rows[a].ItemArray[5].ToString() == ")")
                            {
                                line = line + " )";
                                line2 = line2 + " )";
                            }
                            if (ifDateTime)
                            {
                                if (a == (FilterTbl.Rows.Count - 1))
                                {
                                    line = ifDateOrTime + " )";
                                    line2 = ifDateOrTime2 + " )";
                                }
                                else
                                {
                                    line = ifDateOrTime;
                                    line2 = ifDateOrTime2;
                                }

                            }
                            break;
                        case "больше равно":
                            // Добавление AND; OR
                            if (line == "" | FilterTbl.Rows[a].ItemArray[0].ToString() == "И")
                            {
                                if (OraCommand.IndexOf("WHERE") == -1)
                                {
                                    line = line + " WHERE";
                                }
                                else
                                {
                                    line = line + " AND";
                                    line2 = line2 + " И";
                                }
                            }
                            else
                            {
                                line = line + " OR";
                                line2 = line2 + " ИЛИ";
                            }
                            // Добавление (
                            if (line == " OR" | line == " AND" | line == " WHERE" | FilterTbl.Rows[a].ItemArray[1].ToString() == "(")
                            {
                                line = line + " (";
                                line2 = line2 + " (";
                            }
                            // Добавление Названия колонки таблицы
                            line = line + " " + FilterTbl.Rows[a].ItemArray[2].ToString();
                            for (int b = 0; b < FilterColumns.Rows.Count; b++)
                            {
                                if (FilterColumns.Rows[b].ItemArray[1].ToString() == FilterTbl.Rows[a].ItemArray[2].ToString())
                                {
                                    line2 = line2 + " " + FilterColumns.Rows[b].ItemArray[0].ToString();
                                }
                            }
                            // Добавление =; <>; >; <; LIKE; NOT LIKE
                            line = line + " >=";
                            line2 = line2 + " >=";
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "date")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','dd/mm/yyyy')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "number")
                            {
                                line = line + " replace('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "', '.', ',')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "time")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','HH24:MI')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "")
                            {
                                line = line + " '" + FilterTbl.Rows[a].ItemArray[4].ToString() + "'";
                            }
                            line2 = line2 + " " + FilterTbl.Rows[a].ItemArray[4].ToString();
                            // Добавление )
                            if (a == (FilterTbl.Rows.Count - 1) | FilterTbl.Rows[a].ItemArray[5].ToString() == ")")
                            {
                                line = line + " )";
                                line2 = line2 + " )";
                            }
                            break;
                        case "меньше равно":
                            // Добавление AND; OR
                            if (line == "" | FilterTbl.Rows[a].ItemArray[0].ToString() == "И")
                            {
                                if (OraCommand.IndexOf("WHERE") == -1)
                                {
                                    line = line + " WHERE";
                                }
                                else
                                {
                                    line = line + " AND";
                                    line2 = line2 + " И";
                                }
                            }
                            else
                            {
                                line = line + " OR";
                                line2 = line2 + " ИЛИ";
                            }
                            // Добавление (
                            if (line == " OR" | line == " AND" | line == " WHERE" | FilterTbl.Rows[a].ItemArray[1].ToString() == "(")
                            {
                                line = line + " (";
                                line2 = line2 + " (";
                            }
                            // Добавление Названия колонки таблицы
                            line = line + " " + FilterTbl.Rows[a].ItemArray[2].ToString();
                            for (int b = 0; b < FilterColumns.Rows.Count; b++)
                            {
                                if (FilterColumns.Rows[b].ItemArray[1].ToString() == FilterTbl.Rows[a].ItemArray[2].ToString())
                                {
                                    line2 = line2 + " " + FilterColumns.Rows[b].ItemArray[0].ToString();
                                }
                            }
                            // Добавление =; <>; >; <; LIKE; NOT LIKE
                            line = line + " <=";
                            line2 = line2 + " <=";
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "date")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','dd/mm/yyyy')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "number")
                            {
                                line = line + " replace('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "', '.', ',')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "time")
                            {
                                line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','HH24:MI')";
                            }
                            if (FilterTbl.Rows[a].ItemArray[6].ToString() == "")
                            {
                                line = line + " '" + FilterTbl.Rows[a].ItemArray[4].ToString() + "'";
                            }
                            line2 = line2 + " " + FilterTbl.Rows[a].ItemArray[4].ToString();
                            // Добавление )
                            if (a == (FilterTbl.Rows.Count - 1) | FilterTbl.Rows[a].ItemArray[5].ToString() == ")")
                            {
                                line = line + " )";
                                line2 = line2 + " )";
                            }
                            break;
                    }


                }
            }
            string[] result = new string[2];
            result[0] = line;
            result[1] = line2;
            return result;
        }*/

        public static string[] GetFilter(DataTable FilterTbl, string OraCommand, DataTable FilterColumns)
        {
            string line = "";
            string line2 = "";
            bool Error = false;
            bool SkobkaOpen = false;
            if (FilterTbl != null)
            {
                for (int a = 0; a < FilterTbl.Rows.Count; a++)
                {
                    string ifDateOrTime = line;
                    string ifDateOrTime2 = line2;
                    bool DateOrTime = false;
                    bool LikeOrNot = false;
                    
                    
                    // Добавление AND; OR
                    if (line == "" | FilterTbl.Rows[a].ItemArray[0].ToString() == "И")
                    {
                        if (OraCommand.IndexOf("WHERE") == -1 & line.IndexOf("WHERE") == -1)
                        {
                            line = line + " WHERE";
                        }
                        else
                        {
                            line = line + " AND";
                            line2 = line2 + " И";
                        }
                    }
                    else
                    {
                        line = line + " OR";
                        line2 = line2 + " ИЛИ";
                    }
                    // Добавление (
                    if (line == " OR" | line == " AND" | line == " WHERE")
                    {
                        line = line + " (";
                        line2 = line2 + " (";
                    }
                    if (FilterTbl.Rows[a].ItemArray[1].ToString() == "(")
                    {
                        if (SkobkaOpen)
                        {
                            Error = true;
                        }
                        line = line + " (";
                        line2 = line2 + " (";
                        SkobkaOpen = true;

                    }
                    // Добавление Названия колонки таблицы
                    line = line + " " + FilterTbl.Rows[a].ItemArray[2].ToString();
                    for (int b = 0; b < FilterColumns.Rows.Count; b++)
                    {
                        if (FilterColumns.Rows[b].ItemArray[1].ToString() == FilterTbl.Rows[a].ItemArray[2].ToString())
                        {
                            line2 = line2 + " " + FilterColumns.Rows[b].ItemArray[0].ToString();
                        }
                    }
                    // Добавление =; <>; >; <; LIKE; NOT LIKE; IS NULL; IS NOT NULL;
                    switch (FilterTbl.Rows[a].ItemArray[3].ToString())
                    {
                        case "равно":
                            line = line + " =";
                            line2 = line2 + " =";
                            break;
                        case "не равно":
                            line = line + " <>";
                            line2 = line2 + " <>";
                            break;
                        case "больше":
                            line = line + " >";
                            line2 = line2 + " >";
                            break;
                        case "меньше":
                            line = line + " <";
                            line2 = line2 + " <";
                            break;
                        case "похоже":
                            line = line + " LIKE";
                            line2 = line2 + " похоже на";
                            LikeOrNot = true;
                            break;
                        case "не похоже":
                            line = line + " NOT LIKE";
                            line2 = line2 + " не похоже";
                            LikeOrNot = true;
                            break;
                        case "больше равно":
                            line = line + " >=";
                            line2 = line2 + " >=";
                            break;
                        case "меньше равно":
                            line = line + " <=";
                            line2 = line2 + " <=";
                            break;
                        case "пустое значение":
                            line = line + " IS NULL";
                            line2 = line2 + " = пустое значение";
                            break;
                        case "не пустое значение":
                            line = line + " IS NOT NULL";
                            line2 = line2 + " = не пустое значение";
                            break;
                    }

                    if (FilterTbl.Rows[a].ItemArray[3].ToString() != "пустое значение" & FilterTbl.Rows[a].ItemArray[3].ToString() != "не пустое значение")
                    {
                        // Добавляем значения
                        switch (FilterTbl.Rows[a].ItemArray[6].ToString())
                        {
                            case "date":
                                if (!LikeOrNot)
                                {
                                    line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','dd/mm/yyyy')";
                                }
                                else
                                {
                                    DateOrTime = true;
                                }
                                break;
                            case "number":
                                if (!LikeOrNot)
                                {
                                    line = line + " replace('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "', '.', ',')";
                                }
                                else
                                {
                                    line = line + " replace('%" + FilterTbl.Rows[a].ItemArray[4].ToString() + "%', '.', ',')";
                                }
                                break;
                            case "time":
                                if (!LikeOrNot)
                                {
                                    line = line + " to_date('" + FilterTbl.Rows[a].ItemArray[4].ToString() + "','HH24:MI')";
                                }
                                else
                                {
                                    DateOrTime = true;
                                }
                                break;
                            default:
                                if (!LikeOrNot)
                                {
                                    line = line + " '" + FilterTbl.Rows[a].ItemArray[4].ToString() + "'";
                                }
                                else
                                {
                                    line = line + " '%" + FilterTbl.Rows[a].ItemArray[4].ToString() + "%'";
                                }
                                break;
                        }
                        line2 = line2 + " " + FilterTbl.Rows[a].ItemArray[4].ToString();
                    }
                    // Добавление )
                    if (FilterTbl.Rows[a].ItemArray[5].ToString() == ")")
                    {
                        if (!SkobkaOpen)
                        {
                            Error = true;
                        }
                        line = line + " )";
                        line2 = line2 + " )";
                        SkobkaOpen = false;
                    }
                    if (a == (FilterTbl.Rows.Count - 1))
                    {
                        line = line + " )";
                        line2 = line2 + " )";
                    }
                    if (DateOrTime)
                    {
                        if (a == (FilterTbl.Rows.Count - 1))
                        {
                            line = ifDateOrTime + " )";
                            line2 = ifDateOrTime2 + " )";
                        }
                        else
                        {
                            line = ifDateOrTime;
                            line2 = ifDateOrTime2;
                        }

                    }

                }
            }
            string[] result = new string[2];
            if (Error | SkobkaOpen)
            {
                result[0] = "";
                result[1] = "Не открыта (закрыта) скобка";
            }
            else
            {
                result[0] = line;
                result[1] = line2;
            }
            return result;
        }

        private void dataGridViewFilter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 & e.RowIndex != -1)
            {
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dataGridViewFilter.Rows[e.RowIndex].Cells[2];
                DataTable table = (DataTable)cell.DataSource;
                for (int v = 0; v < table.Rows.Count; v++)
                {
                    if (table.Rows[v].ItemArray[1].ToString() == dataGridViewFilter.Rows[e.RowIndex].Cells[2].Value.ToString())
                    {
                        dataGridViewFilter.Rows[e.RowIndex].Cells[6].Value = table.Rows[v].ItemArray[2].ToString();
                    }
                }
            }

            if (e.ColumnIndex == 3 & e.RowIndex != -1)
            {
                if ((dataGridViewFilter.Rows[e.RowIndex].Cells[3].Value.ToString() == "похоже" | dataGridViewFilter.Rows[e.RowIndex].Cells[3].Value.ToString() == "не похоже") & (dataGridViewFilter.Rows[e.RowIndex].Cells[6].Value.ToString() == "date" | dataGridViewFilter.Rows[e.RowIndex].Cells[6].Value.ToString() == "time"))
                {
                    MessageBox.Show("Для данных типа: время или дата, не возможно установить значение равенства: похоже или не похоже. Строка: " + (e.RowIndex + 1) + " не будет учитываться в фильтре.", "Ошибка");
                }
    
            }
        }
    }
}
