using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Threading;


namespace Bat
{
    public partial class FormWaiting : Form
    {
        private delegate void SetLabelTextCallback(string text);
        private delegate void SetFormTextCallback(string text);
        private delegate void ShowFormCallback();
        private delegate void SetResultCallback(DataTable tbl, object[] cls);
        OracleConnection conn;
        string Command, GroupBy, OrderBy, TypeOfForm;
        string[] StrFilter, columns;
        DataTable resultData;
        object[] resultColors;

        public DataTable Данные
        {
            get { return resultData; }
        }

        public object[] Цвета
        {
            get { return resultColors; }
        }
        
        public FormWaiting(string FormHeader, string cmd, string[] Filtr, string group, string order, OracleConnection connect, string typeofform, string[] col)
        {
            InitializeComponent();
            Command = cmd;
            StrFilter = Filtr;
            GroupBy = group;
            OrderBy = order;
            conn = connect;
            TypeOfForm = typeofform;
            columns = col;
            this.Text = FormHeader;
            Thread thread2 = new Thread(RefreshData);
            thread2.Start();
        }

        public void SetLabelText(string text)
        {

            if (labelProcess.InvokeRequired)
            {
                SetLabelTextCallback d = new SetLabelTextCallback(SetLabelText);
                Invoke(d, new object[] { text });
            }
            else
            {
                labelProcess.Text = text;
            }
        }

        public void ShowForm()
        {
            if (InvokeRequired)
            {
                ShowFormCallback d = new ShowFormCallback(ShowForm);
                Invoke(d, new object[] { });
            }
            else
            {
                ShowDialog();
            }
        }


        public void SetProcLabelText(string text)
        {

            if (labelRowsCount.InvokeRequired)
            {
                SetLabelTextCallback d = new SetLabelTextCallback(SetProcLabelText);
                Invoke(d, new object[] { text });
            }
            else
            {
                labelRowsCount.Text = text;
            }
        }

        public void SetFormText(string text)
        {

            if (InvokeRequired)
            {
                SetFormTextCallback d = new SetFormTextCallback(SetFormText);
                Invoke(d, new object[] { text });
            }
            else
            {
                Text = text;
            }
        }
        private void RefreshData()
        {

            OracleCommand cmd = new OracleCommand(Command + StrFilter[0] + GroupBy + OrderBy, conn);
            SetLabelText("Чтение данных");
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
                            SetProcLabelText(g.ToString());
                            g = g + 1;
                        }
                        reader.Close();
                        cmd.Connection.Close();
                    }
                    else
                    {
                        Data = null;
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
                object[] ClrsMssgs = null;
                if (TypeOfForm == "CheckWorkSheet" | TypeOfForm == "WorkSheetList")
                {
                    ClrsMssgs = MakeCheck(dataTbl);
                }
                SetResult(dataTbl, ClrsMssgs);
                /*if (dataTbl.Rows.Count < 10)
                {
                    Thread.Sleep(1000);
                }*/
            }
            else
            {
                MessageBox.Show("Невозможно обновить данные", "Ошибка", MessageBoxButtons.OK);
                SetResult(null, null);
            }
        }

        void SetResult(DataTable tbl, object[] cls)
        {
            if (InvokeRequired)
            {
                SetResultCallback d = new SetResultCallback(SetResult);
                Invoke(d, new object[] { tbl, cls });
            }
            else
            {
                resultData = tbl;
                resultColors = cls;
                DialogResult = DialogResult.OK;
            }
        }

        private object[] MakeCheck(DataTable GridDataSource)
        {

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            object[] ColorsMessagesObj = new object[GridDataSource.Rows.Count];
            SetLabelText("Проверка рабочих листов");
            for (int i = 0; i < GridDataSource.Rows.Count; i++)
            {
                switch (TypeOfForm)
                {
                    case "CheckWorkSheet":
                        cmd.CommandText = "SELECT w.SALON, w.SHEETNUM, to_char(w.DAT,'ddmmyyyy'), w.WORKER, to_char(w.SDATE,'HH24MI'), to_char(w.FDATE,'HH24MI'), NULL, d.NUMCL, to_char(d.SDATE1,'HH24MI'), to_char(d.FDATE1,'HH24MI'), d.SCODE, d.DISC, d.QUANTITY, d.PRICE, d.IDN, w.SUMM, d.MISTAKES, d.CLIENT, d.DMASTER, d.DDATE FROM LONDA.WRKSHTOCHECK w, LONDA.WRKSHTOCHECKDATA d WHERE w.SALON = '" + GridDataSource.Rows[i].ItemArray[3].ToString() + "' AND w.SHEETNUM = " + GridDataSource.Rows[i].ItemArray[2].ToString() + " AND w.DAT = to_date('" + GridDataSource.Rows[i].ItemArray[4].ToString() + "','dd/mm/yyyy') AND w.IDN = d.WORKSHEET ORDER BY NROW ASC";
                        break;
                    case "WorkSheetList":
                        cmd.CommandText = "SELECT s.SNAME, w.SHEETNUM, to_char(w.DAT,'ddmmyyyy'), f.FNAME || ' ' || f.FSTNAME || ' ' || f.FATHNAME, to_char(w.SDATE,'HH24MI'), to_char(w.FDATE,'HH24MI'), NULL, d.NUMCL, to_char(d.SDATE1,'HH24MI'), to_char(d.FDATE1,'HH24MI'), c.CODE, d.DISC, d.QUANTITY, d.PRICE, d.IDN, NULL, d.MISTAKES, d.CLIENT, d.DMASTER, d.DDATE FROM LONDA.WORKSHEETS w, LONDA.WORKSHEETSDATA d, LONDA.SALONS s, LONDA.STAFF f, LONDA.CASH_REG_CODE c WHERE s.SNAME = '" + GridDataSource.Rows[i].ItemArray[3].ToString() + "' AND w.SHEETNUM = " + GridDataSource.Rows[i].ItemArray[2].ToString() + " AND w.DAT = to_date('" + GridDataSource.Rows[i].ItemArray[4].ToString() + "','dd/mm/yyyy') AND d.WORKSHEET = w.IDN AND w.WORKER = f.IDN AND w.SALON = s.IDN AND d.SCODE = c.IDN ORDER BY d.NROW ASC";
                        break;
                }
                DataTable WorkSheet = Functions.GetData(cmd);
                // Добавить проверку если не получили данные
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
                    SetProcLabelText(g.ToString());

                    ColorsMessagesObj[i] = MessageColours;
                    /*switch (MessageColours[0])
                    {
                        case "1":
                            ColorsMessagesObj[i] = MessageColours;
                            break;
                        case "2":
                            ColorsMessagesObj[i] = MessageColours;
                            break;
                        default:
                            ColorsMessagesObj[i] = MessageColours;
                            break;
                    }*/

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

    }
}
