



$('#TablaEncuesta').on('click', '.Editar', function () {

    $('#EditarEncuesta').show();
    $('#guardarEncuesta').hide();
   
    Lista_preguntas = [];
    Lista_opciones = [];

    document.getElementById("tituloModalAgregarEncuesta").innerHTML = "Editar Encuesta";
    
   
    $('#ModalAgregarEncuesta').modal('show');  
    $('#TbodyPreguntas').empty();
    

    var id = $(this).attr('rel');
    $('#idEncuesta').val(id);
    $.ajax({
        url: "Encuesta/DeepDetails/",
        type: "POST",
        data: { 'id':id},
        success: function (data, textStatus, jqXHR) {
            console.log(data);
        
            $('#nombreEncuesta').val(data.Encuesta.TituloEncuesta);
            $('#Descripcion_encuesta').val(data.Encuesta.Descripcion);
            $('#tablaPreguntas').show();
            $('#msnTablavacia').hide();
           
            



            for (var r = 0; r < data.Preguntas.length; r++) {
                var detallePregunta = {};

                var tipo_pregunta = "";
                if (data.Preguntas[r].Pregunta.IdTipoPregunta==1) {
                    tipo_pregunta = "Abierta";
                }
                else if (data.Preguntas[r].Pregunta.IdTipoPregunta==2) {
                    tipo_pregunta = "Cerrada";
                }
                else {
                    tipo_pregunta = "Opcion Multiple";
                }


                detallePregunta.IdPregunta = generateUUID(); 
                detallePregunta.IdPregunta2 = data.Preguntas[r].Pregunta.IdPregunta;
                detallePregunta.Pregunta = data.Preguntas[r].Pregunta.Pregunta;
                detallePregunta.IdTipoPregunta = data.Preguntas[r].Pregunta.IdTipoPregunta;
                detallePregunta.txtTipoPregunta = tipo_pregunta;
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
                
                $('#TbodyPreguntas').append("<tr><td>" + data.Preguntas[r].Pregunta.Pregunta + "</td><td>" + tipo_pregunta + "</td><td><button class='btn btn-info btn-xs detallepregunta' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Detalles</button> <button class='btn btn-warning btn-xs EditarPregunta ' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Editar</button> <button class='btn btn-danger btn-xs EliminaPregunta' data-name='" + data.Preguntas[r].Pregunta.Pregunta + "' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Eliminar</button></td></tr>")
                // $('#TbodyPreguntas').append("<tr><td>" + data.Preguntas[r].Pregunta.Pregunta + "</td><td>" + data.Preguntas[r].Pregunta.TipoPreguntas.Descripcion + "</td><td><button class='btn btn-info btn-xs detallepregunta' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Detalles</button> <button class='btn btn-warning btn-xs EditarPregunta ' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Editar</button> <button class='btn btn-danger btn-xs EliminaPregunta' data-name='" + data.Preguntas[r].Pregunta.Pregunta + "' rel='" + data.Preguntas[r].Pregunta.IdPregunta + "'>Eliminar</button></td></tr>")
            }
            
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    
    

});



$('#EditarEncuesta').click(function () {
    //validamos que existan preguntas en la tabla y los detalles de la encuesta no esten vacios 

   


    var titulo_encuesta = $('#nombreEncuesta').val();
    var descripcion_encuesta = $('#Descripcion_encuesta').val();

    if (titulo_encuesta == "") {
        swal("A ocurrido un error", "El titulo de la encuesta es obligatorio", "error");
    } else if (descripcion_encuesta == "") {
        swal("A ocurrido un error", "La descripción de la encuesta es obligatorio", "error");
    } else {
        if ($('#TbodyPreguntas').children().length == 0) {
            swal("A ocurrido un error", "Tu encuesta no contiene preguntas", "error");
        } else {
            var encuesta = {
                IdEncuesta: $('#idEncuesta').val(),
                TituloEncuesta: titulo_encuesta,
                Descripcion: descripcion_encuesta,

            }
            var usuario = document.getElementById("username").innerHTML;

            $.ajax({
                url: "/Encuesta/Update/",
                type: "POST",
                data: { 'encuesta': encuesta, 'Preguntas': Lista_preguntas, 'respuestas': Lista_opciones, 'usuario': usuario },
                success: function (data, textStatus, jqXHR) {
                    LlenaTabla();
                    $('#ModalAgregarEncuesta').modal('hide');
                    swal("Hecho!", "Encuesta agregada exitosamente!", "success");

                },
                error: function (jqXHR, textStatus, errorThrown) {

                }
            });

        }
    }





});