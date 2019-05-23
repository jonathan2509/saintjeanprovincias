using System;
using System.Collections.Generic;
using System.Data;
using BasicJS;
namespace ClassBussines
{
    partial class Singleton : IGenericSingleton<Rol>
    {
        void IGenericSingleton<Rol>.Add(Rol Data) { throw new NotImplementedException(); }
        void IGenericSingleton<Rol>.Erase(Rol Data) { throw new NotImplementedException(); }
        string IGenericSingleton<Rol>.Find(Rol Data)
        {
            IC.CreateCommand("Roles_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No Se Pudo Encontrar El Rol.");
            IGSR.LoadClass(DR, Data);
            return Data.RowToJson(DR);
        }
        List<Rol> IGenericSingleton<Rol>.List(Rol Data)
        {
            IC.CreateCommand("Roles_List");
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Roles.");
            List<Rol> Roles = new List<Rol>();
            foreach (DataRow DR in DT.Rows)
            {
                Rol Rol = new Rol();
                IGSR.LoadClass(DR, Rol);
                Roles.Add(Rol);
            }
            return Roles;
        }
        string IGenericSingleton<Rol>.ListToJson(Rol Data)
        {
            IC.CreateCommand("Roles_List");
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Roles.");
            return Data.TableToJson(DT);
        }
        string IGenericSingleton<Rol>.LogIn(Rol Data) { throw new NotImplementedException(); }
        void IGenericSingleton<Rol>.Modify(Rol Data) { throw new NotImplementedException(); }
        void IGenericSingleton<Rol>.LoadClass(DataRow DR, Rol Data)
        {
            Data.ID = int.Parse(DR["ID"].ToString());
            Data.Nombre = DR["Nombre"].ToString();
        }
    }
}