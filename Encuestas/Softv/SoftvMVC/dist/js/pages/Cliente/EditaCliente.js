



$('#TablaClientes').on('click', '.editarCliente', function () {

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

    $('#rfc').val('');
    $('#curp').val('');
    $('#rsocial').val('');
    $('#EstadoFiscal').val('');
    $('#CiudadFiscal').val('');
    $('#ColoniaFiscal').val('');
    $('#calleFiscal').val('');
    $('#callesFiscal').val('');
    $('#NumeroFiscal').val('');
    $('#postafiscal').val('');
    $('#TelefonoFiscal').val('');
    $('#FaxFiscal').val('');

    $('#ciudad').find('option').remove();
    $('#colonia').find('option').remove();
    $('#calle').find('option').remove();

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
            $("#ciudad").val(data[0].Clv_Ciudad);
            $('#colonia').val(data[0].Clv_Colonia);
            $('#calle').val(data[0].Clv_Calle);
            $('#numero').val(data[0].NUMERO);
            $('#cp').val(data[0].CodigoPostal);
            $('#calles').val(data[0].ENTRECALLES);
            $('#telefono').val(data[0].TELEFONO);
            $('#celular').val(data[0].CELULAR);
            $('#correo').val(data[0].Email);
        },
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
            
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });


    
});

function Getcolonias( ciudad,plaza) {
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

function getCalle(plaza,idcolonia) {

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
    cliente.DESGLOSA_Iva = "";
    cliente.SoloInternet = "";
    cliente.eshotel = "";
    cliente.Clv_Ciudad = "";
    cliente.Email = $('#correo').val();
    cliente.clv_sector = "";
    cliente.Clv_Periodo = "";
    cliente.Clv_Tap = "";
    cliente.Zona2 = "";

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