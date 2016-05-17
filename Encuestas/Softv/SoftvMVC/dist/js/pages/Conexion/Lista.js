$(document).ready(function () {
    LlenarTabla();


    //$(".Agregar").click(function () {
    //    $('#ModalAgregarConexion').modal('show');

    //});

});


function LlenarTabla() {
    $('#TablaConexiones').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/Conexion/GetList/",
            "type": "POST",
            "data": { 'data': 1 },
        },


        "columns": [
            { "data": "IdConexion", "orderable": false },
            { "data": "Plaza", "orderable": false },
            { "data": "Servidor", "orderable": false },
            { "data": "BaseDeDatos", "orderable": false },
            { "data": "Usuario", "orderable": false },
            { "data": "Password", "orderable": false },
    
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
        },


        "order": [[0, "asc"]]
    })
    //Botones
}



//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones() {
    var opc = "<button class='btn btn-info btn-xs Detalle' type='button'>Detalles</button> <button class='btn btn-warning btn-xs Editar' type='button'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button> <button class='btn btn-danger btn-xs eliminar'  type='button'> <i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>"
    return opc;
}

$('#TablaConexiones').on('click', '.Detalle', function () {
    $('#ModalDetalleConexion').modal('show');
});

$('#TablaConexiones').on('click', '.Editar', function () {
    $('#ModalDetalleConexion').modal('show');
});

$('#TablaConexiones').on('click', '.Eliminar', function () {
    alert("click");
});



function ActualizaListaPreguntas() {
    $('#TbodyPreguntas').empty();
    for (var b = 0; b < Lista_preguntas.length; b++) {
        $('#tablaPreguntas').append("<tr><td>" + Lista_preguntas[b].Nombre + "</td><td>" + Lista_preguntas[b].TipoControl + "</td><td><button class='btn btn-info btn-xs detallepregunta' rel='" + Lista_preguntas[b].id + "'>Detalles</button> <button class='btn btn-warning btn-xs EditarPregunta ' rel='" + Lista_preguntas[b].id + "'>Editar</button> <button class='btn btn-danger btn-xs EliminaPregunta' rel='" + Lista_preguntas[b].id + "'>Eliminar</button></td></tr>");

    }

}

$('#TablaClientes').on('click', '.EliminaCliente', function () {
    var id = $(this).attr('rel');
    $('#ModalEliminaCliente').modal('show');
    //$('#contrato').val(id);

});


