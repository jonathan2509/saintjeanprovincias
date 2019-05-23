using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ClassConection
{
    public interface IConection
    {
        void Open();
        void CreateCommand(string SProcedureName);
        void Update(string ErrMessage);
        int Insert(string ErrMessage);
        DataTable List(string ErrMessage);
        DataRow Find(string ErrMessage);
        void ParameterAddVarchar(string Name, int Length, int value);
        void ParameterAddFloat(string Name, double value);
        void ParameterAddDateTime(string Name, DateTime value);
        void ParameterAddBit(string Name, bool value);
        void ParameterAddText(string Name, string value);
        void ParameterAddVarchar(string Name, int Length, string Value);
        void ParameterAddInt(string Name, int Value);
    }
}

