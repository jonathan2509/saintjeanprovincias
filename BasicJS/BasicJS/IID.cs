using System;
using System.Web;

namespace BasicJS
{
    public interface IID
    {
        string Directory { get; set; }
        string Prefix { get; set; }
        string URL { get; }
        string Path { get; }
        int ID { get; set; }
        /// <summary>
        /// Se implementa un cambio de prefijo para solucionar un problema del
        /// cache donde queda una imagen guardada y no corresponde.
        /// </summary>
        void ChangePrefix();
        void ResetPrefix();
        HttpPostedFile FU { get; set; }
        void DeleteFile();
        void SaveFile();
    }
}
