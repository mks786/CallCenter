

$(document).ready(function () {
    LlenarTabla(5);

    /*DATOS GENERALES*/
    $('#contrato').val('');
    $('#nombre').val('');
    $('#calle').val('');
    $('#numero').val('');
    $('#cp').val('');
    $('#calles').val('');
    $('#colonia').val('');
    $('.telefono').mask('000-000-0000');
    $('#celular').mask('000-000-0000');
    $('#correo').val('');

    /*DATOS FISCALES*/
    $('#rfc').mask('AAAA-000-000');
    $('#curp').mask('AAAA-000000-AAAAAA-00');
    $('#rsocial').mask('AAAA-000000-AAAAAA-00');
    


});







$("#conexiones").change(function () {
    var id=$(this).val();
    LlenarTabla(id);


});



function LlenarTabla(con) {

    

    $('#TablaClientes').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/Cliente/GetList/",
            "type": "POST",
            "data": { 'data': con },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "CONTRATO", "orderable": false },
            { "data": "NOMBRE", "orderable": false },            
            { "data": "TELEFONO", "orderable": false },            
            { "data": "Email", "orderable": false },

        {
            sortable: false,
            "render": function (data, type, full, meta) {
                return "<button class='btn btn-info btn-xs detalleCliente' rel='" + full.conexion + "' id='"+full.CONTRATO+"'><i class='fa fa-info' aria-hidden='true'></i> Detalles</button> <button rel='" + full.conexion + "'class='btn btn-warning btn-xs editarCliente' id='"+full.CONTRATO+"'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>";
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

    //$("div.toolbar").html('<button class="btn btn-success btn-sm Agregar" style="float:right;" ><i class="fa fa-plus" aria-hidden="true"></i> Nuevo cliente</button> <div class="input-group input-group-sm"><input class="form-control" type="text"><span class="input-group-btn"><button class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');

}








$('#TablaClientes').on('click', '.detalleCliente', function () {
    $('#ModalDetalleCliente').modal('show');
});

$('#TablaClientes').on('click', '.editarCliente', function () {

    $('#contrato').val('');
    $('#nombre').val('');
    $('#calle').val('');
    $('#numero').val('');
    $('#cp').val('');
    $('#calles').val('');
    $('#colonia').val('');
    $('#telefono').val('');
    $('#celular').val('');
    $('#correo').val('');


   var id= $(this).attr('rel');
    var contrato=$(this).attr('id');
    $('#ModalEditarCliente').modal('show');

    $.ajax({
        url: "/CLIENTE/DetalleCliente/",
        type: "POST",
        data: { 'id': id, 'contrato': contrato },
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            console.log(data.CONTRATO);


            $('#contrato').val(data[0].CONTRATO);
            $('#nombre').val(data[0].NOMBRE);
            $('#calle').val(data[0].Clv_Calle);
            $('#numero').val(data[0].NUMERO);
            $('#cp').val(data[0].CodigoPostal);
            $('#calles').val(data[0].ENTRECALLES);
            $('#colonia').val(data[0].Clv_Colonia);
            $('#telefono').val(data[0].TELEFONO);
            $('#celular').val(data[0].CELULAR);
            $('#correo').val(data[0].Email);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });





});



$('#Editar').click(function () {

    var cliente = {};
    cliente.CONTRATO = $('#contrato').val();
    cliente.NOMBRE = $('#nombre').val();
    cliente.Clv_Calle = $('#calle').val();
    cliente.NUMERO = $('#numero').val();
    cliente.ENTRECALLES = $('#calles').val();
    cliente.Clv_Colonia = $('#colonia').val();
    cliente.CodigoPostal =  $('#cp').val();
    cliente.TELEFONO = $('#telefono').val();
    cliente.CELULAR = $('#celular').val();
    cliente.DESGLOSA_Iva = "";
    cliente.SoloInternet = "";
    cliente.eshotel ="";
    cliente.Clv_Ciudad =""; 
    cliente.Email = $('#correo').val();
    cliente.clv_sector = "";
    cliente.Clv_Periodo = "";
    cliente.Clv_Tap = "";
    cliente.Zona2 = "";
    
});

