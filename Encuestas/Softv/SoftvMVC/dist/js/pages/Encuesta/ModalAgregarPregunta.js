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
    $('#PanelPreguntaCerrada').hide();
    $('#PanelPreguntaCerrada-tbody').empty();
    $('#PanelPreguntaOptMultiple-tbody').empty();;

}


//funcion: cuando se hace cambio del select tipo pregunta  se ocultan y muestran los paneles correspondientes
//ademas se borran las tablas de cada tipo de pregunta
$('#TipoPregunta').change(function () {

    var tipo = $('#TipoPregunta').val();
    if (tipo == "1") {
        $('#tipodecontrol').hide();
        $('#tipodecontrolPAbierta').show();
        $('#PanelPreguntaCerrada').hide();
        $('#PanelPreguntaOptMultiple').hide();

    }
    if (tipo == "2") {
        $('#PanelPreguntaCerrada').hide();
        $('#tipodecontrol').hide();
        $('#tipodecontrolPAbierta').hide();
        $('#PanelPreguntaCerrada-tbody').empty()
    }
    if (tipo == "3") {
        $('#PanelPreguntaCerrada').show();;
        $('#tipodecontrol').hide();
        $('#tipodecontrolPAbierta').hide();
        $('#PanelPreguntaOptMultiple-tbody').empty();

    }

});


// agrega dinamicamnete al hacer click diferentes tipos de respuesta de la pregunta
function AgregarRespuesta() {
    $('#PanelPreguntaCerrada-tbody').append("<tr class='nrespuestac'><td></td><td class='nrespuesta'><input class='form-control' class='resp' placeholder='Respuesta' type='text'></td><td><button class='btn btn-danger btn-xs EliminarRespuestaC'>Quitar</button></td></tr>");

}


//elimina las respuestas  de la tabla 
$('#TablaRespuestasOM').on('click', '.EliminarRespuestaOM', function () {
    $(this).closest('tr').remove();
});

//elimina las respuestas  de la tabla 
$('#TablaRespuestasC').on('click', '.EliminarRespuestaC', function () {
    $(this).closest('tr').remove();;
});

// funcion: al guardar 
var vacios = 0;
$('#GuardarPregunta').click(function () {

    //validamos que los detalles de pregunta  esten definidos y si aplica,las opciones deben de estar 
    var nombre_pregunta = $('#NombrePregunta').val();
    var tipo_pregunta = $("#TipoPregunta option:selected").val();
    var tipo_control = $('#tipodecontrol option:selected').val();
    var tipo_control_abierta = $('#tipodecontrolPAbierta option:selected').val();
    if (nombre_pregunta == "") {
        swal("A ocurrido un error", "El nombre de la pregunta es obligatorio", "error");
    } else if (tipo_pregunta == 0) {
        swal("A ocurrido un error", "Por favor selecciona el tipo de respuesta", "error");
    }
    else if (tipo_pregunta == 1) {
        // $('#ModalAgregarPregunta').modal("hide");
        $('#tablaPreguntas').show();
        $('#msnTablavacia').hide();

        var detallePregunta = {};
        detallePregunta.IdPregunta = generateUUID();
        detallePregunta.Pregunta = nombre_pregunta;
        detallePregunta.IdTipoPregunta = tipo_pregunta;
        detallePregunta.txtTipoPregunta = $("#TipoPregunta option:selected").text();
        detallePregunta.TipoControl = $('#tipodecontrolPAbierta').val();
        detallePregunta.txtTipoControl = $("#tipodecontrolPAbierta option:selected").text();
        //detallePregunta.Respuestas = [];

        var tbody = $("#PanelPreguntaOptMultiple-tbody");



        Lista_preguntas.push(detallePregunta);
        ActualizaListaPreguntas();
        $('#ModalAgregarPregunta').modal("hide");


    } else if (tipo_pregunta == 2) {
        // $('#ModalAgregarPregunta').modal("hide");
        $('#tablaPreguntas').show();
        $('#msnTablavacia').hide();

        var detallePregunta = {};
        detallePregunta.IdPregunta = generateUUID();
        detallePregunta.Pregunta = nombre_pregunta;
        detallePregunta.IdTipoPregunta = tipo_pregunta;
        detallePregunta.txtTipoPregunta = $("#TipoPregunta option:selected").text();
        detallePregunta.TipoControl = $('#tipodecontrolPAbierta').val();
        detallePregunta.txtTipoControl = $("#tipodecontrolPAbierta option:selected").text();
        //detallePregunta.Respuestas = [];

        var tbody = $("#PanelPreguntaOptMultiple-tbody");



        Lista_preguntas.push(detallePregunta);
        ActualizaListaPreguntas();
        $('#ModalAgregarPregunta').modal("hide");


    } else if (tipo_pregunta == 3) {
        // $('#ModalAgregarPregunta').modal("hide");
        $('#tablaPreguntas').show();
        $('#msnTablavacia').hide();

        var detallePregunta = {};
        detallePregunta.IdPregunta = generateUUID();
        detallePregunta.Pregunta = nombre_pregunta;
        detallePregunta.IdTipoPregunta = tipo_pregunta;
        detallePregunta.txtTipoPregunta = $("#TipoPregunta option:selected").text();
        detallePregunta.TipoControl = $('#tipodecontrol').val();
        detallePregunta.txtTipoControl = $("#tipodecontrol option:selected").text();
        //detallePregunta.Respuestas = [];

        var tbody = $("#PanelPreguntaCerrada-tbody");


        // si no hay opciones multiples
        if (tbody.children().length == 0) {
            swal("A ocurrido un error", "Por favor agrega respuestas a la pregunta", "error");
        }
        else {
            vacios = 0;
            $('#TablaRespuestasC > tbody  > tr').each(function () {
                var test1 = $(this).closest(".nrespuestac").find("input:text").map(function () { return $(this).val(); });
                for (var a = 0; a < test1.length; a++) {
                    Opciones = {};
                    Opciones.Id_ResOpcMult = detallePregunta.IdPregunta;
                    Opciones.ResOpcMult = test1[a];
                    Lista_opciones.push(Opciones);
                    if (test1[a] == "") {
                        vacios = vacios + 1;
                        console.log("entras");
                    }
                }
            });
            console.log(vacios);
            if (vacios > 0) {
                swal("A ocurrido un error", "Por favor llena todas las respuestas o elimina las vacias", "error");
            } else {

                Lista_preguntas.push(detallePregunta);
                ActualizaListaPreguntas();
                $('#ModalAgregarPregunta').modal("hide");
                $('#PanelPreguntaCerrada').hide();
            }

        }




    }

});