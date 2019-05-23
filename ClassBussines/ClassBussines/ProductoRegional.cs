using System;
using System.Collections.Generic;
using System.Web;
using BasicJS;
namespace ClassBussines
{
    public class ProductoRegional : CID<ProductoRegional>
    {
        IGenericSingleton<ProductoRegional> IGSPR = Singleton.GetInstance;
        IID IID;
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Provincia Provincia { get; set; }
        public ProductoRegional()
        {
            IID = this;
            IID.Directory = "ProductosRegionales";
            IID.Prefix = "ProductoRegional";
        }
        public override void Add() { IGSPR.Add(this); }
        public override void Erase() { IGSPR.Erase(this); IID.DeleteFile(); }
        public override string Find() { return IGSPR.Find(this); }
        public override List<ProductoRegional> List() { return IGSPR.List(this); }
        public override string ListToJson() { return IGSPR.ListToJson(this); }
        public override string LogIn() { return IGSPR.LogIn(this); }
        public override void Modify() { IGSPR.Modify(this); }
        public void SaveImage(HttpPostedFile FU)
        {
            IID.FU = FU;
            IID.SaveFile();
        }
    }
}