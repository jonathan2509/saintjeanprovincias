function AddTable(idContenedor, elementos, columnas, filas, sufijo, cantFilas, controles) {
    var contenedor = document.getElementById(idContenedor);
    var pos0 = 0;
    var length = elementos.length;
    var cant = cantFilas;
    var Max = length - cant;
    construirTabla();
    function construirTabla() {
        var html = "";
        html += "<table class='table table-hover table-primary table-responsive-sm'>";
        html += columnas;
        html += filas(pos0, cant, elementos);
        html += "</table>";
        contenedor.innerHTML = html.replace("ñ", "&ntilde;");
        if (controles === true) {
            detectarControles();
        }
    }
    function detectarControles() {
        var BTStart = document.getElementById('BTStart' + sufijo);
        var BTPrevious = document.getElementById('BTPrevious' + sufijo);
        var BTNext = document.getElementById('BTNext' + sufijo);
        var BTEnd = document.getElementById('BTEnd' + sufijo);
        if (pos0 + cant >= length) {
            BTStart.style.display = "none";
            BTPrevious.style.display = "none";
            BTNext.style.display = "none";
            BTEnd.style.display = "none";
        }
        else {
            BTStart.style.display = "none";
            BTPrevious.style.display = "none";
            BTNext.style.display = "block";
            BTEnd.style.display = "block";
        }
        if (pos0 > 0) {
            BTStart.style.display = "block";
            BTPrevious.style.display = "block";
        }
        if (pos0 >= length - cant && length > cant) {

            BTStart.style.display = "block";
            BTPrevious.style.display = "block";
            BTNext.style.display = "none";
            BTEnd.style.display = "none";
        }
        BTPrevious.onclick = function () {
            pos0 -= cant; if (pos0 < 0) pos0 = 0;
            construirTabla();
        };
        BTStart.onclick = function () {
            pos0 = 0;
            construirTabla();
        };
        BTEnd.onclick = function () {
            pos0 = length - cant;
            construirTabla();
        };
        BTNext.onclick = function () {
            pos0 += cant; if (pos0 > length - cant) pos0 = length - cant;
            construirTabla();
        };
        onMouseWheel(contenedor, fDelta);
        function fDelta(delta) {
            if (delta > 0) {
                pos0--;
                if (pos0 < 0) pos0 = 0;
            }
            else { if (pos0 < Max) pos0++; }
            construirTabla();
        }
    }
}
function ListarProductosRegionales() {
    var elementos = Event0.ListarProductosRegionales();
    var idContenedor = "tablaProductosRegionales";
    var sufijo = "PR";
    var cantFilas = 6;
    var controles = true;
    var filas = function (pos0, cant, elementos) {
        var filas = "";
        filas += "                <tbody>";
        for (var i = pos0; i < elementos.length && i < pos0 + cant; i++) {
            filas += "				    <tr>";
            filas += "				      <td>" + elementos[i].Nombre + "</td>";
            filas += "				      <td>";
            filas += "				      	<button type='button' class='btn btn-default' onclick='Event0.EditarProductoRegional(" + i + ");' data-toggle='modal' data-target='#VModalEditarPR'>";
            filas += "				      	  <img src='Imagenes/Iconos/edit.png' class='img-fluid' style='height: 1rem;'>";
            filas += "						</button>";
            filas += "				      </td>";
            filas += "				      <td>";
            filas += "				      	<button type='button' class='btn btn-default' onclick='Event0.EliminarProductoRegional(" + elementos[i].ID + ", " + i + ");'>";
            filas += "						  <img src='Imagenes/Iconos/erase.png' class='img-fluid' style='height: 1rem;'>";
            filas += "						</button>";
            filas += "				      </td>";
            filas += "				      <td>";
            filas += "				      	<button type='button' class='btn btn-default' onclick='Event0.VerProductoRegional(" + i + ");' data-toggle='modal' data-target='#VModalImagen'>";
            filas += "						  <img src='Imagenes/Iconos/photo.png' class='img-fluid' style='height: 1rem;'>";
            filas += "						</button>";
            filas += "				      </td>";
            filas += "				    </tr>";
        }
        filas += "				    <tr class='text-center'>";
        filas += "				      <td colspan='4'>";
        filas += "                      <div class='form-inline justify-content-center'>";
        filas += "				      	  <button id='BTStartPR' class='btn btn-info shadow-sm mx-2'>&lt;&lt;</button>";
        filas += "				      	  <button id='BTPreviousPR' class='btn btn-info shadow-sm'>&lt;</button>";
        filas += "				      	  <button id='BTNextPR' class='btn btn-info shadow-sm mx-2'>&gt;</button>";
        filas += "				      	  <button id='BTEndPR' class='btn btn-info shadow-sm'>&gt;&gt;</button>";
        filas += "                      </div>";
        filas += "				      </td>";
        filas += "				    </tr>";
        filas += "                </tbody>";
        return filas;
    };
    var columnas = "";
    columnas += "				  <thead>";
    columnas += "				    <tr>";
    columnas += "				      <th scope='col'>Nombre</th>";
    columnas += "				      <th scope='col'>Editar</th>";
    columnas += "				      <th scope='col'>Eliminar</th>";
    columnas += "				      <th scope='col'>Ver Imagen</th>";
    columnas += "				    </tr>";
    columnas += "				  </thead>";
    AddTable(idContenedor, elementos, columnas, filas, sufijo, cantFilas, controles);
}
function ListarPuntosDeInteres() {
    var elementos = Event0.ListarPuntosDeInteres();
    var idContenedor = "tablaPuntosDeInteres";
    var sufijo = "PI";
    var cantFilas = 6;
    var controles = true;
    var filas = function (pos0, cant, elementos) {
        var filas = "";
        filas += "                <tbody>";
        for (var i = pos0; i < elementos.length && i < pos0 + cant; i++) {
            filas += "				    <tr>";
            filas += "				      <td>" + elementos[i].Nombre + "</td>";
            filas += "				      <td>";
            filas += "				      	<button type='button' class='btn btn-default' onclick='Event0.EditarPuntoDeInteres(" + i + ");' data-toggle='modal' data-target='#VModalEditarPI'>";
            filas += "				      	  <img src='Imagenes/Iconos/edit.png' class='img-fluid' style='height: 1rem;'>";
            filas += "						</button>";
            filas += "				      </td>";
            filas += "				      <td>";
            filas += "				      	<button type='button' class='btn btn-default' onclick='Event0.EliminarPuntoDeInteres(" + elementos[i].ID + ", " + i + ");'>";
            filas += "						  <img src='Imagenes/Iconos/erase.png' class='img-fluid' style='height: 1rem;'>";
            filas += "						</button>";
            filas += "				      </td>";
            filas += "				      <td>";
            filas += "				      	<button type='button' class='btn btn-default' onclick='Event0.VerPuntoDeInteres(" + i + ");' data-toggle='modal' data-target='#VModalImagen'>";
            filas += "						  <img src='Imagenes/Iconos/photo.png' class='img-fluid' style='height: 1rem;'>";
            filas += "						</button>";
            filas += "				      </td>";
            filas += "				    </tr>";
        }
        filas += "				    <tr class='text-center'>";
        filas += "				      <td colspan='4'>";
        filas += "                      <div class='form-inline justify-content-center'>";
        filas += "				      	  <button id='BTStartPI' class='btn btn-info shadow-sm mx-2'>&lt;&lt;</button>";
        filas += "				      	  <button id='BTPreviousPI' class='btn btn-info shadow-sm'>&lt;</button>";
        filas += "				      	  <button id='BTNextPI' class='btn btn-info shadow-sm mx-2'>&gt;</button>";
        filas += "				      	  <button id='BTEndPI' class='btn btn-info shadow-sm'>&gt;&gt;</button>";
        filas += "                      </div>";
        filas += "				      </td>";
        filas += "				    </tr>";
        filas += "                </tbody>";
        return filas;
    };
    var columnas = "";
    columnas += "				  <thead>";
    columnas += "				    <tr>";
    columnas += "				      <th scope='col'>Nombre</th>";
    columnas += "				      <th scope='col'>Editar</th>";
    columnas += "				      <th scope='col'>Eliminar</th>";
    columnas += "				      <th scope='col'>Ver Imagen</th>";
    columnas += "				    </tr>";
    columnas += "				  </thead>";
    AddTable(idContenedor, elementos, columnas, filas, sufijo, cantFilas, controles);
}
function ListarAlumnos() {
    var elementos = Event0.ListarAlumnos();
    var idContenedor = "tablaAlumnos";
    var sufijo = "";
    var cantFilas = 0;
    var controles = false;
    var filas = function (pos0, cant, elementos) {
        var filas = "";
        filas += "                <tbody id='tbodyAlumnos'>";
        for (var i = 0; i < elementos.length; i++) {
            filas += "		    <tr>";
            filas += "		      <td>" + elementos[i].Nombre + "</td>";
            filas += "		      <td>" + elementos[i].DNI + "</td>";
            filas += "		      <td>" + elementos[i].Grupo.Nombre + "</td>";
            filas += "		      <td>";
            filas += "		      	<button type='button' class='btn btn-default' data-toggle='modal' data-target='#VModalEAlumno' onclick='Event0.EditarAlumno(" + i + ");' >";
            filas += "		      	  <img src='Imagenes/Iconos/edit.png' class='img-fluid' style='height: 1rem;'>";
            filas += "				</button>";
            filas += "		      </td>";
            filas += "		      <td>";
            filas += "		      	<button type='button' class='btn btn-default' onclick='Event0.EliminarAlumno(" + elementos[i].ID + ", " + i + ")'>";
            filas += "				  <img src='Imagenes/Iconos/erase.png' class='img-fluid' style='height: 1rem;'>";
            filas += "				</button>";
            filas += "		      </td>";
            filas += "		      <td>";
            filas += "		      	<button type='button' class='btn btn-default' onclick='Event0.MostrarClave(" + elementos[i].Grupo.ID + ")' >";
            filas += "				  <img src='Imagenes/Iconos/view.png' class='img-fluid' style='height: 1rem;'>";
            filas += "				</button>";
            filas += "		      </td>";
            filas += "		    </tr>";
        }
        filas += "                </tbody>";
        return filas;
    };
    var columnas = "";
    columnas += "		  <thead>";
    columnas += "		    <tr>";
    columnas += "		      <th scope='col'>Nombre Completo</th>";
    columnas += "		      <th scope='col'>D.N.I.</th>";
    columnas += "		      <th scope='col'>Grupo</th>";
    columnas += "		      <th scope='col'>Editar</th>";
    columnas += "		      <th scope='col'>Eliminar</th>";
    columnas += "		      <th scope='col'>Ver Contraseña</th>";
    columnas += "		    </tr>";
    columnas += "		  </thead>";
    AddTable(idContenedor, elementos, columnas, filas, sufijo, cantFilas, controles);
}