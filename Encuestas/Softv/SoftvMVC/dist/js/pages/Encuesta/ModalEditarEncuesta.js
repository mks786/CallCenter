



$('#TablaEncuesta').on('click', '.Editar', function () {


    Lista_preguntas = [];
    Lista_opciones = [];

    document.getElementById("tituloModalAgregarEncuesta").innerHTML = "Editar Encuesta";
    document.getElementById("guardarEncuesta").innerHTML = "Editar Encuesta";
   
    $('#ModalAgregarEncuesta').modal('show');  
    $('#TbodyPreguntas').empty();
    

    var id = $(this).attr('rel');

    $.ajax({
        url: "Encuesta/DeepDetails/",
        type: "POST",
        data: { 'id':id},
        success: function (data, textStatus, jqXHR) {
        
        
            $('#nombreEncuesta').val(data.Encuesta.TituloEncuesta);
            $('#Descripcion_encuesta').val(data.Encuesta.Descripcion);
            $('#tablaPreguntas').show();
            $('#msnTablavacia').hide();
           

            for (var r = 0; r < data.Preguntas.length; r++) {
                var detallePregunta = {};
                detallePregunta.IdPregunta = generateUUID(); 
                detallePregunta.IdPregunta2 = data.Preguntas[r].Pregunta.IdPregunta;
                detallePregunta.Pregunta = data.Preguntas[r].Pregunta.Pregunta;
                detallePregunta.IdTipoPregunta = data.Preguntas[r].Pregunta.IdTipoPregunta;
                detallePregunta.txtTipoPregunta = data.Preguntas[r].Pregunta.TipoPreguntas.Descripcion;
                detallePregunta.TipoControl =3;
                detallePregunta.txtTipoControl ="control de prueba";
                Lista_preguntas.push(detallePregunta);

                for (var f = 0; f < data.Preguntas[r].Respuestas.length; f++) {
                    Opciones = {};
                    Opciones.Id_ResOpcMult = detallePregunta.IdPregunta;
                    Opciones.Id_ResOpcMult2 = data.Preguntas[r].Respuestas[f].Id_ResOpcMult;
                    Opciones.ResOpcMult = data.Preguntas[r].Respuestas[f].ResOpcMult;
                    Lista_opciones.push(Opciones);
                    

                }
                
                $('#TbodyPreguntas').append("<tr><td>" + data.Preguntas[r].Pregunta.Pregunta + "</td><td>" + data.Preguntas[r].Pregunta.TipoPreguntas.Descripcion + "</td><td><button class='btn btn-info btn-xs detallepregunta' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Detalles</button> <button class='btn btn-warning btn-xs EditarPregunta ' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Editar</button> <button class='btn btn-danger btn-xs EliminaPregunta' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Eliminar</button></td></tr>")
            }
            
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    
    

});