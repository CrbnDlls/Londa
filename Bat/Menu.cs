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
using System.Data.SQLite;


namespace Bat
{
    public partial class FormMenu : Form
    {
        private OracleConnection conn;
        private OracleCommand cmd;
        public FormMenu()
        {
            InitializeComponent();
            Login login = new Login("LONDA", "", null);
            //login.ShowDialog();
            conn = login.СоединениеСБазой;
            string userNum = null;
            cmd = new OracleCommand("SELECT s.FNAME, s.FSTNAME, s.FATHNAME, u.USER# FROM LONDA.STAFF s, SYS.USER$ u WHERE u.NAME = USER AND u.USER# = s.ISUSER", conn);
            
            DataTable Data = Functions.GetData(cmd);
            if (Data != null)
            {
                this.Text = Data.Rows[0].ItemArray[0].ToString() + " " + Data.Rows[0].ItemArray[1].ToString() + " " + Data.Rows[0].ItemArray[2].ToString();
                userNum = Data.Rows[0].ItemArray[3].ToString();
                Data.Dispose();
            }
            else
            {
                MessageBox.Show("Невозможно определить пользователя, программа завершает работу.", "Потеряно соединение с базой", MessageBoxButtons.OK);
                Environment.Exit(0);
            }
            if (Int32.Parse(userNum) == 90)
            {
                cmd.CommandText = "SELECT TOPNAME, MIDDLENAME, BOTTOMNAME FROM LONDA.MENUS";
            }
            else
            {
                cmd.CommandText = "SELECT m.TOPNAME, m.MIDDLENAME, m.BOTTOMNAME FROM LONDA.MENUS m, LONDA.USER_RIGHTS r WHERE r.MENU = m.IDN AND r.GSELECT = 'Y' AND r.LONDAUSER = " + userNum;
            }

            DataTable menuTable = Functions.GetData(cmd);
            if (menuTable != null)
            {
                ToolStripMenuItem menuTopItem, menuItem, menuBottomItem;
                for (int i = 0; i < menuTable.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        menuTopItem = new ToolStripMenuItem(menuTable.Rows[i].ItemArray[0].ToString());
                        menuTopItem.Name = menuTable.Rows[i].ItemArray[0].ToString();
                        if (menuTable.Rows[i].ItemArray[1].ToString() == "")
                        {
                            menuTopItem.Click += new EventHandler(menuItem_Click);
                        }
                        else
                        {
                            menuItem = new ToolStripMenuItem(menuTable.Rows[i].ItemArray[1].ToString());
                            menuItem.Name = menuTable.Rows[i].ItemArray[1].ToString();
                            if (menuTable.Rows[i].ItemArray[2].ToString() == "")
                            {
                                menuItem.Click += new EventHandler(menuItem_Click);
                            }
                            else
                            {
                                menuBottomItem = new ToolStripMenuItem(menuTable.Rows[i].ItemArray[2].ToString());
                                menuBottomItem.Name = menuTable.Rows[i].ItemArray[2].ToString();
                                menuBottomItem.Click += new EventHandler(menuItem_Click);
                                menuItem.DropDownItems.Add(menuBottomItem);
                            }
                            menuTopItem.DropDownItems.Add(menuItem);
                        }
                        menuStripMainMenu.Items.Insert(0, menuTopItem);
                    }
                    else
                    {
                        if (!menuStripMainMenu.Items.ContainsKey(menuTable.Rows[i].ItemArray[0].ToString()))
                        {
                            menuTopItem = new ToolStripMenuItem(menuTable.Rows[i].ItemArray[0].ToString());
                            menuTopItem.Name = menuTable.Rows[i].ItemArray[0].ToString();
                            if (menuTable.Rows[i].ItemArray[1].ToString() == "")
                            {
                                menuTopItem.Click += new EventHandler(menuItem_Click);
                            }
                            else
                            {
                                menuItem = new ToolStripMenuItem(menuTable.Rows[i].ItemArray[1].ToString());
                                menuItem.Name = menuTable.Rows[i].ItemArray[1].ToString();
                                if (menuTable.Rows[i].ItemArray[2].ToString() == "")
                                {
                                    menuItem.Click += new EventHandler(menuItem_Click);
                                }
                                else
                                {
                                    menuBottomItem = new ToolStripMenuItem(menuTable.Rows[i].ItemArray[2].ToString());
                                    menuBottomItem.Name = menuTable.Rows[i].ItemArray[2].ToString();
                                    menuBottomItem.Click += new EventHandler(menuItem_Click);
                                    menuItem.DropDownItems.Add(menuBottomItem);
                                }
                                menuTopItem.DropDownItems.Add(menuItem);
                            }
                            menuStripMainMenu.Items.Insert(0, menuTopItem);
                        }
                        else
                        {
                            menuTopItem = (ToolStripMenuItem)menuStripMainMenu.Items[menuTable.Rows[i].ItemArray[0].ToString()];
                            if (!menuTopItem.DropDownItems.ContainsKey(menuTable.Rows[i].ItemArray[1].ToString()))
                            {
                                menuItem = new ToolStripMenuItem(menuTable.Rows[i].ItemArray[1].ToString());
                                menuItem.Name = menuTable.Rows[i].ItemArray[1].ToString();
                                if (menuTable.Rows[i].ItemArray[2].ToString() == "")
                                {
                                    menuItem.Click += new EventHandler(menuItem_Click);
                                }
                                else
                                {
                                    menuBottomItem = new ToolStripMenuItem(menuTable.Rows[i].ItemArray[2].ToString());
                                    menuBottomItem.Name = menuTable.Rows[i].ItemArray[2].ToString();
                                    menuBottomItem.Click += new EventHandler(menuItem_Click);
                                    menuItem.DropDownItems.Add(menuBottomItem);
                                }
                                menuTopItem.DropDownItems.Add(menuItem);
                            }
                            else
                            {
                                menuItem = (ToolStripMenuItem)menuTopItem.DropDownItems[menuTable.Rows[i].ItemArray[1].ToString()];
                                menuBottomItem = new ToolStripMenuItem(menuTable.Rows[i].ItemArray[2].ToString());
                                menuBottomItem.Name = menuTable.Rows[i].ItemArray[2].ToString();
                                menuBottomItem.Click += new EventHandler(menuItem_Click);
                                menuItem.DropDownItems.Add(menuBottomItem);
                            }
                        }

                    }

                }
                menuTable.Dispose();
            }
            else
            {
                MessageBox.Show("Невозможно загрузить меню, программа завершает работу.", "Потеряно соединение с базой", MessageBoxButtons.OK);
                Environment.Exit(0);
            }

            this.WindowState = FormWindowState.Maximized;
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            bool FormExists = false;
            foreach (Form activeForm in MdiChildren)
            {
                if (activeForm.Text == menuItem.Name)
                {
                    activeForm.BringToFront();
                    FormExists = true;
                    break;
                }
            }
            if (!FormExists)
            {
                switch (menuItem.Name)
                {
                    case "Добавить рабочий лист"://0
                        FormWorkSheetSmall workSheet = new FormWorkSheetSmall(conn);
                        workSheet.MdiParent = this;
                        workSheet.Show();
                        break;
                    case "Список сотрудников"://1
                        #region Кнопка Список сотрудников
                        DataTable commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        DataRow row = commandTbl.NewRow();
                        row[0] = "Фамилия";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Имя";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Отчество";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "ID в программе";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Девичья фамилия";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Должность";
                        row[1] = "combobox SELECT IDN, PROF FROM LONDA.PROFESSIONS";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Звание";
                        row[1] = "combobox SELECT IDN, STAGE FROM LONDA.STAGES";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Дата присвоения";
                        row[1] = "datepicker";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Дата приема";
                        row[1] = "datepicker";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Основной салон";
                        row[1] = "combobox SELECT IDN, SNAME FROM LONDA.SALONS";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "День рождения";
                        row[1] = "datepicker";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Дата Увольнения";
                        row[1] = "datepicker";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Оформлен";
                        row[1] = "combobox SELECT IDN, CNAME FROM LONDA.COMPANY";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Дата Оформления";
                        row[1] = "datepicker";
                        commandTbl.Rows.Add(row);
                        DataTable filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Фамилия";
                        row[1] = "s.FNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Имя";
                        row[1] = "s.FSTNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Отчество";
                        row[1] = "s.FATHNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "ID в программе";
                        row[1] = "s.STAFFID";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Девичья фамилия";
                        row[1] = "s.PRENAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Должность";
                        row[1] = "p.PROF";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Звание";
                        row[1] = "a.STAGE";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Дата присвоения";
                        row[1] = "s.STAGEDATE";
                        row[2] = "date";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Дата приема";
                        row[1] = "s.HIREDATE";
                        row[2] = "date";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Основной салон";
                        row[1] = "l.SNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "День рождения";
                        row[1] = "s.BIRTHDAY";
                        row[2] = "date";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Дата Увольнения";
                        row[1] = "s.DROPDATE";
                        row[2] = "date";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Оформлен";
                        row[1] = "c.CNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Дата Оформления";
                        row[1] = "s.FORMALDATE";
                        row[2] = "date";
                        filterTbl.Rows.Add(row);
                        //string[] colName = { "№", "Фамилия", "Имя", "Отчество", "Девичья фамилия", "Профессия", "Звание", "Дата присвоения", "Дата приема", "День рождения", "Дата Увольнения", "Оформлен", "Дата Оформления" };
                        string command = "SELECT s.IDN, s.FNAME, s.FSTNAME, s.FATHNAME, s.STAFFID, s.PRENAME, p.PROF, a.STAGE, to_char(s.STAGEDATE,'dd/mm/yyyy'), to_char(s.HIREDATE,'dd/mm/yyyy'), l.SNAME, to_char(s.BIRTHDAY,'dd/mm/yyyy'), to_char(s.DROPDATE,'dd/mm/yyyy'), c.CNAME, to_char(s.FORMALDATE,'dd/mm/yyyy') FROM LONDA.STAFF s, LONDA.PROFESSIONS p, LONDA.STAGES a, LONDA.COMPANY c, LONDA.SALONS l WHERE s.PROF = p.IDN AND s.STAGE = a.IDN AND s.FORMALIZED = c.IDN AND s.MAINSALON = l.IDN"; // 
                        OracleCommand delCommand = new OracleCommand("DELETE FROM LONDA.STAFF WHERE IDN = ");
                        OracleCommand cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        OracleParameter par;
                        DataTable sortTbl = new DataTable();
                        sortTbl.Columns.Add("OraCol", typeof(string));          //0 
                        sortTbl.Columns.Add("Direction", typeof(string));       //1
                        row = sortTbl.NewRow();
                        row[0] = "S.FNAME";
                        row[1] = "ASC";
                        sortTbl.Rows.Add(row);
                        cmd.CommandText = "LONDA.INSSTAFF";
                        string[] parameters = { "in_IDN",              
                                      "in_FNAME",           
                                      "in_FSTNAME",          
                                      "in_FATHNAME",
                                      "in_STAFFID",
                                      "in_PRENAME",          
                                      "in_PROF",             
                                      "in_STAGE",            
                                      "in_STAGEDATE",       
                                      "in_HIREDATE",
                                      "in_MAINSALON",
                                      "in_BIRTHDAY",        
                                      "in_DROPDATE",         
                                      "in_FORMALIZED",      
                                      "in_FORMALDATE" };
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
                        par.ParameterName = "ERRSTAFF";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        FormTable staff = new FormTable(conn, commandTbl, command, delCommand, "Список сотрудников", cmd, sortTbl, filterTbl, null, "staffList", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Должности"://2
                        #region Кнопка Должности
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Должность";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Выписывать рабочий лист";
                        row[1] = "combobox SELECT IDN, PERMISSION FROM LONDA.PERMISSIONS";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Должность";
                        row[1] = "p.PROF";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Должность";
                        row[1] = "e.PERMISSION";
                        filterTbl.Rows.Add(row);
                        command = "SELECT p.IDN, p.PROF, e.PERMISSION FROM LONDA.PROFESSIONS p, LONDA.PERMISSIONS e WHERE p.IDN <> 0 AND p.ALWRKSH = e.IDN";
                        delCommand = new OracleCommand("DELETE FROM LONDA.PROFESSIONS WHERE IDN = ");
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.CommandText = "LONDA.INSPROF";
                        string[] parameters1 = { "in_IDN",              
                                            "in_PROF",
                                           "in_ALWRKSH"};
                        foreach (string line in parameters1)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRPROF";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, delCommand, "Должности", cmd, null, filterTbl, null, "default", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Звания"://3
                        #region Кнопка Звания
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Звание";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Звание";
                        row[1] = "STAGE";
                        filterTbl.Rows.Add(row);
                        command = "SELECT IDN, STAGE FROM LONDA.STAGES WHERE IDN <> 0";
                        delCommand = new OracleCommand("DELETE FROM LONDA.STAGES WHERE IDN = ");
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.CommandText = "LONDA.INSSTAGES";
                        string[] parameters2 = { "in_IDN",              
                                      "in_STAGE"};
                        foreach (string line in parameters2)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRSTAGE";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, delCommand, "Звания", cmd, null, filterTbl, null, "default", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Список услуг"://4
                        #region Кнопка Cписок услуг
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Наименование услуги";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Объем услуги";
                        row[1] = "combobox SELECT IDN, VNAME FROM LONDA.SERV_VOL";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Раздел прейскуранта";
                        row[1] = "combobox SELECT IDN, UNAME FROM LONDA.PRICE_UNITS WHERE IDN <> 0";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Наименование услуги";
                        row[1] = "s.SERV_NAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Объем услуги";
                        row[1] = "v.VNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Раздел прейскуранта";
                        row[1] = "p.UNAME";
                        filterTbl.Rows.Add(row);
                        command = "SELECT s.IDN, s.SERV_NAME, v.VNAME, p.UNAME FROM LONDA.SERV_INFO s, LONDA.PRICE_UNITS p, LONDA.SERV_VOL v WHERE s.UNIT = p.IDN AND s.VOL = v.IDN";
                        delCommand = new OracleCommand("DELETE FROM LONDA.SERV_INFO WHERE IDN = ");
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.CommandText = "LONDA.INSSERV_INFO";
                        string[] parameters3 = { "in_IDN",              
                                      "in_SERV_NAME",
                                            "in_VOL",
                                           "in_UNIT"};
                        foreach (string line in parameters3)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRSERV_NAME";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, delCommand, "Список услуг", cmd, null, filterTbl, null, "default", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Кода кассы"://5
                        #region Кнопка Кода кассы
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Код кассы";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Услуга";
                        row[1] = "combobox SELECT i.IDN, i.SERV_NAME || ' | ' || v.VNAME || ' | ' || p.UNAME FROM LONDA.SERV_INFO i, LONDA.PRICE_UNITS p, LONDA.SERV_VOL v WHERE p.IDN = i.UNIT AND v.IDN = i.VOL";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Цена грн.";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Цена 10% грн.";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Цена 50% грн.";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Цена сотрудника грн.";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Код кассы";
                        row[1] = "c.CODE";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Имя услуги";
                        row[1] = "s.SERV_NAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Раздел прейскуранта";
                        row[1] = "p.UNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Объем услуги";
                        row[1] = "v.VNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Цена грн.";
                        row[1] = "c.PRICE";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Цена 10% грн.";
                        row[1] = "c.PRICE10";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Цена 50% грн.";
                        row[1] = "c.PRICE50";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Цена сотрудника грн.";
                        row[1] = "c.PRICESTAFF";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        command = "SELECT c.IDN, c.CODE, s.SERV_NAME || ' | ' || v.VNAME || ' | ' || p.UNAME, c.PRICE, c.PRICE10, c.PRICE50, c.PRICESTAFF FROM LONDA.CASH_REG_CODE c, LONDA.SERV_INFO s, LONDA.SERV_VOL v, LONDA.PRICE_UNITS p WHERE c.SERVICE = s.IDN AND s.UNIT = p.IDN AND s.VOL = v.IDN";
                        delCommand = new OracleCommand("DELETE FROM LONDA.CASH_REG_CODE WHERE IDN = ");
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.CommandText = "LONDA.INSCASH_REG_CODE";
                        string[] parameters4 = { "in_IDN",              
                                      "in_CODE",
                                      "in_SERVICE",
                                           "in_PRICE",
                                           "in_PRICE10",
                                               "in_PRICE50",
                                               "in_PRICESTAFF"};
                        foreach (string line in parameters4)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRCASH_REG_CODE";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, delCommand, "Кода кассы", cmd, null, filterTbl, null, "CASH_REG_CODE", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Схемы списания"://6
                        #region Кнопка Схемы списания
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Услуга";
                        row[1] = "combobox SELECT IDN, SERV_NAME FROM LONDA.SERV_INFO";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Продукция";
                        row[1] = "combobox SELECT IDN, PROD_NAME FROM LONDA.PROD_INFO";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Количество (в мл., гр., шт.)";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Услуга";
                        row[1] = "s.SERV_NAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Продукция";
                        row[1] = "pi.PROD_NAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Количество (в мл., гр., шт.)";
                        row[1] = "w.QUANTITY";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        command = "SELECT w.IDN, s.SERV_NAME, pi.PROD_NAME, w.QUANTITY FROM LONDA.WRITE_OFF_PLAN w, LONDA.PROD_INFO pi, LONDA.SERV_INFO s WHERE w.SERVICE = s.IDN AND w.PROD = pi.IDN";
                        delCommand = new OracleCommand("DELETE FROM LONDA.WRITE_OFF_PLAN WHERE IDN = ");
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.CommandText = "LONDA.INSWRITE_OFF_PLAN";
                        string[] parameters5 = { "in_IDN",
                                             "in_SERVICE",
                                             "in_PROD",
                                             "in_QUANTITY"};
                        foreach (string line in parameters5)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRWRITE_OFF_PLAN";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, delCommand, "Схемы списания", cmd, null, filterTbl, null, "default", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Описание товаров"://7
                        #region Кнопка Описание товаров
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Наименование товара";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Цена грн.";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Объем, вес(в мл., гр., шт.)";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Наименование товара";
                        row[1] = "PROD_NAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Цена грн.";
                        row[1] = "PRICE";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Объем, вес(в мл., гр., шт.)";
                        row[1] = "CAPACITY";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        command = "SELECT IDN, PROD_NAME, PRICE, CAPACITY FROM LONDA.PROD_INFO WHERE IDN <> 0";
                        delCommand = new OracleCommand("DELETE FROM LONDA.PROD_INFO WHERE IDN = ");
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.CommandText = "LONDA.INSPROD_INFO";
                        string[] parameters6 = { "in_IDN",
                                             "in_PROD_NAME",
                                             "in_PRICE",
                                             "in_CAPACITY"};
                        foreach (string line in parameters6)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRPROD_INFO";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, delCommand, "Описание товаров", cmd, null, filterTbl, null, "default", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Пользователи БД"://8
                        #region Кнопка Пользователи БД
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Сотрудник";
                        row[1] = "combobox SELECT IDN, FNAME || ' ' || FSTNAME || ' ' || FATHNAME FROM LONDA.STAFF";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Имя пользователя";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Пароль";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Фамилия";
                        row[1] = "s.FNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Имя";
                        row[1] = "s.FSTNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Отчество";
                        row[1] = "s.FATHNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Имя пользователя";
                        row[1] = "u.NAME";
                        filterTbl.Rows.Add(row);
                        command = "SELECT s.IDN, s.FNAME || ' ' || s.FSTNAME || ' ' || s.FATHNAME, u.NAME FROM LONDA.STAFF s, SYS.USER$ u WHERE s.ISUSER = u.USER#";
                        delCommand = new OracleCommand();
                        delCommand.CommandType = CommandType.StoredProcedure;
                        delCommand.CommandText = "LONDA.DEL_USER";
                        par = delCommand.CreateParameter();
                        par.ParameterName = "in_IDN";
                        par.OracleType = OracleType.VarChar;
                        par.Size = 0;
                        par.Direction = ParameterDirection.Input;
                        delCommand.Parameters.Add(par);
                        par = delCommand.CreateParameter();
                        par.ParameterName = "ERRDEL_USER";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        delCommand.Parameters.Add(par);
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "LONDA.CREATE_USER";
                        string[] parameters7 = { "in_NEWUSER",
                                             "in_IDN",
                                             "in_USER_NAME",
                                             "in_PASSWORD"};
                        foreach (string line in parameters7)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRCREATE_USER";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);
                        FormTable users = new FormTable(conn, commandTbl, command, delCommand, "Пользователи БД", cmd, null, filterTbl, null, "UserRights", null);
                        users.MdiParent = this;
                        users.Show();
                        #endregion
                        break;
                    case "Список рабочих листов"://9
                        #region Кнопка Список рабочих листов
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Номер листа";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Салон";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Дата";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Сотрудник";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Время начала листа";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Время окончания листа";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Сумма";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Номер листа";
                        row[1] = "w.SHEETNUM";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Салон";
                        row[1] = "s.SNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Дата";
                        row[1] = "w.DAT";
                        row[2] = "date";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Фамилия";
                        row[1] = "t.FNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Имя";
                        row[1] = "t.FSTNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Отчество";
                        row[1] = "t.FATHNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Время начала листа";
                        row[1] = "w.SDATE";
                        row[2] = "time";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Время окончания листа";
                        row[1] = "w.FDATE";
                        row[2] = "time";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Сумма";
                        row[1] = "d.SUM(PRICE)";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        command = "SELECT w.IDN, w.SHEETNUM, s.SNAME, to_char(w.DAT,'dd/mm/yyyy'), t.FNAME || ' ' || t.FSTNAME || ' ' || t.FATHNAME, to_char(w.SDATE,'HH24:MI'), to_char(w.FDATE,'HH24:MI'), SUM(d.PRICE) FROM LONDA.WORKSHEETS w, LONDA.WORKSHEETSDATA d, LONDA.STAFF t, LONDA.SALONS s, LONDA.CASH_REG_CODE r WHERE t.IDN = w.WORKER AND s.IDN = w.SALON AND d.SCODE = r.IDN AND w.IDN = d.WORKSHEET AND w.IDN <> 0 AND d.MISTAKES <> 1";
                        string GroupBy = " GROUP BY w.IDN, s.SNAME, w.DAT, t.FNAME, t.FSTNAME, t.FATHNAME, w.SHEETNUM, w.SDATE, w.FDATE";
                        delCommand = new OracleCommand();
                        delCommand.CommandType = CommandType.StoredProcedure;
                        delCommand.CommandText = "LONDA.DEL_WORKSHEET";
                        par = delCommand.CreateParameter();
                        par.ParameterName = "IN_IDN";
                        par.OracleType = OracleType.VarChar;
                        par.Size = 0;
                        par.Direction = ParameterDirection.Input;
                        delCommand.Parameters.Add(par);
                        par = delCommand.CreateParameter();
                        par.ParameterName = "ERRDEL_WORKSHEET";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        delCommand.Parameters.Add(par);
                        sortTbl = new DataTable();
                        sortTbl.Columns.Add("OraCol", typeof(string));          //0 
                        sortTbl.Columns.Add("Direction", typeof(string));       //1
                        row = sortTbl.NewRow();
                        row[0] = "s.SNAME";
                        row[1] = "DESC";
                        sortTbl.Rows.Add(row);
                        row = sortTbl.NewRow();
                        row[0] = "w.DAT";
                        row[1] = "ASC";
                        sortTbl.Rows.Add(row);
                        row = sortTbl.NewRow();
                        row[0] = "t.FNAME";
                        row[1] = "DESC";
                        sortTbl.Rows.Add(row);
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "LONDA.NOTHINGTODO";
                        par = cmd.CreateParameter();
                        par.ParameterName = "MESSAGE";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);
                        string[] empty = { "", "" };
                        FormFilterConstructor fltrConstr = new FormFilterConstructor(filterTbl, null, empty, command);
                        if (fltrConstr.ShowDialog() == DialogResult.OK)
                        {
                            staff = new FormTable(conn, commandTbl, command, delCommand, "Список рабочих листов", cmd, sortTbl, filterTbl, fltrConstr.FilterTable, "WorkSheetList", GroupBy);
                            staff.MdiParent = this;
                            staff.Show();
                        }
                        #endregion
                        break;
                    case "Предприятия"://10
                        #region Кнопка Предприятия
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Наименование предприятия";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Наименование предприятия";
                        row[1] = "CNAME";
                        filterTbl.Rows.Add(row);
                        command = "SELECT IDN, CNAME FROM LONDA.COMPANY WHERE IDN <> 0";
                        delCommand = new OracleCommand("DELETE FROM LONDA.COMPANY WHERE IDN = ");
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "LONDA.INSCOMPANY";
                        string[] parameters9 = { "in_IDN",
                                             "in_CNAME" };
                        foreach (string line in parameters9)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRCNAME";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, delCommand, "Предприятия", cmd, null, filterTbl, null, "default", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Салоны"://11
                        #region Кнопка Описание товаров
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Тип";
                        row[1] = "combobox SELECT IDN, STYPE FROM LONDA.SALONTYPE WHERE IDN <> 0";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Наименование салона";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Адресс";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Телефон 1";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Телефон 2";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Состояние";
                        row[1] = "combobox SELECT IDN, STATE FROM LONDA.SALONSTATE WHERE IDN <> 0";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Тип";
                        row[1] = "st.STYPE";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Наименование салона";
                        row[1] = "s.SNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Адресс";
                        row[1] = "s.ADRSS";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Телефон 1";
                        row[1] = "s.TEL1";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Телефон 2";
                        row[1] = "s.TEL2";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Состояние";
                        row[1] = "ss.STATE";
                        filterTbl.Rows.Add(row);
                        command = "SELECT s.IDN, st.STYPE, s.SNAME, s.ADRSS, s.TEL1, s.TEL2, ss.STATE FROM LONDA.SALONS s, LONDA.SALONTYPE st, LONDA.SALONSTATE ss WHERE s.IDN <> 0 AND s.STYPE = st.IDN AND s.STATE = ss.IDN";
                        delCommand = new OracleCommand("DELETE FROM LONDA.SALONS WHERE IDN = ");
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.CommandText = "LONDA.INSSALONS";
                        string[] parameters10 = { "in_IDN",
                                                    "in_STYPE",
                                             "in_SNAME",
                                             "in_ADRSS",
                                             "in_TEL1",
                                             "in_TEL2",
                                                "in_STATE"};
                        foreach (string line in parameters10)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRSNAME";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, delCommand, "Салоны", cmd, null, filterTbl, null, "Salons", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Отделы Прейскуранта"://12
                        #region Кнопка Отделы Прейскуранта
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Наименование отделов";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Наименование отделов";
                        row[1] = "UNAME";
                        filterTbl.Rows.Add(row);
                        command = "SELECT IDN, UNAME FROM LONDA.PRICE_UNITS WHERE IDN <> 0";
                        delCommand = new OracleCommand("DELETE FROM LONDA.PRICE_UNITS WHERE IDN = ");
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.CommandText = "LONDA.INSPRICE_UNITS";
                        string[] parameters11 = { "in_IDN",              
                                      "in_UNAME"};
                        foreach (string line in parameters11)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRUNAME";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, delCommand, "Отделы Прейскуранта", cmd, null, filterTbl, null, "default", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Журнал событий"://13
                        #region Кнопка Журнал событий
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Дата";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Время";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Имя пользователя";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Сообщение";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Таблица";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Дата";
                        row[1] = "CDATE";
                        row[2] = "date";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Время";
                        row[1] = "CTIME";
                        row[2] = "time";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Имя пользователя";
                        row[1] = "USER_NAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Сообщение";
                        row[1] = "MESSAGE";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Таблица";
                        row[1] = "TABLE_NAME";
                        filterTbl.Rows.Add(row);
                        DataTable StartFilterTbl = new DataTable();
                        StartFilterTbl.Columns.Add("AndOr");
                        StartFilterTbl.Columns.Add("SkobkaOpen");
                        StartFilterTbl.Columns.Add("OraCol");
                        StartFilterTbl.Columns.Add("Equals");
                        StartFilterTbl.Columns.Add("Value");
                        StartFilterTbl.Columns.Add("SkobkaClose");
                        StartFilterTbl.Columns.Add("DataType");
                        row = StartFilterTbl.NewRow();
                        row[0] = "И";
                        row[2] = "CDATE";
                        row[3] = "равно";
                        row[4] = Functions.AddZero(DateTime.Today.Day.ToString()) + "/" + Functions.AddZero(DateTime.Today.Month.ToString()) + "/" + DateTime.Today.Year.ToString();
                        row[6] = "date";
                        StartFilterTbl.Rows.Add(row);
                        sortTbl = new DataTable();
                        sortTbl.Columns.Add("OraCol", typeof(string));          //0 
                        sortTbl.Columns.Add("Direction", typeof(string));       //1
                        row = sortTbl.NewRow();
                        row[0] = "CDATE";
                        row[1] = "DESC";
                        sortTbl.Rows.Add(row);
                        row = sortTbl.NewRow();
                        row[0] = "CTIME";
                        row[1] = "DESC";
                        sortTbl.Rows.Add(row);
                        command = "SELECT IDN, to_char(CDATE,'dd/mm/yyyy'), to_char(CTIME,'HH24:MI'), USER_NAME, MESSAGE, TABLE_NAME FROM LONDA.LONDA_LOG";
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "LONDA.NOTHINGTODO";
                        par = cmd.CreateParameter();
                        par.ParameterName = "MESSAGE";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, cmd, "Журнал событий", cmd, sortTbl, filterTbl, StartFilterTbl, "default", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Списание по рабочим листам"://14
                        #region Кнопка Списание по рабочим листам
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Салон";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Продукция";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Количество (в мл., гр., шт.)";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Сумма в грн.";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Салон";
                        row[1] = "l.SNAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Продукция";
                        row[1] = "p.PROD_NAME";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Количество (в мл., гр., шт.)";
                        row[1] = "SUM(w.QUANTITY)*SUM(r.QUANTITY)";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Сумма в грн.";
                        row[1] = "SUM(w.QUANTITY)*SUM(r.QUANTITY)/p.CAPACITY*p.PRICE";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Дата";
                        row[1] = "e.DAT";
                        row[2] = "date";
                        filterTbl.Rows.Add(row);
                        StartFilterTbl = new DataTable();
                        StartFilterTbl.Columns.Add("AndOr");
                        StartFilterTbl.Columns.Add("SkobkaOpen");
                        StartFilterTbl.Columns.Add("OraCol");
                        StartFilterTbl.Columns.Add("Equals");
                        StartFilterTbl.Columns.Add("Value");
                        StartFilterTbl.Columns.Add("SkobkaClose");
                        StartFilterTbl.Columns.Add("DataType");
                        row = StartFilterTbl.NewRow();
                        row[0] = "И";
                        row[2] = "e.DAT";
                        row[3] = "больше равно";
                        int Days = DateTime.Today.Day - 6;
                        int Month = DateTime.Today.Month;
                        int Year = DateTime.Today.Year;
                        if (Days < 1)
                        {
                            Month = Month - 1;
                            if (Month < 1)
                            {
                                Year = Year - 1;
                                Month = 12;
                                Days = DateTime.DaysInMonth(Year, Month) - Days;
                            }
                        }
                        row[4] = Days.ToString() + "/" + Month + "/" + Year;
                        StartFilterTbl.Rows.Add(row);
                        row = StartFilterTbl.NewRow();
                        row[0] = "И";
                        row[2] = "e.DAT";
                        row[3] = "меньше равно";
                        row[4] = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
                        StartFilterTbl.Rows.Add(row);

                        command = "SELECT COUNT(l.IDN), l.SNAME, p.PROD_NAME, SUM(w.QUANTITY)*SUM(r.QUANTITY), SUM(w.QUANTITY)*SUM(r.QUANTITY)/p.CAPACITY*p.PRICE FROM LONDA.WORKSHEETS e, LONDA.WORKSHEETSDATA r, LONDA.WRITE_OFF_PLAN w, LONDA.CASH_REG_CODE c, LONDA.PROD_INFO p, LONDA.SALONS l WHERE r.SCODE = c.IDN AND c.SERVICE = w.SERVICE AND w.PROD = p.IDN AND e.SALON = l.IDN AND e.IDN = r.WORKSHEET";
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "LONDA.NOTHINGTODO";
                        par = cmd.CreateParameter();
                        par.ParameterName = "MESSAGE";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);
                        GroupBy = " GROUP BY p.PROD_NAME, p.CAPACITY, p.PRICE, l.SNAME";
                        staff = new FormTable(conn, commandTbl, command, cmd, "Списание по рабочим листам", cmd, null, filterTbl, StartFilterTbl, "default", GroupBy);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Проверить рабочие листы"://15
                        #region Кнопка Списание по рабочим листам
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Номер листа";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Салон";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Дата";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Сотрудник";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Время начала листа";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Время окончания листа";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Сумма";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Номер листа";
                        row[1] = "SHEETNUM";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Салон";
                        row[1] = "SALON";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Дата";
                        row[1] = "DAT";
                        row[2] = "date";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Сотрудник";
                        row[1] = "WORKER";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Время начала листа";
                        row[1] = "SDATE";
                        row[2] = "time";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Время окончания листа";
                        row[1] = "FDATE";
                        row[2] = "time";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Сумма";
                        row[1] = "SUM(PRICE)";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        sortTbl = new DataTable();
                        sortTbl.Columns.Add("OraCol", typeof(string));          //0 
                        sortTbl.Columns.Add("Direction", typeof(string));       //1
                        row = sortTbl.NewRow();
                        row[0] = "SALON";
                        row[1] = "DESC";
                        sortTbl.Rows.Add(row);
                        row = sortTbl.NewRow();
                        row[0] = "DAT";
                        row[1] = "ASC";
                        sortTbl.Rows.Add(row);
                        row = sortTbl.NewRow();
                        row[0] = "WORKER";
                        row[1] = "DESC";
                        sortTbl.Rows.Add(row);
                        command = "SELECT IDN, SHEETNUM, SALON, to_char(DAT,'dd/mm/yyyy'), WORKER, to_char(SDATE,'HH24:MI'), to_char(FDATE,'HH24:MI'), SUMM FROM LONDA.WRKSHTOCHECK";
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "LONDA.NOTHINGTODO";
                        par = cmd.CreateParameter();
                        par.ParameterName = "MESSAGE";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);
                        delCommand = new OracleCommand();
                        delCommand.CommandType = CommandType.StoredProcedure;
                        delCommand.CommandText = "LONDA.DEL_CHCKWORKSHEET";
                        string[] parameters12 = { "in_DAT", 
                                      "in_SHEETNUM"};
                        foreach (string line in parameters12)
                        {
                            par = delCommand.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            delCommand.Parameters.Add(par);
                        }
                        par = delCommand.CreateParameter();
                        par.ParameterName = "ERRDEL_WORKSHEET";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        delCommand.Parameters.Add(par);
                        staff = new FormTable(conn, commandTbl, command, delCommand, "Проверить рабочие листы", cmd, sortTbl, filterTbl, null, "CheckWorkSheet", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Объем услуг"://16
                        #region Кнопка Объем услуг
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Объем услуг";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Объем услуг";
                        row[1] = "VNAME";
                        filterTbl.Rows.Add(row);
                        command = "SELECT IDN, VNAME FROM LONDA.SERV_VOL WHERE IDN <> 0";
                        delCommand = new OracleCommand("DELETE FROM LONDA.STAGES WHERE IDN = ");
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.CommandText = "LONDA.INSSERV_VOL";
                        string[] parameters13 = { "in_IDN",              
                                      "in_VNAME"};
                        foreach (string line in parameters13)
                        {
                            par = cmd.CreateParameter();
                            par.ParameterName = line;
                            par.OracleType = OracleType.VarChar;
                            par.Size = 0;
                            par.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(par);
                        }
                        par = cmd.CreateParameter();
                        par.ParameterName = "ERRVNAME";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);

                        staff = new FormTable(conn, commandTbl, command, delCommand, "Объем услуг", cmd, null, filterTbl, null, "default", null);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    case "Создать файлы для программы счетов"://17
                        #region Создать файлы для программы счетов
                        // генерация XML файла с салонами для программы счетов
                        
                        
                        XmlDocument resultXml = new XmlDocument();
                        string Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        File.Delete(Path + "\\lndbll.db");
                        SQLiteConnection.CreateFile(Path + "\\lndbll.db");
                        SQLiteConnection LiteConn = new SQLiteConnection("Data Source=" + Path + "\\lndbll.db");
                        
                        SQLiteCommand LiteCmd = new SQLiteCommand("CREATE TABLE salon (IDN INTEGER PRIMARY KEY, TYPE TEXT, SUB TEXT, ADR TEXT, TEL1 TEXT, TEL2 TEXT);", LiteConn);
                        LiteConn.Open();
                        LiteCmd.ExecuteNonQuery();

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
                        XmlElement lineXml;

                        FormWaiting waiting = new FormWaiting("Салоны", "SELECT s.IDN, st.STYPE, s.SNAME, s.ADRSS, s.TEL1, s.TEL2 FROM LONDA.SALONS s, LONDA.SALONTYPE st WHERE s.IDN <> 0 AND s.STYPE = st.IDN AND s.STATE = 1", new string[] { "", "" }, null, null, conn, "default", new string[] { "#", "ID", "TYPE", "SUB", "ADR", "TEL1", "TEL2" });
                        waiting.ShowDialog();
                        if (waiting.Данные != null)
                        {
                            for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                            {
                                lineXml = resultXml.CreateElement("item");
                                lineXml.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                                lineXml.SetAttribute("TYPE", waiting.Данные.Rows[i].ItemArray[2].ToString());
                                lineXml.SetAttribute("SUB", waiting.Данные.Rows[i].ItemArray[3].ToString());
                                lineXml.SetAttribute("ADR", waiting.Данные.Rows[i].ItemArray[4].ToString());
                                lineXml.SetAttribute("TEL1", waiting.Данные.Rows[i].ItemArray[5].ToString());
                                lineXml.SetAttribute("TEL2", waiting.Данные.Rows[i].ItemArray[6].ToString());
                                resultXml.DocumentElement.AppendChild(lineXml);
                                LiteCmd.CommandText = "INSERT INTO salon (IDN, TYPE, SUB, ADR, TEL1, TEL2) VALUES (" + waiting.Данные.Rows[i].ItemArray[1] + ", '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[2].ToString() + "', '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[3].ToString() + "', '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[4].ToString() + "', '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[5].ToString() + "', '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[6].ToString() + "')";
                                LiteCmd.ExecuteNonQuery();
                            }
                        }
                        waiting.Dispose();
                        resultXml.Save(Path + "\\salon.xml");
                        MessageBox.Show("Файл \"salon.xml\" сохранен на рабочий стол");
                        
                        // генерация XML файла прайс листа для программы счетов
                        LiteCmd.CommandText = "CREATE TABLE price (IDN INTEGER PRIMARY KEY, CODE INTEGER, NAME TEXT, PRICE0 TEXT, PRICE10 TEXT, PRICE50 TEXT, PRICESTAFF TEXT);";
                        LiteCmd.ExecuteNonQuery();
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

                        waiting = new FormWaiting("Прайс", "SELECT c.CODE, s.SERV_NAME || ' ' || v.VNAME, c.PRICE, c.PRICE10, c.PRICE50, c.PRICESTAFF, c.IDN FROM LONDA.CASH_REG_CODE c, LONDA.SERV_INFO s, LONDA.SERV_VOL v WHERE c.SERVICE = s.IDN AND s.VOL = v.IDN", new string[] { "", "" }, null, null, conn, "default", new string[] { "#", "ID", "NAME", "PRICE0", "PRICE10", "PRICE50", "PRICESTAFF", "IDN" });
                        waiting.ShowDialog();
                        if (waiting.Данные != null)
                        {
                            for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                            {
                                lineXml = resultXml.CreateElement("item");
                                lineXml.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                                lineXml.SetAttribute("NAME", waiting.Данные.Rows[i].ItemArray[2].ToString());
                                lineXml.SetAttribute("PRICE0", waiting.Данные.Rows[i].ItemArray[3].ToString());
                                LiteCmd.CommandText = "INSERT INTO price (IDN, CODE, NAME, PRICE0, PRICE10, PRICE50, PRICESTAFF) VALUES (" + waiting.Данные.Rows[i].ItemArray[7] + ", '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[1].ToString() + "', '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[2].ToString() + "', '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[3].ToString() + "', ";
                                if (waiting.Данные.Rows[i].ItemArray[4].ToString() != "")
                                {
                                    lineXml.SetAttribute("PRICE10", waiting.Данные.Rows[i].ItemArray[4].ToString());
                                    LiteCmd.CommandText = LiteCmd.CommandText + "'" + waiting.Данные.Rows[i].ItemArray[4].ToString() + "', ";
                                }
                                else
                                {
                                    LiteCmd.CommandText = LiteCmd.CommandText + "NULL, ";
                                }

                                if (waiting.Данные.Rows[i].ItemArray[5].ToString() != "")
                                {
                                    lineXml.SetAttribute("PRICE50", waiting.Данные.Rows[i].ItemArray[5].ToString());
                                    LiteCmd.CommandText = LiteCmd.CommandText + "'" + waiting.Данные.Rows[i].ItemArray[5].ToString() + "', ";
                                }
                                else
                                {
                                    LiteCmd.CommandText = LiteCmd.CommandText + "NULL, ";
                                }
                                if (waiting.Данные.Rows[i].ItemArray[6].ToString() != "")
                                {
                                    lineXml.SetAttribute("PRICESTAFF", waiting.Данные.Rows[i].ItemArray[6].ToString());
                                    LiteCmd.CommandText = LiteCmd.CommandText + "'" + waiting.Данные.Rows[i].ItemArray[6].ToString() + "')";
                                }
                                else
                                {
                                    LiteCmd.CommandText = LiteCmd.CommandText + "NULL)";
                                }
                                lineXml.SetAttribute("IDN", waiting.Данные.Rows[i].ItemArray[7].ToString());
                                resultXml.DocumentElement.AppendChild(lineXml);
                                //LiteCmd.ExecuteNonQuery();
                           }
                        }
                        waiting.Dispose();
                        resultXml.Save(Path + "\\price.xml");
                        MessageBox.Show("Файл \"price.xml\" сохранен на рабочий стол");

                        // генерация XML файла со списком сотрудников для программы счетов

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
                        LiteCmd.CommandText = "CREATE TABLE prof (IDN INTEGER PRIMARY KEY, NAME TEXT);";
                        LiteCmd.ExecuteNonQuery();
                        waiting = new FormWaiting("Профессии", "SELECT IDN, PROF FROM LONDA.PROFESSIONS", new string[] { "", "" }, null, null, conn, "default", new string[] { "#", "IDN", "PROF" });
                        waiting.ShowDialog();
                        if (waiting.Данные != null)
                        {
                            for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                            {
                                lineXml = resultXml.CreateElement("profession");
                                lineXml.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                                lineXml.SetAttribute("NAME", waiting.Данные.Rows[i].ItemArray[2].ToString());
                                resultXml.DocumentElement.AppendChild(lineXml);
                                LiteCmd.CommandText = "INSERT INTO prof (IDN, NAME) VALUES (" + waiting.Данные.Rows[i].ItemArray[1] + ", '" + waiting.Данные.Rows[i].ItemArray[2].ToString() + "')";
                                LiteCmd.ExecuteNonQuery();
                            }
                        }
                        waiting.Dispose();
                        LiteCmd.CommandText = "CREATE TABLE rank (IDN INTEGER PRIMARY KEY, NAME TEXT);";
                        LiteCmd.ExecuteNonQuery();
                        waiting = new FormWaiting("Звания", "SELECT IDN, STAGE FROM LONDA.STAGES", new string[] { "", "" }, null, null, conn, "default", new string[] { "#", "IDN", "STAGE" });
                        waiting.ShowDialog();
                        if (waiting.Данные != null)
                        {
                            for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                            {
                                lineXml = resultXml.CreateElement("rank");
                                lineXml.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                                lineXml.SetAttribute("NAME", waiting.Данные.Rows[i].ItemArray[2].ToString());
                                resultXml.DocumentElement.AppendChild(lineXml);
                                LiteCmd.CommandText = "INSERT INTO rank (IDN, NAME) VALUES (" + waiting.Данные.Rows[i].ItemArray[1] + ", '" + waiting.Данные.Rows[i].ItemArray[2].ToString() + "')";
                                LiteCmd.ExecuteNonQuery();
                            }
                        }
                        waiting.Dispose();
                        LiteCmd.CommandText = "CREATE TABLE staff (IDN INTEGER PRIMARY KEY, STAFFID INTEGER, NAME TEXT, LEVEL INTEGER, PROF INTEGER);";
                        LiteCmd.ExecuteNonQuery();
                        waiting = new FormWaiting("Сотрудники", "SELECT STAFFID, FNAME || ' ' || FSTNAME, STAGE, PROF, IDN FROM LONDA.STAFF WHERE STAFFID IS NOT NULL AND HIREDATE IS NOT NULL AND DROPDATE IS NULL", new string[] { "", "" }, null, null, conn, "default", new string[] { "#", "STAFFID", "NAME", "STAGE", "PROF", "IDN" });
                        waiting.ShowDialog();
                        if (waiting.Данные != null)
                        {
                            for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                            {
                                lineXml = resultXml.CreateElement("worker");
                                lineXml.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                                lineXml.SetAttribute("NAME", waiting.Данные.Rows[i].ItemArray[2].ToString());
                                lineXml.SetAttribute("LEVEL", waiting.Данные.Rows[i].ItemArray[3].ToString());
                                lineXml.SetAttribute("PROF", waiting.Данные.Rows[i].ItemArray[4].ToString());
                                lineXml.SetAttribute("IDN", waiting.Данные.Rows[i].ItemArray[5].ToString());
                                resultXml.DocumentElement.AppendChild(lineXml);
                                LiteCmd.CommandText = "INSERT INTO staff (IDN, STAFFID, NAME, LEVEL, PROF) VALUES (" + waiting.Данные.Rows[i].ItemArray[5] + ", '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[1].ToString() + "', '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[2].ToString().Replace("'","''") + "', '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[3].ToString() + "', '" +
                                                                                                                        waiting.Данные.Rows[i].ItemArray[4].ToString() + "')";
                                LiteCmd.ExecuteNonQuery();
                            }
                        }
                        waiting.Dispose();
                        LiteCmd.CommandText = "CREATE TABLE curver (VERSION INTEGER);";
                        LiteCmd.ExecuteNonQuery();
                        waiting = new FormWaiting("Обновления", "SELECT MAX(IDN) FROM LONDA.SALON_UPDATES", new string[] { "", "" }, null, null, conn, "default", new string[] { "#", "IDN" });
                        waiting.ShowDialog();
                        if (waiting.Данные != null)
                        {
                            for (int i = 0; i < waiting.Данные.Rows.Count; i++)
                            {
                                lineXml = resultXml.CreateElement("updates");
                                lineXml.SetAttribute("ID", waiting.Данные.Rows[i].ItemArray[1].ToString());
                                resultXml.DocumentElement.AppendChild(lineXml);
                                LiteCmd.CommandText = "INSERT INTO curver (VERSION) VALUES (" + waiting.Данные.Rows[i].ItemArray[1] + ")";
                                LiteCmd.ExecuteNonQuery();
                            }
                        }
                        waiting.Dispose();
                        LiteConn.Close();
                        resultXml.Save(Path + "\\staff.xml");
                        MessageBox.Show("Файл \"staff.xml\" сохранен на рабочий стол");
                        break;
                        #endregion
                    case "Проверить счета"://18
                        #region Кнопка Проверить счета
                        commandTbl = new DataTable();
                        commandTbl.Columns.Add("Подписи", typeof(string));          //0 
                        commandTbl.Columns.Add("EditBox", typeof(string));          //1
                        row = commandTbl.NewRow();
                        row[0] = "Номер счета";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Дата";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Салон";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Номер сотрудника";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Ф.и. сотрудника";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Время начала процедур(ы)";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Время окончания процедур(ы)";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        row = commandTbl.NewRow();
                        row[0] = "Сумма";
                        row[1] = "textbox";
                        commandTbl.Rows.Add(row);
                        filterTbl = new DataTable();
                        filterTbl.Columns.Add("Подписи", typeof(string));          //0 
                        filterTbl.Columns.Add("OraColumns", typeof(string));       //1
                        filterTbl.Columns.Add("DataType", typeof(string));       //2
                        row = filterTbl.NewRow();
                        row[0] = "Номер счета";
                        row[1] = "b.NUM";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Дата";
                        row[1] = "b.DAT";
                        row[2] = "date";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Салон";
                        row[1] = "s.ADRSS";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Номер сотрудника";
                        row[1] = "b.WORKERNUM";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Ф.и. сотрудника";
                        row[1] = "b.WORKER";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Время начала процедур(ы)";
                        row[1] = "b.STIME";
                        row[2] = "time";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Время окончания процедур(ы)";
                        row[1] = "b.FTIME";
                        row[2] = "time";
                        filterTbl.Rows.Add(row);
                        row = filterTbl.NewRow();
                        row[0] = "Сумма";
                        row[1] = "SUM(d.SELLPRICE*d.QUANTITY)";
                        row[2] = "number";
                        filterTbl.Rows.Add(row);
                        sortTbl = new DataTable();
                        sortTbl.Columns.Add("OraCol", typeof(string));          //0 
                        sortTbl.Columns.Add("Direction", typeof(string));       //1
                        row = sortTbl.NewRow();
                        row[0] = "b.DAT";
                        row[1] = "DESC";
                        sortTbl.Rows.Add(row);
                        row = sortTbl.NewRow();
                        row[0] = "b.STIME";
                        row[1] = "DESC";
                        sortTbl.Rows.Add(row);
                        command = "SELECT b.IDN, b.NUM, to_char(b.DAT,'dd/mm/yyyy'), s.ADRSS, b.WORKERNUM, b.WORKER, to_char(b.STIME,'HH24:MI'), to_char(b.FTIME,'HH24:MI'), SUM(d.SELLPRICE*d.QUANTITY) FROM LONDA.BILLSTOCHECK b, LONDA.BILLSDATATOCHECK d, LONDA.SALONS s WHERE b.IDN = d.BILL AND b.SALON = s.IDN";
                        GroupBy = " GROUP BY b.IDN, b.NUM, b.DAT, s.ADRSS, b.WORKERNUM, b.WORKER, b.STIME, b.FTIME";
                        cmd = new OracleCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "LONDA.NOTHINGTODO";
                        par = cmd.CreateParameter();
                        par.ParameterName = "MESSAGE";
                        par.OracleType = OracleType.NVarChar;
                        par.Size = 4000;
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);
                        staff = new FormTable(conn, commandTbl, command, cmd, "Проверить рабочие листы", cmd, sortTbl, filterTbl, null, "default", GroupBy);
                        staff.MdiParent = this;
                        staff.Show();
                        #endregion
                        break;
                    default:
                        MessageBox.Show("Функция для кнопки не указана");
                        break;
                }
            }
            //throw new NotImplementedException();

        }

        private void импортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleParameter par;
            OracleCommand importCmd = new OracleCommand();
            importCmd.CommandType = CommandType.StoredProcedure;
            importCmd.CommandText = "LONDA.INSSTAFF";
            string[] parameters = { "in_IDN",              
                                      "in_FNAME",           
                                      "in_FSTNAME",          
                                      "in_FATHNAME",         
                                      "in_PRENAME",          
                                      "in_PROF",             
                                      "in_STAGE",            
                                      "in_STAGEDATE",       
                                      "in_HIREDATE",
                                      "in_MAINSALON",
                                      "in_BIRTHDAY",        
                                      "in_DROPDATE",         
                                      "in_FORMALIZED",      
                                      "in_FORMALDATE" };
                    foreach (string line in parameters)
                    {
                        par = importCmd.CreateParameter();
                        par.ParameterName = line;
                        par.OracleType = OracleType.VarChar;
                        par.Size = 0;
                        par.Direction = ParameterDirection.Input;
                        importCmd.Parameters.Add(par);
                    }
                    par = importCmd.CreateParameter();
                    par.ParameterName = "ERRSTAFF";
                    par.OracleType = OracleType.NVarChar;
                    par.Size = 4000;
                    par.Direction = ParameterDirection.Output;
                    importCmd.Parameters.Add(par);
            FormImport import = new FormImport(importCmd ,conn);
            import.ShowDialog();
        }

        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable commandTbl = new DataTable();
            commandTbl.Columns.Add("Подписи", typeof(string));          //0 
            commandTbl.Columns.Add("EditBox", typeof(string));          //1
            DataRow row = commandTbl.NewRow();
            row[0] = "ID";
            row[1] = "textbox";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Фамилия";
            row[1] = "textbox";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Имя";
            row[1] = "textbox";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Отчество";
            row[1] = "textbox";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Девичья фамилия";
            row[1] = "textbox";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Должность";
            row[1] = "combobox SELECT IDN, PROF FROM LONDA.PROFESSIONS";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Звание";
            row[1] = "combobox SELECT IDN, STAGE FROM LONDA.STAGES";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Дата присвоения";
            row[1] = "datepicker";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Дата приема";
            row[1] = "datepicker";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "День рождения";
            row[1] = "datepicker";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Дата Увольнения";
            row[1] = "datepicker";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Оформлен";
            row[1] = "combobox SELECT IDN, CNAME FROM LONDA.COMPANY";
            commandTbl.Rows.Add(row);
            row = commandTbl.NewRow();
            row[0] = "Дата Оформления";
            row[1] = "datepicker";
            commandTbl.Rows.Add(row);
            DataTable filterTbl = new DataTable();
            filterTbl.Columns.Add("Подписи", typeof(string));          //0 
            filterTbl.Columns.Add("OraColumns", typeof(string));       //1
            filterTbl.Columns.Add("DataType", typeof(string));       //2
            row = filterTbl.NewRow();
            row[0] = "ID";
            row[1] = "s.IDN";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Фамилия";
            row[1] = "s.FNAME";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Имя";
            row[1] = "s.FSTNAME";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Отчество";
            row[1] = "s.FATHNAME";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Девичья фамилия";
            row[1] = "s.PRENAME";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Должность";
            row[1] = "p.PROF";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Звание";
            row[1] = "a.STAGE";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Дата присвоения";
            row[1] = "s.STAGEDATE";
            row[2] = "date";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Дата приема";
            row[1] = "s.HIREDATE";
            row[2] = "date";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "День рождения";
            row[1] = "s.BIRTHDAY";
            row[2] = "date";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Дата Увольнения";
            row[1] = "s.DROPDATE";
            row[2] = "date";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Оформлен";
            row[1] = "c.CNAME";
            filterTbl.Rows.Add(row);
            row = filterTbl.NewRow();
            row[0] = "Дата Оформления";
            row[1] = "s.FORMALDATE";
            row[2] = "date";
            filterTbl.Rows.Add(row);
            //string[] colName = { "№", "Фамилия", "Имя", "Отчество", "Девичья фамилия", "Профессия", "Звание", "Дата присвоения", "Дата приема", "День рождения", "Дата Увольнения", "Оформлен", "Дата Оформления" };
            string command = "SELECT s.IDN, s.IDN, s.FNAME, s.FSTNAME, s.FATHNAME, s.PRENAME, p.PROF, a.STAGE, to_char(s.STAGEDATE,'dd/mm/yyyy'), to_char(s.HIREDATE,'dd/mm/yyyy'), to_char(s.BIRTHDAY,'dd/mm/yyyy'), to_char(s.DROPDATE,'dd/mm/yyyy'), c.CNAME, to_char(s.FORMALDATE,'dd/mm/yyyy') FROM LONDA.STAFF s, LONDA.PROFESSIONS p, LONDA.STAGES a, LONDA.COMPANY c WHERE s.PROF <> 0 AND s.PROF = p.IDN AND s.STAGE = a.IDN AND s.FORMALIZED = c.IDN AND s.DROPDATE IS NULL"; // 
            
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter par;
            DataTable sortTbl = new DataTable();
            sortTbl.Columns.Add("OraCol", typeof(string));          //0 
            sortTbl.Columns.Add("Direction", typeof(string));       //1
            row = sortTbl.NewRow();
            row[0] = "S.FNAME";
            row[1] = "ASC";
            sortTbl.Rows.Add(row);
            cmd.CommandText = "LONDA.NOTHINGTODO";
            par = cmd.CreateParameter();
            par.ParameterName = "MESSAGE";
            par.OracleType = OracleType.NVarChar;
            par.Size = 4000;
            par.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par);

            FormTable staff = new FormTable(conn, commandTbl, command, cmd, "Экспорт", cmd, sortTbl, filterTbl, null, "default", null);
            staff.MdiParent = this;
            staff.Show();
        }
       
    

       
     
        
    }
}
