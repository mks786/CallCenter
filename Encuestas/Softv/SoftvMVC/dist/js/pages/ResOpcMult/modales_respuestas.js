function agregar() {
    $('#ModalAgregarResOpcMults').modal('show');
}

function enviar_respuesta() {
    var objResOpcMults = {};
    objResOpcMults.ResOpcMult = $('#nueva_respuesta').val();
    if (objResOpcMults.ResOpcMult == "") {
        swal("Por favor introduce el nombre de la respuesta", "", "error");
    } else {
        $.ajax({
            url: "/ResOpcMults/Create/",
            type: "POST",
            data: { 'objResOpcMults': objResOpcMults },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Respuesta agregada con éxito!",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#5cb85c",
                    confirmButtonText: "Aceptar",
                    cancelButtonText: "Aceptar",
                    closeOnConfirm: false,
                    closeOnCancel: false
                }, function (isConfirm) {
                    location.reload();
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }
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

function enviar_editar_respuesta(){
    var nueva_pregunta = $('.editar_respuesta').val();
    var id_respuesta = $('.editar_respuesta').attr('id');
    var objResOpcMults = {};
    objResOpcMults.ResOpcMult = nueva_pregunta;
    objResOpcMults.Id_ResOpcMult = id_respuesta;
    if (nueva_pregunta == "") {
        swal("Por favor introduce el nombre de la respuesta", "", "error");
    } else {
        $.ajax({
            url: "/ResOpcMults/Edit/",
            type: "POST",
            data: { 'objResOpcMults': objResOpcMults },
            success: function (data, textStatus, jqXHR) {
                LlenarTabla();
                $('#ModalEditarResOpcMults').modal('hide');
                swal("Hecho!", "Respuesta editado exitosamente!", "success");
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }
}
function eliminar_respuesta(data) {

    $('#respuesta_texto').text(data.getAttribute('data-name'));
    $('#id_respuesta').val(data.getAttribute('id'));
    $('#eliminar_respuesta_modal').modal('show');
}

function confiramr_eliminar_respuesta() {
    var id_respuesta = $('#id_respuesta').val();
    if (id_respuesta == "") {
        swal("Por favor introduce el nombre de la respuesta", "", "error");
    } else {
        $.ajax({
            url: "/ResOpcMults/Delete/",
            type: "POST",
            data: { 'id': id_respuesta },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Respuesta eliminada con éxito!",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#5cb85c",
                    confirmButtonText: "Aceptar",
                    cancelButtonText: "Aceptar",
                    closeOnConfirm: false,
                    closeOnCancel: false
                }, function (isConfirm) {
                    location.reload();
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }
}