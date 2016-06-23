$(document).ready(function () {
    LlenarTabla();


});

function Busqueda() {
    var cadena = $('#buscar').val();
    if(cadena == ""){
        LlenarTabla()
    }else{
        LlenarTabla(cadena)
    }
}

function LlenarTabla(cadena) {
    if(cadena == undefined){
        cadena = "";
    }
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
            "data": { 'data': cadena },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },

        "columns": [
            { "data": "IdConexion", "orderable": false },
            { "data": "Plaza", "orderable": false },
            { "data": "Servidor", "orderable": false },
            { "data": "BaseDeDatos", "orderable": false },
            { "data": "Usuario", "orderable": false },
            { "data": "Password", "orderable": false },

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
    $("div.toolbar").html('<button class="btn bg-olive btn-sm" style="float:right;" onclick="agregrarConexion()"><i class="fa fa-plug" aria-hidden="true"></i> Nueva Plaza</button> <div class="input-group input-group-sm"><input class="form-control" id="buscar" type="text"><span class="input-group-btn"><button onclick="Busqueda()" class="btn btn-info btn-flat" type="button"><i class="fa fa-search" aria-hidden="true"></i> Buscar</button></span></div>');
}

function Opciones(id) {
    var opc = "<button class='btn btn-warning btn-xs Editar' type='button' id='" + id.IdConexion + "' onclick='datosConexion(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button> <button class='btn btn-danger btn-xs' type='button' data-name='"+id.Plaza+"' id='" + id.IdConexion + "' onclick='eliminarConexion(this)'><i class='fa fa-trash' aria-hidden='true'></i> Eliminar</button>"
    return opc;
}

function agregrarConexion() {
    $('#ModalAgregarConexion').modal('show');
    $('#Nombreplaza').val('');
    $('#Servidor').val('');
    $('#base').val('');
    $('#user').val('');
    $('#pass').val('');
}

function datosConexion(e) {
    var id = e.getAttribute('id');
    $.ajax({
        url: "/Conexion/getConexionData/",
        type: "POST",
        data: {'id_conexion':id},
        success: function (data, textStatus, jqXHR) {
            console.log(data);  
            $('#idplaza_editar').val(data.IdConexion);
            $('#nombre_plaza_editar').val(data.Plaza);
            $('#intancia_editar').val(data.Servidor);
            $('#base_editar').val(data.BaseDeDatos);
            $('#usuario_editar').val(data.Usuario);
            $('#password_editar').val(data.Password);
            $('#ModalEditarConexion').modal('show');
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}


function eliminarConexion(e) {
    var id = e.getAttribute('id');
    var nombre = e.getAttribute('data-name');
    $('#idplaza_eliminar').val(id);
    $('#nombre_plaza').text(nombre);
    $('#ModaleliminarConexion').modal('show');
}


function deleteConexion() {
    var id = $('#idplaza_eliminar').val();
    $.ajax({
            url: "/Conexion/Delete/",
            type: "POST",
            data: { 'id': id },
            success: function (data, textStatus, jqXHR) {
                LlenarTabla();
                $('#ModaleliminarConexion').modal('hide');
                swal("Hecho!", "Plaza eliminada exitosamente!", "success");

            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });

}
$('#guardarConexion').click(function () {




    if ($('#Nombreplaza').val() == "") {
        sweetAlert("El nombre de plaza es requerido!", "", "error");
    }
    else if ($('#Servidor').val() == "") {
        sweetAlert("El nombre del servidor es requerido!", "", "error");
    }
    else if ($('#base').val() == "") {
        sweetAlert("El nombre de la base de datos es requerido!", "", "error");
    }
    else if ($('#user').val() == "") {
        sweetAlert("El nombre del usuario es requerido!", "", "error");
    }
    else if ($('#pass').val() == "") {
        sweetAlert("la contraseña es requerida!", "", "error");
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
                LlenarTabla();
                $('#ModalAgregarConexion').modal('hide');
                swal("Hecho!", "Plaza agregada exitosamente!", "success");

            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });


    }


});


function editarConexion() {
    var conexion = {};
    conexion.IdConexion = $('#idplaza_editar').val();
    conexion.Plaza = $('#nombre_plaza_editar').val();
    conexion.Servidor = $('#intancia_editar').val();
    conexion.BaseDeDatos = $('#base_editar').val();
    conexion.Usuario = $('#usuario_editar').val();
    conexion.Password = $('#password_editar').val();
    $.ajax({
        url: "/Conexion/Edit/",
        type: "POST",
        data: { 'conexion': conexion },
        success: function (data, textStatus, jqXHR) {
            $('#ModalEditarConexion').modal('hide');
            LlenarTabla();
            swal("Hecho!", "Plaza editada exitosamente!", "success");

        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}