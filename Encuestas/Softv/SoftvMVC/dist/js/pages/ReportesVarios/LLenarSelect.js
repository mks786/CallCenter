

function cargarDatos(numModal) {
    var tipoServicioSeleccionado = cableInternet(); //1 = cable, 2 = internet
    var idConexion = $("#listaConexionPlaza").val(); //obtener idConexion seleccionada

    //'<option value="0">Servicio Básico </option>'

    if (numModal == 1) //TipoCliente y cable
    {
        $.ajax({
            url: '/TipoCliente/GetTipoClientes/',
            type: 'POST',
            dataType: 'json',
            data: { 'numModal': numModal,'idConexion': idConexion },
            success: function (data) {
                console.log(data);
                //alert(data);            
                $.each(data, function (i, item) {

                    $('<option value="' + item.Clv_TipoCliente + '">' + item.Descripcion + '</option>').appendTo('#origenClientes');
                });
            },
            error: function () {
                console.log('err')
            }
        });
    }
        // Dependiendo del modal y si es cable o internet, se cargan los datos en el modal de Servicio

    else if (numModal == 2) //Servicio con cable o internet 
    {
        $.ajax({
            url: '/Servicio/GetServicioCable/',
            type: 'POST',
            dataType: 'json',
            data: { 'tipoServicioSeleccionado':tipoServicioSeleccionado, 'idConexion': idConexion },
            success: function (data) {
                console.log(data);
                //alert(data);
                $.each(data, function (i, item) {
                    $('<option value="' + item.Clv_Servicio + '">' + item.Descripcion + '</option>').appendTo('#origenServicios');
                });
            },
            error: function () {
                console.log('err')
            }
        });
    }
    else if (numModal == 3) // Ciudades
    {
        $.ajax({
            url: '/CIUDAD/GetCiudad/',
            type: 'POST',
            dataType: 'json',
            data: { 'numModal': numModal, 'idConexion': idConexion },
            success: function (data) {
                //alert(data);
                $.each(data, function (i, item) {
                    $('<option value="' + item.Clv_Ciudad + '">' + item.Nombre + '</option>').appendTo('#origenCiudades');
                });
            },
            error: function () {
                console.log('err')
            }
        });

    }
    else if (numModal == 4) // Colonias
    {      
        $.ajax({
            url: '/COLONIA/GetColonia/',
            type: 'POST',
            dataType: 'json',
            data: { 'numModal': numModal, 'idConexion': idConexion },
            success: function (data) {
                console.log(data);
                //alert(data);
                $.each(data, function (i, item) {

                    $('<option value="' + item.clv_colonia + '">' + item.Nombre + '</option>').appendTo('#origenColonias');
                });
            },
            error: function () {
                console.log('err')
            }
        });



    }
    else if (numModal == 6) // Periodo
    {
        $.ajax({
            url: '/CatalogoPeriodosCorte/GetCatalogoPeriodosCorte/',
            type: 'POST',
            dataType: 'json',
            data: { 'numModal': numModal, 'idConexion': idConexion },
            success: function (data) {
                //alert(data);
                $.each(data, function (a, item1) {
                    $('<option value="' + item1.Clv_Periodo + '">' + item1.Descripcion + '</option>').appendTo('#origenPeriodo');
                });
            },
            error: function () {
                console.log('err')
            }
        });
    }
    else if (numModal == 8)// RangoFechas
    {
        //alert(numModal)
        $.ajax({
            url: '/MotivoCancelacion/GetMotivoCancelacion/',
            type: 'POST',
            dataType: 'json',
            data: { 'numModal': numModal, 'idConexion': idConexion },
            success: function (data) {
                console.log(data);
                //alert(data);
                $.each(data, function (a, item1) {
                    $('<option value="' + item1.Clv_MOTCAN + '">' + item1.MOTCAN + '</option>').appendTo('#listaMotivoCancelacion');
                });
            },
            error: function () {
                console.log('err')
            }
        });
    }
    else if (numModal == 11) //Calles
    {
        $.ajax({
            url: '/CALLE/GetCALLE/',
            type: 'POST',
            dataType: 'json',
            data: { 'numModal': numModal, 'idConexion': idConexion },
            success: function (data) {
                //alert(data);
                $.each(data, function (i, item) {
                    $('<option value="' + item.Clv_Calle + '">' + item.NOMBRE + '</option>').appendTo('#origenCalles');
                });
            },
            error: function () {
                console.log('err')
            }
        });
    }
    else if (numModal == 12) //EstatusCliente
    {
        //alert(numModal)
        $.ajax({
            url: '/MotivoCancelacion/GetMotivoCancelacion/',
            type: 'POST',
            dataType: 'json',
            data: { 'numModal': numModal, 'idConexion': idConexion },
            success: function (data) {
                console.log(data);
                $.each(data, function (a, item1) {
                    $('<option value="' + item1.Clv_MOTCAN + '">' + item1.MOTCAN + '</option>').appendTo('#listaMotivoCancelacion12');
                });
            },
            error: function () {
                console.log('err')
            }
        });
    }
}


$(document).ready(function () {
    $.ajax({
        url: "/Conexion/Plazas/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            for (var i = 0; i < data.length; i++) {
                $('#listaConexionPlaza').append($('<option>', {
                    value: data[i].IdConexion,
                    text: data[i].Plaza
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
});


