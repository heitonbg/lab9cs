public class SyncLog
{
  public DateTime SyncTime { get; set; }
  public List<FileInfo> Files { get; set; } = new List<FileInfo>();
  public List<string> Actions { get; set; } = new List<string>();
}