function agregar() {
    $('#ModalAgregarRoles').modal('show');
}

function enviar_role() {

    var objRole = {};
    var Estado = true;
    objRole.Nombre = $('#Nombre').val();
    objRole.Descripcion = $('#Descripcion').val();
    objRole.Estado = Estado
    if (objRole.Nombre == "") {
        swal("Por favor introduce el nombre del rol", "", "error");
    }
    else if (objRole.Descripcion == "") {
        swal("Por favor introduce la descripcion del rol", "", "error");
    }
    else {
        $.ajax({
            url: "/Role/Create/",
            type: "POST",
            data: { 'objRole': objRole },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Role agregado con éxito!",
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


function detalle_role(data) {


    $('.detalle_role').val(data.getAttribute('data-name'));
    $('.detalle_role2').val(data.getAttribute('data-name2'));
    $('.detalle_role').attr('id', data.getAttribute('id'));
    $('#ModalDetalleRoles').modal('show');
}



function editar_role(data) {

    $('.editar_role').val(data.getAttribute('data-name'));
    $('.editar_role2').val(data.getAttribute('data-name2'));
    $('.editar_role').attr('id', data.getAttribute('id'));
    $('#ModalEditarRoles').modal('show');
}

function enviar_editar_role() {
    var NuevoRolNombre = $('.editar_role').val();
    var NuevoRolDescripcion = $('.editar_role2').val();
    var NuevoRolEstado = true;
    var IdRol = $('.editar_role').attr('id');

    var objRole = {};
    objRole.Nombre = NuevoRolNombre;
    objRole.Descripcion = NuevoRolDescripcion;
    objRole.Estado = NuevoRolEstado;
    objRole.IdRol = IdRol;
    if (NuevoRolNombre == "") {
        swal("Por favor introduce un nombre para el Rol", "", "error");
    }
    if (NuevoRolDescripcion == "") {
        swal("Por favor introduce una descripción para el Rol", "", "error");
    }
    else {
        $.ajax({
            url: "/Role/Edit/",
            type: "POST",
            data: { 'objRole': objRole },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Rol editado con éxito!",
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



$('#TablaRoles').on('click', '.eliminar', function () {


    var id = $(this).attr('id');
    var Nombre = $(this).attr('data-name');

    //alert(id);
    //alert(Nombre);

    $('#IdRol').val(id);
    $('#Eliminar_Nombre').text(Nombre)
    $('#ModalEliminaRol').modal('show');

})


 


function confirmar_eliminar_rol() {
    var IdRol = $('#IdRol').val();
    if (IdRol == "") {
        swal("Por favor introduce el nombre del rol", "", "error");
    } else {
        $.ajax({
            url: "/Role/Delete/",
            type: "POST",
            data: { 'id': IdRol },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Role eliminado con éxito!",
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