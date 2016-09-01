
//Fecha actual en los date 
document.getElementById('fechaFinal').valueAsDate = new Date();
document.getElementById('fechaInicial').valueAsDate = new Date();




//Valida input numérico
function valida(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }
    // Patron de entrada, en este caso solo acepta numeros
    patron = /[0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}


//Aparecer o desaparecer el div de filtrar si es internet
function filtraOnChange(sel) {
    if (sel.value == "1") {
        divT = document.getElementById("esinternet");
        divT.style.display = "none"; //no muestra
        document.getElementById("soloInternet").checked = false;//checkbox se reestablece a no seleccionado

    } else {
        divT = document.getElementById("esinternet");
        divT.style.display = ""; //muestra
    }
}



function functionAceptar(idReporte, numModal, idModal) {

    // CABLE (1) O INTERNET (2)
    var lista = document.getElementById("listaTipoServicio"); //select id = listaTipoServicio 
    var op = lista.options[lista.selectedIndex].value;   // Obtener el valor de la opción seleccionada

    //VALIDA QUE EL SELECT DESTINO NO ESTÁ VACÍO
    if (numModal == 1) {
        var destino = "destinoClientes"; //select a validar

    }
    else if (numModal == 2) {
        var destino = "destinoServicios"; //select a validar
    }
        //else if (numModal == 3) {
        //    var destino = "destinoCiudades"; //select a validar
        //}
    else if (numModal == 4) {
        var destino = "destinoColonias"; //select a validar
    }
    else if (numModal == 6) {
        var destino = "destinoPeriodo"; //select a validar       
    }
    else if (numModal == 11) {
        var destino = "destinoCalles"; //select a validar
    }//CHECKBOX 
    else if (numModal == 5) {
        var ele1 = document.getElementById("conTel");
        var ele2 = document.getElementById("sinTel");  //id checkbox
    }
    else if (numModal == 7) {
        var ele1 = document.getElementById("ejecutada");
        var ele2 = document.getElementById("pendiente");  //id checkbox
    }
    else if (numModal == 12) {

        var ele1 = document.getElementById("contratado");
        var ele2 = document.getElementById("fuera");
        var ele3 = document.getElementById("suspendidos");
        var ele4 = document.getElementById("instalados");
        var ele5 = document.getElementById("desconectado");
        var ele6 = document.getElementById("cancelados");
        var ele7 = document.getElementById("suspendidosTempo");
        var ele8 = document.getElementById("ocultarMostrar");
    }
    else if (numModal == 8) {

        //$(idModal).modal('hide');
        cancelar(numModal, idModal);  //Llama a la función para que reinicie el modal cada vez que se presiona 'aceptar'
        secuenciaModal(idReporte, numModal);

    }
    if (numModal == 12) {
        //Si ningún checkbox está seleccionado
        if (ele1.checked == false && ele2.checked == false && ele3.checked == false && ele4.checked == false && ele5.checked == false && ele6.checked == false && ele7.checked == false) //checkbox
        {
            alert('seleccione alguna opción');
        }

        if (ele8.checked == true) //si el div está habilitado, verificar que los campos estén seleccionados
        {
            var valor = document.getElementById("anioC").value; //en modal estatusCliente

            document.getElementById("txtEscondido").innerHTML = valor;

            if (valor == '') {
                alert("Escriba un año");
            }
        }

        //al menos un checkbox seleccionado      
        if ((ele1.checked == true || ele2.checked == true || ele3.checked == true || ele4.checked == true
            || ele5.checked == true || ele6.checked == true || ele7.checked == true) && ele8.checked == false) //checkbox
        {

            //alert("pasa al siguiente modal nombre modal: " + idModal);
            //$(idModal).modal('hide');
            cancelar(numModal, idModal);//Llama a la función para que reinicie el modal cada vez que se presiona 'aceptar'
            secuenciaModal(idReporte, numModal);

            // enviarDatos(numModal);         

        }
        else if ((ele1.checked == true || ele2.checked == true || ele3.checked == true || ele4.checked == true
            || ele5.checked == true || ele6.checked == true || ele7.checked == true) && ele8.checked == true && valor != '') //checkbox
        {
            //alert("pasa al siguiente modal nombre modal: " + idModal);
            //$(idModal).modal('hide');
            cancelar(numModal, idModal);  //Llama a la función para que reinicie el modal cada vez que se presiona 'aceptar'
            secuenciaModal(idReporte, numModal);

            // enviarDatos(numModal);            
        }

    }
    else if (numModal == 5 || numModal == 7) {
        if (ele1.checked == false && ele2.checked == false) //checkbox
        {
            alert('Seleccione al menos una opción')
        }
        else if (ele1.checked == true || ele2.checked == true) //si la lista select no está vacía llama a la función 
        {
            //$(idModal).modal('hide');
            cancelar(numModal, idModal);  //Llama a la función para que reinicie el modal cada vez que se presiona 'aceptar'
            // enviarDatos(numModal);
            if (numModal == 7) {
                //enviar los datos
                enviarDatos();
            }

            secuenciaModal(idReporte, numModal);
        }

    }
    else if (numModal >= 1 && numModal <= 4 || numModal == 6 || numModal == 7 || numModal == 11) //select
    {
        var bandera = 0; //select vacío inicialmente
        var lista = document.getElementById(destino); //destino=select destino

        //SI HAY ELEMENTOS EN EL SELECT, ENTRA AL FOR
        for (var i = 0; i < lista.length; i++) {
            //var texto = lista.options[i].text //Obtener texto de todas las opciones en la lista destino
            //var valor = lista.options[i].value //Obtener valor de todas las opciones en la lista destino
            //alert("texto= " + texto + ", value = " + valor)
            bandera = 1; //al entrar al for, la lista no está vacía
            //alert('LA LISTA NO ESTÁ VACÍA');
        }
    }
    if (bandera == 0)//Si la lista select está vacía
    {
        if (numModal == 1) {
            alert('Seleccione al menos un cliente')
        }
        if (numModal == 2) {
            alert('Seleccione al menos un servicio')
        }
        //if (numModal == 3) {
        //    alert('Seleccione al menos una ciudad')
        //}
        if (numModal == 4) {
            alert('Seleccione al menos una colonia')
        }

        if (numModal == 6) {
            alert('Seleccione al menos un periodo')
        }

        if (numModal == 7) {
            alert('Seleccione al menos un estatus')
        }

        if (numModal == 11) {
            alert('Seleccione al menos una calle')
        }
    }
    else if (bandera == 1 && numModal == 4) {
        //Envía los IdColonias;
        cancelar(numModal, idModal);//Llama a la función para que reinicie el modal cada vez que se presiona 'aceptar'
        secuenciaModal(idReporte, numModal);
    }
    else if (bandera == 1 && numModal != 6 || bandera == 1 && numModal == 6 && idReporte != 5) //si solo hay un filtro
    {

        cancelar(numModal, idModal);  //Llama a la función para que reinicie el modal cada vez que se presiona 'aceptar'


        if (numModal == 6 && idReporte != 0) {
            enviarDatos();
        }

        if (numModal == 6 && idReporte == 0 && op == 2) {
            enviarDatos();
        }

        secuenciaModal(idReporte, numModal);
    }
        //else if (bandera == 1 && numModal == 6 && idReporte != 5)
        //{
        //    $(idModal).modal('hide');
        //    secuenciaModal(idReporte, numModal);
        //}
    else if (bandera == 1 && numModal == 6 && idReporte == 5) {
        //revisar que el text anioCP tengá un valor
        var valorp = document.getElementById("anioCP").value; //en modal estatusCliente
        //document.getElementById("txtEscondido").innerHTML = valor;
        if (valorp == '') {
            alert("Escriba un año");
        }
        else //si pasa todos los filtros
        {
            //$(idModal).modal('hide');
            cancelar(numModal, idModal);  //Llama a la función para que reinicie el modal cada vez que se presiona 'aceptar'
            enviarDatos(); //se envian datos al controlador ReportesVarios
            secuenciaModal(idReporte, numModal);

        }


    }
}

// Función para dejar modales en blanco y resetearlos
function cancelar(numModal, idModal) {


    if (numModal == 1) {
    }
    $('#tipoCliente').modal('hide');

    if (numModal == 2) {
        $('#tipoServicio').modal('hide');
    }
    //if (numModal == 3) {
    //    $('#ciudades').modal('hide');
    //}
    if (numModal == 4) {
        $('#colonias').modal('hide');
    }
    if (numModal == 5) {
        $('#telefono').modal('hide');
    }
    if (numModal == 6) {
        $('#periodo').modal('hide');
    }
    if (numModal == 7) {
        $('#estatusOrden').modal('hide');
    }
    if (numModal == 8) {
        $('#rangoFechas').modal('hide');
    }
    if (numModal == 11) {
        $('#calles').modal('hide');
    }
    if (numModal == 12) {
        $('#estatusCliente').modal('hide');
    }

    //Modal 1, vacía los dos select 
    document.getElementById("destinoClientes").options.length = 0;
    document.getElementById("origenClientes").options.length = 0;

    //Modal 2, vacía los select
    document.getElementById("destinoServicios").options.length = 0;
    document.getElementById("origenServicios").options.length = 0;
    //$('#origenServicios').append('<option value="0">Servicio Básico </option>'); //ELIMINAR CUANDO SERVICIO FUNCIONE

    //document.getElementById("destinoCiudades").options.length = 0;
    //document.getElementById("origenCiudades").options.length = 0;
    //$('#origenCiudades').append('<option value="0">Mérida</option>');

    document.getElementById("destinoColonias").options.length = 0;
    document.getElementById("origenColonias").options.length = 0;

    document.getElementById("conTel").checked = false;
    document.getElementById("sinTel").checked = false;

    document.getElementById("destinoPeriodo").options.length = 0;
    document.getElementById("origenPeriodo").options.length = 0;
    $('#xmlComoCadena').val(''); //hidden, contiene la cadena xml que se envía al controlador


    document.getElementById("ejecutada").checked = false;
    document.getElementById("pendiente").checked = false;

    //numModal == 8
    document.getElementById('fechaFinal').valueAsDate = new Date();
    document.getElementById('fechaInicial').valueAsDate = new Date();
    $('#listaMotivoCancelacion').val($('#listaMotivoCancelacion > option:first').val());

    document.getElementById("destinoCalles").options.length = 0;
    document.getElementById("origenCalles").options.length = 0;

    document.getElementById("listaMotivoCancelacion").options.length = 0;
    document.getElementById("listaMotivoCancelacion12").options.length = 0;

    document.getElementById("contratado").checked = false;
    document.getElementById("fuera").checked = false;
    document.getElementById("suspendidos").checked = false;
    document.getElementById("instalados").checked = false;
    document.getElementById("desconectado").checked = false;
    document.getElementById("cancelados").checked = false;
    document.getElementById("suspendidosTempo").checked = false;
    document.getElementById("ocultarMostrar").checked = false;

    $('#listaMotivoCancelacion12').attr('disabled', true);
    $('#listaMotivoCancelacion12').val($('#listaMotivoCancelacion12 > option:first').val());
    $('#listaMesQueAdeuda').attr('disabled', true);
    $('#listaMesQueAdeuda').val($('#listaMesQueAdeuda > option:first').val());
    $('#listaMenorIgual').attr('disabled', true);
    $('#listaMenorIgual').val($('#listaMenorIgual > option:first').val());
    $('#anioC').val('');
    $('#anioC').attr('disabled', true);
    $('#anioCP').val('');

}




function cableInternet() {
    //REVISA SI EL TIPO DE SERVICIO ES CABLE (1) O INTERNET (2) y retorna su value
    var lista = document.getElementById("listaTipoServicio"); //select id = listaTipoServicio 
    var valorSeleccionado = lista.options[lista.selectedIndex].value;   // Obtener el valor de la opción seleccionada

    return (valorSeleccionado);
}



function secuenciaModal(idReporte, numModal) { //recibe el id o value del reporte 

    //desaparecer el div
    divT = document.getElementById("mesP");
    divT.style.display = "none";
    divT1 = document.getElementById("anioP");
    divT1.style.display = "none";
    //desaparecer el div
    divT2 = document.getElementById("motivoCancelacion"); //modal fechaRangos
    divT2.style.display = "none";
    $("#listaMesP").val("1").change();  //Establecer el primer valor como seleccionado


    //// REVISA SI EL TIPO DE SERVICIO ES CABLE (1) O INTERNET (2)
    var tipoServicioSeleccionado = cableInternet();

    // DEPENDIENDO DEL REPORTE Y MODAL, REALIZA LA SECUENCIA DE MODALES
    if (numModal == 0) {
        $('#idReporte1').val(idReporte);
        cargarDatos(1);
        $('#tipoCliente').modal('show'); //nombre del modal
        //muestra los datos
    }
    else if (numModal == 1) {
        //document.write(idReporte + " 1");
        $('#idReporte1').val(idReporte);
        cargarDatos(2);
        $('#tipoServicio').modal('show');
    }
    else if (numModal == 2) {
        cargarDatos(4);
        $('#colonias').modal('show'); //todos van al modal 4: Colonias
    }

    else if (idReporte == 0) { //reporte Desconectados

        if (numModal == 4) {
            $('#telefono').modal('show'); // 5
        }
        else if (numModal == 5) {
            cargarDatos(6);
            $('#periodo').modal('show'); //6
        }
        else if (numModal == 6 && tipoServicioSeleccionado == 1) {
            $('#estatusOrden').modal('show'); //abre el modal 7
        }
    }
    else if (idReporte == 1) { // reporte Suspendidos

        if (numModal == 4) {
            $('#telefono').modal('show'); // 5
        }
        else if (numModal == 5) {
            cargarDatos(6);
            $('#periodo').modal('show'); //6
            //enviar todos los datos
        }
    }

    else if (idReporte == 2 || idReporte == 3 || idReporte == 8) {
        if (numModal == 4) {
            cargarDatos(6);
            $('#periodo').modal('show');
        }
    }
    else if (idReporte == 4 || idReporte == 6) {
        if (numModal == 4) {
            $('#rangoFechas').modal('show'); //al 8
        }
        if (numModal == 8) {
            cargarDatos(6);
            $('#periodo').modal('show'); //al 6
        }
    }
    else if (idReporte == 10) //10: paquetes de cortesía
    {
        if (numModal == 4) {
            cargarDatos(6);
            $('#periodo').modal('show'); //al 6
        }
    }
    else if (idReporte == 9) {
        if (numModal == 4) {
            $('#rangoFechas').modal('show'); //al 8
        }
        //else if (numModal == 4 && tipoServicioSeleccionado == 2) //inter
        //{
        //    divT2 = document.getElementById("motivoCancelacion"); //modal fechaRangos
        //    divT2.style.display = "";
        //    cargarDatos(8);
        //    $('#rangoFechas').modal('show'); //al 8
        //}
        if (numModal == 8) {

            cargarDatos(6);
            $('#periodo').modal('show'); //al 6
        }
    }
    else if (idReporte == 5) { // reporte Por pagar
        if (numModal == 4) {

            $('#telefono').modal('show'); //al 5    
        }
        if (numModal == 5) {
            //mostrar el div
            divT = document.getElementById("mesP");
            divT.style.display = "";
            divT1 = document.getElementById("anioP");
            divT1.style.display = "";
            cargarDatos(6);
            $('#periodo').modal('show');  //va al 6
        }
    }

    else if (idReporte == 7) { //reporte Cancelaciones
        if (numModal == 4) {//mostrar div
            divT2 = document.getElementById("motivoCancelacion"); //modal fechaRangos
            divT2.style.display = "";
            cargarDatos(8);
            $('#rangoFechas').modal('show'); //al 8
        }
        if (numModal == 8) {
            cargarDatos(6);
            $('#periodo').modal('show'); //al 6
        }
    }
        //idReporte = 11 ó idReporte = 12 usan modal Ciudad 
    else if (idReporte == 11) { // reporte Ciudad
        if (numModal == 4) {
            cargarDatos(11);
            $('#calles').modal('show'); //al 11
        }
        if (numModal == 11) {
            //mostrar el botón
            divT = document.getElementById("imprimirEtiquetas");
            divT.style.display = "";
            cargarDatos(12);


            $('#estatusCliente').modal('show');  //va al 12
        }
        if (numModal == 12) {
            cargarDatos(6);
            $('#periodo').modal('show'); // va al 6
        }
    }

    else if (idReporte == 12) { // reporte Ciudad
        if (numModal == 4) {
            cargarDatos(11);
            $('#calles').modal('show'); //al 11
        }
        if (numModal == 11) {
            //desaparecer el botón  imprimir etiquetas
            divT = document.getElementById("imprimirEtiquetas");
            divT.style.display = "none";
            cargarDatos(12);
            $('#estatusCliente').modal('show');  //va al 12
        }
        if (numModal == 12) {
            cargarDatos(6);
            $('#periodo').modal('show'); // va al 6
        }
    }

}

function checar() {
    //muestra u oculta los div dependiendo si está marcado el check
    //check 1
    if ($('#ocultarMostrar').is(':checked')) {
        $('#ocultarMostrarDiv :input').removeAttr('disabled');
    } else {
        $('#ocultarMostrarDiv :input').attr('disabled', true);
    }

    //check 2
    if ($('#cancelados').is(':checked')) {  //
        $('#cancelacionDiv :input').removeAttr('disabled');
    } else {
        $('#cancelacionDiv :input').attr('disabled', true);
    }
}


//  Recibe el id del select al que hace referencia, y ordena sus elementos alfabeticamente
function ordenarAlfa(id_componente) {
    var selectToSort = jQuery('#' + id_componente);
    var optionActual = selectToSort.val();
    selectToSort.html(selectToSort.children('option').sort(function (a, b) {
        return a.text === b.text ? 0 : a.text < b.text ? -1 : 1;
    })).val(optionActual);
}



function ordenDesorden(id_componente) {

    if (document.getElementById("ordenBtn")) // primera acción
    {
        //alert('primera accion');
        ordenarAlfa(id_componente);
        document.getElementById("ordenBtn").id = "desordenBtn";
    } else // segunda acción
    {
        var selectList = $('#reportes option');

        selectList.sort(function (a, b) {
            a = a.value;
            b = b.value;
            return a - b;
        });
        $('#reportes').html(selectList);
        document.getElementById("desordenBtn").id = "ordenBtn";
    }
}




function seleccionar(idModal) {
    //Mueve los elementos de una lista a otra
    if (idModal == 'tipoCliente') {
        $('.pasar').click(function () { return !$('#origenClientes option:selected').remove().appendTo('#destinoClientes'); });
        $('.quitar').click(function () { return !$('#destinoClientes option:selected').remove().appendTo('#origenClientes'); });
        $('.pasarTodos').click(function () { $('#origenClientes option').each(function () { $(this).remove().appendTo('#destinoClientes'); }); });
        $('.quitarTodos').click(function () { $('#destinoClientes option').each(function () { $(this).remove().appendTo('#origenClientes'); }); });
    }
    else if (idModal == 'tipoServicio') {
        $('.pasar').click(function () { return !$('#origenServicios option:selected').remove().appendTo('#destinoServicios'); });
        $('.quitar').click(function () { return !$('#destinoServicios option:selected').remove().appendTo('#origenServicios'); });
        $('.pasarTodos').click(function () { $('#origenServicios option').each(function () { $(this).remove().appendTo('#destinoServicios'); }); });
        $('.quitarTodos').click(function () { $('#destinoServicios option').each(function () { $(this).remove().appendTo('#origenServicios'); }); });
    }
    else if (idModal == 'ciudades') {
        $('.pasar').click(function () { return !$('#origenCiudades option:selected').remove().appendTo('#destinoCiudades'); });
        $('.quitar').click(function () { return !$('#destinoCiudades option:selected').remove().appendTo('#origenCiudades'); });
        $('.pasarTodos').click(function () { $('#origenCiudades option').each(function () { $(this).remove().appendTo('#destinoCiudades'); }); });
        $('.quitarTodos').click(function () { $('#destinoCiudades option').each(function () { $(this).remove().appendTo('#origenCiudades'); }); });
    }
    else if (idModal == 'colonias') {
        $('.pasar').click(function () { return !$('#origenColonias option:selected').remove().appendTo('#destinoColonias'); });
        $('.quitar').click(function () { return !$('#destinoColonias option:selected').remove().appendTo('#origenColonias'); });
        $('.pasarTodos').click(function () { $('#origenColonias option').each(function () { $(this).remove().appendTo('#destinoColonias'); }); });
        $('.quitarTodos').click(function () { $('#destinoColonias option').each(function () { $(this).remove().appendTo('#origenColonias'); }); });
    }
    else if (idModal == 'periodo') {
        $('.pasar').click(function () { return !$('#origenPeriodo option:selected').remove().appendTo('#destinoPeriodo'); });
        $('.quitar').click(function () { return !$('#destinoPeriodo option:selected').remove().appendTo('#origenPeriodo'); });
        $('.pasarTodos').click(function () { $('#origenPeriodo option').each(function () { $(this).remove().appendTo('#destinoPeriodo'); }); });
        $('.quitarTodos').click(function () { $('#destinoPeriodo option').each(function () { $(this).remove().appendTo('#origenPeriodo'); }); });
    }
    else if (idModal == 'calles') {
        $('.pasar').click(function () { return !$('#origenCalles option:selected').remove().appendTo('#destinoCalles'); });
        $('.quitar').click(function () { return !$('#destinoCalles option:selected').remove().appendTo('#origenCalles'); });
        $('.pasarTodos').click(function () { $('#origenCalles option').each(function () { $(this).remove().appendTo('#destinoCalles'); }); });
        $('.quitarTodos').click(function () { $('#destinoCalles option').each(function () { $(this).remove().appendTo('#origenCalles'); }); });
    }
}


function pdf() {

    document.getElementById("reportePdf").innerHTML = '<embed src=\"Reportes/otroReporte.pdf\" width=\"500\" height=\"750\">';

}




