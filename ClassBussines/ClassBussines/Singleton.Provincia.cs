using System;
using System.Collections.Generic;
using System.Data;
using BasicJS;
namespace ClassBussines
{
    partial class Singleton : IGenericSingleton<Provincia>
    {
        void IGenericSingleton<Provincia>.Add(Provincia Data) { throw new NotImplementedException(); }
        void IGenericSingleton<Provincia>.Erase(Provincia Data) { throw new NotImplementedException(); }
        string IGenericSingleton<Provincia>.Find(Provincia Data)
        {
            IC.CreateCommand("Provincias_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No Se Pudo Encontrar La Provincia.");
            IGSP.LoadClass(DR, Data);
            return Data.RowToJson(DR);
        }
        List<Provincia> IGenericSingleton<Provincia>.List(Provincia Data)
        {
            IC.CreateCommand("Provincias_List");
            DataTable DT = IC.List("Error: No Se Pudo Listar Las Provincias.");
            List<Provincia> Provincias = new List<Provincia>();
            foreach (DataRow DR in DT.Rows)
            {
                Provincia Provincia = new Provincia();
                IGSP.LoadClass(DR, Provincia);
                Provincias.Add(Provincia);
            }
            return Provincias;
        }
        string IGenericSingleton<Provincia>.ListToJson(Provincia Data)
        {
            IC.CreateCommand("Provincias_List");
            DataTable DT = IC.List("Error: No Se Pudo Listar Las Provincias.");
            return Data.TableToJson(DT);
        }
        void IGenericSingleton<Provincia>.LoadClass(DataRow DR, Provincia Data)
        {
            Data.ID = int.Parse(DR["ID"].ToString());
            Data.Nombre = DR["Nombre"].ToString();
            Data.Capital = DR["Capital"].ToString();
            int x;
            int.TryParse(DR["CantSenadores"].ToString(), out x);
            Data.CantSenadores = x;
            int.TryParse(DR["CantDiputados"].ToString(), out x);
            Data.CantDiputados = x;
            int.TryParse(DR["Superficie"].ToString(), out x);
            Data.Superficie = x;
            int.TryParse(DR["Poblacion"].ToString(), out x);
            Data.Poblacion = x;
            int.TryParse(DR["Nota"].ToString(), out x);
            Data.Nota = x;
            Data.Gobernador = new Gobernador();
            Data.Gobernador.ID = int.Parse(DR["IDGobernador"].ToString());
        }
        string IGenericSingleton<Provincia>.LogIn(Provincia Data) { throw new NotImplementedException(); }
        void IGenericSingleton<Provincia>.Modify(Provincia Data)
        {
            IC.CreateCommand("Provincias_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 40, Data.Nombre);
            IC.ParameterAddInt("CantSenadores", Data.CantSenadores);
            IC.ParameterAddInt("CantDiputados", Data.CantDiputados);
            IC.ParameterAddVarchar("Capital", 40, Data.Capital);
            IC.ParameterAddInt("Superficie", Data.Superficie);
            IC.ParameterAddInt("Poblacion", Data.Poblacion);
            IC.ParameterAddInt("Nota", Data.Nota);
            IC.Update("Error: No Se Pudo Modificar La Provincia.");
        }
    }
}