$('#TablaEncuesta').on('click', '.Detalle', function () {
    var Lista_preguntas = [];
    var Lista_opciones = [];




    $('#ModalDetalleEncuesta').modal('show');
    var id = $(this).attr('rel');
   
    var youtubeimgsrc = document.getElementById("ipreview").src;   
  
   
    document.getElementById("ipreview").src = "/Encuesta/Details/" + id;
    console.log("/Encuesta/Details/" + id);
  
});