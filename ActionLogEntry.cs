namespace FileSync.Model
{
    public class ActionLogEntry
    {
        public SyncAction Action { get; set; }
        public string RelativePath { get; set; }
    }
}