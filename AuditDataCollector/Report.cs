using System;
using System.Windows.Forms;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;


namespace AuditDataCollector
{
    public partial class FormMain : Form
    {
        static string defaultFormat = "#,##0.00_);[Red]!!! (-#,##0.00); ";

        private XlBordersIndex Border(int x)
        {
            XlBordersIndex r = new XlBordersIndex();
            switch (x)
            {
                case 07: r = XlBordersIndex.xlEdgeLeft; break;
                case 08: r = XlBordersIndex.xlEdgeTop; break;
                case 09: r = XlBordersIndex.xlEdgeBottom; break;
                case 10: r = XlBordersIndex.xlEdgeRight; break;
                case 11: r = XlBordersIndex.xlInsideVertical; break;
                case 12: r = XlBordersIndex.xlInsideHorizontal; break;
            }
            return r;
        }

        private string ToLetter(int x)
        {
            char c1 = (char)((x - 1) % 26 + 65);
            char c2 = (char)((x - 1) / 26 + 64);
            if (c2 == 64) return c1.ToString();
            else return c2.ToString() + c1.ToString();
        }


        /*/
        //int xtimer;

        private int timer
        {
            get
            {
                return Environment.TickCount - xtimer;
            }
            set
            {
                xtimer = Environment.TickCount + value;
            }
        }
        /*/

        private void Report(string YEAR)
        {
            string saveStatus = statusLabel.Text;
            int NY = 0;
            int[] YearIndex = new int[100];
            int[] colors = { 17, 38, 35, 39 }; // Бюджеты {15455409, 12303591, 12445144, 14731988};
            //System.IO.StreamWriter LogFile;
            int[] progressTArr1 = {34, 9, 7, 20, 5, 22, 4, 30, 25};
            int[] progressTArr2 = {4, 10, 2, 13, 6, 12};

            List<string> budgets = new List<string>();
            budgets.AddRange(new string[] {
                    "Сумма",
                    "Федеральный",
                    "Региональный",
                    "Местный",
                    "Прочее"
                    });

            List<string> reasons = new List<string>();
            reasons.AddRange(new string[] {
                    "Плановая проверка",
                    "По заданию (постановлению, распоряжению) Правительства РТ",
                    "По обращению правоохранительных органов",
                    "По обращениям граждан, включая обращения должностных лиц и бюджетных учреждений и организаций"
                });

            List<string> violations = new List<string>();
            violations.AddRange(new string[] {
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
                    "Нарушения Федерального закона №94-ФЗ"
                });

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
                "Уплачено штрафов" } );

            // Выбираем нужные годы из базы, составляем список и массив индексов. Для удобства NY сохраняет количество годов + 1 
            List<int> Years = new List<int>();
            try
            {
                MySQL_cmd.CommandText = "SELECT _year FROM violations WHERE inspection IN "+
                    "(SELECT _id FROM inspections WHERE YEAR(_date)=" + YEAR + ") GROUP BY _year ORDER BY _year ASC";
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                for (NY = 1; MySQL_Reader.Read(); NY++)
                {
                    int year = MySQL_Reader.GetInt32("_year");
                    Years.Add(year);
                    YearIndex[year - 1994] = NY;
                }
                MySQL_Reader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
            }
            //Выбираем количество протоколов за год
            int CNT = 1;
            try
            {
                MySQL_cmd.CommandText = "SELECT COUNT(_id) FROM inspections WHERE YEAR(_date)=" + YEAR;
                CNT = int.Parse(MySQL_cmd.ExecuteScalar().ToString());
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка MySQL");
            }

            statusLabel.Text = "Экспорт в Excel: ";
            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Maximum = 0;
            for (int i = 0; i < progressTArr1.Length; i++)
            {
                progressBar.Maximum += progressTArr1[i];
            }
            for (int i = 0; i < progressTArr2.Length; i++)
            {
                progressBar.Maximum += progressTArr2[i] * (CNT + 1);
            }
            progressBar.Maximum -= progressTArr2[0];
            progressBar.Maximum -= progressTArr2[1];

            try
            {
                // Открываем лог файл
                /*/
                try
                {
                    LogFile = new System.IO.StreamWriter("A:\\auditdc_report.log", true);
                    LogFile.WriteLine("---=* " + DateTime.Now.ToString() + " *=----------------");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка записи лога");
                }
                /*/

                // Открываем Excel
                //LogFile.WriteLine("Открываем Excel"); timer = 0;
                Excel.Application ExcelApp = new Excel.Application();
                ExcelApp.Visible = false;
                ExcelApp.SheetsInNewWorkbook = 3;
                progressBar.Value += progressTArr1[0];
                //LogFile.WriteLine(timer);
                // Добавляем книгу
                //LogFile.WriteLine("Добавляем книгу"); timer = 0;
                ExcelApp.Workbooks.Add();
                ExcelApp.Calculation = XlCalculation.xlCalculationManual;
                Excel.Workbook Book = ExcelApp.Workbooks[1];
                Excel._Worksheet Sheet = Book.Worksheets[1];
                Excel.Range Cells = Sheet.Cells;
                Excel.Range Cell = Cells.Item[1, 1];
                progressBar.Value += progressTArr1[1];
                //LogFile.WriteLine(timer);
                // -------!!!------- Лист1 -------!!!------- //
                Sheet.Name = "Протоколы" + YEAR;
                Sheet.Activate();
                // Ячейки по умолчанию
                //LogFile.WriteLine("Форматируем ячейки по умолчанию"); timer = 0;
                Cells.Font.Size = 8;
                Cells.Font.Name = "Times New Roman";
                Cells.VerticalAlignment = -4108;
                Cells.RowHeight = 16;
                Cells.WrapText = true;
                Cells.ColumnWidth = 10.2;
                Cells.NumberFormat = defaultFormat;
                progressBar.Value += progressTArr1[2];
                //LogFile.WriteLine(timer);

                // --- ШАПКА --------------------------------------------------------------------------=* !!!!!!!!!!!!!!!!!!!! 
                // Высота шапки + 1 для удобства
                int HeadHeight = 4;
                // Высота второй строки 24 (по умолчанию 16)
                Cells.Item[2, 1].RowHeight = 24;
                // Строка с годами из нешего списка
                //LogFile.WriteLine("Выводим строку с годами");
                for (int i = 0; i < 19; i++)
                {
                    Cells.Item[3, 8 + i * NY].Value = "Всего";
                    for (int j = 0; j < Years.Count; j++)
                    {
                        Cell = Cells.Item[3, 9 + i * NY + j];
                        Cell.Value = Years[j];
                        Cell.NumberFormat = "####";
                    }
                }
                // Объединение пустот (там где нет годов - пустоты)
                Sheet.Range["A3:G3"].Merge();
                Sheet.Range[ToLetter(19 * NY + 8) + "3:" + ToLetter(19 * NY + 16) + "3"].Merge();
                progressBar.Value += progressTArr1[3];
                //LogFile.WriteLine(timer);

                // Статичные заголовки
                //LogFile.WriteLine("Статичные заголовки"); timer = 0;

                // Объединяем первые семь пар ячеек (по вертикали: строки 1 и 2) для заголовков полей
                for (int i = 1; i < 8; i++)
                {
                    Sheet.Range[ToLetter(i) + "1:" + ToLetter(i) + "2"].Merge();
                }
                Cell = Cells.Item[1, 1];
                Cell.Value = "№ п/п";
                Cell.ColumnWidth = 7;

                Cells.Item[1, 2] = "№ дела";

                Cell = Cells.Item[1, 3];
                Cell.Value = "Наименование проверки";
                Cell.ColumnWidth = 40;

                Cell = Cells.Item[1, 4];
                Cell.Value = "Основание проверки";
                Cell.ColumnWidth = 20;

                Cell = Cells.Item[1, 5];
                Cell.Value = "Руководитель ревизионной группы";
                Cell.ColumnWidth = 20;

                Cell = Cells.Item[1, 6];
                Cell.Value = "Проверяемый период";
                Cell.ColumnWidth = 20;

                Cell = Cells.Item[1, 7];
                Cell.Value = "Уровни бюджетов";
                Cell.ColumnWidth = 10;
                progressBar.Value += progressTArr1[4];
                //LogFile.WriteLine(timer);

                // Выводим заголовки данных
                //LogFile.WriteLine("Выводим заголовки данных");

                Cells.Item[1, 8].Value = "Общий объем проверенных средств";
                Cells.Item[1, 8 + NY].Value = "Общая сумма нарушений";

                for (int i = 2; i < 17; i++)
                {
                    Cell = Cells.Item[1, 8 + i * NY];
                    Cell.Interior.ColorIndex = 20;
                    Cell.Value = violations[i - 2];
                }

                for (int i = 0; i < 19; i++)
                {
                    Sheet.Range[ToLetter(i * NY + 8) + "1:" + ToLetter(i * NY + 8 + NY - 1) + "2"].Merge();
                }

                Cell = Cells.Item[1, 17 * NY + 8];
                Cell.Value = "Всего финансовых нарушений";
                Cell.Interior.ColorIndex = 36;
                Cell = Cells.Item[1, 18 * NY + 8];
                Cell.Value = "Всего нефинансовых нарушений";
                Cell.Interior.ColorIndex = 36;

                Sheet.Range[ToLetter(19 * NY + 8) + "1:" + ToLetter(19 * NY + 10) + "1"].Merge();
                Cells.Item[1, 19 * NY + 8].Value = "Устранено нарушений";
                Cells.Item[2, 19 * NY + 8].Value = "Всего";
                Cells.Item[2, 19 * NY + 9].Value = "Зачет нецелевых";
                Cells.Item[2, 19 * NY + 10].Value = "Возмещено в бюджеты";

                for (int j = 0; j < 6; j++)
                {
                    Sheet.Range[ToLetter(19 * NY + 11 + j) + "1:" + ToLetter(19 * NY + 11 + j) + "2"].Merge();
                }

                Cell = Cells.Item[1, 19 * NY + 11];
                Cell.Value = "Внесены изменения в РЦП";
                Cell.ColumnWidth = 12;
                Cell = Cells.Item[1, 19 * NY + 12];
                Cell.Value = "Неустранимые";
                Cell.ColumnWidth = 12;
                Cell = Cells.Item[1, 19 * NY + 13];
                Cell.Value = "Не устранено";
                Cell.ColumnWidth = 12;
                Cell = Cells.Item[1, 19 * NY + 14];
                Cell.Value = "Наложены штрафы";
                Cell.ColumnWidth = 12;
                Cell = Cells.Item[1, 19 * NY + 15];
                Cell.Value = "Уплачены штрафы";
                Cell.ColumnWidth = 12;
                Cell = Cells.Item[1, 19 * NY + 16];
                Cell.Value = "Примечание";
                Cell.ColumnWidth = 56;
                progressBar.Value += progressTArr1[5];
                //LogFile.WriteLine(timer);
                // Форматирование всей шапки
                //LogFile.WriteLine("Форматирование всей шапки"); timer = 0;
                Cell = Sheet.Range["A1:" + ToLetter(19 * NY + 16) + "3"];
                Cell.Font.Bold = true;
                Cell.HorizontalAlignment = -4108;
                Cell.Borders[XlBordersIndex.xlEdgeRight].Weight = 3;
                Cell.Borders[XlBordersIndex.xlInsideHorizontal].Weight = 3;
                Cell.Borders[XlBordersIndex.xlInsideVertical].Weight = 2;
                progressBar.Value += progressTArr1[6];
                //LogFile.WriteLine(timer);
                //--- КОНЕЦ ШАПКИ ---------------------------------------------------------------------=* !!!!!!!!!!!!!!!!!!!! 



                //LogFile.WriteLine("---------------");
                // Загрузим все проверки за год
                MySQL_cmd.CommandText = "SELECT inspections._id as _id, inspections.protocol as proton, inspections.name as name, reasons.name as reason, " +
                    "CONCAT_WS(' ', inspectors.surname, inspectors.name, inspectors.patronymic) as fio, inspections.period_begin as pb, inspections.period_end as pe, inspections._comment as cmt " +
                    "FROM inspections, reasons, inspectors " +
                    "WHERE inspections.reason = reasons._id AND inspections.director = inspectors._id AND YEAR(inspections._date)=" + YEAR +
                    " ORDER BY _id";
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                // ID проверок сохраним в список
                List<int> inspections = new List<int>();
                // Вывод общей информации по проверке
                for (int xx = 0; MySQL_Reader.Read(); xx++)
                {
                    //LogFile.WriteLine("Вывод общей информации по проверке " + xx); timer = 0;
                    inspections.Add(MySQL_Reader.GetInt32("_id"));
                    for (int i = 1; i < 7; i++)
                    {
                        Cell = Sheet.Range[ToLetter(i) + (HeadHeight + xx * 5).ToString() + ":" + ToLetter(i) + (HeadHeight + xx * 5 + 4).ToString()];
                        Cell.Merge();
                        Cell.NumberFormat = "####";
                    }
                    Cells.Item[HeadHeight + xx * 5, 1].Value = MySQL_Reader["proton"];
                    for (int i = 2; i < 5; i++)
                        Cells.Item[HeadHeight + xx * 5, i + 1].Value = MySQL_Reader[i];
                    Cells.Item[HeadHeight + xx * 5, 6].Value = "c " + MySQL_Reader.GetDateTime("pb").ToShortDateString() + " по " + MySQL_Reader.GetDateTime("pe").ToShortDateString();
                    Cell = Sheet.Range[ToLetter(19 * NY + 16) + (HeadHeight + xx * 5).ToString() + ":" + ToLetter(19 * NY + 16) + (HeadHeight + xx * 5 + 4).ToString()];
                    Cell.Merge();
                    Cell.Value = MySQL_Reader["cmt"];
                    progressBar.Value += progressTArr2[0];
                    //LogFile.WriteLine(timer);
                }
                MySQL_Reader.Close();
                // А теперь для каждой проверки в списке загрузим данные
                for (int xx = 0; xx <= inspections.Count; xx++)
                {
                    // Данные протокола 
                    if (xx != inspections.Count)
                    {
                        //LogFile.WriteLine("Данные протокола " + xx); timer = 0;
                        // Данные группа 1
                        MySQL_cmd.CommandText = "SELECT * FROM violations WHERE inspection=" + inspections[xx];
                        MySQL_Reader = MySQL_cmd.ExecuteReader();
                        for (int i = 0; MySQL_Reader.Read(); i++)
                        {
                            int budget = MySQL_Reader.GetInt32("budget_lvl");
                            int year = MySQL_Reader.GetInt32("_year");
                            Cell = Cells.Item[HeadHeight + xx * 5 + budget, 8 + YearIndex[year - 1994]];
                            Cell.Value = MySQL_Reader.GetDouble(18);
                            for (int j = 2; j < 17; j++)
                            {
                                Cell = Cells.Item[HeadHeight + xx * 5 + budget, 8 + YearIndex[year - 1994] + j * NY];
                                Cell.Value = MySQL_Reader.GetDouble(j + 1);
                            }
                        }
                        MySQL_Reader.Close();
                        // Данные группа 2
                        MySQL_cmd.CommandText = "SELECT * FROM eliminations WHERE inspection=" + inspections[xx];
                        MySQL_Reader = MySQL_cmd.ExecuteReader();
                        for (int i = 0; MySQL_Reader.Read(); i++)
                        {
                            for (int j = 2; j < 9; j++)
                            {
                                int budget = MySQL_Reader.GetInt32("budget_lvl");
                                Cell = Cells.Item[HeadHeight + xx * 5 + budget, 7 + NY * 19 + j];
                                Cell.Value = MySQL_Reader.GetDouble(j);
                            }
                        }
                        MySQL_Reader.Close();
                        progressBar.Value += progressTArr2[1];
                        //LogFile.WriteLine(timer);
                    }
                    // Все что ниже применяется и к ИТОГО
                    // цвета бюджетов 
                    //LogFile.WriteLine("Бюджеты"); timer = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        Cell = Sheet.Range["G" + (xx * 5 + i + 5) + ":" + ToLetter(19 * NY + 15) + (xx * 5 + i + 5)];
                        Cell.Interior.ColorIndex = colors[i];
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        Cell = Cells.Item[HeadHeight + xx * 5 + i, 7];
                        Cell.Value = budgets[i];
                    }
                    progressBar.Value += progressTArr2[2];
                    //LogFile.WriteLine(timer);

                    // формулы СУММ #ИМЯ??? чеза!??!... видимо, плохо пережевывает русский UTF... SUM работает нормально
                    //LogFile.WriteLine("Формулы СУММ по годам и по бюджетам для нарушений"); timer = 0;
                    for (int i = 0; i < 19; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Cell = Cells.Item[HeadHeight + xx * 5 + j, 8 + i * NY];
                            Cell.Formula = "=SUM(" + ToLetter(9 + i * NY) + (HeadHeight + xx * 5 + j) + ":" + ToLetter(7 + i * NY + NY) + (HeadHeight + xx * 5 + j) + ")";
                        }
                        for (int j = 1; j < NY; j++)
                        {
                            Cell = Cells.Item[HeadHeight + xx * 5, 8 + i * NY + j];
                            Cell.Formula = "=SUM(" + ToLetter(8 + i * NY + j) + (xx * 5 + 5) + ":" + ToLetter(8 + i * NY + j) + (xx * 5 + 8) + ") ";
                        }
                    }
                    progressBar.Value += progressTArr2[3];
                    //LogFile.WriteLine(timer);

                    // Формулы для общей суммы нарушений и сумм финансовых/нефинансовых нарушений
                    //LogFile.WriteLine("Формулы для общей суммы нарушений и сумм финансовых/нефинансовых нарушений"); timer = 0;

                    for (int i = 1; i < NY; i++)
                        for (int j = 1; j < 5; j++)
                        {
                            Cell = Cells.Item[HeadHeight + xx * 5 + j, 8 + NY * 17 + i];
                            string tmp = "=";
                            for (int k = 2; k < 15; k++)
                            {
                                if (k != 2) tmp += "+";
                                tmp += ToLetter(8 + NY * k + i) + (xx * 5 + 4 + j);
                            }
                            Cell.Value = tmp;
                            Cell = Cells.Item[HeadHeight + xx * 5 + j, 8 + 18 * NY + i];
                            Cell.Value = "=" + ToLetter(8 + NY * 15 + i) + (xx * 5 + 4 + j) + "+" + ToLetter(8 + NY * 16 + i) + (xx * 5 + 4 + j);
                            Cell = Cells.Item[HeadHeight + xx * 5 + j, 8 + NY + i];
                            Cell.Value = "=" + ToLetter(8 + NY * 17 + i) + (xx * 5 + 4 + j) + "+" + ToLetter(8 + NY * 18 + i) + (xx * 5 + 4 + j);
                        }

                    for (int i = 0; i < 8; i++)
                    {
                        Cell = Cells.Item[HeadHeight + xx * 5, 8 + 19 * NY + i];
                        Cell.Value = "=SUM(" + ToLetter(8 + 19 * NY + i) + (xx * 5 + 5) + ":" + ToLetter(8 + 19 * NY + i) + (xx * 5 + 8) + ")";
                    }
                    progressBar.Value += progressTArr2[4];
                    //LogFile.WriteLine(timer);

                    // Всего устранено нарушений
                    //LogFile.WriteLine("Формулы \"Всего устранено нарушений\"");
                    for (int i = 0; i < 4; i++)
                    {
                        Cell = Cells.Item[xx * 5 + 5 + i, 8 + 19 * NY];
                        Cell.Value = "=" + ToLetter(9 + 19 * NY) + (xx * 5 + 5 + i) + "+" + ToLetter(10 + 19 * NY) + (xx * 5 + 5 + i);
                    }
                    Cell = Sheet.Range["A" + (xx * 5 + 4) + ":" + ToLetter(19 * NY + 16) + (xx * 5 + 8)];
                    Cell.Borders[XlBordersIndex.xlEdgeTop].Weight = 3;
                    Cell.Borders[XlBordersIndex.xlEdgeRight].Weight = 3;
                    Cell.Borders[XlBordersIndex.xlInsideVertical].Weight = 2;
                    Cell.Borders[XlBordersIndex.xlInsideHorizontal].Weight = 2;
                    progressBar.Value += progressTArr2[5];
                    //LogFile.WriteLine(timer);
                }
                Cell.Borders[XlBordersIndex.xlEdgeBottom].Weight = 3;

                // ИТОГО
                //LogFile.WriteLine("---- ИТОГО ----"); timer = 0;
                Cell = Sheet.Range["A" + (HeadHeight + inspections.Count * 5).ToString() + ":" + "F" + (HeadHeight + inspections.Count * 5 + 4).ToString()];
                Cell.Merge();
                Cell.Font.Bold = true;
                Cell.HorizontalAlignment = -4108;
                Cell.Value = "Итого за " + YEAR + " год";
                Cell = Sheet.Range[ToLetter(NY * 19 + 16) + (HeadHeight + inspections.Count * 5) + ":" + ToLetter(NY * 19 + 16) + (HeadHeight + inspections.Count * 5 + 4)];
                Cell.Merge();

                MySQL_cmd.CommandText = "SELECT budget_lvl, _year, SUM(d1), SUM(d2), SUM(d3), SUM(d4), SUM(d5), SUM(d6), SUM(d7), SUM(d8), SUM(d9), SUM(d10), SUM(d11), SUM(d12), SUM(d13), SUM(d14), SUM(d15), SUM(d16) FROM violations WHERE inspection IN (SELECT _id FROM inspections WHERE YEAR(_date)=" + YEAR + ") GROUP BY budget_lvl, _year";
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                while (MySQL_Reader.Read())
                {
                    int budget = MySQL_Reader.GetInt32("budget_lvl");
                    int year = MySQL_Reader.GetInt32("_year");
                    Cell = Cells.Item[HeadHeight + inspections.Count * 5 + budget, 8 + YearIndex[year - 1994]];
                    Cell.Value = MySQL_Reader.GetDouble(17);
                    for (int j = 2; j < 17; j++)
                    {
                        Cell = Cells.Item[HeadHeight + inspections.Count * 5 + budget, 8 + YearIndex[year - 1994] + j * NY];
                        Cell.Value = MySQL_Reader.GetDouble(j);
                    }
                }
                MySQL_Reader.Close();

                MySQL_cmd.CommandText = "SELECT SUM(d1), SUM(d2), SUM(d3), SUM(d4), SUM(d5), SUM(d6), SUM(d7) FROM eliminations WHERE inspection IN (SELECT _id FROM inspections WHERE YEAR(_date)=" + YEAR + ") GROUP BY budget_lvl ORDER BY budget_lvl";
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                for (int i = 1; MySQL_Reader.Read(); i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Cell = Cells.Item[HeadHeight + inspections.Count * 5 + i, 9 + 19 * NY + j];
                        Cell.Value = MySQL_Reader.GetDouble(j);
                    }
                }
                MySQL_Reader.Close();
                progressBar.Value += progressTArr1[7];

                //Заморозка шапки
                Cell = Cells.Item[4, 8];
                Cell.Select();
                ExcelApp.ActiveWindow.FreezePanes = true;

                //LogFile.WriteLine(timer);

                /* Лист 3: цвета на выбор */
                /*/
                Sheet = Book.Worksheets.Item[3];
                Sheet.Name = "Цвета";
                Sheet.Activate();
                for (int i = 1; i < 57; i++)
                {
                    Cell = Sheet.Range["A" + i + ":" + "A" + i];
                    Cell.Interior.ColorIndex = i;
                    Cell.Value = i;
                }
                /**/

                // -------!!!------- Лист2 -------!!!------- //
                //LogFile.WriteLine("-----------");
                //LogFile.WriteLine("Лист второй"); timer = 0;

                Sheet = Book.Worksheets.Item[2];
                Cells = Sheet.Cells;
                Sheet.Name = "Отчет" + YEAR;
                Sheet.Activate();

                // Формат ячеек по умолчанию
                Cells.Font.Size = 8;
                Cells.Font.Name = "Times New Roman";
                Cells.RowHeight = 18; //16?
                Cells.ColumnWidth = 10.2;
                Cells.VerticalAlignment = -4108;
                Cells.NumberFormat = defaultFormat;

                // ------- Заголовок
                Cell = Sheet.Range["A1:N1"];
                Cell.Merge();
                Cell.Borders[XlBordersIndex.xlEdgeBottom].Weight = 3;

                Cell = Cells.Item[1, 1];
                Cell.RowHeight = 30;
                Cell.HorizontalAlignment = -4108;
                Cell.VerticalAlignment = -4108;
                Cell.Font.Size = 15;
                Cell.Font.Bold = true;
                Cell.Value = "ОТЧЕТ СЛУЖБЫ ПО ФИНАНСОВО-БЮДЖЕТНОМУ НАДЗОРУ РЕСПУБЛИКИ ТЫВА ЗА " + YEAR + " ГОД";

                // ------- Таблица1
                for (int i = 3; i < 8; i++)
                {
                    Cell = Sheet.Range["A" + i + ":G" + i];
                    Cell.Merge();
                }

                //Данные
                for (int i = 0; i < 4; i++)
                {
                    Cell = Cells.Item[i + 3, 1];
                    Cell.Value = reasons[i];
                }

                MySQL_cmd.CommandText = "SELECT reason, count(_id) as cnt FROM inspections WHERE YEAR(_date)=" + YEAR + " GROUP BY reason";
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                while (MySQL_Reader.Read())
                {
                    Cell = Cells.Item[2 + MySQL_Reader.GetInt32("reason"), 8];
                    Cell.Value = MySQL_Reader["cnt"];
                }
                MySQL_Reader.Close();

                Cell = Cells.Item[7, 1];
                Cell.Value = "ВСЕГО в " + YEAR + "г. проведено проверок:";
                Cell = Cells.Item[7, 8];
                Cell.Value = "=SUM(H3:H6)";
                //Рамка
                Cell = Sheet.Range["A3:H7"];
                for (int i = 7; i < 11; i++)
                {
                    Cell.Borders[Border(i)].Weight = 3;
                }
                Cell.Borders[XlBordersIndex.xlInsideHorizontal].Weight = 2;
                Cell.Borders[XlBordersIndex.xlInsideVertical].Weight = 2;
                // ------- Таблица 2

                //Данные
                string q1 = "SELECT SUM(d1), SUM(d2), SUM(d3), SUM(d4), SUM(d5), SUM(d6), SUM(d7)";
                string q2 = ", SUM(d8), SUM(d9), SUM(d10), SUM(d11), SUM(d12), SUM(d13), SUM(d14), SUM(d15), SUM(d16) FROM violations";
                string q3 = " FROM eliminations";
                string q4 = " WHERE inspection IN (SELECT _id FROM inspections WHERE YEAR(_date)=" + YEAR + ") GROUP BY budget_lvl";

                for (int i = 1; i < 5; i++) Cells.Item[9 + i, 1].Value = budgets[i];

                MySQL_cmd.CommandText = q1 + q2 + q4;
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                for (int i = 0; MySQL_Reader.Read(); i++)
                {
                    Cells.Item[10 + i, 2].Value = MySQL_Reader.GetDouble(15);
                    double sum = 0;
                    int j;
                    for (j = 3; j < 6; j++)
                    {
                        sum += MySQL_Reader.GetDouble(j);
                    }
                    Cells.Item[10 + i, 3].Value = sum;
                    Cells.Item[10 + i, 4].Value = MySQL_Reader.GetDouble(4);
                    for (j = 0, sum = 0; j < 13; j++) sum += MySQL_Reader.GetDouble(j);
                    Cells.Item[10 + i, 5].Value = sum;
                    Cells.Item[10 + i, 6].Value = MySQL_Reader.GetDouble(13);
                    Cells.Item[10 + i, 7].Value = MySQL_Reader.GetDouble(14);
                }
                MySQL_Reader.Close();

                MySQL_cmd.CommandText = q1 + q3 + q4;
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                for (int i = 0; MySQL_Reader.Read(); i++)
                {
                    Cells.Item[10 + i, 8].Value = MySQL_Reader.GetDouble(0) + MySQL_Reader.GetDouble(1);
                    Cells.Item[10 + i, 9].Value = MySQL_Reader.GetDouble(3);
                    Cells.Item[10 + i, 10].Value = MySQL_Reader.GetDouble(4);
                    Cells.Item[10 + i, 11].Value = MySQL_Reader.GetDouble(0);
                    Cells.Item[10 + i, 12].Value = MySQL_Reader.GetDouble(1);
                    Cells.Item[10 + i, 13].Value = MySQL_Reader.GetDouble(5);
                    Cells.Item[10 + i, 14].Value = MySQL_Reader.GetDouble(6);
                }
                MySQL_Reader.Close();

                /* Пример как сделать систему более гибкой через БД, однако лучше через XML
                MySQL_cmd.CommandText = "SELECT name FROM total_info_data_names";
                MySQL_Reader = MySQL_cmd.ExecuteReader();
                for (int i = 0; MySQL_Reader.Read(); i++)
                {
                    Cells.Item[9, i + 2].Value = MySQL_Reader.GetString("name");
                }
                MySQL_Reader.Close();
                */

                for (int i = 0; i < total_info_data_names.Count; i++)
                {
                    Cells.Item[9, i + 2].Value = total_info_data_names[i];
                }

                //Строчка формул сумм
                Cells.Item[14, 1].Value = "Всего";
                for (int i = 2; i < 15; i++)
                {
                    Cells.Item[14, i].Value = "=SUM(" + ToLetter(i) + "10:" + ToLetter(i) + "13)";
                }

                //Размеры столбцов и строк, перенос слов
                Cell = Sheet.Range["A9:N9"];
                Cell.RowHeight = 40;
                Cell.WrapText = true;
                //Рамка
                Cell = Sheet.Range["A9:N14"];
                for (int i = 7; i < 11; i++) Cell.Borders[Border(i)].Weight = 3;
                Cell.Borders[XlBordersIndex.xlInsideHorizontal].Weight = 2;
                Cell.Borders[XlBordersIndex.xlInsideVertical].Weight = 2;

                progressBar.Value += progressTArr1[8];
                //LogFile.WriteLine(timer);
                //for (int i = 0; i < 8; i++) LogFile.WriteLine();

                // Покажем что получилось
                ExcelApp.Visible = true;
                ExcelApp.Calculation = XlCalculation.xlCalculationAutomatic;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                progressBar.Visible = false;
                statusLabel.Text = saveStatus;
                //LogFile.Close();
            }
        }
    }
}