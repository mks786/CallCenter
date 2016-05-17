


$('#guardarEncuesta').click(function(){
    //validamos que existan preguntas en la tabla y los detalles de la encuesta no esten vacios 
    console.log(Lista_preguntas);
    console.log(encuesta);
    if ($('#TbodyPreguntas').children().length == 0) {
        swal("A ocurrido un error", "Tu encuesta no contiene preguntas", "error");
    } else {
        var encuesta = {
            IdEncuesta: 4,
            TituloEncuesta: $('#nombreEncuesta').val(),
            Descripcion: $('#Descripcion_encuesta').val(),
           
        }
        var usuario= document.getElementById("username").innerHTML;

        $.ajax({
            url: "Encuesta/Create/",
            type: "POST",
            data: { 'encuesta': encuesta, 'Preguntas': Lista_preguntas, 'respuestas': Lista_opciones, 'usuario': usuario },
            success: function (data, textStatus, jqXHR) {
                //data - response from server
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });

    }
   
   


});