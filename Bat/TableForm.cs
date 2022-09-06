using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using MSO.Excel;
using System.IO;
using System.Xml;


namespace Bat
{
    public partial class FormTable : Form
    {
        private OracleConnection conn;
        private OracleCommand insUpdCommand, delCommand;
        string Command, formName, OrderBy, GroupBy, TypeOfForm;
        string[] columns, StrFilter;
        object[] ColorsMessages;
        DataTable columnsTbl, Filter, FilterColumns, SortTbl;
        private FormWaiting waiting;
       
        
        
        public FormTable(OracleConnection baseConn, DataTable editTbl, string SELECT, OracleCommand delete, string FormName, OracleCommand INSERTUPDATE, DataTable Order, DataTable FilterTbl, DataTable StartFilter, string FormType, string Group)
        {
            InitializeComponent();

            switch (FormType)
            {
                case "UserRights":
                    Button buttonUserRights = new Button();
                    tableLayoutPanel1.Controls.Add(buttonUserRights, 4, 0);
                    buttonUserRights.Dock = DockStyle.Fill;
                    buttonUserRights.Text = "Права пользователя";
                    buttonUserRights.Click += new EventHandler(buttonUserRights_Click);
                    break;
                case "CheckWorkSheet":
                    Button buttonChecked = new Button();
                    tableLayoutPanel1.Controls.Add(buttonChecked, 4, 0);
                    buttonChecked.Dock = DockStyle.Fill;
                    buttonChecked.Text = "Внести белые и желтые листы в базу";
                    buttonChecked.Click += new EventHandler(buttonChecked_Click);
                    break;
              /*  case "staffList":
                    Button buttonStaffXml = new Button();
                    tableLayoutPanel1.Controls.Add(buttonStaffXml, 4, 0);
                    buttonStaffXml.Dock = DockStyle.Fill;
                    buttonStaffXml.Text = "Экспорт файла для салона";
                    buttonStaffXml.Click += new EventHandler(buttonStaffXml_Click);
                    break;
                case "CASH_REG_CODE":
                    Button buttonCash_reg_codeXml = new Button();
                    tableLayoutPanel1.Controls.Add(buttonCash_reg_codeXml, 4, 0);
                    buttonCash_reg_codeXml.Dock = DockStyle.Fill;
                    buttonCash_reg_codeXml.Text = "Экспорт файла для салона";
                    buttonCash_reg_codeXml.Click += new EventHandler(buttonCash_reg_codeXml_Click);
                    break;
                case "Salons":
                    Button buttonSalons = new Button();
                    tableLayoutPanel1.Controls.Add(buttonSalons, 4, 0);
                    buttonSalons.Dock = DockStyle.Fill;
                    buttonSalons.Text = "Экспорт файла для салона";
                    buttonSalons.Click += new EventHandler(buttonSalons_Click);
                    break;*/
            }
                        
            TypeOfForm = FormType;
            conn = baseConn;
            insUpdCommand = INSERTUPDATE;
            Command = SELECT;
            FilterColumns = FilterTbl;
            delCommand = delete;
            SortTbl = Order;
            GroupBy = Group;
            OrderBy = FormSortConstructor.SortString(SortTbl);
            columns = new string[2];
            columns[1] = "IDN";
            columns[0] = "№";
            for (int i = 0; i < editTbl.Rows.Count; i++)
            {
                string[] tmp = new string[1];
                tmp[0] = editTbl.Rows[i].ItemArray[0].ToString();
                columns = columns.Concat(tmp).ToArray();
            }
            
            formName = FormName;
            Text = FormName;
            columnsTbl = editTbl;
            Filter = StartFilter;
            StrFilter = FormFilterConstructor.GetFilter(Filter, Command, FilterColumns);
            RefreshData();
            //RefreshData();
            
        }
        /*
        // генерация XML файла с салонами для программы счетов
        void buttonSalons_Click(object sender, EventArgs e)
        {
            XmlDocument resultXml = new XmlDocument();
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (!File.Exists(Path + "\\salon.xml"))
            {

                using (XmlWriter writer = XmlWriter.Create(Path + "\\salon.xml"))
                {
                    // Write XML data.
                    writer.WriteStartElement("salon");
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            resultXml.Load(Path + "\\salon.xml");
            resultXml.DocumentElement.RemoveAll();
            XmlElement line;

            waiting = new FormWaiting("Салоны", "SELECT s.IDN, st.STYPE, s.SNAME, s.ADRSS, s.TEL1, s.TEL2 FROM LONDA.SALONS s, LONDA.SALONTYPE st WHERE s.IDN <> 0 AND s.STYPE = st.IDN AND s.STATE = 1", new string[] { "", "" }, null, null, conn, TypeOfForm, new string[] { "#", "ID", "TYPE", "SUB", "ADR", "TEL1", "TEL2" });
            waiting.ShowDialog();
            if (waiting.Данные != null)
            {
                for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                {
                    line = resultXml.CreateElement("item");
                    line.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                    line.SetAttribute("TYPE", waiting.Данные.Rows[i].ItemArray[2].ToString());
                    line.SetAttribute("SUB", waiting.Данные.Rows[i].ItemArray[3].ToString());
                    line.SetAttribute("ADR", waiting.Данные.Rows[i].ItemArray[4].ToString());
                    line.SetAttribute("TEL1", waiting.Данные.Rows[i].ItemArray[5].ToString());
                    line.SetAttribute("TEL2", waiting.Данные.Rows[i].ItemArray[6].ToString());
                    resultXml.DocumentElement.AppendChild(line);
                }
            }
            waiting.Dispose();
            resultXml.Save(Path + "\\salon.xml");
            MessageBox.Show("Файл \"salon.xml\" сохранен на рабочий стол");
        }
        // генерация XML файла прайс листа для программы счетов
        void buttonCash_reg_codeXml_Click(object sender, EventArgs e)
        {
            XmlDocument resultXml = new XmlDocument();
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (!File.Exists(Path + "\\price.xml"))
            {

                using (XmlWriter writer = XmlWriter.Create(Path + "\\price.xml"))
                {
                    // Write XML data.
                    writer.WriteStartElement("price");
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            resultXml.Load(Path + "\\price.xml");
            resultXml.DocumentElement.RemoveAll();
            XmlElement line;

            waiting = new FormWaiting("Прайс", "SELECT c.CODE, s.SERV_NAME || ' ' || v.VNAME, c.PRICE, c.PRICE10, c.PRICE50, c.PRICESTAFF, c.IDN FROM LONDA.CASH_REG_CODE c, LONDA.SERV_INFO s, LONDA.SERV_VOL v WHERE c.SERVICE = s.IDN AND s.VOL = v.IDN", new string[] { "", "" }, null, null, conn, TypeOfForm, new string[] { "#", "ID", "NAME", "PRICE0", "PRICE10", "PRICE50", "PRICESTAFF", "IDN" });
            waiting.ShowDialog();
            if (waiting.Данные != null)
            {
                for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                {
                    line = resultXml.CreateElement("item");
                    line.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                    line.SetAttribute("NAME", waiting.Данные.Rows[i].ItemArray[2].ToString());
                    line.SetAttribute("PRICE0", waiting.Данные.Rows[i].ItemArray[3].ToString());
                    if (waiting.Данные.Rows[i].ItemArray[4].ToString() != "")
                    {
                        line.SetAttribute("PRICE10", waiting.Данные.Rows[i].ItemArray[4].ToString());
                    }
                    if (waiting.Данные.Rows[i].ItemArray[5].ToString() != "")
                    {
                        line.SetAttribute("PRICE50", waiting.Данные.Rows[i].ItemArray[5].ToString());
                    }
                    if (waiting.Данные.Rows[i].ItemArray[6].ToString() != "")
                    {
                        line.SetAttribute("PRICESTAFF", waiting.Данные.Rows[i].ItemArray[6].ToString());
                    }
                    line.SetAttribute("IDN", waiting.Данные.Rows[i].ItemArray[7].ToString());
                    resultXml.DocumentElement.AppendChild(line);
                }
            }
            waiting.Dispose();
            resultXml.Save(Path + "\\price.xml");
            MessageBox.Show("Файл \"price.xml\" сохранен на рабочий стол");
        }
        // генерация XML файла со списком сотрудников для программы счетов
        void buttonStaffXml_Click(object sender, EventArgs e)
        {
            XmlDocument resultXml = new XmlDocument();
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (!File.Exists(Path + "\\staff.xml"))
            {

                using (XmlWriter writer = XmlWriter.Create(Path + "\\staff.xml"))
                {
                    // Write XML data.
                    writer.WriteStartElement("staff");
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            resultXml.Load(Path + "\\staff.xml");
            resultXml.DocumentElement.RemoveAll();
            XmlElement line;
            waiting = new FormWaiting("Профессии", "SELECT IDN, PROF FROM LONDA.PROFESSIONS", new string[] { "", "" }, null, null, conn, TypeOfForm, new string[] { "#", "IDN", "PROF" });
            waiting.ShowDialog();
            if (waiting.Данные != null)
            {
                for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                {
                    line = resultXml.CreateElement("profession");
                    line.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                    line.SetAttribute("NAME", waiting.Данные.Rows[i].ItemArray[2].ToString());
                    resultXml.DocumentElement.AppendChild(line);
                }
            }
            waiting.Dispose();
            waiting = new FormWaiting("Звания", "SELECT IDN, STAGE FROM LONDA.STAGES", new string[] { "", "" }, null, null, conn, TypeOfForm, new string[] { "#", "IDN", "STAGE" });
            waiting.ShowDialog();
            if (waiting.Данные != null)
            {
                for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                {
                    line = resultXml.CreateElement("rank");
                    line.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                    line.SetAttribute("NAME", waiting.Данные.Rows[i].ItemArray[2].ToString());
                    resultXml.DocumentElement.AppendChild(line);
                }
            }
            waiting.Dispose();
            waiting = new FormWaiting("Сотрудники", "SELECT STAFFID, FNAME || ' ' || FSTNAME, STAGE, PROF, IDN FROM LONDA.STAFF WHERE STAFFID IS NOT NULL AND HIREDATE IS NOT NULL AND DROPDATE IS NULL", new string[] { "", "" }, null, null, conn, TypeOfForm, new string[] { "#", "STAFFID", "NAME", "STAGE", "PROF", "IDN" });
            waiting.ShowDialog();
            if (waiting.Данные != null)
            {
                for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                {
                    line = resultXml.CreateElement("worker");
                    line.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                    line.SetAttribute("NAME", waiting.Данные.Rows[i].ItemArray[2].ToString());
                    line.SetAttribute("LEVEL", waiting.Данные.Rows[i].ItemArray[3].ToString());
                    line.SetAttribute("PROF", waiting.Данные.Rows[i].ItemArray[4].ToString());
                    line.SetAttribute("IDN", waiting.Данные.Rows[i].ItemArray[5].ToString());
                    resultXml.DocumentElement.AppendChild(line);
                }
            }
            waiting.Dispose();
            resultXml.Save(Path + "\\staff.xml");
            MessageBox.Show("Файл \"staff.xml\" сохранен на рабочий стол");

        }
        */
        void RefreshData()
        {
            waiting = new FormWaiting("Обновление данных", Command, StrFilter, GroupBy, OrderBy, conn, TypeOfForm, columns);
            waiting.ShowDialog();
            if (waiting.Данные != null)
            {
                ColorsMessages = waiting.Цвета;
                dataGridViewData.DataSource = waiting.Данные;
                dataGridViewData.Columns[1].Visible = false;
                dataGridViewData.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewData.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewData.MultiSelect = false;
                for (int t = 2; t < dataGridViewData.Columns.Count; t++)
                {
                    dataGridViewData.Columns[t].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewData.Columns[t].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                labelFiltr.Text = "Фильтр: " + StrFilter[1];
            }
            waiting.Dispose();
        }

        void buttonChecked_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Внести все белые и желтые рабочие листы в список проверенных рабочих листов ?", "Проверка рабочих листов", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Login.OpenConnection(conn))
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "LONDA.VALIDATEWRKSH";
                    OracleParameter par;
                    string[] parameters = { "in_DAT", 
                                      "in_SHEETNUM" };
                    foreach (string line in parameters)
                    {
                        par = cmd.CreateParameter();
                        par.ParameterName = line;
                        par.OracleType = OracleType.VarChar;
                        par.Size = 0;
                        par.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(par);
                    }
                    par = cmd.CreateParameter();
                    par.ParameterName = "ERRSHEET";
                    par.OracleType = OracleType.NVarChar;
                    par.Size = 4000;
                    par.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(par);
                    cmd.Connection = conn;
                    string[] MessageSh = new string[0];
                    bool error = false;
                    for (int i = 0; i < ColorsMessages.Count(); i++)
                    {
                        error = false;
                        string[] ErrorType = (string[])ColorsMessages[i];
                        if (ErrorType[0] != "1")
                        {
                            string[] tmp = new string[1];

                            cmd.Parameters[0].Value = dataGridViewData.Rows[i].Cells[4].Value.ToString();
                            cmd.Parameters[1].Value = dataGridViewData.Rows[i].Cells[2].Value.ToString();
                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (OracleException oe)
                            {
                                //MessageBox.Show(oe.Message, "Ошибка");
                                tmp[0] = "Ошибка. Рабочий лист №" + dataGridViewData.Rows[i].Cells[2].Value.ToString() + " - " + oe.Message;
                                MessageSh = MessageSh.Concat(tmp).ToArray();
                                error = true;
                            }
                            
                            if (!error)
                            {
                                tmp[0] = cmd.Parameters[cmd.Parameters.Count - 1].Value.ToString();
                                MessageSh = MessageSh.Concat(tmp).ToArray();
                            }

                        }
                    }
                    conn.Close();
                    ErrorForm err = new ErrorForm(MessageSh, "Рабочие листы обработаны");
                    err.ShowDialog();
                    RefreshData();
                    //throw new NotImplementedException();
                }
                else
                {
                    MessageBox.Show("Невозможно выполнить команду", "Потеряно соединение с базой", MessageBoxButtons.OK);
                }
            }
        }

        void buttonUserRights_Click(object sender, EventArgs e)
        {
            FormUserRights userRights = new FormUserRights(conn, dataGridViewData.SelectedRows[0].Cells[1].Value.ToString());
            userRights.ShowDialog();
            //throw new NotImplementedException();
        }

        private void RefreshRow(int RowIndex)
        {
            /*string StrFilterIDN = StrFilter[0] + " AND " + Command.Substring(7, Command.IndexOf(",") - 7) + " = " + dataGridViewData.Rows[RowIndex].Cells[1].Value.ToString();

            OracleCommand cmd = new OracleCommand(Command + StrFilterIDN + GroupBy + OrderBy, conn);
            DataTable Data = Functions.GetData(cmd);*/

            string StrFilterIDN = " AND " + Command.Substring(7, Command.IndexOf(",") - 7) + " = " + dataGridViewData.Rows[RowIndex].Cells[1].Value.ToString();

            OracleCommand cmd = new OracleCommand(Command + StrFilterIDN, conn);
            DataTable Data = Functions.GetData(cmd);
            if (Data != null)
            {
                if (Data.Rows.Count > 0)
                {
                    DataTable dataTbl = new DataTable();
                    for (int i = 0; i < columns.Count(); i++)
                    {
                        dataTbl.Columns.Add(columns[i]);
                    }

                    for (int y = 0; y < Data.Rows.Count; y++)
                    {
                        DataRow row = dataTbl.NewRow();
                        row[0] = y + 1;
                        for (int x = 0; x < Data.Columns.Count; x++)
                        {
                            row[x + 1] = Data.Rows[y].ItemArray[x];
                        }
                        dataTbl.Rows.Add(row);
                    }

                    object[] ClrsMssgs = null;
                    if (TypeOfForm == "CheckWorkSheet" | TypeOfForm == "WorkSheetList")
                    {
                        ClrsMssgs = MakeCheck(dataTbl);
                        ColorsMessages[RowIndex] = ClrsMssgs[0];
                    }

                    for (int f = 2; f < dataTbl.Columns.Count; f++)
                    {
                        dataGridViewData.Rows[RowIndex].Cells[f].Value = dataTbl.Rows[0].ItemArray[f];
                    }
                }
                else
                {
                    MessageBox.Show("Невозможно обновить данные", "Ошибка", MessageBoxButtons.OK);
                    RefreshData();
                }
            }
            else
            {
                MessageBox.Show("Невозможно обновить данные", "Ошибка", MessageBoxButtons.OK);
                RefreshData();
            }
        }
        /*private void RefreshData()
        {
            
            string test = Command + StrFilter;
            OracleCommand cmd = new OracleCommand(Command + StrFilter[0] + GroupBy + OrderBy, conn);
            waiting.SetLabelText("Чтение данных");
        START:
            OracleDataReader reader = Functions.ExecuteReader(cmd);
            DataTable Data = new DataTable();
            if (reader != null)
            {
                
                
                try
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Data.Columns.Add();
                    }
                }
                catch (Exception e)
                {
                    if (MessageBox.Show(e.Message, "", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    {
                        goto START;
                    }
                    else
                    {

                        Data = null;
                    }
                }

                try
                {
                    if (Login.OpenConnection(cmd.Connection))
                    {
                        int g = 1;
                        while (reader.Read())
                        {
                            DataRow row = Data.NewRow();

                            for (int x = 0; x < reader.FieldCount; x++)
                            {
                                switch (reader.GetDataTypeName(x))
                                {
                                    case "VARCHAR2":
                                        try
                                        {
                                            row[x] = reader.GetString(x);
                                        }
                                        catch
                                        {

                                        }
                                        break;
                                    case "NUMBER":
                                        try
                                        {
                                            OracleNumber num = reader.GetOracleNumber(x);
                                            if (!num.IsNull)
                                            {
                                                row[x] = num.Value;
                                            }
                                        }
                                        catch (OracleException oe)
                                        {
                                            MessageBox.Show(oe.Message, "Ошибка");
                                        }
                                        break;
                                    case "DATE":
                                        try
                                        {

                                            row[x] = reader.GetString(x);
                                        }
                                        catch
                                        {

                                        }
                                        break;
                                }
                            }
                            Data.Rows.Add(row);
                            waiting.SetProcLabelText(g.ToString());
                            g = g + 1;
                        }
                        reader.Close();
                        cmd.Connection.Close();
                    }
                    else
                    {
                        Data =null;
                    }
                }
                catch (OracleException oe)
                {
                    if (MessageBox.Show(oe.Message, "", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    {
                        goto START;
                    }
                    else
                    {
                        Data = null;
                    }
                }
                
            }
            else
            {
                Data = null;
            }
            //DataTable Data = Functions.GetData(cmd);
            if (Data != null)
            {
                
                DataTable dataTbl = new DataTable();
                for (int i = 0; i < columns.Count(); i++)
                {
                    dataTbl.Columns.Add(columns[i]);
                }

                for (int y = 0; y < Data.Rows.Count; y++)
                {
                    DataRow row = dataTbl.NewRow();
                    row[0] = y + 1;
                    for (int x = 0; x < Data.Columns.Count; x++)
                    {
                        row[x + 1] = Data.Rows[y].ItemArray[x];
                    }
                    dataTbl.Rows.Add(row);
                }

                if (TypeOfForm == "CheckWorkSheet" | TypeOfForm == "WorkSheetList")
                {
                    ColorsMessages = MakeCheck(dataTbl);
                }
                //SetDataGridSource(dataTbl);
            }
            else
            {
                MessageBox.Show("Невозможно обновить данные", "Ошибка", MessageBoxButtons.OK);
            }
        }
        */
       

        private object[] MakeCheck(DataTable GridDataSource)
        {
            
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            object[] ColorsMessagesObj = new object[GridDataSource.Rows.Count];
            waiting.SetLabelText("Проверка рабочих листов");
            for (int i = 0; i < GridDataSource.Rows.Count; i++)
            {
                switch (TypeOfForm)
                {
                    case "CheckWorkSheet":
                        cmd.CommandText = "SELECT w.SALON, w.SHEETNUM, to_char(w.DAT,'ddmmyyyy'), w.WORKER, to_char(w.SDATE,'HH24MI'), to_char(w.FDATE,'HH24MI'), NULL, d.NUMCL, to_char(d.SDATE1,'HH24MI'), to_char(d.FDATE1,'HH24MI'), d.SCODE, d.DISC, d.QUANTITY, d.PRICE, d.IDN, w.SUMM, d.MISTAKES, d.CLIENT, d.DMASTER, d.DDATE FROM LONDA.WRKSHTOCHECK w, LONDA.WRKSHTOCHECKDATA d WHERE w.SALON = '" + GridDataSource.Rows[i].ItemArray[3].ToString() + "' AND w.SHEETNUM = " + GridDataSource.Rows[i].ItemArray[2].ToString() + " AND w.DAT = to_date('" + GridDataSource.Rows[i].ItemArray[4].ToString() + "','dd/mm/yyyy') AND w.IDN = d.WORKSHEET ORDER BY d.NROW ASC";
                        break;
                    case "WorkSheetList":
                        cmd.CommandText = "SELECT s.SNAME, w.SHEETNUM, to_char(w.DAT,'ddmmyyyy'), f.FNAME || ' ' || f.FSTNAME || ' ' || f.FATHNAME, to_char(w.SDATE,'HH24MI'), to_char(w.FDATE,'HH24MI'), NULL, d.NUMCL, to_char(d.SDATE1,'HH24MI'), to_char(d.FDATE1,'HH24MI'), c.CODE, d.DISC, d.QUANTITY, d.PRICE, d.IDN, NULL, d.MISTAKES, d.CLIENT, d.DMASTER, d.DDATE FROM LONDA.WORKSHEETS w, LONDA.WORKSHEETSDATA d, LONDA.SALONS s, LONDA.STAFF f, LONDA.CASH_REG_CODE c WHERE s.SNAME = '" + GridDataSource.Rows[i].ItemArray[3].ToString() + "' AND w.SHEETNUM = " + GridDataSource.Rows[i].ItemArray[2].ToString() + " AND w.DAT = to_date('" + GridDataSource.Rows[i].ItemArray[4].ToString() + "','dd/mm/yyyy') AND d.WORKSHEET = w.IDN AND w.WORKER = f.IDN AND w.SALON = s.IDN AND d.SCODE = c.IDN ORDER BY d.NROW ASC";
                        break;
                }
                DataTable WorkSheet = Functions.GetData(cmd);
                WorkSheet.Columns[0].ColumnName = "SALON";     //0
                WorkSheet.Columns[1].ColumnName = "SHEETNUM";  //1
                WorkSheet.Columns[2].ColumnName = "DAT";       //2
                WorkSheet.Columns[3].ColumnName = "WORKER";    //3
                WorkSheet.Columns[4].ColumnName = "SDATE";     //4
                WorkSheet.Columns[5].ColumnName = "FDATE";     //5
                WorkSheet.Columns[6].ColumnName = "NROW";      //6
                WorkSheet.Columns[7].ColumnName = "NUMCL";     //7
                WorkSheet.Columns[8].ColumnName = "SDATE1";    //8
                WorkSheet.Columns[9].ColumnName = "FDATE1";    //9
                WorkSheet.Columns[10].ColumnName = "SCODE";     //10
                WorkSheet.Columns[11].ColumnName = "DISCOUNT";  //11
                WorkSheet.Columns[12].ColumnName = "QUANTITY";  //12
                WorkSheet.Columns[13].ColumnName = "PRICE";     //13
                WorkSheet.Columns[14].ColumnName = "IDN";       //14
                WorkSheet.Columns[15].ColumnName = "SUMM";      //15
                WorkSheet.Columns[16].ColumnName = "MISTAKES";  //16
                WorkSheet.Columns[17].ColumnName = "CLIENT";    //17
                WorkSheet.Columns[18].ColumnName = "DMASTER";   //18
                WorkSheet.Columns[19].ColumnName = "DDATE";     //19
                
                if (WorkSheet != null)
                {
                    if (TypeOfForm == "WorkSheetList")
                    {
                        decimal result, summ = 0;
                        for (int v = 0; v < WorkSheet.Rows.Count; v++)
                        {
                            if (WorkSheet.Rows[v].ItemArray[16].ToString() != "1" && decimal.TryParse(WorkSheet.Rows[v].ItemArray[13].ToString(), out result))
                            {
                                summ = summ + result;
                            }
                        }

                        DataRow row = WorkSheet.Rows[0];
                        row[15] = summ;
                    }
                    string[] MessageColours = FormWorkSheetEdit.CheckWorkSheet(WorkSheet, conn);
                    int g = GridDataSource.Rows.Count - i;
                    waiting.SetProcLabelText(g.ToString());

                    ColorsMessagesObj[i] = MessageColours;
                    

                    WorkSheet.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Невозможно верно отобразить таблицу, окно будет закрыто", "Ошибка оракле ридер", MessageBoxButtons.OK);
                    this.Dispose();
                }
            }
            return ColorsMessagesObj;    
        }

        

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            switch (TypeOfForm)
            {
                case "CheckWorkSheet":
                    MessageBox.Show("Добавлять рабочие листы разрешается только с помощью меню \"Добавить рабочий лист\"", "Сообщение");
                    break;
                default:
                    DataGridViewRow row = new DataGridViewRow();
                    FormAddEdit add = new FormAddEdit(formName, conn, columnsTbl, insUpdCommand, null);
                    if (add.ShowDialog() == DialogResult.OK)
                    {
                        RefreshData();
                        for (int o = 0; o < dataGridViewData.Rows.Count; o++)
                        {
                            if (int.Parse(dataGridViewData.Rows[o].Cells[1].Value.ToString()) == add.ItemNum)
                            {
                                dataGridViewData.CurrentCell = dataGridViewData.Rows[o].Cells[0];
                                break;
                            }
                        }



                    }
                    break;
            }
        }

        private void EditRow()
        {
            switch (TypeOfForm)
            {
                case "CheckWorkSheet":
                    FormWorkSheetEdit edit = new FormWorkSheetEdit(conn, (string[])ColorsMessages[dataGridViewData.SelectedRows[0].Index], dataGridViewData.SelectedRows[0], "CheckWorkSheet");
                    if (edit.ShowDialog() == DialogResult.OK)
                    {
                        RefreshRow(dataGridViewData.SelectedRows[0].Index);
                        //RefreshData();
                    }

                    break;
                case "WorkSheetList":
                    edit = new FormWorkSheetEdit(conn, (string[])ColorsMessages[dataGridViewData.SelectedRows[0].Index], dataGridViewData.SelectedRows[0], "WorkSheetList");
                    if (edit.ShowDialog() == DialogResult.OK)
                    {
                        RefreshRow(dataGridViewData.SelectedRows[0].Index);
                        //RefreshData();
                    }
                    break;
                default:
                    FormAddEdit add = new FormAddEdit(formName, conn, columnsTbl, insUpdCommand, dataGridViewData.SelectedRows[0]);
                    if (add.ShowDialog() == DialogResult.OK)
                    {
                        RefreshRow(dataGridViewData.SelectedRows[0].Index);
                        //RefreshData();
                        /*for (int o = 0; o < dataGridViewData.Rows.Count; o++)
                        {
                            if (int.Parse(dataGridViewData.Rows[o].Cells[1].Value.ToString()) == add.ItemNum)
                            {
                                dataGridViewData.CurrentCell = dataGridViewData.Rows[o].Cells[0];
                                break;
                            }
                        }*/
                    }
                    break;
            }
        }
        

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditRow();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string tmpDel = delCommand.CommandText;

            if (MessageBox.Show("Вы хотите удалить " + dataGridViewData.SelectedRows[0].Cells[2].Value.ToString() + " ?", "Удаление", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                bool error = false;
                if (delCommand.CommandType == CommandType.Text)
                {
                    delCommand.CommandText = delCommand.CommandText + dataGridViewData.SelectedRows[0].Cells[1].Value.ToString();
                }
                if (delCommand.CommandType == CommandType.StoredProcedure)
                {
                    switch (TypeOfForm)
                    {
                        case "CheckWorkSheet":
                            delCommand.Parameters[0].Value = dataGridViewData.SelectedRows[0].Cells[4].Value.ToString();
                            delCommand.Parameters[1].Value = dataGridViewData.SelectedRows[0].Cells[2].Value.ToString();
                            break;
                        default:
                            delCommand.Parameters[0].Value = dataGridViewData.SelectedRows[0].Cells[1].Value.ToString();
                            break;
                    }
                }
                if (Login.OpenConnection(conn))
                {
                    delCommand.Connection = conn;
                    try
                    {
                        delCommand.ExecuteNonQuery();
                    }
                    catch (OracleException oe)
                    {
                        MessageBox.Show(oe.Message, "Ошибка");
                        error = true;
                    }
                    int result;
                    if (error == false & delCommand.CommandType == CommandType.StoredProcedure && !int.TryParse(delCommand.Parameters[delCommand.Parameters.Count - 1].Value.ToString(), out result))
                    {
                        MessageBox.Show(delCommand.Parameters[delCommand.Parameters.Count - 1].Value.ToString(), "Ошибка");
                        error = true;
                    }
                    if (!error)
                    {
                        OracleCommand cmd2 = new OracleCommand("COMMIT", conn);
                        try
                        {
                            cmd2.ExecuteNonQuery();
                            if (delCommand.CommandType == CommandType.Text)
                            {
                                delCommand.CommandText = delCommand.CommandText.Substring(0, delCommand.CommandText.Length - dataGridViewData.SelectedRows[0].Cells[1].Value.ToString().Length);
                            }
                        }
                        catch (OracleException oe)
                        {
                            MessageBox.Show(oe.Message, "Ошибка");
                        }
                    }
                    conn.Close();
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Невозможно выполнить удаление", "Потеряно соединение с базой", MessageBoxButtons.OK);
                }
            }
            delCommand.CommandText = tmpDel;
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            FormFilterConstructor filtr = new FormFilterConstructor(FilterColumns, Filter, StrFilter, Command);
            if (filtr.ShowDialog() == DialogResult.OK)
            {
                Filter = filtr.FilterTable;
                StrFilter = filtr.AddFilter;

                RefreshData();
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            FormSortConstructor sort = new FormSortConstructor(FilterColumns, SortTbl, OrderBy);
            sort.ShowDialog();
            SortTbl = sort.SortTable;
            OrderBy = sort.AddSort;
            RefreshData();
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            if (saveFileDialogExcel.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialogExcel.FileName))
                {
                    try
                    {
                        File.Delete(saveFileDialogExcel.FileName);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message, "Ошибка");
                    }

                }

                string[] range1 = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                string[] range2 = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                Excel ex = new Excel();
                System.Globalization.CultureInfo info = System.Globalization.CultureInfo.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                ex.NewDocument();
                int x1 = 0, x2 = 0;
                for (int y = 2; y < dataGridViewData.Columns.Count; y++)
                {
                    ex.SetValue(range1[x1] + range2[x2] + (1).ToString(), dataGridViewData.Columns[y].HeaderText);
                    ex.SetColumnWidth(range1[x1] + range2[x2] + (1).ToString(), dataGridViewData.Columns[y].Width / 9);
                    for (int i = 0; i < dataGridViewData.Rows.Count; i++)
                    {

                        if (dataGridViewData.Rows[i].Cells[y].Value.ToString() != "" & FilterColumns.Rows[y - 2].ItemArray[2].ToString() == "date")
                        {
                            ex.SetValue(range1[x1] + range2[x2] + (i + 2).ToString(), "d" + dataGridViewData.Rows[i].Cells[y].Value.ToString());
                        }
                        else
                        {
                            ex.SetValue(range1[x1] + range2[x2] + (i + 2).ToString(), dataGridViewData.Rows[i].Cells[y].Value.ToString());
                        }
                    }
                    if (y != dataGridViewData.Columns.Count - 1)
                    {
                        if (range2[x2] == "Z")
                        {
                            x1 = x1 + 1;
                        }
                        x2 = x2 + 1;
                        if (x2 == 25)
                        {
                            x2 = 0;
                        }
                    }
                }

                ex.SetBorderStyle("A1:" + range1[x1] + range2[x2] + (dataGridViewData.Rows.Count + 1).ToString(), 1);

                ex.SaveDocument(saveFileDialogExcel.FileName);
                ex.CloseDocument();
                ex.Dispose();
                System.Threading.Thread.CurrentThread.CurrentCulture = info;
 
            }
        }

        private void dataGridViewData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (TypeOfForm == "CheckWorkSheet" | TypeOfForm == "WorkSheetList")
            {
                if (ColorsMessages.Count() != 0)
                {
                    string[] tmpStrArray = (string[])ColorsMessages[e.RowIndex];
                    switch (int.Parse(tmpStrArray[0]))
                    {
                        case 1:
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.SelectionBackColor = Color.RosyBrown;
                            break;
                        case 2:
                            e.CellStyle.BackColor = Color.Yellow;
                            e.CellStyle.SelectionBackColor = Color.YellowGreen;
                            break;
                    }

                }   
            }
        }

        private void dataGridViewData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                EditRow();
            }
        }

       
        
            
        
    }
}
