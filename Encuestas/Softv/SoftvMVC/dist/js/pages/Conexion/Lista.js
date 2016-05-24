$(document).ready(function () {
    LlenarTabla();


    $(".Agregar").click(function () {
        $('#ModalAgregarConexion').modal('show');
        $('#Nombreplaza').val('');
        $('#Servidor').val('');
        $('#base').val('');
        $('#user').val('');
        $('#pass').val('');


    });

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
    $("div.toolbar").html('<button class="btn bg-olive Agregar" style="float:right;" onclick="MostrarModalConexion();" ><i class="fa fa-plug" aria-hidden="true"></i> Nueva conexión</button> <div class="input-group input-group-sm"><input class="form-control" id="abuscar" type="text"><span class="input-group-btn"><button onclick="BuscarEncuesta();" class="btn btn-info btn-flat" type="button">Buscar</button></span></div>');
}



$('#guardarConexion').click(function () {




    if ($('#Nombreplaza').val() == "") {
        sweetAlert("Oops...", "El nombre de plaza es requerido!", "error");
    }
    else if ($('#Servidor').val() == "") {
        sweetAlert("Oops...", "El nombre del servidor es requerido!", "error");
    }
    else if ($('#base').val() == "") {
        sweetAlert("Oops...", "El nombre de la base de datos es requerido!", "error");
    }
    else if ($('#user').val() == "") {
        sweetAlert("Oops...", "El nombre del usuario es requerido!", "error");
    }
    else if ($('#pass').val() == "") {
        sweetAlert("Oops...", "la contraseña es requerida!", "error");
    } else {



        var conexion = {};
        conexion.Plaza = $('#Nombreplaza').val();
        conexion.Servidor = $('#Servidor').val();
        conexion.BaseDeDatos = $('#base').val();
        conexion.Usuario = $('#user').val();
        conexion.Password = $('#pass').val();




        $.ajax({
            url: "/Conexion/AddConexion/",
            type: "POST",
            data: { 'conexion': conexion },
            success: function (data, textStatus, jqXHR) {
                console.log(data);
                // LlenaTabla();
                $('#ModalAgregarConexion').modal('hide');
                swal("Hecho!", "Plaza agregada exitosamente!", "success");

            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });


    }


});