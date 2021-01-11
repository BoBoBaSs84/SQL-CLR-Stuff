using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void PrintText ()
    {
        SqlPipe sqlP = SqlContext.Pipe;
        sqlP.Send("This is a first stored procedure using CLR database object");
    }
}
