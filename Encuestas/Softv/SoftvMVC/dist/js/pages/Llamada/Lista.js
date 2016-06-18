
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
    var botones = "<button class='btn btn-info btn-xs detalleLlamada' id='detalleLlamada'>Detalles</button> <button class='btn btn-warning btn-xs editarLlamada' id='editarLlamada'>Editar</button>";
    return botones;
}



//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones(id) {
    var opc = "<button class='btn btn-info btn-xs Detalle' type='button' id='" + id + "' onclick='MostrarDetalles(this)'>Detalles</button> <button class='btn btn-warning btn-xs Editar' type='button' id='" + id + "' onclick='editarLlamada(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>"
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
            console.log(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function editarLlamada(e) {
    $('#cargando').show();
    $('#panel_tabs_llamadas').hide();
    var llamada = e.getAttribute('id');
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/getOneCall/",
        type: "GET",
        data: { 'plaza': id_plaza, 'id_llamada': llamada },
        success: function (data, textStatus, jqXHR) {
            var contrato = data[0].Contrato;
            $('#detalle_editar').val(data[0].Detalle);
            $('#solucion_editar').val(data[0].Solucion);
            $('#contrato_editar').val(data[0].Contrato);
            $('#id_llamada').val(data[0].IdLlamada);
            var queja = data[0].Clv_Queja;
            
            $('#contrato_oculto').val(contrato);
            $('#queja_oculto').val(queja);
            if(queja > 0){
                $('#tab_queja').show();
                $('#id_queja').val(queja);
                getDatosQueja(queja);
                getTurno(data[0].IdTurno)
            } else {
                $('#tab_queja').hide();
            }
            if (contrato > 0) {
                $('#clasificacion_editar').show(); 
                $('#contrato_panel_editar').show(); 
                $('#celular_panel_editar').hide();
                $('#nombre_panel_editar').hide();
                $('#telefono_panel_editar').hide();
                $('#domicilio_panel_editar').hide();
                $('#email_panel_editar').hide(); 
                $('#clasificacion_solucion_editar').show();
                getSolucionProblemas(data[0].Clv_Problema);
                getClasificacion(data[0].Clv_Trabajo);
            } else {
                getNoCliente(data[0].IdLlamada);
                $('#clasificacion_editar').hide();
                $('#contrato_panel_editar').hide();
                $('#celular_panel_editar').show();
                $('#nombre_panel_editar').show();
                $('#telefono_panel_editar').show();
                $('#domicilio_panel_editar').show();
                $('#email_panel_editar').show();
                $('#clasificacion_solucion_editar').hide();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    setTimeout("mostrarDatos()", 1000);
}

function mostrarDatos() {
    $('#cargando').hide();
    $('#panel_tabs_llamadas').show();
}

function getNoCliente(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/getDatosNoCliente/",
        type: "GET",
        data: { 'plaza': id_plaza, 'llamada': id },
        success: function (data, textStatus, jqXHR) {
            $('#nombre_editar').val(data[0].Nombre);
            $('#domicilio_editar').val(data[0].Domicilio);
            $('#telefono_editar').val(data[0].Telefono);
            $('#celular_editar').val(data[0].Celular);
            $('#email_editar').val(data[0].Email);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getSolucionProblemas(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetClasficacionProblema/",
        type: "GET",
        data: { 'IdPlaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            $("#select_problemas option").remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].clvProblema == id) {
                    $('#select_problemas').append('<option value="'+data[i].clvProblema+'" selected>'+data[i].descripcion+'</option>');
                } else {
                    $('#select_problemas').append('<option value="' + data[i].clvProblema + '">' + data[i].descripcion + '</option>');
                }
                
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getClasificacion(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetClasficacionSolucion/",
        type: "GET",
        data: { 'IdPlaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            for (var i = 0; i < data.length; i++) {
                $("#select_soluciones option").remove();
                if (data[i].CLV_TRABAJO == id) {
                    $('#select_solucion').append('<option value="' + data[i].CLV_TRABAJO + '" selected>' + data[i].DESCRIPCION + '</option>');
                } else {
                    $('#select_solucion').append('<option value="' + data[i].CLV_TRABAJO + '">' + data[i].DESCRIPCION + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getDatosQueja(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/getDatosQueja/",
        type: "GET",
        data: { 'plaza': id_plaza, 'queja': id },
        success: function (data, textStatus, jqXHR) {
            getPrioridad(data[0].clvPrioridadQueja);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}


function getPrioridad(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetPrioridad/",
        type: "GET",
        data: { 'IdPlaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            $("#prioridad_editar option").remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].clvPrioridadQueja == id) {
                    $('#prioridad_editar').append('<option value="' + data[i].clvPrioridadQueja + '" selected>' + data[i].Descripcion + '</option>');
                } else {
                    $('#prioridad_editar').append('<option value="' + data[i].clvPrioridadQueja + '">' + data[i].Descripcion + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getTurno(id) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetTurno/",
        type: "GET",
        data: { 'IdPlaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            $("#turno_editar option").remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].IdTurno == id) {
                    $('#turno_editar').append('<option value="' + data[i].IdTurno + '" selected>' + data[i].Turno + '</option>');
                } else {
                    $('#turno_editar').append('<option value="' + data[i].IdTurno + '">' + data[i].Turno + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}


function guardarLlamada() {
    var id_plaza = $("#plaza_llamadas").val();
    var llamada = {};
    var contrato = $('#contrato_oculto').val();
    var queja = $('#queja_oculto').val();
    llamada.solucion = $('#solucion_editar').val();
    llamada.detalle = $('#detalle_editar').val();
    llamada.id_llamada = $('#id_llamada').val();
    llamada.contrato = $('#contrato_editar').val();
    if(contrato > 0){
        llamada.clas_problema = $('#select_problemas').val();
        llamada.clas_solucion = $('#select_solucion').val();
        if (queja > 0) {
            llamada.prioridad = $('#prioridad_editar').val();
            llamada.turno = $('#turno_editar').val();
            llamada.queja = queja;
        }
    } else {
        llamada.nombre = $('#nombre_editar').val();
        llamada.domicilio = $('#domicilio_editar').val();
        llamada.telefono = $('#telefono_editar').val();
        llamada.celular = $('#celular_editar').val();
        llamada.email = $('#email_editar').val();
    }
    console.log(llamada);
    $.ajax({
        url: "/Llamada/editarLLamada/",
        type: "POST",
        data: { 'plaza': id_plaza, 'llamada': llamada },
        success: function (data, textStatus, jqXHR) {
            $('#ModalEditarLlamada').modal("hide");
            LlenarTabla(id_plaza);
            swal("La llamada se guaró exitosamente", "", "success");
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}