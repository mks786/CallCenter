function detalle_respuesta(data) {
    $('.detalle_respuesta').val(data.getAttribute('data-name'));
    $('.detalle_respuesta').attr('id',data.getAttribute('id'));
    $('#ModalDetalleResOpcMults').modal('show');
}