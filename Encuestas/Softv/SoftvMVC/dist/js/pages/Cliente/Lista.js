$(document).ready(function () {


    LlenarTabla($("#conexiones").val(),"","","");

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
    LlenarTabla(id,"","","");


});

function LlenarTabla(plaza,contrato,cliente,direccion) {



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
            "data": { 'plaza': plaza,'contrato':contrato,'cliente':cliente,'direccion':direccion },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "CONTRATO", "orderable": false },
            { "data": "NOMBRE", "orderable": false },
            { "data": "Colonia", "orderable": false },
            { "data": "Calle", "orderable": false },

        {
            sortable: false,
            "render": function (data, type, full, meta) {
                
                return "<button class='btn btn-info btn-xs detalleCliente' rel='" + full.conexion + "' id='" + full.CONTRATO + "'><i class='fa fa-info' aria-hidden='true'></i> Detalles</button> <button rel='" + full.conexion + "'class='btn btn-warning btn-xs editarCliente' id='" + full.CONTRATO + "'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>";
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



$('#filtros').change(function () {
    var filtro = $(this).val();

    if (filtro == "1") {
        $('#Pcontrato').show();
        $('#Pdireccion').hide();
        $('#Pnombre').hide();
    }
    else if (filtro == "2") {
        $('#Pcontrato').hide();
        $('#Pdireccion').hide();
        $('#Pnombre').show();
    }
    else if (filtro == "3") {
        $('#Pcontrato').hide();
        $('#Pdireccion').show();
        $('#Pnombre').hide();
    }
    

});

$('#bcontrato').click(function () {
    
    LlenarTabla($("#conexiones").val(),$('#fcontrato').val(), "", "")
});

$('#bnombre').click(function () {

    LlenarTabla($("#conexiones").val(), "", $('#fnombre').val(), "")
});

$('#bdireccion').click(function () {

    LlenarTabla($("#conexiones").val(), "", "", $('#fdireccion').val())
});









