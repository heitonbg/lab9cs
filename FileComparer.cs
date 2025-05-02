public class FileComparer
{
  public List<string> CompareDirectories(string sourceDirectory, string targetDirectory, SyncLog lastLog)
  {
    var actions = new List<string>();
    var sourceFiles = GetFilesWithHashes(sourceDirectory);
    var targetFiles = GetFilesWithHashes(targetDirectory);

    foreach (var sourceFile in sourceFiles)
    {
      var relativePath = GetRelativePath(sourceFile.Path, sourceDirectory);
      var targetPath = Path.Combine(targetDirectory, relativePath);

      var lastFileLog = lastLog.Files.FirstOrDefault(file => 
        GetRelativePath(file.Path, sourceDirectory) == relativePath);

      if (!File.Exists(targetPath))
      {
        actions.Add($"CREATE: {relativePath}");
      }
      else
      {
        var targetFile = targetFiles.First(file => file.Path == targetPath);
        if (sourceFile.LastModified > targetFile.LastModified || 
          sourceFile.Hash != targetFile.Hash)
        {
          actions.Add($"UPDATE: {relativePath}");
        }
      }
    }

    foreach (var targetFile in targetFiles)
    {
      var relativePath = GetRelativePath(targetFile.Path, targetDirectory);
      var sourcePath = Path.Combine(sourceDirectory, relativePath);

      if (!File.Exists(sourcePath))
      {
        actions.Add($"DELETE: {relativePath}");
      }
    }

    return actions;
  }

  private List<FileInfo> GetFilesWithHashes(string directory)
  {
    var files = new List<FileInfo>();
    foreach (var filePath in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
    {
      files.Add(new FileInfo
      {
        Path = filePath,
        Name = Path.GetFileName(filePath),
        LastModified = File.GetLastWriteTime(filePath),
        Size = new FileInfo(filePath).Length,
        Hash = ComputeFileHash(filePath)
      });
    }
    return files;
  }

  private string ComputeFileHash(string filePath)
  {
    using (var md5 = MD5.Create())
    using (var stream = File.OpenRead(filePath))
    {
      return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
    }
  }

  private string GetRelativePath(string fullPath, string basePath)
  {
    return Path.GetRelativePath(basePath, fullPath);
  }
}