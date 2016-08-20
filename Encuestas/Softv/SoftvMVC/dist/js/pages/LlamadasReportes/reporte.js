$(document).ready(function () {
    $('.collapse').collapse('show');
    $.ajax({
        url: "/Conexion/Plazas/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            for (var i = 0; i < data.length; i++) {
                $('#plazas').append($('<option>', {
                    value: data[i].IdConexion,
                    text: data[i].Plaza
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('#motivo').on('change', function () {
        var tipo = $(this).val();
        if(tipo == 2){
            $('#problema').prop("disabled", false);
            $('#quejas').prop("disabled", false);
        } else {
            $('#problema').prop("disabled", true);
            $('#quejas').prop("disabled", true);
        }

    });
    $.ajax({
        url: "/Usuario/todosUsuarios/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            $("#usuario option").remove();
            $('#usuario').append('<option selected disabled>--------------------</option>');
            $('#usuario').append('<option value="999999">Todos los usuarios</option>');
            for (var i = 0; i < data.length; i++) {
                $('#usuario').append($('<option>', {
                    value: data[i].IdUsuario,
                    text: data[i].Nombre
                }));
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
    $('#plazas').on('change', function () {
        llenarCiudades($(this).val());
        $('#ciudades').removeAttr('disabled');
        $('#tipo_servicio').prop('disabled', false);
        $('#tipo_llamada').prop('disabled', false);
        $('#motivo').removeAttr('disabled');
        $('#inicio').removeAttr('disabled');
        $('#fin').removeAttr('disabled');
        $('#usuario').removeAttr('disabled');
        $('#btn-reporte').removeClass('disabled');
        $.ajax({
            url: "/tblClasificacionProblema/GetClasficacionProblemas/",
            type: "GET",
            data: { 'IdPlaza': $(this).val(), },
            success: function (data, textStatus, jqXHR) {
                alert('asffsdfdfdfs');
                console.log(data);
                $("#problema option").remove();
                $('#problema').append('<option selected disabled>--------------------</option>');
                $('#problema').append('<option value="999999">Todos los problemas</option>');
                for (var i = 0; i < data.length; i++) {
                    $('#problema').append($('<option>', {
                        value: data[i].ClvProblema,
                        text: data[i].Descripcion
                    }));
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
        $.ajax({
            url: "/CLIENTE/getTipoServicio/",
            type: "GET",
            data: { "IdPlaza": $(this).val() },
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
    }); 
});

function llenarCiudades(id) {
    $.ajax({
        url: "/Conexion/ListaConexiones/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            $('#ciudades').empty();
            $('#ciudades').append('<option selected disabled>--------------------</option>');
            if(data.length > 1){
                $('#ciudades').append('<option value="999999">Todas las Ciudades</option>');
            }
            for (var i = 0; i < data.length; i++) {
                if (data[i].IdPlaza == id) {
                    $('#ciudades').append($('<option>', {
                        value: data[i].IdPlaza,
                        text: data[i].Ciudad
                    }));
                } 
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}


function reportear() {
    var plaza = $('#plazas').val();
    var ciudad = $('#ciudades option:selected').text();
    var motivo = $('#motivo').val();
    var problema = $('#problema').val();
    var usuario = $('#usuario').val();
    var inicio = $('#inicio').val();
    var ifecha = new Date(inicio);
    inicio = ifecha.getDate() + 1 + "/" + (ifecha.getMonth() + 1) + "/" + ifecha.getFullYear();
    var queja = $('#quejas').val();
    var fin = $('#fin').val();
    var ffecha = new Date(fin);
    fin = ffecha.getDate() + 1 + "/" + (ffecha.getMonth() + 1) + "/" + ffecha.getFullYear();
    var reporte = {};
    if (ciudad == "--------------------") {
        reporte.ciudad = "";
    } else {
        reporte.ciudad = ciudad;
    }
    if (motivo == "99999") {
        reporte.motivo = 0;
    } else {
        reporte.motivo = motivo;
    }
    if(motivo == 2){
        if (problema == "999999") {
            reporte.problema = 0;
        } else {
            reporte.problema = problema;
        }
        reporte.queja = queja;
    }
    
    if (usuario == "999999") {
        reporte.usuario = 0;
    } else {
        reporte.usuario = usuario;
    }
    reporte.plaza = plaza;
    reporte.inicio = inicio;
    reporte.fin = fin;
    console.log(reporte);
    $.ajax({
        url: "/Llamada/ReporteLLamadas/",
        type: "GET",
        data:reporte,
        success: function (data, textStatus, jqXHR) {

        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

}