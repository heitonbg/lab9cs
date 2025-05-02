using System;
using System.IO;

public class SyncPresenter
{
  private readonly ISyncView _view;
  private readonly FileComparer _fileComparer;
  private string _lastSourceDir;
  private string _lastTargetDir;

  public SyncPresenter(ISyncView view)
  {
    _view = view;
    _fileComparer = new FileComparer();

    _view.SourceDirSelected += OnSourceDirSelected;
    _view.TargetDirSelected += OnTargetDirSelected;
    _view.SyncRequested += OnSyncRequested;
  }

  private void OnSourceDirSelected(string path)
  {
    _lastSourceDir = path;
    _view.SetSourceDir(path);
  }

  private void OnTargetDirSelected(string path)
  {
    _lastTargetDir = path;
    _view.SetTargetDir(path);
  }

  private void OnSyncRequested(string sourceDir, string targetDir, bool useJson)
  {
    try
    {
      if (!Directory.Exists(sourceDir) || !Directory.Exists(targetDir))
      {
        _view.UpdateLog("Error: One of directories does not exist");
        return;
      }

      ILogManager logManager = useJson 
        ? new JsonLogManager("sync_log.json") 
        : new XmlLogManager("sync_log.xml");

      var lastLog = logManager.LoadLog();
      var actions = _fileComparer.CompareDirectories(sourceDir, targetDir, lastLog);

      if (actions.Count == 0)
      {
        _view.UpdateLog("Files are already synchronized");
        return;
      }

      foreach (var action in actions)
      {
        _view.UpdateLog(action);
      }

      var newLog = new SyncLog
      {
        SyncTime = DateTime.Now,
        Files = _fileComparer.GetFilesWithHashes(sourceDir),
        Actions = actions
      };
      
      logManager.SaveLog(newLog);
      _view.UpdateLog("Synchronization completed successfully");
    }
    catch (Exception ex)
    {
      _view.UpdateLog($"Error: {ex.Message}");
    }
  }
}