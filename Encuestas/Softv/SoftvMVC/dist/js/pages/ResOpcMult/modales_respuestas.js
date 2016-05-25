function agregar() {
    $('.maskara').mask("99/99/9999", { placeholder: "mm/dd/yyyy" });
    $('#ModalAgregarResOpcMults').modal('show');

}
function detalle_respuesta(data) {
    $('.detalle_respuesta').val(data.getAttribute('data-name'));
    $('.detalle_respuesta').attr('id', data.getAttribute('id'));
    $('#ModalDetalleResOpcMults').modal('show');
}

function editar_respuesta(data) {
    console.log(data);
    $('.editar_respuesta').val(data.getAttribute('data-name'));
    $('.editar_respuesta').attr('id', data.getAttribute('id'));
    $('#ModalEditarResOpcMults').modal('show');
}