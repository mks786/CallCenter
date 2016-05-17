$('#TablaEncuesta').on('click', '.Editar', function () {

    $('#ModalEditarEncuesta').modal('show');
    

    var id = $(this).attr('rel');

    $.ajax({
        url: "Encuesta/DeepDetails/",
        type: "POST",
        data: { 'id':id},
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            console.log(data.Encuesta.Descripcion);
            console.log(data.Encuesta.TituloEncuesta);
            $('#ed_nombreEncuesta').val(data.Encuesta.TituloEncuesta);
            $('#ed_Descripcion_encuesta').val(data.Encuesta.Descripcion);
            $('#ed_tablaPreguntas').show();
            $('#ed_msnTablavacia').hide();
            for (var r = 0; r < data.Preguntas.length; r++) {
                console.log(data.Preguntas[r].Pregunta.Pregunta);
                console.log(data.Preguntas[r].Pregunta.TipoPreguntas.Descripcion);
                $('#ed_TbodyPreguntas').append("<tr><td>" + data.Preguntas[r].Pregunta.Pregunta + "</td><td>" + data.Preguntas[r].Pregunta.TipoPreguntas.Descripcion + "</td><td><button class='btn btn-info btn-xs detallepregunta' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Detalles</button> <button class='btn btn-warning btn-xs EditarPregunta ' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Editar</button> <button class='btn btn-danger btn-xs EliminaPregunta' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Eliminar</button></td></tr>")
            }
            
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });



});