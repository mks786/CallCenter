








$('#TablaEncuesta').on('click', '.Detalle', function () {
    $('#ModalDetalleEncuesta').modal('show');
    var id = $(this).attr('rel');
   
    var youtubeimgsrc = document.getElementById("ipreview").src;   
  
   
    document.getElementById("ipreview").src = youtubeimgsrc + id;
  
});