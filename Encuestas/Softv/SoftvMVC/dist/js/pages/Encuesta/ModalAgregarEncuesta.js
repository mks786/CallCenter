
$('#guardarEncuesta').click(function () {
    //validamos que existan preguntas en la tabla y los detalles de la encuesta no esten vacios 

   


    var titulo_encuesta = $('#nombreEncuesta').val();
    var descripcion_encuesta = $('#Descripcion_encuesta').val();
    var contador_encuesta = 0;
    for (var i = 0; i < Lista_preguntas.length - 1; i++) {
        for (var j = i + 1; j < Lista_preguntas.length; j++) {
            if (Lista_preguntas[i].Pregunta == Lista_preguntas[j].Pregunta) {
                contador_encuesta += 1;
            }
        }

    }
    if (titulo_encuesta == "") {
        swal("El titulo de la encuesta es obligatorio","", "error");
    } else if (descripcion_encuesta == "") {
        swal("La descripción de la encuesta es obligatorio", "", "error");
    } else if (contador_encuesta > 0) {
        swal("No puede haber preguntas con el mismo nombre", "", "error");
    } else {
        if ($('#TbodyPreguntas').children().length == 0) {
            swal("Tu encuesta no contiene preguntas", "", "error");
        } else {
            var encuesta = {
                IdEncuesta: 4,
                TituloEncuesta: titulo_encuesta,
                Descripcion: descripcion_encuesta,

            }
            var usuario = document.getElementById("username").innerHTML;

            $.ajax({
                url: "/Encuesta/Create/",
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