



var encontrado = document.getElementById('RecibeData');


function buscarelemento(a) {

    if (encontrado != null) {
        return true;
    }
    else { return false;}

}



function cargarhtm(dato) {

    if (encontrado != null) {
        document.getElementById("RecibeData").innerHTML = JSON.stringify(dato);
    }




}