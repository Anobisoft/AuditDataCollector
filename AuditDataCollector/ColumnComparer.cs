using System;
using System.Collections;
using System.Windows.Forms;

namespace AuditDataCollector
{
//Этот класс реализует интерфейс IComparer
    class ColumnComparer : IComparer
    {
        int columnIndex = 0;
        public bool sortAscending = false;
        public string ColumnText;
        //Это свойство инициализируется при каждом клике на column header'e
        public int ColumnIndex
        {
            set
            {
                if (columnIndex == value)
                {
                    sortAscending = !sortAscending;
                }
                else
                {
                    columnIndex = value;
                    sortAscending = true;
                }
            }
            get
            {
                return columnIndex;
            }
        }

        public int Compare(object x, object y)
        {
            string value1 = ((ListViewItem)x).SubItems[columnIndex].Text;
            string value2 = ((ListViewItem)y).SubItems[columnIndex].Text;
            return String.Compare(value1, value2) * (sortAscending ? 1 : -1);
        }
    }
}
