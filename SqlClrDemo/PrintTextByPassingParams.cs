using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void PrintTextByPassingParams(string strInParam, out string strOutParam)
    {
        strOutParam = $"Hi '{strInParam}', this is a stored procedure with parameters using CLR database object.";
    }
}
