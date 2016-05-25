

function ShowCatalogoPreguntas(valor, tipo) {
   
    
    if (tipo == ""|| tipo==undefined) {
        tipo = 0;
    }

    if (valor == "" || valor == undefined) {
        valor = "";
    }

    $('#ModalCatalogoPreguntas').modal('show');
    
    $('#TablaCatalogoPreguntas').dataTable({
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "dom": '<"toolbar">frtip',
        "bDestroy": true,
        "info": true,
        "stateSave": true,
        "lengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "ajax": {
            "url": "/Pregunta/GetDetallePregunta/",
            "type": "POST",
            "data": {'data':valor,'tipo':tipo},           
        },        
        "columns": [
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                    
                    return full.Pregunta.Pregunta;
                }
            },
            {
                sortable: false,
                "render": function (data, type, full, meta) {
                   
                    return full.Pregunta.TipoPreguntas.Descripcion;
                }
            },
           
        {
            sortable: false,
            "render": function (data, type, full, meta) {                
                var detalle = full.Pregunta.IdPregunta + "/" + full.Pregunta.IdTipoPregunta + "/" + full.Pregunta.Pregunta + "/" + full.Pregunta.TipoPreguntas.Descripcion+"/"
                for (var o = 0; o < full.Respuestas.length; o++){
                    detalle += "*" + full.Respuestas[o].Id_ResOpcMult + "," + full.Respuestas[o].ResOpcMult ;
                }
                
                return "<button class='btn btn-info btn-xs AgregarPreguntaCatalogo' rel='"+detalle+"'><i class='fa fa-exchange' aria-hidden='true'></i> Agrega a encuesta</button>";
            }
        }
        ],

        language: {
            processing: "Buscando preguntas",
            search: "Buscar&nbsp;:",
            lengthMenu: "Mostrar _MENU_ Elementos",
            info: "Mostrando   _START_ de _END_ Total _TOTAL_ elementos",
            infoEmpty: "No hay elemetos para mostrar",
            infoFiltered: "(filtrados _MAX_ )",
            infoPostFix: "",
            loadingRecords: "Búsqueda en curso...",
            zeroRecords: "No hay registros para mostrar",
            emptyTable: "No hay registros disponibles",
            paginate: {
                first: "Primera",
                previous: "Anterior",
                next: "Siguiente",
                last: "Ultima"
            },
            aria: {
                sortAscending: ": activer pour trier la colonne par ordre croissant",
                sortDescending: ": activer pour trier la colonne par ordre décroissant"
            }
        },


        "order": [[0, "asc"]]
    })

   // $("div.toolbar").html(' ');

}

function Buscar() {

    var valor = $('#search').val();
    alert(valor);
    ShowCatalogoPreguntas(valor,null);

}


$('#FiltraTipoPregunta').change(function () {  
    ShowCatalogoPreguntas(null, $(this).val());
});

$('#TablaCatalogoPreguntas').on('click', '.AgregarPreguntaCatalogo', function () {
    
    var detalle = $(this).attr('rel');
    var elem = detalle.split('/');
    var Idpregunta = elem[0];
    var IdTipopregunta = elem[1];
    var Pregunta = elem[2];
    var txtTipoPregunta = elem[3];


    var detallePregunta = {};
    detallePregunta.IdPregunta = generateUUID();
    detallePregunta.IdPregunta2 = Idpregunta;
    detallePregunta.Pregunta = Pregunta;
    detallePregunta.IdTipoPregunta = IdTipopregunta;
    detallePregunta.txtTipoPregunta = txtTipoPregunta;
    
    var result = $.grep(Lista_preguntas, function (e) { return e.IdPregunta == detallePregunta.IdPregunta || e.IdPregunta2 == detallePregunta.IdPregunta2; });
    console.log(result);
    if (result.length>0){
        sweetAlert("Oops...", "La pregunta ya esta en tu encuesta", "error");
    } else {
       
        swal("Hecho", "La pregunta se ha agregado", "success");
        Lista_preguntas.push(detallePregunta);
        var respuestas = elem[4];
        var res = respuestas.split('*');
        for (var u = 1; u < res.length; u++) {
            var s = res[u].split(',');
            Opciones = {};
            Opciones.Id_ResOpcMult = detallePregunta.IdPregunta;
            Opciones.Id_ResOpcMult2 = s[0];
            Opciones.ResOpcMult = s[1];            
            Lista_opciones.push(Opciones);
        }
        ActualizaListaPreguntas(false);
    }
   
    
});

