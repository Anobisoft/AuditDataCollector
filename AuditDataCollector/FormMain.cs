using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace AuditDataCollector
{

    public partial class FormMain : Form
    {
        ClassOptions Opt;
        MySqlConnection MySQL_Connection;
        MySqlDataReader MySQL_Reader;
        public MySqlCommand MySQL_cmd = new MySqlCommand();

        ColumnComparer columnComparer = new ColumnComparer();
        bool autorized = false;
        public int Autorized_Inspector_ID
        {
            set;
            get;
        }

        private void EnableControls(bool x)
        {
            MainMenu.Items[2].Enabled = x;
            MainMenu.Items[3].Enabled = x;
            MMItem_AddUser.Enabled = x;
            MMItem_ChangeUser.Enabled = x;
            tabControl.Enabled = x;
            autorized = x;
        }
        private void Autorization()
        {
            if (autorized)
            {
                EnableControls(false);
                tabControl.SelectedIndex = 0;
                Journal_listView.Items.Clear();
                MMItem_Autorization.Text = "Авторизация";
                this.Text = "Ревизор - Неавторизованный пользователь";
            }
            else
            {
                DialogResult dr = (new FormAutorization(this)).ShowDialog();
                if (dr == DialogResult.OK)
                {
                    EnableControls(true);
                    JournalRead();
                    MMItem_Autorization.Text = "Выход";
                }
            }
        }

        private void Connect()
        {
            try
            {
                MySQL_Connection = new MySqlConnection("Host=" + Opt.DBHost + ";Port=" + Opt.DBPort + ";User=" + Opt.DBUser + ";Pwd=" + Opt.DBPass + ";Database=" + Opt.DBName + ";SslMode=Preferred;");
                MySQL_Connection.Open();
                MySQL_cmd.Connection = MySQL_Connection;
                statusLabel.Text = "Подключено к " + Opt.DBHost + ":" + Opt.DBPort + " / MySQL v" + MySQL_Connection.ServerVersion;
                MMItem_Connect.Text = "Отключиться";
                Autorization();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
                statusLabel.Text = "Соединение не установлено.";
            }
        }
        private void Disconnect()
        {
            try
            {
                MySQL_Connection.Close();
                statusLabel.Text = "Соеденение с сервером MySQL закрыто.";
                MMItem_Connect.Text = "Подключиться";
                EnableControls(false);
                tabControl.SelectedIndex = 0;
                Journal_listView.Items.Clear();
                MMItem_Autorization.Text = "Авторизация";
                this.Text = "Ревизор - Неавторизованный пользователь";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
            }
        }
        private bool Connected()
        {
            try
            {
                if (MySQL_Connection != null)
                {
                    return MySQL_Connection.State == ConnectionState.Open;
                }
                else return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public FormMain()
        {
            InitializeComponent();
            Opt = new ClassOptions();
            Journal_listView.ListViewItemSorter = columnComparer;
            columnComparer.ColumnText = Journal_listView.Columns[columnComparer.ColumnIndex].Text;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Opt.Get()) Connect();
            else (new FormOptions(true)).ShowDialog();
        }
        private void MMItem_Connect_Click(object sender, EventArgs e)
        {
            if (Connected())
            {
                Disconnect();
            }
            else
            {
                if (Opt.Get())
                {
                    Connect();
                }
                else
                {
                    (new FormOptions(true)).ShowDialog();
                }
            }
        }
        private void MMItem_Options_Click(object sender, EventArgs e)
        {
            (new FormOptions(false)).ShowDialog();
        }
        private void MMItem_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void JournalRead()
        {
            Journal_listView.Items.Clear();
            MySQL_cmd.CommandText = "SELECT inspections._id, inspections.protocol, DATE_FORMAT(inspections._date, '%d.%m.%Y'), CONCAT_WS(' ', inspectors.surname, inspectors.name, inspectors.patronymic), inspections.name FROM inspections, inspectors WHERE inspections.director = inspectors._id ORDER BY inspections._id DESC";
            try
            {
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                while (MySQL_Reader.Read())
                {
                    ListViewItem x = new ListViewItem(MySQL_Reader.GetString(0));
                    for (int i = 1; i < 5; i++)
                    {
                        x.SubItems.Add(MySQL_Reader.GetString(i));
                    }
                    Journal_listView.Items.Add(x);
                }
                MySQL_Reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
            }
            finally {  }
        }
        private void MMItem_Journal_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
            JournalRead();
        }
        private string ProtocolNumber()
        {
            return Journal_listView.Items[Journal_listView.SelectedItems[0].Index].SubItems[1].Text;
        }
        private int InspectionID
        {
            get
            {
                return int.Parse(Journal_listView.Items[Journal_listView.SelectedItems[0].Index].SubItems[0].Text);
            }
        }

        private void Journal_EditProtocol()
        {
            bool found = false;
            for (int i = 2; i < tabControl.TabCount; i++)
            {
                if ((int)tabControl.TabPages[i].Tag == InspectionID)
                {
                    found = true;
                    tabControl.SelectedIndex = i;
                }
            }

                if (!found)
                {
                    ProtocolTabPage newTP = new ProtocolTabPage(this, InspectionID);
                    tabControl.TabPages.Add(newTP.tabPage);
                    tabControl.SelectedTab = newTP.tabPage;
                }
        }
        private void Journal_DeleteProtocol(bool confirmed)
        {
            string ProtoN = ProtocolNumber();
            if (!confirmed)
            {
                DialogResult dr = MessageBox.Show("Вы уверены, что хотите удалить протокол №" + ProtoN + "?", "Удаление протокола проверки", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK) confirmed = true;
            }
            if (confirmed)
            {
                MySQL_cmd.CommandText = "DELETE FROM inspections WHERE _id='" + InspectionID + "'";
                try
                {
                    MySQL_cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка MySQL");
                }
                JournalRead();
            }
        }
        private void Journal_listView_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (Journal_listView.SelectedItems.Count == 1))
            {
                Journal_contextMenu.Show((Control)sender, e.Location);
            }
        }
        private void Journal_listViewItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Journal_EditProtocol();
        }
        private void Journal_contextMenuItem_Edit_Click(object sender, EventArgs e)
        {
            Journal_EditProtocol();
        }
        private void Journal_contextMenuItem_Delete_Click(object sender, EventArgs e)
        {
            Journal_DeleteProtocol(false);
        }
        private void tabControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                if (e.KeyCode == Keys.Delete && !(e.Alt || e.Control))
                {
                    if (e.Shift) Journal_DeleteProtocol(true);
                    else Journal_DeleteProtocol(false);
                }
                if (e.KeyCode == Keys.F5 && !(e.Alt || e.Control || e.Shift))
                {
                    JournalRead();
                }
            }
        }
        private void Journal_listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            char[] sds = { '▲', '▼' };
            Journal_listView.Columns[columnComparer.ColumnIndex].Text = columnComparer.ColumnText;
            columnComparer.ColumnIndex = e.Column;
            columnComparer.ColumnText = Journal_listView.Columns[columnComparer.ColumnIndex].Text;
            Journal_listView.Columns[columnComparer.ColumnIndex].Text += " " + (columnComparer.sortAscending ? sds[0] : sds[1]); 
            Journal_listView.Sort();
        }
        private void MMItem_Report_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }
        private void MMItem_Autorization_Click(object sender, EventArgs e)
        {
            Autorization();
        }
        private void MMItem_ChangeUser_Click(object sender, EventArgs e)
        {
            Autorization();
            Autorization();
        }
        private void MMItem_AddUser_Click(object sender, EventArgs e)
        {
            new FormNewUser(MySQL_cmd).ShowDialog();
        }
        private void MMItem_NewProto_Click(object sender, EventArgs e)
        {
            ProtocolTabPage x = new ProtocolTabPage(this, 0);
            tabControl.TabPages.Add(x.tabPage);
        }
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    JournalRead();
                    break;
                case 1:
                    try
                    {
                        Report_comboBoxYear.Items.Clear();
                        MySQL_cmd.CommandText = "SELECT YEAR(_date) as years FROM inspections GROUP BY years DESC";
                        MySQL_Reader = MySQL_cmd.ExecuteReader();
                        while (MySQL_Reader.Read())
                        {
                            Report_comboBoxYear.Items.Add(MySQL_Reader.GetString("years"));
                        }
                        MySQL_Reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка MySQL");
                    }
                    break;
                    // TabPage
                default: ;
                    break;
            }
        }
        private void Report_buttonExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Report_comboBoxYear.SelectedIndex == -1)
                {
                    MessageBox.Show("Необходимо выбрать год!");
                }
                else
                {
                    string YEAR = Report_comboBoxYear.SelectedItem.ToString();
                    Report(YEAR);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Report_comboBoxYear.SelectedIndex == -1)
                {
                    MessageBox.Show("Необходимо выбрать год!");
                }
                else
                {
                    string YEAR = Report_comboBoxYear.SelectedItem.ToString();
                    dataGridView.Columns.Clear();
                    DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                    dataGridView.RowHeadersWidth = 700;
                    Column.HeaderText = "Количество";
                    Column.ReadOnly = true;
                    dataGridView.Columns.Add(Column);
                    dataGridView.RowCount = 4;
                    MySQL_cmd.CommandText = "SELECT inspections.reason as ri, reasons.name as rn, count(inspections._id) as cnt FROM inspections, reasons WHERE YEAR(inspections._date)=" + YEAR + " AND inspections.reason <> 1 AND inspections.reason=reasons._id GROUP BY ri";
                    MySQL_Reader = MySQL_cmd.ExecuteReader();
                    for (int r = 0; MySQL_Reader.Read(); r++)
                    {
                        dataGridView.Rows[r].HeaderCell.Value = MySQL_Reader["rn"];
                        dataGridView.Rows[r].Cells[0].Value = MySQL_Reader["cnt"];
                    }
                    MySQL_Reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                DateTime xx = dateTimePicker1.Value;
                dateTimePicker1.Value = dateTimePicker2.Value;
                dateTimePicker2.Value = xx;
            }
            string q1 = "SELECT SUM(d1), SUM(d2), SUM(d3), SUM(d4), SUM(d5), SUM(d6), SUM(d7)";
            string q2 = ", SUM(d8), SUM(d9), SUM(d10), SUM(d11), SUM(d12), SUM(d13), SUM(d14), SUM(d15), SUM(d16) FROM violations";
            string q3 = " FROM eliminations";
            string q4 = " WHERE inspection IN (SELECT _id FROM inspections WHERE _date BETWEEN '"
                + dateTimePicker1.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND '"
                + dateTimePicker2.Value.ToString("yyyy-MM-dd") + " 23:59:59') GROUP BY budget_lvl";

            dataGridView.Columns.Clear();
            List<string> total_info_data_names = new List<string>();
            total_info_data_names.AddRange(new string[]
            {
                "Общий объем проверенных средств",
                "Общая сумма нарушений",
                "В том числе, нецелевые",
                "Финансовые нарушения",
                "Прочие Нефинансовые нарушения",
                "Нарушения  94-ФЗ",
                "Устранено нарушений",
                "Неустранимые",
                "Не устранено",
                "Зачет нецелевых",
                "Возмещено в бюджет",
                "Наложено штрафов",
                "Уплачено штрафов" });
            int CN = 13;
            DataGridViewTextBoxColumn[] Columns;
            Columns = new DataGridViewTextBoxColumn[CN];
            for (int i = 0; i < CN; i++)
            {
                Columns[i] = new DataGridViewTextBoxColumn();
                Columns[i].HeaderText = total_info_data_names[i];
                Columns[i].Width = 100;
                Columns[i].ReadOnly = true;
                dataGridView.Columns.Add(Columns[i]);
            }


            dataGridView.RowCount = 5;
            dataGridView.RowHeadersWidth = 120;
            dataGridView.Rows[0].HeaderCell.Value = "Федеральный";
            dataGridView.Rows[1].HeaderCell.Value = "Региональный";
            dataGridView.Rows[2].HeaderCell.Value = "Местный";
            dataGridView.Rows[3].HeaderCell.Value = "Прочее";
            dataGridView.Rows[4].HeaderCell.Value = "Всего";

            try
            {
                MySQL_cmd.CommandText = q1 + q2 + q4;
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                for (int i = 0; MySQL_Reader.Read(); i++)
                {
                    dataGridView.Rows[i].Cells[0].Value = MySQL_Reader[15];
                    double sum = 0;
                    int j;
                    for (j = 3; j < 6; j++)
                    {
                        sum += MySQL_Reader.GetDouble(j);
                    }
                    dataGridView.Rows[i].Cells[1].Value = sum;
                    dataGridView.Rows[i].Cells[2].Value = MySQL_Reader[4];
                    for (j = 0, sum = 0; j < 13; j++) sum += MySQL_Reader.GetDouble(j);
                    dataGridView.Rows[i].Cells[3].Value = sum;
                    dataGridView.Rows[i].Cells[4].Value = MySQL_Reader.GetDouble(13);
                    dataGridView.Rows[i].Cells[5].Value = MySQL_Reader.GetDouble(14);
                }
                MySQL_Reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
            }
            try
            {
                MySQL_cmd.CommandText = q1 + q3 + q4;
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                for (int i = 0; MySQL_Reader.Read(); i++)
                {
                    dataGridView.Rows[i].Cells[6].Value = MySQL_Reader.GetDouble(0) + MySQL_Reader.GetDouble(1);
                    dataGridView.Rows[i].Cells[7].Value = MySQL_Reader.GetDouble(3);
                    dataGridView.Rows[i].Cells[8].Value = MySQL_Reader.GetDouble(4);
                    dataGridView.Rows[i].Cells[9].Value = MySQL_Reader.GetDouble(0);
                    dataGridView.Rows[i].Cells[10].Value = MySQL_Reader.GetDouble(1);
                    dataGridView.Rows[i].Cells[11].Value = MySQL_Reader.GetDouble(5);
                    dataGridView.Rows[i].Cells[12].Value = MySQL_Reader.GetDouble(6);
                }
                MySQL_Reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
            }
            for (int i = 0; i < 13; i++)
            {
                double sum = 0;
                for (int j = 0; j < 4; j++)
                {
                    sum += double.Parse(dataGridView.Rows[j].Cells[i].Value.ToString());
                }
                dataGridView.Rows[4].Cells[i].Value = sum;
            }

        }

        private void MMItem_WhatNew_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://178.49.119.4/auditdc/whatnew.html");
        }

        private void MMItem_About_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://178.49.119.4/auditdc/about.html");
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://178.49.119.4/auditdc/help.html");
        }

    }
}
