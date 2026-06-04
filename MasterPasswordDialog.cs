namespace PasswordManagerApp;

public partial class MasterPasswordDialog : Form
{
    public string MasterPassword { get; private set; } = string.Empty;
    private readonly bool _isNewStore;
    private readonly TextBox _txtPassword;
    private readonly TextBox _txtConfirm;

    public MasterPasswordDialog(bool isNewStore)
    {
        _isNewStore = isNewStore;
        _txtPassword = new TextBox();
        _txtConfirm = new TextBox();
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        var lblTitle = new Label();
        var lblPassword = new Label();
        var lblConfirm = new Label();
        var btnOK = new Button();
        var btnCancel = new Button();

        SuspendLayout();

        // フォーム設定
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(400, _isNewStore ? 200 : 150);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "マスターパスワード";
        Icon = null;

        // タイトルラベル
        lblTitle.AutoSize = true;
        lblTitle.Location = new Point(12, 12);
        lblTitle.Text = _isNewStore ? "新しいマスターパスワードを設定してください" : "マスターパスワードを入力してください";

        // パスワードラベル
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(12, 40);
        lblPassword.Text = "パスワード:";

        // パスワードテキストボックス
        _txtPassword.Location = new Point(12, 60);
        _txtPassword.Name = "txtPassword";
        _txtPassword.Size = new Size(376, 27);
        _txtPassword.UseSystemPasswordChar = true;

        // 確認ラベル
        lblConfirm.AutoSize = true;
        lblConfirm.Location = new Point(12, 95);
        lblConfirm.Text = "パスワードの確認:";
        lblConfirm.Visible = _isNewStore;

        // 確認テキストボックス
        _txtConfirm.Location = new Point(12, 115);
        _txtConfirm.Name = "txtConfirm";
        _txtConfirm.Size = new Size(376, 27);
        _txtConfirm.UseSystemPasswordChar = true;
        _txtConfirm.Visible = _isNewStore;

        // OKボタン
        btnOK.DialogResult = DialogResult.None;
        btnOK.Location = new Point(232, _isNewStore ? 160 : 110);
        btnOK.Name = "btnOK";
        btnOK.Size = new Size(75, 23);
        btnOK.TabIndex = _isNewStore ? 3 : 2;
        btnOK.Text = "OK";
        btnOK.UseVisualStyleBackColor = true;
        btnOK.Click += BtnOK_Click;

        // キャンセルボタン
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(313, _isNewStore ? 160 : 110);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(75, 23);
        btnCancel.TabIndex = _isNewStore ? 4 : 3;
        btnCancel.Text = "キャンセル";
        btnCancel.UseVisualStyleBackColor = true;

        // フォームにコントロール追加
        Controls.Add(lblTitle);
        Controls.Add(lblPassword);
        Controls.Add(_txtPassword);
        Controls.Add(lblConfirm);
        Controls.Add(_txtConfirm);
        Controls.Add(btnOK);
        Controls.Add(btnCancel);

        AcceptButton = btnOK;
        CancelButton = btnCancel;

        ResumeLayout(false);
        PerformLayout();
    }

    private void BtnOK_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_txtPassword.Text))
        {
            MessageBox.Show("パスワードを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_isNewStore)
        {
            if (_txtPassword.Text != _txtConfirm.Text)
            {
                MessageBox.Show("パスワードが一致しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_txtPassword.Text.Length < 6)
            {
                MessageBox.Show("パスワードは6文字以上である必要があります", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        MasterPassword = _txtPassword.Text;
        DialogResult = DialogResult.OK;
        Close();
    }
}
