function detalleCliente(id) {
    var id_plaza = $('#paza_conectando').val();
    console.log(id_plaza);
    $.ajax({
        url: "/CLIENTE/GetClientesporPlazaJson/",
        type: "GET",
        data: { 'id': id_plaza, "contrato": id, "cliente1": "", "direccion": "" },
        success: function (data, textStatus, jqXHR) {
            $('#nombre_cliente').text(data[0].NOMBRE);
            $('#contrato_cliente').text(data[0].CONTRATO);
            $('#telefono_cliente').text(data[0].TELEFONO);
            $('#celular_cliente').text(data[0].CELULAR);
            $('#correo_cliente').text(data[0].Email);
            $('#ciudad_cliente').text(data[0].Ciudad);
            $('#colonia_cliente').text(data[0].Colonia);
            $('#calle_cliente').text(data[0].Calle);
            $('#numero_cliente').text(data[0].NUMERO);
            $('#cp_cliente').text(data[0].CodigoPostal);
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    $('#ModalDetalleCliente').modal('show');
}