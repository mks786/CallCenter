
$(document).ready(function () {
    LlenarTabla();

    $(".Agregar").click(function () {
        $('#ModalAgregarLlamada').modal('show');

    });

});

function LlenarTabla() {
    $('#TablaLlamadas').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/Llamada/GetList/",
            "type": "POST",
            "data": { 'data': 1 },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "IdLlamada", "orderable": false },
            { "data": "IdUsuario", "orderable": false },
            { "data": "Tipo_Llamada", "orderable": false },
            { "data": "Contrato", "orderable": false },
            //{ "data": "Detalle", "orderable": false },
            //{ "data": "Solucion", "orderable": false },
            { "data": "Fecha", "orderable": false },
            //{ "data": "HoraInicio", "orderable": false },
            //{ "data": "HoraFin", "orderable": false },
            //{ "data": "IdTurno", "orderable": false },
            { "data": "IdQueja", "orderable": false },
            { "data": "IdConexion", "orderable": false },
            //{ "data": "Clv_Trabajo", "orderable": false },
            //{ "data": "Clv_TipSer", "orderable": false },
        

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

    $("div.toolbar").html('<a href="/Llamada/nueva" class="btn btn-success btn-sm" style="float:right;" ><i class="fa fa-plus" aria-hidden="true"></i> Nueva Llamada </a> <div class="input-group input-group-sm"><input class="form-control" type="text"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');

}

function Opciones() {
    var botones = "<button class='btn btn-info btn-xs detalleLlamada' id='detalleLlamada'>Detalles</button> <button class='btn btn-warning btn-xs editarLlamada' id='editarLlamada'>Editar</button> <button class='btn btn-danger btn-xs eliminarLlamada' id='eliminarLlamada'> Eliminar</button> ";
    return botones;
}



//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones() {
    var opc = "<button class='btn btn-info btn-xs Detalle' type='button'>Detalles</button> <button class='btn btn-warning btn-xs Editar' type='button'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button> <button class='btn btn-danger btn-xs eliminar'  type='button'> <i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>"
    return opc;
}

$('#TablaLlamadas').on('click', '.Detalle', function () {
    $('#ModalDetalleLlamada').modal('show');
});

$('#TablaLlamadas').on('click', '.Editar', function () {
    $('#ModalEditarLlamada').modal('show');
});

$('#TablaLlamadas').on('click', '.Eliminar', function () {
    alert("click");
});



//function ActualizaListaPreguntas() {
//    $('#TbodyPreguntas').empty();
//    for (var b = 0; b < Lista_preguntas.length; b++) {
//        $('#tablaPreguntas').append("<tr><td>" + Lista_preguntas[b].Nombre + "</td><td>" + Lista_preguntas[b].TipoControl + "</td><td><button class='btn btn-info btn-xs detallepregunta' rel='" + Lista_preguntas[b].id + "'>Detalles</button> <button class='btn btn-warning btn-xs EditarPregunta ' rel='" + Lista_preguntas[b].id + "'>Editar</button> <button class='btn btn-danger btn-xs EliminaPregunta' rel='" + Lista_preguntas[b].id + "'>Eliminar</button></td></tr>");

//    }

//}

//$('#TablaClientes').on('click', '.EliminaCliente', function () {
//    var id = $(this).attr('rel');
//    $('#ModalEliminaCliente').modal('show');
//    //$('#contrato').val(id);

//});