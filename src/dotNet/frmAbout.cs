using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace StuffCraft
{
    /// <summary>
    /// バージョン情報画面
    /// </summary>
    public partial class frmAbout : Form
    {
        /// <summary>
        /// 構造函数
        /// </summary>
        public frmAbout()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
