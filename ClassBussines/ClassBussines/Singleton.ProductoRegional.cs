using System;
using System.Collections.Generic;
using System.Data;
using BasicJS;
namespace ClassBussines
{
    partial class Singleton : IGenericSingleton<ProductoRegional>
    {
        void IGenericSingleton<ProductoRegional>.Add(ProductoRegional Data)
        {
            IC.CreateCommand("ProductosRegionales_Insert");
            IC.ParameterAddVarchar("Nombre", 60, Data.Nombre);
            IC.ParameterAddText("Descripcion", Data.Descripcion);
            IC.ParameterAddInt("IDProvincia", Data.Provincia.ID);
            Data.ID = IC.Insert("Error: No Se Pudo Agregar El Producto Regional.");
        }
        void IGenericSingleton<ProductoRegional>.Erase(ProductoRegional Data)
        {
            IC.CreateCommand("ProductosRegionales_Delete");
            IC.ParameterAddInt("ID", Data.ID);
            IC.Update("Error: No Se Pudo Eliminar El Producto Regional.");
        }
        string IGenericSingleton<ProductoRegional>.Find(ProductoRegional Data)
        {
            IC.CreateCommand("ProductosRegionales_Find");
            IC.ParameterAddInt("ID", Data.ID);
            DataRow DR = IC.Find("Error: No Se Pudo Encontrar El Producto Regional.");
            IGSPR.LoadClass(DR, Data);
            return Data.RowToJson(DR);
        }
        List<ProductoRegional> IGenericSingleton<ProductoRegional>.List(ProductoRegional Data)
        {
            IC.CreateCommand("ProductosRegionales_List");
            IC.ParameterAddInt("IDProvincia", Data.Provincia.ID);
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Productos Regionales.");
            List<ProductoRegional> ProductosRegionales = new List<ProductoRegional>();
            foreach (DataRow DR in DT.Rows)
            {
                ProductoRegional ProductoRegional = new ProductoRegional();
                IGSPR.LoadClass(DR, ProductoRegional);
                ProductosRegionales.Add(ProductoRegional);
            }
            return ProductosRegionales;
        }
        string IGenericSingleton<ProductoRegional>.ListToJson(ProductoRegional Data)
        {
            IC.CreateCommand("ProductosRegionales_List");
            IC.ParameterAddInt("IDProvincia", Data.Provincia.ID);
            DataTable DT = IC.List("Error: No Se Pudo Listar Los Productos Regionales.");
            return Data.TableToJson(DT);
        }
        void IGenericSingleton<ProductoRegional>.LoadClass(DataRow DR, ProductoRegional Data)
        {
            Data.ID = int.Parse(DR["ID"].ToString());
            Data.Nombre = DR["Nombre"].ToString();
            Data.Descripcion = DR["Descripcion"].ToString();
            Data.Provincia = new Provincia();
            Data.Provincia.ID = int.Parse(DR["IDProvincia"].ToString());
            Data.Provincia.Find();
        }
        string IGenericSingleton<ProductoRegional>.LogIn(ProductoRegional Data) { throw new NotImplementedException(); }
        void IGenericSingleton<ProductoRegional>.Modify(ProductoRegional Data)
        {
            IC.CreateCommand("ProductosRegionales_Update");
            IC.ParameterAddInt("ID", Data.ID);
            IC.ParameterAddVarchar("Nombre", 60, Data.Nombre);
            IC.ParameterAddText("Descripcion", Data.Descripcion);
            IC.ParameterAddInt("IDProvincia", Data.Provincia.ID);
            IC.Update("Error: No Se Pudo Modificar El Producto Regional.");
        }
    }
}