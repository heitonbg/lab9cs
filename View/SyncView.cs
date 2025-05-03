using System;
using System.Windows.Forms;

namespace FileSync.View
{
  public partial class SyncView : Form, ISyncView
  {
    public event Action<string, string, bool> SyncRequested;
    public event Action<string> SourceDirSelected; 
    public event Action<string> TargetDirSelected;

    private TextBox sourceDirTextBox;
    private TextBox targetDirTextBox;
    private TextBox logTextBox;
    private RadioButton jsonRadioButton;
    private Button syncButton;
    private Button sourceBrowseButton;
    private Button targetBrowseButton;
    private FolderBrowserDialog folderBrowserDialog;

    public SyncView()
    {
      InitializeComponent();
    }

    public void UpdateLog(string message)
    {
      logTextBox.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
    }

    public void SetSourceDir(string path)
    {
      sourceDirTextBox.Text = path;
    }

    public void SetTargetDir(string path)
    {
      targetDirTextBox.Text = path;
    }

    private void OnSourceBrowseButtonClick(object sender, EventArgs eventArgs)
    {
      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        SourceDirSelected?.Invoke(folderBrowserDialog.SelectedPath);
      }
    }

    private void OnTargetBrowseButtonClick(object sender, EventArgs eventArgs)
    {
      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        TargetDirSelected?.Invoke(folderBrowserDialog.SelectedPath);
      }
    }

    private void OnSyncButtonClick(object sender, EventArgs eventArgs)
    {
      bool useJson = jsonRadioButton.Checked;
      SyncRequested?.Invoke(
        sourceDirTextBox.Text, 
        targetDirTextBox.Text, 
        useJson
      );
    }
  }
}