using System;
using System.Collections.Generic;
using System.Text;

namespace StuffCraft
{
    /// <summary>
    /// 箱のクラス
    /// </summary>
    class clsCase
    {
        #region フィールド定義
        /// <summary>
        /// 自身のID
        /// </summary>
        public Guid ID = Guid.Empty;
        /// <summary>
        /// 上位箱のID
        /// </summary>
        public Guid ParentID = Guid.Empty;
        /// <summary>
        /// 画面表示文字
        /// </summary>
        public string Name = null;
        /// <summary>
        /// 作成日時（現地）　※　DBでの値は「UTC」
        /// </summary>
        public DateTime CreatedDT = DateTime.Now;
        /// <summary>
        /// 更新日時（現地）　※　DBでの値は「UTC」
        /// </summary>
        public DateTime UpdatedDT = DateTime.Now;
        /// <summary>
        /// 内装の紙の数量
        /// </summary>
        public int ItemCount = 0;
        #endregion

        /// <summary>
        /// 構造関数
        /// </summary>
        public clsCase()
        {
        }

        /// <summary>
        /// 構造関数
        /// </summary>
        /// <param name="_ID">自身のID</param>
        /// <param name="_ParentID">上位箱のID</param>
        /// <param name="_Name">画面表示文字</param>
        /// <param name="_CDT">作成日時</param>
        /// <param name="_UDT">更新日時</param>
        /// <param name="_ItemCount">内装紙の数量</param>
        public clsCase(Guid _ID, Guid _ParentID, string _Name, DateTime _CDT, DateTime _UDT, int _ItemCount)
        {
            ID = _ID;
            ParentID = _ParentID;
            Name = _Name;
            CreatedDT = _CDT;
            UpdatedDT = _UDT;
            ItemCount = _ItemCount;
        }
    }
}
