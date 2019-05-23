using System;
using System.Data;

namespace BasicJS
{
    public interface IJson
    {
        string TableToJson(DataTable DT);
        string RowToJson(DataRow DR);
    }
}
