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
    $('#TablaResOpcMults').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/ResOpcMults/GetList/",
            "type": "POST",
            "data": { 'data': cadena },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "Id_ResOpcMult", "orderable": false },
            { "data": "ResOpcMult", "orderable": false },
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

    $("div.toolbar").html('<div class="input-group input-group-sm"><input class="form-control" type="text" id="buscar"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button" onclick="Busqueda()">Buscar</button></span></div>');
    //Respaldo botn agregar
    //$("div.toolbar").html('<button class="btn btn-success btn-sm Agregar" style="float:right;" onclick="agregar()"><i class="fa fa-plus" aria-hidden="true"></i> Nueva ResOpcMult </button> <div class="input-group input-group-sm"><input class="form-control" type="text"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');

}

function Opciones() {
    var botones = "<button class='btn btn-info btn-xs detalleResOpcMult' id='detalleResOpcMult'>Detalles</button> <button class='btn btn-warning btn-xs editarResOpcMult' id='editarResOpcMult'>Editar</button>";
    return botones;
}




//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones(e) {
    var opc = "<button class='btn btn-warning btn-xs Editar' data-name='" + e.ResOpcMult + "' id='" + e.Id_ResOpcMult + "' type='button' onclick='editar_respuesta(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>"
    return opc;
}

