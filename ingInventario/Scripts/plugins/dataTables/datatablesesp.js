//DataTables a español
function dataTableEsp() {
    $('.dataTables-example').DataTable({
        pageLength: 25,
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [
            { extend: 'excel', title: 'ExampleFile' },
            { extend: 'pdf', title: 'ExampleFile' },

            {
                extend: 'print',
                text: 'Imprimir',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],
        language:
        {
            sProcessing: "Procesando...",
            sLengthMenu: "Mostrar _MENU_ registros",
            sZeroRecords: "No se encontraron resultados",
            sEmptyTable: "Ningún dato disponible en esta tabla",
            sInfo: "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            sInfoEmpty: "Mostrando registros del 0 al 0 de un total de 0 registros",
            sInfoFiltered: "(filtrado de un total de _MAX_ registros)",
            sInfoPostFix: "",
            sSearch: "Buscar:",
            sUrl: "",
            sInfoThousands: ",",
            sLoadingRecords: "Cargando...",
            oPaginate: {
                sFirst: "Primero",
                sLast: "Último",
                sNext: "Siguiente",
                sPrevious: "Anterior"
            },
            oAria: {
                sSortAscending: ": Activar para ordenar la columna de manera ascendente",
                sSortDescending: ": Activar para ordenar la columna de manera descendente"
            }
        }
    });
}
//Levanta el popUp de edición
function modal() {
    $('.btn-bootstrap-dialog').click(function () {
        var url = $(this).data('url');
        $.get(url, function (data) {
            $('#bootstrapDialog').html(data);
            $('#bootstrapDialog').modal('show');
        });
    });
}
//Levanta el sweetAlert de eliminar
function eliminar() {
    $('.btn-danger').click(function () {
        var id = $(this).attr('id');
        var nom = $(this).attr('data-nombre');
        swal({
            title: "¿Está seguro de eliminar el registro?",
            text: "Esta a punto de eliminar el registro de " + nom,
            icon: "warning",
            buttons: true,
            buttons: {
                cancel: {
                    text: "Cancelar",
                    visible: true
                },
                confirm: {
                    text: "Eliminar"
                }
            },
            dangerMode: true
        }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "POST",
                    url: '@Url.Action("Delete")' + '/' + id,
                    data: '{id:' + JSON.stringify(id) + '}',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (ok) {
                        swal("Eliminado!", ok, {
                            icon: "success"
                        });
                    }
                });
            }
        }).then(function () { window.location.reload(); });
    });
}