$("#tipo_llamada").change(function () {
    var tipo = $("#tipo_llamada").val();
    if(tipo == 1){
        $('#no_contrato').show();
        $('#nombre_llamada').hide();
    } else if(tipo == 2) {
        $('#no_contrato').hide();
        $('#nombre_llamada').show();
    }
});

$("#motivo_llamada").change(function () {
    var tipo = $("#motivo_llamada").val();
    if (tipo == 1) {
        $('#panel_queja').hide();
    } else if (tipo == 2) {
        $('#panel_queja').show();
    } else if (tipo == 3) {
        $('#panel_queja').hide();
    }
});
$("#accion_queja").change(function () {
    var tipo = $("#accion_queja").val();
    if (tipo == 1) {
        $('#panel_reporte').hide();
    } else if (tipo == 2) {
        $('#panel_reporte').show();
    }
});
