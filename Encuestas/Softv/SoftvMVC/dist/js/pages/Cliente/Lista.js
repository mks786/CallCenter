$(document).ready(function () {


    /*DATOS GENERALES*/
    $('#contrato').val('');
    $('#nombre').val('');
    $('#calle').val('');
    $('#numero').val('');
    $('#cp').val('');
    $('#calles').val('');
    $('#colonia').val('');
    $('.telefono').mask('000-000-0000');
    $('#celular').mask('000-000-0000');
    $('#correo').val('');

    /*DATOS FISCALES*/
    $('#rfc').mask('AAAA-000-000');
    $('#curp').mask('AAAA-000000-AAAAAA-00');
    $('#rsocial').mask('AAAA-000000-AAAAAA-00');
});





$('#filtros').change(function () {
    var filtro = $(this).val();

    if (filtro == "1") {
        $('#Pcontrato').show();
        $('#Pdireccion').hide();
        $('#Pnombre').hide();
    }
    else if (filtro == "2") {
        $('#Pcontrato').hide();
        $('#Pdireccion').hide();
        $('#Pnombre').show();
    }
    else if (filtro == "3") {
        $('#Pcontrato').hide();
        $('#Pdireccion').show();
        $('#Pnombre').hide();
    }
    

});

