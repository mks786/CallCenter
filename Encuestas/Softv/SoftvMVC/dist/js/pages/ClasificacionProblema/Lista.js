$(document).ready(function () {
    LlenarTabla();
    
    $.ajax({
        url: "/ClasificacionProblema/getAllServicios/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            $('#tipo_servicio_select').empty();
            $('#tipo_servicio_select').append('<option selected disabled>--------------------------</option>');
            for (var i = 0; i < data.length; i++) {
                $('#tipo_servicio_select').append('<option value="' + data[i].descripcion + '"> ' + data[i].descripcion + '</option>');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
});



function LlenarTabla(cadena) {
    $('#ClasificacionProblem').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/ClasificacionProblema/GetList/",
            "type": "POST",
            "data": { 'cadena': cadena },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "ClvProblema", "orderable": false },
            { "data": "Descripcion", "orderable": false },

             {
                 sortable: false,
                 "render": function (data, type, full, meta) {
                     var ruta;
                     if (full.Activo == true) {
                         ruta = '/dist/img/true.png';
                     } else {
                         ruta = '/dist/img/check-false.png'
                     }
                     return "<p class='text-center'><img src='" + ruta + "' /></p>";  //Es el campo de opciones de la tabla.
                 }
             },
             { "data": "TipServ", "orderable": false },

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

    if (permiso_Problema == "False") {
        $("div.toolbar").html('<div class="input-group input-group-sm"><input class="form-control" type="text" id="buscar"><span class="input-group-btn"><button onclick="BuscarProblema()"; class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');
    } else {
        $("div.toolbar").html('<button class="btn btn-success Agregar" style="float:right;" onclick="agregar()"><i class="fa fa-plus" aria-hidden="true"></i> Nueva Clasificación Problema </button> <div class="input-group input-group-sm"><input class="form-control" type="text"  id="buscar"><span class="input-group-btn"><button onclick="BuscarProblema()"; class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');
    }

}



//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones(e) {
    var opc;
    if (Problema_editar == "False") {
        if (Problema_eliminar == "False") {
            opc = '';
        } else {
            opc = "<button class='btn btn-danger btn-xs eliminar'  type='button' data-name='" + e.Descripcion + "'data-name2='" + e.Activo + "' id='" + e.ClvProblema + "' > <i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>";
        }
    } else {
        if (Problema_eliminar == "False") {
            opc = "<button class='btn btn-warning btn-xs Editar' data-name='" + e.Descripcion + "'data-name2='" + e.Activo + "' id='" + e.ClvProblema + "' data-name3='" + e.TipServ + "' type='button' onclick='editar_Problema(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>";
        } else {
            opc = "<button class='btn btn-warning btn-xs Editar' data-name='" + e.Descripcion + "'data-name2='" + e.Activo + "' id='" + e.ClvProblema + "' data-name3='" + e.TipServ + "' type='button' onclick='editar_Problema(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button> <button class='btn btn-danger btn-xs eliminar'  type='button' data-name='" + e.Descripcion + "'data-name2='" + e.Activo + "' id='" + e.ClvProblema + "' > <i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>";
        }
    }

    return opc;
}



function BuscarProblema() {

    var cadena = $('#buscar').val();
    LlenarTabla(cadena);

}