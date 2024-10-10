var contador = 0;
var contador_ori = 0;

function fnestablecer(xcontador) {
    contador = xcontador;
    contador_ori = xcontador;
    fncontador();
}


function fncontador() {
    if (contador != 0) {
        contador = contador - 1;
        var texto = "<br/><b>Por tu seguridad, se cerrará sesión luego de " + contador_ori + " segundos de inactividad.</b><br/>"
        texto += " Se cerrará sesión en " + contador + " segundos. ";
        $("#contadortiempo").html(texto);
        if (contador == 0) {
            cerraExpirado();
        }
        setTimeout("fncontador()", 1000);//cada segundo
    }
}

function fnresetcontador() {
    contador = contador_ori;
    var texto = "<br/><b>Por tu seguridad, se cerrará sesión luego de " + contador_ori + " segundos de inactividad.</b><br/>"
    texto += " Se cerrará sesión en " + contador + " segundos. ";
    $("#contadortiempo").html(texto);
}


