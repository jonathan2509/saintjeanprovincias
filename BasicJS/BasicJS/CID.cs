using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BasicJS
{
    public abstract class CID<T> : IID, IAbstractID<T>, IJson
    {
        #region IID
        IID IID;
        IJson IJson;
        string IID.Directory { get; set; }
        string IID.Prefix { get; set; }
        string IID.URL
        {
            get
            {
                IID.ResetPrefix();
                if (File.Exists(IID.Path))
                {
                    return "Imagenes/" + IID.Directory + "/" + IID.Prefix + this.ID + ".jpg";
                }
                IID.ChangePrefix();
                if (File.Exists(IID.Path))
                {
                    return "Imagenes/" + IID.Directory + "/" + IID.Prefix + this.ID + ".jpg";
                }
                IID.ResetPrefix();
                return "Imagenes/" + IID.Directory + "/" + IID.Prefix + "Default.jpg";
            }
        }
        string IID.Path
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\" + IID.Directory + "\\" + IID.Prefix + this.ID + ".jpg";
            }
        }
        public int ID { get; set; }
        HttpPostedFile IID.FU { get; set; }
        public CID()
        {
            IID = this;
            IJson = this;
        }
        void IID.ChangePrefix()
        {
            if (IID.Prefix.EndsWith("_"))
            {
                IID.ResetPrefix();
            }
            else
            {
                IID.Prefix += "_";
            }
        }
        void IID.DeleteFile()
        {
            IID.ResetPrefix();
            if (File.Exists(IID.Path))
            {
                File.Delete(IID.Path);
                IID.ChangePrefix();
                return;
            }
            IID.ChangePrefix();
            if (File.Exists(IID.Path))
            {
                File.Delete(IID.Path);
                IID.ChangePrefix();
                return;
            }
            IID.ResetPrefix();
        }
        void IID.ResetPrefix()
        {
            if (IID.Prefix.EndsWith("_"))
            {
                IID.Prefix = IID.Prefix.Remove(IID.Prefix.Length - 1);
            }
        }
        void IID.SaveFile()
        {
            if (IID.FU == null) return;
            IID.DeleteFile();
            IID.FU.SaveAs(IID.Path);
        }
        #endregion
        #region IAbstractID
        public abstract void Add();
        public abstract void Modify();
        public abstract void Erase();
        public abstract string Find();
        public abstract List<T> List();
        public abstract string ListToJson();
        public abstract string LogIn();
        #endregion
        #region IJson
        public string TableToJson(DataTable DT)
        {
            if (DT.Rows.Count == 0) return "{}";
            string Json = "[";
            foreach (DataRow DR in DT.Rows)
            {
                Json += IJson.RowToJson(DR) + ",";

            }
            Json = Json.Remove(Json.Length - 1) + "]";
            return Json;
        }
        public string RowToJson(DataRow DR)
        {
            string Quot = "\"";
            string Json = "{";
            for (int i = 0; i < DR.Table.Columns.Count; i++)
            {
                Json += Quot + DR.Table.Columns[i].ColumnName + Quot + ":" + Quot + DR[i].ToString() + Quot + ",";
            }
            if(IID.Directory != null)
            {
                int OldID = this.ID;
                this.ID = int.Parse(DR["ID"].ToString());
                Json += Quot + "Foto" + Quot + ":" + Quot + IID.URL + Quot + ",";
                this.ID = OldID;
            }
            Json = Json.Remove(Json.Length - 1) + "}";
            return Json;
        }
        #endregion
    }
}
