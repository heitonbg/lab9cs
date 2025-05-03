namespace FileSync.Logging
{
    using FileSync.Models;

    public interface ILogManager
    {
        void SaveLog(SyncLog log);
        SyncLog LoadLog();
    }
}