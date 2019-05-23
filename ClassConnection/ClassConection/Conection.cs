using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ClassConection
{
    public class Conection : IConection
    {
        #region Properties
        SqlConnection MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        SqlCommand MyCommand = null;
        #endregion
        protected IConection IC = null;
        public Conection()
        {
            IC = this;
        }
        void IConection.Open()
        {
            if (MyConnection.State != ConnectionState.Open)
            {
                try
                {
                    MyConnection.Open();
                }
                catch (Exception)
                {
                    throw new Exception("Error : No se pudo conectar a la base de datos.");
                }
            }
        }
        void IConection.CreateCommand(string SProcedureName)
        {
            MyCommand = new SqlCommand(SProcedureName, MyConnection);
            MyCommand.CommandType = CommandType.StoredProcedure;
        }
        void IConection.Update(string ErrMessage)
        {
            MyConnection.Open();
            try
            {
                MyCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception(ErrMessage);
            }
            finally { MyConnection.Close(); }
        }

        int IConection.Insert(string ErrMessage)
        {
            MyConnection.Open();
            try
            {
                int ID = int.Parse(MyCommand.ExecuteScalar().ToString());
                return ID;
            }
            catch (Exception)
            {
                throw new Exception(ErrMessage);
            }
            finally { MyConnection.Close(); }
        }

        DataTable IConection.List(string ErrMessage)
        {
            MyConnection.Open();
            try
            {
                DataTable DT = new DataTable();
                DT.Load(MyCommand.ExecuteReader());
                return DT;
            }
            catch (Exception)
            {
                throw new Exception(ErrMessage);
            }
            finally { MyConnection.Close(); }
        }

        DataRow IConection.Find(string ErrMessage)
        {
            MyConnection.Open();
            try
            {
                DataTable DT = new DataTable();
                DT.Load(MyCommand.ExecuteReader());
                return DT.Rows[0];
            }
            catch (Exception)
            {
                throw new Exception(ErrMessage);
            }
            finally { MyConnection.Close(); }
        }

        void IConection.ParameterAddVarchar(string Name, int Length, int value)
        {
            MyCommand.Parameters.Add("@" + Name, SqlDbType.VarChar, Length).Value = value;
        }

        void IConection.ParameterAddFloat(string Name, double value)
        {
            MyCommand.Parameters.Add("@" + Name, SqlDbType.Float).Value = value;
        }

        void IConection.ParameterAddDateTime(string Name, DateTime value)
        {
            if (value == null) MyCommand.Parameters.Add("@" + Name, SqlDbType.SmallDateTime).Value = DBNull.Value;
            else MyCommand.Parameters.Add("@" + Name, SqlDbType.SmallDateTime).Value = value.ToString();
        }

        void IConection.ParameterAddBit(string Name, bool value)
        {
            MyCommand.Parameters.Add("@" + Name, SqlDbType.Bit).Value = Convert.ToByte(value);
        }

        void IConection.ParameterAddText(string Name, string value)
        {
            MyCommand.Parameters.Add("@" + Name, SqlDbType.Text).Value = value;
        }

        void IConection.ParameterAddVarchar(string Name, int Length, string value)
        {
            MyCommand.Parameters.Add("@" + Name, SqlDbType.VarChar, Length).Value = value;
        }

        void IConection.ParameterAddInt(string Name, int Value)
        {
            MyCommand.Parameters.Add("@" + Name, SqlDbType.Int).Value = Value;
        }
    }
}