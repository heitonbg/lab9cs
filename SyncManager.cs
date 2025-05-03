namespace FileSync.Services
{
    using System.Collections.Generic;
    using FileSync.Models;
    using FileSync.Logging;
    using FileSync.Model;

    public class SyncManager
    {
        private readonly FileComparer _fileComparer;
        private readonly ILogManager _logManager;

        public SyncManager(FileComparer fileComparer, ILogManager logManager)
        {
            _fileComparer = fileComparer;
            _logManager = logManager;
        }

        public List<ActionLogEntry> SyncDirectories(string sourceDirectory, string targetDirectory)
        {
            var lastLog = _logManager.LoadLog();
            var actions = _fileComparer.CompareDirectories(sourceDirectory, targetDirectory, lastLog);

            if (actions.Count > 0)
            {
                var newLog = new SyncLog
                {
                    Files = _fileComparer.GetFilesWithHashes(sourceDirectory),
                    Actions = actions
                };
                _logManager.SaveLog(newLog);
            }

            return actions;
        }
    }
}