using System.Collections.Generic;

public interface IDataHandler
{
    bool SaveData<T> (string Path, T Data);

    T LoadData<T> (string Path);

    List<string> FindJsonFilesInDirectory(string directoryPath);

    void DeleteAllFilesInDirectory(string directoryPath);

    void CreateDirectory(string directoryPath);
    void CopyFileIntoAppData(string targetPath, string fileToCopyPath);
}
