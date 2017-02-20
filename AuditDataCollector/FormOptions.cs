using System;
using System.Windows.Forms;

namespace AuditDataCollector
{

    public partial class FormOptions : Form
    {
        ClassOptions Opt;
        bool getoptfail = false;

        public FormOptions(bool onstart)
        {
            InitializeComponent();
            Opt = new ClassOptions();
            getoptfail = onstart;
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            if (!getoptfail && Opt.Get())
            {
                textBox1.Text = Opt.DBHost;
                textBox2.Text = Opt.DBPort.ToString();
                textBox3.Text = Opt.DBUser;
                textBox4.Text = Opt.DBPass;
                textBox5.Text = Opt.DBName;
            }
            else
            {
                textBox1.Text = "localhost";
                textBox2.Text = "3306";
                textBox3.Text = "auditdc";
                textBox5.Text = "auditdb";
            }
            buttonApply.Enabled = false;
        }

        private void textbox2opt()
        {
            Opt.DBHost = textBox1.Text;
            Opt.DBPort = int.Parse(textBox2.Text);
            Opt.DBUser = textBox3.Text;
            Opt.DBPass = textBox4.Text;
            Opt.DBName = textBox5.Text;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            textbox2opt();
            Opt.Set();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            textbox2opt();
            Opt.Set();
            buttonApply.Enabled = false;
        }

        private void textBoxes_TextChanged(object sender, EventArgs e)
        {
            buttonApply.Enabled = true;
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    textbox2opt();
                    Opt.Set();
                    Close();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }
    }
}
