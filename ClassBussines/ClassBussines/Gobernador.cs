using System;
using System.Collections.Generic;
using System.Web;
using BasicJS;
namespace ClassBussines
{
    public class Gobernador : CID<Gobernador>
    {
        IGenericSingleton<Gobernador> IGSG = Singleton.GetInstance;
        IID IID;
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string PeriodoGobierno { get; set; }
        public string Historial { get; set; }
        public Provincia Provincia { get; set; }
        public Gobernador()
        {
            IID = this;
            IID.Directory = "Gobernadores";
            IID.Prefix = "Gobernador";
        }
        public override void Add() { IGSG.Add(this); }
        public override void Erase() { IGSG.Erase(this); }
        public override string Find() { return IGSG.Find(this); }
        public override List<Gobernador> List() { return IGSG.List(this); }
        public override string ListToJson() { return IGSG.ListToJson(this); }
        public override string LogIn() { return IGSG.LogIn(this); }
        public override void Modify() { IGSG.Modify(this); }
        public void SaveImage(HttpPostedFile FU)
        {
            IID.FU = FU;
            IID.SaveFile();
        }
    }
}