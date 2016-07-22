$(document).ready(function () {
    LlenarTabla();

});

function Busqueda() {
    var cadena = $('#buscar').val();
    if(cadena == ""){
        LlenarTabla();
    } else {
        LlenarTabla(cadena);
    }
}

function LlenarTabla(cadena) {
    if(cadena == undefined){
        cadena = "";
    }
    $('#TablaUsuarios').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/Usuario/GetList/",
            "type": "POST",
            "data": { 'data': cadena },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "IdUsuario", "orderable": false },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    return full.Role.Nombre;  //Es el campo de opciones de la tabla.

                }
            },
            { "data": "Nombre", "orderable": false },
            { "data": "Email", "orderable": false },
            { "data": "Usuario", "orderable": false },
            { "data": "Password", "orderable": false },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    var ruta;
                    if (full.Estado == true) {
                        ruta = '/dist/img/true.png';
                    } else {
                        ruta = '/dist/img/check-false.png'
                    }
                    return "<p class='text-center'><img src='" + ruta + "' /></p>";  //Es el campo de opciones de la tabla.

                }
            },
            {
            sortable: false,
            "render": function (data, type, full, meta) {
                console.log(full);
                return Opciones(full);  //Es el campo de opciones de la tabla.
            }
        }
        ],

        language: {
            processing: "Procesando información...",
            search: "Buscar&nbsp;:",
            lengthMenu: "Mostrar _MENU_ Elementos",
            info: "Mostrando   _START_ de _END_ Total _TOTAL_ elementos",
            infoEmpty: "No hay elemetos para mostrar",
            infoFiltered: "(filtrados _MAX_ )",
            infoPostFix: "",
            loadingRecords: "Búsqueda en curso...",
            zeroRecords: "No hay registros para mostrar",
            emptyTable: "No hay registros disponibles",
            paginate: {
                first: "Primera",
                previous: "Anterior",
                next: "Siguiente",
                last: "Ultima"
            },
            aria: {
                sortAscending: ": activer pour trier la colonne par ordre croissant",
                sortDescending: ": activer pour trier la colonne par ordre décroissant"
            }
        },


        "order": [[0, "asc"]]
    })
    if (permiso_agregar == "False") {
        $("div.toolbar").html('<div class="input-group input-group-sm"><input class="form-control" type="text" id="buscar"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button" onclick="Busqueda()"><i class="fa fa-search" aria-hidden="true"></i> Buscar</button></span></div>');
    } else {
        $("div.toolbar").html('<button class="btn btn-success" style="float:right;" onclick="agregarUsuario()"><i class="fa fa-user-plus" aria-hidden="true"></i> Nuevo Usuario </button> <div class="input-group input-group-sm"><input class="form-control" type="text" id="buscar"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button" onclick="Busqueda()"><i class="fa fa-search" aria-hidden="true"></i> Buscar</button></span></div>');
    }

}

function agregarUsuario() {
    $('#Nombre').val('');
    $('#Username').val('');
    $('#email').val('');
    $('#pass').val('');
    $('#cpass').val('');
    $('#rol').val('0').change();
    var checked = $("#Status").parent('[class*="icheckbox"]').hasClass("checked");
    if (checked) {
        $("#Status").removeClass('checked');
    }
    $('#ModalAgregarUsuario').modal('show');
}

function Opciones(full) {
    var botones;
    if (permiso_editar == "False") {
        if (permiso_eliminar == "False") {
            botones = "";
        }else{
            botones = "<button class='btn btn-danger btn-xs' id='" + full.IdUsuario + "' data-name='" + full.Nombre + "' onclick='eliminarUsuario(this)'><i class='fa fa-trash' aria-hidden='true'></i> Eliminar</button> ";
         }
    } else {
        if (permiso_eliminar == "False") {
            botones = "<button class='btn btn-warning btn-xs' id='" + full.IdUsuario + "' onclick='datosUsuario(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>";
        } else {
            botones = "<button class='btn btn-warning btn-xs' id='" + full.IdUsuario + "' onclick='datosUsuario(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button> <button class='btn btn-danger btn-xs' id='" + full.IdUsuario + "' data-name='" + full.Nombre + "' onclick='eliminarUsuario(this)'><i class='fa fa-trash' aria-hidden='true'></i> Eliminar</button> ";
        }

    }
    return botones;
}

function datosUsuario(e) {
    var id = e.getAttribute('id');
    $.ajax({
        url: "/Usuario/getUsuarionData/",
        type: "POST",
        data: { 'id_usuario': id },
        success: function (data, textStatus, jqXHR) {
            $('#id_editar').val(data.IdUsuario);
            $('#Nombre_editar').val(data.Nombre);
            $('#Username_editar').val(data.Usuario);
            $('#Email_editar').val(data.Email);
            $('#pass_editar').val(data.Password);
            $('#cpass_editar').val(data.Password);
            $('#rol_editar').val(data.IdRol).change();
            var status = data.Estado;
            if(status){
                $('input').iCheck('check');
            } else {
                $('input').iCheck('uncheck');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('#ModalEditarUsuario').modal('show');
}
function editarUsuario() {
    var usuario = {};
    usuario.IdUsuario = $('#id_editar').val();
    usuario.Nombre = $('#Nombre_editar').val();
    usuario.Usuario = $('#Username_editar').val();
    usuario.Email = $('#Email_editar').val();
    if ($('#pass_editar').val() != $('#cpass_editar').val()) {
        swal("Las contraseñas no coinciden", "", "error");
    }else{
        usuario.Password = $('#pass_editar').val();
        usuario.IdRol = $('#rol_editar').val();
        var checked = $("#Status_editar").parent('[class*="icheckbox"]').hasClass("checked");
        if (checked) {
            usuario.Estado = true;
        }
        else {
            usuario.Estado = false;
        }
        $.ajax({
            url: "/Usuario/Update/",
            type: "POST",
            data: { 'usuario': usuario },
            success: function (data, textStatus, jqXHR) {
                LlenarTabla();
                $('#ModalEditarUsuario').modal('hide');
                swal("Hecho!", "Usuario editado exitosamente!", "success");
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });

    }
    
}

function eliminarUsuario(e) {
    var id = e.getAttribute('id');
    var nombre = e.getAttribute('data-name');
    $('#eliminarUsuario_id').val(id);
    $('#nombre_usuario').text(nombre);
    $('#ModalEliminarUsuario').modal('show');
}

function deleteUsuario() {
    var id = $('#eliminarUsuario_id').val();
    $.ajax({
        url: "/Usuario/Delete/",
        type: "POST",
        data: { 'id': id },
        success: function (data, textStatus, jqXHR) {
            LlenarTabla();
            $('#ModalEliminarUsuario').modal('hide');
            swal("Hecho!", "Usuario eliminada exitosamente!", "success");
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

}

function GuardaUsuario() {
    if ($('#Nombre').val() == "") {
        swal("Define un nombre para el usuario", "", "error");
    }
    else if ($('#email').val() == "") {
        swal("Define un correo para el usuario", "", "error");
    }
    else if ($('#pass').val() == "") {
        swal("Define un contraseña para el usuario", "", "error");
    }
    else if ($('#cpass').val() == "") {
        swal("Confirma contraseña", "", "error");
    }
    else if ($('#pass').val() != $('#cpass').val()) {
        swal("Las contraseñas no coinciden", "", "error");
    }
    else if ($('#rol').val() == null) {
        swal("Define un rol para el usuario", "", "error");
    }
    else {
        var objUsuario = {};
        objUsuario.IdRol = $('#rol').val(); 
        objUsuario.Nombre = $('#Nombre').val();
        objUsuario.Usuario = $('#Username').val();
        objUsuario.Email = $('#email').val();
        objUsuario.Password = $('#pass').val();
        var checked = $("#Status").parent('[class*="icheckbox"]').hasClass("checked");
        if (checked) {
            objUsuario.Estado = true;
        }
        else {
            objUsuario.Estado = false;
        }

        console.log(objUsuario);
        $.ajax({
            url: "/Usuario/CreateUser/",
            type: "POST",
            data: { 'objUsuario': objUsuario },
            success: function (data, textStatus, jqXHR) {
                LlenarTabla();
                $('#ModalAgregarUsuario').modal('hide');
                swal("Hecho!", "Usuario guardado exitosamente!", "success");
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });

    }
}







