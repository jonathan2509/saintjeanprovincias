using System;
using System.Collections.Generic;
using System.Data;
using BasicJS;
namespace ClassBussines
{
    partial class Singleton : IGenericSingleton<Usuario>
    {
        void IGenericSingleton<Usuario>.Add(Usuario Data) { throw new NotImplementedException(); }
        void IGenericSingleton<Usuario>.Erase(Usuario Data) { throw new NotImplementedException(); }
        string IGenericSingleton<Usuario>.Find(Usuario Data)
        {
            IC.CreateCommand("Usuarios_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No Se Pudo Encontrar El Usuario.");
            IGSU.LoadClass(DR, Data);
            return Data.RowToJson(DR);
        }
        List<Usuario> IGenericSingleton<Usuario>.List(Usuario Data)
        {
            IC.CreateCommand("Usuarios_List");
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Usuarios.");
            List<Usuario> Usuarios = new List<Usuario>();
            foreach (DataRow DR in DT.Rows)
            {
                Usuario Usuario = new Usuario();
                IGSU.LoadClass(DR, Usuario);
                Usuarios.Add(Usuario);
            }
            return Usuarios;
        }
        string IGenericSingleton<Usuario>.ListToJson(Usuario Data)
        {
            IC.CreateCommand("Usuarios_List");
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Usuarios.");
            return Data.TableToJson(DT);
        }
        void IGenericSingleton<Usuario>.LoadClass(DataRow DR, Usuario Data)
        {
            Data.ID = int.Parse(DR["ID"].ToString());
            Data.Nombre = DR["Nombre"].ToString();
            Data.Clave = DR["Clave"].ToString();
            Data.Rol = new Rol();
            Data.Rol.ID = int.Parse(DR["IDRol"].ToString());
            Data.Rol.Find();
        }
        string IGenericSingleton<Usuario>.LogIn(Usuario Data)
        {
            IC.CreateCommand("Usuarios_Login");
            IC.ParameterAddVarchar("Nombre", 40, Data.Nombre);
            IC.ParameterAddVarchar("Clave", 40, Data.Clave);
            DataRow DR = IC.Find("Error: Usuario o Contraseña Incorrectos.");
            IGSU.LoadClass(DR, Data);
            return Data.RowToJson(DR);
        }
        void IGenericSingleton<Usuario>.Modify(Usuario Data)
        {
            IC.CreateCommand("Usuarios_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Clave", 40, Data.Clave);
            IC.Update("Error: No Se Pudo Modificar La Clave Del Usuario.");
        }
    }
}