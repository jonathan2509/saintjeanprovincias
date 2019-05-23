// Anula la actualización con la tecla F5.
function anularF5() {
    document.onkeydown = function () {
        if (window.event && window.event.keyCode === 116) return false;
    };
}
// Captura el movimiento de la rueda del ratón.
function onMouseWheel(element, fDelta) {
    if (element.addEventListener) element.addEventListener('DOMMouseScroll', wheel, false);
    element.onmousewheel = wheel;
    function wheel(event) {
        var delta = 0;
        if (!event) {
            event = window.event;
        }
        if (event.wheelDelta) {
            delta = event.wheelDelta / 120;
        }
        else if (event.detail) {
            delta = -event.detail / 3;
        }
        if (delta)
            fDelta(delta);
        if (event.preventDefault)
            event.preventDefault();
        event.returnValue = false;
    }
}
// AJAX.
var xhttp = new XMLHttpRequest();
var p;
xhttp.onreadystatechange = function () {
    if (xhttp.readyState === 4) {
        if (xhttp.status === 200) {
            p = xhttp.responseText;
        }
        else if (xhttp.status === 500) {
            swal("Error 500.", "Es posible que no pueda realizar ninguna acción. Estamos trabajando para mejorar y corregir el sistema.", "error");
        }
        else if (xhttp.status === 400) {
            swal("Error 400.", "Nuestro sitio se encuentra bajo mantenimiento. Por favor, vuelva a intentarlo más tarde.");
        }
    }
};
function cargarArchivo(ArchName, container) {

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            if (xhttp.status === 200)
                container.innerHTML = xhttp.responseText;
            return;
        }
    };
    xhttp.open("GET", ArchName, true);
    xhttp.send();
}
function cargarArchivoPost(archNombre, params) {
    xhttp.open("POST", archNombre, false);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.send(params);
    return p;
}