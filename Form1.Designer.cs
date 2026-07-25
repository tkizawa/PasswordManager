namespace PasswordManagerApp;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.ListView lvEntries;
    private System.Windows.Forms.ColumnHeader columnName;
    private System.Windows.Forms.ColumnHeader columnAccount;
    private System.Windows.Forms.ColumnHeader columnPasswordPreview;
    private System.Windows.Forms.Button btnCopyPassword;
    private System.Windows.Forms.Button btnDeleteEntry;
    private System.Windows.Forms.Button btnImportText;
    private System.Windows.Forms.Button btnExportText;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.TextBox txtAccount;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Button btnSaveEntry;
    private System.Windows.Forms.CheckBox cbShowPasswords;
    private System.Windows.Forms.Button btnClearSelection;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label lblAccount;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Panel panelLeft;
    private System.Windows.Forms.Panel panelRight;
    private System.Windows.Forms.Panel panelButtons;
    private System.Windows.Forms.Panel panelEntryFields;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        var mainLayout = new System.Windows.Forms.TableLayoutPanel();
        var leftLayout = new System.Windows.Forms.TableLayoutPanel();
        panelLeft = new System.Windows.Forms.Panel();
        panelRight = new System.Windows.Forms.Panel();
        lvEntries = new System.Windows.Forms.ListView();
        columnName = new System.Windows.Forms.ColumnHeader();
        columnAccount = new System.Windows.Forms.ColumnHeader();
        columnPasswordPreview = new System.Windows.Forms.ColumnHeader();
        panelButtons = new System.Windows.Forms.Panel();
        btnCopyPassword = new System.Windows.Forms.Button();
        btnDeleteEntry = new System.Windows.Forms.Button();
        btnImportText = new System.Windows.Forms.Button();
        btnExportText = new System.Windows.Forms.Button();
        btnClearSelection = new System.Windows.Forms.Button();
        panelEntryFields = new System.Windows.Forms.Panel();
        lblName = new System.Windows.Forms.Label();
        lblAccount = new System.Windows.Forms.Label();
        lblPassword = new System.Windows.Forms.Label();
        txtName = new System.Windows.Forms.TextBox();
        txtAccount = new System.Windows.Forms.TextBox();
        txtPassword = new System.Windows.Forms.TextBox();
        cbShowPasswords = new System.Windows.Forms.CheckBox();
        btnSaveEntry = new System.Windows.Forms.Button();
        lblStatus = new System.Windows.Forms.Label();
        SuspendLayout();

        mainLayout.ColumnCount = 2;
        mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
        mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
        mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        mainLayout.Location = new System.Drawing.Point(0, 0);
        mainLayout.Name = "mainLayout";
        mainLayout.RowCount = 1;
        mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        mainLayout.Size = new System.Drawing.Size(900, 600);
        mainLayout.TabIndex = 0;

        // 左パネルのレイアウト管理
        leftLayout.ColumnCount = 1;
        leftLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        leftLayout.RowCount = 3;
        leftLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        leftLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        leftLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        leftLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        leftLayout.Location = new System.Drawing.Point(0, 0);
        leftLayout.Name = "leftLayout";
        leftLayout.Padding = new System.Windows.Forms.Padding(10);

        panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;

        panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
        panelRight.Padding = new System.Windows.Forms.Padding(10);
        panelRight.Controls.Add(panelEntryFields);
        panelRight.Controls.Add(btnSaveEntry);
        panelRight.Controls.Add(cbShowPasswords);

        lvEntries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
        {
            columnName,
            columnAccount,
            columnPasswordPreview
        });
        lvEntries.Dock = System.Windows.Forms.DockStyle.Fill;
        lvEntries.FullRowSelect = true;
        lvEntries.GridLines = true;
        lvEntries.HideSelection = false;
        lvEntries.Location = new System.Drawing.Point(0, 0);
        lvEntries.MultiSelect = false;
        lvEntries.Name = "lvEntries";
        lvEntries.Size = new System.Drawing.Size(504, 380);
        lvEntries.TabIndex = 0;
        lvEntries.UseCompatibleStateImageBehavior = false;
        lvEntries.View = System.Windows.Forms.View.Details;
        lvEntries.SelectedIndexChanged += lvEntries_SelectedIndexChanged;

        columnName.Text = "名前";
        columnName.Width = 180;
        columnAccount.Text = "アカウント";
        columnAccount.Width = 180;
        columnPasswordPreview.Text = "パスワード";
        columnPasswordPreview.Width = 160;

        panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
        panelButtons.Location = new System.Drawing.Point(0, 0);
        panelButtons.Name = "panelButtons";
        panelButtons.Size = new System.Drawing.Size(504, 110);
        panelButtons.TabIndex = 1;

        btnCopyPassword.Location = new System.Drawing.Point(0, 0);
        btnCopyPassword.Name = "btnCopyPassword";
        btnCopyPassword.Size = new System.Drawing.Size(120, 32);
        btnCopyPassword.TabIndex = 1;
        btnCopyPassword.Text = "コピー";
        btnCopyPassword.UseVisualStyleBackColor = true;
        btnCopyPassword.Click += btnCopyPassword_Click;

        btnDeleteEntry.Location = new System.Drawing.Point(130, 0);
        btnDeleteEntry.Name = "btnDeleteEntry";
        btnDeleteEntry.Size = new System.Drawing.Size(120, 32);
        btnDeleteEntry.TabIndex = 2;
        btnDeleteEntry.Text = "削除";
        btnDeleteEntry.UseVisualStyleBackColor = true;
        btnDeleteEntry.Click += btnDeleteEntry_Click;

        btnClearSelection.Location = new System.Drawing.Point(260, 0);
        btnClearSelection.Name = "btnClearSelection";
        btnClearSelection.Size = new System.Drawing.Size(120, 32);
        btnClearSelection.TabIndex = 3;
        btnClearSelection.Text = "クリア";
        btnClearSelection.UseVisualStyleBackColor = true;
        btnClearSelection.Click += btnClearSelection_Click;

        btnImportText.Location = new System.Drawing.Point(0, 40);
        btnImportText.Name = "btnImportText";
        btnImportText.Size = new System.Drawing.Size(120, 32);
        btnImportText.TabIndex = 4;
        btnImportText.Text = "インポート";
        btnImportText.UseVisualStyleBackColor = true;
        btnImportText.Click += btnImportText_Click;

        btnExportText.Location = new System.Drawing.Point(130, 40);
        btnExportText.Name = "btnExportText";
        btnExportText.Size = new System.Drawing.Size(120, 32);
        btnExportText.TabIndex = 5;
        btnExportText.Text = "エクスポート";
        btnExportText.UseVisualStyleBackColor = true;
        btnExportText.Click += btnExportText_Click;

        lblStatus.AutoSize = false;
        lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
        lblStatus.Location = new System.Drawing.Point(0, 0);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new System.Drawing.Size(504, 20);
        lblStatus.TabIndex = 6;
        lblStatus.Text = "ロード中...";

        panelEntryFields.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        panelEntryFields.Location = new System.Drawing.Point(10, 10);
        panelEntryFields.Name = "panelEntryFields";
        panelEntryFields.Size = new System.Drawing.Size(324, 180);
        panelEntryFields.TabIndex = 7;

        lblName.AutoSize = true;
        lblName.Location = new System.Drawing.Point(0, 0);
        lblName.Name = "lblName";
        lblName.Size = new System.Drawing.Size(42, 20);
        lblName.TabIndex = 8;
        lblName.Text = "名前";

        txtName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        txtName.Location = new System.Drawing.Point(0, 24);
        txtName.Name = "txtName";
        txtName.Size = new System.Drawing.Size(324, 27);
        txtName.TabIndex = 9;

        lblAccount.AutoSize = true;
        lblAccount.Location = new System.Drawing.Point(0, 64);
        lblAccount.Name = "lblAccount";
        lblAccount.Size = new System.Drawing.Size(74, 20);
        lblAccount.TabIndex = 10;
        lblAccount.Text = "アカウント";

        txtAccount.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        txtAccount.Location = new System.Drawing.Point(0, 88);
        txtAccount.Name = "txtAccount";
        txtAccount.Size = new System.Drawing.Size(324, 27);
        txtAccount.TabIndex = 11;

        lblPassword.AutoSize = true;
        lblPassword.Location = new System.Drawing.Point(0, 128);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new System.Drawing.Size(66, 20);
        lblPassword.TabIndex = 12;
        lblPassword.Text = "パスワード";

        txtPassword.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        txtPassword.Location = new System.Drawing.Point(0, 152);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new System.Drawing.Size(324, 27);
        txtPassword.TabIndex = 13;
        txtPassword.UseSystemPasswordChar = true;

        cbShowPasswords.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
        cbShowPasswords.AutoSize = true;
        cbShowPasswords.Location = new System.Drawing.Point(10, 210);
        cbShowPasswords.Name = "cbShowPasswords";
        cbShowPasswords.Size = new System.Drawing.Size(178, 24);
        cbShowPasswords.TabIndex = 14;
        cbShowPasswords.Text = "リストでパスワードを表示";
        cbShowPasswords.UseVisualStyleBackColor = true;
        cbShowPasswords.CheckedChanged += cbShowPasswords_CheckedChanged;

        btnSaveEntry.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
        btnSaveEntry.Location = new System.Drawing.Point(10, 250);
        btnSaveEntry.Name = "btnSaveEntry";
        btnSaveEntry.Size = new System.Drawing.Size(120, 36);
        btnSaveEntry.TabIndex = 15;
        btnSaveEntry.Text = "Add";
        btnSaveEntry.UseVisualStyleBackColor = true;
        btnSaveEntry.Click += btnSaveEntry_Click;

        panelEntryFields.Controls.Add(lblName);
        panelEntryFields.Controls.Add(txtName);
        panelEntryFields.Controls.Add(lblAccount);
        panelEntryFields.Controls.Add(txtAccount);
        panelEntryFields.Controls.Add(lblPassword);
        panelEntryFields.Controls.Add(txtPassword);

        panelButtons.Controls.Add(btnCopyPassword);
        panelButtons.Controls.Add(btnDeleteEntry);
        panelButtons.Controls.Add(btnClearSelection);
        panelButtons.Controls.Add(btnImportText);
        panelButtons.Controls.Add(btnExportText);

        leftLayout.Controls.Add(lvEntries, 0, 0);
        leftLayout.Controls.Add(panelButtons, 0, 1);
        leftLayout.Controls.Add(lblStatus, 0, 2);

        panelLeft.Controls.Add(leftLayout);

        panelRight.Controls.Add(panelEntryFields);
        panelRight.Controls.Add(cbShowPasswords);
        panelRight.Controls.Add(btnSaveEntry);

        mainLayout.Controls.Add(panelLeft, 0, 0);
        mainLayout.Controls.Add(panelRight, 1, 0);

        ClientSize = new System.Drawing.Size(900, 600);
        Controls.Add(mainLayout);
        Name = "Form1";
        Text = "WoodStream PasswordManager";
        ResumeLayout(false);
    }

    #endregion
}
