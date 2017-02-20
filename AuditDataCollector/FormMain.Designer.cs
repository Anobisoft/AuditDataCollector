namespace AuditDataCollector
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MMItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_Connect = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_User = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_ChangeUser = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_AddUser = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_Autorization = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_Proto = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_NewProto = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_Journal = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_Report = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_WhatNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Journal_listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Report_labelYear = new System.Windows.Forms.Label();
            this.Report_buttonExcel = new System.Windows.Forms.Button();
            this.Report_comboBoxYear = new System.Windows.Forms.ComboBox();
            this.Journal_contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.Journal_contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItem_File,
            this.MMItem_User,
            this.MMItem_Proto,
            this.MMItem_Report,
            this.MMItem_Help});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(840, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "MainMenu";
            // 
            // MMItem_File
            // 
            this.MMItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItem_Connect,
            this.MMItem_Options,
            this.MMItem_Exit});
            this.MMItem_File.Name = "MMItem_File";
            this.MMItem_File.Size = new System.Drawing.Size(48, 20);
            this.MMItem_File.Text = "Файл";
            // 
            // MMItem_Connect
            // 
            this.MMItem_Connect.Name = "MMItem_Connect";
            this.MMItem_Connect.Size = new System.Drawing.Size(179, 22);
            this.MMItem_Connect.Text = "Подключиться";
            this.MMItem_Connect.Click += new System.EventHandler(this.MMItem_Connect_Click);
            // 
            // MMItem_Options
            // 
            this.MMItem_Options.Name = "MMItem_Options";
            this.MMItem_Options.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.MMItem_Options.Size = new System.Drawing.Size(179, 22);
            this.MMItem_Options.Text = "Параметры";
            this.MMItem_Options.Click += new System.EventHandler(this.MMItem_Options_Click);
            // 
            // MMItem_Exit
            // 
            this.MMItem_Exit.Name = "MMItem_Exit";
            this.MMItem_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.MMItem_Exit.Size = new System.Drawing.Size(179, 22);
            this.MMItem_Exit.Text = "Выход";
            this.MMItem_Exit.Click += new System.EventHandler(this.MMItem_Exit_Click);
            // 
            // MMItem_User
            // 
            this.MMItem_User.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItem_ChangeUser,
            this.MMItem_AddUser,
            this.MMItem_Autorization});
            this.MMItem_User.Name = "MMItem_User";
            this.MMItem_User.Size = new System.Drawing.Size(96, 20);
            this.MMItem_User.Text = "Пользователь";
            // 
            // MMItem_ChangeUser
            // 
            this.MMItem_ChangeUser.Enabled = false;
            this.MMItem_ChangeUser.Name = "MMItem_ChangeUser";
            this.MMItem_ChangeUser.Size = new System.Drawing.Size(204, 22);
            this.MMItem_ChangeUser.Text = "Сменить пользователя";
            this.MMItem_ChangeUser.Click += new System.EventHandler(this.MMItem_ChangeUser_Click);
            // 
            // MMItem_AddUser
            // 
            this.MMItem_AddUser.Enabled = false;
            this.MMItem_AddUser.Name = "MMItem_AddUser";
            this.MMItem_AddUser.Size = new System.Drawing.Size(204, 22);
            this.MMItem_AddUser.Text = "Добавить пользователя";
            this.MMItem_AddUser.Click += new System.EventHandler(this.MMItem_AddUser_Click);
            // 
            // MMItem_Autorization
            // 
            this.MMItem_Autorization.Name = "MMItem_Autorization";
            this.MMItem_Autorization.Size = new System.Drawing.Size(204, 22);
            this.MMItem_Autorization.Text = "Авторизация";
            this.MMItem_Autorization.Click += new System.EventHandler(this.MMItem_Autorization_Click);
            // 
            // MMItem_Proto
            // 
            this.MMItem_Proto.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItem_NewProto,
            this.MMItem_Journal});
            this.MMItem_Proto.Enabled = false;
            this.MMItem_Proto.Name = "MMItem_Proto";
            this.MMItem_Proto.Size = new System.Drawing.Size(74, 20);
            this.MMItem_Proto.Text = "Протокол";
            // 
            // MMItem_NewProto
            // 
            this.MMItem_NewProto.Name = "MMItem_NewProto";
            this.MMItem_NewProto.Size = new System.Drawing.Size(168, 22);
            this.MMItem_NewProto.Text = "Новый протокол";
            this.MMItem_NewProto.Click += new System.EventHandler(this.MMItem_NewProto_Click);
            // 
            // MMItem_Journal
            // 
            this.MMItem_Journal.Name = "MMItem_Journal";
            this.MMItem_Journal.Size = new System.Drawing.Size(168, 22);
            this.MMItem_Journal.Text = "Журнал";
            this.MMItem_Journal.Click += new System.EventHandler(this.MMItem_Journal_Click);
            // 
            // MMItem_Report
            // 
            this.MMItem_Report.Enabled = false;
            this.MMItem_Report.Name = "MMItem_Report";
            this.MMItem_Report.Size = new System.Drawing.Size(51, 20);
            this.MMItem_Report.Text = "Отчет";
            this.MMItem_Report.Click += new System.EventHandler(this.MMItem_Report_Click);
            // 
            // MMItem_Help
            // 
            this.MMItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItem_WhatNew,
            this.помощьToolStripMenuItem,
            this.MMItem_About});
            this.MMItem_Help.Name = "MMItem_Help";
            this.MMItem_Help.Size = new System.Drawing.Size(65, 20);
            this.MMItem_Help.Text = "Справка";
            // 
            // MMItem_WhatNew
            // 
            this.MMItem_WhatNew.Name = "MMItem_WhatNew";
            this.MMItem_WhatNew.Size = new System.Drawing.Size(152, 22);
            this.MMItem_WhatNew.Text = "Что нового?";
            this.MMItem_WhatNew.Click += new System.EventHandler(this.MMItem_WhatNew_Click);
            // 
            // MMItem_About
            // 
            this.MMItem_About.Name = "MMItem_About";
            this.MMItem_About.Size = new System.Drawing.Size(152, 22);
            this.MMItem_About.Text = "О программе";
            this.MMItem_About.Click += new System.EventHandler(this.MMItem_About_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 490);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(840, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(64, 17);
            this.statusLabel.Text = "Ожидание";
            // 
            // progressBar
            // 
            this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(500, 16);
            this.progressBar.Step = 1;
            this.progressBar.Visible = false;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Enabled = false;
            this.tabControl.Location = new System.Drawing.Point(0, 23);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(842, 468);
            this.tabControl.TabIndex = 2;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            this.tabControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tabControl_KeyUp);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.Journal_listView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(834, 442);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Журнал";
            // 
            // Journal_listView
            // 
            this.Journal_listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.Journal_listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Journal_listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Journal_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.Journal_listView.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Journal_listView.FullRowSelect = true;
            this.Journal_listView.GridLines = true;
            this.Journal_listView.Location = new System.Drawing.Point(0, 0);
            this.Journal_listView.MultiSelect = false;
            this.Journal_listView.Name = "Journal_listView";
            this.Journal_listView.Size = new System.Drawing.Size(839, 457);
            this.Journal_listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.Journal_listView.TabIndex = 0;
            this.Journal_listView.UseCompatibleStateImageBehavior = false;
            this.Journal_listView.View = System.Windows.Forms.View.Details;
            this.Journal_listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.Journal_listView_ColumnClick);
            this.Journal_listView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tabControl_KeyUp);
            this.Journal_listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Journal_listViewItem_MouseDoubleClick);
            this.Journal_listView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Journal_listView_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "№Протокола";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Дата";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Руководитель";
            this.columnHeader4.Width = 240;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Наименование проверки";
            this.columnHeader5.Width = 420;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.dateTimePicker2);
            this.tabPage2.Controls.Add(this.dataGridView);
            this.tabPage2.Controls.Add(this.dateTimePicker1);
            this.tabPage2.Controls.Add(this.Report_labelYear);
            this.tabPage2.Controls.Add(this.Report_buttonExcel);
            this.tabPage2.Controls.Add(this.Report_comboBoxYear);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(834, 442);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Отчет";
            // 
            // Report_labelYear
            // 
            this.Report_labelYear.AutoSize = true;
            this.Report_labelYear.Location = new System.Drawing.Point(8, 20);
            this.Report_labelYear.Name = "Report_labelYear";
            this.Report_labelYear.Size = new System.Drawing.Size(28, 13);
            this.Report_labelYear.TabIndex = 2;
            this.Report_labelYear.Text = "Год:";
            // 
            // Report_buttonExcel
            // 
            this.Report_buttonExcel.Location = new System.Drawing.Point(139, 7);
            this.Report_buttonExcel.Name = "Report_buttonExcel";
            this.Report_buttonExcel.Size = new System.Drawing.Size(373, 38);
            this.Report_buttonExcel.TabIndex = 1;
            this.Report_buttonExcel.Text = "Сформировать годовой отчет в электронной таблице MS Excel";
            this.Report_buttonExcel.UseVisualStyleBackColor = true;
            this.Report_buttonExcel.Click += new System.EventHandler(this.Report_buttonExcel_Click);
            // 
            // Report_comboBoxYear
            // 
            this.Report_comboBoxYear.FormattingEnabled = true;
            this.Report_comboBoxYear.Location = new System.Drawing.Point(42, 17);
            this.Report_comboBoxYear.Name = "Report_comboBoxYear";
            this.Report_comboBoxYear.Size = new System.Drawing.Size(91, 21);
            this.Report_comboBoxYear.TabIndex = 0;
            // 
            // Journal_contextMenu
            // 
            this.Journal_contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditToolStripMenuItem,
            this.DeleteToolStripMenuItem});
            this.Journal_contextMenu.Name = "contextMenuStrip1";
            this.Journal_contextMenu.Size = new System.Drawing.Size(155, 48);
            this.Journal_contextMenu.Text = "Journal_contextMenu";
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.EditToolStripMenuItem.Text = "Редактировать";
            this.EditToolStripMenuItem.Click += new System.EventHandler(this.Journal_contextMenuItem_Edit_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.DeleteToolStripMenuItem.Text = "Удалить";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.Journal_contextMenuItem_Delete_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(218, 51);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(11, 103);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(813, 333);
            this.dataGridView.TabIndex = 4;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(218, 77);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(201, 46);
            this.button1.TabIndex = 6;
            this.button1.Text = "Сводные данные за период";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(518, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(306, 38);
            this.button2.TabIndex = 7;
            this.button2.Text = "Количество проверок по основанию проведения";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.помощьToolStripMenuItem.Text = "Помощь";
            this.помощьToolStripMenuItem.Click += new System.EventHandler(this.помощьToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 512);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(856, 550);
            this.Name = "FormMain";
            this.Text = "Ревизор";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.Journal_contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MMItem_File;
        private System.Windows.Forms.ToolStripMenuItem MMItem_Connect;
        private System.Windows.Forms.ToolStripMenuItem MMItem_Options;
        private System.Windows.Forms.ToolStripMenuItem MMItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem MMItem_Proto;
        private System.Windows.Forms.ToolStripMenuItem MMItem_NewProto;
        private System.Windows.Forms.ToolStripMenuItem MMItem_Journal;
        private System.Windows.Forms.ToolStripMenuItem MMItem_Report;
        private System.Windows.Forms.ToolStripMenuItem MMItem_Help;
        private System.Windows.Forms.ToolStripMenuItem MMItem_WhatNew;
        private System.Windows.Forms.ToolStripMenuItem MMItem_About;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView Journal_listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ContextMenuStrip Journal_contextMenu;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MMItem_User;
        private System.Windows.Forms.ToolStripMenuItem MMItem_Autorization;
        private System.Windows.Forms.ToolStripMenuItem MMItem_ChangeUser;
        private System.Windows.Forms.ToolStripMenuItem MMItem_AddUser;
        private System.Windows.Forms.ComboBox Report_comboBoxYear;
        private System.Windows.Forms.Button Report_buttonExcel;
        private System.Windows.Forms.Label Report_labelYear;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
    }
}

