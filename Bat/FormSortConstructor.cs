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
    public partial class FormSortConstructor : Form
    {
        DataTable sortTbl;
        string sortStr;
        public string AddSort
        {
            get { return sortStr; }
        }
        public DataTable SortTable
        {
            get { return sortTbl; }
        }
        public FormSortConstructor(DataTable OraColumns, DataTable Sort, string StringSort)
        {
            InitializeComponent();
            sortTbl = Sort;
            sortStr = StringSort;
            DataTable direction = new DataTable();
            direction.Columns.Add("Ora");
            direction.Columns.Add("User");
            DataRow row = direction.NewRow();
            row[0] = "ASC";
            row[1] = "Увеличение";
            direction.Rows.Add(row);
            row = direction.NewRow();
            row[0] = "DESC";
            row[1] = "Уменьшение";
            direction.Rows.Add(row);
            DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dataGridViewSort.Columns[1];
            col.DataSource = direction;
            col.DisplayMember = "User";
            col.ValueMember = "Ora";
            col = (DataGridViewComboBoxColumn)dataGridViewSort.Columns[0];
            col.DataSource = OraColumns;
            col.DisplayMember = "Подписи";
            col.ValueMember = "OraColumns";
            if (sortTbl == null)
            {
                /*dataGridViewSort.Rows.Add();
                DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)dataGridViewSort.Rows[0].Cells[0];
                DataRowView rowView = (DataRowView)comboCell.Items[0];
                comboCell.Value = rowView[1];
                comboCell = (DataGridViewComboBoxCell)dataGridViewSort.Rows[0].Cells[1];
                rowView = (DataRowView)comboCell.Items[0];
                comboCell.Value = rowView[0];*/
                sortTbl = new DataTable();
                sortTbl.Columns.Add("OraCol");
                sortTbl.Columns.Add("Direction");
                }
            else
            {
                for (int h = 0; h < sortTbl.Rows.Count; h++)
                {
                    dataGridViewSort.Rows.Add();
                    dataGridViewSort.Rows[h].Cells[0].Value = sortTbl.Rows[h].ItemArray[0];
                    dataGridViewSort.Rows[h].Cells[1].Value = sortTbl.Rows[h].ItemArray[1];
                    
                }
                //dataGridViewFilter.DataSource = filterTbl;
            }
            
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            dataGridViewSort.Rows.Add(); DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)dataGridViewSort.Rows[dataGridViewSort.Rows.Count - 1].Cells[0];
            DataRowView rowView = (DataRowView)comboCell.Items[0];
            comboCell.Value = rowView[1];
            comboCell = (DataGridViewComboBoxCell)dataGridViewSort.Rows[dataGridViewSort.Rows.Count - 1].Cells[1];
            rowView = (DataRowView)comboCell.Items[0];
            comboCell.Value = rowView[0];
            
        }

        private void buttonDeleteRow_Click(object sender, EventArgs e)
        {
            dataGridViewSort.Rows.Remove(dataGridViewSort.SelectedRows[0]);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            sortTbl.Rows.Clear();
            sortStr = "";
            for (int i = 0; i < dataGridViewSort.Rows.Count; i++)
            {
                DataRow row = sortTbl.NewRow();
                row[0] = dataGridViewSort.Rows[i].Cells[0].Value;
                row[1] = dataGridViewSort.Rows[i].Cells[1].Value;
                sortTbl.Rows.Add(row);
                if (i == 0)
                {
                    sortStr = " ORDER BY";
                }

                sortStr = sortStr + " " + dataGridViewSort.Rows[i].Cells[0].Value + " " + dataGridViewSort.Rows[i].Cells[1].Value + ",";
            }
            if (sortStr.Length != 0)
            {
                sortStr = sortStr.Substring(0, sortStr.Length - 1);
            }
            this.Close();

        }
        public static string SortString(DataTable SortTable)
        {
            string sortStr;
            if (SortTable != null)
            {
                sortStr = " ORDER BY";
                for (int h = 0; h < SortTable.Rows.Count; h++)
                {
                    sortStr = sortStr + " " + SortTable.Rows[h].ItemArray[0] + " " + SortTable.Rows[h].ItemArray[1] + ",";
                }
                sortStr = sortStr.Substring(0, sortStr.Length - 1);
                
            }
            else
            {
                sortStr = "";
            }
            return sortStr;
        }
    }
}
