
$(document).ready(function () {    
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
        $("#panel_busqueda").show();
        $("#tipo_llamada").removeAttr('disabled');
        var url = '/Llamada/nueva?id_plaza='+$(this).val();
        $('#nueva_llamada_link').attr('href',url);
        $('#nueva_llamada_link').removeClass('disabled');
        
    });
    $("#tipo_llamada").change(function () {
        $('.collapse').collapse('hide');
        var tipo = $(this).val();
        var id_plaza = $("#plaza_llamadas").val();
        var cliente;
        if(tipo == 1){
            cliente = true;
        }else if(tipo == 2){
            cliente = false;
        }else{
            cliente = '';
        }
        LlenarTabla(id_plaza, '', '', '', cliente);
    });
});

function buscar_por_id() {
    $('.collapse').collapse('hide');
    var id_llamada = $('#input_llamada').val();
    var id_plaza = $("#plaza_llamadas").val();
    if (id_llamada == "") {
        swal("El No. de contrato no puede ir en blanco ", "", "danger");
    } else {
        LlenarTabla(id_plaza, '', '', id_llamada, '');
    }
}

function buscar_por_nombre() {
    $('.collapse').collapse('hide');
    var nombre = $('#nombre_individual').val();
    var id_plaza = $("#plaza_llamadas").val();
    if (nombre == "") {
        swal("El nombre no puede ir en blanco ", "", "danger");
    } else {
        LlenarTabla(id_plaza, '', nombre, '', '');
    }
}

function buscar_por_contrato() {
    $('.collapse').collapse('hide');
    var contrato = $('#input_contrato').val();
    var id_plaza = $("#plaza_llamadas").val();
    if (contrato == "") {
        swal("El No. de contrato no puede ir en blanco ", "", "danger");
    } else {
        LlenarTabla(id_plaza, contrato, '', '', '');
    }
}

function LlenarTabla(idplaza,contrato,cadena,id_llamada,tipo_llamada) {

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
            "url": "/DatosLlamada/GetList/",
            "type": "POST",
            "data": { 'idplaza': idplaza, 'cadena': cadena, 'id_llamada': id_llamada, 'tipo_llamada': tipo_llamada, 'contrato': contrato },
        },
        "fnInitComplete": function (oSettings, json) {


        },
        "fnDrawCallback": function (oSettings) {

        },


        "columns": [
            { "data": "IdLlamada", "orderable": false },
            { "data": "Usuario", "orderable": false },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    var cliente;
                    if (full.TipoLlamada == true) {
                        cliente = 'Es Cliente';
                    } else {
                        cliente = 'No Es Cliente';
                    }
                    return ("<td>"+cliente+"<td>");  //Es el campo de opciones de la tabla.
                }
            },
            //{ "data": "Tipo_Llamada", "orderable": false },
            { "data": "Contrato", "orderable": false },
            { "data": "Nombre", "orderable": false },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    var fecha = full.Fecha;
                    fecha = fecha.split(" ", 1); 
                    
                    return ("<td>"+fecha+"<td>");  //Es el campo de opciones de la tabla.
                }
            },
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

//funcion:retorna las opciones que tendra cada row en la tabla principal
function Opciones(id) {
    var opc;
    if (permiso_editar == "False") {
        opc = "<button class='btn btn-info btn-xs Detalle' type='button' id='" + id + "' onclick='MostrarDetalles(this)'><i class='fa fa-info' aria-hidden='true'></i> Detalles</button>";
    } else {
        opc = "<button class='btn btn-info btn-xs Detalle' type='button' id='" + id + "' onclick='MostrarDetalles(this)'><i class='fa fa-info' aria-hidden='true'></i> Detalles</button> <button class='btn btn-warning btn-xs Editar' type='button' id='" + id + "' onclick='editarLlamada(this)'><i class='fa fa-pencil' aria-hidden='true'></i> Editar</button>";
    }
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
            var problemaSolucion = data[0].ProblemaSolucion;
            if (problemaSolucion == true) {
                $('#pan_solucion').show();
            } else {
                $('#pan_solucion').hide();
            }
            if (queja < 1) {
                $('#pan_queja').hide();
            } else {
                $('#queja').text(queja);
                $('#pan_queja').show();
            }
            if (contrato == "") {
                $('#pan_contrato').hide();
                $('#pan_detalle').show();
                $('#pan_nombre').show();
                $('#pan_domicilio').show();
                $('#pan_telefono').show();
                $('#pan_celular').show();
                $('#pan_email').show();
                $('#nombre_completo').text(data[0].nombre);
                $('#domicilio').text(data[0].domicilio);
                $('#telefono').text(data[0].telefono);
                $('#celular').text(data[0].celular);
                $('#email').text(data[0].email);
            } else {
                $('#pan_contrato').show();
                $('#pan_detalle').show();
                $('#pan_nombre').show();
                $('#pan_domicilio').hide();
                $('#pan_telefono').hide();
                $('#pan_celular').hide();
                $('#pan_email').hide()
            }
            $('#contrato_detalle').text(contrato);
            var fecha = data[0].Fecha; 
            var inicio = data[0].HoraInicio;
            var fin = data[0].HoraFin;
            $('#fecha').text(fecha);
            $('#hora_inicio').text(inicio);
            $('#hora_fin').text(fin);
            $('#detalle').text(data[0].Detalle);
            $('#solucion').text(data[0].Solucion);
            $('#nombre_completo').text(data[0].nombre);
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
            console.log(data);
            var contrato = data[0].Contrato;
            $('#detalle_editar').val(data[0].Detalle);
            $('#solucion_editar').val(data[0].Solucion);
            $('#contrato_editar').val(data[0].Contrato);
            $('#id_llamada').val(data[0].IdLlamada);
            var queja = data[0].Clv_Queja;
            var problemaSolucion = data[0].ProblemaSolucion;
            if (problemaSolucion == true) {
                $('#clasificacion_editar').show();
                $('#panel_solucion').show();
                $('#clasificacion_solucion_editar').show();
                $('#panel_tipo_informacion').hide(); 
                $('#guardar_llamada').attr('data-problem', problemaSolucion);
            } else if (problemaSolucion == false) {
                $('#clasificacion_editar').hide();
                $('#panel_solucion').hide();
                $('#clasificacion_solucion_editar').hide(); 
                $('#panel_tipo_informacion').show();
                $('#guardar_llamada').attr('data-problem', problemaSolucion);
                $.ajax({
                    url: "/Llamada/TipoLlamada/",
                    type: "GET",
                    success: function (datas, textStatus, jqXHR) {
                        $('#tipo_informacion_editar option').remove();
                        for (var i = 0; i < datas.length; i++) {
                            if (datas[i].clv_motivo == data[0].Clv_Motivo) {
                                $('#tipo_informacion_editar').append('<option value="' + datas[i].clv_motivo + '" selected>' + datas[i].descricpion + '</option>');
                            } else {
                                $('#tipo_informacion_editar').append('<option value="' + datas[i].clv_motivo + '">' + datas[i].descricpion + '</option>');
                            }
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {

                    }
                });
            } else if (problemaSolucion == null) {
                $('#panel_solucion').hide();
                $('#clasificacion_editar').hide();
                $('#panel_solucion').hide();
                $('#clasificacion_solucion_editar').hide();
                $('#panel_tipo_informacion').hide();
                $('#guardar_llamada').removeAttr('data-problem');
            }
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
            if (contrato != "") {          
                $('#contrato_panel_editar').show(); 
                $('#celular_panel_editar').hide();
                $('#nombre_panel_editar').hide();
                $('#telefono_panel_editar').hide();
                $('#domicilio_panel_editar').hide();
                $('#email_panel_editar').hide(); 
                getClasificacionSolucion(data[0].Clv_TipSer, data[0].Clv_Trabajo);
                getClasificacionProblemas(data[0].Clv_Problema);
            } else {
                $('#telefono_editar').inputmask("(999)9999999");
                $('#celular_editar').inputmask("999-999-99-99");
                $('#nombre_editar').val(data[0].nombre);
                $('#domicilio_editar').val(data[0].domicilio);
                $('#telefono_editar').val(data[0].telefono);
                $('#celular_editar').val(data[0].celular);
                $('#email_editar').val(data[0].email);
                $('#clasificacion_editar').hide();
                $('#contrato_panel_editar').hide();
                $('#celular_panel_editar').show();
                $('#nombre_panel_editar').show();
                $('#telefono_panel_editar').show();
                $('#domicilio_panel_editar').show();
                $('#email_panel_editar').show();
                $('#panel_solucion').hide();
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

function getClasificacionSolucion(id, select) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetClasficacionSolucion/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, 'idServicio': id },
        success: function (data, textStatus, jqXHR) {
            $("#select_solucion option").remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].CLV_TRABAJO == select) {
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
function getClasificacionProblemas(select) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetClasficacionProblema/",
        type: "GET",
        data: { 'IdPlaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            $("#select_problemas option").remove();
            for (var i = 0; i < data.length; i++) {
                if (data[i].clvProblema == select) {
                    $('#select_problemas').append('<option value="' + data[i].clvProblema + '" selected>' + data[i].descripcion + '</option>');
                } else {
                    $('#select_problemas').append('<option value="' + data[i].clvProblema + '">' + data[i].descripcion + '</option>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getClasificacion(id, idServicio) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/tblClasificacionProblema/GetClasficacionSolucion/",
        type: "GET",
        data: { 'IdPlaza': id_plaza,'idServicio':idServicio  },
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


function guardarLlamada(e) {
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
        var clv_motivo = $('#tipo_informacion_editar').val();
        var problemasolucion = e.getAttribute('data-problem');
        if(problemasolucion == "false"){
            llamada.clv_motivo = clv_motivo;
        }
        if (queja > 0) {
            llamada.prioridad = $('#prioridad_editar').val();
            llamada.turno = $('#turno_editar').val();
            llamada.queja = queja;
        }
    } else {
        llamada.nombre = $('#nombre_editar').val();
        llamada.domicilio = $('#domicilio_editar').val();
        llamada.telefono = $('#telefono_editar').inputmask('unmaskedvalue');
        llamada.celular = $('#celular_editar').inputmask('unmaskedvalue');
        llamada.email = $('#email_editar').val();
    }
    $.ajax({
        url: "/Llamada/editarLLamada/",
        type: "POST",
        data: { 'plaza': id_plaza, 'llamada': llamada },
        success: function (data, textStatus, jqXHR) {
            $('#ModalEditarLlamada').modal("hide");
            LlenarTabla(id_plaza,'','',data,'');
            swal("La llamada se guaró exitosamente", "", "success");
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}