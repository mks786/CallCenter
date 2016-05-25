function agregar() {
    $('#ModalAgregarResOpcMults').modal('show');

}
function detalle_respuesta(data) {
    $('.detalle_respuesta').val(data.getAttribute('data-name'));
    $('.detalle_respuesta').attr('id', data.getAttribute('id'));
    $('#ModalDetalleResOpcMults').modal('show');
}

function editar_respuesta(data) {
    $('.editar_respuesta').val(data.getAttribute('data-name'));
    $('.editar_respuesta').attr('id', data.getAttribute('id'));
    $('#ModalEditarResOpcMults').modal('show');
}

function eliminar_respuesta(data) {

    $('#respuesta_texto').text(data.getAttribute('data-name'));
    $('#id_respuesta').val(data.getAttribute('id'));
    $('#eliminar_respuesta_modal').modal('show');
}