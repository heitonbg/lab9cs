namespace FileSync.Logging
{
    using System.IO;
    using Newtonsoft.Json;
    using FileSync.Models;
    using System;

    public class JsonLogManager : ILogManager
    {
        private readonly string _logFilePath;

        public JsonLogManager(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void SaveLog(SyncLog log)
        {
            log.SyncTime = DateTime.Now;
            string json = JsonConvert.SerializeObject(log, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_logFilePath, json);
        }

        public SyncLog LoadLog()
        {
            if (!File.Exists(_logFilePath))
            {
                return new SyncLog();
            }

            string json = File.ReadAllText(_logFilePath);
            return JsonConvert.DeserializeObject<SyncLog>(json);
        }
    }
}