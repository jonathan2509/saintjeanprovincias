using System;
using System.Collections.Generic;
using System.Web;
using BasicJS;
namespace ClassBussines
{
    public class PuntoDeInteres : CID<PuntoDeInteres>
    {
        IGenericSingleton<PuntoDeInteres> IGSPI = Singleton.GetInstance;
        IID IID;
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Provincia Provincia { get; set; }
        public PuntoDeInteres()
        {
            IID = this;
            IID.Directory = "PuntosDeInteres";
            IID.Prefix = "PuntoDeInteres";
        }
        public override void Add() { IGSPI.Add(this); }
        public override void Erase() { IGSPI.Erase(this); IID.DeleteFile(); }
        public override string Find() { return IGSPI.Find(this); }
        public override List<PuntoDeInteres> List() { return IGSPI.List(this); }
        public override string ListToJson() { return IGSPI.ListToJson(this); }
        public override string LogIn() { return IGSPI.LogIn(this); }
        public override void Modify() { IGSPI.Modify(this); }
        public void SaveImage(HttpPostedFile FU)
        {
            IID.FU = FU;
            IID.SaveFile();
        }
    }
}