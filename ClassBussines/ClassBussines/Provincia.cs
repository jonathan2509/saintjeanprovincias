using System;
using System.Collections.Generic;
using System.Web;
using BasicJS;
namespace ClassBussines
{
    public class Provincia : CID<Provincia>
    {
        IGenericSingleton<Provincia> IGSP = Singleton.GetInstance;
        IID IID;
        public string Nombre { get; set; }
        public int CantSenadores { get; set; }
        public int CantDiputados { get; set; }
        public string Capital { get; set; }
        public int Superficie { get; set; }
        public int Poblacion { get; set; }
        public int Nota { get; set; }
        public Gobernador Gobernador { get; set; }
        public Provincia()
        {
            IID = this;
            IID.Directory = "Provincias";
            IID.Prefix = "Provincia";
        }
        public override void Add() { IGSP.Add(this); }
        public override void Erase() { IGSP.Erase(this); }
        public override string Find() { return IGSP.Find(this); }
        public override List<Provincia> List() { return IGSP.List(this); }
        public override string ListToJson() { return IGSP.ListToJson(this); }
        public override string LogIn() { return IGSP.LogIn(this); }
        public override void Modify() { IGSP.Modify(this); }
        public void SaveImage(HttpPostedFile FU)
        {
            IID.FU = FU;
            IID.SaveFile();
        }
    }
}