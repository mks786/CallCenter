function agregar() {
    $('#ModalAgregarMotivoInfo').modal('show');
}


function enviar_MotAtenTel() {

    //alert($('#Descripcion').val());

    var objMotAtenTel = {};

    objMotAtenTel.Descripcion = $('#Descripcion').val();


    if (objMotAtenTel.Descripcion == "") {
        swal("Por favor ingrese un motivo", "", "error");
    }
    else {
        $.ajax({
            url: "/MotAtenTel/Create/",
            type: "POST",
            data: { 'objMotAtenTel': objMotAtenTel },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Motivo agregado con éxito!",
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


function detalle_MotAtenTel(data) {

    $('.detalle_MotAtenTel').val(data.getAttribute('data-name'));
    $('.detalle_MotAtenTel').attr('id', data.getAttribute('id'));
    $('#ModalDetalleMotivoInfo').modal('show');
}



function editar_MotAtenTel(data) {

    $('.editar_MotAtenTel').val(data.getAttribute('data-name'));
    $('.editar_MotAtenTel').attr('id', data.getAttribute('id'));
    $('#ModalEditarMotivoInfo').modal('show');
}


function enviar_editar_MotivoInfo() {

    var NuevoMotivo = $('.editar_MotAtenTel').val();
    var IdMotivo = $('.editar_MotAtenTel').attr('id');

    var objMotAtenTel = {};
    objMotAtenTel.Descripcion = NuevoMotivo;
    objMotAtenTel.Clv_Motivo = IdMotivo;

    if (objMotAtenTel.Descripcion == "") {
        swal("Por favor introduce un motivo", "", "error");
    }
    else {
        $.ajax({
            url: "/MotAtenTel/Edit/",
            type: "POST",
            data: { 'objMotAtenTel': objMotAtenTel },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Motivo editado con éxito!",
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


$('#TablaMotAtenTel').on('click', '.eliminar', function () {


    var id = $(this).attr('id');
    var Motivo = $(this).attr('data-name');

    //alert(id);
    //alert(Nombre);

    $('#IdMotivo').val(id);
    $('#Eliminar_Motivo').text(Motivo)
    $('#ModalEliminaMotivoInfo').modal('show');

})



function confirmar_eliminar_MotivoInfo() {
    var IdMotivo = $('#IdMotivo').val();
    if (IdMotivo == "") {
        swal("Por favor introduce el motivo", "", "error");
    } else {
        $.ajax({
            url: "/MotAtenTel/Delete/",
            type: "POST",
            data: { 'id': IdMotivo },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Motivo eliminado con éxito!",
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