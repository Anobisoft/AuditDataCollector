using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace AuditDataCollector
{
    public partial class FormAutorization : Form
    {
        struct pwdrec
        {
            public int id_db;
            public string pwdhash;
        }
        MySqlCommand MySQL_cmd;
        List <pwdrec> pwdlist = new List <pwdrec>();
        FormMain FM;
        bool kEnter_Pressed, kEsc_Pressed;

        public FormAutorization(FormMain parent)
        {
            InitializeComponent();
            MySQL_cmd = parent.MySQL_cmd;
            FM = parent;
        }

        private void FormAutorization_Load(object sender, EventArgs e)
        {
            // НАЧАЛО Пропуск авторизации УДАЛИТЬ!
            //FM.Autorized_Inspector_ID = 1;
            //FM.Text = "Ревизор - Заплатка авторизации";
            //DialogResult = DialogResult.OK;
            //return;
            // КОНЕЦ Пропуск авторизации
            try
            {
                MySQL_cmd.CommandText = "SELECT _id, CONCAT_WS(' ', surname, name, patronymic) as fio, pwdhash FROM inspectors ORDER BY fio";
                MySqlDataReader MySQL_Reader = MySQL_cmd.ExecuteReader();
                pwdrec x = new pwdrec();
                comboBox.Items.Clear();
                pwdlist.Clear();
                while (MySQL_Reader.Read())
                {
                    x.id_db = MySQL_Reader.GetInt32("_id");
                    x.pwdhash = MySQL_Reader.GetString("pwdhash");
                    comboBox.Items.Add(MySQL_Reader["fio"]);
                    pwdlist.Add(x);
                }
                MySQL_Reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
            }
        }

        private void Compare()
        {
            if (comboBox.SelectedIndex > -1)
            {
                string dbpwdhash = pwdlist[comboBox.SelectedIndex].pwdhash;
                string tbpwd = textBox.Text, tbpwdhash = "";
                try
                {
                    MySQL_cmd.CommandText = "SELECT PASSWORD('" + tbpwd + "') as hash";
                    tbpwdhash = (MySQL_cmd.ExecuteScalar()).ToString();
                    if (dbpwdhash == tbpwdhash)
                    {
                        FM.Text = "Ревизор - " + comboBox.Items[comboBox.SelectedIndex].ToString();
                        FM.Autorized_Inspector_ID = pwdlist[comboBox.SelectedIndex].id_db;
                        DialogResult = DialogResult.OK;
                    }
                    else MessageBox.Show("Неверный пароль!");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Вы должны выбрать свое имя из списка!");
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Compare();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xC_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Enter:
                    if (kEnter_Pressed) Compare();
                    kEnter_Pressed = false;
                    break;
                case Keys.Escape:
                    if (kEsc_Pressed) DialogResult = DialogResult.Cancel;
                    kEsc_Pressed = false;
                    break;
            }
        }

        private void xC_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    kEnter_Pressed = true;
                    break;
                case Keys.Escape:
                    kEsc_Pressed = true;
                    break;
            }
        }
    }
}
