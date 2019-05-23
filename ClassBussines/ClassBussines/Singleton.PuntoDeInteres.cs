using System;
using System.Collections.Generic;
using System.Data;
using BasicJS;
namespace ClassBussines
{
    partial class Singleton : IGenericSingleton<PuntoDeInteres>
    {
        void IGenericSingleton<PuntoDeInteres>.Add(PuntoDeInteres Data)
        {
            IC.CreateCommand("PuntosDeInteres_Insert");
            IC.ParameterAddVarchar("Nombre", 60, Data.Nombre);
            IC.ParameterAddText("Descripcion", Data.Descripcion);
            IC.ParameterAddInt("IDProvincia", Data.Provincia.ID);
            Data.ID = IC.Insert("Error: No Se Pudo Agregar El Punto De Interes.");
        }
        void IGenericSingleton<PuntoDeInteres>.Erase(PuntoDeInteres Data)
        {
            IC.CreateCommand("PuntosDeInteres_Delete");
            IC.ParameterAddInt("ID", Data.ID);
            IC.Update("Error: No Se Pudo Eliminar El Punto De Interes.");
        }
        string IGenericSingleton<PuntoDeInteres>.Find(PuntoDeInteres Data)
        {
            IC.CreateCommand("PuntosDeInteres_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No Se Pudo Encontrar El Punto De Interes.");
            IGSPI.LoadClass(DR, Data);
            return Data.RowToJson(DR);
        }
        List<PuntoDeInteres> IGenericSingleton<PuntoDeInteres>.List(PuntoDeInteres Data)
        {
            IC.CreateCommand("PuntosDeInteres_List");
            IC.ParameterAddInt("IDProvincia", Data.Provincia.ID);
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Puntos De Interes.");
            List<PuntoDeInteres> PuntosDeInteres = new List<PuntoDeInteres>();
            foreach (DataRow DR in DT.Rows)
            {
                PuntoDeInteres PuntoDeInteres = new PuntoDeInteres();
                IGSPI.LoadClass(DR, PuntoDeInteres);
                PuntosDeInteres.Add(PuntoDeInteres);
            }
            return PuntosDeInteres;
        }
        string IGenericSingleton<PuntoDeInteres>.ListToJson(PuntoDeInteres Data)
        {
            IC.CreateCommand("PuntosDeInteres_List");
            IC.ParameterAddInt("IDProvincia", Data.Provincia.ID);
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Puntos De Interes.");
            return Data.TableToJson(DT);
        }
        void IGenericSingleton<PuntoDeInteres>.LoadClass(DataRow DR, PuntoDeInteres Data)
        {
            Data.ID = int.Parse(DR["ID"].ToString());
            Data.Nombre = DR["Nombre"].ToString();
            Data.Descripcion = DR["Descripcion"].ToString();
            Data.Provincia = new Provincia();
            Data.Provincia.ID = int.Parse(DR["IDProvincia"].ToString());
            Data.Provincia.Find();
        }
        string IGenericSingleton<PuntoDeInteres>.LogIn(PuntoDeInteres Data) { throw new NotImplementedException(); }
        void IGenericSingleton<PuntoDeInteres>.Modify(PuntoDeInteres Data)
        {
            IC.CreateCommand("PuntosDeInteres_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 60, Data.Nombre);
            IC.ParameterAddText("Descripcion", Data.Descripcion);
            IC.ParameterAddInt("IDProvincia", Data.Provincia.ID);
            IC.Update("Error: No Se Pudo Modificar El Punto De Interes.");
        }
    }
}