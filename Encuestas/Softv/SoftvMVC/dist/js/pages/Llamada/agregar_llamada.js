var contrato_enviar;
var tiene_queja_aux = 0;
var nombre;
var colonia;
var calle;
var numero;
var nombre_enviar;
var telefono;
var celular;
$(document).ready(function () {
    $.ajax({
        url: "/CIUDAD/getAllCiudades/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            for (var i = 0; i < data.length; i++) {
                $('#plaza_llamadas').append($('<option>', {
                    value: data[i].IdPlaza,
                    text: data[i].Ciudad
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('#select_soluciones').on('change', function () {

        var option = $(this).val();
        if (option == 0) {
            $('#btn_queja').removeClass('disabled');
        } else {
            $('#btn_queja').addClass('disabled');
        }
        
    });
    $('#telefono').inputmask("(999)9999999");
    $('#celular').inputmask("999-999-99-99");
    $('.collapse').collapse('show');
    $("#plaza_llamadas").change(function () {
        $("#panel_busqueda").show();
        $("#tipo_llamada").removeAttr('disabled');
        llenarDireccion($(this).val());
    }); 
    $("#tipo_queja").change(function () {
        var filtro = $(this).val();
        var id_plaza = $("#plaza_llamadas").val();
        $.ajax({
            url: "/Llamada/HistorialQuejasL/",
            type: "GET",
            data: { 'plaza': id_plaza, 'contrato': contrato_enviar, 'filtro': filtro },
            success: function (data, textStatus, jqXHR) {
                $('#tableHistorialQuejas tbody').empty();
                for (var i = 0; i < data.length; i++) {
                    $('#tableHistorialQuejas tbody').append('<tr  onclick="detalleQueja(' + data[i].Queja + ')"><td>' + data[i].Queja + '</td><td>' + data[i].Status + '</td><td>' + data[i].Contrato + '</td><td>' + data[i].Nombre + '</td><td>' + data[i].Calle + '</td><td>' + data[i].Numero + '</td><td>' + data[i].TipSer + '</td><td><button class="btn btn-sm btn-priamry" onclick="detalleQueja(' + data[i].Queja + ')"> Ver</button></td></tr>');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    });
    $("#tipo_llamada").change(function () {
        var tipo = $(this).val();
        if (tipo == 1) {
            $('#titulo_llamada').text(' > Información');
            $('#btn_queja').addClass('disabled');
        } else {
            $('#btn_queja').removeClass('disabled');
            $('#titulo_llamada').text(' > Atención Telefónica');
        }
        $('#nueva_llamada_link').removeClass('disabled');
    });
    $("#tipo_cliente").change(function () {
        var tipo = $(this).val();
        if (tipo == 1) {
            $("#tipo_llamante").hide();
            tipoServicio();
            $('#motivo_llamada_panel').show();
            $('#collapseOne').collapse('show');
            $('#btn_queja').addClass('disabled');
        } else {
            $("#tipo_llamante").hide();
            $("#panel_no_cliente").show();
            $("#telefono_no_cliente").show();
            $("#email_no_cliente").show();
            $("#domicilio_no_cliente").show();
            $.ajax({
                url: "/Llamada/TipoLlamada/",
                type: "GET",
                success: function (data, textStatus, jqXHR) {
                    $('#tipo_informacion option').remove();
                    for (var i = 0; i < data.length; i++) {
                        $('#tipo_informacion').append($('<option>', {
                            value: data[i].clv_motivo,
                            text: data[i].descricpion
                        }));
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {

                }
            });
            $('#panel_tipo_informacion').show(); 
            $('#panel_detalle_informacion').show();
            $('#btn_enviar').show();
            var currentdate = new Date();
            var minutos;
            if (currentdate.getMinutes() < 10) {
                minutos = "0" + currentdate.getMinutes();
            } else {
                minutos = currentdate.getMinutes();
            }
            var hora = currentdate.getHours() + ":" + minutos + ":" + currentdate.getSeconds();
            var fecha = currentdate.getDate() + "/" + (currentdate.getMonth() + 1) + "/" + currentdate.getFullYear();
            $('#hora').val(hora);
            $('#fecha').val(fecha); 
            $('#panel_hora_fecha').show();
        }
    });
    $('#tipo_servicio').on('change', function () {
        $('#panel_llamante').show();
    });
});

function tipoServicio() {
    var plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/CLIENTE/getTipoServicio/",
        type: "GET",
        data: { "IdPlaza": plaza },
        success: function (data, textStatus, jqXHR) {
            // una vez que fue exitosa la peticion asignamos los datos a un select
            $('#tipo_servicio').empty();
            $('#tipo_servicio').append('<option disabled selected>----------------</option>');
            for (var i = 0; i < data.length; i++) {
                $('#tipo_servicio').append($('<option>', {
                    value: data[i].Clv_TipSer,
                    text: data[i].Concepto
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function llenarDireccion(id_plaza) {
    $.ajax({
        url: "/CIUDAD/GetCiudadByPlaza/",
        type: "POST",
        data: { 'plaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            for (var a = 0; a < data.length; a++) {
                $('#ciudad_select').append("<option id='" + data[a].Clv_Ciudad + "' value='" + data[a].Clv_Ciudad + "'>" + data[a].Nombre + "</option>");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('#ciudad_select').on('change', function () {
        cambiarColonia($(this).val());
    });
    $("#colonia_select").change(function () {
        cambiarCalle($(this).val());
    });
    function cambiarColonia(id) {
        $.ajax({
            url: "/COLONIA/GetColoniaByCiudad/",
            type: "POST",
            data: { 'idciudad': id, 'plaza': id_plaza },
            success: function (data, textStatus, jqXHR) {
                $('#colonia_select').find('option').remove().end();
                $('#calle_editar').empty();
                for (var i = 0; i < data.length; i++) {
                    $('#colonia_select').append("<option id='seleccion' value='" + data[i].clv_colonia + "' selected>" + data[i].Nombre + "</option>");
                    if (data.length == 1) {
                        $('#colonia_select').val(data[i].clv_colonia).change();
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }
    function cambiarCalle(id) {
        $.ajax({
            url: "/CALLE/GetCalleByColonia/",
            type: "POST",
            data: { 'colonia': id, 'plaza': id_plaza },
            success: function (data, textStatus, jqXHR) {
                $('#calle_editar').empty();
                for (var i = 0; i < data.length; i++) {
                    $('#calle_editar').append("<option id='seleccion' value='" + data[i].Clv_Calle + "' selected>" + data[i].Nombre + "</option>");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }
}

function nueva_llamada() {
    var ciudad = $("#plaza_llamadas").val()
    $.ajax({
        url: "/Conexion/listaPlazas/",
        type: "GET",
        data: { "idPlaza": ciudad },
        success: function (data, textStatus, jqXHR) {
            var ciudad_select = $("#plaza_llamadas option:selected").text();
            $('#nombre_plaza').text("CIUDAD DE " + ciudad_select.toUpperCase() + ", SERVIDOR " + data.Plaza.toUpperCase());
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    regresar();
    var plaza = $("#plaza_llamadas").val();
    $('#regresar').show();
    
    var tipo = $("#tipo_llamada").val();
    $('#datosGrafica').collapse('hide');
    $('#Mygrafica').hide();
    if(tipo == 1){
        $("#tipo_llamante").show();
        $('#sub_titulo').text("Llamada de Información");
        
    } else {
        tipoServicio();
        $("#motivo_llamada_panel").show();
        $('#sub_titulo').text("Llamada de Atención Telefónica");
    }
}


$('#buscar_por_contrato').on('click', function () {
    $('#collapseOne').collapse('hide');
    var contrato = $('#input_contrato').val();
    var id_plaza = $("#plaza_llamadas").val();
    var ciudad_flitro = $("#plaza_llamadas option:selected").text();
    var servicio = $('#tipo_servicio').val();
    $.ajax({
        url: "/CLIENTE/GetClientesLlamada/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "contrato": contrato,'servicio':servicio,'filtro':ciudad_flitro},
        success: function (data, textStatus, jqXHR) {
            if (data.length == 0) {
                $('#panel_tabla_clientes').hide();
                $('#invalido').show();
            } else {         
                $('#Tabla_Clientes tbody > tr').remove();
                var tipo_llamada = $('#tipo_llamada').val();
                if (tipo_llamada == 1) {
                    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><a id="' + data[0].CONTRATO + '" data-name="' + data[0].Nombre + '" data-colonia="' + data[0].Colonia + '" data-calle="' + data[0].calle + '" data-telefono="' + data[0].Telefono + '"  data-celular="' + data[0].Celular + '" data-numero="' + data[0].Numero + '" class="btn btn-success" onclick="selecion_cliente(this)">Seleccionar</a></td></tr>');
                } else {
                    //if (data[0].STATUS == 'B') {
                    //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><p style="color:red;">BAJA</td></tr>');
                    //} else if (data[0].STATUS == 'S') {
                    //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><p style="color:red;">SUSPENDIDO</td></tr>');
                    //} else if (data[0].STATUS == 'C') {
                    //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><p style="color:red;">CONTRATADO</td></tr>');
                    //} else if (data[0].STATUS == 'F') {
                    //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><p style="color:red;">FUERA DE AREA</td></tr>');
                    //} else if (data[0].STATUS == 'T') {
                    //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><p style="color:red;">SUSPENDIDO TEMPORAL</td></tr>');
                    //} else {
                    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><a id="' + data[0].CONTRATO + '" data-name="' + data[0].Nombre + '" data-colonia="' + data[0].Colonia + '" data-calle="' + data[0].calle + '" data-telefono="' + data[0].Telefono + '"  data-celular="' + data[0].Celular + '" data-numero="' + data[0].Numero + '" data-status="' + data[0].STATUS + '" class="btn btn-success" onclick="selecion_cliente(this)">Seleccionar</a></td></tr>');
                    //}
                }
                $('#invalido').hide();
                $('#panel_tabla_clientes').show();
            }
        },
        error: function (data, jqXHR, textStatus) {
            console.log(data);
        }

    });
});
$('#buscar_por_nombre').on('click', function () {
    $('#collapseOne').collapse('hide');
    var id_plaza = $("#plaza_llamadas").val(); nombre_individual
    var servicio = $('#tipo_servicio').val();
    var Nombrecliente = $('#nombre_individual').val();
    var ciudad_flitro = $("#plaza_llamadas option:selected").text();
    $.ajax({
        url: "/CLIENTE/GetClientesLlamada/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "Nombrecliente": Nombrecliente, 'servicio': servicio, 'filtro': ciudad_flitro },
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            if (data.length == 0) {
                $('#panel_tabla_clientes').hide();
                $('#invalido').show();
            } else {
                $('#invalido').hide();
                $('#panel_tabla_clientes').show();
                $('#Tabla_Clientes tbody > tr').remove();
                var tipo_llamada = $('#tipo_llamada').val();
                for (var i = 0; i < data.length; i++) {
                    if (tipo_llamada == 1) {
                        $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].Nombre + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].calle + '</td><td>' + data[i].Numero + '</td><td><a id="' + data[i].CONTRATO + '" data-name="' + data[i].Nombre + '" data-colonia="' + data[i].Colonia + '" data-calle="' + data[i].calle + '" data-telefono="' + data[i].Telefono + '"  data-celular="' + data[i].Celular + '" data-numero="' + data[i].Numero + '" class="btn btn-success" onclick="selecion_cliente(this)">Seleccionar</a></td></tr>');
                    } else {
                        //if (data[i].STATUS == 'B') {
                        //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].Nombre + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].calle + '</td><td>' + data[i].Numero + '</td><td><p style="color:red;">BAJA</td></tr>');
                        //} else if (data[i].STATUS == 'S') {
                        //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].Nombre + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].calle + '</td><td>' + data[i].Numero + '</td><td><p style="color:red;">SUSPENDIDO</td></tr>');
                        //} else if (data[i].STATUS == 'C') {
                        //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].Nombre + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].calle + '</td><td>' + data[i].Numero + '</td><td><p style="color:red;">CONTRATADO</td></tr>');
                        //} else if (data[i].STATUS == 'F') {
                        //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].Nombre + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].calle + '</td><td>' + data[i].Numero + '</td><td><p style="color:red;">FUERA DE AREA</td></tr>');
                        //} else if (data[i].STATUS == 'T') {
                        //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].Nombre + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].calle + '</td><td>' + data[i].Numero + '</td><td><p style="color:red;">SUSPENDIDO TEMPORAL</td></tr>');
                        //} else {
                        $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].Nombre + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].calle + '</td><td>' + data[i].Numero + '</td><td><a id="' + data[i].CONTRATO + '" data-name="' + data[i].Nombre + '" data-colonia="' + data[i].Colonia + '" data-calle="' + data[i].calle + '" data-telefono="' + data[i].Telefono + '"  data-celular="' + data[i].Celular + '" data-numero="' + data[i].Numero + '" data-status="' + data[i].STATUS + '" class="btn btn-success" onclick="selecion_cliente(this)">Seleccionar</a></td></tr>');
                        //}
                    }
                }
            }
        },
        error: function (data, jqXHR, textStatus) {
            console.log(data);
        }

    });
});
$('#buscar_por_direccion').on('click', function () {
    $('#collapseOne').collapse('hide');
    var id_plaza = $("#plaza_llamadas").val();
    var servicio = $('#tipo_servicio').val();
    var ciudad = $("#ciudad_select option:selected").text();
    var colonia = $("#colonia_select option:selected").text();
    var calle = $("#calle_editar option:selected").text();
    var numero = $("#numero_domicilio").val();
    var ciudad_flitro = $("#plaza_llamadas option:selected").text();
    $.ajax({
        url: "/CLIENTE/GetClientesLlamada/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "ciudad": ciudad, 'servicio': servicio, 'colonia': colonia, 'calle': calle, 'numero': numero, 'filtro': ciudad_flitro },
        success: function (data, textStatus, jqXHR) {
            if (data.length == 0) {
                $('#panel_tabla_clientes').hide();
                $('#invalido').show();
            } else {
                $('#invalido').hide();
                $('#panel_tabla_clientes').show();
                $('#Tabla_Clientes tbody > tr').remove();
                var tipo_llamada = $('#tipo_llamada').val();
                if (tipo_llamada == 1) {
                    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><a id="' + data[0].CONTRATO + '" data-name="' + data[0].Nombre + '" data-colonia="' + data[0].Colonia + '" data-calle="' + data[0].calle + '" data-telefono="' + data[0].Telefono + '"  data-celular="' + data[0].Celular + '" data-numero="' + data[0].Numero + '" class="btn btn-success" onclick="selecion_cliente(this)">Seleccionar</a></td></tr>');
                } else {
                    //if (data[0].STATUS == 'B') {
                    //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><p style="color:red;">BAJA</td></tr>');
                    //} else if (data[0].STATUS == 'S') {
                    //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><p style="color:red;">SUSPENDIDO</td></tr>');
                    //} else if (data[0].STATUS == 'C') {
                    //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><p style="color:red;">CONTRATADO</td></tr>');
                    //} else if (data[0].STATUS == 'F') {
                    //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><p style="color:red;">FUERA DE AREA</td></tr>');
                    //} else if (data[0].STATUS == 'T') {
                    //    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><p style="color:red;">SUSPENDIDO TEMPORAL</td></tr>');
                    //} else {
                    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].Nombre + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].calle + '</td><td>' + data[0].Numero + '</td><td><a id="' + data[0].CONTRATO + '" data-name="' + data[0].Nombre + '" data-colonia="' + data[0].Colonia + '" data-calle="' + data[0].calle + '" data-telefono="' + data[0].Telefono + '"  data-celular="' + data[0].Celular + '" data-numero="' + data[0].Numero + '" data-status="' + data[0].STATUS + '" class="btn btn-success" onclick="selecion_cliente(this)">Seleccionar</a></td></tr>');
                    //}
                }
            }
        },
        error: function (data, jqXHR, textStatus) {
            console.log(data);
        }

    });
});


function selecion_cliente(e) {
    var tipo_llamada = $('#tipo_llamada').val();
    var status = e.getAttribute('data-status');
    if (tipo_llamada == 1) {
        $('#guardar_llamada').removeClass('disabled');
        //$('#btn_queja').removeClass('disabled');
    } else {
        if (status == 'I') {
            $('#guardar_llamada').removeClass('disabled');
            $('#btn_queja').removeClass('disabled');
        } else {
            $('#guardar_llamada').addClass('disabled');
            $('#btn_queja').addClass('disabled');
            if (status == 'B') {
                new PNotify({
                    title: 'El cliente está en baja',
                    text: 'El cliente está en baja, por tal motivo no se puede atender su llamada en esta sección',
                    icon: 'fa fa-info-circle',
                    type: 'warning',
                    hide: false
                });
            }else if(status == 'S'){
                new PNotify({
                    title: 'El cliente está suspendido',
                    text: 'El cliente está suspendido, por tal motivo no se puede atender su llamada en esta sección',
                    icon: 'fa fa-info-circle',
                    type: 'warning',
                    hide: false
                });
            } else if (status == 'T') {
                new PNotify({
                    title: 'El cliente está temporalmente suspendido',
                    text: 'El cliente está temporalmente suspendido, por tal motivo no se puede atender su llamada en esta sección',
                    icon: 'fa fa-info-circle',
                    type: 'warning',
                    hide: false
                });
            }else if(status == 'C'){
                new PNotify({
                title: 'El cliente está contratado',
                text: 'El cliente está contratado, por tal motivo no se puede atender su llamada en esta sección',
                icon: 'fa fa-info-circle',
                type: 'warning',
                hide: false
            });
            }
            
        }
        
    }
    //selecion_cliente
    $('#panel_botones').show();
    $('#btn_enviar').show();
    var currentdate = new Date();
    var minutos;
    if (currentdate.getMinutes() < 10) {
        minutos = "0" + currentdate.getMinutes();
    } else {
        minutos = currentdate.getMinutes();
    }
    var hora = currentdate.getHours() + ":" + minutos + ":" + currentdate.getSeconds();
    var fecha = currentdate.getDate() + "/" + (currentdate.getMonth() + 1) + "/" + currentdate.getFullYear();
    $('#hora').val(hora);
    $('#fecha').val(fecha);
    $('#panel_hora_fecha').show();
    $('#motivo_llamada_panel').hide(); 
    var contrato = e.getAttribute('id');
    $('#panel_tipo_llamada_cliente').show();
    document.getElementById("servicios_contratados").innerHTML = "";
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/TipoLlamada/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            $('#tipo_informacion option').remove();
            for (var i = 0; i < data.length; i++) {
                $('#tipo_informacion').append($('<option>', {
                    value: data[i].clv_motivo,
                    text: data[i].descricpion
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $.ajax({
        url: "/Llamada/TieneQueja/",
        type: "GET",
        data: { 'id_plaza': id_plaza, "contrato": contrato },
        success: function (data, textStatus, jqXHR) {
            if (data.length > 0) {
                new PNotify({
                    title: 'El cliente tiene una queja pendiente',
                    text: 'El número de contrato ' + contrato + ' tiene una queja pendiente con el número ' + data[0].Clv_Queja,
                    icon: 'fa fa-info-circle',
                    type: 'info',
                    hide: false
                });
                $('#panel_tiene_queja').show();
                if ($('#tipo_llamada').val() == 2) {
                    $('#tiene_queja').text('El número de contrato: ' + contrato + ' tiene una queja pendiente con el número: ' + data[0].Clv_Queja);
                } else {
                    $('#tiene_queja').text('');
                }
                tiene_queja_aux = 1;
            } else {
                $('#panel_tiene_queja').hide();
                tiene_queja_aux = 0;
            }
        },
        error: function (data, jqXHR, textStatus) {
            console.log(data);
        }

    });
    $.ajax({
        url: "/Llamada/getTreeView/",
        type: "GET",
        data: { 'plaza': id_plaza, "contrato": contrato },
        success: function (data, textStatus, jqXHR) {
            for (var i = 0; i < data.length; i++) {
                $('#servicios_contratados').append('<p style="color:#4b646f; font-size:12px;">' + data[i].cadena + '</p>');
            }
        },
        error: function (data, jqXHR, textStatus) {
            console.log(data);
        }

    });
    if ($('#tipo_llamada').val() == 1) {
        contrato_enviar = e.getAttribute('id');
        $('#texto_contrato').html("");
        $('#texto_nombre').html("");
        $('#texto_telefono').html("");
        $('#texto_celular').html("");
        $('#texto_colonia').html("");
        $('#texto_calle').html("");
        $('#texto_numero').html("");
         nombre = e.getAttribute('data-name');
         colonia = e.getAttribute('data-colonia');
         calle = e.getAttribute('data-calle');
         numero = e.getAttribute('data-numero');
         telefono = e.getAttribute('data-telefono');
         celular = e.getAttribute('data-celular');
        $('#texto_contrato').append("<strong>Contrato: </strong>" + e.getAttribute('id'));
        $('#texto_nombre').append('<strong>Nombre: </strong>' + e.getAttribute('data-name'));
        $('#texto_colonia').append('<strong>Colonia: </strong>' + e.getAttribute('data-colonia'));
        $('#texto_telefono').append('<strong>Teléfono: </strong>' + e.getAttribute('data-telefono'));
        $('#texto_celular').append('<strong>Celular: </strong>' + e.getAttribute('data-celular'));
        $('#texto_calle').append("<strong>Calle: </strong> " + e.getAttribute('data-calle'));
        $('#texto_numero').append("<strong>Número: </strong>" + e.getAttribute('data-numero'));
        $('#panel_tabla_clientes').hide();
        $('#panel_llamante').hide();
        $('#panel_datos_cliente').show();
        $('#motivo_llamada_cliente').hide();
        $('#panel_tipo_informacion').show();
        $('#panel_detalle_informacion').show();
        $('#btn_enviar').show();
    } else {
        var tipo_servicio_texto = $('#tipo_servicio option:selected').text();
        var tipo_servicio = $('#tipo_servicio').val();
        $.ajax({
            url: "/tblClasificacionProblema/GetClasficacionProblema/",
            type: "GET",
            data: { 'IdPlaza': id_plaza, 'idServicio': tipo_servicio_texto },
            success: function (data, textStatus, jqXHR) {
                $("#select_problemas option").remove();
                $('#select_problemas').append('<option selected disabled>----------------------------</option>');
                for (var i = 0; i < data.length; i++) {
                    $('#select_problemas').append($('<option>', {
                        value: data[i].ClvProblema,
                        text: data[i].Descripcion
                    }));
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
        
        $.ajax({
            url: "/tblClasificacionProblema/GetClasficacionSolucion/",
            type: "GET",
            data: { 'IdPlaza': id_plaza, 'idServicio': tipo_servicio },
            success: function (data, textStatus, jqXHR) {
                $("#select_soluciones option").remove();
                $('#select_soluciones').append('<option value="0"selected>----------------------------</option>');
                for (var i = 0; i < data.length; i++) {
                    $('#select_soluciones').append($('<option>', {
                        value: data[i].CLV_TRABAJO,
                        text: data[i].DESCRIPCION
                    }));
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
        $('#busqueda_collapse').removeClass('collapsed');
        $('#texto_contrato').html("");
        $('#texto_nombre').html("");
        $('#texto_telefono').html("");
        $('#texto_celular').html("");
        $('#texto_colonia').html("");
        $('#texto_calle').html("");
        $('#texto_numero').html("");
        $('#panel_datos_cliente').show();
        $('#texto_contrato').append('<strong>Contrato: </strong>' + e.getAttribute('id'));
        contrato_enviar = e.getAttribute('id');
        nombre = e.getAttribute('data-name');
        colonia = e.getAttribute('data-colonia');
        calle = e.getAttribute('data-calle');
        numero = e.getAttribute('data-numero');
        telefono = e.getAttribute('data-telefono');
        celular = e.getAttribute('data-celular');
        $('#texto_nombre').append('<strong>Nombre: </strong>' + e.getAttribute('data-name'));
        $('#texto_telefono').append('<strong>Teléfono: </strong>' + e.getAttribute('data-telefono'));
        $('#texto_celular').append('<strong>Celular: </strong>' + e.getAttribute('data-celular'));
        $('#texto_colonia').append('<strong>Colonia: </strong>' + e.getAttribute('data-colonia'));
        $('#texto_calle').append("<strong>Calle: </strong> " + e.getAttribute('data-calle'));
        $('#texto_numero').append("<strong>Número: </strong>" + e.getAttribute('data-numero'));
        $('#panel_tabla_clientes').hide();
        $('#panel_llamante').hide(); 
        $('#panel_reporte_cliente').show(); 
        $('#panel_clasificacion').show(); 
        $('#panel_problema_real').show(); 
        $('#panel_solucion').show();
    }
    
}

function guardarLlamada() {
    var tipo = $('#tipo_llamada').val();
    var llamada = {};
    var id_plaza = $("#plaza_llamadas").val();
    var ciudad = $("#plaza_llamadas option:selected").text();
    llamada.IdConexion = id_plaza;
    llamada.usuario = usuario;
    llamada.ciudad = ciudad;
    var currentdate = new Date();
    var minutos;
    if (currentdate.getMinutes() < 10) {
        minutos = "0" + currentdate.getMinutes();
    } else {
        minutos = currentdate.getMinutes();
    }
    var hora_fin = currentdate.getHours() + ":" + minutos + ":" + currentdate.getSeconds();
    llamada.fecha = $('#fecha').val();
    llamada.horainicio = $('#fecha').val() + " " + $('#hora').val();
    llamada.horatermino = $('#fecha').val() + " " + hora_fin;
    llamada.tipo_llamada = tipo;
    llamada.nombre = nombre;
    llamada.telefono = telefono;
    llamada.celular = celular;
    if (tipo == 1) {
        var es_cliente = $('#tipo_cliente').val();
        if (es_cliente == 1) {
            llamada.contrato = contrato_enviar;
            llamada.Clv_TipSer = $('#tipo_servicio').val();
            if ($('#tipo_informacion').val() == null) {
                swal("Seleccione el tipo de información", "", "error");
            } else {
                llamada.tipo_informacion = $('#tipo_informacion').val();
                if ($('#detalle_llamada_informacion').val() == "") {
                    swal("El detalle de la llamada es obligatorio", "", "error");
                } else {
                    llamada.motivo = $('#detalle_llamada_informacion').val();
                    llamada.queja = 2;
                    llamada.tipo_llamada_cliente = true;
                    $.ajax({
                        url: "/Llamada/InsertLlamada",
                        type: "POST",
                        data: { 'IdPlaza': id_plaza, "llamada": llamada },
                        success: function (data, textStatus, jqXHR) {
                            PNotify.removeAll();
                            $('#regresar').hide(); 
                            $('#sub_titulo').hide(); 
                            $('#panel_botones').hide(); 
                            $('#Mygrafica').show();
                            $('#panel_tiene_queja').hide();
                            $('#nombre_plaza').text("");
                            $('#motivo_llamada_panel').hide();
                            $('#panel_llamante').hide();
                            $('#panel_tabla_clientes').hide();
                            $('#invalido').hide();
                            $('#panel_datos_cliente').hide();
                            $('#panel_hora_fecha').hide();
                            $('#panel_tipo_informacion').hide();
                            $('#panel_detalle_informacion').hide();
                            $('#btn_enviar').hide();
                            $('.collapse').collapse('show');
                            document.getElementById("llamadaForm").reset();
                            swal("La llamada se guardo exitosamente", "", "success");
                        },
                        error: function (data, jqXHR, textStatus) {
                            swal("Error la llamada no se guardo", "", "danger");
                        }

                    });
                }

            }
        } else {
            if ($('#nombre_no_cliente').val() == "") {
                swal("El nombre es obligatorio", "", "error");
            } else {
                llamada.nombre = $('#nombre_no_cliente').val();
                if ($('#tipo_informacion').val() == null) {
                    swal("Selecciona el tipo de información", "", "error");
                } else {
                    if ($('#detalle_llamada_informacion').val() == "") {
                        swal("El detalle de la llamada es obligatorio", "", "error");
                    } else {
                        llamada.telefono = $('#telefono').inputmask('unmaskedvalue');
                        llamada.celular = $('#celular').inputmask('unmaskedvalue');
                        llamada.email = $('#email').val();
                        llamada.domicilio = $('#domicilio').val();
                        llamada.tipo_informacion = $('#tipo_informacion').val();
                        llamada.motivo = $('#detalle_llamada_informacion').val();
                        llamada.tipo_llamada_cliente = false;
                        $.ajax({
                            url: "/Llamada/InsertLlamada",
                            type: "POST",
                            data: { 'IdPlaza': id_plaza, "llamada": llamada },
                            success: function (data, textStatus, jqXHR) {
                                PNotify.removeAll();
                                $('#sub_titulo').hide();
                                $('#panel_botones').hide();
                                $('#Mygrafica').show();
                                $('#regresar').hide();
                                $('#panel_tiene_queja').hide();
                                $('#nombre_plaza').text("");
                                $('#panel_no_cliente').hide();
                                $('#telefono_no_cliente').hide();
                                $('#email_no_cliente').hide();
                                $('#domicilio_no_cliente').hide();
                                $('#panel_tipo_informacion').hide();
                                $('#panel_detalle_informacion').hide();
                                $('#panel_hora_fecha').hide();
                                $('#btn_enviar').hide();
                                $('.collapse').collapse('show');
                                document.getElementById("llamadaForm").reset();
                                swal("La llamada se gaurdo exitosamente", "", "success");
                            },
                            error: function (data, jqXHR, textStatus) {
                                swal("Error la llamada no se guardo", "", "error");
                            }

                        });
                    }
                }
            }
        }
        
    } else {
        llamada.contrato = contrato_enviar;
        llamada.Clv_TipSer = $('#tipo_servicio').val();
        llamada.motivo = $('#reporte_cliente').val();
        llamada.clas_problema = $('#select_problemas').val();
        llamada.clas_solucion = $('#select_soluciones').val();
        llamada.solucion = $('#problema_real').val();
        llamada.tipo_llamada_cliente = true;
        llamada.queja == 0;
        if($('#reporte_cliente').val() == ""){
            swal("Se requiere la descripción del problema", "", "error");
        } else if ($('#select_problemas').val() == null) {
            swal("Seleccione la clasificación del problema", "", "error");
        } else if ($('#problema_real').val() == "") {
            swal("Se requiere el problema real", "", "error");
        } else if ($('#select_soluciones').val() == null) {
            swal("Seleccione la clasificación de la solución", "", "error");
        } else {
            $.ajax({
                url: "/Llamada/InsertLlamada",
                type: "POST",
                data: { 'IdPlaza': id_plaza, "llamada": llamada },
                success: function (data, textStatus, jqXHR) {
                    modalSinQueja(data.reporte);
                    $('#sub_titulo').hide();
                    $('#panel_botones').hide();
                    $('#Mygrafica').show();
                },
                error: function (data, jqXHR, textStatus) {
                    swal("Error la llamada no se guardo", "", "error");
                }

            });
        }
    }
}

function modalSinQueja(reporte) {
    $('#no_reporte_sin').text(reporte);
    $('#sinQueja').modal('show');
}

function GuardarQueja() {
    var comenatrio = $("#comentario").val();
    var turno = $("#turno").val();
    var prioridad = $("#prioridad").val();
    if(turno == null){
        swal("Seleccione el turno", "", "error");
    } else {
        var tipo = $('#tipo_llamada').val();
        var llamada = {};
        var id_plaza = $("#plaza_llamadas").val();
        llamada.comentario = comenatrio;
        llamada.IdConexion = id_plaza;
        llamada.usuario = usuario;
        llamada.nombre = nombre;
        llamada.telefono = telefono;
        llamada.celular = celular;
        var ciudad = $("#plaza_llamadas option:selected").text();
        llamada.ciudad = ciudad;
        var currentdate = new Date();
        var minutos;
        if (currentdate.getMinutes() < 10) {
            minutos = "0" + currentdate.getMinutes();
        } else {
            minutos = currentdate.getMinutes();
        }
        var hora_fin = currentdate.getHours() + ":" + minutos + ":" + currentdate.getSeconds();
        llamada.fecha = $('#fecha').val();
        llamada.horainicio = $('#fecha').val() + " " + $('#hora').val();
        llamada.horatermino = $('#fecha').val() + " " + hora_fin;
        llamada.tipo_llamada = tipo;
        llamada.queja = 1;
        llamada.tipo_llamada_cliente = true; 
        llamada.motivo = $('#reporte_cliente').val(); 
        llamada.clas_problema = $('#select_problemas').val();
        llamada.prioridad = prioridad;
        llamada.Clv_TipSer = $('#tipo_servicio').val();
        llamada.contrato = contrato_enviar;
        llamada.solucion = $('#problema_real').val();
        llamada.IdTurno = turno;
        $.ajax({
            url: "/Llamada/InsertLlamada",
            type: "POST",
            data: { 'IdPlaza': id_plaza, "llamada": llamada },
            success: function (data, textStatus, jqXHR) {
                $('#queja_modal').modal('hide');
                modalExito(data.reporte, data.queja);
                $('#sub_titulo').hide();
                $('#panel_botones').hide();
                $('#Mygrafica').show();
                $("#comentario").val(" ");
            },
            error: function (data, jqXHR, textStatus) {
                swal("Error la llamada no se guardo", "", "error");
            }

        });

        
    }
}

function modalExito(reporte,queja) {
    $('#no_reporte').text(reporte);
    $('#no_queja').text(queja);
    $('#exito').modal('show');
}

function abrir_modal() {
    var id_plaza = $("#plaza_llamadas").val();
    //$.ajax({
    //    url: "/tblClasificacionProblema/GetDepartamentoResponsable/",
    //    type: "GET",
    //    data: { 'IdPlaza': id_plaza },
    //    success: function (data, textStatus, jqXHR) {
    //        $('#departamento').empty();
    //        $('#departamento').append('<option disabled selected>Departamento</option>');
    //        for (var i = 0; i < data.length; i++) {
    //            $('#departamento').append($('<option>', {
    //                value: data[i].Clave,
    //                text: data[i].Concepto
    //            }));
    //        }
    //    },
    //    error: function (jqXHR, textStatus, errorThrown) {

    //    }
    //});
    //$.ajax({
    //    url: "/tblClasificacionProblema/GetPrioridad/",
    //    type: "GET",
    //    data: { 'IdPlaza': id_plaza },
    //    success: function (data, textStatus, jqXHR) {
    //        $('#prioridad').empty();
    //        $('#prioridad').append('<option disabled selected>Prioridad</option>');
    //        for (var i = 0; i < data.length; i++) {
    //            $('#prioridad').append($('<option>', {
    //                value: data[i].clvPrioridadQueja,
    //                text: data[i].Descripcion
    //            }));
    //        }
    //    },
    //    error: function (jqXHR, textStatus, errorThrown) {

    //    }
    //});
    $.ajax({
        url: "/tblClasificacionProblema/GetTurno/",
        type: "GET",
        data: { 'IdPlaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            $('#turno').empty();
            $('#turno').append('<option disabled selected>Turno</option>');
            for (var i = 0; i < data.length; i++) {
                $('#turno').append($('<option>', {
                    value: data[i].IdTurno,
                    text: data[i].Turno
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    var reporte = $('#reporte_cliente').val();
    var problema = $('#select_problemas').val();
    if(reporte == ""){
        swal("Se requiere la descripción del problema", "", "error");
    } else {
        if(problema == null){
            swal("Seleccione la clasificación del problema", "", "error");
        } else {
            $('#queja_modal').modal('show');
        }
    }
    
}
function regresar() {
    PNotify.removeAll()
    $('#panel_botones').hide();
    $('#panel_tiene_queja').hide();
    $('#sinQueja').modal('hide');
    $('#nombre_plaza').text(""); 
    $('#sub_titulo').text(""); 
    $('#tipo_llamante').hide();
    $('#exito').modal('hide');
    $('#regresar').hide();
    $('#motivo_llamada_panel').hide();
    $('#panel_llamante').hide();
    $('#panel_tabla_clientes').hide();
    $('#invalido').hide();
    $('#panel_datos_cliente').hide();
    $('#nombre_plaza').text("");
    $('#panel_no_cliente').hide();
    $('#telefono_no_cliente').hide();
    $('#email_no_cliente').hide();
    $('#domicilio_no_cliente').hide();
    $('#panel_tipo_informacion').hide();
    $('#panel_detalle_informacion').hide();
    $('#panel_hora_fecha').hide();
    $('#btn_enviar').hide();
    $('#panel_reporte_cliente').hide();
    $('#panel_clasificacion').hide();
    $('#panel_problema_real').hide();
    $('#panel_reporte_cliente').hide();
    $('#panel_solucion').hide();
    $('.collapse').collapse('show');
    document.getElementById("llamadaForm").reset();
    $('#Mygrafica').show();
}


function ModalHistorialPagos() {

    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/HistorialPago/",
        type: "GET",
        data: { 'plaza': id_plaza, 'contrato': contrato_enviar },
        success: function (data, textStatus, jqXHR) {
            $('#tableHistorialPago tbody').empty();
            if (data.length == 0) {
                $('#tableHistorialPago tbody').append('<tr><td colspan="6"><p class="text-center alert-warning">No hay registros con este número de contrato</p></td></tr>');
            } else {
                for (var i = 0; i < data.length; i++) {
                    var fecha = data[i].Fecha;
                    fecha = fecha.slice(0, 10);
                    $('#tableHistorialPago tbody').append('<tr><td>' + data[i].Serie + '</td><td>' + data[i].Folio + '</td><td>' + fecha + '</td><td>' + data[i].Importe + '</td><td>' + data[i].Status + '</td><td><button class="btn btn-sm btn-priamry" id="' + data[i].clv_Factura + '" onclick="mostrar_ticket(this)">Ticket</button></td></tr>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    $('#HistorialPagos').modal('show');
}


function ModalHistorialLlamadas() {

    $.ajax({
        url: "/Llamada/HistorialLlamadasL/",
        type: "GET",
        data: { 'contrato': contrato_enviar },
        success: function (data, textStatus, jqXHR) {
            $('#tableHistorialLlamadas tbody').empty();
            if (data.length == 0) {
                $('#tableHistorialLlamadas tbody').append('<tr><td colspan="6"><p class="text-center alert-warning">No hay registros con este número de contrato</p></td></tr>');
            } else {
                for (var i = 0; i < data.length; i++) {
                    var fecha = data[i].Fecha;
                    fecha = fecha.slice(0, 10);
                    var inicio = data[i].HoraInicial;
                    inicio = inicio.slice(11, 19);
                    var fin = data[i].HoraFinal;
                    fin = fin.slice(11, 19);
                    $('#tableHistorialLlamadas tbody').append('<tr><td>' + fecha + '</td><td>' + inicio + '</td><td>' + fin + '</td><td>' + data[i].Motivo + '</td><td>' + data[i].Solucion + '</td><td>' + data[i].Usuario + '</td></tr>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    $('#HistorialLlamadas').modal('show');
}


function ModalHistorialQuejas() {

    var id_plaza = $("#plaza_llamadas").val();
    var tipser = $('#tipo_servicio').val();

    $.ajax({
        url: "/Llamada/HistorialQuejasL/",
        type: "GET",
        data: { 'plaza': id_plaza, 'contrato': contrato_enviar,'filtro':0},
        success: function (data, textStatus, jqXHR) {
            $('#tableHistorialQuejas tbody').empty();
            if (data.length == 0) {
                $('#tableHistorialQuejas tbody').append('<tr><td colspan="7"><p class="text-center alert-warning">No hay registros con este número de contrato</p></td></tr>');
            } else {
                for (var i = 0; i < data.length; i++) {
                    $('#tableHistorialQuejas tbody').append('<tr onclick="detalleQueja(' + data[i].Queja + ')"><td>' + data[i].Queja + '</td><td>' + data[i].Status + '</td><td>' + data[i].Contrato + '</td><td>' + data[i].Nombre + '</td><td>' + data[i].Calle + '</td><td>' + data[i].Numero + '</td><td>' + data[i].TipSer + '</td><td><button class="btn btn-sm btn-priamry" onclick="detalleQueja(' + data[i].Queja + ')"> Ver</button></td></tr>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    $('#HistorialQuejas').modal('show');
}


function ModalConsultarPagos() {
    $('#cargando').show();
    $('#contenido_editar').hide();
    $('#contenido_editar2').hide();
    var id_plaza = $("#plaza_llamadas").val();

    $.ajax({
        url: "/Llamada/ConsultarPagos/",
        type: "GET",
        data: { 'plaza': id_plaza, 'contrato': contrato_enviar },
        success: function (data, textStatus, jqXHR) {
            $('#tableConsultaPago1 tbody').empty();
            $('#tableConsultaPago2 tbody').empty();
            if (data.length == 0) {
                $('#tableConsultaPago1 tbody').append('<tr><td colspan="15"><p class="text-center alert-warning">No hay registros con este número de contrato</p></td></tr>');
            } else {
                for (var i = 0; i < data.length; i++) {
                    for (var j = 0; j < data[i].pagos.length; j++) {
                        $('#tableConsultaPago1 tbody').append('<tr><td>' + data[i].pagos[j].Aparato + '</td><td>' + data[i].pagos[j].Concepto + '</td><td>' + data[i].pagos[j].PagoAde + '</td><td>' + data[i].pagos[j].TVExt + '</td><td>' + data[i].pagos[j].MesesCor + '</td><td>' + data[i].pagos[j].MesesPa + '</td><td>$' + data[i].pagos[j].Importe + '</td><td>' + data[i].pagos[j].PeriodoPI + '</td><td>' + data[i].pagos[j].PeriodoPF + '</td><td>' + data[i].pagos[j].PuntosAPO + '</td><td>' + data[i].pagos[j].PuntosAPA + '</td><td>' + data[i].pagos[j].PuntosAPAde + '</td><td>' + data[i].pagos[j].PuntosCombo + '</td><td>' + data[i].pagos[j].PuntosPPE + '</td><td>' + data[i].pagos[j].ImporteBoni + '</td></tr>');
                    }
                    for (var j = 0; j < data[i].pagosl.length; j++) {
                        $('#tableConsultaPago2 tbody').append('<tr><td>' + data[i].pagosl[j].Descripcion + '</td><td>$' + data[i].pagosl[j].Total + '</td></tr>');
                    }
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('#ConsultaPago').modal('show');
    setTimeout("mostrarConsultar()", 1800);
    
}
function mostrarConsultar() {
    $('#cargando').hide();
    $('#contenido_editar').show();
    $('#contenido_editar2').show();
    
}
function mostrar_ticket(e) {
    var clv_factura = e.getAttribute('id');
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/GetTicket/",
        type: "GET",
        data: { 'plaza': id_plaza, 'clv_factura': clv_factura, 'contrato': contrato_enviar },
        success: function (data, textStatus, jqXHR) {
            $('#nombre_empresa').text(data[0].nombre_empresa); 
            $('#calle_empresa').text(data[0].direccion_empresa);
            $('#colonia_empresa').text(data[0].colonia_empresa);
            $('#ciudad_empresa').text(data[0].ciudad_empresa);
            $('#rfc_empresa').text(data[0].rfc_empresa);
            $('#nombre_sucursal').text(data[0].nombre_sucursal);
            $('#calle_sucursal').text(data[0].calle_sucursal);
            $('#numero_sucursal').text("#"+data[0].numero_sucursal);
            $('#colonia_sucursal').text(data[0].colonia_sucursal);
            $('#cp_sucursal').text("CP:"+data[0].cp_sucursal);
            $('#ciudad_sucursal').text(data[0].ciudad_sucursal);
            $('#telefono_sucursal').text(data[0].telefono_sucursal);
            $('#recibo').text("Recibo " + data[0].serie_factura);
            $('#contrato_ticket').text("Contrato: " + data[0].contrato);
            $('#nombre_cliente').text(data[0].nombre_cliente);
            $('#nombre_calle').text(data[0].nombre_calle);
            $('#nombre_numero').text("#"+data[0].nombre_numero);
            $('#cp_cliente').text("CP " + data[0].cp_cliente);
            $('#colonia_cliente').text("Col: "+data[0].colonia_cliente);
            $('#ciudad_cliente').text(data[0].ciudad_cliente);
            $('#cajero').text(data[0].cajero);
            $('#descripcion_servicio').text(data[0].descripcion_servicio);
            $('#monto_servicio').text("$" + data[0].monto_servicio);
            $('#mes').text("Periodo Pagado "+data[0].mes);
            $('#puntos').text("Puntos Aplicados Por Pago Oportuno $"+data[0].puntos);
            $('#cantidad_letra').text(data[0].cantidad_letra);
            $('#total').text("Total $"+data[0].total);
            $('#cambio').text("Cambio $"+data[0].cambio);
            $('#efectivo').text("Pago en efectivo $" + data[0].efectivo);
            $('#sucursal').text(data[0].nombre_sucursal);
            $('#proximo_pago').text(data[0].proximo_pago);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    $('#modal_ticket').modal('show');
}

function detalleQueja(e) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/detalleQueja/",
        type: "GET",
        data: { 'id_plaza': id_plaza, 'queja': e },
        success: function (data, textStatus, jqXHR) {
            $('#queja_error').hide();
            $('#panel_detalle_queja').show();
            $('#queja_servicio').empty();
            $('#queja_servicio').append('<option selected disabled>' + data.tipo_servicio + '</option>');
            $('#queja_id').text(data.queja);
            $('#queja_contrato').text(data.contrato);
            $('#queja_reporte').val(data.problema);
            $('#queja_observaciones').val(data.observaciones);
            $('#queja_status').text(data.status);
            $('#queja_solicitud').text(data.solicitud);
            $('#queja_fecha_ejecuto').text(data.ejecucion);
            $('#queja_ejecuto').text(data.usuario_ejecuto); 
            var v1 = data.v1;
            var v1 = v1.split(" ", 2);
            var v2 = data.v2;
            var v2 = v2.split(" ", 2);
            var v3 = data.v3;
            var v3 = v3.split(" ", 2);
            $('#queja_fecha_v1').val(v1[0]);
            $('#queja_hora_v1').val(v1[1]);
            $('#queja_fecha_v2').val(v2[0]);
            $('#queja_hora_v2').val(v2[1]);
            $('#queja_fecha_v3').val(v3[0]);
            $('#queja_hora_v3').val(v3[1]);
            $('#queja_nombre_cliente').text(nombre);
            $('#queja_colonia').text(colonia);
            $('#queja_calle').text(calle);
            $('#queja_numero').text(numero);
            $('#queja_usuario').text(data.usuario);
            $('#queja_problema').empty();
            $('#queja_problema').append('<option selected disabled>' + data.class_problema + '</option>');
            $('#queja_clas_solucion').empty();
            $('#queja_clas_solucion').append('<option selected disabled>' + data.clas_solucion + '</option>');
            $('#queja_solucion').val(data.solucion);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('#panel_detalle_queja').hide(); 
            $('#queja_error').show();
        }
    });
    $('#detalle_queja').modal('show');
}

function ModalOrdenes() {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/getOrdenes/",
        type: "GET",
        data: { 'idPlaza': id_plaza, 'Contrato': contrato_enviar},
        success: function (data, textStatus, jqXHR) {
            $('#tableOrdenes tbody').empty();
            for (var i = 0; i < data.length; i++) {
                $('#tableOrdenes tbody').append('<tr><td>' + data[i].Clv_Orden + '</td><td>' + data[i].STATUS + '</td><td>' + data[i].Nombre + '</td><td><button class="btn btn-sm btn-default" onclick="VerOrden(' + data[i].Clv_Orden + ')">Ver</button></td></tr>');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('#HistorialOrdenes').modal('show');
}

function VerOrden(x) {
    var id_plaza = $("#plaza_llamadas").val();
    $.ajax({
        url: "/Llamada/consultarDetalleOrden/",
        type: "GET",
        data: { 'idPlaza': id_plaza, 'Orden': x },
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            $('#noOrden').val(data.clv_orden);
            $('#contratoCliente').val(data.contrato);
            $('#nombre_orden').text(data.nombre);
            $('#numero_orden').text(data.numero);
            $('#colonia_orden').text(data.colonia);
            $('#calle_orden').text(data.calle);
            for (var i = 0; i < data.servicios.length; i++) {
                $('#serviciosCliente').append('<p>'+data.servicios[i].servicio+'</p>');
            }
            $('#selectTecnico').append('<option value="' + data.tecnico.clvTecnico + '">' + data.tecnico.Nombre + '</option>');
            $('#observaciones_orden').val(data.observaciones);
            $('#genero_orden').text(data.genero);
            console.log(data.status);
            if(data.status == "P"){
                $("#pendiente").prop("checked", true);
            }else if(data.status == "E"){
                $("#ejecutada").prop("checked", true);
                $('#ejecuto_orden').text(data.ejecuto);
            } else {
                $("#visita").prop("checked", true);
                $('#ejecuto_orden').text(data.ejecuto);
            }
            $('#fecha_solicitud').val(data.solicitud);
            $('#fecha_ejecucion').val(data.ejecucion);
            $('#fecha_visita1').val(data.visita1);
            $('#fecha_visita2').val(data.visita2);
            $('#TablaDetalleOrdenes tbody').empty();
            for (var i = 0; i < data.detallesOrdenes.length; i++) {
                TablaDetalleOrdenes
                $('#TablaDetalleOrdenes tbody').append('<tr><td>' + data.detallesOrdenes[i].descripcion + '</td><td>' + data.detallesOrdenes[i].accion + '</td><td>' + data.detallesOrdenes[i].observaciones + '</td></tr>');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('#DetalleOrden').modal('show');
}