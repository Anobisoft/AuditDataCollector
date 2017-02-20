using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AuditDataCollector
{
    public partial class FormNewUser : Form
    {
        MySqlCommand MySQL_Cmd;

        public FormNewUser(MySqlCommand myref)
        {
            InitializeComponent();
            MySQL_Cmd = myref;
            buttonOK.Enabled = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddUser()
        {
            string a1 = textBox1.Text, a2 = textBox2.Text, a3 = textBox3.Text, a4 = textBox4.Text;
            MySQL_Cmd.CommandText = "INSERT INTO inspectors (surname, name, patronymic, pwdhash) VALUES ('" + a1 + "','" + a2 + "','" + a3 + "',PASSWORD('" + a4 + "'))";
            MySQL_Cmd.ExecuteNonQuery();
        }
        
        private void buttonOK_Click(object sender, EventArgs e)
        {
            AddUser();
            Close();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                labelMSG.Text = "Все поля должны быть заполнены";
                buttonOK.Enabled = false;
            }
            else if (textBox4.Text != textBox5.Text)
            {
                labelMSG.Text = "Пароли должны совпадать";
                buttonOK.Enabled = false;
            }
            else
            {
                labelMSG.Text = "";
                buttonOK.Enabled = true;
            }
        }

        private void FormNewUser_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (buttonOK.Enabled)
                    {
                        AddUser();
                        Close();
                    }
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }
    }
}
