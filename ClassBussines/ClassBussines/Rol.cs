using System;
using System.Collections.Generic;
using BasicJS;
namespace ClassBussines
{
    public class Rol : CID<Rol>
    {
        public string Nombre { get; set; }
        IGenericSingleton<Rol> IGSR = Singleton.GetInstance;
        public override void Add() { IGSR.Add(this); }
        public override void Erase() { IGSR.Erase(this); }
        public override string Find() { return IGSR.Find(this); }
        public override List<Rol> List() { return IGSR.List(this); }
        public override string ListToJson() { return IGSR.ListToJson(this); }
        public override string LogIn() { return IGSR.LogIn(this); }
        public override void Modify() { IGSR.Modify(this); }
    }
}