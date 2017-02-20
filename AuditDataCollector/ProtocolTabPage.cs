using System;
using System.Drawing;
using System.Windows.Forms;

namespace AuditDataCollector
{
    class ProtocolTabPage
    {
        public TabPage tabPage;
        private SplitContainer splitContainer;
        private Label labelNumber;
        private TextBox textBoxNumber;
        private Label labelName;
        private TextBox textBoxName;
        private Button buttonGroup;
        private GroupBox groupBox1;
        private ListBox listBox1;
        private GroupBox groupBox2;
        private ListBox listBox2;

        private Label labelReason;
        private Label labelDash;
        private ComboBox comboBoxReason;
        private GroupBox groupBoxDate;
        private DateTimePicker dateTimePicker1;
        private GroupBox groupBoxPeriod;
        private DateTimePicker dateTimePicker3;
        private DateTimePicker dateTimePicker2;
        private Label labelGrid;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridView dataGridView;
        private DataGridViewCellStyle dataGridViewCellStyle;
        private Button buttonComment;
        private Button buttonComplite;
        private Button buttonSave;
        private Button buttonCancel;

        private int owner;
        FormMain FM;
        ProtocolDataCollector Protocol;
        struct SelectedItem
        {
            public int Index;
            public bool FirstGroup;
            public SelectedItem(bool first)
            {
                Index = 0;
                FirstGroup = first;
            }
        }
        SelectedItem selectedItem;
        FormGroup formGroup;

        public ProtocolTabPage(FormMain fmref, int id_inspection)
        {
            this.FM = fmref;
            this.owner = FM.Autorized_Inspector_ID;
            selectedItem = new SelectedItem(true);
            formGroup = new FormGroup(FM.MySQL_cmd);
            this.Protocol = new ProtocolDataCollector(FM.MySQL_cmd, formGroup);
            Protocol.Inspector = owner;
            Protocol.Inspection = id_inspection;
            if (id_inspection != 0) Protocol.Load();
            
            this.tabPage = new TabPage();
            this.tabPage.Tag = id_inspection;
            this.splitContainer = new SplitContainer();
            this.labelNumber = new Label();
            this.labelName = new Label();
            this.textBoxNumber = new TextBox();
            this.textBoxName = new TextBox();
            this.buttonGroup = new Button();
            this.groupBox1 = new GroupBox();
            this.listBox1 = new ListBox();
            this.groupBox2 = new GroupBox();
            this.listBox2 = new ListBox();

            this.labelReason = new Label();
            this.comboBoxReason = new ComboBox();
            this.groupBoxDate = new GroupBox();
            this.dateTimePicker1 = new DateTimePicker();
            this.groupBoxPeriod = new GroupBox();
            this.dateTimePicker2 = new DateTimePicker();
            this.labelDash = new Label();
            this.dateTimePicker3 = new DateTimePicker();
            this.labelGrid = new Label();
            this.dataGridView = new DataGridView();
            this.dataGridViewCellStyle = new DataGridViewCellStyle();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.buttonComment = new Button();
            this.buttonComplite = new Button();
            this.buttonSave = new Button();
            this.buttonCancel = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxDate.SuspendLayout();
            this.groupBoxPeriod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();

            ConfigControls();

            this.tabPage.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBoxDate.ResumeLayout(false);
            this.groupBoxPeriod.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
        }
        private void ConfigControls()
        {
            //tabIndex
            this.tabPage.TabIndex = 1;
            this.splitContainer.TabIndex = 2;
            this.labelNumber.TabIndex = 3;
            this.labelName.TabIndex = 4;
            this.textBoxNumber.TabIndex = 5;
            this.textBoxName.TabIndex = 6;
            this.buttonGroup.TabIndex = 7;
            this.groupBox1.TabIndex = 8;
            this.listBox1.TabIndex = 0;
            this.groupBox2.TabIndex = 9;
            this.listBox2.TabIndex = 0;

            this.labelReason.TabIndex = 10;
            this.comboBoxReason.TabIndex = 11;
            this.dateTimePicker1.TabIndex = 12;
            this.dateTimePicker2.TabIndex = 13;
            this.dateTimePicker3.TabIndex = 14;
            this.dataGridView.TabIndex = 15;
            this.buttonComment.TabIndex = 16;
            this.buttonComplite.TabIndex = 17;
            this.buttonCancel.TabIndex = 18;
            this.buttonSave.TabIndex = 19;

            // 
            // tabPage
            //
            this.tabPage.BackColor = SystemColors.Control;
            this.tabPage.Controls.Add(this.splitContainer);
            this.tabPage.Location = new Point(4, 22);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new Size(824, 442);
            this.tabPage.Text = "Протокол";
            this.tabPage.Layout += new LayoutEventHandler(tabPage_Layout);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.splitContainer.FixedPanel = FixedPanel.Panel2;
            this.splitContainer.Location = new Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new Size(824, 442);
            this.splitContainer.SplitterDistance = 314;
            this.splitContainer.SplitterWidth = 2;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.labelNumber);
            this.splitContainer.Panel1.Controls.Add(this.textBoxNumber);
            this.splitContainer.Panel1.Controls.Add(this.labelName);
            this.splitContainer.Panel1.Controls.Add(this.textBoxName);
            this.splitContainer.Panel1.Controls.Add(this.buttonGroup);
            this.splitContainer.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer.Panel1MinSize = 316;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.labelReason);
            this.splitContainer.Panel2.Controls.Add(this.comboBoxReason);
            this.splitContainer.Panel2.Controls.Add(this.groupBoxDate);
            this.splitContainer.Panel2.Controls.Add(this.groupBoxPeriod);
            this.splitContainer.Panel2.Controls.Add(this.labelGrid);
            this.splitContainer.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer.Panel2.Controls.Add(this.buttonCancel);
            this.splitContainer.Panel2.Controls.Add(this.buttonComment);
            this.splitContainer.Panel2.Controls.Add(this.buttonComplite);
            this.splitContainer.Panel2.Controls.Add(this.buttonSave);
            this.splitContainer.Panel2MinSize = 506;
            // 
            // label1
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Location = new Point(0, 0);
            this.labelNumber.Name = "label1";
            this.labelNumber.Size = new Size(76, 13);
            this.labelNumber.Text = "№ Протокола";
            // 
            // label2
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new Point(80, 0);
            this.labelName.Name = "label2";
            this.labelName.Size = new Size(134, 13);
            this.labelName.Text = "Наименование проверки";
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new Point(0, 16);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new Size(73, 20);
            this.textBoxNumber.TextChanged += new EventHandler(this.textBoxNumber_TextChanged);
            this.textBoxNumber.Text = Protocol.Number;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right));
            this.textBoxName.Location = new Point(80, 16);
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new Size(231, 69);
            this.textBoxName.Text = Protocol.Name;
            // 
            // buttonGroup
            // 
            this.buttonGroup.Location = new Point(-1, 42);
            this.buttonGroup.Name = "buttonGroup";
            this.buttonGroup.Size = new Size(75, 44);
            this.buttonGroup.Text = "Состав группы";
            this.buttonGroup.UseVisualStyleBackColor = true;
            this.buttonGroup.Click += new EventHandler(buttonGroup_Click);

            //Заголовки контролов цвет
            this.groupBox1.ForeColor =
                this.groupBox2.ForeColor =
                this.groupBoxDate.ForeColor =
                this.groupBoxPeriod.ForeColor =
                this.labelName.ForeColor =
                this.labelNumber.ForeColor =
                this.labelReason.ForeColor =
                this.labelGrid.ForeColor = SystemColors.ActiveCaption;

            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new Point(0, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(311, 234);
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Нарушения ";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.listBox1.BackColor = SystemColors.Control;
            this.listBox1.BorderStyle = BorderStyle.None;
            this.listBox1.Cursor = Cursors.Hand;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Недостача денежных средств",
            "Недостача ТМЦ",
            "Излишки денежных средств",
            "Излишки ТМЦ",
            "Нецелевые расходы",
            "Неэффективное использование бюджетных средств",
            "Неправильное списание, расходование денежных средств",
            "Неправильное списание, расходование материальных запасов, в том числе ГСМ",
            "Неправильная выплата (переплата) зарплаты, авансов, премий, отпускных, мат.помощи",
            "Недоплата зарплаты, премий, мат.помощи",
            "Недопоступление доходов",
            "Завышение СМР",
            "Прочие Финансовые нарушения",
            "Прочие Нефинансовые нарушения",
            "Нарушения Федерального закона №94-ФЗ",
            "Общий объем проверенных средств"});
            this.listBox1.Location = new Point(4, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(303, 208);
            this.listBox1.SelectedIndex = 0;
            this.listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Location = new Point(0, 325);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(311, 116);
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Устранение ";
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.listBox2.BackColor = SystemColors.Control;
            this.listBox2.BorderStyle = BorderStyle.None;
            this.listBox2.Cursor = Cursors.Hand;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Items.AddRange(new object[] {
            "Устранено нарушений: зачет нецелевых",
            "Устранено нарушений: возмещено в бюджеты",
            "Внесены изменения в РЦП",
            "Неустранимые",
            "Не устранено",
            "Наложены штрафы",
            "Уплачены штрафы"});
            this.listBox2.Location = new Point(2, 19);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new Size(305, 91);
            this.listBox2.SelectedIndexChanged += new EventHandler(listBox2_SelectedIndexChanged);
            // 
            // label3
            // 
            this.labelReason.AutoSize = true;
            this.labelReason.Location = new Point(0, 0);
            this.labelReason.Name = "label3";
            this.labelReason.Size = new Size(114, 13);
            this.labelReason.Text = "Основание проверки";
            // 
            // comboBoxReason
            // 
            this.comboBoxReason.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            this.comboBoxReason.FormattingEnabled = true;
            this.comboBoxReason.Items.AddRange(new object[] {
            "Плановая проверка",
            "По заданию (постановлению, распоряжению) Правительства РТ",
            "По обращению правоохранительных органов",
            "По обращениям граждан, включая обращения должностных лиц и бюджетных учреждений и организаций"});
            this.comboBoxReason.Location = new Point(1, 16);
            this.comboBoxReason.Name = "comboBoxReason";
            this.comboBoxReason.Size = new Size(500, 21);
            this.comboBoxReason.SelectedValueChanged += new EventHandler(comboBoxReason_SelectedValueChanged);
            this.comboBoxReason.SelectedIndex = Protocol.Reason;
            // 
            // dateTimePicker123
            // 
            this.groupBoxDate.Controls.Add(this.dateTimePicker1);
            this.groupBoxDate.Text = "Дата проверки";
            this.groupBoxDate.Location = new Point(1, 40);
            this.groupBoxDate.Size = new Size(166, 46);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Location = new Point(8, 17);
            this.dateTimePicker1.Size = new Size(150, 20);

            this.groupBoxPeriod.Controls.Add(this.dateTimePicker2);
            this.groupBoxPeriod.Controls.Add(this.dateTimePicker3);
            this.groupBoxPeriod.Controls.Add(this.labelDash);
            this.groupBoxPeriod.Location = new Point(179, 40);
            this.groupBoxPeriod.Size = new Size(322, 46);
            this.groupBoxPeriod.Text = "Проверяемый период";
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker2.Size = new Size(145, 20);
            this.dateTimePicker3.Size = new Size(145, 20);
            this.dateTimePicker2.Location = new Point(8, 17);
            this.dateTimePicker3.Location = new Point(169, 17);
            this.labelDash.Name = "label6";
            this.labelDash.Text = "-";
            this.labelDash.AutoSize = true;
            this.labelDash.Location = new Point(156, 20);

            this.dateTimePicker1.MaxDate = DateTime.Now;
            this.dateTimePicker2.MaxDate = DateTime.Now;
            this.dateTimePicker3.MaxDate = DateTime.Now;
            this.dateTimePicker1.MinDate = DateTime.Parse("01.01.1994");
            this.dateTimePicker2.MinDate = DateTime.Parse("01.01.1994");
            this.dateTimePicker3.MinDate = DateTime.Parse("01.01.1994");

            this.dateTimePicker1.Value = Protocol.InspectionDate;
            this.dateTimePicker2.Value = Protocol.periodBegin;
            this.dateTimePicker3.Value = Protocol.periodEnd;

            this.dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            this.dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            this.dateTimePicker3.ValueChanged += new EventHandler(dateTimePicker3_ValueChanged);
            // 
            // labelGrid
            // 
            this.labelGrid.AutoSize = true;
            this.labelGrid.Location = new Point(1, 89);
            this.labelGrid.Name = "labelGrid";
            labelGrid.Text = listBox1.Items[0].ToString();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = SystemColors.Control;
            this.dataGridView.BorderStyle = BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4, this.Column5 });
            this.dataGridView.Location = new Point(1, 108);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 60;
            this.dataGridView.Size = new Size(500, 305);
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.CellEndEdit += new DataGridViewCellEventHandler(dataGridView_CellEndEdit);
            
            dataGridViewCellStyle.Format = "N2";
            dataGridViewCellStyle.NullValue = "0,00";
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle;

            setYears2Grid(selectedItem.FirstGroup);
            Protocol.LoadData1(selectedItem.Index, this.dataGridView.Rows);
            // 
            // Columns
            // 
            this.Column1.HeaderText = "Федеральный";
            this.Column1.Name = "Column1";
            this.Column2.HeaderText = "Региональный";
            this.Column2.Name = "Column2";
            this.Column3.HeaderText = "Местный";
            this.Column3.Name = "Column3";
            this.Column4.HeaderText = "Прочее";
            this.Column4.Name = "Column4";
            this.Column5.HeaderText = "Сумма";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column1.SortMode = 
                this.Column2.SortMode =
                this.Column3.SortMode = 
                this.Column4.SortMode = 
                this.Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width =
                this.Column2.Width = 
                this.Column3.Width = 
                this.Column4.Width = 
                this.Column5.Width = 88;
            // 
            // buttonComment
            // 
            this.buttonComment.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.buttonComment.Location = new Point(1, 419);
            this.buttonComment.Name = "buttonComment";
            this.buttonComment.Size = new Size(101, 23);
            this.buttonComment.Text = "Примечание";
            this.buttonComment.UseVisualStyleBackColor = true;
            this.buttonComment.Click += new EventHandler(buttonComment_Click);
            // 
            // buttonComplite
            // 
            this.buttonComplite.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.buttonComplite.Location = new Point(267, 419);
            this.buttonComplite.Name = "buttonCancel";
            this.buttonComplite.Size = new Size(75, 23);
            this.buttonComplite.Text = "Готово";
            this.buttonComplite.UseVisualStyleBackColor = true;
            this.buttonComplite.Click += new EventHandler(this.buttonComplite_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.buttonCancel.Location = new Point(347, 419);
            this.buttonCancel.Name = "buttonSave";
            this.buttonCancel.Size = new Size(75, 23);
            this.buttonCancel.Text = "Закрыть";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new EventHandler(buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.buttonSave.Location = new Point(427, 419);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new Size(75, 23);
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new EventHandler(buttonSave_Click);

            
        }

        void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void buttonGroup_Click(object sender, EventArgs e)
        {
            formGroup.ShowDialog();
        }

        void comboBoxReason_SelectedValueChanged(object sender, EventArgs e)
        {
            Protocol.Reason = comboBoxReason.SelectedIndex;
        }

        void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    double x = double.Parse(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    if (x < 0) dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0,00";
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат", "Ошибка!");
                dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0,00";
            }
            double summ = 0;
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    double x = double.Parse(dataGridView.Rows[e.RowIndex].Cells[i].Value.ToString());
                    summ += x;
                }
                catch { }
            }
            dataGridView.Rows[e.RowIndex].Cells[4].Value = summ;
        }

        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex != -1)
            {
                if (selectedItem.FirstGroup)
                {
                    Protocol.SaveData1(selectedItem.Index, dataGridView.Rows);
                }
                else
                {
                    Protocol.SaveData2(selectedItem.Index, this.dataGridView.Rows[0].Cells);
                    this.listBox2.SelectedIndex = -1;
                    setYears2Grid(true);
                }
                selectedItem.Index = this.listBox1.SelectedIndex;
                selectedItem.FirstGroup = true;
                this.dataGridView.RowCount = Protocol.YearsCount;
                Protocol.LoadData1(selectedItem.Index, this.dataGridView.Rows);
                labelGrid.Text = listBox1.Items[selectedItem.Index].ToString();
            }
        }
        void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox2.SelectedIndex != -1)
            {
                if (selectedItem.FirstGroup)
                {
                    Protocol.SaveData1(selectedItem.Index, dataGridView.Rows);
                    this.listBox1.SelectedIndex = -1;
                    this.dataGridView.RowCount = 1;
                    this.dataGridView.Rows[0].HeaderCell.Value = "Все";
                }
                else
                {
                    Protocol.SaveData2(selectedItem.Index, this.dataGridView.Rows[0].Cells);
                }
                selectedItem.Index = this.listBox2.SelectedIndex;
                selectedItem.FirstGroup = false;
                Protocol.LoadData2(selectedItem.Index, this.dataGridView.Rows[0].Cells);
                labelGrid.Text = listBox2.Items[selectedItem.Index].ToString();
            }
        }

        void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dateTimePicker1.Value < this.dateTimePicker3.MinDate)
                    this.dateTimePicker3.MinDate = this.dateTimePicker1.Value;
                this.dateTimePicker2.MaxDate = this.dateTimePicker3.MaxDate = this.dateTimePicker1.Value;
                Protocol.InspectionDate = dateTimePicker1.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void setYears2Grid(bool fg)
        {
            if (fg)
            {
                this.dataGridView.RowCount = Protocol.YearsCount;
                int by = Protocol.periodBegin.Year, ey = Protocol.periodEnd.Year;
                for (int i = by; i <= ey; i++)
                {
                    this.dataGridView.Rows[i - by].HeaderCell.Value = i.ToString();
                }
            }
            else
            {
                this.dataGridView.RowCount = 1;
                this.dataGridView.Rows[0].HeaderCell.Value = "Все";
            }
        }
        void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker3.MinDate = this.dateTimePicker2.Value;
            Protocol.periodBegin = this.dateTimePicker2.Value;
            setYears2Grid(selectedItem.FirstGroup);
        }
        void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            Protocol.periodEnd = this.dateTimePicker3.Value;
            setYears2Grid(selectedItem.FirstGroup);
        }

        void tabPage_Layout(object sender, LayoutEventArgs e)
        {
            if (FM.Autorized_Inspector_ID != owner) this.tabPage.Hide();
        }

        public void Close()
        {
            TabControl tabControl = (TabControl)(this.tabPage.Parent);
            tabControl.TabPages.Remove(this.tabPage);
        }

        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {
            this.tabPage.Text = "Протокол " + this.textBoxNumber.Text;
        }

        private void buttonComplite_Click(object sender, EventArgs e)
        {
            Protocol.Number = textBoxNumber.Text;
            Protocol.Name = textBoxName.Text;
            if (selectedItem.FirstGroup)
            {
                Protocol.SaveData1(selectedItem.Index, dataGridView.Rows);
            }
            else
            {
                Protocol.SaveData2(selectedItem.Index, this.dataGridView.Rows[0].Cells);
            }
            if (Protocol.Save()) this.Close();
        }
        void buttonSave_Click(object sender, EventArgs e)
        {
            Protocol.Number = textBoxNumber.Text;
            Protocol.Name = textBoxName.Text;
            if (selectedItem.FirstGroup)
            {
                Protocol.SaveData1(selectedItem.Index, dataGridView.Rows);
            }
            else
            {
                Protocol.SaveData2(selectedItem.Index, this.dataGridView.Rows[0].Cells);
            }
            if (Protocol.Save()) MessageBox.Show("Все данные успешно сохранены в базе данных");
        }
        void buttonComment_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Здесь можно будет оставить комментарий.");
        }

    }
}
