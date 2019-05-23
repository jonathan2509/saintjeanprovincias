using System;
using System.Collections.Generic;
using BasicJS;
namespace ClassBussines
{
    public class Alumno : CID<Alumno>
    {
        IGenericSingleton<Alumno> IGSA = Singleton.GetInstance;
        public string Nombre { get; set; }
        public int DNI { get; set; }
        public Provincia Provincia { get; set; }
        public override void Add() { IGSA.Add(this); }
        public override void Erase() { IGSA.Erase(this); }
        public override string Find() { return IGSA.Find(this); }
        public override List<Alumno> List() { return IGSA.List(this); }
        public override string ListToJson() { return IGSA.ListToJson(this); }
        public override string LogIn() { return IGSA.LogIn(this); }
        public override void Modify() { IGSA.Modify(this); }
    }
}