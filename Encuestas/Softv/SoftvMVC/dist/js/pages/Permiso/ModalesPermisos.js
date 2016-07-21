function agregar() {
    $('#ModalAgregarPermiso').modal('show');
}

function enviar_permiso() {

    if (document.getElementById('OptAdd').checked) { var a = true } else { var a = false }
    if (document.getElementById('OptSelect').checked) { var s = true } else { var s = false }
    if (document.getElementById('OptDelete').checked) { var d = true } else { var d = false }
    if (document.getElementById('OptUpdate').checked) { var u = true } else { var u = false }


    var objPermiso = {};

    objPermiso.IdRol = $('#IdRol').val();
    objPermiso.IdModule = $('#IdModule').val();
    objPermiso.OptAdd = a;
    objPermiso.OptSelect = s;
    objPermiso.OptDelete = d;
    objPermiso.OptUpdate = u;


    if (objPermiso.IdRol == "") {
        swal("Por favor seleccione un Rol", "", "error");
    }
    else if (objPermiso.IdModule == "") {
        swal("Por favor seleccione un Modulo", "", "error");
    }
    else {
        $.ajax({
            url: "/Permiso/Create/",
            type: "POST",
            data: { 'objPermiso': objPermiso },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Permiso agregado con éxito!",
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



function detalle_permiso(data) {
    var IdPermiso = (data.getAttribute('id'));
    var IdRol = (data.getAttribute('data-name'));
    var IdModule = (data.getAttribute('data-name2'));
    var OptAdd = (data.getAttribute('data-name3'));
    var OptUpdate = (data.getAttribute('data-name4'));
    var OptDelete = (data.getAttribute('data-name5'));
    var OptSelect = (data.getAttribute('data-name6'));
    $.ajax({
        url: "/Permiso/getOneRol/",
        type: "GET",
        data: { 'IdRol': IdRol },
        success: function (data, textStatus, jqXHR) {
            $('#role_detalle').val(data.Nombre);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $.ajax({
        url: "/Permiso/getOneModule/",
        type: "GET",
        data: { 'IdModule': IdModule },
        success: function (data, textStatus, jqXHR) {
            $('#module_detalle').val(data.Description);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    
    if (OptAdd == "true") {
        $('#OptAdd_detalle').prop("checked", true);
    } else {
        $('#OptAdd_detalle').prop("checked", false);
    }
    if (OptUpdate == "true") {
        $('#OptUpdate_detalle').prop("checked", true);
        console.log('checado');
    } else {
        $('#OptUpdate_detalle').prop("checked", false);
    }
    if (OptDelete == "true") {
        $('#OptDelete_detalle').prop("checked", true);
        console.log('checado');
    } else {
        $('#OptDelete_detalle').prop("checked", false);
    }
    if (OptSelect == "true") {
        $('#OptSelect_detalle').prop("checked", true);
        console.log('checado');
    } else {
        $('#OptSelect_detalle').prop("checked", false);
    }
    $('#ModalDetalleRoles').modal('show');

}



function editar_permiso(data) {
    roleGetList(data.getAttribute('data-name'));
    moduleGetList(data.getAttribute('data-name2'))
    var OptAdd = (data.getAttribute('data-name3'));
    var OptUpdate = (data.getAttribute('data-name4'));
    var OptDelete = (data.getAttribute('data-name5'));
    var OptSelect = (data.getAttribute('data-name6'));
    var IdPermiso = (data.getAttribute('id'));
    $('#idPermiso_edit').val(IdPermiso);
    if (OptAdd == "true") {
        $('#OptAdd_editar').prop("checked", true);
    } else {
        $('#OptAdd_editar').prop("checked", false);
    }
    if (OptUpdate == "true") {
        $('#OptUpdate_editar').prop("checked", true);
    } else {
        $('#OptUpdate_editar').prop("checked", false);
    }
    if (OptDelete == "true") {
        $('#OptDelete_editar').prop("checked", true);
    } else {
        $('#OptDelete_editar').prop("checked", false);
    }
    if (OptSelect == "true") {
        $('#OptSelect_editar').prop("checked", true);
    } else {
        $('#OptSelect_editar').prop("checked", false);
    }
    $('#ModalEditarPermiso').modal('show');

}

function roleGetList(id) {
    $.ajax({
        url: "/Permiso/getAllRoles/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            $('#role_select option').remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].IdRol == id) {
                    $('#role_select').append('<option value="' + data[i].IdRol + '" selected>' + data[i].Nombre + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
function moduleGetList(id) {
    $.ajax({
        url: "/Permiso/getAllModules/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            $('#module_select option').remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].IdModule == id) {
                    $('#module_select').append('<option value="' + data[i].IdModule + '" selected>' + data[i].Description + '</option>');
                } else {
                    $('#module_select').append('<option value="' + data[i].IdModule + '">' + data[i].Description + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function enviar_editar_permiso() {

    if (document.getElementById('OptAdd_editar').checked) { var a = true } else { var a = false }
    if (document.getElementById('OptSelect_editar').checked) { var s = true } else { var s = false }
    if (document.getElementById('OptDelete_editar').checked) { var d = true } else { var d = false }
    if (document.getElementById('OptUpdate_editar').checked) { var u = true } else { var u = false }

    console.log($('#idPermiso_edit').val());
    var objPermiso = {};
    objPermiso.IdPermiso = $('#idPermiso_edit').val();
    objPermiso.OptAdd = a;
    objPermiso.OptSelect = s;
    objPermiso.OptDelete = d;
    objPermiso.OptUpdate = u;
    objPermiso.IdRol = $('#role_select').val();
    objPermiso.IdModule = $('#module_select').val();

    if (objPermiso.IdRol == "") {
        swal("Por favor seleccione un Rol", "", "error");
    }
    else if (objPermiso.IdModule == "") {
        swal("Por favor seleccione un Modulo", "", "error");
    }
    else {

        $.ajax({
            url: "/Permiso/Edit/",
            type: "POST",
            data: { 'objPermiso': objPermiso },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Permiso editado con éxito!",
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