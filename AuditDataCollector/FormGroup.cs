using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AuditDataCollector
{
    public partial class FormGroup : Form
    {
        MySqlCommand MySQL_cmd;
        int dir_index, dir_index_loaded;
        List <int> indxs = new List <int>();
        public List<int> checkedList = new List<int>();
        List<int> tempList = new List<int>();
        public int Director
        {
            get;
            set;
        }
        public FormGroup(MySqlCommand cmdref)
        {
            MySQL_cmd = cmdref;
            dir_index_loaded = -1;
            InitializeComponent();
        }
        private void GetInspectors(int Dir_id)
        {
            indxs.Clear();
            checkedListBox.Items.Clear();
            comboBox.Items.Clear();
            dir_index_loaded = -1;
            try
            {
                MySQL_cmd.CommandText = "SELECT _id, CONCAT_WS(' ', surname, name, patronymic) as fio FROM inspectors ORDER BY fio";
                MySqlDataReader MySQL_Reader = MySQL_cmd.ExecuteReader();
                comboBox.Items.Clear();
                while (MySQL_Reader.Read())
                {
                    int id_db = MySQL_Reader.GetInt32("_id");
                    int r = comboBox.Items.Add(MySQL_Reader["fio"]);
                    checkedListBox.Items.Add(MySQL_Reader["fio"]);
                    indxs.Add(id_db);
                    if (Dir_id == id_db) dir_index_loaded = r;
                }
                MySQL_Reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
            }
        }
        private void FormGroup_Shown(object sender, EventArgs e)
        {
            GetInspectors(Director);
            for (int i = 0; i < indxs.Count; i++)
            {
                checkedListBox.SetItemChecked(i, checkedList.Contains(indxs[i]));
            }
            dir_index = -1;
            comboBox.SelectedIndex = dir_index_loaded;
        }
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dir_index > -1)
            {
                checkedListBox.Items.Insert(dir_index, comboBox.Items[dir_index]);
            }
            dir_index = comboBox.SelectedIndex;
            checkedListBox.Items.RemoveAt(dir_index);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dir_index > -1)
            {
                checkedListBox.Items.Insert(dir_index, comboBox.Items[dir_index]);
            }
            checkedList.Clear();
            foreach (int i in checkedListBox.CheckedIndices)
            {
                checkedList.Add(indxs[i]);
            }
            if (dir_index != -1) Director = indxs[dir_index];
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dir_index > -1)
            {
                checkedListBox.Items.Insert(dir_index, comboBox.Items[dir_index]);
            }
            tempList.Clear();
            foreach (int i in checkedListBox.CheckedIndices)
            {
                tempList.Add(indxs[i]);
            }
            if (dir_index > -1)
            {
                checkedListBox.Items.RemoveAt(dir_index);
            }
            new FormNewUser(MySQL_cmd).ShowDialog();
            int tmp_dir = dir_index != -1 ? indxs[dir_index] : 0;
            GetInspectors(tmp_dir);
            for (int i = 0; i < indxs.Count; i++)
            {
                checkedListBox.SetItemChecked(i, tempList.Contains(indxs[i]));
            }
            dir_index = -1;
            comboBox.SelectedIndex = dir_index_loaded;
        }
    }
}
