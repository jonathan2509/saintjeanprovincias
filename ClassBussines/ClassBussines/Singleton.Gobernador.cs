using System;
using System.Collections.Generic;
using System.Data;
using BasicJS;
namespace ClassBussines
{
    partial class Singleton : IGenericSingleton<Gobernador>
    {
        void IGenericSingleton<Gobernador>.Add(Gobernador Data) { throw new NotImplementedException(); }
        void IGenericSingleton<Gobernador>.Erase(Gobernador Data) { throw new NotImplementedException(); }
        string IGenericSingleton<Gobernador>.Find(Gobernador Data)
        {
            IC.CreateCommand("Gobernadores_Find");
            IC.ParameterAddInt("IDProvincia", Data.ID);
            DataRow DR = IC.Find("Error: No Se Pudo Encontrar El Gobernador.");
            IGSG.LoadClass(DR, Data);
            return Data.RowToJson(DR);
        }
        List<Gobernador> IGenericSingleton<Gobernador>.List(Gobernador Data)
        {
            IC.CreateCommand("Gobernadores_List");
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Gobernadores.");
            List<Gobernador> Gobernadores = new List<Gobernador>();
            foreach (DataRow DR in DT.Rows)
            {
                Gobernador Gobernador = new Gobernador();
                IGSG.LoadClass(DR, Gobernador);
                Gobernadores.Add(Gobernador);
            }
            return Gobernadores;
        }
        string IGenericSingleton<Gobernador>.ListToJson(Gobernador Data)
        {
            IC.CreateCommand("Gobernadores_List");
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Gobernadores.");
            return Data.TableToJson(DT);
        }
        void IGenericSingleton<Gobernador>.LoadClass(DataRow DR, Gobernador Data)
        {
            Data.ID = int.Parse(DR["ID"].ToString());
            Data.Nombre = DR["Nombre"].ToString();
            Data.Apellido = DR["Apellido"].ToString();
            Data.PeriodoGobierno = DR["PeriodoGobierno"].ToString();
            Data.Historial = DR["Historial"].ToString();
            Data.Provincia = new Provincia();
            Data.Provincia.ID = int.Parse(DR["IDProvincia"].ToString());
            Data.Provincia.Find();
        }
        string IGenericSingleton<Gobernador>.LogIn(Gobernador Data) { throw new NotImplementedException(); }
        void IGenericSingleton<Gobernador>.Modify(Gobernador Data)
        {
            IC.CreateCommand("Gobernadores_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 40, Data.Nombre);
            IC.ParameterAddVarchar("Apellido", 40, Data.Apellido);
            IC.ParameterAddVarchar("PeriodoGobierno", 30, Data.PeriodoGobierno);
            IC.ParameterAddVarchar("Historial", -1, Data.Historial);
            IC.Update("Error: No Se Pudo Modificar El Gobernador.");
        }
    }
}