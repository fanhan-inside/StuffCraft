using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace StuffCraft
{
    /// <summary>
    /// メーン画面
    /// </summary>
    public partial class frmStuff : Form
    {
#if false
        clsDBOperAccess m_DBOper = new clsDBOperAccess();
#else
        clsDBOperSQLite m_DBOper = new clsDBOperSQLite();
#endif
        Dictionary<int, bool> m_ColumnOrder = null;
        TreeNode m_DragTargetNode = null;

        /// <summary>
        /// 構造函数
        /// </summary>
        public frmStuff()
        {
            InitializeComponent();
        }

        #region 私用関数

        /// <summary>
        /// ツリービューを初期化
        /// </summary>
        /// <returns></returns>
        bool InitMainTree()
        {
            mainTree.Nodes.Clear();
            TreeNode tnRoot = new TreeNode();
            tnRoot.Tag = Guid.Empty;
            tnRoot.Text = @"StuffCraft";
            mainTree.Nodes.Add(tnRoot);
            Guid[] IDs = m_DBOper.GetChildrenCaseIDs(Guid.Empty);
            foreach (Guid id in IDs)
            {
                TreeNode tn = new TreeNode();
                buildTreeNode(tn, id);
                tnRoot.Nodes.Add(tn);
            }
            if (tnRoot.Nodes.Count > 0)
            {
                tnRoot.Expand();
                mainTree.SelectedNode = tnRoot.Nodes[0];
                tnRoot.Nodes[0].Expand();
                InitMainList((Guid)tnRoot.Nodes[0].Tag);
            }
            else
            {
                mainTree.SelectedNode = tnRoot;
                InitMainList((Guid)tnRoot.Tag);
            }
            return true;
        }
        /// <summary>
        /// ツリー結点を作成
        /// </summary>
        /// <param name="tn"></param>
        /// <param name="id"></param>
        void buildTreeNode(TreeNode tn, Guid id)
        {
            clsCase Case = m_DBOper.GetCaseFromID(id);
            if (Case != null)
            {
                tn.Text = Case.Name;
                tn.Tag = Case.ID;
                tn.Nodes.Clear();
                Guid[] IDs = m_DBOper.GetChildrenCaseIDs(id);
                foreach (Guid idChild in IDs)
                {
                    TreeNode tnChild = new TreeNode();
                    buildTreeNode(tnChild, idChild);
                    tn.Nodes.Add(tnChild);
                }
            }
        }
        /// <summary>
        /// リストビューを初期化
        /// </summary>
        /// <param name="id">ツリービューで選択された箱のID</param>
        void InitMainList(Guid id)
        {
            mainList.Tag = id;
            mainList.Items.Clear();
            clsNote[] notes = m_DBOper.GetChildrenNoteWithoutContent(id);
            foreach (clsNote k in notes)
            {
                ListViewItem lvi = buildListViewItem(k);
                mainList.Items.Add(lvi);
            }
        }
        /// <summary>
        /// リストビューの結点を作成
        /// </summary>
        /// <param name="note">紙</param>
        /// <returns></returns>
        ListViewItem buildListViewItem(clsNote note)
        {
            ListViewItem lvi = new ListViewItem();
            if (note.Category != null)
                lvi.Text = note.Category;
            else
                lvi.Text = @"(unknown)";
            lvi.ImageKey = @"Note";
            lvi.Tag = note.ID;
            lvi.SubItems.Add(note.Title);
            lvi.SubItems.Add(note.WordCount.ToString("#,###"));
            lvi.SubItems.Add(note.UpdatedDT.ToString("yyyy-MM-dd HH:mm:ss"));
            return lvi;
        }
        /// <summary>
        /// テキストビューを初期化
        /// </summary>
        /// <param name="id">リストビューで選択された紙のID</param>
        /// <param name="text">紙の内容</param>
        void InitMainText(Guid id, string text)
        {
            mainText.Tag = id;
            mainText.Text = text;
        }
        /// <summary>
        /// 子結点についての判断
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            if (node2.Parent == null)
                return false;
            if (node2.Parent.Equals(node1))
                return true;

            return ContainsNode(node1, node2.Parent);
        }
        /// <summary>
        /// 紙を編集
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        bool EditNote(ref clsNote note)
        {

            frmDetail dlg = new frmDetail();
            // 情報を入れる
            dlg.m_CategoryList = m_DBOper.BuildCategoryList();
            dlg.m_ID = note.ID;
            dlg.m_Category = note.Category;
            dlg.m_Title = note.Title;
            dlg.m_Content = note.Content;
            dlg.m_WordCount = note.WordCount;
            // 画面を表示する
            DialogResult dr = dlg.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                // 紙を更新
                note.Category = dlg.m_Category;
                note.Title = dlg.m_Title;
                note.Content = dlg.m_Content;
                note.WordCount = dlg.m_WordCount;
                note.UpdatedDT = DateTime.Now;
                if (m_DBOper.NoteExists(note.ID))
                {
                    m_DBOper.UpdateNote(note);
                    return true;
                }
            }
            return false;
        }
        #endregion 私用関数

        private void frmStuff_Load(object sender, EventArgs e)
        {
            // 初期化
            m_DBOper.m_DBName = Application.StartupPath + @"\StuffCraft.fsc";
            m_DBOper.m_sqlCreateDB =
                "CREATE TABLE [Case] (CreatedUTC TEXT, ID TEXT, Name TEXT, ParentID TEXT, UpdatedUTC TEXT, ItemCount INTEGER);" +
                "CREATE UNIQUE INDEX _Index_Case ON [Case](ID ASC);" +
                "CREATE TABLE Note (Content TEXT, CreatedUTC TEXT, ID TEXT, ParentID TEXT, Title TEXT, Category TEXT, UpdatedUTC TEXT, WordCount INTEGER, Deleted INTEGER);" +
                "CREATE UNIQUE INDEX _Index_Note ON Note(ID ASC);";
            m_DBOper.Open();

            // DB走査して、ツリー初期化
            InitMainTree();

            // リストのソートオーダーを初期化
            m_ColumnOrder = new Dictionary<int, bool>();
            m_ColumnOrder.Clear();
            foreach (ColumnHeader ch in mainList.Columns)
            {
                m_ColumnOrder.Add(ch.Index, true); // Ascending
            }
        }

        private void frmStuff_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_DBOper.Close();
        }

        private void mainTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (mainTree.SelectedNode != null)
                InitMainList((Guid)mainTree.SelectedNode.Tag);
        }

        private void mainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            // 選択されたアイテムは無し
            //
            if (mainList.SelectedItems.Count == 0)
            {
                InitMainText(Guid.Empty, "");
                // 紙の編集を禁止する
                editNoteMainMenuItem.Enabled = false;
                editNoteListMenuItem.Enabled = false;
                editNoteToolStripButton.Enabled = false;
                // 紙の出力を禁止する
                exportNoteMainMenuItem.Enabled = false;
                exportNoteListMenuItem.Enabled = false;
                exportNoteToolStripButton.Enabled = false;
                // 紙の削除を禁止する
                deleteNoteMainMenuItem.Enabled = false;
                deleteNoteListMenuItem.Enabled = false;
                deleteNoteToolStripButton.Enabled = false;
                return;
            }
            //
            // 選択されたアイテムは有り
            //
            // 紙の削除を許容する
            deleteNoteMainMenuItem.Enabled = true;
            deleteNoteListMenuItem.Enabled = true;
            deleteNoteToolStripButton.Enabled = true;
            if (mainList.SelectedItems.Count == 1)
            {
                // 紙の編集を許容する
                editNoteMainMenuItem.Enabled = true;
                editNoteListMenuItem.Enabled = true;
                editNoteToolStripButton.Enabled = true;
                // 紙の出力を許容する
                exportNoteMainMenuItem.Enabled = true;
                exportNoteListMenuItem.Enabled = true;
                exportNoteToolStripButton.Enabled = true;
                // 選択された紙の内容を表示する
                Guid id = (Guid)mainList.SelectedItems[0].Tag;
                InitMainText(id, m_DBOper.GetNoteContent(id));
            }
            else
            {
                // 紙の編集を禁止する
                editNoteMainMenuItem.Enabled = false;
                editNoteListMenuItem.Enabled = false;
                editNoteToolStripButton.Enabled = false;
                // 紙の出力を禁止する
                exportNoteMainMenuItem.Enabled = false;
                exportNoteListMenuItem.Enabled = false;
                exportNoteToolStripButton.Enabled = false;
            }
        }

        private void mainTree_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // 根結点のラベルは編集不可
            if (e.Node == null || Guid.Empty == (Guid) e.Node.Tag)
            {
                e.CancelEdit = true;
                return;
            }
        }

        private void mainTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                return;
            }
            // 空白不可
            if (e.Label.Length <= 0)
            {
                e.CancelEdit = true;
                return;
            }
            // 無効字符不可
            if (e.Label.IndexOfAny("\t\v\a\r\n".ToCharArray()) != -1)
            {
                e.CancelEdit = true;
                MessageBox.Show(clsMessage.INVALID_NAME, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Node.BeginEdit();
            }
            // できたら、更新する
            if (e.Label != e.Node.Text)
            {
                if (m_DBOper.CaseExists((Guid)e.Node.Tag))
                    m_DBOper.UpdateCaseName((Guid)e.Node.Tag, e.Label);
            }
        }

        private void mainTree_Enter(object sender, EventArgs e)
        {
            newCaseMainMenuItem.Enabled = true;
            newCaseTreeMenuItem.Enabled = true;
            newCaseToolStripButton.Enabled = true;
            deleteCaseMainMenuItem.Enabled = true;
            deleteCaseTreeMenuItem.Enabled = true;
            deleteCaseToolStripButton.Enabled = true;
            renameCaseMainMenuItem.Enabled = true;
            renameCaseTreeMenuItem.Enabled = true;
            renameCaseToolStripButton.Enabled = true;
            exportCaseMainMenuItem.Enabled = true;
            exportCaseTreeMenuItem.Enabled = true;
            exportCaseToolStripButton.Enabled = true;
        }

        private void mainTree_Leave(object sender, EventArgs e)
        {
            newCaseMainMenuItem.Enabled = false;
            newCaseTreeMenuItem.Enabled = false;
            newCaseToolStripButton.Enabled = false;
            deleteCaseMainMenuItem.Enabled = false;
            deleteCaseTreeMenuItem.Enabled = false;
            deleteCaseToolStripButton.Enabled = false;
            renameCaseMainMenuItem.Enabled = false;
            renameCaseTreeMenuItem.Enabled = false;
            renameCaseToolStripButton.Enabled = false;
            exportCaseMainMenuItem.Enabled = false;
            exportCaseTreeMenuItem.Enabled = false;
            exportCaseToolStripButton.Enabled = false;
        }

        private void exportCaseCommand_Click(object sender, EventArgs e)
        {
            /************
             * 箱の出力 *
             ************/
            if (mainTree.SelectedNode == null)
                return;
            Guid id = (Guid)mainTree.SelectedNode.Tag;
            if (id == Guid.Empty)
                return;
            // 箱を出力
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.FileName = sfd.InitialDirectory + "\\StuffCraft.xml";
            sfd.DefaultExt = @".xml";
            sfd.Filter = @"XML File (*.xml)|*.xml||";
            DialogResult dr = sfd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                m_DBOper.ExportData(id, sfd.FileName);
                MessageBox.Show(clsMessage.EXPORT_OK_MESSAGE, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void newCaseCommand_Click(object sender, EventArgs e)
        {
            /****************
             * 新規箱の作成 *
             ****************/

            if (mainTree.SelectedNode == null)
                return;

            Guid currentCaseID = (Guid)mainTree.SelectedNode.Tag;
            Debug.Print("Current Case ID: {0}", currentCaseID);
            string newCaseName = clsMessage.NEW_CASE_NAME;

            // 箱ノード作成
            Guid newCaseID = Guid.NewGuid();
            TreeNode newCaseNode = new TreeNode(newCaseName);
            newCaseNode.Name = newCaseName;
            newCaseNode.Tag = newCaseID;
            mainTree.SelectedNode.Nodes.Add(newCaseNode);

            // 箱データ作成
            m_DBOper.AddCase(new clsCase(newCaseID, currentCaseID, newCaseName, DateTime.Now, DateTime.Now, 0));

            // 現在の箱を展開
            mainTree.SelectedNode.Expand();
        }

        private void deleteCaseCommand_Click(object sender, EventArgs e)
        {
            /************
             * 箱の削除 *
             ************/

            if (mainTree.SelectedNode == null ||
                Guid.Empty == (Guid)mainTree.SelectedNode.Tag)
            {
                // 根結点削除不可
                return;
            }

            string msg = String.Format(clsMessage.TEMPLATE_DELETE_CASE, mainTree.SelectedNode.Text);
            DialogResult dr = MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                if (m_DBOper.CaseExists((Guid)mainTree.SelectedNode.Tag))
                    m_DBOper.DeleteCase((Guid)mainTree.SelectedNode.Tag);
                mainTree.SelectedNode.Nodes.Clear();
                if (mainTree.SelectedNode.Parent != null)
                    mainTree.SelectedNode.Parent.Nodes.Remove(mainTree.SelectedNode);
                if (mainTree.SelectedNode != null)
                    InitMainList((Guid)mainTree.SelectedNode.Tag);
            }
        }

        private void renameCaseCommand_Click(object sender, EventArgs e)
        {
            /******************
             * 箱の名称の変更 *
             ******************/

            // 根結点だったら編集不可
            if (mainTree.SelectedNode != null &&
                Guid.Empty != (Guid) mainTree.SelectedNode.Tag)
            {
                mainTree.SelectedNode.BeginEdit();
            }
        }

        private void mainList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo ti = mainList.HitTest(e.Location);
            if (e.Button == MouseButtons.Left)
            {
                if (ti.Item != null) // 既存紙を編集
                {
                    Guid NoteID = (Guid)ti.Item.Tag;
                    clsNote note = m_DBOper.GetNoteFromID(NoteID);
                    if (EditNote(ref note))
                    {
                        ti.Item.SubItems[0].Text = note.Category;
                        ti.Item.SubItems[1].Text = note.Title;
                        ti.Item.SubItems[2].Text = note.WordCount.ToString();
                        ti.Item.SubItems[3].Text = note.UpdatedDT.ToString("yyyy-MM-dd HH:mm:ss");
                        InitMainText(note.ID, note.Content);
                    }
                }
            }
        }

        private void exportNoteCommand_Click(object sender, EventArgs e)
        {
            /************
             * 紙の出力 *
             ************/

            if (mainList.SelectedItems.Count != 1)
                return;

            // 無効キャラクタを置換
            string filename = FileNameRefiner.Refine(mainList.SelectedItems[0].SubItems[1].Text);

            // 紙を出力
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.FileName = sfd.InitialDirectory + "\\" + filename + ".txt";
            sfd.DefaultExt = @".txt";
            sfd.Filter = @"Text File (*.txt)|*.txt||";
            DialogResult dr = sfd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName, false, new UTF8Encoding(true, false));
                sw.Write(mainText.Text);
                sw.Flush();
                sw.Close();
                MessageBox.Show(clsMessage.EXPORT_OK_MESSAGE, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void newNoteCommand_Click(object sender, EventArgs e)
        {
            /****************
             * 新規紙の作成 *
             ****************/

            clsNote note = new clsNote(Guid.NewGuid(), (Guid)mainList.Tag, "", clsMessage.NEW_NOTE_NAME, DateTime.Now, DateTime.Now);
            frmDetail dlg = new frmDetail();
            dlg.m_CategoryList = m_DBOper.BuildCategoryList();
            dlg.m_ID = note.ID;
            dlg.m_Category = note.Category;
            dlg.m_Title = note.Title;
            dlg.m_Content = note.Content;
            dlg.m_WordCount = note.WordCount;
            DialogResult dr = dlg.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                //
                note.Category = dlg.m_Category;
                note.Title = dlg.m_Title;
                note.Content = dlg.m_Content;
                note.WordCount = dlg.m_WordCount;
                note.CreatedDT = DateTime.Now;
                note.UpdatedDT = DateTime.Now;
                // DBに追加
                m_DBOper.AddNote(note);
                // 画面更新
                mainList.SelectedItems.Clear();
                mainList.Items.Add(buildListViewItem(note)).Selected = true;
            }
        }

        private void editNoteCommand_Click(object sender, EventArgs e)
        {
            /************
             * 紙の編集 *
             ************/

            if (mainList.SelectedItems.Count <= 0)
                return;

            Guid NoteID = (Guid)mainList.SelectedItems[0].Tag;
            clsNote note = m_DBOper.GetNoteFromID(NoteID);
            if (EditNote(ref note))
            {
                mainList.SelectedItems[0].SubItems[0].Text = note.Category;
                mainList.SelectedItems[0].SubItems[1].Text = note.Title;
                mainList.SelectedItems[0].SubItems[2].Text = note.WordCount.ToString();
                mainList.SelectedItems[0].SubItems[3].Text = note.UpdatedDT.ToString("yyyy-MM-dd HH:mm:ss");
                InitMainText(note.ID, note.Content);
            }
        }

        private void deleteNoteCommand_Click(object sender, EventArgs e)
        {
            /************
             * 紙の削除 *
             ************/

            if (mainList.SelectedItems == null || mainList.SelectedItems.Count <= 0)
                return;

            string msg = String.Format(clsMessage.TEMPLATE_DELETE_NOTE, mainList.SelectedItems.Count);
            DialogResult dr = MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                List<Guid> IDs = new List<Guid>();
                foreach (ListViewItem lvi in mainList.SelectedItems)
                {
                    IDs.Add((Guid)lvi.Tag);
                }
                m_DBOper.DeleteNote(IDs.ToArray());
                InitMainList((Guid)mainList.Tag);
                InitMainText(Guid.Empty, "");
            }
        }

        private void mainList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    newNoteCommand_Click(this, e);
                    break;
                case Keys.Delete:
                    deleteNoteCommand_Click(this, e);
                    break;
                default:
                    break;
            }
        }

        private void mainList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            m_ColumnOrder[e.Column] = !(m_ColumnOrder[e.Column]);
            mainList.ListViewItemSorter = new ListViewItemComparer(e.Column, m_ColumnOrder[e.Column]);
        }

        private void mainTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void mainList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                List<Guid> IDs = new List<Guid>();
                foreach (ListViewItem lvi in mainList.SelectedItems)
                {
                    IDs.Add((Guid)lvi.Tag);
                }
                if (IDs.Count > 0)
                    DoDragDrop(IDs.ToArray(), DragDropEffects.Move);
            }
        }

        private void mainTree_DragDrop(object sender, DragEventArgs e)
        {
            // 目標フォルダ
            TreeNode targetNode = mainTree.GetNodeAt(mainTree.PointToClient(new Point(e.X, e.Y)));

            Guid[] draggedIDs = (Guid[])e.Data.GetData(typeof(Guid[]));
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // 箱をドラッグの場合
            if (draggedNode != null &&
                !draggedNode.Equals(targetNode) &&
                !ContainsNode(draggedNode, targetNode))
            {
                if (e.Effect == DragDropEffects.Move)
                {
                    // TreeView 更新
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);

                    // ディスク更新
                    if (m_DBOper.CaseExists((Guid)draggedNode.Tag))
                        m_DBOper.ChangeCaseParent((Guid)draggedNode.Tag, (Guid)targetNode.Tag);
                }
                else if (e.Effect == DragDropEffects.Copy)
                {
                    TreeNode tn = (TreeNode)draggedNode.Clone();
                    clsCase Case = m_DBOper.GetCaseFromID((Guid)tn.Tag);
                    Case.ID = Guid.NewGuid();
                    m_DBOper.AddCase(Case);
                    tn.Tag = Case.ID;
                    targetNode.Nodes.Add(tn);
                }
                targetNode.Expand();
                mainTree.SelectedNode = targetNode;
            }

            // 紙をドラッグの場合
            if (draggedIDs != null && draggedIDs.Length > 0)
            {
                m_DBOper.ChangeNoteParent(draggedIDs, (Guid)targetNode.Tag);
                InitMainList((Guid)targetNode.Tag);
                targetNode.Expand();
                mainTree.SelectedNode = targetNode;
            }

            //
            if (m_DragTargetNode != null)
            {
                m_DragTargetNode.BackColor = SystemColors.Window;
                m_DragTargetNode.ForeColor = SystemColors.WindowText;
                m_DragTargetNode = null;
            }
        }

        private void mainTree_DragEnter(object sender, DragEventArgs e)
        {
            TreeNode tn = (TreeNode)e.Data.GetData(typeof(TreeNode));
            Guid[] ic = (Guid[])e.Data.GetData(typeof(Guid[]));
            if (tn == null && ic == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            e.Effect = e.AllowedEffect;
        }

        private void mainTree_DragOver(object sender, DragEventArgs e)
        {
            TreeNode tn = mainTree.GetNodeAt(mainTree.PointToClient(new Point(e.X, e.Y)));
            if (tn == m_DragTargetNode)
                return;

            if (m_DragTargetNode != null)
            {
                m_DragTargetNode.BackColor = SystemColors.Window;
                m_DragTargetNode.ForeColor = SystemColors.WindowText;
            }
            m_DragTargetNode = tn;
            if (m_DragTargetNode != null)
            {
                m_DragTargetNode.BackColor = SystemColors.Highlight;
                m_DragTargetNode.ForeColor = SystemColors.HighlightText;
            }
        }

        private void menuTree_Opening(object sender, CancelEventArgs e)
        {
            //
            // TreeViewでのコンテクストメニュー出すとき
            //
            Point pt = MousePosition;
            pt = mainTree.PointToClient(pt);
            TreeViewHitTestInfo ti = mainTree.HitTest(pt);
            if (ti.Location != TreeViewHitTestLocations.Label &&
                ti.Location != TreeViewHitTestLocations.Image)
            {
                e.Cancel = true;
            }
            mainTree.SelectedNode = ti.Node;
        }

        private void mainTree_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    newCaseCommand_Click(this, e);
                    break;
                case Keys.Delete:
                    deleteCaseCommand_Click(this, e);
                    break;
                default:
                    break;
            }
        }

        private void exitMainMenuItem_Click(object sender, EventArgs e)
        {
            // 終了
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // バージョン情報
            using (frmAbout frm = new frmAbout())
            {
                frm.ShowDialog();
            }
        }
    }
}
