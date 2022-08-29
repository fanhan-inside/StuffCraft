using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Xml;
using System.IO;

namespace StuffCraft
{
    class clsDBOperSQLite
    {
        #region 変数たち
        /// <summary>
        /// データファイル名
        /// </summary>
        public string m_DBName = null;
        public string m_sqlCreateDB = null;
        /// <summary>
        /// データベース接続
        /// </summary>
        SQLiteConnection m_Connection = null;
        #endregion 変数たち

        /// <summary>
        /// データベース接続開く
        /// </summary>
        /// <returns>成功・失敗</returns>
        public bool Open()
        {
            try
            {
                bool bCreateNew = ! File.Exists(m_DBName);
                SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
                sb.DataSource = m_DBName;
//                sb.CacheSize = 65536;
//                sb.PageSize = 4096;
                if (m_Connection == null)
                {
                    m_Connection = new SQLiteConnection();
                    m_Connection.ConnectionString = sb.ToString();
                }
                if (bCreateNew)
                {
                    SQLiteConnection.CreateFile(m_DBName);
                }
                m_Connection.Open();
                if (bCreateNew)
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = m_Connection;
                    cmd.Transaction = m_Connection.BeginTransaction();
                    cmd.CommandText = m_sqlCreateDB;
                    cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();
                }
            }
            catch (Exception e)
            {
                Debug.Print("DB open failed: {0}\n", e.Message);
                return false;
            }
            finally
            {
            }
            return true;
        }

        /// <summary>
        /// データベース接続閉じる
        /// </summary>
        /// <returns>成功・失敗</returns>
        public bool Close()
        {
            try
            {
                if (m_Connection == null)
                    return true;

                // サイズ最適化
                SQLiteCommand cmd = new SQLiteCommand("VACUUM;", m_Connection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                m_Connection.Close();
                m_Connection.Dispose();
                m_Connection = null;
            }
            catch (Exception e)
            {
                Debug.Print("DB close failed: {0}\n", e.Message);
                return false;
            }
            finally
            {
            }
            return true;
        }

        /// <summary>
        /// 種類のリストを作成
        /// </summary>
        /// <returns>リスト</returns>
        public List<string> BuildCategoryList()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = @"SELECT COUNT(*) FROM (SELECT DISTINCT [Category] FROM [Note] WHERE [Deleted]=0)";
            int n = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Transaction.Commit();
            if (n <= 0)
                return null;

            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = @"SELECT DISTINCT [Category] FROM [Note] WHERE [Deleted]=0 ORDER BY [Category]";
            SQLiteDataReader r = cmd.ExecuteReader();

            List<string> list = new List<string>();
            while (r.Read())
            {
                string s = r.GetString(0);
                if (s != null && s.Length > 0)
                    list.Add(s);
            }
            r.Close();
            cmd.Transaction.Commit();

            return list;
        }

        /// <summary>
        /// 箱が存在するか
        /// </summary>
        /// <param name="CaseID">箱のID</param>
        /// <returns>存在する・存在しない</returns>
        public bool CaseExists(Guid CaseID)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT COUNT(*) FROM [Case] WHERE [ID]=?";
            cmd.Parameters.Add("@ID", DbType.Guid).Value = CaseID;
            int nRecordCount = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Transaction.Commit();

            return (nRecordCount > 0);
        }

        /// <summary>
        /// 紙が存在するか
        /// </summary>
        /// <param name="NoteID">紙のID</param>
        /// <returns>存在する・存在しない</returns>
        public bool NoteExists(Guid NoteID)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT COUNT(*) FROM [Note] WHERE [ID]=? AND [Deleted]=0";
            cmd.Parameters.Add("@ID", DbType.Guid).Value = NoteID;
            int nRecordCount = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Transaction.Commit();

            return (nRecordCount > 0);
        }

        /// <summary>
        /// 箱を受取
        /// </summary>
        /// <param name="CaseID">箱のID</param>
        /// <returns>該当箱</returns>
        public clsCase GetCaseFromID(Guid CaseID)
        {
            clsCase Case = null;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            // 内装紙数を更新
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT COUNT(*) FROM [Note] WHERE [ParentID]=?";
            cmd.Parameters.Add("@ParentID", DbType.Guid).Value = CaseID;
            int nItemCount = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Parameters.Clear();
            cmd.CommandText = "UPDATE [Case] SET [ItemCount]=? WHERE [ID]=?";
            cmd.Parameters.Add("@ItemCount", DbType.Int32).Value = nItemCount;
            cmd.Parameters.Add("@ID", DbType.Guid).Value = CaseID;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cmd.Transaction.Commit();
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT [ID], [ParentID], [Name], [CreatedUTC], [UpdatedUTC], [ItemCount] FROM [Case] WHERE [ID]=?";
            cmd.Parameters.Add("@ID", DbType.Guid).Value = CaseID;
            SQLiteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                Case = new clsCase();
                Case.ID = r.GetGuid(0);
                Case.ParentID = r.GetGuid(1);
                Case.Name = r.GetString(2);
                Case.CreatedDT = r.GetDateTime(3).ToLocalTime();
                Case.UpdatedDT = r.GetDateTime(4).ToLocalTime();
                Case.ItemCount = r.GetInt32(5);
            }
            r.Close();
            cmd.Transaction.Commit();

            return Case;
        }

        /// <summary>
        /// 紙を受取
        /// </summary>
        /// <param name="NoteID">紙のID</param>
        /// <returns>該当紙</returns>
        public clsNote GetNoteFromID(Guid NoteID)
        {
            clsNote note = null;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT [ID], [ParentID], [Category], [Title], [Content], [CreatedUTC], [UpdatedUTC], [WordCount] FROM [Note] WHERE [ID]=? AND [Deleted]=0";
            cmd.Parameters.Add("@ID", DbType.Guid).Value = NoteID;
            SQLiteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                note = new clsNote();
                note.ID = r.GetGuid(0);
                note.ParentID = r.GetGuid(1);
                note.Category = r.GetString(2);
                note.Title = r.GetString(3);
                note.Content = r.GetString(4);
                note.CreatedDT = r.GetDateTime(5).ToLocalTime();
                note.UpdatedDT = r.GetDateTime(6).ToLocalTime();
                note.WordCount = r.GetInt32(7);
            }
            r.Close();
            cmd.Transaction.Commit();

            return note;
        }

        /// <summary>
        /// 入れた箱の下位箱IDを受取
        /// </summary>
        /// <param name="CaseID">検索された箱のID</param>
        /// <returns>下位箱IDの配列</returns>
        public Guid[] GetChildrenCaseIDs(Guid CaseID)
        {
            List<Guid> IDs = new List<Guid>();

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT [ID] FROM [Case] WHERE [ParentID]=?";
            cmd.Parameters.Add("@ParentID", DbType.Guid).Value = CaseID;
            SQLiteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                IDs.Add(r.GetGuid(0));
            }
            r.Close();
            cmd.Transaction.Commit();

            return IDs.ToArray();
        }

        /// <summary>
        /// 入れた箱の所属された紙を受取（内容なし）
        /// </summary>
        /// <param name="CaseID">検索された箱のID</param>
        /// <returns>所属された紙の配列</returns>
        public clsNote[] GetChildrenNoteWithoutContent(Guid CaseID)
        {
            List<clsNote> notes = new List<clsNote>();

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT [ID], [ParentID], [Category], [Title], [CreatedUTC], [UpdatedUTC], [WordCount] FROM [Note] WHERE [ParentID]=? AND [Deleted]=0";
            cmd.Parameters.Add("@ParentID", DbType.Guid).Value = CaseID;
            SQLiteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                clsNote note = new clsNote();
                note.ID = r.GetGuid(0);
                note.ParentID = r.GetGuid(1);
                note.Category = r.GetString(2);
                note.Title = r.GetString(3);
                note.Content = "";
                note.CreatedDT = r.GetDateTime(4).ToLocalTime();
                note.UpdatedDT = r.GetDateTime(5).ToLocalTime();
                note.WordCount = r.GetInt32(6);
                notes.Add(note);
            }
            r.Close();
            cmd.Transaction.Commit();

            return notes.ToArray();
        }

        /// <summary>
        /// 紙の内容を受取
        /// </summary>
        /// <param name="NoteID">紙ID</param>
        /// <returns>内容</returns>
        public string GetNoteContent(Guid NoteID)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT [Content] FROM [Note] WHERE [ID]=? AND [Deleted]=0";
            cmd.Parameters.Add("@ID", DbType.Guid).Value = NoteID;
            string s = (string)cmd.ExecuteScalar();
            cmd.Transaction.Commit();

            return s;
        }

        /// <summary>
        /// 箱の名称を更新
        /// </summary>
        /// <param name="CaseID">箱のID</param>
        /// <param name="CaseName">箱の名称</param>
        /// <returns>成功・失敗</returns>
        public bool UpdateCaseName(Guid CaseID, string CaseName)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "UPDATE [Case] SET [Name]=?, [UpdatedUTC]=? WHERE [ID]=?";
            cmd.Parameters.Add("@Name", DbType.String).Value = CaseName;
            cmd.Parameters.Add("@UpdatedUTC", DbType.DateTime).Value = DateTime.Now.ToUniversalTime();
            cmd.Parameters.Add("@ID", DbType.Guid).Value = CaseID;
            int nLineCount = cmd.ExecuteNonQuery();
            cmd.Transaction.Commit();

            return true;
        }

        /// <summary>
        /// 紙を更新
        /// </summary>
        /// <param name="note">紙</param>
        /// <returns>成功・失敗</returns>
        public bool UpdateNote(clsNote note)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "UPDATE [Note] SET [ParentID]=?, [Category]=?, [Title]=?, [Content]=?, [WordCount]=?, [UpdatedUTC]=? WHERE [ID]=? AND [Deleted]=0";
            cmd.Parameters.Add("@ParentID", DbType.Guid).Value = note.ParentID;
            cmd.Parameters.Add("@Category", DbType.String).Value = note.Category;
            cmd.Parameters.Add("@Title", DbType.String).Value = note.Title;
            cmd.Parameters.Add("@Content", DbType.String).Value = note.Content;
            cmd.Parameters.Add("@WordCount", DbType.Int32).Value = note.WordCount;
            cmd.Parameters.Add("@UpdatedUTC", DbType.DateTime).Value = note.UpdatedDT.ToUniversalTime();
            cmd.Parameters.Add("@ID", DbType.Guid).Value = note.ID;
            int nLineCount = cmd.ExecuteNonQuery();
            cmd.Transaction.Commit();

            return true;
        }

        /// <summary>
        /// 複数の紙を他の箱に移動
        /// </summary>
        /// <param name="NoteIDs">紙のIDの配列</param>
        /// <param name="ParentCaseID">所属箱のID</param>
        /// <returns>移動した紙数</returns>
        public int ChangeNoteParent(Guid[] NoteIDs, Guid ParentCaseID)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            foreach (Guid id in NoteIDs)
            {
                cmd.CommandText = "UPDATE [Note] SET [ParentID]=? WHERE [ID]=? AND [Deleted]=0";
                cmd.Parameters.Add("@ParentID", DbType.Guid).Value = ParentCaseID;
                cmd.Parameters.Add("@ID", DbType.Guid).Value = id;
                cmd.ExecuteNonQuery();
            }
            cmd.Transaction.Commit();

            return NoteIDs.Length;
        }

        /// <summary>
        /// 箱を他の上位箱に移動
        /// </summary>
        /// <param name="CaseID">箱ID</param>
        /// <param name="ParentCaseID">上位箱のID</param>
        /// <returns>成功・失敗</returns>
        public bool ChangeCaseParent(Guid CaseID, Guid ParentCaseID)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "UPDATE [Case] SET [ParentID]=? WHERE [ID]=?";
            cmd.Parameters.Add("@ParentID", DbType.Guid).Value = ParentCaseID;
            cmd.Parameters.Add("@ID", DbType.Guid).Value = CaseID;
            int nLineCount = cmd.ExecuteNonQuery();
            cmd.Transaction.Commit();

            return true;
        }

        /// <summary>
        /// 新規箱作成
        /// </summary>
        /// <param name="Case">箱</param>
        /// <returns>成功・失敗</returns>
        public bool AddCase(clsCase Case)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "INSERT INTO [Case] "
                + " ([ID], [ParentID], [Name], [CreatedUTC], [UpdatedUTC], [ItemCount]) "
                + " VALUES (?, ?, ?, ?, ?, ?)";
            cmd.Parameters.Add("@ID", DbType.Guid).Value = Case.ID;
            cmd.Parameters.Add("@ParentID", DbType.Guid).Value = Case.ParentID;
            cmd.Parameters.Add("@Name", DbType.String).Value = Case.Name;
            cmd.Parameters.Add("@CreatedUTC", DbType.DateTime).Value = Case.CreatedDT.ToUniversalTime();
            cmd.Parameters.Add("@UpdatedUTC", DbType.DateTime).Value = Case.UpdatedDT.ToUniversalTime();
            cmd.Parameters.Add("@ItemCount", DbType.Int32).Value = Case.ItemCount;
            int nLineCount = cmd.ExecuteNonQuery();
            cmd.Transaction.Commit();

            return true;
        }

        /// <summary>
        /// 新規紙作成
        /// </summary>
        /// <param name="note">紙</param>
        /// <returns>成功・失敗</returns>
        public bool AddNote(clsNote note)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = @"INSERT INTO [Note] "
                + " ([ID], [ParentID], [Category], [Title], [Content], [CreatedUTC], [UpdatedUTC], [WordCount], [Deleted]) "
                + " VALUES (?, ?, ?, ?, ?, ?, ?, ?, 0)";
            cmd.Parameters.Add("@ID", DbType.Guid).Value = note.ID;
            cmd.Parameters.Add("@ParentID", DbType.Guid).Value = note.ParentID;
            cmd.Parameters.Add("@Category", DbType.String).Value = note.Category;
            cmd.Parameters.Add("@Title", DbType.String).Value = note.Title;
            cmd.Parameters.Add("@Content", DbType.String).Value = note.Content;
            cmd.Parameters.Add("@CreatedUTC", DbType.DateTime).Value = note.CreatedDT.ToUniversalTime();
            cmd.Parameters.Add("@UpdatedUTC", DbType.DateTime).Value = note.UpdatedDT.ToUniversalTime();
            cmd.Parameters.Add("@WordCount", DbType.Int32).Value = note.WordCount;
            int nLineCount = cmd.ExecuteNonQuery();
            cmd.Transaction.Commit();

            return true;
        }

        /// <summary>
        /// 箱を削除
        /// </summary>
        /// <param name="CaseID">箱のID</param>
        /// <returns>成功・失敗</returns>
        public bool DeleteCase(Guid CaseID)
        {
            int nRecCount = 0;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            SQLiteParameter p = new SQLiteParameter();
            cmd.Parameters.Add(p);

            // 削除される箱・紙のIDを受取
            //
            SQLiteDataReader r = null;
            Queue<Guid> listCase = new Queue<Guid>(), listNote = new Queue<Guid>(), listTmp = new Queue<Guid>();
            listTmp.Enqueue(CaseID);
            while (listTmp.Count > 0)
            {
                Guid id = listTmp.Dequeue();
                p.Value = id;
                // 箱を受取
                cmd.Transaction = m_Connection.BeginTransaction();
                cmd.CommandText = "SELECT [ID] FROM [Case] WHERE [ParentID]=?";
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    listTmp.Enqueue(r.GetGuid(0));
                }
                r.Close();
                cmd.Transaction.Commit();

                // 紙を受取
                cmd.Transaction = m_Connection.BeginTransaction();
                cmd.CommandText = "SELECT [ID] FROM [Note] WHERE [ParentID]=?";
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    listNote.Enqueue(r.GetGuid(0));
                }
                r.Close();
                cmd.Transaction.Commit();

                listCase.Enqueue(id);
            }

            // 本番削除動作
            //
            cmd.Transaction = m_Connection.BeginTransaction();
            // 箱を削除
            cmd.CommandText = "DELETE FROM [Case] WHERE [ID]=?";
            while (listCase.Count > 0)
            {
                p.Value = listCase.Dequeue();
                nRecCount = cmd.ExecuteNonQuery();
            }
            // 箱の紙を削除
            cmd.CommandText = "UPDATE [Note] SET [Deleted]=1 WHERE [ParentID]=?";
            while (listNote.Count > 0)
            {
                p.Value = listNote.Dequeue();
                nRecCount = cmd.ExecuteNonQuery();
            }
            cmd.Transaction.Commit();

            return true;
        }

        /// <summary>
        /// 複数の紙を削除
        /// </summary>
        /// <param name="NoteIDs">紙のIDのリスト</param>
        /// <returns>削除した紙数</returns>
        public int DeleteNote(Guid[] NoteIDs)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            SQLiteParameter p = new SQLiteParameter();
            cmd.Parameters.Add(p);
            cmd.CommandText = "UPDATE [Note] SET [Deleted]=1 WHERE [Note].[ID]=?";
            foreach (Guid id in NoteIDs)
            {
                p.Value = id;
                cmd.ExecuteNonQuery();
            }
            cmd.Transaction.Commit();

            return NoteIDs.Length;
        }

        /// <summary>
        /// XMLファイルにエクスポート
        /// </summary>
        /// <param name="CaseID">箱のID</param>
        /// <param name="url">XMLファイルのパスとネーム</param>
        /// <returns>成功・失敗</returns>
        public bool ExportData(Guid CaseID, string url)
        {
            int nRet = 0;
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            if (CaseID != Guid.Empty)
            {
                cmd.CommandText = "SELECT COUNT(*) FROM [Case] WHERE [ID]=?";
                cmd.Parameters.Add("@ID", DbType.Guid).Value = CaseID;
            }
            else
            {
                cmd.CommandText = "SELECT COUNT(*) FROM [Case]";
            }
            nRet = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Transaction.Commit();
            cmd.Parameters.Clear();

            if (nRet <= 0)
                return false;

            // 指定される箱・紙のIDを受取
            //
            SQLiteDataReader r = null;
            Queue<Guid> listCase = new Queue<Guid>(), listTmp = new Queue<Guid>();
            listTmp.Enqueue(CaseID);
            cmd.CommandText = "SELECT [ID], [ParentID] FROM [Case] WHERE [ParentID]=?";
            while (listTmp.Count > 0)
            {
                Guid id = listTmp.Dequeue();
                // 箱を受取
                cmd.Parameters.Add("@ParentID", DbType.Guid).Value = id;
                cmd.Transaction = m_Connection.BeginTransaction();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    listTmp.Enqueue(r.GetGuid(0));
                    Debug.Print(r.GetGuid(1).ToString());
                }
                r.Close();
                cmd.Transaction.Commit();
                cmd.Parameters.Clear();

                listCase.Enqueue(id);
            }

            // 本番エクスポート動作
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.Indent = true;
            xws.IndentChars = @" ";
            xws.NewLineChars = "\r";
            xws.NewLineHandling = NewLineHandling.Entitize;
            xws.NewLineOnAttributes = true;
            using (XmlWriter w = XmlWriter.Create(url, xws))
            {
                // Start ROOT
                w.WriteStartElement("StuffCraft");
                // 箱
                cmd.CommandText = "SELECT [ID], [ParentID], [CreatedUTC], [UpdatedUTC], [Name] FROM [Case] WHERE [ID]=?";
                foreach (Guid id in listCase)
                {
                    cmd.Parameters.Add("@ID", DbType.Guid).Value = id;
                    cmd.Transaction = m_Connection.BeginTransaction();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        w.WriteStartElement(@"Case");
                        w.WriteAttributeString(@"ID", r.GetGuid(0).ToString());
                        w.WriteAttributeString(@"ParentID", r.GetGuid(1).ToString());
                        w.WriteAttributeString(@"CreatedUTC", r.GetDateTime(2).ToString("yyyy-MM-dd HH:mm:ss"));
                        w.WriteAttributeString(@"UpdatedUTC", r.GetDateTime(3).ToString("yyyy-MM-dd HH:mm:ss"));
                        w.WriteElementString(@"Name", r.GetString(4).ToString());
                        w.WriteEndElement();
                    }
                    r.Close();
                    cmd.Transaction.Commit();
                    cmd.Parameters.Clear();
                }
                // 紙
                cmd.CommandText = "SELECT [ID], [ParentID], [Category], [CreatedUTC], [UpdatedUTC], [WordCount], [Title], [Content] FROM [Note] WHERE [ParentID]=? AND [Deleted]=0";
                foreach (Guid id in listCase)
                {
                    cmd.Parameters.Add("@ParentID", DbType.Guid).Value = id;
                    cmd.Transaction = m_Connection.BeginTransaction();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        w.WriteStartElement(@"Note");
                        w.WriteAttributeString(@"ID", r.GetGuid(0).ToString());
                        w.WriteAttributeString(@"ParentID", r.GetGuid(1).ToString());
                        w.WriteAttributeString(@"Category", r.GetString(2));
                        w.WriteAttributeString(@"CreatedUTC", r.GetDateTime(3).ToString("yyyy-MM-dd HH:mm:ss"));
                        w.WriteAttributeString(@"UpdatedUTC", r.GetDateTime(4).ToString("yyyy-MM-dd HH:mm:ss"));
                        w.WriteAttributeString(@"WordCount", r.GetInt32(5).ToString());
                        w.WriteElementString(@"Title", r.GetString(6));
                        w.WriteElementString(@"Content", r.GetString(7));
                        w.WriteEndElement();
                    }
                    r.Close();
                    cmd.Transaction.Commit();
                    cmd.Parameters.Clear();
                }
                // End ROOT
                w.WriteEndElement();

                // 更新
                w.Flush();
                // 解放
                w.Close();
            }

            return true;
        }
    }
}
