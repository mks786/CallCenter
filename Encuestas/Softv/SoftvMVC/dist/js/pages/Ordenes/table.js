function LlenarTabla(idPlaza, conOrd, status, nombre, paterno, materno, calle, numero) {
    $('#TablaOrdenesIndex tbody > tr').remove();
    $('#TablaOrdenesIndex').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": false,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/Ordenes/getListOrders/",
            "type": "GET",
            "data": { 'idPlaza': idPlaza, 'conOrd': conOrd, 'nombre': nombre, 'status': status, 'calle': calle, 'numero': numero, 'paterno':paterno, 'materno':materno },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "Clv_Orden", "orderable": false },
            { "data": "STATUS", "orderable": false },
            { "data": "Contrato", "orderable": false },
            { "data": "Nombre", "orderable": false },
            { "data": "CALLE", "orderable": false },
            { "data": "NUMERO", "orderable": false }
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


$('#TablaOrdenesIndex tbody').on('click', 'tr', function () {
    $('#seleccionTR').removeClass('alert-info');
    $('#seleccionTR').removeAttr('id');
    $(this).addClass('alert-info');
    $(this).attr('id','seleccionTR');
    var contrato = $(this).find("td:first").html();
    var status = $(this).find("td:eq(1)").html();
    angular.element($('#mostrandoController')).scope().getDetailsOrder(contrato, status);
});