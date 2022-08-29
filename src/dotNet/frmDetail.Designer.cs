namespace StuffCraft
{
    partial class frmDetail
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetail));
            this.fileStatus = new System.Windows.Forms.StatusStrip();
            this.statusID = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.textTitle = new System.Windows.Forms.TextBox();
            this.labelWordCount = new System.Windows.Forms.Label();
            this.textWordCount = new System.Windows.Forms.TextBox();
            this.labelContent = new System.Windows.Forms.Label();
            this.textContent = new System.Windows.Forms.TextBox();
            this.btnRefine = new System.Windows.Forms.Button();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.fileStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileStatus
            // 
            this.fileStatus.AccessibleDescription = null;
            this.fileStatus.AccessibleName = null;
            resources.ApplyResources(this.fileStatus, "fileStatus");
            this.fileStatus.BackgroundImage = null;
            this.fileStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusID});
            this.fileStatus.Name = "fileStatus";
            // 
            // statusID
            // 
            this.statusID.AccessibleDescription = null;
            this.statusID.AccessibleName = null;
            resources.ApplyResources(this.statusID, "statusID");
            this.statusID.BackgroundImage = null;
            this.statusID.Name = "statusID";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = null;
            this.btnOK.AccessibleName = null;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackgroundImage = null;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = null;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // labelTitle
            // 
            this.labelTitle.AccessibleDescription = null;
            this.labelTitle.AccessibleName = null;
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.labelTitle.Font = null;
            this.labelTitle.Name = "labelTitle";
            // 
            // textTitle
            // 
            this.textTitle.AccessibleDescription = null;
            this.textTitle.AccessibleName = null;
            resources.ApplyResources(this.textTitle, "textTitle");
            this.textTitle.BackgroundImage = null;
            this.textTitle.Name = "textTitle";
            // 
            // labelWordCount
            // 
            this.labelWordCount.AccessibleDescription = null;
            this.labelWordCount.AccessibleName = null;
            resources.ApplyResources(this.labelWordCount, "labelWordCount");
            this.labelWordCount.Font = null;
            this.labelWordCount.Name = "labelWordCount";
            // 
            // textWordCount
            // 
            this.textWordCount.AccessibleDescription = null;
            this.textWordCount.AccessibleName = null;
            resources.ApplyResources(this.textWordCount, "textWordCount");
            this.textWordCount.BackColor = System.Drawing.SystemColors.Window;
            this.textWordCount.BackgroundImage = null;
            this.textWordCount.Name = "textWordCount";
            this.textWordCount.ReadOnly = true;
            // 
            // labelContent
            // 
            this.labelContent.AccessibleDescription = null;
            this.labelContent.AccessibleName = null;
            resources.ApplyResources(this.labelContent, "labelContent");
            this.labelContent.Font = null;
            this.labelContent.Name = "labelContent";
            // 
            // textContent
            // 
            this.textContent.AcceptsReturn = true;
            this.textContent.AcceptsTab = true;
            this.textContent.AccessibleDescription = null;
            this.textContent.AccessibleName = null;
            resources.ApplyResources(this.textContent, "textContent");
            this.textContent.BackgroundImage = null;
            this.textContent.Name = "textContent";
            this.textContent.TextChanged += new System.EventHandler(this.textContent_TextChanged);
            // 
            // btnRefine
            // 
            this.btnRefine.AccessibleDescription = null;
            this.btnRefine.AccessibleName = null;
            resources.ApplyResources(this.btnRefine, "btnRefine");
            this.btnRefine.BackgroundImage = null;
            this.btnRefine.Font = null;
            this.btnRefine.Name = "btnRefine";
            this.btnRefine.UseVisualStyleBackColor = true;
            this.btnRefine.Click += new System.EventHandler(this.btnRefine_Click);
            // 
            // comboType
            // 
            this.comboType.AccessibleDescription = null;
            this.comboType.AccessibleName = null;
            resources.ApplyResources(this.comboType, "comboType");
            this.comboType.BackgroundImage = null;
            this.comboType.Font = null;
            this.comboType.FormattingEnabled = true;
            this.comboType.Name = "comboType";
            // 
            // labelType
            // 
            this.labelType.AccessibleDescription = null;
            this.labelType.AccessibleName = null;
            resources.ApplyResources(this.labelType, "labelType");
            this.labelType.Font = null;
            this.labelType.Name = "labelType";
            // 
            // frmDetail
            // 
            this.AcceptButton = this.btnOK;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.comboType);
            this.Controls.Add(this.btnRefine);
            this.Controls.Add(this.textContent);
            this.Controls.Add(this.labelContent);
            this.Controls.Add(this.textWordCount);
            this.Controls.Add(this.labelWordCount);
            this.Controls.Add(this.textTitle);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.fileStatus);
            this.Name = "frmDetail";
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.frmDetail_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDetail_FormClosing);
            this.fileStatus.ResumeLayout(false);
            this.fileStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip fileStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textTitle;
        private System.Windows.Forms.Label labelWordCount;
        private System.Windows.Forms.TextBox textWordCount;
        private System.Windows.Forms.Label labelContent;
        private System.Windows.Forms.TextBox textContent;
        private System.Windows.Forms.Button btnRefine;
        private System.Windows.Forms.ToolStripStatusLabel statusID;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Label labelType;
    }
}