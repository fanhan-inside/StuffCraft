using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace StuffCraft
{
    class FileNameRefiner
    {
        public static string Refine(string s)
        {
            s = s.Replace('\t', ' ');
            s = s.Replace('\v', ' ');
            s = s.Replace('\a', ' ');
            s = s.Replace('\r', ' ');
            s = s.Replace('\n', ' ');
            s = s.Replace('\\', ' ');
            s = s.Replace('/', ' ');
            s = s.Replace(':', ' ');
            s = s.Replace('*', ' ');
            s = s.Replace('?', ' ');
            s = s.Replace('<', ' ');
            s = s.Replace('>', ' ');
            s = s.Replace('|', ' ');
            s = s.Replace('"', ' ');
            return s;
        }
    }
    class ListViewItemComparer : IComparer
    {
        private int col;
        private bool asc;

        public ListViewItemComparer()
        {
            col = 0;
            asc = true;
        }

        public ListViewItemComparer(int column, bool ascending)
        {
            col = column;
            asc = ascending;
        }

        public int Compare(object x, object y)
        {
            int nRet = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            return nRet * (asc ? 1 : -1);
        }
    }
}
