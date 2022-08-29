using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StuffCraft
{
    /// <summary>
    /// 紙編集画面
    /// </summary>
    public partial class frmDetail : Form
    {
        #region 紙についての属性
        /// <summary>
        /// 紙のID
        /// </summary>
        public Guid m_ID = Guid.Empty;
        /// <summary>
        /// 紙のカテゴリ
        /// </summary>
        public string m_Category = "";
        /// <summary>
        /// 紙の見出し
        /// </summary>
        public string m_Title = null;
        /// <summary>
        /// 紙の内容
        /// </summary>
        public string m_Content = null;
        /// <summary>
        /// 紙の内容の字数
        /// </summary>
        public int m_WordCount = 0;
        #endregion

        /// <summary>
        /// 既存カテゴリのリスト
        /// </summary>
        public List<string> m_CategoryList = null;

        /// <summary>
        /// 構造函数
        /// </summary>
        public frmDetail()
        {
            InitializeComponent();
        }

        private void frmDetail_Shown(object sender, EventArgs e)
        {
            comboType.Items.Clear();
            if (m_CategoryList != null && m_CategoryList.Count > 0)
            {
                comboType.Items.AddRange(m_CategoryList.ToArray());
                comboType.SelectedIndex = comboType.Items.IndexOf(m_Category);
            }
            if (m_Title != null)
                textTitle.Text = m_Title;
            if (m_Content != null)
                textContent.Text = m_Content;

            textWordCount.Text = m_WordCount.ToString();
            statusID.Text = m_ID.ToString("B");
            textTitle.Focus();
            textTitle.SelectAll();
        }

        private void textContent_TextChanged(object sender, EventArgs e)
        {
            textWordCount.Text = textContent.Text.Length.ToString();
        }

        private void btnRefine_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in textContent.Lines)
            {
                sb.AppendLine(s.TrimEnd());
            }
            textContent.Text = sb.ToString();
        }

        private void frmDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                return;

            // 種類
            m_Category = comboType.Text;
            // 見出し
            m_Title = textTitle.Text.Trim();
            // 内容
            m_Content = textContent.Text;
            // 字数
            m_WordCount = Convert.ToInt32(textWordCount.Text);

            // 見出しチェック：空白は駄目
            if (m_Title.Length <= 0)
            {
                MessageBox.Show(clsMessage.BLANK_TITLE_WARNING,
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            // 内容チェック：空白は駄目
            if (m_Content.Length <= 0)
            {
                MessageBox.Show(clsMessage.BLANK_CONTENT_WARNING,
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }
    }
}
