using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Diagnostics;
using System.Xml;


namespace StuffCraft
{
    class clsDBOperAccess
    {
        #region 変数たち
        /// <summary>
        /// DB連結ストリング
        /// </summary>
        public string m_ConnStr =String.Format(
            @"Provider=Microsoft.Jet.OLEDB.4.0;"
            + @"Data Source={0};User ID=Admin;Password=;",
//            + @"Extended Properties=;Mode=Share Deny None;"
//            + @"Jet OLEDB:Global Partial Bulk Ops=2;"
//            + @"Jet OLEDB:Registry Path=;"
//            + @"Jet OLEDB:Database Locking Mode=1;"
//            + @"Jet OLEDB:Database Password={1};"
//            + @"Jet OLEDB:Engine Type=5;"
//            + @"Jet OLEDB:Global Bulk Transactions=1;"
//            + @"Jet OLEDB:System database=;"
//            + @"Jet OLEDB:SFP=False;"
//            + @"Jet OLEDB:New Database Password=;"
//            + @"Jet OLEDB:CreatedDT System Database=False;"
//            + @"Jet OLEDB:Don't Copy Locale on Compact=False;"
//            + @"Jet OLEDB:Compact Without Replica Repair=False;"
//            + @"Jet OLEDB:Encrypt Database=False",
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Stuff\StuffCraft.mdb", "");

        /// <summary>
        /// データベース接続
        /// </summary>
        OleDbConnection m_Connection = null;
        #endregion 変数たち

        /// <summary>
        /// データベース接続開く
        /// </summary>
        /// <returns>成功・失敗</returns>
        public bool Open()
        {
            try
            {
                if (m_Connection == null)
                    m_Connection = new OleDbConnection(m_ConnStr);
                m_Connection.Open();
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
        /// カテゴリのリストを作成
        /// </summary>
        /// <returns>リスト</returns>
        public List<string> BuildCategoryList()
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.CommandText = @"SELECT COUNT(*) FROM (SELECT DISTINCT [Note].[Category] FROM [Note] WHERE [Deleted]=FALSE)";
            int n = (int)cmd.ExecuteScalar();
            if (n <= 0)
                return null;

            cmd.CommandText = @"SELECT DISTINCT [Note].[Category] FROM [Note] WHERE [Deleted]=FALSE";
            OleDbDataReader r = cmd.ExecuteReader();

            List<string> list = new List<string>();
            while (r.Read())
            {
                string s = r.GetString(0);
                if (s != null && s.Length > 0)
                    list.Add(s);
            }
            r.Close();

            return list;
        }

        /// <summary>
        /// 箱が存在するか
        /// </summary>
        /// <param name="CaseID">箱のID</param>
        /// <returns>存在する・存在しない</returns>
        public bool CaseExists(Guid CaseID)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT COUNT(*) FROM [Case] WHERE [ID]=?";
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = CaseID;
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT COUNT(*) FROM [Note] WHERE [ID]=? AND [Deleted]=FALSE";
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = NoteID;
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            // 内装紙数を更新
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT COUNT(*) FROM [Note] WHERE [ParentID]=? AND [Deleted]=FALSE";
            cmd.Parameters.Add("@ParentID", OleDbType.Guid).Value = CaseID;
            int nItemCount = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Parameters.Clear();
            cmd.CommandText = "UPDATE [Case] SET [ItemCount]=? WHERE [ID]=?";
            cmd.Parameters.Add("@ItemCount", OleDbType.Integer).Value = nItemCount;
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = CaseID;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cmd.Transaction.Commit();
            // 箱を読み出し
            cmd.CommandText = "SELECT [ID], [ParentID], [Name], [CreatedUTC], [UpdatedUTC], [ItemCount] FROM [Case] WHERE [ID]=?";
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = CaseID;
            OleDbDataReader r = cmd.ExecuteReader();
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.CommandText = "SELECT [ID], [ParentID], [Category], [Title], [Content], [CreatedUTC], [UpdatedUTC], [WordCount] FROM [Note] WHERE [ID]=?";
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = NoteID;
            OleDbDataReader r = cmd.ExecuteReader();
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

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT [ID] from [Case] WHERE [ParentID]=?";
            cmd.Parameters.Add("@ParentID", OleDbType.Guid).Value = CaseID;
            OleDbDataReader r = cmd.ExecuteReader();
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

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "SELECT [ID], [ParentID], [Category], [Title], [CreatedUTC], [UpdatedUTC], [WordCount] FROM [Note] WHERE [ParentID]=? AND [Deleted]=FALSE";
            cmd.Parameters.Add("@ParentID", OleDbType.Guid).Value = CaseID;
            OleDbDataReader r = cmd.ExecuteReader();
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.CommandText = "select [Content] from [Note] where [ID]=?";
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = NoteID;
            string s = (string) cmd.ExecuteScalar();
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.CommandText = "select COUNT(*) from [Case] where [ID]=?";
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = CaseID;
            int nRecordCount = (int)cmd.ExecuteScalar();
            if (nRecordCount <= 0)
            {
                Debug.Print("NO Case matched, CaseID='{0}'\n", CaseID);
                return false;
            }
            cmd.Parameters.Clear();

            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "UPDATE [Case] SET [Name]=?, [UpdatedUTC]=? WHERE [ID]=?";
            cmd.Parameters.Add("@Name", OleDbType.VarWChar).Value = CaseName;
            cmd.Parameters.Add("@UpdatedUTC", OleDbType.Date).Value = DateTime.Now.ToUniversalTime();
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = CaseID;
            int nLineCount = cmd.ExecuteNonQuery();
            Debug.Print("Updated {0} Line(s)\n", nLineCount);
            cmd.Transaction.Commit();
            cmd.Dispose();
            return true;
        }

        /// <summary>
        /// 紙を更新
        /// </summary>
        /// <param name="note">紙</param>
        /// <returns>成功・失敗</returns>
        public bool UpdateNote(clsNote note)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.CommandText = "SELECT COUNT(*) FROM [Note] WHERE [ID]=?";
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = note.ID;
            int nRecordCount = (int) cmd.ExecuteScalar();
            if (nRecordCount <= 0)
            {
                Debug.Print("NO Note matched, NoteID='{0}'\n", note.ID);
                return false;
            }
            cmd.Parameters.Clear();

            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "UPDATE [Note] SET [ParentID]=?, [Category]=?, [Title]=?, [Content]=?, [WordCount]=?, [UpdatedUTC]=? WHERE [ID]=?";
            cmd.Parameters.Add("@ParentID", OleDbType.Guid).Value = note.ParentID;
            cmd.Parameters.Add("@Category", OleDbType.VarWChar).Value = note.Category;
            cmd.Parameters.Add("@Title", OleDbType.VarWChar).Value = note.Title;
            cmd.Parameters.Add("@Content", OleDbType.LongVarWChar).Value = note.Content;
            cmd.Parameters.Add("@WordCount", OleDbType.Integer).Value = note.WordCount;
            cmd.Parameters.Add("@UpdatedUTC", OleDbType.Date).Value = note.UpdatedDT.ToUniversalTime();
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = note.ID;
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();

            foreach (Guid id in NoteIDs)
            {
                cmd.CommandText = "UPDATE [Note] SET [ParentID]=?, WHERE [ID]=?";
                cmd.Parameters.Add("@ParentID", OleDbType.Guid).Value = ParentCaseID;
                cmd.Parameters.Add("@ID", OleDbType.Guid).Value = id;
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = "UPDATE [Case] SET [ParentID]=? WHERE [ID]=?";
            cmd.Parameters.Add("@ParentID", OleDbType.Guid).Value = ParentCaseID;
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = CaseID;
            cmd.ExecuteNonQuery();
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = @"INSERT INTO [Case] "
                + " ([ID], [ParentID], [Name], [CreatedUTC], [UpdatedUTC], [ItemCount]) "
                + " VALUES (?, ?, ?, ?, ?, ?)";
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = Case.ID;
            cmd.Parameters.Add("@ParentID", OleDbType.Guid).Value = Case.ParentID;
            cmd.Parameters.Add("@Name", OleDbType.VarWChar).Value = Case.Name;
            cmd.Parameters.Add("@CreatedUTC", OleDbType.Date).Value = Case.CreatedDT.ToUniversalTime();
            cmd.Parameters.Add("@UpdatedUTC", OleDbType.Date).Value = Case.UpdatedDT.ToUniversalTime();
            cmd.Parameters.Add("@ItemCount", OleDbType.Integer).Value = Case.ItemCount;
            cmd.ExecuteNonQuery();
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            cmd.CommandText = @"INSERT INTO [Note] "
                + " ([ID], [ParentID], [Category], [Title], [Content], [CreatedUTC], [UpdatedUTC], [WordCount], [Deleted]) "
                + " VALUES (?, ?, ?, ?, ?, ?, ?, ?, FALSE)";
            cmd.Parameters.Add("@ID", OleDbType.Guid).Value = note.ID;
            cmd.Parameters.Add("@ParentID", OleDbType.Guid).Value = note.ParentID;
            cmd.Parameters.Add("@Category", OleDbType.VarWChar).Value = note.Category;
            cmd.Parameters.Add("@Title", OleDbType.VarWChar).Value = note.Title;
            cmd.Parameters.Add("@Content", OleDbType.LongVarWChar).Value = note.Content;
            cmd.Parameters.Add("@CreatedUTC", OleDbType.Date).Value = note.CreatedDT.ToUniversalTime();
            cmd.Parameters.Add("@UpdatedUTC", OleDbType.Date).Value = note.UpdatedDT.ToUniversalTime();
            cmd.Parameters.Add("@WordCount", OleDbType.Integer).Value = note.WordCount;
            cmd.ExecuteNonQuery();
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;

            // 削除される箱・紙のIDを受取
            //
            OleDbDataReader r = null;
            Queue<Guid> listCase = new Queue<Guid>(), listNote = new Queue<Guid>(), listTmp = new Queue<Guid>();
            listTmp.Enqueue(CaseID);
            while (listTmp.Count > 0)
            {
                Guid id = listTmp.Dequeue();
                cmd.Parameters.Add("@ParentID", OleDbType.Guid).Value = id;
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

                // クリア
                cmd.Parameters.Clear();

                listCase.Enqueue(id);
            }

            // 本番削除動作
            //
            cmd.Transaction = m_Connection.BeginTransaction();
            while (listCase.Count > 0)
            {
                Guid id = listCase.Dequeue();
                // 箱を削除
                cmd.CommandText = "DELETE FROM [Case] WHERE [ID]=?";
                cmd.Parameters.Add("@ID", OleDbType.Guid).Value = id;
                nRecCount = cmd.ExecuteNonQuery();
                // クリア
                cmd.Parameters.Clear();
            }
            while (listNote.Count > 0)
            {
                Guid id = listNote.Dequeue();
                // 箱の紙を削除
                cmd.CommandText = "UPDATE [Note] SET [Deleted]=TRUE WHERE [ID]=?";
                cmd.Parameters.Add("@ID", OleDbType.Guid).Value = id;
                nRecCount = cmd.ExecuteNonQuery();
                // クリア
                cmd.Parameters.Clear();
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            cmd.Transaction = m_Connection.BeginTransaction();
            foreach (Guid id in NoteIDs)
            {
                // 紙を削除
                cmd.CommandText = "UPDATE [Note] SET [Deleted]=TRUE WHERE [ID]=?";
                cmd.Parameters.Add("@ID", OleDbType.Guid).Value = id;
                cmd.ExecuteNonQuery();
                // クリア
                cmd.Parameters.Clear();
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
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_Connection;
            if (CaseID != Guid.Empty)
            {
                cmd.CommandText = String.Format("select COUNT(*) from [Case] where [ID]={{guid {{{0}}}}}", CaseID);
            }
            else
            {
                cmd.CommandText = String.Format("select COUNT(*) from [Case]");
            }
            nRet = (int)cmd.ExecuteScalar();
            if (nRet <= 0)
                return false;

            // 指定される箱・紙のIDを受取
            //
            OleDbDataReader r = null;
            Queue<Guid> listCase = new Queue<Guid>(), listNote = new Queue<Guid>(), listTmp = new Queue<Guid>();
            listTmp.Enqueue(CaseID);
            while (listTmp.Count > 0)
            {
                Guid id = listTmp.Dequeue();
                // 箱を受取
                cmd.CommandText = String.Format("select [ID] from [Case] where [ParentID]={{guid {{{0}}}}}", id);
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    listTmp.Enqueue(r.GetGuid(0));
                }
                r.Close();
                // 紙を受取
                cmd.CommandText = String.Format("select [ID] from [Note] where [ParentID]={{guid {{{0}}}}}", id);
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    listNote.Enqueue(r.GetGuid(0));
                }
                r.Close();
                listCase.Enqueue(id);
            }

            // エクスポートリストを作成
            //
            List<string> IDs = new List<string>();
            while (listCase.Count > 0)
            {
                IDs.Add(String.Format("{{guid {{{0}}}}}", listCase.Dequeue()));
            }
            string sqlCaseID = String.Join(", ", IDs.ToArray());
            IDs.Clear();
            while (listNote.Count > 0)
            {
                IDs.Add(String.Format("{{guid {{{0}}}}}", listNote.Dequeue()));
            }
            string sqlNoteID = String.Join(", ", IDs.ToArray());
            IDs.Clear();

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
                cmd.CommandText = String.Format("select [ID], [ParentID], [CreatedUTC], [UpdatedUTC], [Name] from [Case] where [ID] in ({0})", sqlCaseID);
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    w.WriteStartElement(@"Case");
                    w.WriteAttributeString(@"ID",         r.GetGuid(0).ToString());
                    w.WriteAttributeString(@"ParentID",   r.GetGuid(1).ToString());
                    w.WriteAttributeString(@"CreatedUTC", r.GetDateTime(2).ToString("yyyy-MM-dd HH:mm:ss"));
                    w.WriteAttributeString(@"UpdatedUTC", r.GetDateTime(3).ToString("yyyy-MM-dd HH:mm:ss"));
                    w.WriteElementString(@"Name", r.GetString(4).ToString());
                    w.WriteEndElement();
                }
                r.Close();
                // 紙
                cmd.CommandText = String.Format("select [ID], [ParentID], [Category], [CreatedUTC], [UpdatedUTC], [WordCount], [Title], [Content] from [Note] where [ID] in ({0})", sqlNoteID);
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    w.WriteStartElement(@"Note");
                    w.WriteAttributeString(@"ID",         r.GetGuid(0).ToString());
                    w.WriteAttributeString(@"ParentID",   r.GetGuid(1).ToString());
                    w.WriteAttributeString(@"Category",   r.GetString(2));
                    w.WriteAttributeString(@"CreatedUTC", r.GetDateTime(3).ToString("yyyy-MM-dd HH:mm:ss"));
                    w.WriteAttributeString(@"UpdatedUTC", r.GetDateTime(4).ToString("yyyy-MM-dd HH:mm:ss"));
                    w.WriteAttributeString(@"WordCount",  r.GetInt32(5).ToString());
                    w.WriteElementString(@"Title",   r.GetString(6));
                    w.WriteElementString(@"Content", r.GetString(7));
                    w.WriteEndElement();
                }
                r.Close();
                // End ROOT
                w.WriteEndElement();
            }

            return true;
        }
    }
}
