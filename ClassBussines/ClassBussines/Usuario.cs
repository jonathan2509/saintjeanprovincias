using System;
using System.Collections.Generic;
using System.Web;
using BasicJS;
namespace ClassBussines
{
    public class Usuario : CID<Usuario>
    {
        IGenericSingleton<Usuario> IGSU = Singleton.GetInstance;
        IID IID;
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public Rol Rol { get; set; }
        public Usuario()
        {
            IID = this;
            IID.Directory = "Usuarios";
            IID.Prefix = "Usuario";
        }
        public override void Add() { IGSU.Add(this); }
        public override void Erase() { IGSU.Erase(this); }
        public override string Find() { return IGSU.Find(this); }
        public override List<Usuario> List() { return IGSU.List(this); }
        public override string ListToJson() { return IGSU.ListToJson(this); }
        public override string LogIn() { return IGSU.LogIn(this); }
        public override void Modify() { IGSU.Modify(this); }
        public void SaveImage(HttpPostedFile FU)
        {
            IID.FU = FU;
            IID.SaveFile();
        }
    }
}