$(document).ready(function () {
    LlenarTabla();


    $(".Agregar").click(function () {
        $('#ModalAgregarRelEncuestaCliente').modal('show');

    });

});


function LlenarTabla() {
    $('#TablaRelEncuestaClientes').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/RelEncuestaClientes/GetList/",
            "type": "POST",
            "data": { 'data': 1 },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "IdProceso", "orderable": false },
            { "data": "IdEncuesta", "orderable": false },
            { "data": "Contrato", "orderable": false },
            { "data": "FechaApli", "orderable": false },
      

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

    $("div.toolbar").html('<button class="btn btn-success btn-sm Agregar" style="float:right;" ><i class="fa fa-plus" aria-hidden="true"></i> Asignar Encuesta a Cliente </button> <div class="input-group input-group-sm"><input class="form-control" type="text"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');

}

function Opciones() {
    var botones = "<button class='btn btn-info btn-xs detalleRelEnCli' id='detalleRelEnCli'>Detalles</button> <button class='btn btn-warning btn-xs editarRelEnCli' id='editarConexion'>Editar</button> <button class='btn btn-danger btn-xs eliminarRelEnCli' id='eliminarConexion'> Eliminar</button> ";
    return botones;
}



//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones() {
    var opc =
   "<button class='btn btn-info btn-xs Detalle' type='button'>Detalles</button> <button class='btn btn-warning btn-xs Editar' type='button'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button> <button class='btn btn-danger btn-xs eliminar'  type='button'> <i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>"
    return opc;
}

$('#TablaRelEncuestaClientes').on('click', '.Detalle', function () {
    $('#ModalDetalleRelEncuestaCliente').modal('show');
});

$('#TablaRelEncuestaClientes').on('click', '.Editar', function () {
    $('#ModalDetalleRelEncuestaCliente').modal('show');
});

$('#TablaRelEncuestaClientes').on('click', '.Eliminar', function () {
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