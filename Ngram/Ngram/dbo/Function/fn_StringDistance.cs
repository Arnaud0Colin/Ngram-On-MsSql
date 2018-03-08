using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlDouble fn_StringDistance(SqlString ChaineA, SqlString ChaineB, SqlInt16 Model)
    {
        return new SqlDouble(Ngram.CalculDistance.IndiceSimilarite(ChaineA.Value, ChaineB.Value, 1, true, 1)[Model.Value]);
    }
}
