var Event0;
var Usuario;
function Evento() {
    var Roles = undefined;
    var Provincia = undefined;
    var Alumnos = undefined;
    var Provincias = undefined;
    Usuario = new Object();
    CargarRoles();
    // Funciones.
    function CargarRoles() {
        var Params = "Accion=ListarRoles";
        var json = cargarArchivoPost("Default.aspx", Params);
        try {
            Roles = JSON.parse(json);
        } catch (e) {
            alert(e);
        }
    }
    // Reemplaza todos aquellos caracteres raros dentro de un json que reciba.
    function replaceString(json) {
        json = json.replace(/\\n/g, "\\n")
            .replace(/\\'/g, "\\'")
            .replace(/\\"/g, '\\"')
            .replace(/\\&/g, "\\&")
            .replace(/\\r/g, "\\r")
            .replace(/\\t/g, "\\t")
            .replace(/\\b/g, "\\b")
            .replace(/\\f/g, "\\f");
        json = json.replace(/[\u0000-\u0019]+/g, "\\n");
        return json;
    }
    // Carga un array de alumnos utilizando un servicio aspx.
    function CargarAlumnos() {
        var params = "Accion=ListarAlumnos";
        var json = cargarArchivoPost("Default.aspx", params);
        try {
            Alumnos = JSON.parse(json);
            BuscarGrupos();
        } catch (e) {
            swal("Oops...!", "Hubo un problema intentando cargar la pantalla de alumnos!", "error");
        }
    }
    // Busca y asigna la provincia correspondiente de cada alumno.
    function BuscarGrupos() {
        for (var i = 0; i < Alumnos.length; i++) {
            Alumnos[i].Grupo = BuscarProvincia(Alumnos[i]);
        }
    }
    // Busca una provincia según un ID de Provincia proveniente de un alumno.
    function BuscarProvincia(Alumno) {
        for (var j = 0; j < Provincias.length; j++) {
            if (Alumno.IDProvincia === Provincias[j].ID) {
                return Provincias[j];
            }
        }
    }
    // Carga un array de provincias utilizando un servicio aspx.
    function CargarProvincias() {
        var params = "Accion=ListarProvincias";
        var json = cargarArchivoPost("Default.aspx", params);
        try {
            Provincias = JSON.parse(json);
        } catch (e) {
            swal("Oops...!", "Hubo un problema intentando cargar la pantalla de alumnos!", "error");
        }
    }
    // Carga una provincia con su gobernador, productos regionales (array) y puntos de interes (array).
    function CargarProvincia(idProvincia) {
        var ParamsProvincia = "Accion=BuscarProvincia&idProvincia=" + idProvincia;
        var JsonProvincia = cargarArchivoPost("Default.aspx", ParamsProvincia);
        var ParamsGobernador = "Accion=BuscarGobernador&idProvincia=" + idProvincia;
        var JsonGobernador = cargarArchivoPost("Default.aspx", ParamsGobernador);
        JsonGobernador = replaceString(JsonGobernador);
        var ParamsProductosRegionales = "Accion=ListarProductosRegionales&idProvincia=" + idProvincia;
        var JsonProductosRegionales = cargarArchivoPost("Default.aspx", ParamsProductosRegionales);
        JsonProductosRegionales = replaceString(JsonProductosRegionales);
        var ParamsPuntosDeInteres = "Accion=ListarPuntosDeInteres&idProvincia=" + idProvincia;
        var JsonPuntosDeInteres = cargarArchivoPost("Default.aspx", ParamsPuntosDeInteres);
        JsonPuntosDeInteres = replaceString(JsonPuntosDeInteres);
        try {
            Provincia = JSON.parse(JsonProvincia);
            var Gobernador = JSON.parse(JsonGobernador);
            var ProductosRegionales = JSON.parse(JsonProductosRegionales);
            var PuntosDeInteres = JSON.parse(JsonPuntosDeInteres);
            Provincia.Gobernador = Gobernador;
            Provincia.ProductosRegionales = ProductosRegionales;
            Provincia.PuntosDeInteres = PuntosDeInteres;
        } catch (e) {
            alert(e);
        }
    }
    // Busca y asigna el rol del usuario según su ID de Rol.
    function BuscarRol() {
        for (var i = 0; i < Roles.length; i++) {
            if (Usuario.IDRol === Roles[i].ID) {
                Usuario.Rol = Roles[i];
                return;
            }
        }
    }
    // Métodos.
    // Cierra la sesión y carga las pantallas de inicio por defecto.
    this.CerrarSesion = function () {
        Usuario = undefined;
        Provincia = undefined;
        Screen0.NavegadorAnonimo();
        Screen0.PreLogIn();
        Screen0.PantallaInicio();
    };
    // Realiza login de usuario y lo carga en el objeto Usuario, luego carga el navegador según su rol correspondiente y pantalla de inicio.
    this.IniciarSesion = function () {
        var TBNombre = document.getElementById('TBNombre');
        var TBClave = document.getElementById('TBClave');
        if (TBNombre.value === "") {
            swal("Importante", "Debe ingresar su nombre de usuario.");
            TBNombre.focus();
        }
        else if (TBClave.value === "") {
            swal("Importante", "Debe ingresar su contraseña.");
            TBClave.focus();
        }
        else {
            var Params = "Accion=Login&Nombre=" + TBNombre.value + "&Clave=" + TBClave.value;
            var json = cargarArchivoPost("Default.aspx", Params);
            try {
                Usuario = JSON.parse(json);
                BuscarRol();
                Screen0.PostLogIn(Usuario);
                if (Usuario.Rol.Nombre === "Grupo") {
                    Screen0.NavegadorGrupo(Usuario);
                }
                else {
                    Screen0.NavegadorMaestro();
                }
                Screen0.PantallaInicio();
            } catch (e) {
                TBNombre.value = "";
                TBClave.value = "";
                swal("Oops...", "Usuario y contraseña no coinciden.", "error");
            }
        }
    };
    // Borra el contenido de los campos de texto de login.
    this.Cancelar = function () {
        document.getElementById('TBNombre').value = "";
        document.getElementById('TBClave').value = "";
    };
    // Carga la provincia y muestra la pantalla de provincia con vista de usuario anónimo.
    this.ProvinciaAnonimo = function (idProvincia) {
        CargarProvincia(idProvincia);
        Screen0.ProvinciaAnonimo(Provincia);
    };
    // Carga la provincia y muestra la pantalla de provincia con vista de usuario Maestro.
    this.ProvinciaMaestro = function (idProvincia) {
        CargarProvincia(idProvincia);
        Screen0.ProvinciaMaestro(Provincia);
    };
    // Carga la provincia y muestra la pantalla de provincia con vista de usuario Grupo.
    this.ProvinciaGrupo = function (idProvincia) {
        CargarProvincia(idProvincia);
        Screen0.ProvinciaGrupo(Provincia);
    };
    // Se encarga de limitar la nota asignada por el maestro en un rango entre 0 y 10.
    this.LimitarNota = function () {
        var calificacion = document.getElementById('TBCalificacion');
        if (calificacion.value !== '' && calificacion.value.indexOf('.') === -1) {
            calificacion.value = Math.max(Math.min(calificacion.value, 10), 0);
        }
        else if (calificacion.value !== '' && calificacion.value.indexOf('.') !== -1) {
            calificacion.value = Math.max(Math.min(Math.round(calificacion.value), 10), 0);
        }
    };
    this.RefrescarCampoNota = function () {
        var nota = document.getElementById('TBCalificacion');
        nota.value = Provincia.Nota;
    };
    // Envía por AJAX una solicitud y modifica la nota de la provincia.
    this.AsignarNota = function (idProvincia) {
        var calificacion = document.getElementById('TBCalificacion').value;
        if (calificacion === "") {
            return swal("Oops...", "Se debe asignar una nota entre 0 y 10", "error");
        }
        var regex = new RegExp("^([0-9])+$");
        if (!regex.test(calificacion)) {
            return swal("Oops...", "El campo 'Nota' debe ser un número entre 0 y 10.", "error");
        }
        if (parseInt(calificacion) < 0 || parseInt(calificacion) > 10) {
            return swal("Oops...", "El campo 'Nota' debe ser un número entre 0 y 10.", "error");
        }
        var Params = "Accion=AsignarNota&idProvincia=" + idProvincia + "&nota=" + calificacion;
        var Json = cargarArchivoPost("Default.aspx", Params);
        try {
            Provincia = JSON.parse(Json);
            swal("Listo!", "La nota fue asignada correctamente al grupo de " + Provincia.Nombre + "!", "success");
        }
        catch (e) {
            swal("Oops...", "Hubo un error al asignar la nota. Por favor, intente nuevamente más tarde...", "error");
        }
    };
    // Carga en el evento onload del iframe de provincias la respuesta a la solicitud que se realiza mediante el formulario.
    // Esta respuesta resulta ser un JSON de la provincia modificada solamente. Luego hace submit del formulario.
    this.GuardarProvincia = function () {
        var regexp_nr = "^([0-9])+$";
        var regexp_str = "^[a-zA-ZñÑ\'áéíóúÁÉÍÓÚ]+( [a-zA-ZñÑ\'áéíóúÁÉÍÓÚ]+)*$";
        var regex = new RegExp(regexp_nr);
        var tbPoblacion = document.getElementById('TBPoblacion');
        if (tbPoblacion.value !== "" && !regex.test(tbPoblacion.value)) {
            swal("Alto ahí!", "El campo 'Población' debe ser un número entero positivo.", "error");
            return;
        }
        var tbSuperficie = document.getElementById('TBSuperficie');
        if (tbSuperficie.value !== "" && !regex.test(tbSuperficie.value)) {
            swal("Alto ahí!", "El campo 'Superficie' debe ser un número entero positivo.", "error");
            return;
        }
        var tbSenadores = document.getElementById('TBSenadores');
        if (tbSenadores.value !== "" && !regex.test(tbSenadores.value)) {
            swal("Alto ahí!", "El campo 'Senadores' debe ser un número entero positivo.", "error");
            return;
        }
        var tbDiputados = document.getElementById('TBDiputados');
        if (tbDiputados.value !== "" && !regex.test(tbDiputados.value)) {
            swal("Alto ahí!", "El campo 'Diputados' debe ser un número entero positivo.", "error");
            return;
        }
        regex = new RegExp(regexp_str);
        var tbCapital = document.getElementById('TBCapital');
        if (tbCapital.value !== "" && !regex.test(tbCapital.value)) {
            swal("Alto ahí!", "El campo 'Capital' debe contener solo letras y un espacio entre palabras.", "error");
            return;
        }
        var tbNombre = document.getElementById('TBNombre');
        if (tbNombre.value !== "" && !regex.test(tbNombre.value)) {
            swal("Alto ahí!", "El campo 'Nombre' debe contener solo letras y un espacio entre palabras.", "error");
            return;
        }
        var tbApellido = document.getElementById('TBApellido');
        if (tbApellido.value !== "" && !regex.test(tbApellido.value)) {
            swal("Alto ahí!", "El campo 'Apellido' debe contener solo letras y un espacio entre palabras.", "error");
            return;
        }
        var iframe = document.getElementById('iframeProvincia');
        iframe.onload = function () {
            json = document.getElementById('iframeProvincia').contentWindow.document.body.innerHTML;
            try {
                Provincia = JSON.parse(json);
                swal("Listo!", "Los cambios fueron realizados correctamente para la provincia de " + Provincia.Nombre + "!", "success");
            }
            catch (e) {
                swal("Oops...", json, "error");
            }
        };
        document.getElementById('formProvincia').submit();
    };
    // Carga dinámicamente la imagen del mapa de la provincia en la pantalla.
    this.CargarMapa = function () {
        $(document).on('change', '#TBMapa', function (e) {
            var TmpPath = URL.createObjectURL(e.target.files[0]);
            $('#IMGMapa').attr('src', TmpPath);
        });
    };
    // Carga dinámicamente la imagen del gobernador de la provincia en la pantalla.
    this.CargarGobernador = function () {
        $(document).on('change', '#TBGobernador', function (e) {
            var TmpPath = URL.createObjectURL(e.target.files[0]);
            $('#IMGGobernador').attr('src', TmpPath);
        });
    };
    // Agrega un producto regional mediante el envío de un formulario. Se carga el evento onload del iframe la respuesta del servidor y se refresca la pantalla de provincia.
    this.AgregarProductoRegional = function () {
        var nombre = document.getElementById('TBNombrePR').value;
        var descripcion = document.getElementById('TBDescripcionPR').value;
        if (nombre === "") {
            return swal("Oops...", "Se debe ingresar el nombre del producto regional.", "error");
        }
        var regex = new RegExp('^[a-zA-ZñÑ\'áéíóúÁÉÍÓÚ0-9]+( [a-zA-ZñÑ\'áéíóúÁÉÍÓÚ0-9]+)*$');
        if (!regex.test(nombre)) {
            return swal("Oops...", "El nombre del producto regional debe contener solo letras y/o números.", "error");
        }
        if (descripcion === "") {
            return swal("Oops...", "Se debe ingresar al menos una breve descripción del producto regional.", "error");
        }
        var iframe = document.getElementById('iframePR');
        iframe.onload = function () {
            json = document.getElementById('iframePR').contentWindow.document.body.innerHTML;
            json = replaceString(json);
            try {
                var ProductoRegional = JSON.parse(json);
                Provincia.ProductosRegionales.push(ProductoRegional);
                Screen0.ProvinciaGrupo(Provincia);
                swal("Listo!", "Se agregó un Producto Regional a la provincia de " + Provincia.Nombre + "!", "success");
            }
            catch (e) {
                swal("Oops...", json, "error");
            }
        };
        document.getElementById('formAPR').submit();
    };
    // Elimina un producto regional mediante una solicitud AJAX.
    this.EliminarProductoRegional = function (idProductoRegional, index) {
        swal({
            title: "Está seguro?",
            text: "Se eliminará el producto regional por completo!",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((willDelete) => {
            if (willDelete) {
                params = "Accion=EliminarProductoRegional&idProductoRegional=" + idProductoRegional;
                json = cargarArchivoPost("Default.aspx", params);
                try {
                    if (json !== "") {
                        var catchText = JSON.parse(json);
                    }
                    Provincia.ProductosRegionales.splice(index, 1);
                    Screen0.ProvinciaGrupo(Provincia);
                    swal("Listo!", "Se eliminó el Producto Regional de la provincia de " + Provincia.Nombre + "!", "success");
                }
                catch (e) {
                    swal("Oops...", "Hubo un error al eliminar los datos. Por favor, intente nuevamente más tarde...", "error");
                }
            }
        });
    };
    // Activa la pantalla modal de un producto regional para ver su imagen y descripción.
    this.VerProductoRegional = function (index) {
        Screen0.ModalImagen(Provincia.ProductosRegionales[index]);
    };
    // Abre la ventana modal para editar un producto regional y la carga de los datos del producto seleccionado.
    this.EditarProductoRegional = function (index) {
        Screen0.ModalEditarPR(Provincia.ProductosRegionales[index], index, Provincia.ID);
    };
    // Envía el formulario de edición del producto regional al servidor y recibe como respuesta el producto regional modificado.
    this.ModificarProductoRegional = function (index) {
        var nombre = document.getElementById('TBENombrePR').value;
        var descripcion = document.getElementById('TBEDescripcionPR').value;
        if (nombre === "") {
            return swal("Oops...", "No se debe borrar el nombre del producto regional.", "error");
        }
        var regex = new RegExp('^[a-zA-ZñÑ\'áéíóúÁÉÍÓÚ0-9]+( [a-zA-ZñÑ\'áéíóúÁÉÍÓÚ0-9]+)*$');
        if (!regex.test(nombre)) {
            return swal("Oops...", "El nombre del producto regional debe contener solo letras y/o números.", "error");
        }
        if (descripcion === "") {
            return swal("Oops...", "No se debe borrar la descripción del producto regional.", "error");
        }
        var iframe = document.getElementById('iframeMPR');
        iframe.onload = function () {
            json = document.getElementById('iframeMPR').contentWindow.document.body.innerHTML;
            json = replaceString(json);
            try {
                var ProductoRegional = JSON.parse(json);
                Provincia.ProductosRegionales[index] = ProductoRegional;
                Screen0.ProvinciaGrupo(Provincia);
                swal("Listo!", "Se modificó el Producto Regional de la provincia de " + Provincia.Nombre + "!", "success");
            }
            catch (e) {
                swal("Oops...", json, "error");
            }
        };
        document.getElementById('formMPR').submit();
    };
    // Agrega un punto de interés mediante el envío de un formulario. Se carga el evento onload del iframe la respuesta del servidor y se refresca la pantalla de provincia.
    this.AgregarPuntoDeInteres = function () {
        var nombre = document.getElementById('TBNombrePI').value;
        var descripcion = document.getElementById('TBDescripcionPI').value;
        if (nombre === "") {
            return swal("Oops...", "Se debe ingresar el nombre del punto de interés.", "error");
        }
        var regex = new RegExp('^[a-zA-ZñÑ\'áéíóúÁÉÍÓÚ0-9]+( [a-zA-ZñÑ\'áéíóúÁÉÍÓÚ0-9]+)*$');
        if (!regex.test(nombre)) {
            return swal("Oops...", "El nombre del punto de interés debe contener solo letras y/o números.", "error");
        }
        if (descripcion === "") {
            return swal("Oops...", "Se debe ingresar al menos una breve descripción del punto de interés.", "error");
        }
        var iframe = document.getElementById('iframePI');
        iframe.onload = function () {
            json = document.getElementById('iframePI').contentWindow.document.body.innerHTML;
            json = replaceString(json);
            try {
                var PuntoDeInteres = JSON.parse(json);
                Provincia.PuntosDeInteres.push(PuntoDeInteres);
                Screen0.ProvinciaGrupo(Provincia);
                swal("Listo!", "Se agregó un Punto de Interés a la provincia de " + Provincia.Nombre + "!", "success");
            }
            catch (e) {
                swal("Oops...", json, "error");
            }
        };
        document.getElementById('formAPI').submit();
    };
    // Elimina un punto de interés mediante una solicitud AJAX.
    this.EliminarPuntoDeInteres = function (idPuntoDeInteres, index) {
        swal({
            title: "Está seguro?",
            text: "Se eliminará el punto de interés por completo!",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((willDelete) => {
            if (willDelete) {
                params = "Accion=EliminarPuntoDeInteres&idPuntoDeInteres=" + idPuntoDeInteres;
                json = cargarArchivoPost("Default.aspx", params);
                try {
                    if (json !== "") {
                        var catchText = JSON.parse(json);
                    }
                    Provincia.PuntosDeInteres.splice(index, 1);
                    Screen0.ProvinciaGrupo(Provincia);
                    swal("Listo!", "Se eliminó el Producto Regional de la provincia de " + Provincia.Nombre + "!", "success");
                }
                catch (e) {
                    swal("Oops...", "Hubo un error al eliminar los datos. Por favor, intente nuevamente más tarde...", "error");
                }
            }
        });
    };
    // Activa la pantalla modal de un punto de interés para ver su imagen y descripción.
    this.VerPuntoDeInteres = function (index) {
        Screen0.ModalImagen(Provincia.PuntosDeInteres[index]);
    };
    // Abre la ventana modal para editar un punto de interés y la carga de los datos del punto de interés seleccionado.
    this.EditarPuntoDeInteres = function (index) {
        Screen0.ModalEditarPI(Provincia.PuntosDeInteres[index], index, Provincia.ID);
    };
    // Envía el formulario de edición del punto de interés al servidor y recibe como respuesta el punto de interés modificado.
    this.ModificarPuntoDeInteres = function (index) {
        var nombre = document.getElementById('TBENombrePI').value;
        var descripcion = document.getElementById('TBEDescripcionPI').value;
        if (nombre === "") {
            return swal("Oops...", "No debe borrar el nombre del punto de interés.", "error");
        }
        var regex = new RegExp('^[a-zA-ZñÑ\'áéíóúÁÉÍÓÚ0-9]+( [a-zA-ZñÑ\'áéíóúÁÉÍÓÚ0-9]+)*$');
        if (!regex.test(nombre)) {
            return swal("Oops...", "El nombre del punto de interés debe contener solo letras y/o números.", "error");
        }
        if (descripcion === "") {
            return swal("Oops...", "No debe borrar la descripción del punto de interés.", "error");
        }
        var iframe = document.getElementById('iframeMPI');
        iframe.onload = function () {
            json = document.getElementById('iframeMPI').contentWindow.document.body.innerHTML;
            json = replaceString(json);
            try {
                var PuntoDeInteres = JSON.parse(json);
                Provincia.PuntosDeInteres[index] = PuntoDeInteres;
                Screen0.ProvinciaGrupo(Provincia);
                swal("Listo!", "Se modificó el Punto de Interés de la provincia de " + Provincia.Nombre + "!", "success");
            }
            catch (e) {
                swal("Oops...", json, "error");
            }
        };
        document.getElementById('formMPI').submit();
    };
    // Devuelve el array de productos regionales para cargarlos en su tabla respectiva. AddTable.js
    this.ListarProductosRegionales = function () {
        return Provincia.ProductosRegionales;
    };
    // Devuelve el array de puntos de interés para cargarlos en su tabla respectiva. AddTable.js
    this.ListarPuntosDeInteres = function () {
        return Provincia.PuntosDeInteres;
    };
    // Se encarga de filtrar la tabla de alumnos mediante nombre, dni o grupo.
    this.FiltrarTablaAlumnos = function () {
        $("#TBFiltro").on("keyup", function () {
            var value = $(this).val().toLowerCase();
            $("#tbodyAlumnos tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
            });
        });
    };
    // Carga las provincias en un array al igual que los alumnos, luego carga la pantalla de alumnos.
    this.PantallaAlumnos = function () {
        CargarProvincias();
        CargarAlumnos();
        Screen0.PantallaAlumnos(Provincias);
    };
    // Devuelve el array de alumnos para cargarlos en su tabla respectiva. AddTable.js
    this.ListarAlumnos = function () {
        return Alumnos;
    };
    // Valida nombre, luego dni mediante una expresión regular. Envía una solicitud AJAX al servidor para agregar el nuevo alumno, luego recarga la pantalla de alumnos.
    this.AgregarAlumno = function () {
        var nombre = document.getElementById('TBNombreA').value;
        var dni = document.getElementById('TBDNIA').value;
        if (nombre === "") {
            return swal("Oops...", "Debe ingresar el nombre!", "error");
        }
        var regex = new RegExp("^[a-zA-ZñÑ\'áéíóúÁÉÍÓÚ]+( [a-zA-ZñÑ\'áéíóúÁÉÍÓÚ]+)*$");
        if (!regex.test(nombre)) {
            return swal("Oops...", "El campo 'Nombre' debe contener solo letras, separando cada palabra con un espacio.", "error");
        }
        if (dni === 0 || dni === "") {
            return swal("Oops...", "Debe ingresar el D.N.I.!", "error");
        }
        regex = new RegExp("^([0-9]){8}?$");
        if (!regex.test(dni)) {
            return swal("Oops...", "El campo 'DNI' debe contener solo ocho números enteros positivos.", "error");
        }
        var tbGrupo = document.getElementById('CBGrupoA');
        var grupo = tbGrupo.options[tbGrupo.selectedIndex].value;
        params = "Accion=AgregarAlumno&nombre=" + nombre + "&dni=" + dni + "&idGrupo=" + grupo;
        json = cargarArchivoPost("Default.aspx", params);
        try {
            var Alumno = JSON.parse(json);
            Alumno.Grupo = BuscarProvincia(Alumno);
            Alumnos.push(Alumno);
            Screen0.PantallaAlumnos(Provincias);
            swal("Listo!", "El alumno fue guardado correctamente!", "success");
        }
        catch (e) {
            swal("Oops...", "Hubo un error al guardar los datos. Por favor, intente nuevamente más tarde...", "error");
        }
    };
    // Elimina un alumno mediante una solicitud AJAX al servidor.
    this.EliminarAlumno = function (idAlumno, index) {
        swal({
            title: "Está seguro?",
            text: "Se eliminará el alumno por completo!",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((willDelete) => {
            if (willDelete) {
                params = "Accion=EliminarAlumno&idAlumno=" + idAlumno;
                json = cargarArchivoPost("Default.aspx", params);
                try {
                    if (json !== "") {
                        var catchText = JSON.parse(json);
                    }
                    Alumnos.splice(index, 1);
                    Screen0.PantallaAlumnos(Provincias);
                    swal("Listo!", "Se eliminó el Alumno correctamente!", "success");
                }
                catch (e) {
                    swal("Oops...", "Hubo un error al eliminar los datos. Por favor, intente nuevamente más tarde...", "error");
                }
            }
        });
    };
    // Abre la ventana modal de edición de alumno y la carga de los datos respectivos.
    this.EditarAlumno = function (index) {
        Screen0.ModalEditarA(Alumnos[index], index, Provincias);
    };
    // Modifica el alumno mediante una solicitud AJAX al servidor. Realiza las mismas validaciones que cuando se agrega.
    this.ModificarAlumno = function (idAlumno, index) {
        var nombre = document.getElementById('TBENombreA').value;
        var dni = document.getElementById('TBEDNIA').value;
        if (nombre === "") {
            return swal("Oops...", "El nombre no debe ser borrado!", "error");
        }
        var regex = new RegExp("^[a-zA-ZñÑ\'áéíóúÁÉÍÓÚ]+( [a-zA-ZñÑ\'áéíóúÁÉÍÓÚ]+)*$");
        if (!regex.test(nombre)) {
            return swal("Oops...", "El campo 'Nombre' debe contener solo letras, separando cada palabra con un espacio.", "error");
        }
        if (dni === 0 || dni === "") {
            return swal("Oops...", "El D.N.I. no debe ser borrado!", "error");
        }
        regex = new RegExp("^([0-9]){8}?$");
        if (!regex.test(dni)) {
            return swal("Oops...", "El campo 'DNI' debe contener solo ocho números enteros positivos.", "error");
        }
        var tbGrupo = document.getElementById('CBEGrupoA');
        var grupo = tbGrupo.options[tbGrupo.selectedIndex].value;
        params = "Accion=ModificarAlumno&nombre=" + nombre + "&dni=" + dni + "&idGrupo=" + grupo + "&idAlumno=" + idAlumno;
        json = cargarArchivoPost("Default.aspx", params);
        try {
            var Alumno = JSON.parse(json);
            Alumno.Grupo = BuscarProvincia(Alumno);
            Alumnos[index] = Alumno;
            Screen0.PantallaAlumnos(Provincias);
            swal("Listo!", "Los datos fueron guardados correctamente!", "success");
        }
        catch (e) {
            swal("Oops...", "Hubo un error al guardar los datos. Por favor, intente nuevamente más tarde...", "error");
        }
    };
    // Muestra en pantalla un mensaje con la clave del grupo del alumno.
    this.MostrarClave = function (idGrupo) {
        params = "Accion=BuscarUsuario&idGrupo=" + idGrupo;
        json = cargarArchivoPost("Default.aspx", params);
        try {
            var Usuario = JSON.parse(json);
            swal(Usuario.Nombre, "Contraseña: " + Usuario.Clave);
        } catch (e) {
            swal("Oops...", "Hubo un problema al intentar buscar el usuario. Por favor, intente nuevamente más tarde.", "error");
        }
    };
    // Genera un PDF con el listado de los alumnos cargados en el sistema con su respectiva nota y grupo.
    // Luego muestra el PDF en un iframe dentro de una ventana modal.
    this.VerNotas = function () {
        var doc = new jsPDF();
        var filas = 25;
        var pages = Math.ceil(Alumnos.length / filas);
        var currentPage = 1;
        var y = 40;
        var incremento = 6;
        var date = new Date();
        doc.setProperties({
            title:   'Calificaciones - Proyecto Saint Jean Provincias',
            subject: 'Lista de Calificaciones',
            creator:  'Jonathan Martinez'
        });
        doc.setFontSize(12);
        doc.setFont("times");
        doc.setFontType("italic");
        doc.text(5, 10, "Proyecto - Saint Jean Provincias");
        doc.text(180, 10, date.getDate() + "/" + date.getMonth() + "/" + date.getFullYear());
        doc.text(170, 280, "Página " + currentPage + " de " + pages);
        doc.setFontSize(22);
        doc.text(70, 25, "Listado de Calificaciones");
        doc.setFont("times");
        doc.setFontType("normal");
        doc.setFontSize(14);
        for (var i = 0; i < Alumnos.length; i++) {
            if (i % filas === 0 && i !== 0) {
                filas = 30;
                y = 25;
                doc.addPage();
                currentPage++;
                doc.setFontSize(12);
                doc.setFont("times");
                doc.setFontType("italic");
                doc.text(5, 10, "Proyecto - Saint Jean Provincias");
                doc.text(180, 10, date.getDate() + "/" + date.getMonth() + "/" + date.getFullYear());
                doc.text(170, 280, "Página " + currentPage + " de " + pages);
                doc.setFont("times");
                doc.setFontType("normal");
                doc.setFontSize(14);
            }
            doc.text(20, y, "Alumno " + Alumnos[i].Nombre + " del Grupo " + Alumnos[i].Grupo.Nombre + ": " + Alumnos[i].Grupo.Nota);
            y += incremento;
        }
        Screen0.ModalPDF(doc);
    };
    // realiza validaciones de contraseña si es que se quiere cambiar. Una vez se chequea todo, se envía el formulario al servidor.
    // Luego el iframe recibe la respuesta y carga la imagen nueva (si se cargó una). En el caso de que se haya modificado la contraseña
    // avisa que en el próximo inicio de sesión deberás de loguearte con la nueva contraseña, en caso contrario, solo avisa que los cambios
    // fueron realizados correctamente.
    this.EditarPerfil = function () {
        var claveActual = document.getElementById('TBClaveActual');
        var claveNueva = document.getElementById('TBClaveNueva');
        var claveNueva2 = document.getElementById('TBClaveNueva2');
        var fotoPerfil = document.getElementById('TBFotoUsuario');
        if (claveNueva.value !== "" || claveNueva2.value !== "") {
            if (claveActual.value === "") {
                claveNueva.value = "";
                claveNueva2.value = "";
                return swal("Oops...", "Debe ingresar su clave actual para poder modificarla.", "error");
            }
        }
        var cambioDeClave = false;
        if (claveActual.value !== "") {
            if (claveActual.value === Usuario.Clave) {
                if (claveNueva.value === "" || claveNueva2.value === "") {
                    return swal("Espera!", "Debes ingresar la contraseña nueva...", "error");
                }
                if (claveNueva.value !== claveNueva2.value) {
                    return swal("Espera!", "Las contraseñas deben coincidir...", "error");
                }
                cambioDeClave = true;
            }
            else {
                claveActual.value = "";
                claveNueva.value = "";
                claveNueva2.value = "";
                return swal("Espera!", "La contraseña actual no coincide con la ingresada!", "error");
            }
        }
        else {
            claveNueva.value = "";
            claveNueva2.value = "";
        }
        if (claveNueva2.value === "" && fotoPerfil.value === "") {
            return swal("(...)", "No realizó cambios.");
        }
        var iframe = document.getElementById('iframeEPerfil');
        iframe.onload = function () {
            json = document.getElementById('iframeEPerfil').contentWindow.document.body.innerHTML;
            try {
                Usuario = JSON.parse(json);
                $('#IMGIconoPerfil').attr('src', Usuario.Foto);
                if (cambioDeClave === true) {
                    swal("Bien Hecho!", "Los cambios fueron guardados correctamente! La próxima vez que inicies sesión deberás hacerlo con tu nueva contraseña.", "success");
                }
                else {
                    swal("Bien Hecho!", "Los cambios fueron guardados correctamente!", "success");
                }
            }
            catch (e) {
                swal("Oops...", json, "error");
            }
        };
        document.getElementById('formEPerfil').submit();
        claveActual.value = "";
        claveNueva.value = "";
        claveNueva2.value = "";
        fotoPerfil.value = "";
    };
    // Carga la imagen de usuario dinámicamente en pantalla cuando se selecciona una nueva imagen dentro de la edición de perfil.
    this.CargarImagenUsuario = function () {
        $(document).on('change', '#TBFotoUsuario', function (e) {
            var TmpPath = URL.createObjectURL(e.target.files[0]);
            $('#IMGPerfil').attr('src', TmpPath);
        });
    };
}