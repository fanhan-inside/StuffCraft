using System;
using System.Collections.Generic;
using System.Text;

namespace StuffCraft
{
    class clsNote
    {
        #region フィールド定義
        /// <summary>
        /// 自身のID
        /// </summary>
        public Guid ID = Guid.Empty;
        /// <summary>
        /// 所属フォルダのID
        /// </summary>
        public Guid ParentID = Guid.Empty;
        /// <summary>
        /// カテゴリ
        /// </summary>
        public string Category = null;
        /// <summary>
        /// 見出し
        /// </summary>
        public string Title = null;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content = null;
        /// <summary>
        /// 作成日時（現地）　※　DBでの値は「UTC」
        /// </summary>
        public DateTime CreatedDT = DateTime.Now;
        /// <summary>
        /// 更新日時（現地）　※　DBでの値は「UTC」
        /// </summary>
        public DateTime UpdatedDT = DateTime.Now;
        /// <summary>
        /// 字数
        /// </summary>
        public int WordCount = 0;
        #endregion

        /// <summary>
        /// 構造関数
        /// </summary>
        public clsNote()
        {
        }

        /// <summary>
        /// 構造関数
        /// </summary>
        /// <param name="_ID">自身のID</param>
        /// <param name="_ParentID">所属箱のID</param>
        /// <param name="_Category">種類</param>
        /// <param name="_Title">見出し</param>
        /// <param name="_CDT">作成日時</param>
        /// <param name="_UDT">更新日時</param>
        public clsNote(Guid _ID, Guid _ParentID, string _Category, string _Title, DateTime _CDT, DateTime _UDT)
        {
            ID = _ID;
            ParentID = _ParentID;
            Category = _Category;
            Title = _Title;
            Content = "";
            CreatedDT = _CDT;
            UpdatedDT = _UDT;
            WordCount = 0;
        }
    }
}
