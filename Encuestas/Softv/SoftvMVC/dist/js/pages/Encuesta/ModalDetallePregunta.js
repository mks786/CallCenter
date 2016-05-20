


$('#tablaPreguntas').on('click', '.detallepregunta', function () {

  
    $('#Detalle-tbody').empty();
    $('#detalleresp').empty();
    var id = $(this).attr('rel');
    $('#ModalDetallePregunta').modal('show');
    var result = $.grep(Lista_preguntas, function (e) { return e.IdPregunta == id || e.IdPregunta2==id; });
    if (result[0].IdTipoPregunta == 1 || result[0].IdTipoPregunta == 2) {
        $('#posibles_respuestas').hide();
    } else {
        $('#posibles_respuestas').show();
    }
    for (var i = 0; i < result.length; i++) {

        $('#Detalle-tbody').append("<tr><td><b>Pregunta </b></td><td colspan='3'>" + result[i].Pregunta + "</td> </tr><tr><td><b>Tipo de pregunta</b></td><td class='tipopregunta'>" + result[i].txtTipoPregunta + "</td></tr>");
        if (result[i].IdTipoPregunta != 1 || result[i].IdTipoPregunta != 2) {
            var respuestas = $.grep(Lista_opciones, function (e) { return e.Id_ResOpcMult == result[i].IdPregunta; });
            for(var s=0; s<respuestas.length; s++){
               $('#detalleresp').append("<option>" + respuestas[s].ResOpcMult + "</option>");
            }
            
        }
        else {
            $('#detalleresp').hide();
        }
    }
    
});


