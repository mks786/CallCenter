$(function () {
    $("#ciudad_select").change(function () {
        cambiarColonia($(this).val());
    });
    $("#colonia_select").change(function () {
        cambiarCalle($(this).val());
    });
    $('#telefono_editar').inputmask("(999)9999999");
    $('#celular_editar').inputmask("999-999-99-99");
});


function cambiarColonia(id) {
    var id_plaza = $('#paza_conectando').val();
    $.ajax({
        url: "/COLONIA/GetColoniaByCiudad/",
        type: "POST",
        data: { 'idciudad': id, 'plaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            $('#colonia_select').find('option').remove().end();
            $('#calle_editar').empty();
            for (var i = 0; i < data.length; i++) {
                $('#colonia_select').append("<option id='seleccion' value='" + data[i].clv_colonia + "' selected>" + data[i].Nombre + "</option>");
                if(data.length == 1){
                    $('#colonia_select').val(data[i].clv_colonia).change();
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}


function cambiarCalle(id) {
    var id_plaza = $('#paza_conectando').val();
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

function editarCliente(id) {
    BorraFormulario();
    $('#personal').addClass('active');
    $('#fiscal').removeClass('active');
    $('#tab_1').addClass('active');
    $('#tab_2a').removeClass('active');
    var id_plaza = $('#paza_conectando').val();
    $.ajax({
        url: "/CLIENTE/getNombreCliente/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "contrato": id },
        success: function (data, textStatus, jqXHR) {
            $('#nombre_editar').val(data[0].nombre);
            $('#segundo_nombre_editar').val(data[0].segundonombre);
            $('#apaterno_editar').val(data[0].apaterno);
            $('#amaterno_editar').val(data[0].amaterno);
            var formatted = $.datepicker.formatDate("yy-mm-dd", new Date(data[0].fnacimiento));
            var fecha = data[0].fnacimiento;
            fecha = fecha.split("", 10);
            fecha = fecha[6] + fecha[7] + fecha[8] + fecha[9] + "-" + fecha[3] + fecha[4] + "-" + fecha[0] + fecha[1];
            $('#fecha_editar').val(fecha);
            
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    $('#ModalEditarCliente').modal('show');
    $('#contenido_editar').hide();
    $('#cargando').show();
    $.ajax({
        url: "/CLIENTE/GetClientesporPlazaJson/",
        type: "GET",
        data: { 'id': id_plaza, "contrato": id, "cliente1": "", "direccion": "" },
        success: function (data, textStatus, jqXHR) {
            $('#ciudad_select').find('option').remove().end();
            getCiudad(data[0].Clv_Ciudad);
            getColonia(parseInt(data[0].Clv_Ciudad), id_plaza, data[0].Clv_Colonia);
            getCalle(data[0].Clv_Colonia, id_plaza, data[0].Clv_Calle);
            $('#contrato_editar').val(data[0].CONTRATO);
            $('#calles_editar').val(data[0].ENTRECALLES);
            $('#numero_editar').val(data[0].NUMERO);
            $('#cp_editar').val(data[0].CodigoPostal);
            $('#calles_editar').val(data[0].ENTRECALLES);
            $('#telefono_editar').val(data[0].TELEFONO);
            $('#celular_editar').val(data[0].CELULAR);
            $('#correo_editar').val(data[0].Email);
            $('#iva_editar').val(data[0].DESGLOSA_Iva);
            $('#solointernet_editar').val(data[0].SoloInternet);
            $('#eshotel_editar').val(data[0].eshotel);
            $('#sector_editar').val(data[0].clv_sector);
            $('#periodo_editar').val(data[0].Clv_Periodo);
            $('#tap_editar').val(data[0].Clv_Tap);
            $('#zona2_editar').val(data[0].Zona2);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $.ajax({
        url: "/CLIENTE/GetDetalleFiscal/",
        type: "POST",
        data: { 'plaza': id_plaza, 'contrato': id },
        success: function (data, textStatus, jqXHR) {
            if (data.Contrato != null) {
                $('#fiscal').show();
                
            } else {
                $('#fiscal').hide();
            }
            $('#rfc').val(data.RFC);
            $('#curp').val(data.CURP);
            $('#rsocial').val(data.RAZON_SOCIAL);
            $('#EstadoFiscal').val(data.ESTADO_RS);
            $('#CiudadFiscal').val(data.CIUDAD_RS);
            $('#ColoniaFiscal').val(data.COLONIA_RS);
            $('#calleFiscal').val(data.CALLE_RS);
            $('#callesFiscal').val(data.ENTRECALLES);
            $('#NumeroFiscal').val(data.NUMERO_RS);
            $('#postafiscal').val(data.CP_RS);
            $('#TelefonoFiscal').val(data.TELEFONO_RS);
            $('#FaxFiscal').val(data.FAX_RS);
            $('#ivafiscal').val(data.IVADESGLOSADO);
            $('#idasociadofiscal').val(data.id_asociado);
            $('#identificadorfiscal').val(data.IDENTIFICADOR);
            $('#tipofiscal').val(data.TIPO);
            $('#PaisFiscal').val(data.Pais);
            $('#EmailFiscal').val(data.Email);
            
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    
    setTimeout("mostrarDatos()", 1800);

}


function mostrarDatos() {
    $('#cargando').hide();
    $('#contenido_editar').show();
}




$('#Editar').click(function () {
    var cliente = {};
    var nombreCliente = {};
    nombreCliente.nombre = $('#nombre_editar').val();
    nombreCliente.segundonombre = $('#segundo_nombre_editar').val();
    nombreCliente.apaterno = $('#apaterno_editar').val();
    nombreCliente.amaterno = $('#amaterno_editar').val();
    var fecha_inicio = $('#fecha_editar').val();
    var fecha;
    if(fecha_inicio != "" && fecha_inicio != null){
        var currentdate = new Date(fecha_inicio);
       fecha = currentdate.getDate() + "/" + (currentdate.getMonth() + 1) + "/" + currentdate.getFullYear();
    } else {
        fecha = "";
    }
   
    nombreCliente.fnacimiento = fecha;
    cliente.CONTRATO = $('#contrato_editar').val();
    var nombre_concatenado = '';
    if ($('#nombre_editar').val() != "") {
        nombre_concatenado += $('#nombre_editar').val();
    }if ($('#segundo_nombre_editar') != "") {
        nombre_concatenado += " " + $('#segundo_nombre_editar').val();
    }if ($('#apaterno_editar').val() != "") {
        nombre_concatenado += " "+$('#apaterno_editar').val();
    }if ($('#amaterno_editar').val()) {
        nombre_concatenado += " " + $('#amaterno_editar').val();
    }
    cliente.NOMBRE = nombre_concatenado
    cliente.Clv_Calle = $('#calle_editar').val();
    cliente.NUMERO = $('#numero_editar').val();
    cliente.ENTRECALLES = $('#calles_editar').val();
    cliente.Clv_Colonia = $('#colonia_select').val();
    cliente.CodigoPostal = $('#cp_editar').val();
    cliente.TELEFONO = $('#telefono_editar').inputmask('unmaskedvalue');
    cliente.CELULAR = $('#celular_editar').inputmask('unmaskedvalue');
    cliente.DESGLOSA_Iva = $('#iva').val();
    cliente.SoloInternet = $('#solointernet_editar').val();
    cliente.eshotel = $('#eshotel_editar').val();
    cliente.Clv_Ciudad = $('#ciudad_select').val();
    cliente.Email = $('#correo_editar').val();
    cliente.clv_sector = $('#sector_editar').val();
    cliente.Clv_Periodo = $('#periodo_editar').val();
    cliente.Clv_Tap = $('#tap_editar').val();
    cliente.Zona2 = $('#zona2_editar').val();
    cliente.conexion = $('#paza_conectando').val();

  
    $.ajax({
        url: "/CLIENTE/UpdateCliente/",
        type: "POST",
        data: { 'cliente': cliente, 'clienteNombres': nombreCliente, 'usuario': usuario },
        success: function (data, textStatus, jqXHR) {
            setUpdateCliente(cliente.CONTRATO);
            swal("Hecho!", "El cliente se edito correctamente!", "success");

        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
   
  

});

function AbrirModalClietes() {
    $('#Modal_Plaza_clientes').modal('show');
}

function plaza_conexion() {
    $('#conetar_plaza').hide();
    $('#Modal_Plaza_clientes').modal('hide');
    $('#invalido').hide();
    $('#panel_tabla_clientes').hide();
    $('#panel_clientes').show();
    $('.collapse').collapse('show');
    var id_plaza = $('#paza_conectando').val();
    $.ajax({
        url: "/Conexion/listaPlazas/",
        type: "GET",
        data: { "idPlaza": id_plaza },
        success: function (data, textStatus, jqXHR) {
            var ciudad_select = $("#paza_conectando option:selected").text();
            $('#nombre_plaza').text("CIUDAD DE " + ciudad_select.toUpperCase() + ", SERVIDOR " + data.Plaza.toUpperCase());
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getColonia(id, id_plaza,colonia) {
    $('#colonia_select').find('option').remove().end();
    $.ajax({
        url: "/COLONIA/GetColoniaByCiudad/",
        type: "POST",
        data: { 'idciudad': id, 'plaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            for (var a = 0; a < data.length; a++) {
                if (colonia == data[a].clv_colonia) {
                    $('#colonia_select').append("<option id='seleccion' value='" + data[a].clv_colonia + "' selected>" + data[a].Nombre + "</option>");

                } else {
                    $('#colonia_select').append("<option id='seleccion' value='" + data[a].clv_colonia + "'>" + data[a].Nombre + "</option>");

                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getCalle(id, id_plaza, calle) {
    $('#calle').find('option').remove().end();
    $.ajax({
        url: "/CALLE/GetCalleByColonia/",
        type: "GET",
        data: { 'colonia': id, 'plaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            $('#calle_editar').empty();
            for (var a = 0; a < data.length; a++) {
                if (calle == data[a].Clv_Calle) {
                    $('#calle_editar').append("<option id='seleccion_calle' value='" + data[a].Clv_Calle + "' selected>" + data[a].Nombre + "</option>");

                } else {
                    $('#calle_editar').append("<option id='seleccion_calle' value='" + data[a].Clv_Calle + "'>" + data[a].Nombre + "</option>");

                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function getCiudad (id){

    var id_plaza = $('#paza_conectando').val();
    $.ajax({
        url: "/CIUDAD/GetCiudadByPlaza/",
        type: "POST",
        data: { 'plaza': id_plaza },
        success: function (data, textStatus, jqXHR) {
            for (var a = 0; a < data.length; a++) {
                if (id == data[a].Clv_Ciudad) {
                    $('#ciudad_select').append("<option id='" + data[a].Clv_Ciudad + "' value='" + data[a].Clv_Ciudad + "' selected>" + data[a].Nombre + "</option>");
                } else {
                    $('#ciudad_select').append("<option id='" + data[a].Clv_Ciudad + "' value='" + data[a].Clv_Ciudad + "'>" + data[a].Nombre + "</option>");

                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function BorraFormulario() {

    $('#contrato_editar').val('');
    $('#nombre_editar').val('');
    $('#calles_editar').val('');
    $("#ciudad_editar").val('');
    $('#colonia_select').val('');
    $('#calle_editar').val('');
    $('#numero_editar').val('');
    $('#cp_editar').val('');
    $('#calles_editar').val('');
    $('#telefono_editar').val('');
    $('#celular_editar').val('');
    $('#correo_editar').val('');
    $('#iva_editar').val('');
    $('#solointernet_editar').val('');
    $('#eshotel_editar').val('');
    $('#sector_editar').val('');
    $('#periodo_editar').val('');
    $('#tap_editar').val('');
    $('#zona2_editar').val('');

    $('#contrato').val('');
    $('#ivafiscal').val('');
    $('#rsocial').val('');
    $('#rfc').val('');
    $('#calleFiscal').val('');
    $('#NumeroFiscal').val('');
    $('#callesFiscal').val('');
    $('#ColoniaFiscal').val('');
    $('#CiudadFiscal').val('');
    $('#EstadoFiscal').val('');
    $('#postafiscal').val('');
    $('#TelefonoFiscal').val('');
    $('#FaxFiscal').val('');
    $('#identificadorfiscal').val('');
    $('#curp').val('');
    $('#idasociadofiscal').val('');

    $('#ciudad').find('option').remove();
    $('#colonia').find('option').remove();
    $('#calle').find('option').remove();

}


$('#buscar_por_nombre').on('click', function () {
    $('.collapse').collapse('hide');
    var id_plaza = $('#paza_conectando').val();
    var nombre = $('#nombre_individual').val();
    var ciudad = $('#paza_conectando option:selected').text();
    $('#panel_masivo').hide();
    $('#panel_individual').show();
    $.ajax({
        url: "/CLIENTE/GetClientesPRUEBA/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "contrato": "", "Nombrecliente": nombre, "direccion": "",'filtro':ciudad},
        success: function (data, textStatus, jqXHR) {
            if (data.length == 0) {
                $('#panel_tabla_clientes').hide();
                $('#invalido').show();
            } else {
                $('#invalido').hide();
                $('#panel_tabla_clientes').show();
                $('#Tabla_Clientes tbody > tr').remove();
                for (var i = 0; i < data.length; i++) {
                    if (permiso_editar == "False") {
                        $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].NOMBRE + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].Calle + '</td><td>' + data[i].NUMERO + '</td><td></td></tr>');
                    }
                    else {
                        $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].NOMBRE + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].Calle + '</td><td>' + data[i].NUMERO + '</td><td><button rel="' + data[i].CONTRATO + '"class="btn btn-warning btn-xs editarCliente" id="' + data[i].CONTRATO + '" onclick="editarCliente(this.id)"><i class="fa fa-pencil" aria-hidden="true"></i> Editar</button></td></tr>');
                    }
                }
            }
        },
        error: function (data, jqXHR, textStatus) {
        }

    });
});

$('#buscar_por_direccion').on('click', function () {
    $('.collapse').collapse('hide');
    var id_plaza = $('#paza_conectando').val();
    var calle = $('#calle_busqueda').val();
    var ciudad = $('#ciudad_busqueda').val();
    var numero = $('#numero_domicilio').val();
    var colonia = $('#colonia_domicilio').val();
    var ciudad_filtro = $('#paza_conectando option:selected').text();
    $('#panel_masivo').hide();
    $('#panel_individual').show();
    $.ajax({
        url: "/CLIENTE/GetClientesPRUEBA/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "contrato": "", "Nombrecliente": "", "calle": calle, "colonia": colonia, "ciudad": ciudad, "numero": numero,'filtro':ciudad_filtro },
        success: function (data, textStatus, jqXHR) {
            if (data.length == 0) {
                $('#panel_tabla_clientes').hide();
                $('#invalido').show();
            } else {
                $('#invalido').hide();
                $('#panel_tabla_clientes').show();
                $('#Tabla_Clientes tbody > tr').remove();
                for (var i = 0; i < data.length; i++) {
                    if (permiso_editar == "False") {
                        $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].NOMBRE + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].Calle + '</td><td>' + data[i].NUMERO + '</td><td></td></tr>');
                    } else {
                        $('#Tabla_Clientes tbody').append('<tr><td>' + data[i].CONTRATO + '</td><td>' + data[i].NOMBRE + '</td><td>' + data[i].Ciudad + '</td><td>' + data[i].Colonia + '</td><td>' + data[i].Calle + '</td><td>' + data[i].NUMERO + '</td><td><button rel="' + data[i].CONTRATO + '"class="btn btn-warning btn-xs editarCliente" id="' + data[i].CONTRATO + '" onclick="editarCliente(this.id)"><i class="fa fa-pencil" aria-hidden="true"></i> Editar</button></td></tr>');
                    }


                }
            }
        },
        error: function (data, jqXHR, textStatus) {
        }

    });
});

$('#buscar_por_contrato').on('click', function () {
    $('.collapse').collapse('hide');
    var id_plaza = $('#paza_conectando').val();
    var contrato = $('#input_contrato').val();
    var ciudad = $('#paza_conectando option:selected').text();
    $('#panel_masivo').hide();
    $('#panel_individual').show();
    $.ajax({
        url: "/CLIENTE/GetClientesPRUEBA/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "contrato": contrato, "cliente1": "", "direccion": "",'filtro':ciudad },
        success: function (data, textStatus, jqXHR) {
            if (data.length == 0) {
                $('#panel_tabla_clientes').hide();
                $('#invalido').show();
            } else {
                $('#invalido').hide();
                $('#panel_tabla_clientes').show();
                $('#Tabla_Clientes tbody > tr').remove();
                if (permiso_editar == "False") {
                    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].NOMBRE + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].Calle + '</td><td>' + data[0].NUMERO + '</td><td></td></tr>');
                } else {
                    $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].NOMBRE + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].Calle + '</td><td>' + data[0].NUMERO + '</td><td><button rel="' + data[0].CONTRATO + '"class="btn btn-warning btn-xs editarCliente" id="' + data[0].CONTRATO + '" onclick="editarCliente(this.id)"><i class="fa fa-pencil" aria-hidden="true"></i> Editar</button></td></tr>');
                }
            }
        },
        error: function (data, jqXHR, textStatus) {
        }

    });
});

function setUpdateCliente(id) {
    var id_plaza = $('#paza_conectando').val();
    $('#panel_masivo').hide();
    $('#panel_individual').show();
    var ciudad = $('#paza_conectando option:selected').text();
    $.ajax({
        url: "/CLIENTE/GetClientesPRUEBA/",
        type: "GET",
        data: { 'IdPlaza': id_plaza, "contrato": id, "cliente1": "", "direccion": "",'filtro':ciudad },
        success: function (data, textStatus, jqXHR) {
            if (data.length == 0) {
                $('#panel_tabla_clientes').hide();
                $('#invalido').show();
            } else {
                $('#invalido').hide();
                $('#panel_tabla_clientes').show();
                $('#Tabla_Clientes tbody > tr').remove();
                $('#Tabla_Clientes tbody').append('<tr><td>' + data[0].CONTRATO + '</td><td>' + data[0].NOMBRE + '</td><td>' + data[0].Ciudad + '</td><td>' + data[0].Colonia + '</td><td>' + data[0].Calle + '</td><td>' + data[0].NUMERO + '</td><td><button rel="' + data[0].CONTRATO + '"class="btn btn-warning btn-xs editarCliente" id="' + data[0].CONTRATO + '" onclick="editarCliente(this.id)"><i class="fa fa-pencil" aria-hidden="true"></i> Editar</button></td></tr>');
            }
        },
        error: function (data, jqXHR, textStatus) {

        }

    });
}