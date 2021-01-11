using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString ScalaUDF()
    {
        // Put your code here
        return "This is a CLR Scalar-Valued function";
    }
    private class FileData
    {
        public string Name, Extension, FullName, DirectoryName;
        public long Size;
        public bool IsReadOnly;
        public DateTime CreationTime, LastAccessTime, LastWriteTime;
        public FileData(string fileName, string fileExtension, string fileFullName, string fileDirectoryName, long fileSize, bool fileIsReadOnly, DateTime fileCreationTime, DateTime fileLastAccessTime, DateTime fileLastWriteTime)
        {
            Name = fileName;
            Extension = fileExtension;
            FullName = fileFullName;
            DirectoryName = fileDirectoryName;
            Size = fileSize;
            IsReadOnly = fileIsReadOnly;
            CreationTime = fileCreationTime;
            LastAccessTime = fileLastAccessTime;
            LastWriteTime = fileLastWriteTime;
        }
    }
    [SqlFunction(FillRowMethodName = "FillRows", TableDefinition = "Name nvarchar(512), Extension nvarchar(512), FullName nvarchar(512), DirectoryName nvarchar(512), Size bigint, IsReadOnly bit, CreationTime datetime, LastAccessTime datetime, LastWriteTime datetime")]
    public static IEnumerable GetFiles(string targetDirectory, string searchPattern)
    {
        try
        {
            ArrayList FilePropertiesCollection = new ArrayList();
            DirectoryInfo dirInfo = new DirectoryInfo(targetDirectory);
            FileInfo[] files = dirInfo.GetFiles(searchPattern);
            foreach (FileInfo fileInfo in files)
            {
                FilePropertiesCollection.Add(new FileData(
                    fileInfo.Name, 
                    fileInfo.Extension,
                    fileInfo.FullName,
                    fileInfo.DirectoryName,
                    fileInfo.Length,
                    fileInfo.IsReadOnly,
                    fileInfo.CreationTime,
                    fileInfo.LastAccessTime,
                    fileInfo.LastWriteTime
                    ));
            }
            return FilePropertiesCollection;
        }
        catch (Exception)
        {
            return null;
        }
    }    private static void FillRows(object objFileProperties, out string fileName, out string fileExtension, out string fileFullName, out string fileDirectoryName, out long fileSize, out bool fileIsReadOnly, out DateTime fileCreationTime, out DateTime fileLastAccessTime, out DateTime fileLastWriteTime)
    {
        FileData fileProperties = (FileData)objFileProperties;
        fileName = fileProperties.Name;
        fileExtension = fileProperties.Extension;
        fileFullName = fileProperties.FullName;
        fileDirectoryName = fileProperties.DirectoryName;
        fileSize = fileProperties.Size;
        fileIsReadOnly = fileProperties.IsReadOnly;
        fileCreationTime = fileProperties.CreationTime;
        fileLastAccessTime = fileProperties.LastAccessTime;
        fileLastWriteTime = fileProperties.LastWriteTime;
    }
}
