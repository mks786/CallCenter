$('#TablaClientes').on('click', '.editarCliente', function () {

    $('#contrato').val('');
    $('#nombre').val('');
    $('#calle').val('');
    $('#numero').val('');
    $('#cp').val('');
    $('#calles').val('');
    $('#colonia').val('');
    $('#telefono').val('');
    $('#celular').val('');
    $('#correo').val('');
    $('#ciudad').val('');


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
            $('#contrato').val(data[0].CONTRATO);
            $('#nombre').val(data[0].NOMBRE);
            $('#calles').val(data[0].ENTRECALLES);           
            $("#ciudad").val(data[0].Clv_Ciudad).change();
            $('#colonia').val(data[0].Clv_Colonia).change();
            $('#calle').val(data[0].Clv_Calle).change();
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