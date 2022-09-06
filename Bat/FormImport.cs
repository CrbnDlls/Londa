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

namespace Bat
{
    public partial class FormImport : Form
    {
        OracleConnection conn;
        OracleCommand cmd;

        public FormImport(OracleCommand importCmd, OracleConnection baseConn)
        {
            InitializeComponent();
            conn = baseConn;
            cmd = importCmd;

        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialogImport.ShowDialog() == DialogResult.OK)
            {
                Excel ex = new Excel();
                System.Globalization.CultureInfo info = System.Globalization.CultureInfo.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                ex.OpenDocument(openFileDialogImport.FileName);
                int x = 0;
                string _Range = "A" + (x + 1).ToString();
                
                string[] timelst = new string[1];
                timelst[0] = "start";
                while (ex.GetValue(_Range) != null)
                {
                    dataGridViewImportTable.Rows.Add();
                    
                    dataGridViewImportTable.Rows[x].Cells["Number"].Value = (x + 1).ToString();
                    dataGridViewImportTable.Rows[x].Cells["IDN"].Value = ex.GetValue(_Range);
                    _Range = "B" + (x + 1).ToString();
                    dataGridViewImportTable.Rows[x].Cells["FNAME"].Value = ex.GetValue(_Range);
                    _Range = "C" + (x + 1).ToString();
                    dataGridViewImportTable.Rows[x].Cells["FSTNAME"].Value = ex.GetValue(_Range);
                    _Range = "D" + (x + 1).ToString();
                    dataGridViewImportTable.Rows[x].Cells["FATHNAME"].Value = ex.GetValue(_Range);
                    _Range = "E" + (x + 1).ToString();
                    dataGridViewImportTable.Rows[x].Cells["OLDNAME"].Value = ex.GetValue(_Range);
                    _Range = "G" + (x + 1).ToString();
                    string value = ex.GetValue(_Range);
                    switch (value)
                    {
                        case "Перукар":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 2;
                            break;
                        case "Перукар ":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 2;
                            break;
                        case "Майстер манікюра":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 3;
                            break;
                        case "Майтер манікюра":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 3;
                            break;
                        case "майтер манікюра":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 3;
                            break;
                        case "Майстер манікюру":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 3;
                            break;
                        case "майстер манікюра":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 3;
                            break;
                        case "манiкюрниця":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 3;
                            break;
                        case "майстер з манiкюру та пед":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 3;
                            break;
                        case "майстер з манiкуру":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 3;
                            break;
                        case "Косметолог":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 4;
                            break;
                        case "Адмiнiстратор":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 9;
                            break;
                        case "Адміністратор салона":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 9;
                            break;
                        case " Адміністратор салона":
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = 9;
                            break;
                        default:
                            dataGridViewImportTable.Rows[x].Cells["PROF"].Value = value;
                            break;
                    }
                    
                    _Range = "F" + (x + 1).ToString();
                    dataGridViewImportTable.Rows[x].Cells["BIRTHDAY"].Value = ex.GetValue(_Range);
                    _Range = "I" + (x + 1).ToString();
                    dataGridViewImportTable.Rows[x].Cells["HIREDATE"].Value = ex.GetValue(_Range);
                    _Range = "H" + (x + 1).ToString();
                    value = ex.GetValue(_Range);
                    switch (value)
                    {
                        case "стиль майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 8;
                            break;
                        case "Vip-майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 1;
                            break;
                        case "авторський майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 3;
                            break;
                        case "майстер високої кваліфікації":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 2;
                            break;
                        case "Гранд-майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 4;
                            break;
                        case "TOP-майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 7;
                            break;
                        case "майтер року":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 6;
                            break;
                        case "майстер року":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 6;
                            break;
                        case "Слиль-директор":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 5;
                            break;
                        case "R стиль майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 8;
                            break;
                        case "R стиль-майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 8;
                            break;
                        case "R-авторський майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 3;
                            break;
                        case "R Vip-майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 1;
                            break;
                        case "R-автор майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 3;
                            break;
                        case "R-стиль майстер":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 8;
                            break;
                        case "R-майстер року":
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 6;
                            break;
                        case null:
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = 0;
                            break;
                        default:
                            dataGridViewImportTable.Rows[x].Cells["STAGE"].Value = value;
                            break;
                    }
                    
                    _Range = "J" + (x + 1).ToString();
                    value = ex.GetValue(_Range);
                    switch (value)
                    {
                        case "КПИ":
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 3;
                            break;
                        case "Дарниця":
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 9;
                            break;
                        case "Мінська":
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 2;
                            break;
                        case "Пестеля":
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 4;
                            break;
                        case "Святошин":
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 5;
                            break;
                        case "Лісова":
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 7;
                            break;
                        case "Подол":
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 1;
                            break;
                        case "Березняки":
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 8;
                            break;
                        case "Шулявка":
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 6;
                            break;
                        case "Райдужний":
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 10;
                            break;
                        case null:
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = 0;
                            break;
                        default:
                            dataGridViewImportTable.Rows[x].Cells["MAINSALON"].Value = value;
                            break;
                    }
                                     
                    
                    x = x + 1;
                    _Range = "A" + (x + 1).ToString();

                }

                
                ex.CloseDocument();
                System.Threading.Thread.CurrentThread.CurrentCulture = info;
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            //cmd.Connection = conn;
            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandType = CommandType.Text;
            string IDN1, FNAME1, FSTNAME1, FATHNAME1, OLDNAME1, PROF1, STAGE1, BIRHDAY1, HIREDATE1, MAINSALON1;
            for (int i = 1; i < dataGridViewImportTable.Rows.Count; i++)
            {
                IDN1 = dataGridViewImportTable.Rows[i].Cells["IDN"].Value.ToString();
                FNAME1 = dataGridViewImportTable.Rows[i].Cells["FNAME"].Value.ToString();
                FSTNAME1 = dataGridViewImportTable.Rows[i].Cells["FSTNAME"].Value.ToString();
                FATHNAME1 = dataGridViewImportTable.Rows[i].Cells["FATHNAME"].Value.ToString();
                if (dataGridViewImportTable.Rows[i].Cells["OLDNAME"].Value != null)
                {
                    OLDNAME1 = "'" + dataGridViewImportTable.Rows[i].Cells["OLDNAME"].Value.ToString() + "'";
                }
                else
                {
                    OLDNAME1 = "NULL";
                }
                PROF1 = dataGridViewImportTable.Rows[i].Cells["PROF"].Value.ToString();
                if (dataGridViewImportTable.Rows[i].Cells["PROF"].Value != null)
                {
                    STAGE1 = dataGridViewImportTable.Rows[i].Cells["STAGE"].Value.ToString();
                }
                else
                {
                    STAGE1 = "0";
                }
                if (dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value != null && (dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Length == 20 | dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Length == 21 | dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Length == 22))
                {
                    int lenth = dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().IndexOf("/", 0);
                    string Month = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Substring(0, lenth));
                    int start = lenth + 1;
                    lenth = dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().IndexOf("/", start) - start;
                    string Day = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Substring(start, lenth));
                    string Year = dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Substring(start + lenth + 1, 4);
                    BIRHDAY1 = "TO_DATE('" + Day + "/" + Month + "/" + Year + "','dd/mm/yyyy')";
                }
                else
                {
                    BIRHDAY1 = "NULL";// dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value;
                }
                if (dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value != null && (dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Length == 20 | dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Length == 21 | dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Length == 22))
                {
                    int lenth = dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().IndexOf("/", 0);
                    string Month = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Substring(0, lenth));
                    int start = lenth + 1;
                    lenth = dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().IndexOf("/", start) - start;
                    string Day = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Substring(start, lenth));
                    string Year = dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Substring(start + lenth + 1, 4);
                    HIREDATE1 = "TO_DATE('" + Day + "/" + Month + "/" + Year + "','dd/mm/yyyy')";
                }
                else
                {
                    HIREDATE1 = "NULL";
                }
                MAINSALON1 = dataGridViewImportTable.Rows[i].Cells["MAINSALON"].Value.ToString();

                if (IDN1 != "0")
                {
                    cmd2.CommandText = "UPDATE STAFF SET STAGE = " + STAGE1 + ", HIREDATE = " + HIREDATE1 + ", MAINSALON = " + MAINSALON1 + " WHERE IDN = " + IDN1;
                }
                else
                {
                    cmd2.CommandText = "INSERT INTO STAFF (IDN, FNAME, FSTNAME, FATHNAME, PRENAME, PROF, STAGE, HIREDATE, BIRTHDAY, MAINSALON) VALUES (STAFF_SEQ.NEXTVAL, '" + FNAME1 + "', '" + FSTNAME1 + "', '" + FATHNAME1 + "', " + OLDNAME1 + ", " + PROF1 + ", " + STAGE1 + ", " + HIREDATE1 + ", " + BIRHDAY1 + ", " + MAINSALON1 + ")";
                }
               
                /*
                cmd.Parameters[0].Value = dataGridViewImportTable.Rows[i].Cells["IDN"].Value.ToString();
                cmd.Parameters[1].Value = dataGridViewImportTable.Rows[i].Cells["FNAME"].Value.ToString();
                cmd.Parameters[2].Value = dataGridViewImportTable.Rows[i].Cells["FSTNAME"].Value.ToString();
                cmd.Parameters[3].Value = dataGridViewImportTable.Rows[i].Cells["FATHNAME"].Value.ToString();
                cmd.Parameters[4].Value = dataGridViewImportTable.Rows[i].Cells["OLDNAME"].Value.ToString();
                cmd.Parameters[5].Value = dataGridViewImportTable.Rows[i].Cells["PROF"].Value.ToString();
                if (dataGridViewImportTable.Rows[i].Cells["PROF"].Value != null)
                {
                    cmd.Parameters[6].Value = dataGridViewImportTable.Rows[i].Cells["STAGE"].Value.ToString();
                }
                else
                {
                    cmd.Parameters[6].Value = "0";
                }
                cmd.Parameters[7].Value = "";
                
                if (dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value != null && (dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Length == 20 | dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Length == 21 | dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Length == 22))
                {
                    int lenth = dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().IndexOf("/", 0);
                    string Month = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Substring(0, lenth));
                    int start = lenth + 1;
                    lenth = dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().IndexOf("/", start) - start;
                    string Day = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Substring(start, lenth));
                    string Year = dataGridViewImportTable.Rows[i].Cells["HIREDATE"].Value.ToString().Substring(start + lenth + 1, 4);
                    cmd.Parameters[8].Value = Day + "/" + Month + "/" + Year;
                }
                else
                {
                    cmd.Parameters[8].Value = "";
                }
                cmd.Parameters[9].Value = dataGridViewImportTable.Rows[i].Cells["MAINSALON"].Value.ToString();
                if (dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value != null && (dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Length == 20 | dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Length == 21 | dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Length == 22))
                {
                    int lenth = dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().IndexOf("/", 0);
                    string Month = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Substring(0, lenth));
                    int start = lenth + 1;
                    lenth = dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().IndexOf("/", start) - start;
                    string Day = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Substring(start, lenth));
                    string Year = dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value.ToString().Substring(start + lenth + 1, 4);
                    cmd.Parameters[10].Value = Day + "/" + Month + "/" + Year;
                }
                else
                {
                    cmd.Parameters[10].Value = "";// dataGridViewImportTable.Rows[i].Cells["BIRTHDAY"].Value;
                }
                if (dataGridViewImportTable.Rows[i].Cells["DROPDATE"].Value != null && (dataGridViewImportTable.Rows[i].Cells["DROPDATE"].Value.ToString().Length == 20 | dataGridViewImportTable.Rows[i].Cells["DROPDATE"].Value.ToString().Length == 21 | dataGridViewImportTable.Rows[i].Cells["DROPDATE"].Value.ToString().Length == 22))
                {
                    int lenth = dataGridViewImportTable.Rows[i].Cells["DROPDATE"].Value.ToString().IndexOf("/", 0);
                    string Month = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["DROPDATE"].Value.ToString().Substring(0, lenth));
                    int start = lenth + 1;
                    lenth = dataGridViewImportTable.Rows[i].Cells["DROPDATE"].Value.ToString().IndexOf("/", start) - start;
                    string Day = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["DROPDATE"].Value.ToString().Substring(start, lenth));
                    string Year = dataGridViewImportTable.Rows[i].Cells["DROPDATE"].Value.ToString().Substring(start + lenth + 1, 4);
                    cmd.Parameters[11].Value = Day + "/" + Month + "/" + Year;
                }
                else
                {
                    cmd.Parameters[11].Value = "";//dataGridViewImportTable.Rows[i].Cells["DROPDATE"].Value;
                }
                
                cmd.Parameters[12].Value = dataGridViewImportTable.Rows[i].Cells["FORMALIZED"].Value.ToString();
                
                if (dataGridViewImportTable.Rows[i].Cells["FORMALDATE"].Value != null && (dataGridViewImportTable.Rows[i].Cells["FORMALDATE"].Value.ToString().Length == 20 | dataGridViewImportTable.Rows[i].Cells["FORMALDATE"].Value.ToString().Length == 21 | dataGridViewImportTable.Rows[i].Cells["FORMALDATE"].Value.ToString().Length == 22))
                {
                    
                    int lenth = dataGridViewImportTable.Rows[i].Cells["FORMALDATE"].Value.ToString().IndexOf("/", 0);
                    string Month = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["FORMALDATE"].Value.ToString().Substring(0, lenth));
                    int start = lenth + 1;
                    lenth = dataGridViewImportTable.Rows[i].Cells["FORMALDATE"].Value.ToString().IndexOf("/", start) - start;
                    string Day = Functions.AddZero(dataGridViewImportTable.Rows[i].Cells["FORMALDATE"].Value.ToString().Substring(start, lenth));
                    string Year = dataGridViewImportTable.Rows[i].Cells["FORMALDATE"].Value.ToString().Substring(start + lenth + 1, 4);
                    cmd.Parameters[12].Value = Day + "/" + Month + "/" + Year;
                }
                else
                {
                    cmd.Parameters[12].Value = "";// dataGridViewImportTable.Rows[i].Cells["FORMALDATE"].Value;
                }
                */
                try
                {
                    if (Login.OpenConnection(conn))
                    {
                        try
                        {
                            cmd2.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(dataGridViewImportTable.Rows[i].Cells["Number"].Value.ToString() + " |||| " + ex.Message, "Ошибка");
                        }
                    }

                }
                catch (OracleException ex)
                {
                    MessageBox.Show(dataGridViewImportTable.Rows[i].Cells["Number"].Value.ToString() + " |||| " + ex.Message, "Ошибка");
                }
            }
            conn.Close();
            MessageBox.Show("Импорт завершен");
            
        }
    }
}
