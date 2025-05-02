public class XmlLogManager : ILogManager
{
  private readonly string _logFilePath;

  public XmlLogManager(string logFilePath)
  {
    _logFilePath = logFilePath;
  }

  public void SaveLog(SyncLog log)
  {
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