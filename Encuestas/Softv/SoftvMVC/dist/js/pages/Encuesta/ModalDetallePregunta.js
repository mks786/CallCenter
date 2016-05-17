


$('#tablaPreguntas').on('click', '.detallepregunta', function () {

  
    $('#Detalle-tbody').empty();
    $('#detalleresp').empty();
    var id = $(this).attr('rel');
    $('#ModalDetallePregunta').modal('show');
    var result = $.grep(Lista_preguntas, function (e) { return e.IdPregunta == id || e.IdPregunta2==id; });
   
    for (var i = 0; i < result.length; i++) {

        $('#Detalle-tbody').append("<tr><td><b>Pregunta </b></td><td colspan='3'>" + result[i].Pregunta + "</td> </tr><tr><td><b>Tipo de pregunta</b></td><td class='tipopregunta'>" + result[i].txtTipoPregunta + "</td><td><b>Tipo de control</b></td> <td>" + result[i].txtTipoControl + "</td></tr>");
       
        if (result[i].TipoPregunta != "1") {
            var respuestas = $.grep(Lista_opciones, function (e) { return e.Id_ResOpcMult == result[i].IdPregunta; });
            for(var s=0; s<respuestas.length; s++){
               $('#detalleresp').append("<option>" + respuestas[s].ResOpcMult + "</option>");
            }
            
        }
    }
    
});


