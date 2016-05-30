



$('#TablaClientes').on('click', '.editarCliente', function () {

    BorraFormulario();
    var id = $(this).attr('rel');
    var contrato = $(this).attr('id');
    $('#ModalEditarCliente').modal('show');

    $.ajax({
        url: "/CIUDAD/GetCiudadByPlaza/",
        type: "POST",
        data: { 'plaza': id },
        success: function (data, textStatus, jqXHR) {
            for (var a = 0; a < data.length; a++) {
                $('#ciudad').append("<option id='" + data[a].plaza + "' value='" + data[a].Clv_Ciudad + "'>" + data[a].Nombre + "</option>");
            }

        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });


  

    $.ajax({
        url: "/CLIENTE/DetalleCliente/",
        type: "POST",
        data: { 'id': id, 'contrato': contrato },
        success: function (data, textStatus, jqXHR) {

            Getcolonias(data[0].Clv_Ciudad, id);
            getCalle(id, data[0].Clv_Colonia);
            $('#contrato').val(data[0].CONTRATO);
            $('#nombre').val(data[0].NOMBRE);
            $('#calles').val(data[0].ENTRECALLES);
            $("#ciudad").val(data[0].Clv_Ciudad).change();
            console.log(data[0].Clv_Ciudad);
            $('#colonia').val(data[0].Clv_Colonia);
            $('#calle').val(data[0].Clv_Calle);
            $('#numero').val(data[0].NUMERO);
            $('#cp').val(data[0].CodigoPostal);
            $('#calles').val(data[0].ENTRECALLES);
            $('#telefono').val(data[0].TELEFONO);
            $('#celular').val(data[0].CELULAR);
            $('#correo').val(data[0].Email);
            $('#iva').val(data[0].DESGLOSA_Iva);
            $('#solointernet').val(data[0].SoloInternet);
            $('#eshotel').val(data[0].eshotel);
            $('#sector').val(data[0].clv_sector);
            $('#periodo').val(data[0].Clv_Periodo);
            $('#tap').val(data[0].Clv_Tap);
            $('#zona2').val(data[0].Zona2);        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });


    $.ajax({
        url: "/CLIENTE/GetDetalleFiscal/",
        type: "POST",
        data: { 'plaza': id, 'contrato': contrato },
        success: function (data, textStatus, jqXHR) {
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
    cliente.CodigoPostal = $('#cp').val();
    cliente.TELEFONO = $('#telefono').val();
    cliente.CELULAR = $('#celular').val();
    cliente.DESGLOSA_Iva = $('#iva').val();
    cliente.SoloInternet = $('#solointernet').val();
    cliente.eshotel = $('#eshotel').val();
    cliente.Clv_Ciudad = $('#ciudad').val();
    cliente.Email = $('#correo').val();
    cliente.clv_sector = $('#sector').val();
    cliente.Clv_Periodo = $('#periodo').val();
    cliente.Clv_Tap = $('#tap').val();
    cliente.Zona2 = $('#zona2').val();
    cliente.conexion = $('#conexiones').val();

    var Datosfiscales = {};
    Datosfiscales.Contrato = $('#contrato').val();
    Datosfiscales.IVADESGLOSADO = $('#ivafiscal').val();
    Datosfiscales.RAZON_SOCIAL = $('#rsocial').val();
    Datosfiscales.RFC = $('#rfc').val();
    Datosfiscales.CALLE_RS = $('#calleFiscal').val();
    Datosfiscales.NUMERO_RS = $('#NumeroFiscal').val();
    Datosfiscales.ENTRECALLES = $('#callesFiscal').val();
    Datosfiscales.COLONIA_RS = $('#ColoniaFiscal').val();
    Datosfiscales.CIUDAD_RS = $('#CiudadFiscal').val();
    Datosfiscales.ESTADO_RS = $('#EstadoFiscal').val();
    Datosfiscales.CP_RS = $('#postafiscal').val();
    Datosfiscales.TELEFONO_RS = $('#TelefonoFiscal').val();
    Datosfiscales.FAX_RS = $('#FaxFiscal').val();
    Datosfiscales.TIPO="";
    Datosfiscales.IDENTIFICADOR = $('#identificadorfiscal').val();
    Datosfiscales.CURP = $('#curp').val();
    Datosfiscales.id_asociado = $('#idasociadofiscal').val();

    $.ajax({
        url: "/CLIENTE/UpdateCliente/",
        type: "POST",
        data: { 'cliente': cliente, 'fiscales': Datosfiscales },
        success: function (data, textStatus, jqXHR) {
            swal("Hecho!", "El cliente se edito correctamente!", "success");

        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
   
  

});



$('#ciudad').change(function () {
    $('#colonia').find('option').remove();
    var idciudad = $(this).val();
    var idplaza = $('option:selected', this).attr('id');
    $.ajax({
        url: '/COLONIA/GetColoniaByCiudad/',
        type: "POST",
        data: { 'idciudad': idciudad, 'plaza': idplaza },
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            for (var a = 0; a < data.length; a++) {
                $('#colonia').append("<option id='" + data[a].plaza + "' value='" + data[a].clv_colonia + "'>" + data[a].Nombre + "</option>");
            }

        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
});


$('#colonia').change(function () {
    $('#calle').find('option').remove();
    var idcolonia = $(this).val();
    var idplaza = $('option:selected', this).attr('id');

    $.ajax({
        url: '/CALLE/GetCalleByColonia/',
        type: "POST",
        data: { 'colonia': idcolonia, 'plaza': idplaza },
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            for (var a = 0; a < data.length; a++) {
                $('#calle').append("<option value='" + data[a].Clv_Calle + "'>" + data[a].Nombre + "</option>");
            }

        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });


});

function Getcolonias(ciudad, plaza) {
    $.ajax({
        url: '/COLONIA/GetColoniaByCiudad/',
        type: "POST",
        data: { 'idciudad': ciudad, 'plaza': plaza },
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            for (var a = 0; a < data.length; a++) {
                $('#colonia').append("<option id='" + data[a].plaza + "' value='" + data[a].clv_colonia + "'>" + data[a].Nombre + "</option>");
            }

        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

}

function getCalle(plaza, idcolonia) {

    $.ajax({
        url: '/CALLE/GetCalleByColonia/',
        type: "POST",
        data: { 'colonia': idcolonia, 'plaza': plaza },
        success: function (data, textStatus, jqXHR) {
            console.log(data);
            for (var a = 0; a < data.length; a++) {
                $('#calle').append("<option value='" + data[a].Clv_Calle + "'>" + data[a].Nombre + "</option>");
            }

        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

}



function BorraFormulario() {

    $('#contrato').val('');
    $('#nombre').val('');
    $('#calles').val('');
    $("#ciudad").val('');
    $('#colonia').val('');
    $('#calle').val('');
    $('#numero').val('');
    $('#cp').val('');
    $('#calles').val('');
    $('#telefono').val('');
    $('#celular').val('');
    $('#correo').val('');
    $('#iva').val('');
    $('#solointernet').val('');
    $('#eshotel').val('');
    $('#sector').val('');
    $('#periodo').val('');
    $('#tap').val('');
    $('#zona2').val('');

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