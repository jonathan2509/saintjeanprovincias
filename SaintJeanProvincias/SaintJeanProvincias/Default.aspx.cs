using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassBussines;
public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Accion"] != null)
        {
            switch (Request["Accion"].ToString())
            {
                case "Login":
                    CargarUsuario();
                    break;
                case "ListarRoles":
                    ListarRoles();
                    break;
                case "BuscarProvincia":
                    BuscarProvincia();
                    break;
                case "BuscarGobernador":
                    BuscarGobernador();
                    break;
                case "ListarProductosRegionales":
                    ListarProductosRegionales();
                    break;
                case "ListarPuntosDeInteres":
                    ListarPuntosDeInteres();
                    break;
                case "AsignarNota":
                    AsignarNota();
                    break;
                case "ModificarProvincia":
                    ModificarProvincia();
                    break;
                case "AgregarProductoRegional":
                    AgregarProductoRegional();
                    break;
                case "EliminarProductoRegional":
                    EliminarProductoRegional();
                    break;
                case "AgregarPuntoDeInteres":
                    AgregarPuntoDeInteres();
                    break;
                case "EliminarPuntoDeInteres":
                    EliminarPuntoDeInteres();
                    break;
                case "ModificarPuntoDeInteres":
                    ModificarPuntoDeInteres();
                    break;
                case "ModificarProductoRegional":
                    ModificarProductoRegional();
                    break;
                case "ListarAlumnos":
                    ListarAlumnos();
                    break;
                case "ListarProvincias":
                    ListarProvincias();
                    break;
                case "AgregarAlumno":
                    AgregarAlumno();
                    break;
                case "EliminarAlumno":
                    EliminarAlumno();
                    break;
                case "ModificarAlumno":
                    ModificarAlumno();
                    break;
                case "BuscarUsuario":
                    BuscarUsuario();
                    break;
                case "EditarPerfil":
                    EditarPerfil();
                    break;
            }
        }
    }
    private void EditarPerfil()
    {
        Usuario Usuario = new Usuario();
        try
        {
            Usuario.ID = int.Parse(Request["IDUsuario"].ToString());
            if (Request["TBClaveNueva2"].ToString() != "")
            {
                Usuario.Clave = Request["TBClaveNueva2"].ToString();
                Usuario.Modify();
            }
            if (Request.Files["TBFotoUsuario"].ContentLength > 0)
            {
                Usuario.SaveImage(Request.Files["TBFotoUsuario"]);
            }
            Response.Write(Usuario.Find());
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void BuscarUsuario()
    {
        Usuario Usuario = new Usuario();
        Usuario.ID = int.Parse(Request["idGrupo"].ToString());
        try
        {
            Response.Write(Usuario.Find());
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void ModificarAlumno()
    {
        Alumno Alumno = new Alumno();
        Alumno.ID = int.Parse(Request["idAlumno"].ToString());
        Alumno.Nombre = Request["nombre"].ToString();
        Alumno.DNI = int.Parse(Request["dni"].ToString());
        Alumno.Provincia = new Provincia();
        Alumno.Provincia.ID = int.Parse(Request["idGrupo"].ToString());
        try
        {
            Alumno.Modify();
            Response.Write(Alumno.Find());
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void EliminarAlumno()
    {
        try
        {
            Alumno Alumno = new Alumno();
            Alumno.ID = int.Parse(Request["idAlumno"].ToString());
            Alumno.Erase();
            Response.Write("");
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void AgregarAlumno()
    {
        Alumno Alumno = new Alumno();
        Alumno.Nombre = Request["nombre"].ToString();
        Alumno.DNI = int.Parse(Request["dni"].ToString());
        Alumno.Provincia = new Provincia();
        Alumno.Provincia.ID = int.Parse(Request["idGrupo"].ToString());
        try
        {
            Alumno.Add();
            Response.Write(Alumno.Find());
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void ListarAlumnos()
    {
        Alumno Alumno = new Alumno();
        Response.Write(Alumno.ListToJson());
    }
    private void ListarProvincias()
    {
        Provincia Provincia = new Provincia();
        Response.Write(Provincia.ListToJson());
    }
    private void ModificarProductoRegional()
    {
        ProductoRegional ProductoRegional = new ProductoRegional();
        ProductoRegional.ID = int.Parse(Request["IDProductoRegional"].ToString());
        ProductoRegional.Nombre = Request["TBENombrePR"].ToString();
        ProductoRegional.Descripcion = Request["TBEDescripcionPR"].ToString();
        ProductoRegional.Provincia = new Provincia();
        ProductoRegional.Provincia.ID = int.Parse(Request["IDProvincia"].ToString());
        try
        {
            ProductoRegional.Modify();
            if (Request.Files["TBEFotoPR"].ContentLength > 0)
            {
                ProductoRegional.SaveImage(Request.Files["TBEFotoPR"]);
            }
            Response.Write(ProductoRegional.Find());
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void ModificarPuntoDeInteres()
    {
        PuntoDeInteres PuntoDeInteres = new PuntoDeInteres();
        PuntoDeInteres.ID = int.Parse(Request["IDPuntoDeInteres"].ToString());
        PuntoDeInteres.Nombre = Request["TBENombrePI"].ToString();
        PuntoDeInteres.Descripcion = Request["TBEDescripcionPI"].ToString();
        PuntoDeInteres.Provincia = new Provincia();
        PuntoDeInteres.Provincia.ID = int.Parse(Request["IDProvincia"].ToString());
        try
        {
            PuntoDeInteres.Modify();
            if (Request.Files["TBEFotoPI"].ContentLength > 0)
            {
                PuntoDeInteres.SaveImage(Request.Files["TBEFotoPI"]);
            }
            Response.Write(PuntoDeInteres.Find());
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void EliminarPuntoDeInteres()
    {
        try
        {
            PuntoDeInteres PuntoDeInteres = new PuntoDeInteres();
            PuntoDeInteres.ID = int.Parse(Request["idPuntoDeInteres"].ToString());
            PuntoDeInteres.Erase();
            Response.Write("");
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void AgregarPuntoDeInteres()
    {
        try
        {
            PuntoDeInteres PuntoDeInteres = new PuntoDeInteres();
            PuntoDeInteres.Nombre = Request["TBNombrePI"].ToString();
            PuntoDeInteres.Descripcion = Request["TBDescripcionPI"].ToString();
            PuntoDeInteres.Provincia = new Provincia();
            PuntoDeInteres.Provincia.ID = int.Parse(Request["IDProvincia"].ToString());
            PuntoDeInteres.Add();
            if (Request.Files["TBFotoPI"].ContentLength > 0)
            {
                PuntoDeInteres.SaveImage(Request.Files["TBFotoPI"]);
            }
            Response.Write(PuntoDeInteres.Find());
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void EliminarProductoRegional()
    {
        try
        {
            ProductoRegional ProductoRegional = new ProductoRegional();
            ProductoRegional.ID = int.Parse(Request["idProductoRegional"].ToString());
            ProductoRegional.Erase();
            Response.Write("");
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void AgregarProductoRegional()
    {
        try
        {
            ProductoRegional ProductoRegional = new ProductoRegional();
            ProductoRegional.Nombre = Request["TBNombrePR"].ToString();
            ProductoRegional.Descripcion = Request["TBDescripcionPR"].ToString();
            ProductoRegional.Provincia = new Provincia();
            ProductoRegional.Provincia.ID = int.Parse(Request["IDProvincia"].ToString());
            ProductoRegional.Add();
            if (Request.Files["TBFotoPR"].ContentLength > 0)
            {
                ProductoRegional.SaveImage(Request.Files["TBFotoPR"]);
            }
            Response.Write(ProductoRegional.Find());
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void ModificarProvincia()
    {
        try
        {
            Provincia Provincia = new Provincia();
            Provincia.ID = int.Parse(Request["IDProvincia"].ToString());
            Provincia.Find();
            Provincia.Capital = Request["TBCapital"].ToString();
            Provincia.Poblacion = int.Parse(Request["TBPoblacion"].ToString());
            Provincia.Superficie = int.Parse(Request["TBSuperficie"].ToString());
            Provincia.CantSenadores = int.Parse(Request["TBSenadores"].ToString());
            Provincia.CantDiputados = int.Parse(Request["TBDiputados"].ToString());
            Provincia.Modify();
            if (Request.Files["TBMapa"].ContentLength > 0)
            {
                Provincia.SaveImage(Request.Files["TBMapa"]);
            }
            Gobernador Gobernador = new Gobernador();
            Gobernador.ID = Provincia.ID;
            Gobernador.Find();
            Gobernador.Nombre = Request["TBNombre"].ToString();
            Gobernador.Apellido = Request["TBApellido"].ToString();
            Gobernador.PeriodoGobierno = Request["TBPeriodo"].ToString();
            Gobernador.Historial = Request["TBHistorial"].ToString();
            Gobernador.Modify();
            if (Request.Files["TBGobernador"].ContentLength > 0)
            {
                Gobernador.SaveImage(Request.Files["TBGobernador"]);
            }
            Response.Write(Provincia.Find());
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
    }
    private void AsignarNota()
    {
        try
        {
            Provincia Provincia = new Provincia();
            Provincia.ID = int.Parse(Request["idProvincia"].ToString());
            Provincia.Find();
            Provincia.Nota = int.Parse(Request["nota"].ToString());
            Provincia.Modify();
            Response.Write(Provincia.Find());
        }
        catch (Exception)
        {
            Response.Write("");
        }
    }
    private void BuscarProvincia()
    {
        Provincia Provincia = new Provincia();
        Provincia.ID = int.Parse(Request["idProvincia"].ToString());
        Response.Write(Provincia.Find());
    }
    private void BuscarGobernador()
    {
        Gobernador Gobernador = new Gobernador();
        Gobernador.ID = int.Parse(Request["idProvincia"].ToString());
        Response.Write(Gobernador.Find());
    }
    private void ListarProductosRegionales()
    {
        ProductoRegional ProductoRegional = new ProductoRegional();
        ProductoRegional.Provincia = new Provincia();
        ProductoRegional.Provincia.ID = int.Parse(Request["idProvincia"].ToString());
        Response.Write(ProductoRegional.ListToJson());
    }
    private void ListarPuntosDeInteres()
    {
        PuntoDeInteres PuntoDeInteres = new PuntoDeInteres();
        PuntoDeInteres.Provincia = new Provincia();
        PuntoDeInteres.Provincia.ID = int.Parse(Request["idProvincia"].ToString());
        Response.Write(PuntoDeInteres.ListToJson());
    }
    private void ListarRoles()
    {
        Rol Rol = new Rol();
        try
        {
            Response.Write(Rol.ListToJson());
        }
        catch (Exception Error)
        {
            Response.Write(Error.Message);
        }
        
    }
    private void CargarUsuario()
    {
        Usuario Usuario = new Usuario();
        Usuario.Nombre = Request["Nombre"].ToString();
        Usuario.Clave = Request["Clave"].ToString();
        try
        {
            Response.Write(Usuario.LogIn());
        }
        catch (Exception)
        {
            Response.Write("");
        }
    }
}