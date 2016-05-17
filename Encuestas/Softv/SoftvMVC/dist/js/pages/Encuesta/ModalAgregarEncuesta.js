
$('#guardarEncuesta').click(function () {
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
                IdEncuesta: 4,
                TituloEncuesta: titulo_encuesta,
                Descripcion: descripcion_encuesta,

            }
            var usuario = document.getElementById("username").innerHTML;

            $.ajax({
                url: "Encuesta/Create/",
                type: "POST",
                data: { 'encuesta': encuesta, 'Preguntas': Lista_preguntas, 'respuestas': Lista_opciones, 'usuario': usuario },
                success: function (data, textStatus, jqXHR) {
                    $('#ModalAgregarEncuesta').modal('hide');
                    location.href = "/Encuesta";
                    swal("Hecho!", "Encuesta agregada exitosamente!", "success");
                },
                error: function (jqXHR, textStatus, errorThrown) {

                }
            });

        }
    }





});