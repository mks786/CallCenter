$(document).ready(function () {
    LlenarTabla();

    $(".Agregar").click(function () {
        $('#ModalAgregarUsuario').modal('show');

    });

});


function LlenarTabla() {
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
            "data": { 'data': 1 },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "IdUsuario", "orderable": false },
            { "data": "IdRol", "orderable": false },
            { "data": "Nombre", "orderable": false },
            { "data": "Email", "orderable": false },
            { "data": "Usuario", "orderable": false },
            { "data": "Password", "orderable": false },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    var status = full.Estado;
                    var res = status.toString().replace("true", "Activo").replace("false","Desactivado");
                    return res;
                }
            },
            {
            sortable: false,
            "render": function (data, type, full, meta) {
                return Opciones();  //Es el campo de opciones de la tabla.
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

    $("div.toolbar").html('<button class="btn btn-success btn-sm Agregar" style="float:right;" ><i class="fa fa-plus" aria-hidden="true"></i> Nuevo Usuario </button> <div class="input-group input-group-sm"><input class="form-control" type="text"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');

}

function Opciones() {
    var botones = "<button class='btn btn-info btn-xs detalleUsuario' id='detalleUsuario'>Detalles</button> <button class='btn btn-warning btn-xs editarUsuario' id='editarUsuario'>Editar</button> <button class='btn btn-danger btn-xs eliminarUsuario' id='eliminarUsuario'> Eliminar</button> ";
    return botones;
}








