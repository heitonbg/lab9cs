using System;

public interface ILogManager
{
  void SaveLog(SyncLog log);
  SyncLog LoadLog();
}