
$(document).ready(function () {
    //LlenarTabla();
    $(".Agregar").click(function () {
        $('#ModalAgregarLlamada').modal('show');

    });
    $.ajax({
        url: "/Conexion/ListaConexiones/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            for (var i = 0; i < data.length; i++) {
                $('#plaza_llamadas').append($('<option>', {
                    value: data[i].IdConexion,
                    text: data[i].Plaza
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $("#plaza_llamadas").change(function () {
        var id_plaza = $("#plaza_llamadas").val();
        LlenarTabla(id_plaza);
    });
});

function LlenarTabla(idplaza) {
    $('#TablaLlamadas tbody > tr').remove();
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
            "data": { 'idplaza': idplaza },
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
            { "data": "Fecha", "orderable": false },
            { "data": "IdConexion", "orderable": false },
        

        {
            sortable: false,
            "render": function (data, type, full, meta) {
                return Opciones(full.IdLlamada);  //Es el campo de opciones de la tabla.
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

    

}

function Opciones() {
    var botones = "<button class='btn btn-info btn-xs detalleLlamada' id='detalleLlamada'>Detalles</button> <button class='btn btn-warning btn-xs editarLlamada' id='editarLlamada'>Editar</button> <button class='btn btn-danger btn-xs eliminarLlamada' id='eliminarLlamada'> Eliminar</button> ";
    return botones;
}



//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones(id) {
    var opc = "<button class='btn btn-info btn-xs Detalle' type='button' id='"+id+"' onclick='MostrarDetalles(this)'>Detalles</button> <button class='btn btn-warning btn-xs Editar' type='button'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button> <button class='btn btn-danger btn-xs eliminar'  type='button'> <i class='fa fa-trash-o' aria-hidden='true'></i> Eliminar</button>"
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


function MostrarDetalles(e){
    var llamada = e.getAttribute('id');
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/getOneCall/",
        type: "GET",
        data: { 'plaza': id_plaza, 'id_llamada': llamada },
        success: function (data, textStatus, jqXHR) {
            $('#id_llamda').text(data[0].IdLlamada);
            var contrato = data[0].Contrato;
            var queja = data[0].Clv_Queja;
            if (queja < 1) {
                $('#pan_queja').hide();
            } else {
                $('#queja').text(queja);
                $('#pan_queja').show();
            }
            if (contrato == 0) {
                getNombreCliente(data[0].IdLlamada);
                $('#pan_contrato').hide();
                $('#pan_detalle').show();
                $('#pan_solucion').show();
                $('#pan_nombre').show();
                $('#pan_domicilio').show();
                $('#pan_telefono').show();
                $('#pan_celular').show();
                $('#pan_email').show();
                //ajax a la de no clientes
            } else {
                $('#pan_contrato').show();
                $('#pan_detalle').show();
                $('#pan_solucion').show();
                $('#pan_nombre').hide();
                $('#pan_domicilio').hide();
                $('#pan_telefono').hide();
                $('#pan_celular').hide();
                $('#pan_email').hide()
            }
            $('#contrato').text(contrato);
            var fecha = data[0].Fecha.split(" ", 1); 
            var inicio = data[0].HoraInicio.substring(19, 11);
            var fin = data[0].HoraFin.substring(19, 11);
            $('#fecha').text(fecha);
            $('#hora_inicio').text(inicio);
            $('#hora_fin').text(fin);
            $('#detalle').text(data[0].Detalle);
            $('#solucion').text(data[0].Solucion);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getNombreCliente(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/getDatosNoCliente/",
        type: "GET",
        data: { 'plaza': id_plaza , 'llamada': id},
        success: function (data, textStatus, jqXHR) {
            $('#nombre').text(data[0].Nombre);
            $('#domicilio').text(data[0].Domicilio);
            $('#telefono').text(data[0].Telefono);
            $('#celular').text(data[0].Celular);
            $('#email').text(data[0].Email);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}