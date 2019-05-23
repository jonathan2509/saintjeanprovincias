using System;
using ClassConection;
using BasicJS;
namespace ClassBussines
{
    partial class Singleton : Conection
    {
        private static Singleton _Instance = new Singleton();
        IGenericSingleton<Usuario> IGSU;
        IGenericSingleton<Rol> IGSR;
        IGenericSingleton<Provincia> IGSP;
        IGenericSingleton<Gobernador> IGSG;
        IGenericSingleton<Alumno> IGSA;
        IGenericSingleton<ProductoRegional> IGSPR;
        IGenericSingleton<PuntoDeInteres> IGSPI;
        IConection IC;
        private Singleton()
        {
            IGSR = this;
            IGSU = this;
            IGSP = this;
            IGSG = this;
            IGSA = this;
            IGSPR = this;
            IGSPI = this;
            IC = this;
        }
        public static Singleton GetInstance { get { return _Instance; } }
    }
}