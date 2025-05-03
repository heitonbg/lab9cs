namespace FileSync.Logging
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using FileSync.Models;

    public class XmlLogManager : ILogManager
    {
        private readonly string _logFilePath;

        public XmlLogManager(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void SaveLog(SyncLog log)
        {
            log.SyncTime = DateTime.Now;
            var serializer = new XmlSerializer(typeof(SyncLog));
            using (var writer = new StreamWriter(_logFilePath))
            {
                serializer.Serialize(writer, log);
            }
        }

        public SyncLog LoadLog()
        {
            if (!File.Exists(_logFilePath))
            {
                return new SyncLog();
            }

            var serializer = new XmlSerializer(typeof(SyncLog));
            using (var reader = new StreamReader(_logFilePath))
            {
                return (SyncLog)serializer.Deserialize(reader);
            }
        }
    }
}