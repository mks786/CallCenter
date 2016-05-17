//START tooltips Section
function putTooltips() {
    //btn editar
    $('.btnedit').qtip({
        position: {
            my: 'right top',
            at: 'top rigth'
        },
        style: {
            classes: 'qtip-bootstrap',
            tip: {
                corner: true,
                mimic: 'bottom left',
                border: 1,
                width: 88,
                height: 56
            }
        },

        content: {
            text: 'Editar'
        }
    });

    //btn detalles
    $('.btndetails').qtip({
        position: {
            my: 'right top',
            at: 'top rigth'
        },
        style: {
            classes: 'qtip-bootstrap',
            tip: {
                corner: true,
                mimic: 'bottom left',
                border: 1,
                width: 88,
                height: 56
            }
        },

        content: {
            text: 'Detalles'
        }
    });

    //btn eliminar
    $('.btndelete').qtip({
        position: {
            my: 'right top',
            at: 'top rigth'
        },
        style: {
            classes: 'qtip-bootstrap',
            tip: {
                corner: true,
                mimic: 'bottom left',
                border: 1,
                width: 88,
                height: 56
            }
        },

        content: {
            text: 'Eliminar'
        }
    });

    //btn actualizar
    $('#btnupdate').qtip({
        position: {
            my: 'right top',
            at: 'top rigth'
        },
        style: {
            classes: 'qtip-bootstrap',
            tip: {
                corner: true,
                mimic: 'bottom left',
                border: 1,
                width: 88,
                height: 56
            }
        },

        content: {
            text: 'Actualizar'
        }
    });

    
   

    //btn nuevo
    $('.btn').qtip({
        position: {
            my: 'right top',
            at: 'top rigth'
        },
        style: {
            classes: 'qtip-bootstrap',
            tip: {
                corner: true,
                mimic: 'bottom left',
                border: 1,
                width: 88,
                height: 56
            }
        },

        content: {
            text: 'Nuevo'
        }
    });
    //END tooltips Section
}