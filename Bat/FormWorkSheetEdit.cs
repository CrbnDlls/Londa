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
    public partial class FormWorkSheetEdit : Form
    {
        string[] ColorMessages, strListBoxItems;
        int switch1;
        DataTable OldWorkSheeT;
        OracleConnection conn;
        OracleCommand Updcmd;
        bool refresh;
        string TypeOfForm;
        public FormWorkSheetEdit(OracleConnection baseConn, string[] Messages, DataGridViewRow WorkSheet, string FormType)
        {
            InitializeComponent();
            refresh = false;
            dataGridView1.Rows.Add(21);
            ColorMessages = Messages;
            conn = baseConn;
            TypeOfForm = FormType;
            
            OracleCommand cmd = new OracleCommand("SELECT s.FNAME, s.FSTNAME, s.FATHNAME, s.PRENAME, s.IDN, l.SNAME, LONDA.workingornot(s.DROPDATE) FROM LONDA.STAFF s, LONDA.PROFESSIONS p, LONDA.SALONS l WHERE s.IDN <> 0 AND s.PROF = p.IDN AND p.ALWRKSH = 0 AND s.MAINSALON = l.IDN", conn);
            DataTable Data = Functions.GetData(cmd);
            if (Data != null)
            {
                DataTable workers_tbl = new DataTable("Workers");
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
            DataTable salons_tbl = Functions.GetData(cmd);
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

            cmd.CommandText = "SELECT IDN, MNAME FROM LONDA.WMISTAKES";
            DataTable mistakes_tbl = Functions.GetData(cmd);
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
            
            switch (TypeOfForm)
            {
                case "WorkSheetList":
                    cmd.CommandText = "SELECT s.SNAME, w.SHEETNUM, to_char(w.DAT,'ddmmyyyy'), f.FNAME || ' ' || f.FSTNAME || ' ' || f.FATHNAME, to_char(w.SDATE,'HH24MI'), to_char(w.FDATE,'HH24MI'), NULL, d.NUMCL, to_char(d.SDATE1,'HH24MI'), to_char(d.FDATE1,'HH24MI'), c.CODE, d.DISC, d.QUANTITY, d.PRICE, d.IDN, NULL, d.MISTAKES, d.CLIENT, d.DMASTER, d.DDATE FROM LONDA.WORKSHEETS w, LONDA.WORKSHEETSDATA d, LONDA.SALONS s, LONDA.STAFF f, LONDA.CASH_REG_CODE c WHERE s.SNAME = '" + WorkSheet.Cells[3].Value.ToString() + "' AND w.SHEETNUM = " + WorkSheet.Cells[2].Value.ToString() + " AND f.FNAME || ' ' || f.FSTNAME || ' ' || f.FATHNAME = '" + WorkSheet.Cells[5].Value.ToString() + "' AND w.DAT = to_date('" + WorkSheet.Cells[4].Value.ToString() + "','dd/mm/yyyy') AND d.WORKSHEET = w.IDN AND w.WORKER = f.IDN AND w.SALON = s.IDN AND d.SCODE = c.IDN ORDER BY d.NROW ASC";
                    break;
                case "CheckWorkSheet":
                    cmd.CommandText = "SELECT w.SALON, w.SHEETNUM, to_char(w.DAT,'ddmmyyyy'), w.WORKER, to_char(w.SDATE,'HH24MI'), to_char(w.FDATE,'HH24MI'), NULL, d.NUMCL, to_char(d.SDATE1,'HH24MI'), to_char(d.FDATE1,'HH24MI'), d.SCODE, d.DISC, d.QUANTITY, d.PRICE, d.IDN, w.SUMM, d.MISTAKES, d.CLIENT, d.DMASTER, d.DDATE FROM LONDA.WRKSHTOCHECK w, LONDA.WRKSHTOCHECKDATA d WHERE w.SALON = '" + WorkSheet.Cells[3].Value.ToString() + "' AND w.SHEETNUM = " + WorkSheet.Cells[2].Value.ToString() + " AND w.WORKER = '" + WorkSheet.Cells[5].Value.ToString() + "' AND w.DAT = to_date('" + WorkSheet.Cells[4].Value.ToString() + "','dd/mm/yyyy') AND w.IDN = d.WORKSHEET ORDER BY NROW ASC";
                    break;
            }
            OldWorkSheeT = Functions.GetData(cmd);
            
            if (OldWorkSheeT != null)
            {
                OldWorkSheeT.Columns[0].ColumnName = "SALON";     //0
                OldWorkSheeT.Columns[1].ColumnName = "SHEETNUM";  //1
                OldWorkSheeT.Columns[2].ColumnName = "DAT";       //2
                OldWorkSheeT.Columns[3].ColumnName = "WORKER";    //3
                OldWorkSheeT.Columns[4].ColumnName = "SDATE";     //4
                OldWorkSheeT.Columns[5].ColumnName = "FDATE";     //5
                OldWorkSheeT.Columns[6].ColumnName = "NROW";      //6
                OldWorkSheeT.Columns[7].ColumnName = "NUMCL";     //7
                OldWorkSheeT.Columns[8].ColumnName = "SDATE1";    //8
                OldWorkSheeT.Columns[9].ColumnName = "FDATE1";    //9
                OldWorkSheeT.Columns[10].ColumnName = "SCODE";     //10
                OldWorkSheeT.Columns[11].ColumnName = "DISCOUNT";  //11
                OldWorkSheeT.Columns[12].ColumnName = "QUANTITY";  //12
                OldWorkSheeT.Columns[13].ColumnName = "PRICE";     //13
                OldWorkSheeT.Columns[14].ColumnName = "IDN";       //14
                OldWorkSheeT.Columns[15].ColumnName = "SUMM";      //15
                OldWorkSheeT.Columns[16].ColumnName = "MISTAKES";  //16
                OldWorkSheeT.Columns[17].ColumnName = "CLIENT";    //17
                OldWorkSheeT.Columns[18].ColumnName = "DMASTER";   //18
                OldWorkSheeT.Columns[19].ColumnName = "DDATE";     //19
                
                for (int i = 0; i< OldWorkSheeT.Rows.Count;i++)
                {
                    if (i == 0)
                    {
                        comboBoxSalon.Text = OldWorkSheeT.Rows[i].ItemArray[0].ToString();
                        textBoxNum.Text = OldWorkSheeT.Rows[i].ItemArray[1].ToString();
                        if (OldWorkSheeT.Rows[i].ItemArray[2] != DBNull.Value)
                        {
                            textBoxDay.Text = OldWorkSheeT.Rows[i].ItemArray[2].ToString().Substring(0, 2);
                            textBoxMonth.Text = OldWorkSheeT.Rows[i].ItemArray[2].ToString().Substring(2, 2);
                            textBoxYear.Text = OldWorkSheeT.Rows[i].ItemArray[2].ToString().Substring(4, 4);
                        }
                        comboBoxWorker.Text = OldWorkSheeT.Rows[i].ItemArray[3].ToString();
                        if (OldWorkSheeT.Rows[i].ItemArray[4] != DBNull.Value)
                        {
                            textBoxHourS.Text = OldWorkSheeT.Rows[i].ItemArray[4].ToString().Substring(0, 2);
                            textBoxMinuteS.Text = OldWorkSheeT.Rows[i].ItemArray[4].ToString().Substring(2, 2);
                        }
                        if (OldWorkSheeT.Rows[i].ItemArray[5] != DBNull.Value)
                        {
                            textBoxHourE.Text = OldWorkSheeT.Rows[i].ItemArray[5].ToString().Substring(0, 2);
                            textBoxMinuteE.Text = OldWorkSheeT.Rows[i].ItemArray[5].ToString().Substring(2, 2);
                        }
                        if (TypeOfForm == "CheckWorkSheet")
                        {
                            textBoxSumm.Text = OldWorkSheeT.Rows[i].ItemArray[15].ToString(); 
                        }
                    }
                    
                    dataGridView1.Rows[i].Cells["ColumnIDN"].Value = OldWorkSheeT.Rows[i].ItemArray[14];
                    dataGridView1.Rows[i].Cells["ClientNum"].Value = OldWorkSheeT.Rows[i].ItemArray[7];
                    if (OldWorkSheeT.Rows[i].ItemArray[8] != DBNull.Value)
                    {
                        dataGridView1.Rows[i].Cells["SvHourS"].Value = OldWorkSheeT.Rows[i].ItemArray[8].ToString().Substring(0, 2);
                        dataGridView1.Rows[i].Cells["SvMinuteS"].Value = OldWorkSheeT.Rows[i].ItemArray[8].ToString().Substring(2, 2);
                    }
                    if (OldWorkSheeT.Rows[i].ItemArray[9] != DBNull.Value)
                    {
                        dataGridView1.Rows[i].Cells["SvHourE"].Value = OldWorkSheeT.Rows[i].ItemArray[9].ToString().Substring(0, 2);
                        dataGridView1.Rows[i].Cells["SvMinuteE"].Value = OldWorkSheeT.Rows[i].ItemArray[9].ToString().Substring(2, 2);
                    }
                    dataGridView1.Rows[i].Cells["SVCode"].Value = OldWorkSheeT.Rows[i].ItemArray[10];
                    dataGridView1.Rows[i].Cells["Discount"].Value = OldWorkSheeT.Rows[i].ItemArray[11];
                    dataGridView1.Rows[i].Cells["Quantity"].Value = OldWorkSheeT.Rows[i].ItemArray[12];
                    dataGridView1.Rows[i].Cells["Summ"].Value = OldWorkSheeT.Rows[i].ItemArray[13];
                    dataGridView1.Rows[i].Cells["Mistakes"].Value = OldWorkSheeT.Rows[i].ItemArray[16].ToString();
                    dataGridView1.Rows[i].Cells["Client"].Value = OldWorkSheeT.Rows[i].ItemArray[17];
                    dataGridView1.Rows[i].Cells["Master"].Value = OldWorkSheeT.Rows[i].ItemArray[18];
                    dataGridView1.Rows[i].Cells["DDATE"].Value = OldWorkSheeT.Rows[i].ItemArray[19];
                    switch1 = 0;
                }
            }
            
            
            decimal result, summ = 0;
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                if (dataGridView1.Rows[j].Cells["Summ"].Value != null & int.Parse(dataGridView1.Rows[j].Cells["Mistakes"].Value.ToString()) != 1 && decimal.TryParse(dataGridView1.Rows[j].Cells["Summ"].Value.ToString(), out result))
                {
                    summ = summ + result;
                }
            }
            labelSumm.Text = summ.ToString() + " грн.";
            
            if (TypeOfForm == "WorkSheetList")
            {
                textBoxSumm.Text = summ.ToString();
                for (int u = 0; u < OldWorkSheeT.Rows.Count; u++)
                {
                    DataRow row = OldWorkSheeT.Rows[u];
                    row[15] = summ;
                }
            }
            OracleParameter par;
            Updcmd = new OracleCommand();
            Updcmd.CommandType = CommandType.StoredProcedure;
            Updcmd.Connection = conn;
            switch (TypeOfForm)
            {
                case "WorkSheetList":
                    Updcmd.CommandText = "LONDA.UPDWORKSHEET";
                    break;
                case "CheckWorkSheet":
                    Updcmd.CommandText = "LONDA.UPDWRKSH";
                    break;
            }
            
            string[] parameters = { "IN_IDN", //0
                                            "IN_SALON", //1
                                            "IN_SHEETNUM", //2
                                            "IN_DAT", //3
                                            "IN_WORKER", //4
                                            "IN_SDATE", //5
                                            "IN_FDATE",//6
                                            "IN_NROW",//7
                                       "IN_NUMCL",//8
                                       "IN_SDATE1",//9
                                       "IN_FDATE1",//10
                                       "IN_SCODE",//11
                                       "IN_DISC",//12
                                       "IN_QUANTITY",//13
                                       "IN_PRICE", //14
                                  "IN_SUMM",        //15
                                  "IN_MISTAKES",    //16
                                       "IN_CLIENT", //17
                                       "IN_DMASTER", //18
                                       "IN_DDATE"};  //19
            foreach (string line in parameters)
            {
                par = Updcmd.CreateParameter();
                par.ParameterName = line;
                par.OracleType = OracleType.VarChar;
                par.Size = 0;
                par.Direction = ParameterDirection.Input;
                Updcmd.Parameters.Add(par);
            }
            par = Updcmd.CreateParameter();
            par.ParameterName = "ERRSHEET";
            par.OracleType = OracleType.NVarChar;
            par.Size = 4000;
            par.Direction = ParameterDirection.Output;
            Updcmd.Parameters.Add(par);
        }

        private void listBoxMessage_Layout(object sender, LayoutEventArgs e)
        {
            ListBoxPaint();
            switch1 = 1;
        }

        private void ListBoxPaint()
        {
            if (ColorMessages != null & switch1 == 1)
            {
                switch (ColorMessages[0])
                {
                    case "1":
                        listBoxMessage.BackColor = Color.Red;
                        break;
                    case "2":
                        listBoxMessage.BackColor = Color.Yellow;
                        break;
                    case "0":
                        listBoxMessage.BackColor = Color.White;
                        break;
                }
                strListBoxItems = new string[0];
                listBoxMessage.Items.Clear();
                for (int h = 1; h < ColorMessages.Count(); h++)
                {
                    string line = h + ". " + ColorMessages[h];
                    Graphics gr = listBoxMessage.CreateGraphics();
                    SizeF lineWdth = gr.MeasureString(line, listBoxMessage.Font);

                    if (lineWdth.Width > listBoxMessage.Width - 10)
                    {

                        int xvb = (int)(lineWdth.Width / (listBoxMessage.Width - 10)) + 1;
                        string[] tmpLine = new string[xvb];
                        int start = 0;
                        int stop = (int)(line.Length / lineWdth.Width * (listBoxMessage.Width - 10));
                        for (int d = 0; d < xvb - 1; d++)
                        {
                            if (line.Substring(start, stop).LastIndexOf(" ") != -1)
                            {
                                stop = line.Substring(start, stop).LastIndexOf(" ");
                            }
                            tmpLine[d] = line.Substring(start, stop);
                            start = start + stop;
                        }
                        tmpLine[tmpLine.Count() - 1] = line.Substring(start);
                        strListBoxItems = strListBoxItems.Concat(tmpLine).ToArray();
                    }
                    else
                    {
                        string[] tmpLine = new string[1];
                        tmpLine[0] = line;
                        strListBoxItems = strListBoxItems.Concat(tmpLine).ToArray();
                    }
                }
                listBoxMessage.Items.AddRange(strListBoxItems);
            } 
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


            if (e.ColumnIndex == dataGridView1.Columns["SvHourS"].Index | e.ColumnIndex == dataGridView1.Columns["SvHourE"].Index | e.ColumnIndex == dataGridView1.Columns["SvMinuteS"].Index | e.ColumnIndex == dataGridView1.Columns["SvMinuteS"].Index)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length == 1)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0" + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    }
                }
            }
            if (e.ColumnIndex == dataGridView1.Columns["Summ"].Index | e.ColumnIndex == dataGridView1.Columns["Mistakes"].Index)
            {
                
                    decimal result, summ = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells["Summ"].Value != null)
                        {
                            dataGridView1.Rows[i].Cells["Summ"].Value = dataGridView1.Rows[i].Cells["Summ"].Value.ToString().Replace(".", ",");
                            if (dataGridView1.Rows[i].Cells["Summ"].Value != null & int.Parse(dataGridView1.Rows[i].Cells["Mistakes"].Value.ToString()) != 1 && decimal.TryParse(dataGridView1.Rows[i].Cells["Summ"].Value.ToString(), out result))
                            {
                                summ = summ + result;
                            }
                        }
                    }
                    labelSumm.Text = summ.ToString() + " грн.";
                    if (TypeOfForm == "WorkSheetList")
                    {
                        textBoxSumm.Text = summ.ToString();
                    }
                
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DataTable NewWorkSheet = new DataTable();
            NewWorkSheet.Columns.Add("SALON");     //0
            NewWorkSheet.Columns.Add("SHEETNUM");  //1
            NewWorkSheet.Columns.Add("DAT");       //2
            NewWorkSheet.Columns.Add("WORKER");    //3
            NewWorkSheet.Columns.Add("SDATE");     //4
            NewWorkSheet.Columns.Add("FDATE");     //5
            NewWorkSheet.Columns.Add("NROW");      //6
            NewWorkSheet.Columns.Add("NUMCL");     //7
            NewWorkSheet.Columns.Add("SDATE1");    //8
            NewWorkSheet.Columns.Add("FDATE1");    //9
            NewWorkSheet.Columns.Add("SCODE");     //10
            NewWorkSheet.Columns.Add("DISCOUNT");  //11
            NewWorkSheet.Columns.Add("QUANTITY");  //12
            NewWorkSheet.Columns.Add("PRICE");     //13
            NewWorkSheet.Columns.Add("IDN");       //14
            NewWorkSheet.Columns.Add("SUMM");      //15
            NewWorkSheet.Columns.Add("MISTAKES");  //16
            NewWorkSheet.Columns.Add("CLIENT");    //17
            NewWorkSheet.Columns.Add("DMASTER");   //18
            NewWorkSheet.Columns.Add("DDATE");     //19


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["ColumnIDN"].Value != null | dataGridView1.Rows[i].Cells["ClientNum"].Value != null | dataGridView1.Rows[i].Cells["SvHourS"].Value != null | dataGridView1.Rows[i].Cells["SvMinuteS"].Value != null | dataGridView1.Rows[i].Cells["SvHourE"].Value != null | dataGridView1.Rows[i].Cells["SvMinuteE"].Value != null | dataGridView1.Rows[i].Cells["SVCode"].Value != null | dataGridView1.Rows[i].Cells["Discount"].Value != null | dataGridView1.Rows[i].Cells["Quantity"].Value != null | dataGridView1.Rows[i].Cells["Summ"].Value != null)
                {
                    DataRow row = NewWorkSheet.NewRow();
                    row[0] = comboBoxSalon.Text;
                    row[1] = textBoxNum.Text;
                    row[2] = textBoxDay.Text + textBoxMonth.Text + textBoxYear.Text;
                    row[3] = comboBoxWorker.Text;
                    row[4] = textBoxHourS.Text + textBoxMinuteS.Text;
                    row[5] = textBoxHourE.Text + textBoxMinuteE.Text;
                    row[15] = textBoxSumm.Text;
                    row[7] = dataGridView1.Rows[i].Cells["ClientNum"].Value;
                    if (dataGridView1.Rows[i].Cells["SvHourS"].Value != null & dataGridView1.Rows[i].Cells["SvMinuteS"].Value != null)
                    {
                        row[8] = dataGridView1.Rows[i].Cells["SvHourS"].Value.ToString() + dataGridView1.Rows[i].Cells["SvMinuteS"].Value.ToString();
                    }

                    if (dataGridView1.Rows[i].Cells["SvHourE"].Value != null & dataGridView1.Rows[i].Cells["SvMinuteE"].Value != null)
                    {
                        row[9] = dataGridView1.Rows[i].Cells["SvHourE"].Value.ToString() + dataGridView1.Rows[i].Cells["SvMinuteE"].Value.ToString();
                    }
                    row[10] = dataGridView1.Rows[i].Cells["SVCode"].Value;
                    row[11] = dataGridView1.Rows[i].Cells["Discount"].Value;
                    row[12] = dataGridView1.Rows[i].Cells["Quantity"].Value;
                    row[13] = dataGridView1.Rows[i].Cells["Summ"].Value;
                    row[14] = dataGridView1.Rows[i].Cells["ColumnIDN"].Value;
                    row[16] = dataGridView1.Rows[i].Cells["Mistakes"].Value;
                    row[17] = dataGridView1.Rows[i].Cells["Client"].Value;
                    row[18] = dataGridView1.Rows[i].Cells["Master"].Value;
                    row[19] = dataGridView1.Rows[i].Cells["DDATE"].Value;
                    NewWorkSheet.Rows.Add(row);
                }
            }

            ColorMessages = CheckWorkSheet(NewWorkSheet, conn);
            ListBoxPaint();
            if (ColorMessages[0] != "1")
            {
                if (NotEqualWorkSheets(NewWorkSheet, OldWorkSheeT))
                {
                    SaveWorkSheet(NewWorkSheet);
                }
            }
            else
            {
                MessageBox.Show("Невозможно сохранить красный рабочий лист", "Ошибка");
            }

        }

        private bool NotEqualWorkSheets(DataTable WorkSheetNew, DataTable WorkSheetOld)
        {
            
            if (WorkSheetNew.Rows.Count != WorkSheetOld.Rows.Count)
            {
                return true;
            }
            for (int i = 0; i < WorkSheetNew.Rows.Count; i++)
            {
                if (WorkSheetNew.Rows[i].ItemArray[0].ToString() != WorkSheetOld.Rows[i].ItemArray[0].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[1].ToString() != WorkSheetOld.Rows[i].ItemArray[1].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[2].ToString() != WorkSheetOld.Rows[i].ItemArray[2].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[3].ToString() != WorkSheetOld.Rows[i].ItemArray[3].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[4].ToString() != WorkSheetOld.Rows[i].ItemArray[4].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[5].ToString() != WorkSheetOld.Rows[i].ItemArray[5].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[6].ToString() != WorkSheetOld.Rows[i].ItemArray[6].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[7].ToString() != WorkSheetOld.Rows[i].ItemArray[7].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[8].ToString() != WorkSheetOld.Rows[i].ItemArray[8].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[9].ToString() != WorkSheetOld.Rows[i].ItemArray[9].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[10].ToString() != WorkSheetOld.Rows[i].ItemArray[10].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[11].ToString() != WorkSheetOld.Rows[i].ItemArray[11].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[12].ToString() != WorkSheetOld.Rows[i].ItemArray[12].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[13].ToString() != WorkSheetOld.Rows[i].ItemArray[13].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[14].ToString() != WorkSheetOld.Rows[i].ItemArray[14].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[15].ToString() != WorkSheetOld.Rows[i].ItemArray[15].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[16].ToString() != WorkSheetOld.Rows[i].ItemArray[16].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[17].ToString() != WorkSheetOld.Rows[i].ItemArray[17].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[18].ToString() != WorkSheetOld.Rows[i].ItemArray[18].ToString())
                {
                    return true;
                }
                if (WorkSheetNew.Rows[i].ItemArray[19].ToString() != WorkSheetOld.Rows[i].ItemArray[19].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private void SaveWorkSheet(DataTable WorkSheet)
        {
            int nrow = 0;
            bool error = false;

            for (int i = 0; i < WorkSheet.Rows.Count; i++)
            {
                if (WorkSheet.Rows[i].ItemArray[7].ToString() != "" | WorkSheet.Rows[i].ItemArray[8].ToString() != "" | WorkSheet.Rows[i].ItemArray[9].ToString() != "" | WorkSheet.Rows[i].ItemArray[10].ToString() != "" | WorkSheet.Rows[i].ItemArray[13].ToString() != "")
                {
                    if (WorkSheet.Rows[i].ItemArray[14].ToString() != "")
                    {
                        Updcmd.Parameters[0].Value = WorkSheet.Rows[i].ItemArray[14].ToString();
                    }
                    else
                    {
                        Updcmd.Parameters[0].Value = "0";
                    }
                    Updcmd.Parameters[1].Value = WorkSheet.Rows[i].ItemArray[0].ToString();
                    Updcmd.Parameters[2].Value = WorkSheet.Rows[i].ItemArray[1].ToString();
                    Updcmd.Parameters[3].Value = WorkSheet.Rows[i].ItemArray[2].ToString();
                    Updcmd.Parameters[4].Value = WorkSheet.Rows[i].ItemArray[3].ToString();
                    Updcmd.Parameters[5].Value = WorkSheet.Rows[i].ItemArray[4].ToString();
                    Updcmd.Parameters[6].Value = WorkSheet.Rows[i].ItemArray[5].ToString();
                    nrow = nrow + 1;
                    Updcmd.Parameters[7].Value = nrow.ToString();

                    Updcmd.Parameters[8].Value = WorkSheet.Rows[i].ItemArray[7].ToString();
                    Updcmd.Parameters[9].Value = WorkSheet.Rows[i].ItemArray[8].ToString();
                    Updcmd.Parameters[10].Value = WorkSheet.Rows[i].ItemArray[9].ToString();
                    Updcmd.Parameters[11].Value = WorkSheet.Rows[i].ItemArray[10].ToString();
                    Updcmd.Parameters[12].Value = WorkSheet.Rows[i].ItemArray[11].ToString();
                    Updcmd.Parameters[13].Value = WorkSheet.Rows[i].ItemArray[12].ToString();
                    Updcmd.Parameters[14].Value = WorkSheet.Rows[i].ItemArray[13].ToString();
                    Updcmd.Parameters[15].Value = WorkSheet.Rows[i].ItemArray[15].ToString();
                    Updcmd.Parameters[16].Value = WorkSheet.Rows[i].ItemArray[16].ToString();
                    Updcmd.Parameters[17].Value = WorkSheet.Rows[i].ItemArray[17].ToString();
                    Updcmd.Parameters[18].Value = WorkSheet.Rows[i].ItemArray[18].ToString();
                    Updcmd.Parameters[19].Value = WorkSheet.Rows[i].ItemArray[19].ToString();

                    if (!Functions.ExecuteNonQuery(Updcmd))
                    {
                        error = true;
                        break;
                    }
                    
                }
                else
                {
                    if (WorkSheet.Rows[i].ItemArray[14].ToString() != "")
                    {
                        OracleCommand delCommand = new OracleCommand("DELETE FROM LONDA.WRKSHTOCHECK WHERE IDN = " + WorkSheet.Rows[i].ItemArray[14].ToString(), conn);
                        if (!Functions.ExecuteNonQuery(delCommand))
                        {
                            error = true;
                            break;
                        }
                    }
                }
            }

            if (error)
            {
                OracleCommand cmd2 = new OracleCommand("ROLLBACK", conn);
                if (!Functions.ExecuteNonQuery(cmd2))
                {
                    MessageBox.Show("Неудалось совершить откат", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else
            {
                OracleCommand cmd2 = new OracleCommand("COMMIT", conn);
                if (Functions.ExecuteNonQuery(cmd2))
                {
                    MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK);
                }
                refresh = true;

            }
        }

        public static string[] CheckWorkSheet(DataTable WorkSheet, OracleConnection OraConn)
        {
            string[] error = { "0" };
            string[] tmp = new string[1];
            int x = 0;
            Int32 result = 0, result1 = 0;
            bool RED = false, Yellow = false;
            decimal summlist = 0;
            DataTable Data;
            if (WorkSheet.Rows.Count > 0)
            {
                //Проверка названия салона
                OracleCommand cmd = new OracleCommand("SELECT IDN FROM LONDA.SALONS WHERE SNAME = '" + WorkSheet.Rows[0].ItemArray[0].ToString() + "'", OraConn);
                Data = Functions.GetData(cmd);
                if (Data != null)
                {
                    if (Data.Rows.Count != 1)
                    {
                        RED = true;
                        tmp[0] = "Название салона заполнено не верно";
                        error = error.Concat(tmp).ToArray();
                    }
                    Data.Dispose();
                }
                else
                {
                    RED = true;
                    tmp[0] = "Ошибка. Не проверенно название салона";
                    error = error.Concat(tmp).ToArray();
                }
                //Проверка номера листа
                if (!Int32.TryParse(WorkSheet.Rows[0].ItemArray[1].ToString(), out result))
                {
                    RED = true;
                    tmp[0] = "Номер листа заполнен не верно";
                    error = error.Concat(tmp).ToArray();
                }
                /// Проверка даты
                Int32 month, year, day;

                if ((!Int32.TryParse(WorkSheet.Rows[0].ItemArray[2].ToString().Substring(2, 2), out month) || month > 12) | !Int32.TryParse(WorkSheet.Rows[0].ItemArray[2].ToString().Substring(4, 4), out year) | !Int32.TryParse(WorkSheet.Rows[0].ItemArray[2].ToString().Substring(0, 2), out day))
                {
                    RED = true;
                    tmp[0] = "Дата заполнена не верно";
                    error = error.Concat(tmp).ToArray();
                }
                else
                {
                    if (day > DateTime.DaysInMonth(year, month))
                    {
                        RED = true;
                        tmp[0] = "В " + month + " месяце " + year + " года меньше " + day + " дня(ей)";
                        error = error.Concat(tmp).ToArray();
                    }
                }
                /// Проверка имени сотрудника
                string FamilyName = "", FirstName = "", FathName = "";
                int dlina = 0;
                try
                {
                    FamilyName = WorkSheet.Rows[0].ItemArray[3].ToString().Substring(dlina, WorkSheet.Rows[0].ItemArray[3].ToString().IndexOf(" "));
                    dlina = WorkSheet.Rows[0].ItemArray[3].ToString().IndexOf(" ") + 1;
                }
                catch
                {
 
                }
                try
                {
                    FirstName = WorkSheet.Rows[0].ItemArray[3].ToString().Substring(dlina, WorkSheet.Rows[0].ItemArray[3].ToString().IndexOf(" ", dlina) - dlina);
                    dlina = WorkSheet.Rows[0].ItemArray[3].ToString().IndexOf(" ", dlina) + 1;
                }
                catch 
                {

                }
                try
                {
                    if (WorkSheet.Rows[0].ItemArray[3].ToString().IndexOf(" ", dlina) == -1)
                    {
                        FathName = WorkSheet.Rows[0].ItemArray[3].ToString().Substring(dlina);
                    }
                    else
                    {
                        FathName = WorkSheet.Rows[0].ItemArray[3].ToString().Substring(dlina, WorkSheet.Rows[0].ItemArray[3].ToString().IndexOf(" ", dlina) - dlina);
                    }
                }
                catch
                {

                }
                cmd.CommandText = "SELECT IDN FROM LONDA.STAFF WHERE FNAME = '" + FamilyName + "' AND FSTNAME = '" + FirstName + "' AND FATHNAME = '" + FathName + "'";
                Data = Functions.GetData(cmd);
                if (Data != null)
                {
                    if (Data.Rows.Count != 1)
                    {
                        RED = true;
                        tmp[0] = "Фамилия сотрудника заполнена не верно";
                        error = error.Concat(tmp).ToArray();
                    }
                }
                else
                {
                    RED = true;
                    tmp[0] = "Ошибка. Не проверенна фамилия сотрудника";
                    error = error.Concat(tmp).ToArray();
                }
                /// Проверка часов начала и конца работы мастера
                Int32 hourS, minuteS, hourE, minuteE;
                if ((!Int32.TryParse(WorkSheet.Rows[0].ItemArray[4].ToString().Substring(0, 2), out hourS) || hourS > 23) | (!Int32.TryParse(WorkSheet.Rows[0].ItemArray[5].ToString().Substring(0, 2), out hourE) || hourE > 23) | (!Int32.TryParse(WorkSheet.Rows[0].ItemArray[4].ToString().Substring(2, 2), out minuteS) || minuteS > 59) | (!Int32.TryParse(WorkSheet.Rows[0].ItemArray[5].ToString().Substring(2, 2), out minuteE) || minuteE > 59))
                {
                    RED = true;
                    tmp[0] = "Время начала или окончания робочего дня мастера введено не верно";
                    error = error.Concat(tmp).ToArray();
                }
                else
                {
                    if (hourE - hourS < 0 || (hourE - hourS == 0 & minuteE - minuteS < 0))
                    {
                        RED = true;
                        tmp[0] = "Время начала позже времени окончания робочего дня мастера";
                        error = error.Concat(tmp).ToArray();
                    }
                    else
                    {
                        if (hourS < 8)
                        {
                            Yellow = true;
                            tmp[0] = "Время начала робочего дня мастера раньше 8:00";
                            error = error.Concat(tmp).ToArray();
                        }
                        if (hourE > 22 | (hourE == 22 & minuteE > 0))
                        {
                            Yellow = true;
                            tmp[0] = "Время окончания робочего дня мастера позже 22:00";
                            error = error.Concat(tmp).ToArray();
                        }
                    }
                }
                
                /// Проверка данных таблицы

                Int32 hours = -1, minutes = -1, houre = -2;
                bool clientOk = false, empty = true;
                for (int i = 0; i < WorkSheet.Rows.Count; i++)
                {
                    clientOk = false;
                    if (!WorkSheet.Rows[i].IsNull(7) | !WorkSheet.Rows[i].IsNull(8) | !WorkSheet.Rows[i].IsNull(9) | !WorkSheet.Rows[i].IsNull(10) | !WorkSheet.Rows[i].IsNull(13))
                    {
                        empty = false;
                        //Порядковый номер
                        if (WorkSheet.Rows[i].ItemArray[7].ToString() == "" & hours == -1)
                        {
                            RED = true;
                            tmp[0] = "Не внесен номер клиента. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        if (WorkSheet.Rows[i].ItemArray[7].ToString() != "" && !Int32.TryParse(WorkSheet.Rows[i].ItemArray[7].ToString(), out result))
                        {
                            RED = true;
                            tmp[0] = "Внесен не верный номер клиента. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        else
                        {
                            if (WorkSheet.Rows[i].ItemArray[7].ToString() != "")
                            {
                                if (result - x != 1)
                                {
                                    RED = true;
                                    tmp[0] = "Внесен не верный номер клиента. Строка №" + (i + 1);
                                    error = error.Concat(tmp).ToArray();
                                }
                                else
                                {
                                    clientOk = true;
                                }
                                x = result;

                            }

                        }

                        //Время начала процедуры часы
                        if (WorkSheet.Rows[i].ItemArray[8].ToString() == "" & clientOk)
                        {
                            RED = true;
                            tmp[0] = "Не внесен час начала процедуры. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                            hours = -2;
                        }
                        if (WorkSheet.Rows[i].ItemArray[8].ToString() != "" && (!Int32.TryParse(WorkSheet.Rows[i].ItemArray[8].ToString().Substring(0, 2), out result) || result > 23))
                        {
                            RED = true;
                            tmp[0] = "Внесен не верный час начала процедуры. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        else
                        {
                            if (WorkSheet.Rows[i].ItemArray[8].ToString() != "")
                            {
                                if (result - hourS < 0)
                                {
                                    RED = true;
                                    tmp[0] = "Час начала процедуры раньше часа начала работы. Строка №" + (i + 1);
                                    error = error.Concat(tmp).ToArray();
                                }
                                else
                                {
                                    if (houre == -1)
                                    {
                                        RED = true;
                                        tmp[0] = "Не заполнено время конца процедуры. Строка №" + (i);
                                        error = error.Concat(tmp).ToArray();
                                    }
                                    houre = -1;
                                    hours = result;
                                }

                            }
                        }
                        /*if (dataGridView1.Rows[i].Cells["SvHourS"].Value == null & dataGridView1.Rows[i].Cells["SvMinuteS"].Value != null)
                        {
                            error[0] = "Незначительная Ошибка";
                            tmp[0] = "Внесены минуты, но не внесены часы начала процедуры. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }*/

                        //Время начала процедуры минуты
                        if (WorkSheet.Rows[i].ItemArray[8].ToString() != "" && (!Int32.TryParse(WorkSheet.Rows[i].ItemArray[8].ToString().Substring(2, 2), out result1) || result1 > 59))
                        {
                            RED = true;
                            tmp[0] = "Внесены не верные минуты начала процедуры. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        else
                        {
                            if (WorkSheet.Rows[i].ItemArray[8].ToString() != "")
                            {
                                if (result - hourS == 0 & result1 - minuteS < 0)
                                {
                                    RED = true;
                                    tmp[0] = "Минуты начала процедуры раньше минут начала работы. Строка №" + (i + 1);
                                    error = error.Concat(tmp).ToArray();
                                }
                                else
                                {
                                    minutes = result1;
                                }

                            }
                        }

                        //Время конца процедуры часы
                        if (WorkSheet.Rows[i].ItemArray[9].ToString() != "" && (!Int32.TryParse(WorkSheet.Rows[i].ItemArray[9].ToString().Substring(0, 2), out result) || result > 23))
                        {
                            RED = true;
                            tmp[0] = "Внесен не верный час окончания процедуры. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        else
                        {
                            if (WorkSheet.Rows[i].ItemArray[9].ToString() != "")
                            {
                                if (hourE - result < 0)
                                {
                                    RED = true;
                                    tmp[0] = "Час окончания процедуры позже часа окончания работы. Строка №" + (i + 1);
                                    error = error.Concat(tmp).ToArray();
                                    houre = -2;
                                }
                                else
                                {
                                    if (hours == -1)
                                    {
                                        RED = true;
                                        tmp[0] = "Не заполнено время начала процедуры. Строка №" + (i + 1);
                                        error = error.Concat(tmp).ToArray();
                                    }
                                    if (hours != -1 && result - hours < 0)
                                    {
                                        RED = true;
                                        tmp[0] = "Час окончания процедуры раньше часа начала. Строка №" + (i + 1);
                                        error = error.Concat(tmp).ToArray();
                                        houre = -2;
                                    }
                                    else
                                    {
                                        houre = result;
                                    }

                                }

                            }
                        }
                        /*if (dataGridView1.Rows[i].Cells["SvHourE"].Value == null & dataGridView1.Rows[i].Cells["SvMinuteE"].Value != null)
                        {
                            error[0] = "Незначительная Ошибка";
                            tmp[0] = "Внесены минуты, но не внесены часы окончания процедуры. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }*/
                        if (i == 20 & houre == -1)
                        {
                            RED = true;
                            tmp[0] = "Не внесены часы окончания процедуры. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }

                        //Время конца процедуры минуты
                        if (WorkSheet.Rows[i].ItemArray[9].ToString() != "" && (!Int32.TryParse(WorkSheet.Rows[i].ItemArray[9].ToString().Substring(2, 2), out result1) || result1 > 59))
                        {
                            RED = true;
                            tmp[0] = "Внесены не верные минуты окончания процедуры. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        else
                        {
                            if (WorkSheet.Rows[i].ItemArray[9].ToString() != "")
                            {
                                if (hourE - result == 0 & minuteE - result1 < 0)
                                {
                                    RED = true;
                                    tmp[0] = "Минуты окончания процедуры позже минут окончания работы. Строка №" + (i + 1);
                                    error = error.Concat(tmp).ToArray();
                                }
                                if (houre - hours == 0 & result1 - minutes < 0)
                                {
                                    RED = true;
                                    tmp[0] = "Минуты окончания процедуры раньше минут начала процедуры. Строка №" + (i + 1);
                                    error = error.Concat(tmp).ToArray();
                                }
                                hours = -1;
                                
                            }
                        }
                        //Код услуги !!!!!!!!!!!!!!!!!!!!!
                        if (WorkSheet.Rows[i].ItemArray[10].ToString() == "")
                        {
                            RED = true;
                            tmp[0] = "Не заполнен код услуги. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        else
                        {
                            if (!Int32.TryParse(WorkSheet.Rows[i].ItemArray[10].ToString(), out result))
                            {
                                RED = true;
                                tmp[0] = "Не верно заполнен код услуги. Строка №" + (i + 1);
                                error = error.Concat(tmp).ToArray();
                            }
                            else
                            {
                                cmd.CommandText = "SELECT IDN FROM LONDA.CASH_REG_CODE WHERE CODE = " + WorkSheet.Rows[i].ItemArray[10].ToString();
                                Data = Functions.GetData(cmd);
                                if (Data != null)
                                {
                                    if (Data.Rows.Count != 1)
                                    {
                                        RED = true;
                                        tmp[0] = "Кода услуги " + WorkSheet.Rows[i].ItemArray[10].ToString() + " в базе не существует. Строка №" + (i + 1);
                                        error = error.Concat(tmp).ToArray();
                                    }
                                }
                                else
                                {
                                    RED = true;
                                    tmp[0] = "Нет соединения с базой. Не проверенн код услуги";
                                    error = error.Concat(tmp).ToArray();
                                }
                            }

                        }
                        // Скидка
                        int discount = 0;
                        if (WorkSheet.Rows[i].ItemArray[11].ToString() != "" && !int.TryParse(WorkSheet.Rows[i].ItemArray[11].ToString(), out discount))
                        {
                            RED = true;
                            tmp[0] = "Не верно внесена скидка. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        else
                        {
                            if (discount < 0 | discount > 100)
                            {
                                RED = true;
                                tmp[0] = "Скидка должна быть в диапазоне от 0 до 100. Строка №" + (i + 1);
                                error = error.Concat(tmp).ToArray();
                            }
                            else
                            {
                                if (discount != 0 & discount != 10 & discount != 50)
                                {
                                    Yellow = true;
                                    tmp[0] = "Не стандартная скидка " + discount + "%. Строка №" + (i + 1);
                                    error = error.Concat(tmp).ToArray();
                                }
                            }
                        }
                        // Количество
                        int quantity = 1;
                        if (WorkSheet.Rows[i].ItemArray[12].ToString() != "" && !int.TryParse(WorkSheet.Rows[i].ItemArray[12].ToString(), out quantity))
                        {
                            RED = true;
                            tmp[0] = "Не верно внесено количество. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        else
                        {
                            if (quantity <= 0)
                            {
                                RED = true;
                                tmp[0] = "Количество должно быть больше 0. Строка №" + (i + 1);
                                error = error.Concat(tmp).ToArray();
                            }
                            else
                            {
                                if (quantity > 1)
                                {
                                    Yellow = true;
                                    tmp[0] = "Большое количество. Строка №" + (i + 1);
                                    error = error.Concat(tmp).ToArray();
                                }
                            }
                        }
                        //Стоимость
                        decimal result2;
                        if (WorkSheet.Rows[i].ItemArray[13].ToString() == "")
                        {
                            RED = true;
                            tmp[0] = "Не внесена стоимость услуги. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }
                        else
                        {
                            if (!decimal.TryParse(WorkSheet.Rows[i].ItemArray[13].ToString(), out result2))
                            {
                                RED = true;
                                tmp[0] = "Не верно внесена стоимость услуги. Строка №" + (i + 1);
                                error = error.Concat(tmp).ToArray();
                            }
                            else
                            {
                                if (WorkSheet.Rows[i].ItemArray[16].ToString() != "1")
                                {
                                    summlist = summlist + result2;
                                }
                                cmd.CommandText = "SELECT PRICE FROM LONDA.CASH_REG_CODE WHERE CODE = " + WorkSheet.Rows[i].ItemArray[10].ToString();
                                Data = Functions.GetData(cmd);
                                if (Data != null)
                                {
                                    if (Data.Rows.Count == 1)
                                    {
                                        decimal dbprice = decimal.Parse(Data.Rows[0].ItemArray[0].ToString());
                                        
                                        if (discount <= 100 & discount >= 0 & quantity >= 1)
                                        {
                                            decimal CorrectPrice = (dbprice - (dbprice * discount / 100)) * quantity;
                                            if (CorrectPrice != result2)
                                            {
                                                if (result2 == 0)
                                                {
                                                    Yellow = true;
                                                    tmp[0] = "БЕЗ ОПЛАТЫ. Коду услуги " + WorkSheet.Rows[i].ItemArray[10].ToString() + " со скидкой в " + WorkSheet.Rows[i].ItemArray[11].ToString() + "% соотвествует цена " + CorrectPrice + ". Строка №" + (i + 1);
                                                    error = error.Concat(tmp).ToArray();
                                                }
                                                else
                                                {
                                                    Yellow = true;
                                                    tmp[0] = "Коду услуги " + WorkSheet.Rows[i].ItemArray[10].ToString() + " со скидкой в " + WorkSheet.Rows[i].ItemArray[11].ToString() + "% соотвествует цена " + CorrectPrice + ". Строка №" + (i + 1);
                                                    error = error.Concat(tmp).ToArray();
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Yellow = true;
                                    tmp[0] = "Ошибка. Не проверенна стоимость услуги";
                                    error = error.Concat(tmp).ToArray();
                                }
                            }

                        }

                        // Исправления в рабочем листе
                        if (WorkSheet.Rows[i].ItemArray[16].ToString() == "1")
                        {
                            Yellow = true;
                            tmp[0] = "Зачеркнута сумма. Строка №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }

                        if (WorkSheet.Rows[i].ItemArray[16].ToString() == "2")
                        {
                            Yellow = true;
                            tmp[0] = "Исправление в строке №" + (i + 1);
                            error = error.Concat(tmp).ToArray();
                        }

                    }
                    else
                    {
                        
                        
                        if (houre == -1)
                        {
                            RED = true;
                            tmp[0] = "Не внесены часы окончания процедуры. Строка №" + (i);
                            error = error.Concat(tmp).ToArray();
                            houre = -2;
                        }
                    }

                }

                if (empty)
                {
                    Yellow = true;
                    tmp[0] = "Пустой рабочий лист";
                    error = error.Concat(tmp).ToArray();
                }
                // Общая сумма
                decimal summ;
                if (WorkSheet.Rows[0].ItemArray[15].ToString() == "")
                {
                    RED = true;
                    tmp[0] = "Не внесена общая сумма.";
                    error = error.Concat(tmp).ToArray();
                }
                else
                {
                    if (!decimal.TryParse(WorkSheet.Rows[0].ItemArray[15].ToString(), out summ))
                    {
                        RED = true;
                        tmp[0] = "Не верно внесена общая сумма.";
                        error = error.Concat(tmp).ToArray();
                    }
                    else
                    {
                        if (summ != summlist)
                        {
                            RED = true;
                            tmp[0] = "Общая сумма " + summ + " грн. не равна сумме данных в строках таблицы, которая равна " + summlist + " грн.";
                            error = error.Concat(tmp).ToArray();
                        }
                    }
                }
            }
            else
            {
                RED = true;
                tmp[0] = "Пустой рабочий лист";
                error = error.Concat(tmp).ToArray();
            }
            if (Yellow)
            {
                error[0] = "2";
            }
            if (RED)
            {
                error[0] = "1";
            }

            
            return error;
        }

        private void FormWorkSheetEdit_Load(object sender, EventArgs e)
        {
            comboBoxSalon.Text = OldWorkSheeT.Rows[0].ItemArray[0].ToString();
            comboBoxWorker.Text = OldWorkSheeT.Rows[0].ItemArray[3].ToString();
        }

        private void FormWorkSheetEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (refresh)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
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
