using System;
using System.Collections.Generic;
using System.Data;
using BasicJS;
namespace ClassBussines
{
    partial class Singleton : IGenericSingleton<Alumno>
    {
        void IGenericSingleton<Alumno>.Add(Alumno Data)
        {
            IC.CreateCommand("Alumnos_Insert");
            IC.ParameterAddVarchar("Nombre", 50, Data.Nombre);
            IC.ParameterAddInt("DNI", Data.DNI);
            IC.ParameterAddInt("IDProvincia", Data.Provincia.ID);
            Data.ID = IC.Insert("Error: No Se Pudo Agregar El Alumno.");
        }
        void IGenericSingleton<Alumno>.Erase(Alumno Data)
        {
            IC.CreateCommand("Alumnos_Delete");
            IC.ParameterAddInt("ID", Data.ID);
            IC.Update("Error: No Se Pudo Eliminar El Alumno.");
        }
        string IGenericSingleton<Alumno>.Find(Alumno Data)
        {
            IC.CreateCommand("Alumnos_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No Se Pudo Encontrar El Alumno.");
            IGSA.LoadClass(DR, Data);
            return Data.RowToJson(DR);
        }
        List<Alumno> IGenericSingleton<Alumno>.List(Alumno Data)
        {
            IC.CreateCommand("Alumnos_List");
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Alumnos.");
            List<Alumno> Alumnos = new List<Alumno>();
            foreach (DataRow DR in DT.Rows)
            {
                Alumno Alumno = new Alumno();
                IGSA.LoadClass(DR, Alumno);
                Alumnos.Add(Alumno);
            }
            return Alumnos;
        }
        string IGenericSingleton<Alumno>.ListToJson(Alumno Data)
        {
            IC.CreateCommand("Alumnos_List");
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Alumnos.");
            return Data.TableToJson(DT);
        }
        void IGenericSingleton<Alumno>.LoadClass(DataRow DR, Alumno Data)
        {
            Data.ID = int.Parse(DR["ID"].ToString());
            Data.Nombre = DR["Nombre"].ToString();
            Data.DNI = int.Parse(DR["DNI"].ToString());
            Data.Provincia = new Provincia();
            Data.Provincia.ID = int.Parse(DR["IDProvincia"].ToString());
            Data.Provincia.Find();
        }
        string IGenericSingleton<Alumno>.LogIn(Alumno Data) { throw new NotImplementedException(); }
        void IGenericSingleton<Alumno>.Modify(Alumno Data)
        {
            IC.CreateCommand("Alumnos_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 50, Data.Nombre);
            IC.ParameterAddInt("DNI", Data.DNI);
            IC.ParameterAddInt("IDProvincia", Data.Provincia.ID);
            IC.Update("Error: No Se Pudo Modificar El Alumno.");
        }
    }
}