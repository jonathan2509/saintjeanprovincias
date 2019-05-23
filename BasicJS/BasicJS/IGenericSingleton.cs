using System;
using System.Data;
using System.Collections.Generic;
namespace BasicJS
{
    public interface IGenericSingleton<T>
    {
        void Add(T Data);
        void Modify(T Data);
        void Erase(T Data);
        string Find(T Data);
        string LogIn(T Data);
        List<T> List(T Data);
        string ListToJson(T Data);
        /// <summary>
        /// Carga los datos del DataRow (DR) en el objeto T (Data).
        /// </summary>
        /// <param name="DR"></param>
        /// <param name="Data"></param>
        void LoadClass(DataRow DR, T Data);
    }
}