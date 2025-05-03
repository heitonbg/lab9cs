using System;
using System.IO;
using FileSync.Services;
using FileSync.Logging;

namespace FileSync.Presenter
{
  public class SyncPresenter
  {
    private readonly ISyncView _view;
    private readonly Func<bool, ILogManager> _logManagerFactory;
    private string _lastSourceDir;
    private string _lastTargetDir;

    public SyncPresenter(ISyncView view, Func<bool, ILogManager> logManagerFactory)
    {
      _view = view;
      _logManagerFactory = logManagerFactory;

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
          _view.UpdateLog("Ошибка: Одной из директорий не существует");
          return;
        }

        var logManager = _logManagerFactory(useJson);
        var fileComparer = new FileComparer();
        var syncManager = new SyncManager(fileComparer, logManager);
        var actions = syncManager.SyncDirectories(sourceDir, targetDir);

        if (actions.Count == 0)
        {
          _view.UpdateLog("Файлы уже синхронизированы");
          return;
        }

        foreach (var action in actions)
        {
          _view.UpdateLog($"{action.Action}: {action.RelativePath}");
        }

        _view.UpdateLog("Файлы синхронизированы успешно");
      }
      catch (Exception ex)
      {
        _view.UpdateLog($"Ошибка: {ex.Message}");
      }
    }
  }
}