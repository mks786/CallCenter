

$('#tablaPreguntas').on('click', '.EditarPregunta', function () {

    $('#edPanelPreguntaOptMultiple').hide();
    $('#edPanelPreguntaCerrada').hide();
    $('#edPanelPreguntaCerrada-tbody').empty();
    $('#edPanelPreguntaOptMultiple-tbody').empty();
    $('#ed_NombrePregunta').val('');
    $('#ed_TipoPregunta').val('');
    $('#ed_tipodecontrol').show();
    $('#ed_tipodecontrol').val('');
    $('#idParaEditar').val('');


    var id = $(this).attr('rel');
    $('#ModalEditarPregunta').modal('show');
    var result = $.grep(Lista_preguntas, function (e) { return e.IdPregunta == id || e.IdPregunta2 == id; });
    console.log(result);
    console.log(result.length);
    for (var r = 0; r < result.length; r++) {
        $('#ed_NombrePregunta').val(result[r].Pregunta);
        $('#ed_TipoPregunta').val(result[r].IdTipoPregunta);

        if (result[r].TipoPregunta == "1") {
            $('#ed_tipodecontrol').hide();
            $('#ed_tipodecontrol').val('');
            $('#ed_TipoPregunta').attr("disabled",true);
            
           
        }
        else {
            $('#ed_TipoPregunta').attr("disabled", false);
            $('#ed_tipodecontrol').show();
            $('#ed_tipodecontrol').val(result[r].TipoControl);
        }   

        //
        $('#idParaEditar').val(result[r].IdPregunta);
        
        var respuestas = $.grep(Lista_opciones, function (e) { return e.Id_ResOpcMult == result[r].IdPregunta; });

        console.log(respuestas);

        for (var t = 0; t < respuestas.length; t++) {
            if (result[r].IdTipoPregunta == "1") {
            }
            if (result[r].IdTipoPregunta == "2") {
                console.log("soy 2");
                
                $('#edPanelPreguntaCerrada').show();
                $('#edPanelPreguntaOptMultiple').hide();
                $('#edPanelPreguntaCerrada-tbody').append("<tr class='nrespuestac'><td></td><td><input type='text' class='form-control' value='" + respuestas[t].ResOpcMult + "'></td><td><button class='btn btn-danger btn-xs edEliminarRespuestaC'>Quitar</button></td></tr>");
            }
            if (result[r].IdTipoPregunta == "3") {
                console.log("soy 3");
                $('#edPanelPreguntaOptMultiple').show();
                $('#edPanelPreguntaCerrada').hide();
                $('#edPanelPreguntaOptMultiple-tbody').append("<tr class='nrespuesta'><td></td><td><input type='text' class='form-control' value='" + respuestas[t].ResOpcMult + "'></td><td><button class='btn btn-danger btn-xs edEliminarRespuestaOM'>Quitar</button></td></tr>");
            }
        }



    }

});

// agrega dinamicamnete al hacer click diferentes tipos de respuesta de la pregunta
$('#edAgregarRespuestasC').click(function () {
    $('#edPanelPreguntaCerrada-tbody').append("<tr class='nrespuestac'><td></td><td class='nrespuesta'><input class='form-control' class='resp' placeholder='Respuesta' type='text'></td><td><button class='btn btn-danger btn-xs edEliminarRespuestaC'>Quitar</button></td></tr>");

});

// agrega dinamicamnete al hacer click diferentes tipos de respuesta de la pregunta
$('#edAgregarRespuestasOM').click(function () {
    $('#edPanelPreguntaOptMultiple-tbody').append("<tr class='nrespuesta'><td></td><td><input class='form-control' placeholder='Respuesta' type='text'></td><td><button class='btn btn-danger btn-xs edEliminarRespuestaOM'>Quitar</button></td></tr>");

});

//funcion: cuando se hace cambio del select tipo pregunta  se ocultan y muestran los paneles correspondientes
//ademas se borran las tablas de cada tipo de pregunta
$('#edTipoPregunta').change(function () {


    var tipo = $('#edTipoPregunta').val();
    console.log(tipo);
    if (tipo == "1") {
        $('#edtipodecontrol').hide();
        $('#edPanelPreguntaCerrada').hide();
        $('#edPanelPreguntaOptMultiple').hide();

    }
    if (tipo == "2") {
        console.log("soy 2");
        $('#edPanelPreguntaCerrada').show();
        $('#edPanelPreguntaOptMultiple').hide();
        $('#edtipodecontrol').show();
        $('#edPanelPreguntaCerrada-tbody').empty()
    }
    if (tipo == "3") {
        $('#edPanelPreguntaCerrada').hide();
        $('#edPanelPreguntaOptMultiple').show();
        $('#edtipodecontrol').show();
        $('#edPanelPreguntaOptMultiple-tbody').empty();

    }

});


$('#EditarPregunta').click(function () {

    var IdPregunta = $('#idParaEditar').val();
  
    //empieza la insercion de nuevo al arreglo lista_preguntas   
    var detallePregunta = {};
    detallePregunta.IdPregunta = generateUUID();
    detallePregunta.Pregunta = $('#NombrePregunta').val();
    detallePregunta.IdTipoPregunta = $('#TipoPregunta').val();
    detallePregunta.txtTipoPregunta = $("#TipoPregunta option:selected").text();
    detallePregunta.TipoControl = $('#tipodecontrol').val();
    detallePregunta.txtTipoControl = $("#tipodecontrol option:selected").text();
  

    var tbody = $("#edPanelPreguntaOptMultiple-tbody");
    // si no hay opciones multiples
    if (tbody.children().length == 0) {

        $('#edTablaRespuestasC > tbody  > tr').each(function () {
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

        $('#edTablaRespuestasOM > tbody  > tr').each(function () {
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

    EliminarDeArreglo(Lista_preguntas, "IdPregunta", IdPregunta);//se elimina la pregunta del arreglo de preguntas
  

    Lista_preguntas.push(detallePregunta);    
    ActualizaListaPreguntas();

    $('#ModalEditarPregunta').modal("hide");

});

$('#edTablaRespuestasOM').on('click', '.edEliminarRespuestaOM', function () {
    $(this).closest('tr').remove();
});

//elimina las respuestas  de la tabla 
$('#edTablaRespuestasC').on('click', '.edEliminarRespuestaC', function () {
    $(this).closest('tr').remove();
});