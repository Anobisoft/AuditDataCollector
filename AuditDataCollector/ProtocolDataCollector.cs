using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Globalization;

namespace AuditDataCollector
{

    class ProtocolDataCollector
    {
        NumberFormatInfo dfi = new NumberFormatInfo();
        struct Period
        {
            public DateTime Begin, End;
        }

        MySqlCommand MySQL_cmd;

        int inspection_id;
        int yearscount;
        Period period;
        double[][,] proto1data;
        double[,] proto2data;
        int reason;

        public int Inspection
        {
            get
            {
                return inspection_id;
            }
            set
            {
                inspection_id = value;
            }
        }
        public DateTime InspectionDate
        {
            get;
            set;
        }
        public int Reason
        {
            get
            {
                return reason - 2;
            }
            set
            {
                reason = value + 2;
            }
        }
        public string Number
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int YearsCount
        {
            get
            {
                return yearscount;
            }
            private set
            {
                if (yearscount != value)
                {
                    int ly = yearscount;
                    yearscount = value;
                    double[][,] tmp = proto1data;
                    proto1data = new double[yearscount][,];
                    for (int i = 0; i < yearscount && i < ly; i++)
                    {
                        proto1data[i] = tmp[i];
                    }
                    for (int i = ly; i < yearscount; i++)
                    {
                        proto1data[i] = new double[4, 16];
                    }
                }
            }
        }
        public DateTime periodBegin
        {
            get
            {
                return period.Begin;
            }
            set
            {
                if (period.Begin != value)
                {
                    period.Begin = value;
                    YearsCount = period.End.Year - period.Begin.Year + 1;
                }
            }
        }
        public DateTime periodEnd
        {
            get
            {
                return period.End;
            }
            set
            {
                if (period.End != value)
                {
                    period.End = value;
                    if (period.End.Year >= period.Begin.Year)
                        YearsCount = period.End.Year - period.Begin.Year + 1;
                }
            }

        }
        public int Inspector
        {
            get;
            set;
        }
        public string Comment
        {
            get;
            set;
        }
        FormGroup Group;

        public void SaveData1(int type, DataGridViewRowCollection Rows)
        {
            try
            {
                for (int i = 0; i < yearscount; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Rows[i].Cells[j].Value != null)
                        {
                            proto1data[i][j, type] = double.Parse(Rows[i].Cells[j].Value.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SaveData2(int type, DataGridViewCellCollection Cells)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Cells[i].Value != null)
                    {
                        proto2data[i, type] = double.Parse(Cells[i].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadData1(int type, DataGridViewRowCollection Rows)
        {
            if (type == -1) return;
            try
            {
                for (int i = 0; i < yearscount; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Rows[i].Cells[j].Value = proto1data[i][j, type];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadData2(int type, DataGridViewCellCollection Cells)
        {
            for (int i = 0; i < 4; i++)
            {
                Cells[i].Value = proto2data[i, type];
            }
        }

        public ProtocolDataCollector(MySqlCommand cmdref, FormGroup fgroupref)
        {
            reason = 1;
            Group = fgroupref;
            proto2data = new double[4, 7];
            period.Begin = DateTime.Now;
            period.End = DateTime.Now;
            InspectionDate = DateTime.Now;
            YearsCount = 1;
            MySQL_cmd = (MySqlCommand)cmdref;
            dfi.NumberDecimalSeparator = ".";
        }

        private void InsertDataGr1()
        {
            try
            {
                for (int i = 0; i < YearsCount; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        MySQL_cmd.CommandText = "INSERT INTO violations VALUES (" + inspection_id + ", " + (j + 1) + ", " + (period.Begin.Year + i);
                        for (int type = 0; type < 16; type++) MySQL_cmd.CommandText += ", '" + proto1data[i][j, type].ToString("N2", dfi) + "'";
                        MySQL_cmd.CommandText += ")";
                        MySQL_cmd.ExecuteNonQuery();
                    }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");

            }
        }
        private void InsertDataGr2()
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    MySQL_cmd.CommandText = "INSERT INTO eliminations VALUES (" + inspection_id + ", " + (i + 1);
                    for (int type = 0; type < 7; type++) MySQL_cmd.CommandText += ", '" + proto2data[i, type].ToString("N2", dfi) + "'";
                    MySQL_cmd.CommandText += ")";
                    MySQL_cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
            }
        }
        private void InsertGroup()
        {
            try
            {
                foreach (int id_inspector in Group.checkedList)
                {
                    MySQL_cmd.CommandText = "INSERT INTO inspection_group VALUES (" + inspection_id + ", " + id_inspector + ")";
                    MySQL_cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");

            }
        }
        public bool Save()
        {
            if (Group.Director <= 0)
            {
                MessageBox.Show("Необходимо выбрать руководителя группы (Состав группы).");
                return false;
            }
            if (Number == "")
            {
                MessageBox.Show("Необходимо ввести уникальный номер протокола проверки.");
                return false;
            }
            if (inspection_id == 0)
            {
                try
                {
                    MySQL_cmd.CommandText =
                        "INSERT INTO inspections (protocol, name, _date, reason, director, " +
                        "period_begin, period_end, _comment, modif_inspector, modif_datetime) " +
                        String.Format("VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', NOW())",
                        Number, Name, InspectionDate.ToString("yyyy-MM-dd"), reason, Group.Director,
                        period.Begin.ToString("yyyy-MM-dd"), period.End.ToString("yyyy-MM-dd"), Comment, Inspector);
                    MySQL_cmd.ExecuteNonQuery();
                    MySQL_cmd.CommandText = "SELECT @@IDENTITY as 'new_id'";
                    inspection_id = int.Parse(MySQL_cmd.ExecuteScalar().ToString());
                    InsertDataGr1();
                    InsertDataGr2();
                    InsertGroup();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message + "\n" + MySQL_cmd.CommandText, "Ошибка MySQL");
                    return false;
                }
            }
            else
            {
                try
                {
                    MySQL_cmd.CommandText =
                        String.Format("UPDATE inspections SET protocol='{0}', name='{1}', _date='{2}', reason='{3}', director='{4}', " +
                        "period_begin='{5}', period_end='{6}', _comment='{7}', modif_inspector='{8}', modif_datetime=NOW() WHERE _id={9}", 
                        Number, Name, InspectionDate.ToString("yyyy-MM-dd"), reason, Group.Director,
                        period.Begin.ToString("yyyy-MM-dd"), period.End.ToString("yyyy-MM-dd"), Comment, Inspector, inspection_id);
                    MySQL_cmd.ExecuteNonQuery();
                    MySQL_cmd.CommandText = "DELETE FROM violations WHERE inspection=" + inspection_id;
                    MySQL_cmd.ExecuteNonQuery();
                    InsertDataGr1();
                    MySQL_cmd.CommandText = "DELETE FROM eliminations WHERE inspection=" + inspection_id;
                    MySQL_cmd.ExecuteNonQuery();
                    InsertDataGr2();
                    MySQL_cmd.CommandText = "DELETE FROM inspection_group WHERE inspection=" + inspection_id;
                    MySQL_cmd.ExecuteNonQuery();
                    InsertGroup();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка MySQL");
                    return false;
                }
            }
            return true;
        }
        public void Load()
        {
            try
            {
                MySQL_cmd.CommandText = "SELECT * FROM inspections WHERE _id=" + inspection_id;
                MySqlDataReader MySQL_DR = MySQL_cmd.ExecuteReader();
                while (MySQL_DR.Read())
                {
                    Number = MySQL_DR.GetString("protocol");
                    Name = MySQL_DR.GetString("name");
                    InspectionDate = MySQL_DR.GetDateTime("_date");
                    reason = MySQL_DR.GetInt32("reason");
                    Group.Director = MySQL_DR.GetInt32("director");
                    period.Begin = MySQL_DR.GetDateTime("period_begin");
                    periodEnd = MySQL_DR.GetDateTime("period_end");
                    Comment = MySQL_DR.GetString("_comment");
                }
                MySQL_DR.Close();
                MySQL_cmd.CommandText = "SELECT * FROM violations WHERE inspection=" + inspection_id;
                MySQL_DR = MySQL_cmd.ExecuteReader();
                int by = period.Begin.Year;
                while (MySQL_DR.Read())
                {
                    for (int i = 0; i < 16; i++)
                    {
                        proto1data[MySQL_DR.GetInt32(2) - by][MySQL_DR.GetInt32(1) - 1, i] = MySQL_DR.GetDouble(i + 3);
                    }
                }
                MySQL_DR.Close();
                MySQL_cmd.CommandText = "SELECT * FROM eliminations WHERE inspection=" + inspection_id;
                MySQL_DR = MySQL_cmd.ExecuteReader();
                while (MySQL_DR.Read())
                {
                    for (int i = 0; i < 7; i++)
                    {
                        proto2data[MySQL_DR.GetInt32(1) - 1, i] = MySQL_DR.GetDouble(i + 2);
                    }
                }
                MySQL_DR.Close();
                MySQL_cmd.CommandText = "SELECT inspector FROM inspection_group WHERE inspection=" + inspection_id;
                MySQL_DR = MySQL_cmd.ExecuteReader();
                Group.checkedList.Clear();
                while (MySQL_DR.Read())
                {
                    Group.checkedList.Add(MySQL_DR.GetInt32("inspector"));
                }
                MySQL_DR.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
            }
        }
    }
}
