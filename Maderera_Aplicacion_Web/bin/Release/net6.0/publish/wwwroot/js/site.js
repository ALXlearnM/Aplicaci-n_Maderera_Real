
//GENERAL

//BARRA DE NAVEGACIÓN DESPLEGABLE
function toggleDropdown(id) {
    var dropdownContent = document.getElementById(id);

    // Close all dropdowns
    var allDropdowns = document.querySelectorAll('.dropdown-content');
    allDropdowns.forEach(function (dropdown) {
        if (dropdown.id !== id) {
            dropdown.style.display = "none";
        }
    });

    if (dropdownContent) {
        dropdownContent.style.display = (dropdownContent.style.display === "block") ? "none" : "block";
    }
}

//Abrir el Modal create de cualquier tabla 
function openSecondModal(modalId) {
    $('#' + modalId).modal('show');
}
// Función para cerrar el modal
function cerrarModal(modalId) {
    document.getElementById(modalId).style.display = "none";
}
//Función para llamar a un modal al seleccionar un option de un select  
function SelectChange(select, modalContentId) {
    const selectedValue = select.value;
    const modalTarget = select.dataset.modalTarget;

    if (selectedValue === 'buscar') {
        $('#' + modalTarget).modal('show');
    } else {
        closeModal(modalTarget, select);
    }
}

//Cerrar el modal y actualizar el select
function closeModal(modalId, selectId) {
    $('#' + modalId).on('hidden.bs.modal', function () {
        const select = document.getElementById(selectId);
        if (select) {
            select.selectedIndex = 0;
        }
    });

    $('#' + modalId).modal('hide');
}


//Función de Ejemplo para no cruzar formularios

function submitForm() {

    event.preventDefault();

}
//FUNCION QUE VERIFICA REQUERIDOS DE FORMULARIOS


function camposRequeridosLlenos(formId) {
    var camposLlenos = true;

    $('#' + formId + ' [required]').each(function () {
        if ($(this).val() === '') {
            camposLlenos = false;
            return false;  // Romper el bucle si al menos un campo está vacío
        }
    });

    return camposLlenos;
}
//CLIENTE


//Proceso para cerrar la ventana modal Cliente
$(document).ready(function () {
    $('#modalclim').on('hidden.bs.modal', function () {
        $('#clicbo').prop('selectedIndex', 0);
        cargarDatosCliente();
        // Vaciar los campos de filtro
        $('#filtroNombreCli').val('');
        $('#filtroRUCCli').val('');
    });
});


//Proceso para cerrar la ventana modal create Cliente

$(document).ready(function () {
    $('#createClientModal').on('hidden.bs.modal', function () {
        // Vaciar los campos de Crear
        cargarDatosCliente();
        $('#prim_nombrecli, #seg_nombrecli, #patapellidocli, #matapellidocli, #razonSocialcli, #nroRuccli, #celcli').val('');
        $('#provinciacli, #distritocli').empty().prop('selectedIndex', 0);
    });
});


//CBO DEPENDIENTE CLIENTE
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#departamentocli').change(function () {
        var departamentoId = $(this).val();

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerProvincias',
            type: 'GET',
            data: { departamentoId: departamentoId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#provinciacli').empty();
                $.each(data, function (index, item) {
                    $('#provinciacli').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
                });

                // Llama al evento change para actualizar los distritos
                $('#provinciacli').change();
            },
            error: function () {
                console.error('Error al obtener provincias.');
            }
        });
    });
    // Manejador de cambio para la provincia
    $('#provinciacli').change(function () {
        var provinciaId = $(this).val();

        // Realiza una solicitud AJAX para obtener los distritos
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerDistritos',
            type: 'GET',
            data: { provinciaId: provinciaId },
            success: function (data) {
                // Actualiza el contenido del select de distrito
                $('#distritocli').empty();
                $.each(data, function (index, item) {
                    $('#distritocli').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
                });
            },
            error: function () {
                console.error('Error al obtener distritos.');
            }
        });
    });
});

//INSERCION CLIENTE
$('#crearClienteBtn').click(function () {
    if (camposRequeridosLlenos('createClienteForm')) {
        var formData = new FormData();

        formData.append('txt_prim_nomb', $('#prim_nombrecli').val() || '');
        formData.append('txt_seg_nomb', $('#seg_nombrecli').val() || '');
        formData.append('txt_ape_pat', $('#patapellidocli').val() || '');
        formData.append('txt_ape_mat', $('#matapellidocli').val() || '');
        formData.append('razonsocial', $('#razonSocialcli').val() !== null ? $('#razonSocialcli').val() : '');
        formData.append('nro_ruc', $('#nroRuccli').val() !== null ? $('#nroRuccli').val() : '');
        formData.append('numtelefono', $('#celcli').val() || '');
        formData.append('id_distrito', $('#distritocli').val() || '');
        formData.append('direccioncli', $('#direccioncli').val() || '');


        $.ajax({
            url: '/Tnst04CompEmitido/CrearCliente',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                console.log('Cliente creado correctamente:', response);
                cargarDatosCliente();
                $('#createClientModal').modal('hide');
            },

            error: function (error) {
                console.error('Error al crear el cliente:', error);
            }
        });
    } else {
        alert('Por favor, complete todos los campos requeridos.');
    }
});
//CARGAR CLIENTE EN TABLA CLIENTE
function cargarDatosCliente() {
    $.ajax({
        url: '/Tnst04CompEmitido/RecargarCliente', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            /*console.log('Cliente:', response);*/
            // Clear the existing tbody content
            $('#bodycliente').empty();

            // Iterate through the updated client data and append rows to tbody
            $.each(response, function (index, cliente) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + cliente.idCliente + '</td>');
                row.append('<td align="center" class="client-cell">' + cliente.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + cliente.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + cliente.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + cliente.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarCliente(\'modalclim\',\'clicbo\',\'clienteID\',\'clienteNombre\',\'clienteTelefono\',\'clienteDireccion\', \'' +
                    cliente.idCliente + '\', \'' + cliente.nombreCompleto + '\', \'' + cliente.telefono + '\', \'' + cliente.direccion + '\')">Seleccionar</button>' +
                    '</td>');

                // Append the row to the tbody
                $('#bodycliente').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del cliente:', error.responseText);
        }
    });
}

//Cargar la selección de Cliente
function seleccionarCliente(modalId, selectId, idcod, idtext, idtelefono, iddireccion, idCliente, nombreCliente, telefono, direccion) {
    event.preventDefault();
    var clienteIdInput = document.getElementById(idcod);
    clienteIdInput.value = idCliente;
    /*    document.getElementById(idcod).innerText = idCliente;*/
    document.getElementById(idtext).innerText = nombreCliente;
    document.getElementById(iddireccion).innerText = direccion;
    document.getElementById(idtelefono).innerText = telefono;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var clienteNombreSpan = document.getElementById('clienteNombre');
    var removeClienteSpan = document.getElementById('removeClienteSpan');
    clienteNombreSpan.innerText = nombreCliente;
    clienteNombreSpan.style.display = 'inline-block';
    removeClienteSpan.style.display = 'inline-block';

    // Ocultar el select
    var selectElement = document.getElementById('clicbo');
    selectElement.style.display = 'none';
}

//El span cliente para volver al select
function removeCliente() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('clienteNombre').innerText = '';
    document.getElementById('clienteID').value = '';
    document.getElementById('clienteDireccion').innerText = '';
    document.getElementById('clienteTelefono').innerText = '';
    document.getElementById('clienteNombre').style.display = 'none';
    document.getElementById('removeClienteSpan').style.display = 'none';
    var selectElement = document.getElementById('clicbo');
    selectElement.style.display = 'inline-block';

}

//filtro de Cliente por Nombre y Ruc
function filtrarClientes() {
    var filtroNombre = document.getElementById('filtroNombreCli').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCCli').value.toLowerCase();

    // Recorremos todas las filas de la tabla
    $('.tablaClientes tbody tr').each(function () {
        var nombreCliente = $(this).find('.client-cell:nth-child(2)').text().toLowerCase();
        var rucCliente = $(this).find('.client-cell:nth-child(5)').text().toLowerCase();

        // Comprobamos si coincide con el filtro de nombre y/o RUC desde el inicio de la palabra
        var nombreCoincide = nombreCliente.startsWith(filtroNombre);
        var rucCoincide = rucCliente.startsWith(filtroRUC);

        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

//CHOFER

//Proceso para cerrar la ventana modal CHOFER
$(document).ready(function () {
    $('#modalcho').on('hidden.bs.modal', function () {
        $('#chocbo').prop('selectedIndex', 0);

        //Vaciar los campos de filtro
        $('#filtroNombreCho').val('');
        $('#filtroRUCCho').val('');
        cargarDatosChofer();
    });
});


//Proceso para cerrar la ventana modal create Chofer
$(document).ready(function () {
    $('#createChoferModal').on('hidden.bs.modal', function () {
        // Vaciar los campos de Crear
        $('#prim_nombrecho, #seg_nombrecho, #patapellidocho, #matapellidocho, #razonSocialcho, #nroRuccho, #celcho').val('');
        $('#provinciacho, #distritocho').empty().prop('selectedIndex', 0);
        cargarDatosChofer();
    });
});

//INSERCION CHOFER
$(document).ready(function () {
    $('#crearChoferBtn').click(function () {
        if (camposRequeridosLlenos('createChoferForm')) {
            // Resto del código para crear chofer
            var formData = new FormData();
            formData.append('txt_prim_nomb', $('#prim_nombrecho').val());
            formData.append('txt_seg_nomb', $('#seg_nombrecho').val());
            formData.append('txt_ape_pat', $('#patapellidocho').val());
            formData.append('txt_ape_mat', $('#matapellidocho').val());
            formData.append('razonsocial', $('#razonSocialcho').val());
            formData.append('nro_ruc', $('#nroRuccho').val());
            formData.append('numtelefono', $('#celcho').val());
            formData.append('id_distrito', $('#distritocho').val());
            formData.append('direccion', $('#direccioncho').val());
            formData.append('id_cat', 2);

            $.ajax({
                url: '/Tnst04CompEmitido/CrearChofer',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    cargarDatosChofer();
                    $('#createChoferModal').modal('hide');
                },
                error: function (error) {
                    console.error('Error al crear el chofer:', error);
                }
            });
        } else {
            alert('Por favor, complete todos los campos requeridos.');
        }
    });
});
//CARGAR CHOFER EN TABLA CHOFER
function cargarDatosChofer() {
    $.ajax({
        url: '/Tnst04CompEmitido/RecargarChofer', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodychofer').empty();
                    $.each(response, function (index, chofer) {
                        var row = $('<tr>');
                        row.append('<td align="center" class="client-cell">' + chofer.idempleado + '</td>');
                        row.append('<td align="center" class="client-cell">' + chofer.nombreCompleto + '</td>');
                        row.append('<td align="center" class="client-cell">' + chofer.direccion + '</td>');
                        row.append('<td align="center" class="client-cell">' + chofer.telefono + '</td>');
                        row.append('<td align="center" class="client-cell">' + chofer.ruc + '</td>');
                        row.append('<td align="center" class="client-cell">' +
                            '<button class="btn btn-primary" onclick="seleccionarChofer(\'modalcho\',\'chocbo\',\'choferId\',\'choferNombre\',\'' +
                            chofer.idempleado + '\', \'' + chofer.nombreCompleto + '\')">Seleccionar</button>' +
                            '</td>');
                        $('#bodychofer').append(row);
                    });
        },
        error: function (error) {
            console.error('Error al cargar los datos del chofer:', error.responseText);
        }
    });
}

//CBO DEPENDIENTE CHOFER
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#departamentocho').change(function () {
        var departamentoId = $(this).val();

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerProvincias',
            type: 'GET',
            data: { departamentoId: departamentoId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#provinciacho').empty();
                $.each(data, function (index, item) {
                    $('#provinciacho').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
                });

                // Llama al evento change para actualizar los distritos
                $('#provinciacho').change();
            },
            error: function () {
                console.error('Error al obtener provincias.');
            }
        });
    });
    // Manejador de cambio para la provincia
    $('#provinciacho').change(function () {
        var provinciaId = $(this).val();

        // Realiza una solicitud AJAX para obtener los distritos
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerDistritos',
            type: 'GET',
            data: { provinciaId: provinciaId },
            success: function (data) {
                // Actualiza el contenido del select de distrito
                $('#distritocho').empty();
                $.each(data, function (index, item) {
                    $('#distritocho').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
                });
            },
            error: function () {
                console.error('Error al obtener distritos.');
            }
        });
    });
});

//Cargar la selección de Chofer
function seleccionarChofer(modalId, selectId, idcod, idtext,idChofer, nombreChofer) {
    event.preventDefault();
    var choferIdInput = document.getElementById(idcod);
    choferIdInput.value = idChofer;
    document.getElementById(idtext).innerText = nombreChofer;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var choferNombreSpan = document.getElementById('choferNombre');
    var removeChoferSpan = document.getElementById('removeChoferSpan');
    choferNombreSpan.innerText = nombreChofer;
    choferNombreSpan.style.display = 'inline-block';
    removeChoferSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('chocbo');
    selectElement.style.display = 'none';
}

//El span cliente para volver al select
function removeChofer() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('choferNombre').innerText = '';
    document.getElementById('choferId').value = '';
    document.getElementById('choferNombre').style.display = 'none';
    document.getElementById('removeChoferSpan').style.display = 'none';
    var selectElement = document.getElementById('chocbo');
    selectElement.style.display = 'inline-block';
}

//filtro de Chofer por Nombre y Ruc
function filtrarChoferes() {
    var filtroNombre = document.getElementById('filtroNombreCho').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCCho').value.toLowerCase();

    // Recorremos todas las filas de la tabla
    $('.tablaChoferes tbody tr').each(function () {
        var nombreChofer = $(this).find('.client-cell:nth-child(2)').text().toLowerCase();
        var rucChofer = $(this).find('.client-cell:nth-child(5)').text().toLowerCase();

        // Comprobamos si coincide con el filtro de nombre y/o RUC desde el inicio de la palabra
        var nombreCoincide = nombreChofer.startsWith(filtroNombre);
        var rucCoincide = rucChofer.startsWith(filtroRUC);

        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

//OPERADOR
$(document).ready(function () {
    $('#modalope').on('hidden.bs.modal', function () {
        $('#opecbo').prop('selectedIndex', 0);
        cargarDatosOperario();
         /*Vaciar los campos de filtro*/
        $('#filtroNombreOpe').val('');
        $('#filtroRUCOpe').val('');

    });
});


//Proceso para cerrar la ventana modal create Operario
$(document).ready(function () {
    $('#createOperarioModal').on('hidden.bs.modal', function () {
        cargarDatosOperario();
        // Vaciar los campos de Crear
        $('#prim_nombreope, #seg_nombreope, #patapellidoope, #matapellidoope, #razonSocialope, #nroRucope, #celope').val('');
        $('#provinciaope, #distritoope').empty().prop('selectedIndex', 0);

    });
});
//INSERCION OPERARIO
$(document).ready(function () {
    $('#crearOperarioBtn').click(function () {
        // Crear un objeto FormData
        var formData = new FormData();

        /*Obtener los valores del formulario y agregarlos a FormData*/
        formData.append('txt_prim_nomb', $('#prim_nombreope').val());
        formData.append('txt_seg_nomb', $('#seg_nombreope').val());
        formData.append('txt_ape_pat', $('#patapellidoope').val());
        formData.append('txt_ape_mat', $('#matapellidoope').val());
        formData.append('razonsocial', $('#razonSocialope').val());
        formData.append('nro_ruc', $('#nroRucope').val());
        formData.append('numtelefono', $('#celope').val());
        formData.append('id_distrito', $('#distritoope').val());
        formData.append('direccion', $('#direccionope').val());
        formData.append('id_cat', 3);
        // Realizar la solicitud AJAX al controlador
        $.ajax({
            url: '/Tnst04CompEmitido/CrearOperario',
            type: 'POST',
            data: formData,
            processData: false,  // No procesar los datos (dejar que FormData lo haga)
            contentType: false,  // No establecer contentType (FormData lo establece correctamente)
            success: function (response) {
                console.log('Operario creado correctamente:', response);
                cargarDatosOperario();
                $('#createOperarioModal').modal('hide');

            },
            error: function (error) {
                console.error('Error al crear el operario:', error);
                // Aquí puedes manejar errores si es necesario
            }
        });
    });
});
//CARGAR OPERARIO EN TABLA OPERARIO
function cargarDatosOperario() {
    $.ajax({
        url: '/Tnst04CompEmitido/RecargarOperario', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            /*console.log('Operario:', response);*/
            $('#bodyope').empty();

            // Iterate through the updated operario data and append rows to tbody
            $.each(response, function (index, operario) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + operario.idempleado + '</td>');
                row.append('<td align="center" class="client-cell">' + operario.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + operario.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + operario.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + operario.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarOperario(\'modalope\',\'opecbo\',\'OperarioId\',\'operarioNombre\',\'' +
                    operario.idempleado + '\', \'' + operario.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodyope').append(row);
            });
            
        },
        error: function (error) {
            console.error('Error al cargar los datos del Operario:', error.responseText);
        }
    });
}

//CBO DEPENDIENTE OPERARIO
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#departamentoope').change(function () {
        var departamentoId = $(this).val();

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerProvincias',
            type: 'GET',
            data: { departamentoId: departamentoId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#provinciaope').empty();
                $.each(data, function (index, item) {
                    $('#provinciaope').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
                });

                // Llama al evento change para actualizar los distritos
                $('#provinciaope').change();
            },
            error: function () {
                console.error('Error al obtener provincias.');
            }
        });
    });

    // Manejador de cambio para la provincia
    $('#provinciaope').change(function () {
        var provinciaId = $(this).val();

        // Realiza una solicitud AJAX para obtener los distritos
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerDistritos',
            type: 'GET',
            data: { provinciaId: provinciaId },
            success: function (data) {
                // Actualiza el contenido del select de distrito
                $('#distritoope').empty();
                $.each(data, function (index, item) {
                    $('#distritoope').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
                });
            },
            error: function () {
                console.error('Error al obtener distritos.');
            }
        });
    });
});



//Cargar la selección de Operario
function seleccionarOperario(modalId, selectId, idcod, idtext, idOperario, nombreOperario) {
    event.preventDefault();
    var operarioIdInput = document.getElementById(idcod);
    operarioIdInput.value = idOperario;
    document.getElementById(idtext).innerText = nombreOperario;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var operarioNombreSpan = document.getElementById('operarioNombre');
    var removeOperarioSpan = document.getElementById('removeOperarioSpan');
    operarioNombreSpan.innerText = nombreOperario;
    operarioNombreSpan.style.display = 'inline-block';
    removeOperarioSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('opecbo');
    selectElement.style.display = 'none';

}


//El span cliente para volver al select
function removeOperario() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('operarioNombre').innerText = '';
    document.getElementById('OperarioId').value = '';
    document.getElementById('operarioNombre').style.display = 'none';
    document.getElementById('removeOperarioSpan').style.display = 'none';
    var selectElement = document.getElementById('opecbo');
    selectElement.style.display = 'inline-block';

}
function filtrarOperario() {
    var filtroNombre = document.getElementById('filtroNombreOpe').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCOpe').value.toLowerCase();

    // Recorremos todas las filas de la tabla
    $('.tablaOperario tbody tr').each(function () {
        var nombreOperario = $(this).find('.client-cell:nth-child(2)').text().toLowerCase();
        var rucOperario = $(this).find('.client-cell:nth-child(5)').text().toLowerCase();

        // Comprobamos si coincide con el filtro de nombre y/o RUC desde el inicio de la palabra
        var nombreCoincide = nombreOperario.startsWith(filtroNombre);
        var rucCoincide = rucOperario.startsWith(filtroRUC);

        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}


//AUTORIZADOR

//Proceso para cerrar la ventana modal AUTORIZADOR
$(document).ready(function () {
    $('#modalaut').on('hidden.bs.modal', function () {
        $('#autcbo').prop('selectedIndex', 0);
        cargarDatosAutorizador();
        // Vaciar los campos de filtro
        $('#filtroNombreAut').val('');
        $('#filtroRUCAut').val('');
    });
});


//Proceso para cerrar la ventana modal create Autorizador
$(document).ready(function () {
    $('#createAutorizadorModal').on('hidden.bs.modal', function () {
        cargarDatosAutorizador();
        // Vaciar los campos de Crear
        $('#prim_nombreaut, #seg_nombreaut, #patapellidoaut, #matapellidoaut, #razonSocialaut, #nroRucaut, #celaut').val('');
        $('#provinciaaut, #distritoaut').empty().prop('selectedIndex', 0);

    });
});

//INSERCION AUTORIZADOR
$(document).ready(function () {
    $('#crearAutorizadorBtn').click(function () {
        // Validar que los campos requeridos estén llenos
        if (camposRequeridosLlenos('createAutorizadorForm')) {
            // Crear un objeto FormData
            var formData = new FormData();

            /*Obtener los valores del formulario y agregarlos a FormData*/
            formData.append('txt_prim_nomb', $('#prim_nombreaut').val());
            formData.append('txt_seg_nomb', $('#seg_nombreaut').val());
            formData.append('txt_ape_pat', $('#patapellidoaut').val());
            formData.append('txt_ape_mat', $('#matapellidoaut').val());
            formData.append('razonsocial', $('#razonSocialaut').val());
            formData.append('nro_ruc', $('#nroRucaut').val());
            formData.append('numtelefono', $('#celaut').val());
            formData.append('id_distrito', $('#distritoaut').val());
            formData.append('direccion', $('#direccionaut').val());
            formData.append('id_cat', 4);

            // Realizar la solicitud AJAX al controlador
            $.ajax({
                url: '/Tnst04CompEmitido/CrearAutorizador',
                type: 'POST',
                data: formData,
                processData: false,  // No procesar los datos (dejar que FormData lo haga)
                contentType: false,  // No establecer contentType (FormData lo establece correctamente)
                success: function (response) {
                    /*console.log('Autorizador creado correctamente:', response);*/
                    cargarDatosAutorizador();
                    $('#createAutorizadorModal').modal('hide');
                },
                error: function (error) {
                    console.error('Error al crear el autorizador:', error);
                    // Aquí puedes manejar errores si es necesario
                }
            });
        } else {
            alert('Por favor, complete todos los campos requeridos.');
        }
    });
});

//CARGAR AUTORIZADOR EN TABLA AUTORIZADOR
function cargarDatosAutorizador() {
    $.ajax({
        url: '/Tnst04CompEmitido/RecargarAutorizador', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            /*console.log('Autorizador:', response);*/
            $('#bodyaut').empty();

            // Iterate through the updated autorizador data and append rows to tbody
            $.each(response, function (index, autorizador) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + autorizador.idempleado + '</td>');
                row.append('<td align="center" class="client-cell">' + autorizador.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + autorizador.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + autorizador.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + autorizador.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarAutorizador(\'modalaut\',\'autcbo\',\'AutorizadorId\',\'autorizadorNombre\',\'' +
                    autorizador.idempleado + '\', \'' + autorizador.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodyaut').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del Autorizador:', error.responseText);
        }
    });
}

//CBO DEPENDIENTE AUTORIZADOR
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#departamentoaut').change(function () {
        var departamentoId = $(this).val();

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerProvincias',
            type: 'GET',
            data: { departamentoId: departamentoId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#provinciaaut').empty();
                $.each(data, function (index, item) {
                    $('#provinciaaut').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
                });

                // Llama al evento change para actualizar los distritos
                $('#provinciaaut').change();
            },
            error: function () {
                console.error('Error al obtener provincias.');
            }
        });
    });

    // Manejador de cambio para la provincia
    $('#provinciaaut').change(function () {
        var provinciaId = $(this).val();

        // Realiza una solicitud AJAX para obtener los distritos
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerDistritos',
            type: 'GET',
            data: { provinciaId: provinciaId },
            success: function (data) {
                // Actualiza el contenido del select de distrito
                $('#distritoaut').empty();
                $.each(data, function (index, item) {
                    $('#distritoaut').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
                });
            },
            error: function () {
                console.error('Error al obtener distritos.');
            }
        });
    });

});

//Cargar la selección de AUTORIZADOR
function seleccionarAutorizador(modalId, selectId, idcod, idtext, idAut, nombreAut) {
    event.preventDefault();
    var AutorizadorIdInput = document.getElementById(idcod);
    AutorizadorIdInput.value = idAut;
    document.getElementById(idtext).innerText = nombreAut;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var AutorizadorNombreSpan = document.getElementById('autorizadorNombre');
    var removeAutorizadorSpan = document.getElementById('removeAutorizadorSpan');
    AutorizadorNombreSpan.innerText = nombreAut;
    AutorizadorNombreSpan.style.display = 'inline-block';
    removeAutorizadorSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('autcbo');
    selectElement.style.display = 'none';

}

//El span AUTORIZADOR para volver al select
function removeAutorizador() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('autorizadorNombre').innerText = '';
    document.getElementById('AutorizadorId').value = '';
    document.getElementById('autorizadorNombre').style.display = 'none';
    document.getElementById('removeAutorizadorSpan').style.display = 'none';
    var selectElement = document.getElementById('autcbo');
    selectElement.style.display = 'inline-block';

}

//filtro de AUTORIZADOR por Nombre y Ruc
function filtrarAutorizador() {
    var filtroNombre = document.getElementById('filtroNombreAut').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCAut').value.toLowerCase();

    // Recorremos todas las filas de la tabla
    $('.tablaautorizador tbody tr').each(function () {
        var nombreAutorizador= $(this).find('.client-cell:nth-child(2)').text().toLowerCase();
        var rucAutorizador = $(this).find('.client-cell:nth-child(5)').text().toLowerCase();

        // Comprobamos si coincide con el filtro de nombre y/o RUC desde el inicio de la palabra
        var nombreCoincide = nombreAutorizador.startsWith(filtroNombre);
        var rucCoincide = rucAutorizador.startsWith(filtroRUC);

        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}
    //LOCATION
    //Proceso para cerrar la ventana modal LOCATION
$(document).ready(function () {
    $('#modalloc').on('hidden.bs.modal', function () {
        $('#loccbo').prop('selectedIndex', 0);
        EleccionCarga();
        // Vaciar los campos de filtro
        $('#filtroNombreLoc').val('');
        $('#filtroRUCLoc').val('');
    });
});
    //Proceso para cerrar la ventana modal create Location
$(document).ready(function () {
    $('#createLocalizacionModal').on('hidden.bs.modal', function () {
        // Vaciar los campos de Crear
        EleccionCarga();
        $('#prim_nombreloc, #seg_nombreloc, #patapellidoloc, #matapellidoloc, #razonSocialloc, #nroRucloc, #celloc').val('');
        $('#provincialoc, #distritoloc').empty().prop('selectedIndex', 0);
    });
});


    //INSERCION LOCATION
$(document).ready(function () {
    $('#crearLocalizacionBtn').click(function () {
        // Validar que los campos requeridos estén llenos
        if (camposRequeridosLlenos('createLocalizacionForm')) {
            // Crear un objeto FormData
            var formData = new FormData();

            /*Obtener los valores del formulario y agregarlos a FormData*/
            formData.append('nombreloc', $('#nombreloc').val());
            formData.append('fechanegloc', $('#fechanegloc').val());
            formData.append('IdTipoLocation', $('#IdTipoLocation').val());
            formData.append('id_distrito', $('#distritoloc').val());
            formData.append('direccionloc', $('#direccionloc').val());
            // Realizar la solicitud AJAX al controlador
            $.ajax({
                url: '/Tnst04CompEmitido/CrearLocalizacion',
                type: 'POST',
                data: formData,
                processData: false,  // No procesar los datos (dejar que FormData lo haga)
                contentType: false,  // No establecer contentType (FormData lo establece correctamente)
                success: function (response) {
                    console.log('Localización creado correctamente:', response);

                    EleccionCarga();

                    $('#createLocalizacionModal').modal('hide');
                },

                error: function (error) {
                    console.error('Error al crear la Localización:', error);
                    // Aquí puedes manejar errores si es necesario
                }
            });
        } else {
            alert('Por favor, complete todos los campos requeridos.');
        }
    });
});

function EleccionCarga() {
    var LocalizadorIdInput = document.getElementById('Locationid');
    var LocalizadortoIdInput = document.getElementById('Locationtoid');
    var valorLoc = LocalizadorIdInput.value;
    var valorLocto = LocalizadortoIdInput.value;

    if ((valorLoc == null && valorLocto == null) ||
        (valorLoc == '' && valorLocto == '') || 
        (valorLoc != null && valorLocto != null &&
        valorLoc != '' && valorLocto != '')) {
        cargarDatosLocalizacion(null);
        console.log(valorLoc);
        console.log(valorLocto);
    }
    else if (valorLoc != null && valorLoc!= '') {
        console.log(valorLoc);
        cargarDatosLocalizacion(valorLoc);
    }
    else if (valorLocto != null && valorLocto != '') {
        console.log(valorLocto);
        cargarDatosLocalizacion(valorLocto);
    }

}
//CARGAR LOCATION GENERAL EN TABLA LOCATION GENERAL
function cargarDatosLocalizacion(idcod) {
    $.ajax({
        url: '/Tnst04CompEmitido/RecargarLocalizacion?id=' + idcod,
        type: 'GET',
        //data: formData,
        //processData: false,  // No procesar los datos (dejar que FormData lo haga)
        //contentType: false,  // No establecer contentType (FormData lo establece correctamente)
        success: function (response) {
            /*console.log("Contenido:response");*/
            // Limpiar el contenido existente de ambas tablas
            $('#bodysameloc1').empty();
            $('#bodysameloc2').empty();

            // Iterar a través de los datos para la primera tabla (localización)
            $.each(response, function (index, localizacion) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + localizacion.idLocation + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacion.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacion.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacion.fechaN + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacion.tipol + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarLocation(\'modalloc\',\'loccbo\',\'Locationid\',\'LocalizacionNombre\',\'' +
                    localizacion.idLocation + '\', \'' + localizacion.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodysameloc1').append(row);
            });

            // Iterar a través de los datos para la segunda tabla (localizaciónto)
            $.each(response, function (index, localizacionto) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + localizacionto.idLocation + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacionto.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacionto.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacionto.fechaN + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacionto.tipol + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarLocationto(\'modallto\',\'ltocbo\',\'Locationtoid\',\'LocalizaciontoNombre\',\'' +
                    localizacionto.idLocation + '\', \'' + localizacionto.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodysameloc2').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del Localizacion:', error.responseText);
        }
    });
}
    //CBO DEPENDIENTE LOCATION
    $(document).ready(function () {
        // Manejador de cambio para el departamento
        $('#departamentoloc').change(function () {
            var departamentoId = $(this).val();

            // Realiza una solicitud AJAX para obtener las provincias
            $.ajax({
                url: '/Tnst04CompEmitido/ObtenerProvincias',
                type: 'GET',
                data: { departamentoId: departamentoId },
                success: function (data) {
                    // Actualiza el contenido del select de provincia
                    $('#provincialoc').empty();
                    $.each(data, function (index, item) {
                        $('#provincialoc').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
                    });

                    // Llama al evento change para actualizar los distritos
                    $('#provincialoc').change();
                },
                error: function () {
                    console.error('Error al obtener provincias.');
                }
            });
        });

        // Manejador de cambio para la provincia
        $('#provincialoc').change(function () {
            var provinciaId = $(this).val();

            // Realiza una solicitud AJAX para obtener los distritos
            $.ajax({
                url: '/Tnst04CompEmitido/ObtenerDistritos',
                type: 'GET',
                data: { provinciaId: provinciaId },
                success: function (data) {
                    // Actualiza el contenido del select de distrito
                    $('#distritoloc').empty();
                    $.each(data, function (index, item) {
                        $('#distritoloc').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
                    });
                },
                error: function () {
                    console.error('Error al obtener distritos.');
                }
            });
        });
    });



    //Cargar la selección de LOCATION
    function seleccionarLocation(modalId, selectId, idcod, idtext, idLoc, nombreLoc) {
        event.preventDefault();
        var LocalizadorIdInput = document.getElementById(idcod);
        LocalizadorIdInput.value = idLoc;
        document.getElementById(idtext).innerText = nombreLoc;
        closeModal(modalId, selectId);
        // Mostrar el span con el nombre del cliente y el botón para remover
        var LocalizadorNombreSpan = document.getElementById('LocalizacionNombre');
        var removeLocalizadorSpan = document.getElementById('removeLocalizacionSpan');
        LocalizadorNombreSpan.innerText = nombreLoc;
        LocalizadorNombreSpan.style.display = 'inline-block';
        removeLocalizadorSpan.style.display = 'inline-block';
        // Ocultar el select
        var selectElement = document.getElementById('loccbo');
        selectElement.style.display = 'none';
        EleccionCarga();

    }

    //El span LOCATION para volver al select
function removeLocation() {
        // Quitar el valor del cliente y ocultar el span
        document.getElementById('LocalizacionNombre').innerText = '';
        document.getElementById('Locationid').value = '';
        document.getElementById('LocalizacionNombre').style.display = 'none';
        document.getElementById('removeLocalizacionSpan').style.display = 'none';
        var selectElement = document.getElementById('loccbo');
        selectElement.style.display = 'inline-block';
        EleccionCarga();

    }

    //filtro de LOCATION por Nombre y Ruc
function filtrarLocation() {
        var filtroNombre = document.getElementById('filtroNombreLoc').value.toLowerCase();
        var filtroRUC = document.getElementById('filtroRUCLoc').value.toLowerCase();

        // Recorremos todas las filas de la tabla
    $('.tablalocalizacion tbody tr').each(function () {
            var nombreLocalizacion = $(this).find('.client-cell:nth-child(2)').text().toLowerCase();
            var rucLocalizacion = $(this).find('.client-cell:nth-child(5)').text().toLowerCase();

            // Comprobamos si coincide con el filtro de nombre y/o RUC desde el inicio de la palabra
            var nombreCoincide = nombreLocalizacion.startsWith(filtroNombre);
            var rucCoincide = rucLocalizacion.startsWith(filtroRUC);

            // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
            if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    }

//LOCATIONTO
//Proceso para cerrar la ventana modal LOCATIONTO
$(document).ready(function () {
    $('#modallto').on('hidden.bs.modal', function () {
        $('#ltocbo').prop('selectedIndex', 0);
        EleccionCarga();
        // Vaciar los campos de filtro
        $('#filtroNombreLto').val('');
        $('#filtroRUCLto').val('');
    });
});


//Proceso para cerrar la ventana modal create LocationTO
$(document).ready(function () {
    $('#createLocalizaciontoModal').on('hidden.bs.modal', function () {
        // Vaciar los campos de Crear
        EleccionCarga();
        $('#prim_nombrelto, #seg_nombrelto, #patapellidolto, #matapellidolto, #razonSociallto, #nroRuclto, #cellto').val('');
        $('#provincialto, #distritolto').empty().prop('selectedIndex', 0);
    });
});

//INSERCION LOCATIONTO
$(document).ready(function () {
    $('#crearLocalizaciontoBtn').click(function () {//autoclick
        // Validar que los campos requeridos estén llenos
        if (camposRequeridosLlenos('createLocalizaciontoForm')) {
            // Crear un objeto FormData
            var formData = new FormData();

            /*Obtener los valores del formulario y agregarlos a FormData*/
            formData.append('nombreloc', $('#nombrelto').val());
            formData.append('fechanegloc', $('#fechaneglto').val());
            formData.append('IdTipoLocation', $('#IdTipoLocationlto').val());
            formData.append('id_distrito', $('#distritolto').val());
            formData.append('direccionloc', $('#direccionlto').val());
            // Realizar la solicitud AJAX al controlador
            $.ajax({
                url: '/Tnst04CompEmitido/CrearLocalizacion',
                type: 'POST',
                data: formData,
                processData: false,  // No procesar los datos (dejar que FormData lo haga)
                contentType: false,  // No establecer contentType (FormData lo establece correctamente)
                success: function (response) {
                    console.log('Localizaciónto creado correctamente:', response);
                    // Clear the existing tbody content
                    
                    $('#createLocalizaciontoModal').modal('hide');
                },
                error: function (error) {
                    console.error('Error al crear la Localizaciónto:', error);
                    // Aquí puedes manejar errores si es necesario
                }
            });
        } else {
            alert('Por favor, complete todos los campos requeridos.');
        }
    });
});

//Cargar la selección de LOCATIONTO
function seleccionarLocationto(modalId, selectId, idcod, idtext, idLocto, nombreLocto) {
    event.preventDefault();
    var LocalizadortoIdInput = document.getElementById(idcod);
    LocalizadortoIdInput.value = idLocto;
    document.getElementById(idtext).innerText = nombreLocto;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var LocalizadortoNombreSpan = document.getElementById('LocalizaciontoNombre');
    var removeLocalizadortoSpan = document.getElementById('removeLocalizaciontoSpan');
    LocalizadortoNombreSpan.innerText = nombreLocto;
    LocalizadortoNombreSpan.style.display = 'inline-block';
    removeLocalizadortoSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('ltocbo');
    selectElement.style.display = 'none';
    EleccionCarga();
}

//El span LOCATIONTO para volver al select
function removeLocationto() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('LocalizaciontoNombre').innerText = '';
    document.getElementById('Locationtoid').value = '';
    document.getElementById('LocalizaciontoNombre').style.display = 'none';
    document.getElementById('removeLocalizaciontoSpan').style.display = 'none';
    var selectElement = document.getElementById('ltocbo');
    selectElement.style.display = 'inline-block';
    EleccionCarga();
}

//filtro de LOCATIONTO por Nombre y Ruc
function filtrarLocationto() {
    var filtroNombre = document.getElementById('filtroNombreLocto').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCLocto').value.toLowerCase();

    // Recorremos todas las filas de la tabla
    $('.tablalocalizacionto tbody tr').each(function () {
        var nombreLocalizacionto = $(this).find('.client-cell:nth-child(2)').text().toLowerCase();
        var rucLocalizacionto = $(this).find('.client-cell:nth-child(5)').text().toLowerCase();

        // Comprobamos si coincide con el filtro de nombre y/o RUC desde el inicio de la palabra
        var nombreCoincide = nombreLocalizacionto.startsWith(filtroNombre);
        var rucCoincide = rucLocalizacionto.startsWith(filtroRUC);

        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

//PRODUCTO
//Proceso para cerrar la ventana modal PRODUCTO
$(document).ready(function () {
    $('#modalpro').on('hidden.bs.modal', function () {
        $('#procbo').prop('selectedIndex', 0);
        cargarDatosProducto();
        // Vaciar los campos de filtro
        $('#filtroNombrePro').val('');
    });
});

//Cargar la selección de PRODUCTO
function seleccionarProducto(modalId, selectId, idcod, idtext, idPro, nombrePro) {
    event.preventDefault();
    var ProductoIdInput = document.getElementById(idcod);
    ProductoIdInput.value = idPro;
    document.getElementById(idtext).innerText = nombrePro;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var ProductoNombreSpan = document.getElementById('ProductoNombre');
    var removeProductoSpan = document.getElementById('removeProductoSpan');
    ProductoNombreSpan.innerText = nombrePro;
    ProductoNombreSpan.style.display = 'inline-block';
    removeProductoSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('procbo');
    selectElement.style.display = 'none';
}

//El span PRODUCTO para volver al select
function removeProducto() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('ProductoId').value = '';
    document.getElementById('ProductoNombre').style.display = 'none';
    document.getElementById('removeProductoSpan').style.display = 'none';
    var selectElement = document.getElementById('procbo');
    selectElement.style.display = 'inline-block';
    cargarDatosProducto();
}
function cargarDatosProducto() {
    // Obtener los IDs de los productos seleccionados
    var productosSeleccionadosIDs = productosSeleccionados.map(function (producto) {
        return producto.idProducto;
    });

    $.ajax({
        url: '/Tnst04CompEmitido/RecargarProducto',
        type: 'GET',
        data: { productosSeleccionadosIds: productosSeleccionadosIDs }, // Pasar los IDs como parámetro
        traditional: true, // Esto es importante para que los parámetros se pasen como una lista

        success: function (response) {
            $('#bodyProducto').empty();

            // Iterar a través de los datos del servidor
            $.each(response, function (index, producto) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + producto.idProducto + '</td>');
                row.append('<td align="center" class="client-cell">' + producto.nombreProducto + '</td>');
                row.append('<td align="center" class="client-cell">' + producto.monto_si + '</td>');
                row.append('<td align="center" class="client-cell">' + producto.monto_ci + '</td');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarProducto(\'modalpro\',\'procbo\',\'ProductoId\',\'ProductoNombre\',\'' +
                    producto.idProducto + '\', \'' + producto.nombreProducto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodyProducto').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del Producto:', error.responseText);
        }
    });
}

//filtro de PRODUCTO por Nombre
function filtrarProducto() {
    var filtroNombre = $('#filtroNombrePro').val().toLowerCase();

    // Recorremos todas las filas de la tabla
    $('.tablaProducto tbody tr').each(function () {
        var nombreProducto = $(this).find('.client-cell:nth-child(2)').text().toLowerCase();

        // Comprobamos si coincide con el filtro de nombre desde el inicio de la palabra
        var nombreCoincide = nombreProducto.startsWith(filtroNombre);

        // Mostrar la fila si coincide con el filtro o está vacío
        if (filtroNombre === '' || nombreCoincide) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

// Declarar el arreglo para almacenar los productos seleccionados
var productosSeleccionados = [];

// Función para añadir un producto a la lista de productos seleccionados
function agregarProducto() {
    var idProducto = parseInt($("#ProductoId").val());
    var cantidad = parseFloat($("#cantidad").val());
    var descuento = parseFloat($("#descuento").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idProducto) || idProducto <= 0 || isNaN(cantidad) || isNaN(descuento)) {
        alert("Por favor, ingrese valores numéricos válidos para el ID, cantidad y descuento.");
        return false; // Detener la ejecución si los campos no son válidos
    }

    $.ajax({
        url: "/Tnst04CompEmitido/ProductoDatos",
        type: "GET",
        data: { idProducto: idProducto },
        success: function (response) {
            if (response != null) {
                var igv = response.igv / 100;
                var mci = response.monto_ci;
                var msi = response.monto_si;

                var neto = cantidad * msi;
                var descuentoPorcentaje = descuento / 100;
                var mtodescuento = descuentoPorcentaje * msi * cantidad;
                var subtotal = cantidad * msi * (1 - descuentoPorcentaje);
                var mtoigv = igv * msi * cantidad;
                var total = subtotal + mtoigv;

                productosSeleccionados.push({
                    idProducto: response.idProducto,
                    nombreProducto: response.nombreProducto,
                    cantidad: cantidad,
                    neto: neto,
                    descuento: descuentoPorcentaje,
                    mtodescuento: mtodescuento,
                    subtotal: subtotal,
                    igv: igv,
                    mtoigv: mtoigv,
                    monto_si: msi,
                    monto_ci: mci,
                    total: total,
                    observacion: ""
                });

                actualizarTablaProductosSeleccionados();
                removeProducto();

                document.getElementById('cantidad').value = 1;
                var descuentoInput = document.getElementById('descuento');

                // Establece el valor en 0
                descuentoInput.value = 0;

            } else {
                alert("Producto no encontrado");
            }
        },
        error: function (response) {
            alert("Error al obtener los datos del producto");
        }
    });

    return false;
}




// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarProducto(idProducto) {
    var producto = productosSeleccionados.find(function (prod) {
        return prod.idProducto === idProducto;
    });

    if (producto) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este producto de la lista?");

        if (confirmar) {
            var productoIndex = productosSeleccionados.findIndex(function (prod) {
                return prod.idProducto === idProducto;
            });

            if (productoIndex !== -1) {
                productosSeleccionados.splice(productoIndex, 1);
                actualizarTablaProductosSeleccionados();
            }
        }
    } else {
        alert("Producto no encontrado en la lista.");
    }
}

// Función para actualizar la tabla de productos seleccionados
function actualizarTablaProductosSeleccionados() {
    // Limpiar la tabla
    $("#productosSeleccionados").empty();

    // Recorrer el arreglo de productos seleccionados y volver a agregarlos a la tabla
    productosSeleccionados.forEach(function (producto) {
        // Redondear el valor del campo "total" y otros campos a dos decimales solo en la vista
        var mci = parseFloat(producto.monto_ci).toFixed(2);
        var msi = parseFloat(producto.monto_si).toFixed(2);
        var neto = parseFloat(producto.neto).toFixed(2);
        var mtodescuento = parseFloat(producto.mtodescuento).toFixed(2);
        var subtotal = parseFloat(producto.subtotal).toFixed(2);
        var mtoigv = parseFloat(producto.mtoigv).toFixed(2);
        var total = parseFloat(producto.total).toFixed(2);

        var newRow = `
            <tr id='${producto.idProducto}'>
                <td>${producto.nombreProducto}</td>
                <td>${producto.cantidad}</td>
                <td>${msi}</td>
                <td>${mtodescuento}</td>
                <td>${mtoigv}</td>
                <td>${mci}</td>
                <td>${total}</td>
                <td colspan='2'>
                    <button type='button' onclick='editarProducto(${producto.idProducto})'>Editar</button>
                    <button type='button' onclick='eliminarProducto(${producto.idProducto})'>Eliminar</button>
                </td>
            </tr>`;

        $("#productosSeleccionados").append(newRow);
    });
    cargarDatosProducto();
    actualizarTotales();
}

//Función para abrir el modal de edición
function editarProducto(idProducto) {
    var producto = productosSeleccionados.find(function (prod) {
        return prod.idProducto === idProducto;
    });
    if (producto) {
        document.getElementById("nuevaCantidad").value = producto.cantidad;
        document.getElementById("IdEditarProducto").value = idProducto;
        document.getElementById("Productoinput").value = producto.nombreProducto;
        document.getElementById("nuevoDescuento").value = producto.descuento*100;
        document.getElementById("observacionP").value = producto.observacion;
        openSecondModal('editarModal');
    } else {
        alert("Producto no encontrado en la lista.");
    }
}

// Función para aplicar los cambios desde el modal y guardarlos
function aplicarCambios() {
    var idProducto = parseInt(document.getElementById("IdEditarProducto").value);
    var producto = productosSeleccionados.find(function (prod) {
        return prod.idProducto === idProducto;
    });
    var nuevaCantidad = parseFloat(document.getElementById("nuevaCantidad").value);
    var nuevoDescuento = parseFloat(document.getElementById("nuevoDescuento").value);
    var nuevaObservacion = document.getElementById("observacionP").value;

    if (!isNaN(nuevaCantidad) && !isNaN(nuevoDescuento)) {
        var confirmar = confirm("¿Deseas aplicar los cambios y guardarlos?");

        if (confirmar) {
            // Aplicar parseFloat con toFixed(2) a las variables
            var neto = parseFloat((nuevaCantidad * producto.monto_si));
            var descuento = parseFloat((nuevoDescuento / 100));
            var mtodescuento = parseFloat((producto.monto_si * nuevaCantidad * nuevoDescuento));
            var subtotal = parseFloat((nuevaCantidad * producto.monto_si * (1 - nuevoDescuento / 100)));
            var mtoigv = parseFloat((producto.monto_si * nuevaCantidad * producto.igv));
            var total = parseFloat((neto + mtoigv - mtodescuento));
            var NuevaObs = nuevaObservacion;

            // Asignar los valores a las propiedades del objeto producto
            producto.cantidad = nuevaCantidad;
            producto.neto = neto;
            producto.subtotal = subtotal;
            producto.mtoigv = mtoigv;
            producto.descuento = descuento;
            producto.mtodescuento = mtodescuento;
            producto.total = total;
            producto.observacion = NuevaObs;

            actualizarTablaProductosSeleccionados();
            cerrarModal('editarModal');
        } else {
            alert("Edición cancelada. Los cambios no se aplicaron.");
        }
    } else {
        alert("Por favor, ingresa valores numéricos válidos para cantidad y descuento.");
    }
}

// Supongamos que tienes un arreglo productosSeleccionados con la estructura:
function actualizarTotales() {
    let Mtoneto = 0;
    let Mtodescuento = 0;
    let Mtoigv = 0;
    let MtoSubtotal = 0;
    let Mtototal = 0;


    for (let i = 0; i < productosSeleccionados.length; i++) {
        const Neto = parseFloat(productosSeleccionados[i].neto);
        const descuentoVal = parseFloat(productosSeleccionados[i].mtodescuento);
        const subtotal = parseFloat(productosSeleccionados[i].subtotal);
        const montoIgv = parseFloat(productosSeleccionados[i].mtoigv);
        const total = parseFloat(productosSeleccionados[i].total);

        // Sumar los valores a los totales
        Mtoneto += Neto;
        Mtodescuento += descuentoVal;
        MtoSubtotal += subtotal;
        Mtoigv += montoIgv;
        Mtototal += total;

    }

    // Actualizar los campos en el documento HTML
    // Actualizar los campos en el documento HTML con dos decimales
    document.getElementById("neto").value = Mtoneto.toFixed(2);
    document.getElementById("desc").value = Mtodescuento.toFixed(2);
    document.getElementById("Mtoapdesc").value = MtoSubtotal.toFixed(2);
    document.getElementById("igv").value = Mtoigv.toFixed(2);
    document.getElementById("total").value = Mtototal.toFixed(2);

}

//BOTONES GUARDAR - GUARDAR Y CERRAR


function obtenerDatosDelComprobante() {
    var checkbox = document.getElementById('checkEstado');

    if (checkbox.checked) {
        var idest = 1;
        var nombreest = "ACTIVO";
    } else {
        var idest = 2;
        var nombreest = "FINALIZADO";
    }

    var data = {
        IdCompEmitido: parseInt(document.getElementById("comprobanteIdInput").value),
        NroCompEmitido: document.getElementById("NroCompEmitido").value,
        NroCheque: null,
        IdTipoComp: parseInt(document.getElementById("idtipoComp").value),
        IdCliente: parseInt(document.getElementById("clienteID").value),
        IdEmpChofer: parseInt(document.getElementById("choferId").value),
        IdEmpleado: parseInt(document.getElementById("OperarioId").value),
        IdEmpAutorizador: parseInt(document.getElementById("AutorizadorId").value),
        CodCaja: null,
        TxtSerie: document.getElementById("TxtSerie").value,
        TxtNumero: document.getElementById("TxtNumero").value,
        TxtSerieFe: null,
        TxtNumeroFe: null,
        FecNegocio: (document.getElementById("FecNegocio").value),
        FecRegEmitido: (document.getElementById("FecRegEmitido").value),
        FecRegistro: (document.getElementById("FecRegistro").value),
        FecEmi: (document.getElementById("fechaEmision").value),
        FecVcto: (document.getElementById("fechaVcto").value),
        FecCanc: (document.getElementById("fechaCancelacion").value),
        IdTipoMoneda: null,
        IdCanVta: parseInt(document.getElementById("CantVta").value),
        IdTipoOrden: parseInt(document.getElementById("TipoOrden").value),
        IdLocation: parseInt(document.getElementById('Locationid').value),
        IdLocationto: parseInt(document.getElementById('Locationtoid').value),
        TxtObserv: document.getElementById("txtobservacion").value,
        MtoTcVta: parseFloat(document.getElementById("mto_tc_vta").value),
        MtoNeto: parseFloat(document.getElementById("neto").value),
        MtoExonerado: 0.0 ,// Debe coincidir con el tipo de la propiedad
        MtoNoAfecto: 0.0, // Debe coincidir con el tipo de la propiedad
        MtoDsctoTot: parseFloat(document.getElementById("desc").value),
        MtoServicio: 0.0, // Debe coincidir con el tipo de la propiedad
        MtoSubTot: parseFloat(document.getElementById("Mtoapdesc").value),
        MtoImptoTot: parseFloat(document.getElementById("igv").value),
        MtoTotComp: parseFloat(document.getElementById("total").value),
        RefIdCompEmitido: null, // Debe coincidir con el tipo de la propiedad
        RefTipoComprobante: null, // Debe coincidir con el tipo de la propiedad
        RefFecha: null, // Debes asegurarte de que el valor sea una fecha en formato correcto
        RefSerie: null, // Debe coincidir con el tipo de la propiedad
        RefNumero: null, // Debe coincidir con el tipo de la propiedad
        SnChkAbierto: false,
        SnChkEnviado: false,
        TaxPor01: 0.18,
        TaxPor02: null, // Debe coincidir con el tipo de la propiedad
        TaxPor03: null, // Debe coincidir con el tipo de la propiedad
        TaxPor04: null, // Debe coincidir con el tipo de la propiedad
        TaxPor05: null, // Debe coincidir con el tipo de la propiedad
        TaxPor06: null, // Debe coincidir con el tipo de la propiedad
        TaxPor07: null, // Debe coincidir con el tipo de la propiedad
        TaxPor08: null, // Debe coincidir con el tipo de la propiedad
        TaxMto01: parseFloat(document.getElementById("igv").value),
        TaxMto02: null, // Debe coincidir con el tipo de la propiedad
        TaxMto03: null, // Debe coincidir con el tipo de la propiedad
        TaxMto04: null, // Debe coincidir con el tipo de la propiedad
        TaxMto05: null, // Debe coincidir con el tipo de la propiedad
        TaxMto06: null, // Debe coincidir con el tipo de la propiedad
        TaxMto07: null, // Debe coincidir con el tipo de la propiedad
        TaxMto08: null, // Debe coincidir con el tipo de la propiedad
        Info01: null, // Debe coincidir con el tipo de la propiedad
        Info02: null, // Debe coincidir con el tipo de la propiedad
        Info03: null, // Debe coincidir con el tipo de la propiedad
        Info04: null, // Debe coincidir con el tipo de la propiedad
        Info05: null, // Debe coincidir con el tipo de la propiedad
        Info06: null, // Debe coincidir con el tipo de la propiedad
        Info07: null, // Debe coincidir con el tipo de la propiedad
        Info08: null, // Debe coincidir con el tipo de la propiedad
        Info09: null, // Debe coincidir con el tipo de la propiedad
        Info10: null, // Debe coincidir con el tipo de la propiedad
        InfoDate01: null, // Debe coincidir con el tipo de la propiedad y asegurarte de que el valor sea una fecha en formato correcto
        InfoDate02: null, // Debe coincidir con el tipo de la propiedad y asegurarte de que el valor sea una fecha en formato correcto
        InfoDate03: null, // Debe coincidir con el tipo de la propiedad y asegurarte de que el valor sea una fecha en formato correcto
        InfoDate04: null, // Debe coincidir con el tipo de la propiedad y asegurarte de que el valor sea una fecha en formato correcto
        InfoDate05: null, // Debe coincidir con el tipo de la propiedad y asegurarte de que el valor sea una fecha en formato correcto
        InfoMto01: null, // Debe coincidir con el tipo de la propiedad
        InfoMto02: null, // Debe coincidir con el tipo de la propiedad
        InfoMto03: null, // Debe coincidir con el tipo de la propiedad
        InfoMto04: null, // Debe coincidir con el tipo de la propiedad
        InfoMto05: null, // Debe coincidir con el tipo de la propiedad
        Post: null, // Debe coincidir con el tipo de la propiedad
        PostDate: null, // Debes asegurarte de que el valor sea una fecha en formato correcto
        NumComensales: null, // Debe coincidir con el tipo de la propiedad
        IdUsuario: 3, // Supongo que esto es el ID de un usuario
        TxtUsuario: "admineagle", // Supongo que esto es el nombre de un usuario
        IdUsuarioModificador: null, // Debe coincidir con el tipo de la propiedad
        TxtUsuarioModificador: null, // Debe coincidir con el tipo de la propiedad
        FechaModificacion: null, // Debes asegurarte de que el valor sea una fecha en formato correcto
        IdEstado: idest, // Supongo que esto es el ID de un estado
        TxtEstado: nombreest,// Supongo que esto es el nombre de un estado
        IdMesa: null, // Debe coincidir con el tipo de la propiedad
        IdTurno: null, // Debe coincidir con el tipo de la propiedad
    };

    return data;
}

$(document).ready(function () {
    // Evento click para el botón "Guardar"
    $("#guardar").click(function () {
        var dataToSend = obtenerDatosDelComprobante();
        enviarDatosGuardar(dataToSend);
    });

    // Evento click para el botón "Guardar y Cerrar"
    $("#guardarycerrar").click(function () {
        var dataToSend = obtenerDatosDelComprobante();
        enviarDatosGuardarYCerrar(dataToSend);
    });

    $("#cancelar").click(function () {
        window.location.href = '/Tnst04CompEmitido/boleta_v_listado'; // Reemplaza 'otra_ruta' con la URL deseada
    });
});

function enviarDatosGuardar(dataToSend) {
    
    var productosJson = JSON.stringify(productosSeleccionados);
    console.log(productosJson);
    var url = '/Tnst04CompEmitido/Guardar?';
    url += 'comprobanteId=' + dataToSend.IdCompEmitido;
    url += '&NroCompEmitido=' + encodeURIComponent(dataToSend.NroCompEmitido);
    url += '&NroCheque=' + encodeURIComponent(dataToSend.NroCheque);
    url += '&IdTipoComp=' + dataToSend.IdTipoComp;
    url += '&IdCliente=' + dataToSend.IdCliente;
    url += '&IdEmpChofer=' + dataToSend.IdEmpChofer;
    url += '&IdEmpleado=' + dataToSend.IdEmpleado;
    url += '&IdEmpAutorizador=' + dataToSend.IdEmpAutorizador;
    url += '&CodCaja=' + encodeURIComponent(dataToSend.CodCaja);
    url += '&TxtSerie=' + encodeURIComponent(dataToSend.TxtSerie);
    url += '&TxtNumero=' + encodeURIComponent(dataToSend.TxtNumero);
    url += '&TxtSerieFe=' + encodeURIComponent(dataToSend.TxtSerieFe);
    url += '&TxtNumeroFe=' + encodeURIComponent(dataToSend.TxtNumeroFe);
    url += '&FecNegocio=' + encodeURIComponent(dataToSend.FecNegocio);
    url += '&FecRegEmitido=' + encodeURIComponent(dataToSend.FecRegEmitido);
    url += '&FecRegistro=' + encodeURIComponent(dataToSend.FecRegistro);
    url += '&FecEmi=' + encodeURIComponent(dataToSend.FecEmi);
    url += '&FecVcto=' + encodeURIComponent(dataToSend.FecVcto);
    url += '&FecCanc=' + encodeURIComponent(dataToSend.FecCanc);
    url += '&IdTipoMoneda=' + dataToSend.IdTipoMoneda;
    url += '&IdCanVta=' + dataToSend.IdCanVta;
    url += '&IdTipoOrden=' + dataToSend.IdTipoOrden;
    url += '&IdLocation=' + dataToSend.IdLocation;
    url += '&IdLocationto=' + dataToSend.IdLocationto;
    url += '&TxtObserv=' + encodeURIComponent(dataToSend.TxtObserv);
    url += '&MtoTcVta=' + dataToSend.MtoTcVta;
    url += '&MtoNeto=' + dataToSend.MtoNeto;
    url += '&MtoExonerado=' + dataToSend.MtoExonerado;
    url += '&MtoNoAfecto=' + dataToSend.MtoNoAfecto;
    url += '&MtoDsctoTot=' + dataToSend.MtoDsctoTot;
    url += '&MtoServicio=' + dataToSend.MtoServicio;
    url += '&MtoSubTot=' + dataToSend.MtoSubTot;
    url += '&MtoImptoTot=' + dataToSend.MtoImptoTot;
    url += '&MtoTotComp=' + dataToSend.MtoTotComp;
    url += '&RefIdCompEmitido=' + dataToSend.RefIdCompEmitido;
    url += '&RefTipoComprobante=' + encodeURIComponent(dataToSend.RefTipoComprobante);
    url += '&RefFecha=' + encodeURIComponent(dataToSend.RefFecha);
    url += '&RefSerie=' + encodeURIComponent(dataToSend.RefSerie);
    url += '&RefNumero=' + encodeURIComponent(dataToSend.RefNumero);
    url += '&SnChkAbierto=' + dataToSend.SnChkAbierto;
    url += '&SnChkEnviado=' + dataToSend.SnChkEnviado;
    url += '&TaxPor01=' + dataToSend.TaxPor01;
    url += '&TaxPor02=' + dataToSend.TaxPor02;
    url += '&TaxPor03=' + dataToSend.TaxPor03;
    url += '&TaxPor04=' + dataToSend.TaxPor04;
    url += '&TaxPor05=' + dataToSend.TaxPor05;
    url += '&TaxPor06=' + dataToSend.TaxPor06;
    url += '&TaxPor07=' + dataToSend.TaxPor07;
    url += '&TaxPor08=' + dataToSend.TaxPor08;
    url += '&TaxMto01=' + dataToSend.TaxMto01;
    url += '&TaxMto02=' + dataToSend.TaxMto02;
    url += '&TaxMto03=' + dataToSend.TaxMto03;
    url += '&TaxMto04=' + dataToSend.TaxMto04;
    url += '&TaxMto05=' + dataToSend.TaxMto05;
    url += '&TaxMto06=' + dataToSend.TaxMto06;
    url += '&TaxMto07=' + dataToSend.TaxMto07;
    url += '&TaxMto08=' + dataToSend.TaxMto08;
    url += '&Info01=' + encodeURIComponent(dataToSend.Info01);
    url += '&Info02=' + encodeURIComponent(dataToSend.Info02);
    url += '&Info03=' + encodeURIComponent(dataToSend.Info03);
    url += '&Info04=' + encodeURIComponent(dataToSend.Info04);
    url += '&Info05=' + encodeURIComponent(dataToSend.Info05);
    url += '&Info06=' + encodeURIComponent(dataToSend.Info06);
    url += '&Info07=' + encodeURIComponent(dataToSend.Info07);
    url += '&Info08=' + encodeURIComponent(dataToSend.Info08);
    url += '&Info09=' + encodeURIComponent(dataToSend.Info09);
    url += '&Info10=' + encodeURIComponent(dataToSend.Info10);
    url += '&InfoDate01=' + encodeURIComponent(dataToSend.InfoDate01);
    url += '&InfoDate02=' + encodeURIComponent(dataToSend.InfoDate02);
    url += '&InfoDate03=' + encodeURIComponent(dataToSend.InfoDate03);
    url += '&InfoDate04=' + encodeURIComponent(dataToSend.InfoDate04);
    url += '&InfoDate05=' + encodeURIComponent(dataToSend.InfoDate05);
    url += '&InfoMto01=' + dataToSend.InfoMto01;
    url += '&InfoMto02=' + dataToSend.InfoMto02;
    url += '&InfoMto03=' + dataToSend.InfoMto03;
    url += '&InfoMto04=' + dataToSend.InfoMto04;
    url += '&InfoMto05=' + dataToSend.InfoMto05;
    url += '&Post=' + encodeURIComponent(dataToSend.Post);
    url += '&PostDate=' + encodeURIComponent(dataToSend.PostDate);
    url += '&NumComensales=' + dataToSend.NumComensales;
    url += '&IdUsuario=' + dataToSend.IdUsuario;
    url += '&TxtUsuario=' + encodeURIComponent(dataToSend.TxtUsuario);
    url += '&IdUsuarioModificador=' + dataToSend.IdUsuarioModificador;
    url += '&TxtUsuarioModificador=' + encodeURIComponent(dataToSend.TxtUsuarioModificador);
    url += '&FechaModificacion=' + encodeURIComponent(dataToSend.FechaModificacion);
    url += '&IdEstado=' + dataToSend.IdEstado;
    url += '&TxtEstado=' + encodeURIComponent(dataToSend.TxtEstado);
    url += '&IdMesa=' + dataToSend.IdMesa;
    url += '&IdTurno=' + dataToSend.IdTurno;
    url += '&productosSeleccionado=' + encodeURIComponent(productosJson);


    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        success: function (response) {
            if (response.mensaje) {
                alert(response.mensaje);
            } else {
                alert('Error: ' + response.errores);
            }
                // Asigna el valor de comprobanteId a tu campo en la vista
                $('#comprobanteIdInput').val(response.comprobanteId);

        }
    });
}

function enviarDatosGuardarYCerrar(dataToSend) {
    var productosJson = JSON.stringify(productosSeleccionados);

    // Verifica si la cantidad de elementos en el arreglo es mayor que 0
    if (productosSeleccionados.length > 0) {
        var confirmar = confirm("¿Seguro que desea guardar y cerrar?");

        if (confirmar) {
            console.log(productosJson);
            var url = '/Tnst04CompEmitido/Guardar?';
            url += 'comprobanteId=' + dataToSend.IdCompEmitido;
            url += '&NroCompEmitido=' + encodeURIComponent(dataToSend.NroCompEmitido);
            url += '&NroCheque=' + encodeURIComponent(dataToSend.NroCheque);
            url += '&IdTipoComp=' + dataToSend.IdTipoComp;
            url += '&IdCliente=' + dataToSend.IdCliente;
            url += '&IdEmpChofer=' + dataToSend.IdEmpChofer;
            url += '&IdEmpleado=' + dataToSend.IdEmpleado;
            url += '&IdEmpAutorizador=' + dataToSend.IdEmpAutorizador;
            url += '&CodCaja=' + encodeURIComponent(dataToSend.CodCaja);
            url += '&TxtSerie=' + encodeURIComponent(dataToSend.TxtSerie);
            url += '&TxtNumero=' + encodeURIComponent(dataToSend.TxtNumero);
            url += '&TxtSerieFe=' + encodeURIComponent(dataToSend.TxtSerieFe);
            url += '&TxtNumeroFe=' + encodeURIComponent(dataToSend.TxtNumeroFe);
            url += '&FecNegocio=' + encodeURIComponent(dataToSend.FecNegocio);
            url += '&FecRegEmitido=' + encodeURIComponent(dataToSend.FecRegEmitido);
            url += '&FecRegistro=' + encodeURIComponent(dataToSend.FecRegistro);
            url += '&FecEmi=' + encodeURIComponent(dataToSend.FecEmi);
            url += '&FecVcto=' + encodeURIComponent(dataToSend.FecVcto);
            url += '&FecCanc=' + encodeURIComponent(dataToSend.FecCanc);
            url += '&IdTipoMoneda=' + dataToSend.IdTipoMoneda;
            url += '&IdCanVta=' + dataToSend.IdCanVta;
            url += '&IdTipoOrden=' + dataToSend.IdTipoOrden;
            url += '&IdLocation=' + dataToSend.IdLocation;
            url += '&IdLocationto=' + dataToSend.IdLocationto;
            url += '&TxtObserv=' + encodeURIComponent(dataToSend.TxtObserv);
            url += '&MtoTcVta=' + dataToSend.MtoTcVta;
            url += '&MtoNeto=' + dataToSend.MtoNeto;
            url += '&MtoExonerado=' + dataToSend.MtoExonerado;
            url += '&MtoNoAfecto=' + dataToSend.MtoNoAfecto;
            url += '&MtoDsctoTot=' + dataToSend.MtoDsctoTot;
            url += '&MtoServicio=' + dataToSend.MtoServicio;
            url += '&MtoSubTot=' + dataToSend.MtoSubTot;
            url += '&MtoImptoTot=' + dataToSend.MtoImptoTot;
            url += '&MtoTotComp=' + dataToSend.MtoTotComp;
            url += '&RefIdCompEmitido=' + dataToSend.RefIdCompEmitido;
            url += '&RefTipoComprobante=' + encodeURIComponent(dataToSend.RefTipoComprobante);
            url += '&RefFecha=' + encodeURIComponent(dataToSend.RefFecha);
            url += '&RefSerie=' + encodeURIComponent(dataToSend.RefSerie);
            url += '&RefNumero=' + encodeURIComponent(dataToSend.RefNumero);
            url += '&SnChkAbierto=' + dataToSend.SnChkAbierto;
            url += '&SnChkEnviado=' + dataToSend.SnChkEnviado;
            url += '&TaxPor01=' + dataToSend.TaxPor01;
            url += '&TaxPor02=' + dataToSend.TaxPor02;
            url += '&TaxPor03=' + dataToSend.TaxPor03;
            url += '&TaxPor04=' + dataToSend.TaxPor04;
            url += '&TaxPor05=' + dataToSend.TaxPor05;
            url += '&TaxPor06=' + dataToSend.TaxPor06;
            url += '&TaxPor07=' + dataToSend.TaxPor07;
            url += '&TaxPor08=' + dataToSend.TaxPor08;
            url += '&TaxMto01=' + dataToSend.TaxMto01;
            url += '&TaxMto02=' + dataToSend.TaxMto02;
            url += '&TaxMto03=' + dataToSend.TaxMto03;
            url += '&TaxMto04=' + dataToSend.TaxMto04;
            url += '&TaxMto05=' + dataToSend.TaxMto05;
            url += '&TaxMto06=' + dataToSend.TaxMto06;
            url += '&TaxMto07=' + dataToSend.TaxMto07;
            url += '&TaxMto08=' + dataToSend.TaxMto08;
            url += '&Info01=' + encodeURIComponent(dataToSend.Info01);
            url += '&Info02=' + encodeURIComponent(dataToSend.Info02);
            url += '&Info03=' + encodeURIComponent(dataToSend.Info03);
            url += '&Info04=' + encodeURIComponent(dataToSend.Info04);
            url += '&Info05=' + encodeURIComponent(dataToSend.Info05);
            url += '&Info06=' + encodeURIComponent(dataToSend.Info06);
            url += '&Info07=' + encodeURIComponent(dataToSend.Info07);
            url += '&Info08=' + encodeURIComponent(dataToSend.Info08);
            url += '&Info09=' + encodeURIComponent(dataToSend.Info09);
            url += '&Info10=' + encodeURIComponent(dataToSend.Info10);
            url += '&InfoDate01=' + encodeURIComponent(dataToSend.InfoDate01);
            url += '&InfoDate02=' + encodeURIComponent(dataToSend.InfoDate02);
            url += '&InfoDate03=' + encodeURIComponent(dataToSend.InfoDate03);
            url += '&InfoDate04=' + encodeURIComponent(dataToSend.InfoDate04);
            url += '&InfoDate05=' + encodeURIComponent(dataToSend.InfoDate05);
            url += '&InfoMto01=' + dataToSend.InfoMto01;
            url += '&InfoMto02=' + dataToSend.InfoMto02;
            url += '&InfoMto03=' + dataToSend.InfoMto03;
            url += '&InfoMto04=' + dataToSend.InfoMto04;
            url += '&InfoMto05=' + dataToSend.InfoMto05;
            url += '&Post=' + encodeURIComponent(dataToSend.Post);
            url += '&PostDate=' + encodeURIComponent(dataToSend.PostDate);
            url += '&NumComensales=' + dataToSend.NumComensales;
            url += '&IdUsuario=' + dataToSend.IdUsuario;
            url += '&TxtUsuario=' + encodeURIComponent(dataToSend.TxtUsuario);
            url += '&IdUsuarioModificador=' + dataToSend.IdUsuarioModificador;
            url += '&TxtUsuarioModificador=' + encodeURIComponent(dataToSend.TxtUsuarioModificador);
            url += '&FechaModificacion=' + encodeURIComponent(dataToSend.FechaModificacion);
            url += '&IdEstado=' + dataToSend.IdEstado;
            url += '&TxtEstado=' + encodeURIComponent(dataToSend.TxtEstado);
            url += '&IdMesa=' + dataToSend.IdMesa;
            url += '&IdTurno=' + dataToSend.IdTurno;
            url += '&productosSeleccionado=' + encodeURIComponent(productosJson);
            $.ajax({
                url: url,
                type: 'POST',
                contentType: 'application/json',
                /*data: JSON.stringify(dataToSend),*/
                success: function (response) {
                    if (response.mensaje) {
                        window.location.href = '/Tnst04CompEmitido/boleta_v_listado'; // Reemplaza 'otra_ruta' con la URL deseada
                    } else {
                        // Muestra errores si los hay
                        alert('Error: ' + response.errores);

                    }
                    // Asigna el valor de comprobanteId a tu campo en la vista
                    $('#comprobanteIdInput').val(response.comprobanteId);

                }
            });
        } else {
            
        }
    } else {
        alert('No puedes guardar un comprobante en blanco');
    }
}

function returndatenow() { 
// Obtiene la fecha actual en UTC
const fechaActualUTC = new Date();

// Ajusta la fecha para la zona horaria de Perú (Lima) (UTC-5)
const fechaActualLima = new Date(fechaActualUTC.getTime() - 5 * 60 * 60 * 1000);

// Obtiene las partes de la fecha
const year = fechaActualLima.getUTCFullYear();
const month = fechaActualLima.getUTCMonth() + 1; // Meses en JavaScript se cuentan desde 0
const day = fechaActualLima.getUTCDate();

// Formatea la fecha en el formato yyyy-MM-dd
    const fechaHoyPeru = `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
    return fechaHoyPeru;
}






//Editar respuesta inmediata
//$.ajax({
//    url: '/Editar/' + id,
//    type: 'GET',
//    success: function (response) {
//        if (response) {
//            var comprobante = response.Comprobante;
//            $('#comprobanteIdInput').val(comprobante.IdComprobante);
//            $('#NroCompEmitido').val(comprobante.NroCompEmitido);
//            $('#TxtNumero').val(comprobante.TxtNumero);
//            $('#TxtSerie').val(comprobante.TxtSerie);
//            $('#idtipoComp').val(comprobante.IdTipoComp);
//            $('#clienteID').val(comprobante.IdCliente);
//            $('#choferId').val(comprobante.IdEmpChofer);
//            $('#OperarioId').val(comprobante.IdEmpleado);
//            $('#AutorizadorId').val(comprobante.IdEmpAutorizador);
//            $('#TxtSerie').val(comprobante.TxtSerie);
//            $('#TxtNumero').val(comprobante.TxtNumero);
//            $('#FecNegocio').val(comprobante.FecNegocio);
//            $('#FecRegEmitido').val(comprobante.FecRegEmitido);
//            $('#FecRegistro').val(comprobante.FecRegistro);
//            $('#fechaEmision').val(comprobante.FecEmi);
//            $('#fechaVcto').val(comprobante.FecVcto);
//            $('#fechaCancelacion').val(comprobante.FecCanc);
//            $('#CantVta').val(comprobante.IdCanVta);
//            $('#TipoOrden').val(comprobante.IdTipoOrden);
//            $('#Locationid').val(comprobante.IdLocation);
//            $('#Locationtoid').val(comprobante.IdLocationto);
//            $('#txtobservacion').val(comprobante.TxtObserv);
//            $('#mto_tc_vta').val(comprobante.MtoTcVta);
//            $('#neto').val(comprobante.MtoNeto);
//            $('#desc').val(comprobante.MtoDsctoTot);
//            $('#Mtoapdesc').val(comprobante.MtoSubTot);
//            $('#igv').val(comprobante.MtoImptoTot);
//            $('#total').val(comprobante.MtoTotComp);


//            var detalles = response.Detalles;
//            if (detalles && detalles.length > 0) {
//                detalles.forEach(function (detalle) {
//                    var producto = {
//                        idProducto: detalle.IdProducto,
//                        nombreProducto: detalle.TxtProducto,
//                        neto: detalle.MtoVtaSinTax,
//                        descuento: detalle.PorDscto,
//                        mtodescuento: detalle.MtoDsctoConTax, // Ejemplo
//                        subtotal: detalle.MtoVtaConTax - detalle.TaxMtoTot, // Ejemplo
//                        igv: detalle.TaxPor01,
//                        mtoigv: detalle.TaxMtoTot,
//                        monto_si: detalle.PunitSinTax, // Ejemplo - debes asignar un valor adecuado
//                        monto_ci: detalle.PunitConTax, // Ejemplo - debes asignar un valor adecuado
//                        total: detalle.MtoVtaConTax, // Ejemplo - debes asignar un valor adecuado
//                        observacion: detalle.TxtObserv // Puedes asignar algún valor relevante
//                    };
//                    productosSeleccionados.push(producto);
//                });
//            }

//            window.location.href = '/Tnst04CompEmitido/boleta_v';
//        } else {
//            // Manejar si no se recibe un comprobante
//        }
//    },
//    error: function () {
//        // Manejar errores si la solicitud AJAX falla
//    }
//});

// Este código se ejecutará cuando la página se cargue completamente


   

