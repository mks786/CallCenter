// obtener valor del radiobutton seleccionado
function radioBtn() {
    var ordenarPor = $("input[name='ordenar']:checked").val();
    return ordenarPor;
}

function filtrarPor() {
    //var marcado = $('#soloInternet').is(':checked');  
    if ($('#soloInternet').is(':checked')) {
        var soloInternet = 1; //seleccionado
    }
    else {
        var soloInternet = 0; //no seleccionado
    }
    return soloInternet;
}

function reporte() {
    //obtiene el id del Reporte del 0 al 12 (no hay 9 ni 10)
    var lista = document.getElementById("reportes"); //select id = reportes 
    var idReporte = lista.options[lista.selectedIndex].id;   // Obtener valor de la opción seleccionada
    return idReporte;
}




var obj = {}; // es global 


function guardarDatos(numModal) {

    var reportes_valor = $('#reportes').val(); //Obtener el val del elemento seleccionado
    //Pantalla principal  
    var soloInternet = 0;

    //REVISA SI EL TIPO DE SERVICIO ES CABLE (1) O INTERNET (2) y retorna su value
    var lista = document.getElementById("listaTipoServicio"); //select id = listaTipoServicio 
    var op = lista.options[lista.selectedIndex].value;   // Obtener el valor de la opción seleccionada
    objPrincipal.op = op;
    objPrincipal.Orden = radioBtn(); //ordenar reportes por contrato(1) ó colonia y calle(2)
    objPrincipal.clv_reporte = reporte(); // Obtener valor de la opción seleccionada//reporte seleccionado

    console.log($("#reportes").attr("id"));
    soloInternet = filtrarPor();//filtrarPor = filtrarPor(); //filtrar por soloInternet
    objPrincipal.soloInternet = soloInternet;
    //Fin pantalla principal

    if (numModal == 1) {

        var seleccionTipoCliente = [];
        var lista = document.getElementById('destinoClientes');
        for (var i = 0; i < lista.length; i++) {
            var opcionSel = lista.options[i].value //Obtener valor de todas las opciones en la lista destino
            seleccionTipoCliente.push(opcionSel);
        }
        objTipoCliente.tipoCliente = seleccionTipoCliente;

    } else if (numModal == 2) {
        var seleccionServicios = [];
        var lista = document.getElementById('destinoServicios');
        for (var i = 0; i < lista.length; i++) {
            var opcionSel = lista.options[i].value //Obtener valor de todas las opciones en la lista destino
            seleccionServicios.push(opcionSel);
        }
        objServicio.servicio = seleccionServicios;

    } else if (numModal == 3) {

        var seleccionCiudades = [];
        var lista = document.getElementById('destinoCiudades');
        for (var i = 0; i < lista.length; i++) {
            var opcionSel = lista.options[i].value //Obtener valor de todas las opciones en la lista destino
            seleccionCiudades.push(opcionSel);
        }
        objCiudades.ciudades = seleccionCiudades;

    } else if (numModal == 4) {
        var seleccionColonias = [];

        var lista = document.getElementById('destinoColonias');
        for (var i = 0; i < lista.length; i++) {
            var opcionSel = lista.options[i].value //Obtener valor de todas las opciones en la lista destino
            seleccionColonias.push(opcionSel);
        }
        objColonias.colonias = seleccionColonias;

    } else if (numModal == 5) {
        var telefono = false; var todos = 0; var conTel = 0; var sinTel = 0;

        if ($("#conTel").is(':checked')) {
            conTel = 1;
            telefono = true;
        }
        if ($("#sinTel").is(':checked')) {
            var sinTel = 1;
            telefono = false;
        }
        if (conTel == 1 && sinTel == 1) {
            todos = 1;
            telefono = 0;
        }

        objTelefono.telefono = telefono;
        objTelefono.todos = todos;

    } else if (numModal == 6) {
        var seleccionPeriodo = [];

        var lista = document.getElementById('destinoPeriodo');
        for (var i = 0; i < lista.length; i++) {
            var opcionSel = lista.options[i].value //Obtener valor de todas las opciones en la lista destino
            seleccionPeriodo.push(opcionSel);
        }
        objPeriodo.periodo = seleccionPeriodo;

        if (reportes_valor == 5 || reportes_valor == 10) {

            objPeriodo.periodo = seleccionPeriodo;
            objPeriodo.ultimo_mes = $("#listaMesP").val(); //select id = listaTipoServicio
            objPeriodo.ultimo_anio = $("#anioCP").val(); //valor del textbox
            //alert($("#listaMesP").val() + "lista y anio " + $("#anioCP").val())
        }
        if (reportes_valor == 0 || reportes_valor == 1) //if idReporte==3 and idReporte==2 (desconectados y suspendidos)
        {
            //alert('reporte cero o uno')
            objPeriodo.ultimo_mes = 1;
            objPeriodo.ultimo_anio = 0;
        }
    } else if (numModal == 7) {

        var ordenEjecutada = 0;
        var pendiente = 0;

        if ($("#pendiente").is(':checked')) {
            pendiente = 1;
        }
        if ($("#ejecutada").is(':checked')) {
            ordenEjecutada = 1;
        }
        if (pendiente == 1 && ordenEjecutada == 1) {
            ordenEjecutada = 2;
        }

        objEstatusOrden.OrdenEjecutada = ordenEjecutada;

    } else if (numModal == 8) {

        var fechaInicial = $("#fechaInicial").val();
        var fechaFinal = $("#fechaFinal").val();

        //alert(fechaInicial+" inicial y final "+fechaFinal);
        objRangoFechas.fechaInicial = fechaInicial;
        objRangoFechas.fechaFinal = fechaFinal;

        if (reportes_valor == 7) {

            var lista = document.getElementById("listaMotivoCancelacion"); //select id = listaTipoServicio 
            objRangoFechas.motcan = lista.options[lista.selectedIndex].value;   // Obtener el valor de la opción seleccionada
            objRangoFechas.motivo = 1; //la lista de motivo de cancelación está activa
        }

    } else if (numModal == 11) {
        var seleccionCalles = [];

        var lista = document.getElementById('destinoCalles');
        for (var i = 0; i < lista.length; i++) {
            var opcionSel = lista.options[i].value //Obtener valor de todas las opciones en la lista destino
            seleccionCalles.push(opcionSel);
        }
        objCalles.calles = seleccionCalles;

    } else if (numModal == 12) //modal EstatusCliente
    {
        objParametros.conectado = 0;
        objParametros.Fuera = 0;
        objParametros.Susp = 0;
        objParametros.Insta = 0;
        objParametros.Desconect = 0;
        objParametros.baja = 0;
        objParametros.DescTmp = 0;

        if ($("#contratado").is(':checked')) {
            objParametros.conectado = 1;
        }
        if ($("#fuera").is(':checked')) {
            objParametros.Fuera = 1;
        }
        if ($("#suspendidos").is(':checked')) {
            objParametros.Susp = 1;
        }
        if ($("#instalados").is(':checked')) {
            objParametros.Insta = 1;
        }
        if ($("#desconectado").is(':checked')) {
            objParametros.Desconect = 1;
        }
        if ($("#cancelados").is(':checked')) {
            objParametros.baja = 1;
        }
        if ($("#suspendidosTempo").is(':checked')) {
            objParametros.DescTmp = 1;
        }
        //  alert('conTel ' + conTel + ' sinTel ' + sinTel);

        var lista = document.getElementById("listaMotivoCancelacion12"); //select id = listaTipoServicio 
        var listaMotivoCancelacion12 = lista.options[lista.selectedIndex].value;   // Obtener el valor de la opción seleccionada

        var listaMes = document.getElementById("listaMesQueAdeuda"); //select id = listaTipoServicio 
        var listaMesQueAdeuda = listaMes.options[listaMes.selectedIndex].value;   // Obtener el valor de la opción seleccionada

        var valorC = document.getElementById("anioC").value; //valor del textbox  

        if ($('#ocultarMostrar').is(':checked')) {
            objEstatusCliente.buscaOno = 1; //seleccionado, busca por mes que adeudan y año
        }
        else {
            objEstatusCliente.buscaOno = 0; //no seleccionado
        }

        var listaMenor = document.getElementById("listaMenorIgual"); //select id = listaTipoServicio 
        var listaMenorIgual = listaMenor.options[listaMenor.selectedIndex].value;   // Obtener el valor de la opción seleccionada
        objEstatusCliente.estatusCliente = 0;
        //objEstatusCliente.estatusCliente = fruits;
        objEstatusCliente.mes = listaMesQueAdeuda;
        objEstatusCliente.anio = valorC;
        objEstatusCliente.buscarPor = listaMenorIgual; //si es 1:'<=' si es 2:'='        
        objEstatusCliente.motivoCancelacion = listaMotivoCancelacion12;
    }

    if (reportes_valor <= 10) //todos los reportes del 0 al 10 excluyen calles
    {
        objCalles.calles = 0;
    }


    if (reportes_valor == 0) //ó idReporte == 3 Desconectados
    {
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;

        if (op == 2) {

            objEstatusOrden.ordenEjecutada = 2;
        }
    }

    if (reportes_valor == 1) //reportes_valor=1 o idReporte==2 es suspendidos
    {
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;

        if (op == 2) {

            objEstatusOrden.ordenEjecutada = 2;
        }
    }

    if (reportes_valor == 2) //al corriente
    {
        objParametros.op_rep = 1;
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;
    }
    if (reportes_valor == 3) //adelantados
    {
        //alert('adelantados')
        objParametros.op_rep = 3;
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;
    }
    if (reportes_valor == 4) //contrataciones Reporte_TiposClienteTv_nuevo
    {
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;

        objParametros.conectado = 1;
        objParametros.baja = 0;
        objParametros.Insta = 0;
        objParametros.Desconect = 0;
        objParametros.Susp = 0;
        objParametros.Fuera = 0;

    }
    if (reportes_valor == 5) //adelantados
    {
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;

        //objEstatusOrden.OrdenEjecutada = 2
    }

    if (reportes_valor == 6) //Instalaciones Reporte_TiposClienteTv_nuevo
    {
        objRangoFechas.motcan = 0; //motivoCancelación

        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;

        objParametros.conectado = 0;
        objParametros.baja = 0;
        objParametros.Insta = 1;
        objParametros.Desconect = 0;
        objParametros.Susp = 0;
        objParametros.Fuera = 0;
    }
    if (reportes_valor == 7) //Cancelaciones Reporte_TiposClienteTv_nuevo
    {
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;

        objParametros.conectado = 0;
        objParametros.baja = 1;
        objParametros.Insta = 0;
        objParametros.Desconect = 0;
        objParametros.Susp = 0;
        objParametros.Fuera = 0;
    }

    if (reportes_valor == 8) //por instalar
    {
        objParametros.op_rep = 2;
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;
        objEstatusOrden.OrdenEjecutada = 2
    }
    if (reportes_valor == 9) //Fueras de Area - Reporte_TiposClienteTv_nuevo
    {
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;

        objParametros.conectado = 0;
        objParametros.baja = 0;
        objParametros.Insta = 0;
        objParametros.Desconect = 0;
        objParametros.Susp = 0;
        objParametros.Fuera = 1;
    }
    if (reportes_valor == 10) //paquetes de cortesia Reporte_cortesia_nuevo
    {
        objTipoCliente.tipoCliente = 0;

        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;
    }
    if (reportes_valor == 11) //Ciudad   Reporte_TiposCliente_Ciudad 
    {
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;

    }
    if (reportes_valor == 12) //Resumen por Colonia   Reporte_TiposCliente_Ciudad_Resumen 
    {
        objParametros.habilita = 0;
        objParametros.periodo1 = 0;
        objParametros.periodo2 = 0;
    }
    console.log(obj)
}


function borrarObj() {

    objPrincipal = {};
    objTipoCliente = {};
    objServicio = {};
    objCiudades = {};
    objColonias = {};
    objTelefono = {};
    objPeriodo = {};
    objEstatusOrden = {};
    objRangoFechas = {};
    objCalles = {};
    objParametros = {};
    objEstatusCliente = {};
    objClv_Session = {};
    objReporte = {};
    $('#xmlComoCadena').val(" ");
}

//mandar los datos al controlador ReportesVarios
function enviarDatos() {
    //se activa cuando se presiona el botón id='aceptar6' ó 'aceptar7'
    //siempre y cuando pase los filtros 


    objReporte.num_reporte = $('#reportes').val(); //Obtener el val del elemento seleccionado

    //  alert('se envían los datos');
    console.log('objPrincipal' + objPrincipal + ' objTipoCliente' + objTipoCliente + 'objServicio' + objServicio +
              'objCiudades' + objCiudades + 'objColonias' + objColonias + 'objTelefono' + objTelefono + 'objPeriodo' + objPeriodo,
              'objEstatusOrden' + objEstatusOrden + 'objRangoFechas' + objRangoFechas + 'objCalles' + objCalles +
              'objEstatusCliente' + objEstatusCliente + 'objParametros' + objParametros + 'objReporte' + objReporte);
    $.ajax({
        url: "ReportesVarios/Create/",
        type: "POST",
        data: {
            'objPrincipal': objPrincipal, 'objTipoCliente': objTipoCliente, 'objServicio': objServicio,
            'objCiudades': objCiudades, 'objColonias': objColonias, 'objTelefono': objTelefono, 'objPeriodo': objPeriodo,
            'objEstatusOrden': objEstatusOrden, 'objRangoFechas': objRangoFechas, 'objCalles': objCalles,
            'objEstatusCliente': objEstatusCliente, 'objParametros': objParametros, 'objReporte': objReporte
        },
        success: function (data) {
            console.log(data);
            //alert(data);
            $('#xmlComoCadena').val(data); //Guarda el xml ya hecho
            //alert(data);
            //alert('llama a enviarxmlcomostring');
            enviarXmlComoString();
        }
    });


}





function enviarXmlComoString() {

    var reportes_valor = $('#reportes').val(); //Obtener el val del reporte seleccionado
    //alert('num repor ' + reportes_valor);
    //REVISA SI EL TIPO DE SERVICIO ES CABLE (1) O INTERNET (2) y retorna su value
    var lista = document.getElementById("listaTipoServicio"); //select id = listaTipoServicio 
    var op = lista.options[lista.selectedIndex].value;   // Obtener el valor de la opción seleccionada
    var idConexion = $("#listaConexionPlaza").val(); //obtener idConexion seleccionada


    //objPrincipal.Orden //ordenar reportes por contrato(1) ó colonia y calle(2)

    //Envía cada cadena a la clase que crea su pdf
    var url;
    if (reportes_valor == 0) //desconectados
    {
        if (op == 1) //cable
        {
            url = "ReportesVarios/ReportePorPagarInternet_2_descon";
        }
        else if (op == 2) //internet
        {
            //alert("tres filtros");
            url = "ReportesVarios/REportePorPagarSDP";
        }
    }
    else if (reportes_valor == 5) // por pagar
    {
        if (op == 1) //cable
        {
            url = "ReportesVarios/ReportePorPagarInternet_2_porPagar";
        }
        else if (op == 2) //internet
        {
            //alert("tres filtros");
            url = "ReportesVarios/REportePorPagarSDP";
        }
    }
    else if (reportes_valor == 1) // suspendidos
    {

        if (op == 1) //cable
        {
            url = "ReportesVarios/REportePorPagarTv_2";
        }
        else if (op == 2) //internet
        {
            //alert("tres filtros");
            url = "ReportesVarios/REportePorPagarSDP";
        }

    }
    else if (reportes_valor == 2 || reportes_valor == 3 || reportes_valor == 8) //al corriente, adelantados, por instalar
    {

        if (objPrincipal.Orden == 1) //contrato
        {
            url = "ReportesVarios/ReportePIInt_2";
        }
        else if (objPrincipal.Orden == 2) //colonia y calle
        {
            //alert("por colonia y calle")
            url = "ReportesVarios/ReportePIInt_2_Colonias";

        }

    }

    else if (reportes_valor == 4 || reportes_valor == 6 || reportes_valor == 7 || reportes_valor == 9) //contrataciones, instalacion, cacncelacion, fueras de a
    {
        if (objPrincipal.Orden == 1)//por contrato
        {
            if (op == 1) //cable
            { url = "ReportesVarios/Reporte_Rango_Fechas_Tv_2"; }
            else if (op == 2) //internet
            {
                //url = "ReportesVarios/GenerarPdf"; //*****************PDF DE EJEMPLO
                url = "ReportesVarios/Reportes_varios_Fechas_2";
            }
        }
        else if (objPrincipal.Orden == 2)//por colonia y calle
        {
            if (op == 1) //cable
            {
                // alert("cable y colonias");
                url = "ReportesVarios/Reporte_Rango_Fechas_Tv_2_Colonias";
            } else if (op == 2) //internet
            {
                //   alert("internet");
                url = "ReportesVarios/Reportes_varios_Fechas_2_Colonias";
            }

        }

    }
    else if (reportes_valor == 10) //paquetes de cortesia
    {
        if (op == 1) {
            url = "ReportesVarios/ReporteNuevoCortesiaTv_2";
        } else if (op == 2) {
            // alert("internet");
            url = "ReportesVarios/ReporteNuevoCortesiaIntDig_2"
        }
    }
    else if (reportes_valor == 11) //ciudad,
    {
        if (objPrincipal.Orden == 1)//Orden por contrato
        {
            if (op == 1) //cable
            {
                var suma = objParametros.conectado + objParametros.Fuera + objParametros.Susp +
                    objParametros.Insta + objParametros.Desconect + objParametros.baja + objParametros.DescTmp
                if (suma == 1) //Un solo filtro
                {
                    //alert("Un solo filtro");
                    url = "ReportesVarios/ReporteCiudad_2_CU";
                }
                else if (suma == 2) //Dos filtros
                {
                    // alert("Dos filtros");
                    url = "ReportesVarios/ReporteCiudad_2_E";
                }
                else if (suma >= 3 && suma <= 6)//tres a seis filtros
                {
                    //alert("Tres filtros");
                    url = "ReportesVarios/ReporteCiudad_2_B";
                }
                else if (suma == 7) //siete filtros
                {
                    //alert("Todos los filtros");
                    url = "ReportesVarios/ReporteCiudad_2_A";
                }
            }
            else if (op == 2) //Internet y todos los filtros
            {
                //alert("INTERNET");
                url = "ReportesVarios/ReporteCiudad_2_Internet";
            }
        }
        else if (objPrincipal.Orden == 2)//Orden por colonia y calle
        {
            if (op == 1) {
                var suma = objParametros.conectado + objParametros.Fuera + objParametros.Susp +
                   objParametros.Insta + objParametros.Desconect + objParametros.baja + objParametros.DescTmp
                if (suma == 1 && objParametros.baja == 0) //Un solo filtro
                {
                    //alert("Un solo filtro");
                    url = "ReportesVarios/ReporteCiudad_2_CU_CableColonias";
                }
                else if (suma >= 2 && objParametros.baja == 0) //Un solo filtro
                {
                    // alert("2 a 6 filtros");
                    url = "ReportesVarios/ReporteCiudad_2_E_CableColonias";
                }
                else if (objParametros.baja == 1) //Un solo filtro
                {
                    //alert("baja=1 --cancelados--todos los filtros que incluyan a baja");
                    url = "ReportesVarios/ReporteCiudad_2_Baja_CableColonias";
                }
            } else if (op == 2) {
                if (objParametros.baja == 0) //Un solo filtro
                {
                    url = "ReportesVarios/ReporteCiudad_2_InternetColonias";
                }
                else if (objParametros.baja == 1) //Un solo filtro
                   // alert("baja=1 --cancelados--todos los filtros que incluyan a baja");
                url = "ReportesVarios/ReporteCiudad_2_Baja_CableColonias";
            }

        }
    }
        //url = "ReportesVarios/ReporteCiudad_2";

    else if (reportes_valor == 12) //ciudad resumen
    {    //ir a ReporteCiudad_2        
        url = "ReportesVarios/Reporte_Resumen_Por_Colonia";
    }
    //alert(url);


    $('#Espere').modal('show');
    //Envía el xml ya hecho al controlador con la ruta indicada
    var cadena = $('#xmlComoCadena').val();
    // alert('se envía el xml como cadena ' + cadena);
    $.ajax({
        type: "POST",
        //dataType: 'json',
        //cache: false,
        url: url, //  url: "ReportesVarios/GenerarPdf",
        data: { 'cadena': cadena, 'idConexion': idConexion },
        success: function (data) {
            console.log(data);
            //alert('idConexion ' + idConexion +' data: '+data);
            $('#Espere').modal('hide');
            // alert('Reporte Generado con Éxito');
            //  alert('ruta' + data);
            //Muestra el Pdf en vista previa
            document.getElementById("reportePdf").innerHTML = '<embed src=' + data + ' width=\"600\" height=\"750\">';
            // document.getElementById("reportePdf").innerHTML = '<embed src=\"Reportes/otroReporte.pdf\" width=\"500\" height=\"750\">';

        }
    });
}



//data: {
//    'objPrincipal': objPrincipal, 'objTipoCliente': objTipoCliente, 'objServicio': objServicio,
//    'objCiudades': objCiudades, 'objColonias': objColonias, 'objTelefono': objTelefono, 'objPeriodo': objPeriodo,
//    'objEstatusOrden': objEstatusOrden, 'objRangoFechas': objRangoFechas, 'objCalles': objCalles,
//    'objEstatusCliente': objEstatusCliente, 'objParametros': objParametros, 'objReporte': objReporte
//},