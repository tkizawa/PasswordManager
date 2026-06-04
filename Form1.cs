using PasswordManagerApp.Models;
using PasswordManagerApp.Services;

namespace PasswordManagerApp;

public partial class Form1 : Form
{
    private readonly PasswordStore _store;
    private readonly UserSettings _userSettings;
    private PasswordEntry? _selectedEntry;

    public Form1()
    {
        InitializeComponent();
        _userSettings = UserSettingsManager.Load();
        ApplyWindowSettings();
        _store = new PasswordStore(GetDataFilePath());
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        // マスターパスワード入力ダイアログを表示
        if (!ShowMasterPasswordDialog())
        {
            Close();
            return;
        }

        LoadEntries();
    }

    /// <summary>
    /// マスターパスワード入力ダイアログを表示
    /// </summary>
    private bool ShowMasterPasswordDialog()
    {
        var dataPath = GetDataFilePath();
        var isNewStore = !File.Exists(dataPath);

        while (true)
        {
            var dialog = new MasterPasswordDialog(isNewStore);
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return false;
            }

            var masterPassword = dialog.MasterPassword;

            try
            {
                _store.Initialize(masterPassword);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラー: {ex.Message}", "マスターパスワード", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isNewStore = false; // 次は既存ファイルとして扱う
            }
        }
    }

    private static string GetDataFilePath()
    {
        var appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PasswordManagerApp");
        return Path.Combine(appFolder, "passwords.json");
    }

    private void ApplyWindowSettings()
    {
        if (_userSettings.WindowWidth > 0 && _userSettings.WindowHeight > 0)
        {
            StartPosition = FormStartPosition.Manual;
            Bounds = new Rectangle(_userSettings.WindowLeft, _userSettings.WindowTop, _userSettings.WindowWidth, _userSettings.WindowHeight);
        }

        if (_userSettings.WindowState != FormWindowState.Minimized)
        {
            WindowState = _userSettings.WindowState;
        }

        if (_userSettings.ColumnWidths?.Count >= 3)
        {
            columnName.Width = _userSettings.ColumnWidths[0];
            columnAccount.Width = _userSettings.ColumnWidths[1];
            columnPasswordPreview.Width = _userSettings.ColumnWidths[2];
        }
    }

    private void SaveWindowSettings()
    {
        var bounds = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;
        _userSettings.WindowLeft = bounds.Left;
        _userSettings.WindowTop = bounds.Top;
        _userSettings.WindowWidth = bounds.Width;
        _userSettings.WindowHeight = bounds.Height;
        _userSettings.WindowState = WindowState;
        _userSettings.ColumnWidths = new List<int>
        {
            columnName.Width,
            columnAccount.Width,
            columnPasswordPreview.Width
        };

        UserSettingsManager.Save(_userSettings);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        SaveWindowSettings();
        base.OnFormClosing(e);
    }

    private void LoadEntries()
    {
        lvEntries.Items.Clear();

        foreach (var entry in _store.Entries)
        {
            var item = new ListViewItem(entry.Name);
            item.SubItems.Add(entry.Account);
            item.SubItems.Add(cbShowPasswords.Checked ? entry.Password : new string('•', Math.Max(entry.Password.Length, 8)));
            item.Tag = entry;
            lvEntries.Items.Add(item);
        }

        UpdateControls();
    }

    private void UpdateControls()
    {
        var hasSelection = lvEntries.SelectedItems.Count > 0;
        btnCopyPassword.Enabled = hasSelection;
        btnDeleteEntry.Enabled = hasSelection;
        btnClearSelection.Enabled = hasSelection;
        btnSaveEntry.Text = _selectedEntry == null ? "Add" : "Update";
        lblStatus.Text = hasSelection ? "選択されたエントリを編集できます。" : "新しいエントリを追加してください。";
    }

    private void btnSaveEntry_Click(object sender, EventArgs e)
    {
        var name = txtName.Text.Trim();
        var account = txtAccount.Text.Trim();
        var password = txtPassword.Text;

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("名前、アカウント名、パスワードをすべて入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_selectedEntry == null)
        {
            _store.Entries.Add(new PasswordEntry
            {
                Name = name,
                Account = account,
                Password = password
            });
        }
        else
        {
            _selectedEntry.Name = name;
            _selectedEntry.Account = account;
            _selectedEntry.Password = password;
            _selectedEntry = null;
        }

        _store.Save();
        LoadEntries();
        ClearEntryFields();
    }

    private void lvEntries_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvEntries.SelectedItems.Count != 1)
        {
            _selectedEntry = null;
            ClearEntryFields();
            UpdateControls();
            return;
        }

        _selectedEntry = (PasswordEntry)lvEntries.SelectedItems[0].Tag!;
        txtName.Text = _selectedEntry.Name;
        txtAccount.Text = _selectedEntry.Account;
        txtPassword.Text = _selectedEntry.Password;
        UpdateControls();
    }

    private void cbShowPasswords_CheckedChanged(object sender, EventArgs e)
    {
        LoadEntries();
    }

    private void btnCopyPassword_Click(object sender, EventArgs e)
    {
        if (lvEntries.SelectedItems.Count != 1)
        {
            return;
        }

        var entry = (PasswordEntry)lvEntries.SelectedItems[0].Tag!;
        Clipboard.SetText(entry.Password);
        MessageBox.Show("パスワードがクリップボードにコピーされました。", "コピー完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnDeleteEntry_Click(object sender, EventArgs e)
    {
        if (lvEntries.SelectedItems.Count != 1)
        {
            return;
        }

        var entry = (PasswordEntry)lvEntries.SelectedItems[0].Tag!;

        var result = MessageBox.Show($"'{entry.Name}' を削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
        {
            return;
        }

        _store.Entries.Remove(entry);
        _store.Save();
        LoadEntries();
        ClearEntryFields();
    }

    private void btnImportText_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*",
            Title = "インポートするテキストファイルを選択してください"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        _store.ImportFromTextFile(dialog.FileName);
        LoadEntries();
        MessageBox.Show("インポートが完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnExportText_Click(object sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*",
            Title = "エクスポート先のファイルを選択してください",
            FileName = "passwords.txt"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        _store.ExportToTextFile(dialog.FileName);
        MessageBox.Show("エクスポートが完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnClearSelection_Click(object sender, EventArgs e)
    {
        lvEntries.SelectedItems.Clear();
        _selectedEntry = null;
        ClearEntryFields();
        UpdateControls();
    }

    private void ClearEntryFields()
    {
        txtName.Text = string.Empty;
        txtAccount.Text = string.Empty;
        txtPassword.Text = string.Empty;
        btnSaveEntry.Text = "Add";
    }
}
