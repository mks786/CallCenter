//funcion:Al dar click al boton de eliminar pregunta se elimina el row
//*$('#tablaPreguntas').on('click', '.EliminaPregunta', function () {
//    $(this).closest('tr').remove();
//});

//se muestra modal de detalle preguntas y se borran tablas y controles
function DetallePreguntas() {
    $('#ModalAgregarPregunta').modal('show');
    $('#NombrePregunta').val('');
    $('#TipoPregunta').val(0);
    $('#tipodecontrol').val(0);
    $('#PanelPreguntaCerrada-tbody').empty();
    $('#PanelPreguntaOptMultiple-tbody').empty();;

}


//funcion: cuando se hace cambio del select tipo pregunta  se ocultan y muestran los paneles correspondientes
//ademas se borran las tablas de cada tipo de pregunta
$('#TipoPregunta').change(function () {


    var tipo = $('#TipoPregunta').val();
    console.log(tipo);
    if (tipo == "1") {
        $('#tipodecontrol').hide();
        $('#tipodecontrolPAbierta').show();
        $('#PanelPreguntaCerrada').hide();
        $('#PanelPreguntaOptMultiple').hide();

    }
    if (tipo == "2") {
        console.log("soy 2");
        $('#PanelPreguntaCerrada').show();
        $('#PanelPreguntaOptMultiple').hide();
        $('#tipodecontrol').show();
        $('#tipodecontrolPAbierta').hide();
        $('#PanelPreguntaCerrada-tbody').empty()
    }
    if (tipo == "3") {
        $('#PanelPreguntaCerrada').hide();
        $('#PanelPreguntaOptMultiple').show();
        $('#tipodecontrol').show();
        $('#tipodecontrolPAbierta').hide();
        $('#PanelPreguntaOptMultiple-tbody').empty();

    }

});


// agrega dinamicamnete al hacer click diferentes tipos de respuesta de la pregunta
$('#AgregarRespuestasC').click(function () {
    $('#PanelPreguntaCerrada-tbody').append("<tr class='nrespuestac'><td></td><td class='nrespuesta'><input class='form-control' class='resp' placeholder='Respuesta' type='text'></td><td><button class='btn btn-danger btn-xs EliminarRespuestaC'>Quitar</button></td></tr>");

});

// agrega dinamicamnete al hacer click diferentes tipos de respuesta de la pregunta
$('#AgregarRespuestasOM').click(function () {
    $('#PanelPreguntaOptMultiple-tbody').append("<tr class='nrespuesta'><td></td><td><input class='form-control' placeholder='Respuesta' type='text'></td><td><button class='btn btn-danger btn-xs EliminarRespuestaOM'>Quitar</button></td></tr>");

});


//elimina las respuestas  de la tabla 
$('#TablaRespuestasOM').on('click', '.EliminarRespuestaOM', function () {
    $(this).closest('tr').remove();
});

//elimina las respuestas  de la tabla 
$('#TablaRespuestasC').on('click', '.EliminarRespuestaC', function () {
    $(this).closest('tr').remove();
});

// funcion: al guardar 
$('#GuardarPregunta').click(function () {

    //validamos que los detalles de pregunta  esten definidos y si aplica,las opciones deben de estar 
   


    //if ($('#NombrePregunta').val() == "") {
    //    swal("Atención", "Debes definir un mombre a tu pregunta", "error");
    //}

    //else if ($('#TipoPregunta').val() == null) {

    //    swal("Atención", "Debes definir un tipo de pregunta", "error");
    //}   
    //else if ($('#TipoPregunta').val() == "2") {

    //        if ($('#tipodecontrol').val() == null) {
    //            swal("Atención", "Debes definir un tipo de control", "error");
    //        }

    //}
    //else if ($('#TipoPregunta').val() == "1") {

    //    if ($('#tipodecontrolPAbierta').val() == null) {
    //        swal("Atención", "Debes definir un tipo de control", "error");
    //    }

    //}


    //else if ($('#TipoPregunta').val() == "3") {

    //   // if ($('#tipodecontrol').val() == null) {
    //   //     swal("Atención", "Debes definir un tipo de control", "error");
    //   // }
    //   //else if ($("#PanelPreguntaOptMultiple-tbody").children().length == 0) {
    //   //     swal("Atención", "Debes definir un opciones para tu respuesta", "error");

    //   // }
    //}
    //else {
        $('#ModalAgregarPregunta').modal("hide");
        $('#tablaPreguntas').show();
        $('#msnTablavacia').hide();

        var detallePregunta = {};
        detallePregunta.IdPregunta = generateUUID();
        detallePregunta.Pregunta = $('#NombrePregunta').val();
        detallePregunta.IdTipoPregunta = $('#TipoPregunta').val();
        detallePregunta.txtTipoPregunta = $("#TipoPregunta option:selected").text();
        detallePregunta.TipoControl = $('#tipodecontrol').val();
        detallePregunta.txtTipoControl = $("#tipodecontrol option:selected").text();
        //detallePregunta.Respuestas = [];

        var tbody = $("#PanelPreguntaOptMultiple-tbody");


        // si no hay opciones multiples
        if (tbody.children().length == 0) {

            $('#TablaRespuestasC > tbody  > tr').each(function () {
                var test1 = $(this).closest(".nrespuestac").find("input:text").map(function () { return $(this).val(); });
                for (var a = 0; a < test1.length; a++) {
                    Opciones = {};
                    Opciones.Id_ResOpcMult = detallePregunta.IdPregunta;
                    Opciones.ResOpcMult = test1[a];
                    console.log(Opciones);
                    Lista_opciones.push(Opciones);
                }
            });
        }
        else {

            $('#TablaRespuestasOM > tbody  > tr').each(function () {
                var test1 = $(this).closest(".nrespuesta").find("input:text").map(function () { return $(this).val(); });
                for (var a = 0; a < test1.length; a++) {
                    Opciones = {};
                    Opciones.Id_ResOpcMult = detallePregunta.IdPregunta;
                    Opciones.ResOpcMult = test1[a];
                    console.log(Opciones);
                    Lista_opciones.push(Opciones);

                }
            });
        }

    
        Lista_preguntas.push(detallePregunta);
        console.log(detallePregunta);
        console.log(Lista_preguntas);
        ActualizaListaPreguntas();

   




    

});