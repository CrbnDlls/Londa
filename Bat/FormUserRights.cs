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
    public partial class FormUserRights : Form
    {
        OracleConnection conn;
        string dbUser;
        string UserName;
        string[] Tables, Proc;
        public FormUserRights(OracleConnection baseConn, string userIDN)
        {
            InitializeComponent();
            conn = baseConn;
            OracleCommand cmd = new OracleCommand("SELECT u.USER#, u.NAME FROM LONDA.STAFF s, SYS.USER$ u WHERE s.ISUSER = u.USER# AND s.IDN = " + userIDN, conn);
            DataTable Data = Functions.GetData(cmd);
            if (Data != null)
            {
                dbUser = Data.Rows[0].ItemArray[0].ToString();
                UserName = Data.Rows[0].ItemArray[1].ToString();
            }
            else
            {
                MessageBox.Show("Невозможно отобразить данные", "Ошибка", MessageBoxButtons.OK);
                this.Dispose();
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LONDA.CHK_USER_MENU_RIGHTS";
            OracleParameter par = cmd.CreateParameter();
            par.ParameterName = "in_IDN";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "ERRCHK";
            par.OracleType = OracleType.NVarChar;
            par.Size = 4000;
            par.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par);
            cmd.Parameters["in_IDN"].Value = dbUser;
            if (!Functions.ExecuteNonQuery(cmd))
            {
                MessageBox.Show("Невозможно отобразить данные", "Ошибка", MessageBoxButtons.OK);
                this.Dispose();
            }
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            // Меню
            cmd.CommandText = "SELECT r.IDN, m.TOPNAME || ' | ' ||m.MIDDLENAME || ' | ' || m.BOTTOMNAME || '|', r.GSELECT, r.GINSERT, r.GUPDATE, r.GDROP FROM LONDA.MENUS m, LONDA.USER_RIGHTS r WHERE r.LONDAUSER = " + dbUser + " AND r.MENU = m.IDN ORDER BY m.TOPNAME ASC, m.MIDDLENAME ASC, m.BOTTOMNAME ASC";
            Data = Functions.GetData(cmd);
            if (Data != null)
            {
                for (int z = 0; z < Data.Rows.Count; z++)
                {
                    dataGridViewMain.Rows.Add();
                    dataGridViewMain.Rows[dataGridViewMain.Rows.Count - 1].Cells["ColumnIDN"].Value = Data.Rows[z].ItemArray[0];
                    string TrueFalse = Data.Rows[z].ItemArray[1].ToString();
                    dataGridViewMain.Rows[dataGridViewMain.Rows.Count - 1].Cells["ColumnMenus"].Value = TrueFalse.Substring(0, TrueFalse.LastIndexOf("|"));
                    TrueFalse = "N";
                    TrueFalse = Data.Rows[z].ItemArray[2].ToString();
                    if (TrueFalse == "Y")
                    {
                        dataGridViewMain.Rows[dataGridViewMain.Rows.Count - 1].Cells["ColumnSELECT"].Value = true;
                    }
                    else
                    {
                        dataGridViewMain.Rows[dataGridViewMain.Rows.Count - 1].Cells["ColumnSELECT"].Value = false;
                    }
                    TrueFalse = "N";
                    TrueFalse = Data.Rows[z].ItemArray[3].ToString();
                    if (TrueFalse == "Y")
                    {
                        dataGridViewMain.Rows[dataGridViewMain.Rows.Count - 1].Cells["ColumnINSERT"].Value = true;
                    }
                    else
                    {
                        dataGridViewMain.Rows[dataGridViewMain.Rows.Count - 1].Cells["ColumnINSERT"].Value = false;
                    }
                    TrueFalse = "N";
                    TrueFalse = Data.Rows[z].ItemArray[4].ToString();
                    if (TrueFalse == "Y")
                    {
                        dataGridViewMain.Rows[dataGridViewMain.Rows.Count - 1].Cells["ColumnUPDATE"].Value = true;
                    }
                    else
                    {
                        dataGridViewMain.Rows[dataGridViewMain.Rows.Count - 1].Cells["ColumnUPDATE"].Value = false;
                    }
                    TrueFalse = "N";
                    TrueFalse = Data.Rows[z].ItemArray[5].ToString();
                    if (TrueFalse == "Y")
                    {
                        dataGridViewMain.Rows[dataGridViewMain.Rows.Count - 1].Cells["ColumnDELETE"].Value = true;
                    }
                    else
                    {
                        dataGridViewMain.Rows[dataGridViewMain.Rows.Count - 1].Cells["ColumnDELETE"].Value = false;
                    }

                }
            }
            else
            {
                MessageBox.Show("Невозможно отобразить данные", "Ошибка", MessageBoxButtons.OK);
                this.Dispose();
            }
            // Настройки
            cmd.CommandText = "SELECT s.IDN, n.SNAME, s.VAL FROM LONDA.SETTINGS_NAME n, LONDA.USER_SETTINGS s WHERE s.LONDAUSER = " + dbUser + " AND s.SETTING = n.IDN ORDER BY n.SNAME ASC";
            Data = Functions.GetData(cmd);
            if (Data != null)
            {
                for (int z = 0; z < Data.Rows.Count; z++)
                {
                    dataGridViewSettings.Rows.Add();
                    dataGridViewSettings.Rows[dataGridViewSettings.Rows.Count - 1].Cells["ColumnIDNS"].Value = Data.Rows[z].ItemArray[0];
                    dataGridViewSettings.Rows[dataGridViewSettings.Rows.Count - 1].Cells["ColumnName"].Value = Data.Rows[z].ItemArray[1];
                    string TrueFalse = "N";
                    TrueFalse = Data.Rows[z].ItemArray[2].ToString();
                    if (TrueFalse == "Y")
                    {
                        dataGridViewSettings.Rows[dataGridViewSettings.Rows.Count - 1].Cells["ColumnValue"].Value = true;
                    }
                    else
                    {
                        dataGridViewSettings.Rows[dataGridViewSettings.Rows.Count - 1].Cells["ColumnValue"].Value = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Невозможно отобразить данные", "Ошибка", MessageBoxButtons.OK);
                this.Dispose();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            bool error = false;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LONDA.UPD_USER_RIGHTS";
            OracleParameter par = cmd.CreateParameter();
            par.ParameterName = "in_IDN";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "in_GSELECT";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "in_GINSERT";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "in_GUPDATE";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "in_GDROP";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "ERRUPD_USER_RIGHTS";
            par.OracleType = OracleType.NVarChar;
            par.Size = 4000;
            par.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par);

            for (int i = 0; i < dataGridViewMain.Rows.Count; i++)
            {
                cmd.Parameters["in_IDN"].Value = dataGridViewMain.Rows[i].Cells["ColumnIDN"].Value.ToString();
                if ((bool)dataGridViewMain.Rows[i].Cells["ColumnSELECT"].Value == true)
                {
                    cmd.Parameters["in_GSELECT"].Value = "Y";
                }
                else
                {
                    cmd.Parameters["in_GSELECT"].Value = "N";
                }
                if ((bool)dataGridViewMain.Rows[i].Cells["ColumnINSERT"].Value == true)
                {
                    cmd.Parameters["in_GINSERT"].Value = "Y";
                }
                else
                {
                    cmd.Parameters["in_GINSERT"].Value = "N";
                }
                if ((bool)dataGridViewMain.Rows[i].Cells["ColumnUPDATE"].Value == true)
                {
                    cmd.Parameters["in_GUPDATE"].Value = "Y";
                }
                else
                {
                    cmd.Parameters["in_GUPDATE"].Value = "N";
                }
                if ((bool)dataGridViewMain.Rows[i].Cells["ColumnDELETE"].Value == true)
                {
                    cmd.Parameters["in_GDROP"].Value = "Y";
                }
                else
                {
                    cmd.Parameters["in_GDROP"].Value = "N";
                }
                if (!Functions.ExecuteNonQuery(cmd))
                {
                    error = true;
                    break;
                }
            }
            cmd.Parameters.Clear();

            cmd.CommandText = "LONDA.UPD_USER_SETTINGS";
            par = cmd.CreateParameter();
            par.ParameterName = "in_IDN";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "in_VAL";
            par.OracleType = OracleType.VarChar;
            par.Size = 0;
            par.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(par);
            par = cmd.CreateParameter();
            par.ParameterName = "ERRUPD_USER_SETTINGS";
            par.OracleType = OracleType.NVarChar;
            par.Size = 4000;
            par.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par);
            for (int i = 0; i < dataGridViewSettings.Rows.Count; i++)
            {
                cmd.Parameters["in_IDN"].Value = dataGridViewSettings.Rows[i].Cells["ColumnIDNS"].Value.ToString();
                if ((bool)dataGridViewSettings.Rows[i].Cells["ColumnValue"].Value == true)
                {
                    cmd.Parameters["in_VAL"].Value = "Y";
                }
                else
                {
                    cmd.Parameters["in_VAL"].Value = "N";
                }

                if (!Functions.ExecuteNonQuery(cmd))
                {
                    error = true;
                    break;
                }
            }
            cmd.Parameters.Clear();

            cmd.CommandType = CommandType.Text;
            string[] gProc = new string[0];
            string[] rProc = new string[0];
            string[] Columns = { "SLCTTBLS", "SLCTPROC" };
            if (!TablesAndProc(true, Columns))
            {
                error = true;
            }
            string[] gTables = Tables;
            gProc = gProc.Concat(Proc).ToArray();
            if (!TablesAndProc(false, Columns))
            {
                error = true;
            }
            string[] rTables = Tables;
            rProc = rProc.Concat(Proc).ToArray();
            rTables = rTables.Except(gTables).ToArray();
            string[] Commands = new string[0];
            for (int u = 0; u < gTables.Count(); u++)
            {
                gTables[u] = "GRANT SELECT ON " + gTables[u] + " TO " + UserName;
            }
            Commands = Commands.Concat(gTables).ToArray();
            for (int n = 0; n < rTables.Count(); n++)
            {
                rTables[n] = "REVOKE SELECT ON " + rTables[n] + " FROM " + UserName;
            }
            Commands = Commands.Concat(rTables).ToArray();


            Columns[0] = "INSTBLS";
            Columns[1] = "INSPROC";
            if (!TablesAndProc(true, Columns))
            {
                error = true;
            }
            gTables = Tables;
            gProc = gProc.Concat(Proc).ToArray();
            if (!TablesAndProc(false, Columns))
            {
                error = true;
            }
            rTables = Tables;
            rProc = rProc.Concat(Proc).ToArray();
            rTables = rTables.Except(gTables).ToArray();
            for (int u = 0; u < gTables.Count(); u++)
            {
                gTables[u] = "GRANT INSERT ON " + gTables[u] + " TO " + UserName;
            }
            Commands = Commands.Concat(gTables).ToArray();
            for (int n = 0; n < rTables.Count(); n++)
            {
                rTables[n] = "REVOKE INSERT ON " + rTables[n] + " FROM " + UserName;
            }
            Commands = Commands.Concat(rTables).ToArray();
            Columns[0] = "UPDTBLS";
            Columns[1] = "UPDPROC";
            if (!TablesAndProc(true, Columns))
            {
                error = true;
            }
            gTables = Tables;
            gProc = gProc.Concat(Proc).ToArray();
            if (!TablesAndProc(false, Columns))
            {
                error = true;
            }
            rTables = Tables;
            rProc = rProc.Concat(Proc).ToArray();
            rTables = rTables.Except(gTables).ToArray();
            for (int u = 0; u < gTables.Count(); u++)
            {
                gTables[u] = "GRANT UPDATE ON " + gTables[u] + " TO " + UserName;
            }
            Commands = Commands.Concat(gTables).ToArray();
            for (int n = 0; n < rTables.Count(); n++)
            {
                rTables[n] = "REVOKE UPDATE ON " + rTables[n] + " FROM " + UserName;
            }
            Commands = Commands.Concat(rTables).ToArray();
            Columns[0] = "DROPTBLS";
            Columns[1] = "DROPPROC";
            if (!TablesAndProc(true, Columns))
            {
                error = true;
            }
            gTables = Tables;
            gProc = gProc.Concat(Proc).ToArray();
            if (!TablesAndProc(false, Columns))
            {
                error = true;
            }
            rTables = Tables;
            rProc = rProc.Concat(Proc).ToArray();
            rTables = rTables.Except(gTables).ToArray();
            for (int u = 0; u < gTables.Count(); u++)
            {
                gTables[u] = "GRANT DELETE ON " + gTables[u] + " TO " + UserName;
            }
            Commands = Commands.Concat(gTables).ToArray();
            for (int n = 0; n < rTables.Count(); n++)
            {
                rTables[n] = "REVOKE DELETE ON " + rTables[n] + " FROM " + UserName;
            }
            Commands = Commands.Concat(rTables).ToArray();
            gProc = gProc.Distinct().ToArray();
            rProc = rProc.Distinct().ToArray();
            rProc = rProc.Except(gProc).ToArray();
            for (int u = 0; u < gProc.Count(); u++)
            {
                gProc[u] = "GRANT EXECUTE ON " + gProc[u] + " TO " + UserName;
            }
            Commands = Commands.Concat(gProc).ToArray();
            for (int n = 0; n < rProc.Count(); n++)
            {
                rProc[n] = "REVOKE UPDATE ON " + rProc[n] + " FROM " + UserName;
            }
            Commands = Commands.Concat(rProc).ToArray();
            foreach (string line in Commands)
            {
                cmd.CommandText = line;
            START:
                try
                {
                    if (Login.OpenConnection(cmd.Connection))
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                    else
                    {
                        error = true;
                        break;
                    }
                }
                catch (OracleException oe)
                {
                    if (oe.Code != 1927)
                    {
                        if (MessageBox.Show(oe.Message, "", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                        {
                            goto START;
                        }
                        else
                        {
                            error = true;
                            break;
                        }
                    }
                }
            }
            if (error)
            {
                MessageBox.Show("Не все разрешения были предоставлены", "Ошибка");
            }
            else
            {
                MessageBox.Show("Разрешения предоставлены", "Сообщение");
                this.Dispose();
            }
        }

        private bool TablesAndProc(bool EqualNot, string[] Columns)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            if (EqualNot == true)
            {
                cmd.CommandText = "SELECT m." + Columns[0] + ", m." + Columns[1] + " FROM LONDA.USER_RIGHTS r, LONDA.MENUS m WHERE r.MENU = m.IDN AND GSELECT = 'Y'";
            }
            else
            {
                cmd.CommandText = "SELECT m." + Columns[0] + ", m." + Columns[1] + " FROM LONDA.USER_RIGHTS r, LONDA.MENUS m WHERE r.MENU = m.IDN AND GSELECT <> 'Y'";
            }
            DataTable Data = Functions.GetData(cmd);

            if (Data != null)
            {
                Tables = new string[0];
                Proc = new string[0];
                string[] separator = { ", " };
                string tmp;
                for (int z = 0; z < Data.Rows.Count; z++)
                {
                    tmp = Data.Rows[z].ItemArray[0].ToString();
                    Tables = Tables.Concat(tmp.Split(separator, StringSplitOptions.RemoveEmptyEntries)).ToArray();
                    tmp = Data.Rows[z].ItemArray[1].ToString();
                    Proc = Proc.Concat(tmp.Split(separator, StringSplitOptions.RemoveEmptyEntries)).ToArray();
                }
                Tables = Tables.Distinct().ToArray();
                Proc = Proc.Distinct().ToArray();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
