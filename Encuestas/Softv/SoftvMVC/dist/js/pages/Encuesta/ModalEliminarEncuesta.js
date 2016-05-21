function eliminarEncuesta(id) {
    var name_encuesta = id.getAttribute("data-name");
    var id_encuesta = id.getAttribute("id");
    $('#Modal_id_encuesta').val(id_encuesta);
    $('#Modal_nombre_encuesta').text(name_encuesta);
    $('#ModalEliminarEncuesta').modal('show');
}