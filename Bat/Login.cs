using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Security;
using System.Runtime.InteropServices;

namespace Bat
{
    public partial class Login : Form
    {
        private OracleConnection conn;
        private string User;
        private SecureString Pass;
        private string DataBase;

        public string Пользователь
        { get { return User; } }

        public SecureString Пароль
        { get { return Pass; } }

        public string БазаДанных
        { get { return DataBase; } }

        public OracleConnection СоединениеСБазой
        { get { return conn; } }
        
        public Login(string Database, string UserID, SecureString Password)
        {
            InitializeComponent();
            ShowInTaskbar = false;
            //textBoxUserId.Text = UserID;
            if (Database != null)
            {
                textBoxDataBase.Text = Database;
            }
            Pass = new SecureString();
            
            if (!AutoConnection(Password))
            {                
                ShowDialog();
            }
            if (conn != null)
            {
                conn.InfoMessage += new OracleInfoMessageEventHandler(conn_InfoMessage);
            }
        }

        void conn_InfoMessage(object sender, OracleInfoMessageEventArgs e)
        {
            MessageBox.Show(e.Message, "Сообщение коннектора", MessageBoxButtons.OK);
            //throw new NotImplementedException();
        }

        public static bool OpenConnection(OracleConnection DBConn)
        {
            START:
            try
            {
                if (DBConn.State == ConnectionState.Closed | DBConn.State == ConnectionState.Broken)
                {
                    DBConn.Open();
                }
                return true;
            }
            catch (OracleException oe)
            {
                if (MessageBox.Show(oe.Message, "Ошибка подключения", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                {
                    goto START;
                }
                else
                {
                    return false;
                }
            }

        }

        private bool AutoConnection(SecureString Password)
        {
            if (Password != null && Password.Length != 0)
            {
                Pass = Password;
                conn = SetConnection(textBoxUserId.Text, textBoxDataBase.Text, Pass);
                if (OpenConnection(conn))
                {
                    User = textBoxUserId.Text;
                    DataBase = textBoxDataBase.Text;
                    /*Buttons butt = new Buttons(conn, Pass, textBoxUserId.Text, textBoxDataBase.Text);
                    butt.Show();*/
                    conn.Close();
                    Dispose(true);
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static OracleConnection SetConnection(string UserName, string OraBase, SecureString PassWord)
        {
            OracleConnection connection = new OracleConnection();
            OracleConnectionStringBuilder connString = new OracleConnectionStringBuilder();
            connString.DataSource = OraBase;
            connString.UserID = UserName;
            IntPtr hWndPass = Marshal.SecureStringToCoTaskMemAnsi(PassWord);
            connString.Password = Marshal.PtrToStringAnsi(hWndPass, PassWord.Length);
            Marshal.ZeroFreeCoTaskMemAnsi(hWndPass);
            connection.ConnectionString = connString.ConnectionString;
            connString.Password = "";

                    
            return connection;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            foreach (char sign in textBoxPassword.Text.ToCharArray())
            {
                Pass.AppendChar(sign);
            }
            textBoxPassword.Text = "xxxxxxxx";

            conn = SetConnection(textBoxUserId.Text, textBoxDataBase.Text, Pass);

            if (OpenConnection(conn))
            {
                User = textBoxUserId.Text;
                DataBase = textBoxDataBase.Text;
                /*Buttons but = new Buttons(conn, Pass, User, textBoxDataBase.Text);
                but.Show();*/
                conn.Close();
                Dispose(true);
            }
            else
            {
                Pass.Clear();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Dispose(true);
            Environment.Exit(0);
        }
        
    }
}
