using System.Collections.Generic;
using System;

namespace FileSync.Models
{
    public class SyncLog
    {
        public DateTime SyncTime { get; set; }
        public List<FileInfo> Files { get; set; } = new List<FileInfo>();
        public List<ActionLogEntry> Actions { get; set; } = new List<ActionLogEntry>();
    }
}