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
        public string Name;
        public long Size;
        public DateTime CreationTime;
        public FileData(string fileName, long fileSize, DateTime creationTime)
        {
            Name = fileName;
            Size = fileSize;
            CreationTime = creationTime;
        }
    }
    [SqlFunction(FillRowMethodName = "FillRows", TableDefinition = "Name nvarchar(500), Size bigint, CreationTime datetime")]
    public static IEnumerable GetFiles(string targetDirectory, string searchPattern)
    {
        try
        {
            ArrayList FilePropertiesCollection = new ArrayList();
            DirectoryInfo dirInfo = new DirectoryInfo(targetDirectory);
            FileInfo[] files = dirInfo.GetFiles(searchPattern);
            foreach (FileInfo fileInfo in files)
            {
                FilePropertiesCollection.Add(new FileData(fileInfo.Name, fileInfo.Length, fileInfo.CreationTime));
            }
            return FilePropertiesCollection;
        }
        catch (Exception)
        {
            return null;
        }
    }    private static void FillRows(object objFileProperties, out string fileName, out long fileSize, out DateTime creationTime)
    {
        FileData fileProperties = (FileData)objFileProperties;
        fileName = fileProperties.Name;
        fileSize = fileProperties.Size;
        creationTime = fileProperties.CreationTime;
    }
}
