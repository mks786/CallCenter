$(document).ready(function () {
    LlenarTabla();

});


function LlenarTabla(cadena) {
    $('#TablaPermisos').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/Permiso/GetList/",
            "type": "POST",
            "data": { 'cadena': cadena },
        },


        "columns": [
            //{ "data": "IdPermiso", "orderable": false },
            { "data": "Rol", "orderable": false },
            { "data": "Modulo", "orderable": false },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    var ruta;
                    if(full.OptAdd == true){
                        ruta = '/dist/img/true.png';
                    }else{
                        ruta = '/dist/img/check-false.png'
                    }
                    return "<img src='" + ruta + "' />";  //Es el campo de opciones de la tabla.
                }
            },
             {
                 sortable: false,
                 "render": function (data, type, full, meta) {
                     var ruta;
                     if (full.OptUpdate == true) {
                         ruta = '/dist/img/true.png';
                     } else {
                         ruta = '/dist/img/check-false.png'
                     }
                     return "<img src='" + ruta + "' />";  //Es el campo de opciones de la tabla.
                 }
             },
             {
                 sortable: false,
                 "render": function (data, type, full, meta) {
                     var ruta;
                     if (full.OptDelete == true) {
                         ruta = '/dist/img/true.png';
                     } else {
                         ruta = '/dist/img/check-false.png'
                     }
                     return "<img src='" + ruta + "' />";  //Es el campo de opciones de la tabla.
                 }
             },
             {
                 sortable: false,
                 "render": function (data, type, full, meta) {
                     var ruta;
                     if (full.OptSelect == true) {
                         ruta = '/dist/img/true.png';
                     } else {
                         ruta = '/dist/img/check-false.png'
                     }
                     return "<img src='" + ruta + "' />";  //Es el campo de opciones de la tabla.
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
    });
    if (permiso_agregar == "False") {
        $("div.toolbar").html('<div class="input-group input-group-sm"><input class="form-control" id="buscar" type="text"><span class="input-group-btn"><button onclick="BuscarPermiso();" class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');

    } else {
        $("div.toolbar").html('<button class="btn btn-success Agregar" style="float:right;" onclick="agregar();" ><i class="fa fa-key" aria-hidden="true"></i> Nuevo Permiso</button> <div class="input-group input-group-sm"><input class="form-control" id="buscar" type="text"><span class="input-group-btn"><button onclick="BuscarPermiso();" class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');

    }
}


//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones(e) {
    var opc;
    if (permiso_editar == "False") {
        opc = "<button class='btn btn-info btn-xs Detalle' data-name='" + e.IdRol + "'data-name2='" + e.IdModule + "'data-name3='" + e.OptAdd + "'data-name4='" + e.OptUpdate + "'data-name5='" + e.OptDelete + "'data-name6='" + e.OptSelect + "' id='" + e.IdPermiso + "' onclick='detalle_permiso(this)' type='button'><i class='fa fa-info' aria-hidden='true'></i> Detalles</button>";
    } else {
        opc = "<button class='btn btn-info btn-xs Detalle' data-name='" + e.IdRol + "'data-name2='" + e.IdModule + "'data-name3='" + e.OptAdd + "'data-name4='" + e.OptUpdate + "'data-name5='" + e.OptDelete + "'data-name6='" + e.OptSelect + "' id='" + e.IdPermiso + "' onclick='detalle_permiso(this)' type='button'><i class='fa fa-info' aria-hidden='true'></i> Detalles</button> <button class='btn btn-warning btn-xs Editar' data-name='" + e.IdRol + "'data-name2='" + e.IdModule + "'data-name3='" + e.OptAdd + "'data-name4='" + e.OptUpdate + "'data-name5='" + e.OptDelete + "'data-name6='" + e.OptSelect + "' id='" + e.IdPermiso + "'  type='button' onclick='editar_permiso(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>";
    }
    return opc;
}


function BuscarPermiso() {
    var cadena = $('#buscar').val();
    LlenarTabla(cadena);
}


