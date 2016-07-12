$(document).ready(function () {
    LlenarTabla();

});



function LlenarTabla(cadena) {
    $('#TablaRoles').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/Role/GetList/",
            "type": "POST",
            "data": { 'cadena': cadena },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "IdRol", "orderable": false },
            { "data": "Nombre", "orderable": false },
            { "data": "Descripcion", "orderable": false },
            ////{
            ////    sortable: false,
            ////    "render": function (data, type, full, meta) {
            ////        console.log(full);                   
            ////    }
            ////},

        {
            sortable: false,
            "render": function (data, type, full, meta) {
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
        $("div.toolbar").html('<div class="input-group input-group-sm"><input class="form-control" type="text" id="buscar"><span class="input-group-btn"><button onclick="BuscarRole()"; class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');
    } else {
        $("div.toolbar").html('<button class="btn btn-success btn-sm Agregar" style="float:right;" onclick="agregar()"><i class="fa fa-plus" aria-hidden="true"></i> Nuevo Rol </button> <div class="input-group input-group-sm"><input class="form-control" type="text"  id="buscar"><span class="input-group-btn"><button onclick="BuscarRole()"; class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');
    }

}


//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones(e) {
    var opc;
    if (permiso_editar == "False") {
        if(permiso_eliminar == "False"){
            opc = "<button class='btn btn-info btn-xs Detalle' data-name='" + e.Nombre + "'data-name2='" + e.Descripcion + "' id='" + e.IdRol + "' onclick='detalle_role(this)' type='button'>Detalles</button>";
        } else {
            opc = "<button class='btn btn-info btn-xs Detalle' data-name='" + e.Nombre + "'data-name2='" + e.Descripcion + "' id='" + e.IdRol + "' onclick='detalle_role(this)' type='button'>Detalles</button> <button class='btn btn-danger btn-xs eliminar'  type='button' data-name='" + e.Nombre + "' id='" + e.IdRol + "' > <i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>";
        }
    } else {
        if (permiso_eliminar == "False") {
            opc = "<button class='btn btn-info btn-xs Detalle' data-name='" + e.Nombre + "'data-name2='" + e.Descripcion + "' id='" + e.IdRol + "' onclick='detalle_role(this)' type='button'>Detalles</button> <button class='btn btn-warning btn-xs Editar' data-name='" + e.Nombre + "'data-name2='" + e.Descripcion + "' id='" + e.IdRol + "' type='button' onclick='editar_role(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>";
        } else {
            opc = "<button class='btn btn-info btn-xs Detalle' data-name='" + e.Nombre + "'data-name2='" + e.Descripcion + "' id='" + e.IdRol + "' onclick='detalle_role(this)' type='button'>Detalles</button> <button class='btn btn-warning btn-xs Editar' data-name='" + e.Nombre + "'data-name2='" + e.Descripcion + "' id='" + e.IdRol + "' type='button' onclick='editar_role(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button> <button class='btn btn-danger btn-xs eliminar'  type='button' data-name='" + e.Nombre + "' id='" + e.IdRol + "' > <i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>";
        }
    }
    
    return opc;
}



function BuscarRole() {

    var cadena = $('#buscar').val();
    LlenarTabla(cadena);

}