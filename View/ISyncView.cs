using System;

public interface ISyncView
{
  event Action<string, string, bool> SyncRequested;
  event Action<string> SourceDirSelected;
  event Action<string> TargetDirSelected;
  
  void UpdateLog(string message);
  void SetSourceDir(string path);
  void SetTargetDir(string path);
}