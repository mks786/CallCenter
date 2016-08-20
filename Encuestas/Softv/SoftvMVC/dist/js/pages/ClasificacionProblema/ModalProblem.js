function agregar() {
    $('#ModalClasificacionProblem').modal('show');
}


function enviar_problema() {

    if (document.getElementById('Activo').checked) { var a = true } else { var a = false }

    var objClasificacionProblema = {};

    objClasificacionProblema.Descripcion = $('#Descripcion').val();
    objClasificacionProblema.Activo = a;
    objClasificacionProblema.TipServ = $('#tipo_servicio_select').val();


    if (objClasificacionProblema.Descripcion == "") {
        swal("Por favor ingrese un Problema", "", "error");
    }
    else {
        $.ajax({
            url: "/ClasificacionProblema/Create/",
            type: "POST",
            data: { 'objClasificacionProblema': objClasificacionProblema },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Clasificación de Problema agregado con éxito!",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#5cb85c",
                    confirmButtonText: "Aceptar",
                    cancelButtonText: "Aceptar",
                    closeOnConfirm: false,
                    closeOnCancel: false
                }, function (isConfirm) {
                    location.reload();
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }
}

function detalle_Problema(data) {
    
    var IdProblema = (data.getAttribute('id'));
    var Descripcion = (data.getAttribute('data-name'));
    var Activo = (data.getAttribute('data-name2'));
    var TipoServicio = (data.getAttribute('data-name3'));
    $.ajax({
        url: "/ClasificacionProblema/getAllServicios/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            $('#tipo_servicio_edit').empty();
            for (var i = 0; i < data.length; i++) {
                if (TipoServicio == data[i].descripcion) {
                    $('#tipo_servicio_edit').append('<option value="' + data[i].descripcion + '" selected> ' + data[i].descripcion + '</option>');
                } else {
                    $('#tipo_servicio_edit').append('<option value="' + data[i].descripcion + '"> ' + data[i].descripcion + '</option>');
                }
              
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });


    $('#Descripcion_editar').val(Descripcion);
    $('.detalle_Problem').attr('id', data.getAttribute('id'));


    if (Activo == "true") {
        $('#Activo_detalle').prop("checked", true);
    } else {
        $('#Activo_detalle').prop("checked", false);
    }

    $('#ModalDetalleClasificacionProblem').modal('show');
}


function editar_Problema(data) {

    var IdProblema = (data.getAttribute('id'));
    var Descripcion = (data.getAttribute('data-name'));
    var Activo = (data.getAttribute('data-name2'));
    var TipoServicio = (data.getAttribute('data-name3'));
    console.log(TipoServicio);
    $.ajax({
        url: "/ClasificacionProblema/getAllServicios/",
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            $('#tipo_servicio_edit').empty();
            for (var i = 0; i < data.length; i++) {
                if(i == 0 && TipoServicio != "Todos"){
                    $('#tipo_servicio_edit').append('<option value="Todos">Todos</option>');
                } else {
                    if (i == data.length -1 &&TipoServicio == "Todos") {
                        $('#tipo_servicio_edit').append('<option value="Todos" selected>Todos</option>');
                    }
                }
                 
                if (TipoServicio == data[i].descripcion) {
                    $('#tipo_servicio_edit').append('<option value="' + data[i].descripcion + '" selected> ' + data[i].descripcion + '</option>');
                } else {
                    $('#tipo_servicio_edit').append('<option value="' + data[i].descripcion + '"> ' + data[i].descripcion + '</option>');
                }
                    
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });

    $('#descripcion_editar').val(data.getAttribute('data-name'));
    $('#id_clasificacion').val(data.getAttribute('id'));


    if (Activo == "true") {
        $('#Activo_editar').prop("checked", true);
    } else {
        $('#Activo_editar').prop("checked", false);
    }
    $('#ModalEditarClasificacionProblem').modal('show');

}



function enviar_editar_MotivoInfo() {
    var activo = $('#Activo_editar').prop("checked");
    if (activo) { var a = true } else { var a = false }
    var Descripcion = $('#descripcion_editar').val();
    var IdProblem = $('#id_clasificacion').val();


    var objClasificacionProblema = {};
    objClasificacionProblema.ClvProblema = IdProblem;
    objClasificacionProblema.Descripcion = Descripcion;
    objClasificacionProblema.Activo = a;
    objClasificacionProblema.TipServ = $('#tipo_servicio_edit').val();


    if (objClasificacionProblema.Descripcion == "") {
        swal("Por favor seleccione un Rol", "", "error");
    }
    else {

        $.ajax({
            url: "/ClasificacionProblema/Edit/",
            type: "POST",
            data: { 'objClasificacionProblema': objClasificacionProblema },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Clasificación de Problema editado con éxito!",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#5cb85c",
                    confirmButtonText: "Aceptar",
                    cancelButtonText: "Aceptar",
                    closeOnConfirm: false,
                    closeOnCancel: false
                }, function (isConfirm) {
                    location.reload();
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }
}


$('#ClasificacionProblem').on('click', '.eliminar', function () {


    var id = $(this).attr('id');
    var Descripcion = $(this).attr('data-name');

    $('#IdProblema').val(id);
    $('#Eliminar_Problema').text(Descripcion)
    $('#ModalEliminaClasificacionProblem').modal('show');

})

function confirmar_eliminar_Problema() {

    var IdProblema = $('#IdProblema').val();

    if (Descripcion == "") {
        swal("Por favor introduce el problema", "", "error");
    } else {
        $.ajax({
            url: "/ClasificacionProblema/Delete/",
            type: "POST",
            data: { 'id': IdProblema },
            success: function (data, textStatus, jqXHR) {
                swal({
                    title: "!Hecho!", text: "Clasificación del Problema eliminado con éxito!",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#5cb85c",
                    confirmButtonText: "Aceptar",
                    cancelButtonText: "Aceptar",
                    closeOnConfirm: false,
                    closeOnCancel: false
                }, function (isConfirm) {
                    location.reload();
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {

            }
        });
    }
}