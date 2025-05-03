using FileSync.Model;

namespace FileSync.Logging
{
  public interface ILogManager
  {
    void SaveLog(SyncLog log);
    SyncLog LoadLog();
  }
}