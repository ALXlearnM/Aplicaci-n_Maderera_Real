
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
        $('#clienteoptionname').prop('selectedIndex', 0);
        $('#clienteoptionruc').prop('selectedIndex', 0);
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
        $('#prim_nombrecli, #seg_nombrecli, #patapellidocli, #matapellidocli, #razonSocialcli, #nroDoccli, #celcli').val('');
        $('#provinciacli, #distritocli').empty().prop('selectedIndex', 0);
        $('#TipoNroDoccli').prop('selectedIndex', 0);
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
        formData.append('tipo_doc', $('#TipoNroDoccli').val() !== null ? $('#TipoNroDoccli').val() : '');
        formData.append('nro_doc', $('#nroDoccli').val() !== null ? $('#nroDoccli').val() : '');
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
//function filtrarClientes() {
//    var filtroNombre = document.getElementById('filtroNombreCli').value.toLowerCase();
//    var filtroRUC = document.getElementById('filtroRUCCli').value.toLowerCase();
//    var optcliname = document.getElementById('clienteoptionname').value;
//    var optcliruc = document.getElementById('clienteoptionid').value;

//    $('#tablaClientes tbody tr').each(function () {
//        var nombreCliente = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
//        var rucCliente = $(this).find('.client-cell:nth-child(4)').text().toLowerCase();

//        // Lógica para aplicar el filtro según la opción seleccionada por el usuario
//        var nombreCoincide, rucCoincide;

//        if (optcliname === '1') {
//            // Opción "Comienza con"
//            nombreCoincide = nombreCliente.startsWith(filtroNombre);
//        } else if (optcliname === '2') {
//            // Opción "Contiene"
//            nombreCoincide = nombreCliente.includes(filtroNombre);
//        }

//        if (optcliruc === '1') {
//            // Opción "Comienza con"
//            rucCoincide = rucCliente.startsWith(filtroRUC);
//        } else if (optcliruc === '2') {
//            // Opción "Contiene"
//            rucCoincide = rucCliente.includes(filtroRUC);
//        }

//        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
//        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
//            $(this).show();
//        } else {
//            $(this).hide();
//        }
//    });
//}

//CHOFER

//Proceso para cerrar la ventana modal CHOFER
$(document).ready(function () {
    $('#modalcho').on('hidden.bs.modal', function () {
        $('#chocbo').prop('selectedIndex', 0);
        $('#choferoptionname').prop('selectedIndex', 0);
        $('#choferoptionruc').prop('selectedIndex', 0);

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
        $('#prim_nombrecho, #seg_nombrecho, #patapellidocho, #matapellidocho, #razonSocialcho, #nroDoccho, #celcho').val('');
        $('#provinciacho, #distritocho').empty().prop('selectedIndex', 0);
        $('##TipoNroDoccho').prop('selectedIndex', 0);

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
            formData.append('tipo_doc', $('#TipoNroDoccho').val() !== null ? $('#TipoNroDoccho').val() : '');
            formData.append('nro_doc', $('#nroDoccho').val() !== null ? $('#nroDoccho').val() : '');
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
    var optchoname = document.getElementById('choferoptionname').value;
    var optcliruc= document.getElementById('choferoptionruc').value;
    // Recorremos todas las filas de la tabla

    $('.tablaChoferes tbody tr').each(function () {

        var nombreChofer = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
        var rucChofer = $(this).find('.client-cell:nth-child(4)').text().toLowerCase();

        var nombreCoincide, rucCoincide;

        if (optchoname === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreChofer.startsWith(filtroNombre);
        } else if (optchoname === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreChofer.includes(filtroNombre);
        }

        if (optcliruc === '1') {
            // Opción "Comienza con"
            rucCoincide = rucChofer.startsWith(filtroRUC);
        } else if (optcliruc === '2') {
            // Opción "Contiene"
            rucCoincide = rucChofer.includes(filtroRUC);
        }


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
        $('#operariooptionruc').prop('selectedIndex', 0);
        $('#operariooptionname').prop('selectedIndex', 0);
        
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
        $('#prim_nombreope, #seg_nombreope, #patapellidoope, #matapellidoope, #razonSocialope, #nroDocope, #celope').val('');
        $('#provinciaope, #distritoope').empty().prop('selectedIndex', 0);
        $('#TipoNroDocope').prop('selectedIndex', 0);


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
        formData.append('tipo_doc', $('#TipoNroDocope').val() !== null ? $('#TipoNroDocope').val() : '');
        formData.append('nro_doc', $('#nroDocope').val() !== null ? $('#nroDocope').val() : '');
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
function filtrarOperarios() {
    var filtroNombre = document.getElementById('filtroNombreOpe').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCOpe').value.toLowerCase();
    var optopername = document.getElementById('operariooptionname').value;
    var optoperruc = document.getElementById('operariooptionruc').value;

    // Recorremos todas las filas de la tabla de Operarios
    $('.tablaOperario tbody tr').each(function () {
        var nombreOperario = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
        var rucOperario = $(this).find('.client-cell:nth-child(4)').text().toLowerCase();

        var nombreCoincide, rucCoincide;

        if (optopername === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreOperario.startsWith(filtroNombre);
        } else if (optopername === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreOperario.includes(filtroNombre);
        }

        if (optoperruc === '1') {
            // Opción "Comienza con"
            rucCoincide = rucOperario.startsWith(filtroRUC);
        } else if (optoperruc === '2') {
            // Opción "Contiene"
            rucCoincide = rucOperario.includes(filtroRUC);
        }

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
        $('#autorizadoroptionname').prop('selectedIndex', 0);
        $('#autorizadoroptionruc').prop('selectedIndex', 0);
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
        $('#prim_nombreaut, #seg_nombreaut, #patapellidoaut, #matapellidoaut, #razonSocialaut, #nroDocaut, #celaut').val('');
        $('#provinciaaut, #distritoaut').empty().prop('selectedIndex', 0);
        $('#TipoNroDocaut').prop('selectedIndex', 0);


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
            formData.append('tipo_doc', $('#TipoNroDocaut').val() !== null ? $('#TipoNroDocaut').val() : '');
            formData.append('nro_doc', $('#nroDocaut').val() !== null ? $('#nroDocaut').val() : '');
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
    var optautorname = document.getElementById('autorizadoroptionname').value;
    var optautorruc = document.getElementById('autorizadoroptionruc').value;

    // Recorremos todas las filas de la tabla de Autorizadores
    $('.tablaautorizador tbody tr').each(function () {
        var nombreAutorizador = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
        var rucAutorizador = $(this).find('.client-cell:nth-child(4)').text().toLowerCase();

        var nombreCoincide, rucCoincide;

        if (optautorname === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreAutorizador.startsWith(filtroNombre);
        } else if (optautorname === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreAutorizador.includes(filtroNombre);
        }

        if (optautorruc === '1') {
            // Opción "Comienza con"
            rucCoincide = rucAutorizador.startsWith(filtroRUC);
        } else if (optautorruc === '2') {
            // Opción "Contiene"
            rucCoincide = rucAutorizador.includes(filtroRUC);
        }

        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

//CORTADOR

//Proceso para cerrar la ventana modal CORTADOR
$(document).ready(function () {
    $('#modalcort').on('hidden.bs.modal', function () {
        $('#cortcbo').prop('selectedIndex', 0);
        $('#cortadoroptionname').prop('selectedIndex', 0);
        $('#cortadoroptionruc').prop('selectedIndex', 0);
        cargarDatosCortador();
        // Vaciar los campos de filtro
        $('#filtroNombreCort').val('');
        $('#filtroRUCcort').val('');
    });
});

//Proceso para cerrar la ventana modal create Cortador
$(document).ready(function () {
    $('#createCortadorModal').on('hidden.bs.modal', function () {
        cargarDatosCortador();
        // Vaciar los campos de Crear
        $('#prim_nombrecort, #seg_nombrecort, #patapellidocort, #matapellidocort, #razonSocialcort, #nroDoccort, #celcort').val('');
        $('#provinciacort, #distritocort').empty().prop('selectedIndex', 0);
        $('#TipoNroDoccort').prop('selectedIndex', 0);
    });
});

//INSERCION CORTADOR
$(document).ready(function () {
    $('#crearCortadorBtn').click(function () {
        // Validar que los campos requeridos estén llenos
        if (camposRequeridosLlenos('createCortadorForm')) {
            // Crear un objeto FormData
            var formData = new FormData();

            /*Obtener los valores del formulario y agregarlos a FormData*/
            formData.append('txt_prim_nomb', $('#prim_nombrecort').val());
            formData.append('txt_seg_nomb', $('#seg_nombrecort').val());
            formData.append('txt_ape_pat', $('#patapellidocort').val());
            formData.append('txt_ape_mat', $('#matapellidocort').val());
            formData.append('razonsocial', $('#razonSocialcort').val());
            formData.append('tipo_doc', $('#TipoNroDoccort').val() !== null ? $('#TipoNroDoccort').val() : '');
            formData.append('nro_doc', $('#nroDoccort').val() !== null ? $('#nroDoccort').val() : '');
            formData.append('numtelefono', $('#celcort').val());
            formData.append('id_distrito', $('#distritocort').val());
            formData.append('direccion', $('#direccioncort').val());
            formData.append('id_cat', 5);

            // Realizar la solicitud AJAX al controlador
            $.ajax({
                url: '/Tnst04CompEmitido/CrearCortador',
                type: 'POST',
                data: formData,
                processData: false,  // No procesar los datos (dejar que FormData lo haga)
                contentType: false,  // No establecer contentType (FormData lo establece correctamente)
                success: function (response) {
                    /*console.log('Cortador creado correctamente:', response);*/
                    cargarDatosCortador();
                    $('#createCortadorModal').modal('hide');
                },
                error: function (error) {
                    console.error('Error al crear el cortador:', error);
                    // Aquí puedes manejar errores si es necesario
                }
            });
        } else {
            alert('Por favor, complete todos los campos requeridos.');
        }
    });
});

//CARGAR CORTADOR EN TABLA CORTADOR
function cargarDatosCortador() {
    $.ajax({
        url: '/Tnst04CompEmitido/RecargarCortador', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            /*console.log('Cortador:', response);*/
            $('#bodycort').empty();

            // Iterate through the updated cortador data and append rows to tbody
            $.each(response, function (index, cortador) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + cortador.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + cortador.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + cortador.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + cortador.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarCortador(\'modalcort\',\'cortcbo\',\'CortadorId\',\'cortadorNombre\',\'' +
                    cortador.idempleado + '\', \'' + cortador.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodycort').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del Cortador:', error.responseText);
        }
    });
}

//CBO DEPENDIENTE CORTADOR
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#departamentocort').change(function () {
        var departamentoId = $(this).val();

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerProvincias',
            type: 'GET',
            data: { departamentoId: departamentoId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#provinciacort').empty();
                $.each(data, function (index, item) {
                    $('#provinciacort').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
                });

                // Llama al evento change para actualizar los distritos
                $('#provinciacort').change();
            },
            error: function () {
                console.error('Error al obtener provincias.');
            }
        });
    });

    // Manejador de cambio para la provincia
    $('#provinciacort').change(function () {
        var provinciaId = $(this).val();

        // Realiza una solicitud AJAX para obtener los distritos
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerDistritos',
            type: 'GET',
            data: { provinciaId: provinciaId },
            success: function (data) {
                // Actualiza el contenido del select de distrito
                $('#distritocort').empty();
                $.each(data, function (index, item) {
                    $('#distritocort').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
                });
            },
            error: function () {
                console.error('Error al obtener distritos.');
            }
        });
    });
});

//Cargar la selección de CORTADOR
function seleccionarCortador(modalId, selectId, idcod, idtext, idCort, nombreCort) {
    event.preventDefault();
    var CortadorIdInput = document.getElementById(idcod);
    CortadorIdInput.value = idCort;
    document.getElementById(idtext).innerText = nombreCort;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var CortadorNombreSpan = document.getElementById('cortadorNombre');
    var removeCortadorSpan = document.getElementById('removeCortadorSpan');
    CortadorNombreSpan.innerText = nombreCort;
    CortadorNombreSpan.style.display = 'inline-block';
    removeCortadorSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('cortcbo');
    selectElement.style.display = 'none';
}

//El span CORTADOR para volver al select
function removeCortador() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('cortadorNombre').innerText = '';
    document.getElementById('CortadorId').value = '';
    document.getElementById('cortadorNombre').style.display = 'none';
    document.getElementById('removeCortadorSpan').style.display = 'none';
    var selectElement = document.getElementById('cortcbo');
    selectElement.style.display = 'inline-block';
}

//filtro de CORTADOR por Nombre y Ruc
function filtrarCortador() {
    var filtroNombre = document.getElementById('filtroNombreCort').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCcort').value.toLowerCase();
    var optcortname = document.getElementById('cortadoroptionname').value;
    var optcortruc = document.getElementById('cortadoroptionruc').value;

    // Recorremos todas las filas de la tabla de Cortadores
    $('.tablacortador tbody tr').each(function () {
        var nombreCortador = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
        var rucCortador = $(this).find('.client-cell:nth-child(4)').text().toLowerCase();

        var nombreCoincide, rucCoincide;

        if (optcortname === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreCortador.startsWith(filtroNombre);
        } else if (optcortname === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreCortador.includes(filtroNombre);
        }

        if (optcortruc === '1') {
            // Opción "Comienza con"
            rucCoincide = rucCortador.startsWith(filtroRUC);
        } else if (optcortruc === '2') {
            // Opción "Contiene"
            rucCoincide = rucCortador.includes(filtroRUC);
        }

        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

//ASERRADERO

//Proceso para cerrar la ventana modal ASERRADERO
$(document).ready(function () {
    $('#modalase').on('hidden.bs.modal', function () {
        $('#asecbo').prop('selectedIndex', 0);
        $('#aserraderooptionname').prop('selectedIndex', 0);
        $('#aserraderooptionruc').prop('selectedIndex', 0);
        cargarDatosAserradero();
        // Vaciar los campos de filtro
        $('#filtroNombreAse').val('');
        $('#filtroRUCase').val('');
    });
});

//Proceso para cerrar la ventana modal create Aserradero
$(document).ready(function () {
    $('#createAserraderoModal').on('hidden.bs.modal', function () {
        cargarDatosAserradero();
        // Vaciar los campos de Crear
        $('#prim_nombrease, #seg_nombrease, #patapellidoase, #matapellidoase, #razonSocialase, #nroDocase, #celase').val('');
        $('#provinciaase, #distritoase').empty().prop('selectedIndex', 0);
        $('#TipoNroDocase').prop('selectedIndex', 0);
    });
});

//INSERCION ASERRADERO
$(document).ready(function () {
    $('#crearAserraderoBtn').click(function () {
        // Validar que los campos requeridos estén llenos
        if (camposRequeridosLlenos('createAserraderoForm')) {
            // Crear un objeto FormData
            var formData = new FormData();

            /*Obtener los valores del formulario y agregarlos a FormData*/
            formData.append('txt_prim_nomb', $('#prim_nombrease').val());
            formData.append('txt_seg_nomb', $('#seg_nombrease').val());
            formData.append('txt_ape_pat', $('#patapellidoase').val());
            formData.append('txt_ape_mat', $('#matapellidoase').val());
            formData.append('razonsocial', $('#razonSocialase').val());
            formData.append('tipo_doc', $('#TipoNroDocase').val() !== null ? $('#TipoNroDocase').val() : '');
            formData.append('nro_doc', $('#nroDocase').val() !== null ? $('#nroDocase').val() : '');
            formData.append('numtelefono', $('#celase').val());
            formData.append('id_distrito', $('#distritoase').val());
            formData.append('direccion', $('#direccionase').val());
            formData.append('id_cat', 4);

            // Realizar la solicitud AJAX al controlador
            $.ajax({
                url: '/Tnst04CompEmitido/CrearAserradero',
                type: 'POST',
                data: formData,
                processData: false,  // No procesar los datos (dejar que FormData lo haga)
                contentType: false,  // No establecer contentType (FormData lo establece correctamente)
                success: function (response) {
                    /*console.log('Aserradero creado correctamente:', response);*/
                    cargarDatosAserradero();
                    $('#createAserraderoModal').modal('hide');
                },
                error: function (error) {
                    console.error('Error al crear el aserradero:', error);
                    // Aquí puedes manejar errores si es necesario
                }
            });
        } else {
            alert('Por favor, complete todos los campos requeridos.');
        }
    });
});

//CARGAR ASERRADERO EN TABLA ASERRADERO
function cargarDatosAserradero() {
    $.ajax({
        url: '/Tnst04CompEmitido/RecargarAserradero', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            /*console.log('Aserradero:', response);*/
            $('#bodyase').empty();

            // Iterate through the updated aserradero data and append rows to tbody
            $.each(response, function (index, aserradero) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + aserradero.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + aserradero.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + aserradero.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + aserradero.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarAserradero(\'modalase\',\'asecbo\',\'AserraderoId\',\'aserraderoNombre\',\'' +
                    aserradero.idempleado + '\', \'' + aserradero.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodyase').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del Aserradero:', error.responseText);
        }
    });
}

//CBO DEPENDIENTE ASERRADERO
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#departamentoase').change(function () {
        var departamentoId = $(this).val();

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerProvincias',
            type: 'GET',
            data: { departamentoId: departamentoId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#provinciaase').empty();
                $.each(data, function (index, item) {
                    $('#provinciaase').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
                });

                // Llama al evento change para actualizar los distritos
                $('#provinciaase').change();
            },
            error: function () {
                console.error('Error al obtener provincias.');
            }
        });
    });

    // Manejador de cambio para la provincia
    $('#provinciaase').change(function () {
        var provinciaId = $(this).val();

        // Realiza una solicitud AJAX para obtener los distritos
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerDistritos',
            type: 'GET',
            data: { provinciaId: provinciaId },
            success: function (data) {
                // Actualiza el contenido del select de distrito
                $('#distritoase').empty();
                $.each(data, function (index, item) {
                    $('#distritoase').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
                });
            },
            error: function () {
                console.error('Error al obtener distritos.');
            }
        });
    });
});

//Cargar la selección de ASERRADERO
function seleccionarAserradero(modalId, selectId, idcod, idtext, idAse, nombreAse) {
    event.preventDefault();
    var AserraderoIdInput = document.getElementById(idcod);
    AserraderoIdInput.value = idAse;
    document.getElementById(idtext).innerText = nombreAse;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var AserraderoNombreSpan = document.getElementById('aserraderoNombre');
    var removeAserraderoSpan = document.getElementById('removeAserraderoSpan');
    AserraderoNombreSpan.innerText = nombreAse;
    AserraderoNombreSpan.style.display = 'inline-block';
    removeAserraderoSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('asecbo');
    selectElement.style.display = 'none';
}

//El span ASERRADERO para volver al select
function removeAserradero() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('aserraderoNombre').innerText = '';
    document.getElementById('AserraderoId').value = '';
    document.getElementById('aserraderoNombre').style.display = 'none';
    document.getElementById('removeAserraderoSpan').style.display = 'none';
    var selectElement = document.getElementById('asecbo');
    selectElement.style.display = 'inline-block';
}

//filtro de ASERRADERO por Nombre y Ruc
function filtrarAserradero() {
    var filtroNombre = document.getElementById('filtroNombreAse').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCAse').value.toLowerCase();
    var optasename = document.getElementById('aserraderooptionname').value;
    var optaseruc = document.getElementById('aserraderooptionruc').value;

    // Recorremos todas las filas de la tabla de Aserraderos
    $('.tablaaserradero tbody tr').each(function () {
        var nombreAserradero = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
        var rucAserradero = $(this).find('.client-cell:nth-child(4)').text().toLowerCase();

        var nombreCoincide, rucCoincide;

        if (optasename === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreAserradero.startsWith(filtroNombre);
        } else if (optasename === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreAserradero.includes(filtroNombre);
        }

        if (optaseruc === '1') {
            // Opción "Comienza con"
            rucCoincide = rucAserradero.startsWith(filtroRUC);
        } else if (optaseruc === '2') {
            // Opción "Contiene"
            rucCoincide = rucAserradero.includes(filtroRUC);
        }

        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

//UNIM

//Proceso para cerrar la ventana modal UNIM
$(document).ready(function () {
    $('#modalunim').on('hidden.bs.modal', function () {
        $('#unimcbo').prop('selectedIndex', 0);
        $('#unimoptionname').prop('selectedIndex', 0);
        $('#unimoptionruc').prop('selectedIndex', 0);
        cargarDatosUNIM();
        // Vaciar los campos de filtro
        $('#filtroNombreUNIM').val('');
        $('#filtroRUCunim').val('');
    });
});

//Proceso para cerrar la ventana modal create UNIM
$(document).ready(function () {
    $('#createUnimoqModal').on('hidden.bs.modal', function () {
        cargarDatosUNIM();
        // Vaciar los campos de Crear
        $('#prim_nombreunim, #seg_nombreunim, #patapellidounim, #matapellidounim, #razonSocialunim, #nroDocunim, #celunim').val('');
        $('#provinciaunim, #distritounim').empty().prop('selectedIndex', 0);
        $('#TipoNroDocunim').prop('selectedIndex', 0);
    });
});

//INSERCION UNIM
$(document).ready(function () {
    $('#crearUNIMBtn').click(function () {
        // Validar que los campos requeridos estén llenos
        if (camposRequeridosLlenos('createUnimoqForm')) {
            // Crear un objeto FormData
            var formData = new FormData();

            /*Obtener los valores del formulario y agregarlos a FormData*/
            formData.append('txt_prim_nomb', $('#prim_nombreunim').val());
            formData.append('txt_seg_nomb', $('#seg_nombreunim').val());
            formData.append('txt_ape_pat', $('#patapellidounim').val());
            formData.append('txt_ape_mat', $('#matapellidounim').val());
            formData.append('razonsocial', $('#razonSocialunim').val());
            formData.append('tipo_doc', $('#TipoNroDocunim').val() !== null ? $('#TipoNroDocunim').val() : '');
            formData.append('nro_doc', $('#nroDocunim').val() !== null ? $('#nroDocunim').val() : '');
            formData.append('numtelefono', $('#celunim').val());
            formData.append('id_distrito', $('#distritounim').val());
            formData.append('direccion', $('#direccionunim').val());
            formData.append('id_cat', 7);

            // Realizar la solicitud AJAX al controlador
            $.ajax({
                url: '/Tnst04CompEmitido/CrearUnimoq',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    cargarDatosUNIM();
                    $('#createUnimoqModal').modal('hide');
                },
                error: function (error) {
                    console.error('Error al crear UNIM:', error);
                }
            });
        } else {
            alert('Por favor, complete todos los campos requeridos.');
        }
    });
});

//CARGAR UNIM EN TABLA UNIM
function cargarDatosUNIM() {
    $.ajax({
        url: '/Tnst04CompEmitido/RecargarUNIM',
        type: 'GET',
        success: function (response) {
            $('#bodyunim').empty();

            // Iterate through the updated UNIM data and append rows to tbody
            $.each(response, function (index, unim) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + unim.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + unim.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + unim.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + unim.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarUNIM(\'modalunim\',\'unimcbo\',\'UNIMId\',\'unimNombre\',\'' +
                    unim.idempleado + '\', \'' + unim.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodyunim').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del UNIM:', error.responseText);
        }
    });
}

//CBO DEPENDIENTE UNIM
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#departamentounim').change(function () {
        var departamentoId = $(this).val();

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerProvincias',
            type: 'GET',
            data: { departamentoId: departamentoId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#provinciaunim').empty();
                $.each(data, function (index, item) {
                    $('#provinciaunim').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
                });

                // Llama al evento change para actualizar los distritos
                $('#provinciaunim').change();
            },
            error: function () {
                console.error('Error al obtener provincias.');
            }
        });
    });

    // Manejador de cambio para la provincia
    $('#provinciaunim').change(function () {
        var provinciaId = $(this).val();

        // Realiza una solicitud AJAX para obtener los distritos
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerDistritos',
            type: 'GET',
            data: { provinciaId: provinciaId },
            success: function (data) {
                // Actualiza el contenido del select de distrito
                $('#distritounim').empty();
                $.each(data, function (index, item) {
                    $('#distritounim').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
                });
            },
            error: function () {
                console.error('Error al obtener distritos.');
            }
        });
    });
});

//Cargar la selección de UNIM
function seleccionarUNIM(modalId, selectId, idcod, idtext, idUNIM, nombreUNIM) {
    event.preventDefault();
    var UNIMIdInput = document.getElementById(idcod);
    UNIMIdInput.value = idUNIM;
    document.getElementById(idtext).innerText = nombreUNIM;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var UNIMNombreSpan = document.getElementById('unimNombre');
    var removeUNIMSpan = document.getElementById('removeUNIMSpan');
    UNIMNombreSpan.innerText = nombreUNIM;
    UNIMNombreSpan.style.display = 'inline-block';
    removeUNIMSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('unimcbo');
    selectElement.style.display = 'none';
}

//El span UNIM para volver al select
function removeUNIM() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('unimNombre').innerText = '';
    document.getElementById('UNIMId').value = '';
    document.getElementById('unimNombre').style.display = 'none';
    document.getElementById('removeUNIMSpan').style.display = 'none';
    var selectElement = document.getElementById('unimcbo');
    selectElement.style.display = 'inline-block';
}

//filtro de UNIM por Nombre y Ruc
function filtrarUNIM() {
    var filtroNombre = document.getElementById('filtroNombreUNIM').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCUNIM').value.toLowerCase();
    var optasename = document.getElementById('unimoptionname').value;
    var optaseruc = document.getElementById('unimoptionruc').value;

    // Recorremos todas las filas de la tabla de UNIM
    $('.tablaunim tbody tr').each(function () {
        var nombreUNIM = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
        var rucUNIM = $(this).find('.client-cell:nth-child(4)').text().toLowerCase();

        var nombreCoincide, rucCoincide;

        if (optasename === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreUNIM.startsWith(filtroNombre);
        } else if (optasename === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreUNIM.includes(filtroNombre);
        }

        if (optaseruc === '1') {
            // Opción "Comienza con"
            rucCoincide = rucUNIM.startsWith(filtroRUC);
        } else if (optaseruc === '2') {
            // Opción "Contiene"
            rucCoincide = rucUNIM.includes(filtroRUC);
        }

        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

//CARGADOR

//Proceso para cerrar la ventana modal CARGADOR
$(document).ready(function () {
    $('#modalcar').on('hidden.bs.modal', function () {
        $('#carcbo').prop('selectedIndex', 0);
        $('#cargadoroptionname').prop('selectedIndex', 0);
        $('#cargadoroptionruc').prop('selectedIndex', 0);
        cargarDatosCargador();
        // Vaciar los campos de filtro
        $('#filtroNombreCar').val('');
        $('#filtroRUCcar').val('');
    });
});

//Proceso para cerrar la ventana modal create Cargador
$(document).ready(function () {
    $('#createCargadorModal').on('hidden.bs.modal', function () {
        cargarDatosCargador();
        // Vaciar los campos de Crear
        $('#prim_nombrecar, #seg_nombrecar, #patapellidocar, #matapellidocar, #razonSocialcar, #nroDoccar, #celcar').val('');
        $('#provinciacar, #distritocar').empty().prop('selectedIndex', 0);
        $('#TipoNroDoccar').prop('selectedIndex', 0);
    });
});

//INSERCION CARGADOR
$(document).ready(function () {
    $('#crearCargadorBtn').click(function () {
        // Validar que los campos requeridos estén llenos
        if (camposRequeridosLlenos('createCargadorForm')) {
            // Crear un objeto FormData
            var formData = new FormData();

            /*Obtener los valores del formulario y agregarlos a FormData*/
            formData.append('txt_prim_nomb', $('#prim_nombrecar').val());
            formData.append('txt_seg_nomb', $('#seg_nombrecar').val());
            formData.append('txt_ape_pat', $('#patapellidocar').val());
            formData.append('txt_ape_mat', $('#matapellidocar').val());
            formData.append('razonsocial', $('#razonSocialcar').val());
            formData.append('tipo_doc', $('#TipoNroDoccar').val() !== null ? $('#TipoNroDoccar').val() : '');
            formData.append('nro_doc', $('#nroDoccar').val() !== null ? $('#nroDoccar').val() : '');
            formData.append('numtelefono', $('#celcar').val());
            formData.append('id_distrito', $('#distritocar').val());
            formData.append('direccion', $('#direccioncar').val());
            formData.append('id_cat', 8);

            // Realizar la solicitud AJAX al controlador
            $.ajax({
                url: '/Tnst04CompEmitido/CrearCargador',
                type: 'POST',
                data: formData,
                processData: false,  // No procesar los datos (dejar que FormData lo haga)
                contentType: false,  // No establecer contentType (FormData lo establece correctamente)
                success: function (response) {
                    /*console.log('Cargador creado correctamente:', response);*/
                    cargarDatosCargador();
                    $('#createCargadorModal').modal('hide');
                },
                error: function (error) {
                    console.error('Error al crear el cargador:', error);
                    // Aquí puedes manejar errores si es necesario
                }
            });
        } else {
            alert('Por favor, complete todos los campos requeridos.');
        }
    });
});

//Cargar CARGADOR EN TABLA CARGADOR
function cargarDatosCargador() {
    $.ajax({
        url: '/Tnst04CompEmitido/RecargarCargador', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            /*console.log('Cargador:', response);*/
            $('#bodycar').empty();

            // Iterate through the updated cargador data and append rows to tbody
            $.each(response, function (index, cargador) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + cargador.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + cargador.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + cargador.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + cargador.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarCargador(\'modalcar\',\'carcbo\',\'CargadorId\',\'cargadorNombre\',\'' +
                    cargador.idempleado + '\', \'' + cargador.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodycar').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del Cargador:', error.responseText);
        }
    });
}

//CBO DEPENDIENTE CARGADOR
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#departamentocar').change(function () {
        var departamentoId = $(this).val();

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerProvincias',
            type: 'GET',
            data: { departamentoId: departamentoId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#provinciacar').empty();
                $.each(data, function (index, item) {
                    $('#provinciacar').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
                });

                // Llama al evento change para actualizar los distritos
                $('#provinciacar').change();
            },
            error: function () {
                console.error('Error al obtener provincias.');
            }
        });
    });

    // Manejador de cambio para la provincia
    $('#provinciacar').change(function () {
        var provinciaId = $(this).val();

        // Realiza una solicitud AJAX para obtener los distritos
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerDistritos',
            type: 'GET',
            data: { provinciaId: provinciaId },
            success: function (data) {
                // Actualiza el contenido del select de distrito
                $('#distritocar').empty();
                $.each(data, function (index, item) {
                    $('#distritocar').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
                });
            },
            error: function () {
                console.error('Error al obtener distritos.');
            }
        });
    });
});

//Cargar la selección de CARGADOR
function seleccionarCargador(modalId, selectId, idcod, idtext, idCarg, nombreCarg) {
    event.preventDefault();
    var CargadorIdInput = document.getElementById(idcod);
    CargadorIdInput.value = idCarg;
    document.getElementById(idtext).innerText = nombreCarg;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var CargadorNombreSpan = document.getElementById('cargadorNombre');
    var removeCargadorSpan = document.getElementById('removeCargadorSpan');
    CargadorNombreSpan.innerText = nombreCarg;
    CargadorNombreSpan.style.display = 'inline-block';
    removeCargadorSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('carcbo');
    selectElement.style.display = 'none';
}

//El span CARGADOR para volver al select
function removeCargador() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('cargadorNombre').innerText = '';
    document.getElementById('CargadorId').value = '';
    document.getElementById('cargadorNombre').style.display = 'none';
    document.getElementById('removeCargadorSpan').style.display = 'none';
    var selectElement = document.getElementById('carcbo');
    selectElement.style.display = 'inline-block';
}

//filtro de CARGADOR por Nombre y Ruc
function filtrarCargador() {
    var filtroNombre = document.getElementById('filtroNombreCar').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCcar').value.toLowerCase();
    var optcargname = document.getElementById('cargadoroptionname').value;
    var optcargruc = document.getElementById('cargadoroptionruc').value;

    // Recorremos todas las filas de la tabla de Cargadores
    $('.tablacargador tbody tr').each(function () {
        var nombreCargador = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
        var rucCargador = $(this).find('.client-cell:nth-child(4)').text().toLowerCase();

        var nombreCoincide, rucCoincide;

        if (optcargname === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreCargador.startsWith(filtroNombre);
        } else if (optcargname === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreCargador.includes(filtroNombre);
        }

        if (optcargruc === '1') {
            // Opción "Comienza con"
            rucCoincide = rucCargador.startsWith(filtroRUC);
        } else if (optcargruc === '2') {
            // Opción "Contiene"
            rucCoincide = rucCargador.includes(filtroRUC);
        }

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
        $('#locationoptionname').prop('selectedIndex', 0);
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
        $('#IdTipoLocation').prop('selectedIndex', 0);

        
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
    }
    else if (valorLoc != null && valorLoc!= '') {
        cargarDatosLocalizacion(valorLoc);
    }
    else if (valorLocto != null && valorLocto != '') {
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
    var optlocrname = document.getElementById('locationoptionname').value;

        // Recorremos todas las filas de la tabla
    $('.tablalocalizacion tbody tr').each(function () {
            var nombreLocalizacion = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
            
        var nombreCoincide, rucCoincide;

        if (optlocrname === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreLocalizacion.startsWith(filtroNombre);
        } else if (optlocrname === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreLocalizacion.includes(filtroNombre);
        }

            // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
            if ((filtroNombre === '' || nombreCoincide) /*&& (filtroRUC === '' || rucCoincide)*/) {
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
        $('#locationtooptionname').prop('selectedIndex', 0);
        
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
        $('#IdTipoLocationto').prop('selectedIndex', 0);

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

//CBO DEPENDIENTE LOCATIONTO
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#departamentolto').change(function () {
        var departamentoId = $(this).val();

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerProvincias',
            type: 'GET',
            data: { departamentoId: departamentoId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#provincialto').empty();
                $.each(data, function (index, item) {
                    $('#provincialto').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
                });

                // Llama al evento change para actualizar los distritos
                $('#provincialto').change();
            },
            error: function () {
                console.error('Error al obtener provincias.');
            }
        });
    });

    // Manejador de cambio para la provincia
    $('#provincialto').change(function () {
        var provinciaId = $(this).val();

        // Realiza una solicitud AJAX para obtener los distritos
        $.ajax({
            url: '/Tnst04CompEmitido/ObtenerDistritos',
            type: 'GET',
            data: { provinciaId: provinciaId },
            success: function (data) {
                // Actualiza el contenido del select de distrito
                $('#distritolto').empty();
                $.each(data, function (index, item) {
                    $('#distritolto').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
                });
            },
            error: function () {
                console.error('Error al obtener distritos.');
            }
        });
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
        var nombreLocalizacionto = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
        var nombreCoincide;
        if (optloctorname === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreLocalizacionto.startsWith(filtroNombre);
        } else if (optloctorname === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreLocalizacionto.includes(filtroNombre);
        }
        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide)/* && (filtroRUC === '' || rucCoincide)*/) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

//INVERSIONISTA

// Proceso para cerrar la ventana modal Inversionista
$(document).ready(function () {
    $('#modalinversionista').on('hidden.bs.modal', function () {
        $('#incbo').prop('selectedIndex', 0);
        $('#inversionistaoptionname').prop('selectedIndex', 0);
        $('#inversionistaoptionruc').prop('selectedIndex', 0);
        cargarDatosInversionista();
        // Vaciar los campos de filtro
        $('#filtroNombreInv').val('');
        $('#filtroRUCInv').val('');
    });
});

// Proceso para cerrar la ventana modal create Inversionista
$(document).ready(function () {
    $('#createInversionistaModal').on('hidden.bs.modal', function () {
        // Vaciar los campos de Crear
        cargarDatosInversionista();
        $('#prim_nombreinversionista, #seg_nombreinversionista, #patapellidoinversionista, #matapellidoinversionista, #razonSocialinversionista, #nroDocinversionista, #celinversionista').val('');
        $('#TipoNroDocinversionista').prop('selectedIndex', 0);
    });
});

// INSERCION INVERSIONISTA
$('#crearInversionistaBtn').click(function () {
    if (camposRequeridosLlenos('createInversionistaForm')) {
        // Verificar valores individuales en la consola
        console.log('txt_prim_nomb:', $('#prim_nombreinversionista').val());
        console.log('txt_seg_nomb:', $('#seg_nombreinversionista').val());
        console.log('txt_ape_pat:', $('#patapellidoinversionista').val());
        console.log('txt_ape_mat:', $('#matapellidoinversionista').val());
        console.log('razonsocial:', $('#razonSocialinversionista').val());
        console.log('tipo_doc:', $('#TipoNroDocinversionista').val());
        console.log('nro_doc:', $('#nroDocinversionista').val());
        console.log('numtelefono:', $('#celinversionista').val());
        console.log('direccioninv:', $('#direccioninversionista').val());

        // Crear un objeto de datos directo
        var data = {
            txt_prim_nomb: $('#prim_nombreinversionista').val(),
            txt_seg_nomb: $('#seg_nombreinversionista').val(),
            txt_ape_pat: $('#patapellidoinversionista').val(),
            txt_ape_mat: $('#matapellidoinversionista').val(),
            razonsocial: $('#razonSocialinversionista').val() || null,
            tipo_doc: $('#TipoNroDocinversionista').val() || null,
            nro_doc: $('#nroDocinversionista').val() || null,
            numtelefono: $('#celinversionista').val(),
            direccioninv: $('#direccioninversionista').val()
        };

        // Imprimir el objeto de datos en la consola
        console.log('Objeto de datos:', data);

        // Enviar la solicitud AJAX con el objeto de datos
        $.ajax({
            url: '/Pret01Predio/CrearInversionista',
            type: 'POST',
            data: data,
            success: function (response) {
                console.log('Inversionista creado correctamente:', response);
                cargarDatosInversionista();
                $('#createInversionistaModal').modal('hide');
            },
            error: function (error) {
                console.error('Error al crear el inversionista:', error.responseText);
            }
        });
    } else {
        alert('Por favor, complete todos los campos requeridos.');
    }
});


// CARGAR INVERSIONISTA EN TABLA INVERSIONISTA
function cargarDatosInversionista() {
    $.ajax({
        url: '/Pret01Predio/RecargarInversionista', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            // Clear the existing tbody content
            $('#bodyinversionista').empty();

            // Iterate through the updated inversionista data and append rows to tbody
            $.each(response, function (index, inversionista) {
                var row = $('<tr>');
                row.append('<td align="center" class="inversionista-cell">' + inversionista.nombreCompleto + '</td>');
                row.append('<td align="center" class="inversionista-cell">' + inversionista.telefono + '</td>');
                row.append('<td align="center" class="inversionista-cell">' + inversionista.ruc + '</td>');
                row.append('<td align="center" class="inversionista-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarInversionista(\'modalinversionista\',\'incbo\',\'inversionistaID\',\'inversionistaNombre\', \'' +
                    inversionista.idInversionista + '\', \'' + inversionista.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                // Append the row to the tbody
                $('#bodyinversionista').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del inversionista:', error.responseText);
        }
    });
}

// Cargar la selección de Inversionista
function seleccionarInversionista(modalId, selectId, idcod, idtext, idInversionista, nombreInversionista) {
    event.preventDefault();
    var inversionistaIdInput = document.getElementById(idcod);
    inversionistaIdInput.value = idInversionista;
    document.getElementById(idtext).innerText = nombreInversionista;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del inversionista y el botón para remover
    var inversionistaNombreSpan = document.getElementById('inversionistaNombre');
    var removeInversionistaSpan = document.getElementById('removeInversionistaSpan');
    inversionistaNombreSpan.innerText = nombreInversionista;
    inversionistaNombreSpan.style.display = 'inline-block';
    removeInversionistaSpan.style.display = 'inline-block';

    // Ocultar el select
    var selectElement = document.getElementById('incbo');
    selectElement.style.display = 'none';
}

// El span inversionista para volver al select
function removeInversionista() {
    // Quitar el valor del inversionista y ocultar el span
    document.getElementById('inversionistaNombre').innerText = '';
    document.getElementById('inversionistaID').value = '';
    document.getElementById('inversionistaNombre').style.display = 'none';
    document.getElementById('removeInversionistaSpan').style.display = 'none';
    var selectElement = document.getElementById('incbo');
    selectElement.style.display = 'inline-block';
}

// Filtro de Inversionista por Nombre y Ruc
function filtrarInversionistas() {
    var filtroNombre = document.getElementById('filtroNombreInv').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCInv').value.toLowerCase();
    var optinvname = document.getElementById('inversionistaoptionname').value;
    var optinvruc = document.getElementById('inversionistaoptionid').value;

    $('.tablaInversionistas tbody tr').each(function () {
        var nombreInversionista = $(this).find('.inversionista-cell:nth-child(1)').text().toLowerCase();
        var rucInversionista = $(this).find('.inversionista-cell:nth-child(4)').text().toLowerCase();

        // Lógica para aplicar el filtro según la opción seleccionada por el usuario
        var nombreCoincide, rucCoincide;

        if (optinvname === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreInversionista.startsWith(filtroNombre);
        } else if (optinvname === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreInversionista.includes(filtroNombre);
        }

        if (optinvruc === '1') {
            // Opción "Comienza con"
            rucCoincide = rucInversionista.startsWith(filtroRUC);
        } else if (optinvruc === '2') {
            // Opción "Contiene"
            rucCoincide = rucInversionista.includes(filtroRUC);
        }

        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}
//PREDIO
//ELIMINAR PREDIO
$(document).ready(function () {
$('#confirmarEliminarPredio').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var idPredio = button.data('id');
    $(this).data('id', idPredio);
}); 
});


function eliminarPredio() {

    var idPredio = $('#confirmarEliminarPredio').data('id');
    console.log('ID del predio a eliminar:', idPredio);
    $.ajax({
        url: '/Pret01Predio/EliminarPredio', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        data: { id: idPredio },
        success: function (response) {
            if (response.mensaje == null) {
                $('#confirmarEliminarPredio').modal('hide');

                window.location.href = response.redirectUrl;

            } else {


            }
        },
        error: function (error) {
            console.error('Error al intentar eliminar', error.responseText);
        }
    });
}


//Crear Predio
$(document).ready(function () {
    $('#crearPredioBtn').click(function () {
        if (camposRequeridosLlenos('FormPredio')) {
            // Crear un objeto FormData
            var formData = new FormData();

            // Agregar datos del formulario al objeto FormData
            formData.append('nroSitio', $('#nrositioPredio').val());
            formData.append('area', parseFloat($('#areapredio').val()));
            formData.append('unidadCatastral', $('#unidadcat').val());
            formData.append('nroHectareas', parseFloat($('#numhectareapredios').val()));
            formData.append('latitud', ($('#latitudpred').val()));
            formData.append('longitud', ($('#longitudpred').val()));
            formData.append('idDistrito', parseInt($('#distritopredio').val()));
            formData.append('idInversionista', parseInt($('#inversionistaID').val()));
            formData.append('nrocomp', $('#nrocomprobantepred').val());
            formData.append('partidaregistral', $('#partidareg').val());
            formData.append('idtipoPredio', $('#tipopredio').val());
            formData.append('fechaadquisicion', $('#fechaAdquisicion').val());
            formData.append('fechacompra', $('#fechaCompra').val());


            for (var i = 0; i < archivos.length; i++) {
                formData.append('archivos', archivos[i]);
            }

            $.ajax({
                url: '/Pret01Predio/CrearPredio',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    alert(response.mensaje);
                },
                error: function (error) {
                    console.error('Error al crear el predio:', error.responseText);
                }
            });
        } else {
            alert('Por favor, complete todos los campos requeridos.');
        }
    });
    $('#crearPredioyCerrarBtn').click(function () {
        var confirmar = confirm("¿Seguro que desea guardar y cerrar?");

        if (confirmar) {
            if (camposRequeridosLlenos('FormPredio')) {
                var formData = new FormData();

                // Agregar datos del formulario al objeto FormData
                formData.append('nroSitio', $('#nrositioPredio').val());
                formData.append('area', parseFloat($('#areapredio').val()));
                formData.append('unidadCatastral', $('#unidadcat').val());
                formData.append('nroHectareas', parseFloat($('#numhectareapredios').val()));
                formData.append('latitud', ($('#latitudpred').val()));
                formData.append('longitud', ($('#longitudpred').val()));
                formData.append('idDistrito', parseInt($('#distritopredio').val()));
                formData.append('idInversionista', parseInt($('#inversionistaID').val()));
                formData.append('nrocomp', $('#nrocomprobantepred').val());
                formData.append('partidaregistral', $('#partidareg').val());
                formData.append('idtipoPredio', $('#tipopredio').val());
                formData.append('fechaadquisicion', $('#fechaAdquisicion').val());
                formData.append('fechacompra', $('#fechaCompra').val());

                for (var i = 0; i < archivos.length; i++) {
                    formData.append('archivos', archivos[i]);
                }

                $.ajax({
                    url: '/Pret01Predio/CrearPredioyCerrar',
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        
                        if (response.mensaje == null) { 
                        window.location.href = response.redirectUrl;
                    }else{
                        alert(response.mensaje);
                    }
                    },
                    error: function (error) {
                        console.error('Error al crear el predio:', error.responseText);
                    }
                });
            } else {
                alert('Por favor, complete todos los campos requeridos.');
            }
        }
        });
    $("#cancelarPred").click(function () {
        window.location.href = '/Pret01Predio/ListadoPre';
    });
});




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
    var productosSeleccionadosIDs = ListadoProductosSeleccionados.map(function (producto) {
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
                var dimension = producto.dimension;
                var peso= producto.peso;
                var unm= producto.unm;
                var diametro = producto.diametro;
                var monto_ci = parseFloat(producto.monto_ci).toFixed(2);
                var NombreProducto=producto.nombreProducto;
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + NombreProducto + '</td>');
                row.append('<td align="center" class="client-cell">' + monto_ci + '</td>');
                row.append('<td align="center" class="client-cell">' + unm + '</td');
                row.append('<td align="center" class="client-cell">' + dimension+ '</td');
                row.append('<td align="center" class="client-cell">' + diametro + '</td');
                row.append('<td align="center" class="client-cell">' + peso + '</td');
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
    var optproname = document.getElementById('productooptionname').value;
    // Recorremos todas las filas de la tabla
    $('.tablaProducto tbody tr').each(function () {
        var nombreProducto = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();

        var nombreCoincide;
        if (optproname === '1') {
            // Opción "Comienza con"
            nombreCoincide = nombreProducto.startsWith(filtroNombre);
        } else if (optproname === '2') {
            // Opción "Contiene"
            nombreCoincide = nombreProducto.includes(filtroNombre);
        }
        // Mostrar la fila si coincide con alguno de los filtros o ambos están vacíos
        if ((filtroNombre === '' || nombreCoincide)/* && (filtroRUC === '' || rucCoincide)*/) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

// Declarar el arreglo para almacenar los productos seleccionados
var ListadoProductosSeleccionados = [];

// Función para añadir un producto a la lista de productos seleccionados
function agregarProducto() {
    var idProducto = parseInt($("#ProductoId").val());
    var cantidad = parseFloat($("#cantidad").val());
    var descuento = parseFloat($("#descuento").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idProducto) || idProducto <= 0 || isNaN(cantidad) || isNaN(descuento) || descuento<0 || cantidad<=0) {
        alert("Por favor, ingrese valores numéricos válidos para el ID, cantidad y descuento.");
        return false; // Detener la ejecución si los campos no son válidos
    }

    $.ajax({
        url: "/Tnst04CompEmitido/ProductoDatos",
        type: "GET",
        data: {
            idProducto: idProducto,
            cantidad: cantidad,
            descuento: descuento
            },
        success: function (response) {
            if (response != null) {
                var igv = response.igv / 100;
                var mci = response.monto_ci;
                var msi = response.monto_si;

                var neto = response.neto;
                var descuentoPorcentaje =response.descuentoPorcentaje;
                var mtodescuento = response.mtodescuento;
                var subtotal = response.subtotal;
                var mtoigv = response.mtoigv;
                var total = response.total;

                ListadoProductosSeleccionados.push({
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
    var producto = ListadoProductosSeleccionados.find(function (prod) {
        return prod.idProducto === idProducto;
    });

    if (producto) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este producto de la lista?");

        if (confirmar) {
            var productoIndex = ListadoProductosSeleccionados.findIndex(function (prod) {
                return prod.idProducto === idProducto;
            });

            if (productoIndex !== -1) {
                ListadoProductosSeleccionados.splice(productoIndex, 1);
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
    ListadoProductosSeleccionados.forEach(function (producto) {
        // Redondear el valor del campo "total" y otros campos a dos decimales solo en la vista
        var mci = parseFloat(producto.monto_ci).toFixed(2);
        var msi = parseFloat(producto.monto_si).toFixed(2);
        var neto = parseFloat(producto.neto).toFixed(2);
        var mtodescuento = parseFloat(producto.mtodescuento).toFixed(2);
        var subtotal = parseFloat(producto.subtotal).toFixed(2);
        var mtoigv = parseFloat(producto.mtoigv).toFixed(2);
        var total = parseFloat(producto.total).toFixed(2);

        var newRow = `
            <tr style="font-size:14px;" id='${producto.idProducto}'>
                <td style="font-size:14px;">${producto.nombreProducto}</td>
                <td style="font-size:14px;">${producto.cantidad}</td>
                <td style="font-size:14px;">${mtodescuento}</td>
                
                <td style="font-size:14px;">${mci}</td>
                <td style="font-size:14px;">${total}</td>
                <td colspan='2' style="font-size:14px;">
                    <button class="btn btn-primary btn-sm" type='button' onclick='editarProducto(${producto.idProducto})'><i class='fa fa-edit'></i></button>
                    <button class="btn btn-danger btn-sm" type='button' onclick='eliminarProducto(${producto.idProducto})'><i class='fa fa-trash'></i></button>
                </td>
            </tr>`;

        $("#productosSeleccionados").append(newRow);
    });
    cargarDatosProducto();
    actualizarTotales();
}
/*
<td>${mtoigv}</td>
<td>${msi}</td>
*/
//Función para abrir el modal de edición
function editarProducto(idProducto) {
    var producto = ListadoProductosSeleccionados.find(function (prod) {
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
    var producto = ListadoProductosSeleccionados.find(function (prod) {
        return prod.idProducto === idProducto;
    });
    var nuevaCantidad = parseFloat(document.getElementById("nuevaCantidad").value);
    var nuevoDescuento = parseFloat(document.getElementById("nuevoDescuento").value);
    var nuevaObservacion = document.getElementById("observacionP").value;

    if (!isNaN(nuevaCantidad) && !isNaN(nuevoDescuento)) {
        var confirmar = confirm("¿Deseas aplicar los cambios y guardarlos?");

        if (confirmar) {

            $.ajax({
                url: "/Tnst04CompEmitido/ProductoDatos",
                type: "GET",
                data: {
                    idProducto: idProducto,
                    cantidad: nuevaCantidad,
                    descuento: nuevoDescuento
                },
                success: function (response) {
                    if (response != null) {
                        var neto = response.neto;
                        var descuentoPorcentaje = response.descuentoPorcentaje;
                        var mtodescuento = response.mtodescuento;
                        var subtotal = response.subtotal;
                        var mtoigv = response.mtoigv;
                        var total = response.total;

                        producto.cantidad = nuevaCantidad;
                        producto.neto = neto;
                        producto.subtotal = subtotal;
                        producto.mtoigv = mtoigv;
                        producto.descuento = descuentoPorcentaje;
                        producto.mtodescuento = mtodescuento;
                        producto.total = total;
                        producto.observacion = nuevaObservacion;

                        document.getElementById('cantidad').value = 1;
                        var descuentoInput = document.getElementById('descuento');

                        // Establece el valor en 0
                        descuentoInput.value = 0;
                        actualizarTablaProductosSeleccionados();
                        cerrarModal('editarModal');
                        actualizarTotales();
                    } else {
                        alert("Producto no encontrado");
                    }
                },
            });
           
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


    for (let i = 0; i < ListadoProductosSeleccionados.length; i++) {
        const Neto = parseFloat(ListadoProductosSeleccionados[i].neto);
        const descuentoVal = parseFloat(ListadoProductosSeleccionados[i].mtodescuento);
        const subtotal = parseFloat(ListadoProductosSeleccionados[i].subtotal);
        const montoIgv = parseFloat(ListadoProductosSeleccionados[i].mtoigv);
        const total = parseFloat(ListadoProductosSeleccionados[i].total);

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


//PREDIO

//Proceso para cerrar la ventana modal CARGADOR
$(document).ready(function () {
    $('#modalvenpre').on('hidden.bs.modal', function () {
        $('#venprecbo').prop('selectedIndex', 0);
        cargarDatosVenPre();
    });
});



//Cargar CARGADOR EN TABLA CARGADOR
function cargarDatosVenPre() {
    $.ajax({
        url: '/Tnst04CompEmitido/RecargarPredio', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            /*console.log('Cargador:', response);*/
            $('#bodyvenpre').empty();

            // Iterate through the updated cargador data and append rows to tbody
            $.each(response, function (index, predio) {
                var fechaFormateada = new Date(predio.fechC).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + predio.unidC+ '</td>');
                row.append('<td align="center" class="client-cell">' + predio.nroS+ '</td>');
                row.append('<td align="center" class="client-cell">' + fechaFormateada + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarVenPredio(\'modalvenpre\',\'venprecbo\',\'idpredioventas\',\'venpreNombre\',\'' +
                    predio.id + '\', \'' + predio.unidC+ '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodyvenpre').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del Cargador:', error.responseText);
        }
    });
}

//CBO DEPENDIENTE CARGADOR
$(document).ready(function () {
    
});

//Cargar la selección de CARGADOR
function seleccionarVenPredio(modalId, selectId, idcod, idtext, idPredio, UnidCatastral) {
    event.preventDefault();
    var PredioIdInput = document.getElementById(idcod);
    PredioIdInput.value = idPredio;
    document.getElementById(idtext).innerText = UnidCatastral;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var CargadorNombreSpan = document.getElementById('venpreNombre');
    var removeCargadorSpan = document.getElementById('removevenpreSpan');
    CargadorNombreSpan.innerText = UnidCatastral;
    CargadorNombreSpan.style.display = 'inline-block';
    removeCargadorSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('venprecbo');
    selectElement.style.display = 'none';
    $.ajax({
        url: '/Tnst04CompEmitido/Recargarcampanas', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { IdPredio: idPredio}, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodycamven').empty();
            $.each(response, function (index, campanas) {
                var fechaFormateada = new Date(campanas.fechI).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });

                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + campanas.codC+ '</td>');
                row.append('<td align="center" class="client-cell">' + parseFloat(campanas.area).toFixed(2) + '</td>');
                row.append('<td align="center" class="client-cell">' + fechaFormateada+ '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarVenCampana(\'modalcamven\',\'camvencbo\',\'idcampanaventas\',\'camvenNombre\',\'' +
                    campanas.id + '\', \'' + campanas.codC + '\')">Seleccionar</button>' +
                    '</td>');
                $('#bodycamven').append(row);
            });

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });

}

//El span CARGADOR para volver al select
function removeVenPre() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('venpreNombre').innerText = '';
    document.getElementById('idpredioventas').value = '';
    document.getElementById('venpreNombre').style.display = 'none';
    document.getElementById('removevenpreSpan').style.display = 'none';
    var selectElement = document.getElementById('venprecbo');
    selectElement.style.display = 'inline-block';
    $.ajax({
        url: '/Tnst04CompEmitido/RemovePredio', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function () {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodycamven').empty();
            removeCamVen();

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });
    
}

//CAMPAÑA

//Proceso para cerrar la ventana modal CARGADOR
$(document).ready(function () {
    $('#modalcamven').on('hidden.bs.modal', function () {
        $('#camvencbo').prop('selectedIndex', 0);
        cargarDatosCamVen();
    });
});



//Cargar CARGADOR EN TABLA CARGADOR
function cargarDatosCamVen() {
    $.ajax({
        url: '/Tnst04CompEmitido/Recargarcampanas', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { IdPredio: null}, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodycamven').empty();
            $.each(response, function (index, campanas) {
                var fechaFormateada = new Date(campanas.fechI).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });

                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + campanas.codC + '</td>');
                row.append('<td align="center" class="client-cell">' + campanas.area + '</td>');
                row.append('<td align="center" class="client-cell">' + fechaFormateada + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarVenCampana(\'modalcamven\',\'camvencbo\',\'idcampanaventas\',\'camvenNombre\',\'' +
                    campanas.id + '\', \'' + campanas.codC + '\')">Seleccionar</button>' +
                    '</td>');
                $('#bodycamven').append(row);
            });

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });
}


//Cargar la selección de CARGADOR
function seleccionarVenCampana(modalId, selectId, idcod, idtext, idCampana, codC) {
    event.preventDefault();
    var PredioIdInput = document.getElementById(idcod);
    PredioIdInput.value = idCampana;
    document.getElementById(idtext).innerText = codC;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var CargadorNombreSpan = document.getElementById('camvenNombre');
    var removeCargadorSpan = document.getElementById('removecamvenSpan');
    CargadorNombreSpan.innerText = codC;
    CargadorNombreSpan.style.display = 'inline-block';
    removeCargadorSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('camvencbo');
    selectElement.style.display = 'none';
    $.ajax({
        url: '/Tnst04CompEmitido/SelecCampana', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { idCampana: idCampana}, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            console.log(response);

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });

}

//El span CARGADOR para volver al select
function removeCamVen() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('camvenNombre').innerText = '';
    document.getElementById('idcampanaventas').value = '';
    document.getElementById('camvenNombre').style.display = 'none';
    document.getElementById('removecamvenSpan').style.display = 'none';
    var selectElement = document.getElementById('camvencbo');
    selectElement.style.display = 'inline-block';
    $.ajax({
        url: '/Tnst04CompEmitido/RemoveCampana', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function () {

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });
}

//empleado extraccion

//Proceso para cerrar la ventana modal CHOFER
$(document).ready(function () {
    $('#modalempven').on('hidden.bs.modal', function () {
        $('#empvencbo').prop('selectedIndex', 0);

        cargarDatosempven();
    });
    $('#modalempven').on('show.bs.modal', function () {

        cargarDatosempven();
    });
});



//CARGAR CHOFER EN TABLA CHOFER
function cargarDatosempven() {
    var idsempleado = arreglovenemp.map(function (campaña) {
        return campaña.idempleado;
    });
    $.ajax({
        url: '/Tnst04CompEmitido/Recargarempven', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { listaIds: idsempleado }, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodyempven').empty();
            $.each(response, function (index, EmpleadoVen) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + EmpleadoVen.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoVen.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoVen.cargo + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoVen.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoVen.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarempven(\'modalempven\',\'empvencbo\',\'empvenId\',\'empvenNombre\',\'' +
                    EmpleadoVen.idempleado + '\', \'' + EmpleadoVen.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');
                $('#bodyempven').append(row);
            });

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });
}

function seleccionarempven(modalId, selectId, idcod, idtext, idempven, nombreempven) {
    event.preventDefault();
    var choferIdInput = document.getElementById(idcod);
    choferIdInput.value = idempven;
    document.getElementById(idtext).innerText = nombreempven;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var choferNombreSpan = document.getElementById('empvenNombre');
    var removeChoferSpan = document.getElementById('removeempvenSpan');
    choferNombreSpan.innerText = nombreempven;
    choferNombreSpan.style.display = 'inline-block';
    removeChoferSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('empvencbo');
    selectElement.style.display = 'none';
    $.ajax({
        url: '/Tnst04CompEmitido/CargarDatosEmpleado',
        type: 'GET',
        data: { idEmpleado: idempven },
        success: function (data) {
            if (!data) {
                // Si data es null o undefined
                document.getElementById('nrodocempleadoven').value = "";
                document.getElementById('categoriaempleadoven').value = "";
                document.getElementById('condicionempleadoven').value = "";
                document.getElementById('celularempleadoven').value = "";
            } else {
                // Si data no es null o undefined
                document.getElementById('nrodocempleadoven').value = data.nrodoc || "";
                document.getElementById('categoriaempleadoven').value = data.cargo || "";
                document.getElementById('condicionempleadoven').value = data.condicion || "";
                document.getElementById('celularempleadoven').value =  data.celular || "";

            }
        },
        error: function (error) {
            console.error('Error al cargar datos del empleado:', error.responseText);

        }
    });

}

//El span cliente para volver al select
function removeempven() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('nrodocempleadoven').value = '';
    document.getElementById('categoriaempleadoven').value = '';
    document.getElementById('celularempleadoven').value = '';
    /*document.getElementById('salarioempleado').value = 1;*/
    document.getElementById('empvenId').value = 0;
    document.getElementById('condicionempleadoven').value = '';
    ;
    document.getElementById('empvenNombre').style.display = 'none';
    document.getElementById('removeempvenSpan').style.display = 'none';
    var selectElement = document.getElementById('empvencbo');
    selectElement.style.display = 'inline-block';
}

//AREGLO EXT EMP
var arreglovenemp = [];



function agregarvenemp() {
    var idempleado = parseInt($("#empvenId").val());
    /*var salario= parseFloat($("#salarioempleado").val());*/
    var nrodoc = ($("#nrodocempleadoven").val());
    var categoria = ($("#categoriaempleadoven").val());
    var condicion = ($("#condicionempleadoven").val());
    var celular = ($("#celularempleadoven").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idempleado) || idempleado <= 0 /*|| isNaN(salario) ||   0 >(salario)*/) {
        alert("Por favor, ingrese valores numéricos para la extracción por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }
    $.ajax({
        url: "/Tnst04CompEmitido/EmpleadoVen",
        type: "GET",
        data: { empleadoID: idempleado },
        success: function (response) {
            if (response != null) {


                arreglovenemp.push({
                    idempleado: idempleado,
                    txtempleado: response.txtnombre,
                    /*                    salario: salario,*/
                    celular:celular,
                    nrodoc: nrodoc,
                    categoria: categoria,
                    condicion: condicion
                });

                actualizartablavenemp();
                removeempven();


            } else {
                alert("Tipo de Arbol no encontrado");
            }
        },
        error: function (response) {
            alert("Error al obtener los datos del Tipo de Arbol");
        }
    });


}

// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarvenemp(idvenemp) {
    var campanavenemp = arreglovenemp.find(function (campTA) {
        return campTA.idempleado === idvenemp;
    });

    if (campanavenemp) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este registro de la lista?");

        if (confirmar) {
            var campTAVenindex = arreglovenemp.findIndex(function (campTA) {
                return campTA.idempleado === idvenemp;
            });

            if (campTAVenindex !== -1) {
                arreglovenemp.splice(campTAVenindex, 1);
                actualizartablavenemp();
            }
        }
    } else {
        alert("Registro no encontrado en la lista.");
    }
}

function actualizartablavenemp() {
    // Limpiar la tabla
    $("#empSeleccionadosVen").empty();

    // Recorrer el arreglo de tipo de árboles seleccionados y volver a agregarlos a la tabla
    arreglovenemp.forEach(function (Empleado) {
        var newRow = `
            <tr id='${Empleado.idempleado}'>
                <td class="text-center">${Empleado.txtempleado}</td>
                <td class="text-center">${Empleado.condicion}</td>
                <td class="text-center">${Empleado.celular}</td>
                <td class="text-center">${Empleado.nrodoc}</td>
                <td class="text-center">${Empleado.categoria}</td>
                <td colspan='2' align='center'>
                    
                    <button type='button' class="btn btn-danger btn-sm" onclick='eliminarvenemp(${Empleado.idempleado})'><i class="fas fa-trash alt"></i></button>
                </td>
            </tr>`;

        $("#empSeleccionadosVen").append(newRow);
    });
    cargarDatosempven();

}
/*<button type='button' onclick='editarempleadosExtraccion(${Empleado.idempleado})'>Editar</button>*/


        
//BOTONES GUARDAR - GUARDAR Y CERRAR


function obtenerDatosDelComprobante() {
    

    var data = {

        /*NroCheque: null,*/
        IdCliente: parseInt(document.getElementById("clienteID").value),
        //IdEmpChofer: parseInt(document.getElementById("choferId").value),
        //IdEmpleado: parseInt(document.getElementById("OperarioId").value),
        //IdEmpAutorizador: parseInt(document.getElementById("AutorizadorId").value),
        //IdEmpcortador: parseInt(document.getElementById("CortadorId").value),
        //IdEmpaserradero: parseInt(document.getElementById("AserraderoId").value),
        //IdEmpunimoq: parseInt(document.getElementById("UnimoqId").value),
        //IdEmpcargador: parseInt(document.getElementById("CargadorId").value),
        /*CodCaja: null,*/
        //TxtSerie: document.getElementById("TxtSerie").value,
        //TxtNumero: document.getElementById("TxtNumero").value,
        //TxtSerieFe: null,
        //TxtNumeroFe: null,
        //FecNegocio: (document.getElementById("FecNegocio").value),
        //FecRegEmitido: (document.getElementById("FecRegEmitido").value),
        //FecRegistro: (document.getElementById("FecRegistro").value),
        FecEmi: (document.getElementById("fechaEmision").value),

        /*IdCanVta: null *//*parseInt(document.getElementById("CantVta").value)*//*,*/
        IdTipoOrden: parseInt(document.getElementById("TipoOrden").value),
        IdLocation: parseInt(document.getElementById('Locationid').value),
        IdLocationto: parseInt(document.getElementById('Locationtoid').value),
        TxtObserv: document.getElementById("comentarioven").value,
        /* MtoTcVta: parseFloat(document.getElementById("mto_tc_vta").value),*/
        MtoNeto: parseFloat(document.getElementById("neto").value),
        //MtoExonerado: 0.0,// Debe coincidir con el tipo de la propiedad
        //MtoNoAfecto: 0.0, // Debe coincidir con el tipo de la propiedad
        MtoDsctoTot: parseFloat(document.getElementById("desc").value),
        /*MtoServicio: 0.0, // Debe coincidir con el tipo de la propiedad*/
        MtoSubTot: parseFloat(document.getElementById("Mtoapdesc").value),
        MtoImptoTot: parseFloat(document.getElementById("igv").value),
        MtoTotComp: parseFloat(document.getElementById("total").value),
        //RefIdCompEmitido: null, // Debe coincidir con el tipo de la propiedad
        //RefTipoComprobante: null, // Debe coincidir con el tipo de la propiedad
        //RefFecha: null, // Debes asegurarte de que el valor sea una fecha en formato correcto
        //RefSerie: null, // Debe coincidir con el tipo de la propiedad
        //RefNumero: null, // Debe coincidir con el tipo de la propiedad
        //SnChkAbierto: false,
        //SnChkEnviado: false,
        /*TaxPor01: 0.18,*/
        //TaxPor02: null, // Debe coincidir con el tipo de la propiedad
        //TaxPor03: null, // Debe coincidir con el tipo de la propiedad
        //TaxPor04: null, // Debe coincidir con el tipo de la propiedad
        //TaxPor05: null, // Debe coincidir con el tipo de la propiedad
        //TaxPor06: null, // Debe coincidir con el tipo de la propiedad
        //TaxPor07: null, // Debe coincidir con el tipo de la propiedad
        //TaxPor08: null, // Debe coincidir con el tipo de la propiedad
        TaxMto01: parseFloat(document.getElementById("igv").value),
        //TaxMto02: null, // Debe coincidir con el tipo de la propiedad
        //TaxMto03: null, // Debe coincidir con el tipo de la propiedad
        //TaxMto04: null, // Debe coincidir con el tipo de la propiedad
        //TaxMto05: null, // Debe coincidir con el tipo de la propiedad
        //TaxMto06: null, // Debe coincidir con el tipo de la propiedad
        //TaxMto07: null, // Debe coincidir con el tipo de la propiedad
        //TaxMto08: null, // Debe coincidir con el tipo de la propiedad
        //Info01: null, // Debe coincidir con el tipo de la propiedad
        //Info02: null, // Debe coincidir con el tipo de la propiedad
        //Info03: null, // Debe coincidir con el tipo de la propiedad
        //Info04: null, // Debe coincidir con el tipo de la propiedad
        //Info05: null, // Debe coincidir con el tipo de la propiedad
        //Info06: null, // Debe coincidir con el tipo de la propiedad
        //Info07: null, // Debe coincidir con el tipo de la propiedad
        //Info08: null, // Debe coincidir con el tipo de la propiedad
        //Info09: null, // Debe coincidir con el tipo de la propiedad
        //Info10: null, // Debe coincidir con el tipo de la propiedad
        //InfoDate01: null, // Debe coincidir con el tipo de la propiedad y asegurarte de que el valor sea una fecha en formato correcto
        //InfoDate02: null, // Debe coincidir con el tipo de la propiedad y asegurarte de que el valor sea una fecha en formato correcto
        //InfoDate03: null, // Debe coincidir con el tipo de la propiedad y asegurarte de que el valor sea una fecha en formato correcto
        //InfoDate04: null, // Debe coincidir con el tipo de la propiedad y asegurarte de que el valor sea una fecha en formato correcto
        //InfoDate05: null, // Debe coincidir con el tipo de la propiedad y asegurarte de que el valor sea una fecha en formato correcto
        //InfoMto01: null, // Debe coincidir con el tipo de la propiedad
        //InfoMto02: null, // Debe coincidir con el tipo de la propiedad
        //InfoMto03: null, // Debe coincidir con el tipo de la propiedad
        //InfoMto04: null, // Debe coincidir con el tipo de la propiedad
        //InfoMto05: null, // Debe coincidir con el tipo de la propiedad
        //Post: null, // Debe coincidir con el tipo de la propiedad
        //PostDate: null, // Debes asegurarte de que el valor sea una fecha en formato correcto
        //NumComensales: null, // Debe coincidir con el tipo de la propiedad
        //IdMesa: null, // Debe coincidir con el tipo de la propiedad
        //IdTurno: null, // Debe coincidir con el tipo de la propiedad
    };
    return data;
}
//ELIMINAR PREDIO
$(document).ready(function () {
    $('#confirmarEliminarVenta').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var idPredio = button.data('id');
        $(this).data('id', idPredio);
    });
    $('#confirmarAnularVenta').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var idPredio = button.data('id');
        $(this).data('id', idPredio);
    });
});


function eliminarVenta() {

    var idVentas = $('#confirmarEliminarVenta').data('id');
    console.log('ID del predio a eliminar:', idVentas);
    $.ajax({
        url: '/Tnst04CompEmitido/EliminarVenta', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        data: { id: idVentas },
        success: function (response) {
            if (response.mensaje == null) {
                $('#confirmarEliminarVenta').modal('hide');

                window.location.href = response.redirectUrl;

            } else {
                

            }

        },
        error: function (error) {
            alert('Eliminación no exitosa');
            console.error('Error al intentar eliminar', error.responseText);
        }
    });
}
function anularventa() {

    var idVentas = $('#confirmarAnularVenta').data('id');
    console.log('ID del predio a eliminar:', idVentas);
    $.ajax({
        url: '/Tnst04CompEmitido/AnularVenta', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        data: { id: idVentas },
        success: function (response) {
            if (response.mensaje == null) {
                $('#confirmarAnularVenta').modal('hide');

                window.location.href = response.redirectUrl;

            } else {


            }

        },
        error: function (error) {
            alert('Eliminación no exitosa');
            console.error('Error al intentar eliminar', error.responseText);
        }
    });
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
        $.ajax({
            url: '/Tnst04CompEmitido/CerrarCampanaVen', // Reemplaza 'TuControlador' con el nombre real de tu controlador
            type: 'GET',
            success: function (response) {
                /*console.log('Cargador:', response);*/
                if (response.mensaje == null) {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.mensaje);
                }
            },
            error: function (error) {
                console.error('Error al cargar los datos del Cargador:', error.responseText);
            }
        });
    });
});
   
   
function enviarDatosGuardar(dataToSend) {
    if (arreglovenemp.length > 0) {
        if (ListadoProductosSeleccionados.length > 0) {
            var checkbox = document.getElementById('checkEstado');

            if (checkbox.checked) {
                var check = true;
            } else {
                var check = false;
            }
    var productosJson = JSON.stringify(ListadoProductosSeleccionados);
            var empleadosJson = JSON.stringify(arreglovenemp);
            if (camposRequeridosLlenos('FormVentas')) {
    console.log(productosJson);
    var url = '/Tnst04CompEmitido/Guardar?';
    //url += 'comprobanteId=' + dataToSend.IdCompEmitido;
    //url += '&NroCompEmitido=' + encodeURIComponent(dataToSend.NroCompEmitido);
    /*url += '&NroCheque=' + encodeURIComponent(dataToSend.NroCheque);*/
    /*url += '&IdTipoComp=' + dataToSend.IdTipoComp;*/
    url += '&IdCliente=' + dataToSend.IdCliente;
    //url += '&IdEmpChofer=' + dataToSend.IdEmpChofer;
    //url += '&IdEmpleado=' + dataToSend.IdEmpleado;
    //url += '&IdEmpAutorizador=' + dataToSend.IdEmpAutorizador;
    //url += '&IdEmpcortador=' + dataToSend.IdEmpcortador;
    //url += '&IdEmpaserradero=' + dataToSend.IdEmpaserradero;
    //url += '&IdEmpunimoq=' + dataToSend.IdEmpunimoq;
    //url += '&IdEmpcargador=' + dataToSend.IdEmpcargador;
   /* url += '&CodCaja=' + encodeURIComponent(dataToSend.CodCaja);*/
    //url += '&TxtSerie=' + encodeURIComponent(dataToSend.TxtSerie);
    //url += '&TxtNumero=' + encodeURIComponent(dataToSend.TxtNumero);
    //url += '&TxtSerieFe=' + encodeURIComponent(dataToSend.TxtSerieFe);
    //url += '&TxtNumeroFe=' + encodeURIComponent(dataToSend.TxtNumeroFe);
    //url += '&FecNegocio=' + encodeURIComponent(dataToSend.FecNegocio);
    //url += '&FecRegEmitido=' + encodeURIComponent(dataToSend.FecRegEmitido);
    //url += '&FecRegistro=' + encodeURIComponent(dataToSend.FecRegistro);
    url += '&FecEmi=' + encodeURIComponent(dataToSend.FecEmi);
    //url += '&FecVcto=' + encodeURIComponent(dataToSend.FecVcto);
    //url += '&FecCanc=' + encodeURIComponent(dataToSend.FecCanc);
    /*url += '&IdTipoMoneda=' + dataToSend.IdTipoMoneda;*/
    /*url += '&IdCanVta=' + dataToSend.IdCanVta;*/
    url += '&IdTipoOrden=' + dataToSend.IdTipoOrden;
    url += '&IdLocation=' + dataToSend.IdLocation;
    url += '&IdLocationto=' + dataToSend.IdLocationto;
    url += '&TxtObserv=' + encodeURIComponent(dataToSend.TxtObserv);
/*    url += '&MtoTcVta=' + dataToSend.MtoTcVta;*/
    url += '&MtoNeto=' + dataToSend.MtoNeto;
    //url += '&MtoExonerado=' + dataToSend.MtoExonerado;
    //url += '&MtoNoAfecto=' + dataToSend.MtoNoAfecto;
    url += '&MtoDsctoTot=' + dataToSend.MtoDsctoTot;
/*    url += '&MtoServicio=' + dataToSend.MtoServicio;*/
    url += '&MtoSubTot=' + dataToSend.MtoSubTot;
    url += '&MtoImptoTot=' + dataToSend.MtoImptoTot;
    url += '&MtoTotComp=' + dataToSend.MtoTotComp;
/*    url += '&RefIdCompEmitido=' + dataToSend.RefIdCompEmitido;*/
    //url += '&RefTipoComprobante=' + encodeURIComponent(dataToSend.RefTipoComprobante);
    //url += '&RefFecha=' + encodeURIComponent(dataToSend.RefFecha);
    //url += '&RefSerie=' + encodeURIComponent(dataToSend.RefSerie);
    //url += '&RefNumero=' + encodeURIComponent(dataToSend.RefNumero);
    //url += '&SnChkAbierto=' + dataToSend.SnChkAbierto;
    //url += '&SnChkEnviado=' + dataToSend.SnChkEnviado;
    //url += '&TaxPor01=' + dataToSend.TaxPor01;
    //url += '&TaxPor02=' + dataToSend.TaxPor02;
    //url += '&TaxPor03=' + dataToSend.TaxPor03;
    //url += '&TaxPor04=' + dataToSend.TaxPor04;
    //url += '&TaxPor05=' + dataToSend.TaxPor05;
    //url += '&TaxPor06=' + dataToSend.TaxPor06;
    //url += '&TaxPor07=' + dataToSend.TaxPor07;
    //url += '&TaxPor08=' + dataToSend.TaxPor08;
    url += '&TaxMto01=' + dataToSend.TaxMto01;
    //url += '&TaxMto02=' + dataToSend.TaxMto02;
    //url += '&TaxMto03=' + dataToSend.TaxMto03;
    //url += '&TaxMto04=' + dataToSend.TaxMto04;
    //url += '&TaxMto05=' + dataToSend.TaxMto05;
    //url += '&TaxMto06=' + dataToSend.TaxMto06;
    //url += '&TaxMto07=' + dataToSend.TaxMto07;
    //url += '&TaxMto08=' + dataToSend.TaxMto08;
    //url += '&Info01=' + encodeURIComponent(dataToSend.Info01);
    //url += '&Info02=' + encodeURIComponent(dataToSend.Info02);
    //url += '&Info03=' + encodeURIComponent(dataToSend.Info03);
    //url += '&Info04=' + encodeURIComponent(dataToSend.Info04);
    //url += '&Info05=' + encodeURIComponent(dataToSend.Info05);
    //url += '&Info06=' + encodeURIComponent(dataToSend.Info06);
    //url += '&Info07=' + encodeURIComponent(dataToSend.Info07);
    //url += '&Info08=' + encodeURIComponent(dataToSend.Info08);
    //url += '&Info09=' + encodeURIComponent(dataToSend.Info09);
    //url += '&Info10=' + encodeURIComponent(dataToSend.Info10);
    //url += '&InfoDate01=' + encodeURIComponent(dataToSend.InfoDate01);
    //url += '&InfoDate02=' + encodeURIComponent(dataToSend.InfoDate02);
    //url += '&InfoDate03=' + encodeURIComponent(dataToSend.InfoDate03);
    //url += '&InfoDate04=' + encodeURIComponent(dataToSend.InfoDate04);
    //url += '&InfoDate05=' + encodeURIComponent(dataToSend.InfoDate05);
    //url += '&InfoMto01=' + dataToSend.InfoMto01;
    //url += '&InfoMto02=' + dataToSend.InfoMto02;
    //url += '&InfoMto03=' + dataToSend.InfoMto03;
    //url += '&InfoMto04=' + dataToSend.InfoMto04;
    //url += '&InfoMto05=' + dataToSend.InfoMto05;
    //url += '&Post=' + encodeURIComponent(dataToSend.Post);
    //url += '&PostDate=' + encodeURIComponent(dataToSend.PostDate);
    //url += '&NumComensales=' + dataToSend.NumComensales;
    //url += '&IdUsuario=' + dataToSend.IdUsuario;
    //url += '&TxtUsuario=' + encodeURIComponent(dataToSend.TxtUsuario);
    //url += '&IdUsuarioModificador=' + dataToSend.IdUsuarioModificador;
    //url += '&TxtUsuarioModificador=' + encodeURIComponent(dataToSend.TxtUsuarioModificador);
    //url += '&FechaModificacion=' + encodeURIComponent(dataToSend.FechaModificacion);
    //url += '&IdMesa=' + dataToSend.IdMesa;
    //url += '&IdTurno=' + dataToSend.IdTurno;
    url += '&productosSeleccionado=' + encodeURIComponent(productosJson);
    url += '&empleadosSeleccionados=' + encodeURIComponent(empleadosJson);
    url += '&check=' + encodeURIComponent(check);


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
            $.ajax({
                url: '/Tnst04CompEmitido/CargarMood', // Reemplaza 'TuControlador' con el nombre real de tu controlador
                type: 'GET',
                success: function (response) {
                    if (response.id == 1) {
                        document.getElementById('mood').innerText = response.name;
                        // Supongamos que tienes un input checkbox con el id "miCheckbox"
                        $('#checkEstado').prop('checked', true);
                        $('#checkEstado').prop('disabled', false);

                    } else if (response.id == 3) {
                        document.getElementById('mood').innerText = response.name;
                        // Supongamos que tienes un input checkbox con el id "miCheckbox"
                        $('#checkEstado').prop('checked', true);
                        $('#checkEstado').prop('disabled', true);
                    }
                },
                error: function (error) {
                    console.error('Error al cargar los datos del Cargador:', error.responseText);
                }
            });
        }
    });
            } else {
                alert('Por favor, complete todos los campos requeridos.');
            }
        } else {
            alert('Registre al menos un tipo de arbol por extracción.')
        }
    } else {
        alert('Registre al menos un empleado.')
    }
}

function enviarDatosGuardarYCerrar(dataToSend) {
    if (arreglovenemp.length > 0) {
        if (ListadoProductosSeleccionados.length > 0) {
            var checkbox = document.getElementById('checkEstado');

            if (checkbox.checked) {
                var check = true;
            } else {
                var check = false;
            }
            var productosJson = JSON.stringify(ListadoProductosSeleccionados);
            var empleadosJson = JSON.stringify(arreglovenemp);
            if (camposRequeridosLlenos('FormVentas')) {
                console.log(productosJson);
                var url = '/Tnst04CompEmitido/GuardaryCerrar?';
                //url += 'comprobanteId=' + dataToSend.IdCompEmitido;
                //url += '&NroCompEmitido=' + encodeURIComponent(dataToSend.NroCompEmitido);
                /*url += '&NroCheque=' + encodeURIComponent(dataToSend.NroCheque);*/
                /*url += '&IdTipoComp=' + dataToSend.IdTipoComp;*/
                url += '&IdCliente=' + dataToSend.IdCliente;
                //url += '&IdEmpChofer=' + dataToSend.IdEmpChofer;
                //url += '&IdEmpleado=' + dataToSend.IdEmpleado;
                //url += '&IdEmpAutorizador=' + dataToSend.IdEmpAutorizador;
                //url += '&IdEmpcortador=' + dataToSend.IdEmpcortador;
                //url += '&IdEmpaserradero=' + dataToSend.IdEmpaserradero;
                //url += '&IdEmpunimoq=' + dataToSend.IdEmpunimoq;
                //url += '&IdEmpcargador=' + dataToSend.IdEmpcargador;
                /* url += '&CodCaja=' + encodeURIComponent(dataToSend.CodCaja);*/
                //url += '&TxtSerie=' + encodeURIComponent(dataToSend.TxtSerie);
                //url += '&TxtNumero=' + encodeURIComponent(dataToSend.TxtNumero);
                //url += '&TxtSerieFe=' + encodeURIComponent(dataToSend.TxtSerieFe);
                //url += '&TxtNumeroFe=' + encodeURIComponent(dataToSend.TxtNumeroFe);
                //url += '&FecNegocio=' + encodeURIComponent(dataToSend.FecNegocio);
                //url += '&FecRegEmitido=' + encodeURIComponent(dataToSend.FecRegEmitido);
                //url += '&FecRegistro=' + encodeURIComponent(dataToSend.FecRegistro);
                url += '&FecEmi=' + encodeURIComponent(dataToSend.FecEmi);
                //url += '&FecVcto=' + encodeURIComponent(dataToSend.FecVcto);
                //url += '&FecCanc=' + encodeURIComponent(dataToSend.FecCanc);
                /*url += '&IdTipoMoneda=' + dataToSend.IdTipoMoneda;*/
                /*url += '&IdCanVta=' + dataToSend.IdCanVta;*/
                url += '&IdTipoOrden=' + dataToSend.IdTipoOrden;
                url += '&IdLocation=' + dataToSend.IdLocation;
                url += '&IdLocationto=' + dataToSend.IdLocationto;
                url += '&TxtObserv=' + encodeURIComponent(dataToSend.TxtObserv);
                /*    url += '&MtoTcVta=' + dataToSend.MtoTcVta;*/
                url += '&MtoNeto=' + dataToSend.MtoNeto;
                //url += '&MtoExonerado=' + dataToSend.MtoExonerado;
                //url += '&MtoNoAfecto=' + dataToSend.MtoNoAfecto;
                url += '&MtoDsctoTot=' + dataToSend.MtoDsctoTot;
                /*    url += '&MtoServicio=' + dataToSend.MtoServicio;*/
                url += '&MtoSubTot=' + dataToSend.MtoSubTot;
                url += '&MtoImptoTot=' + dataToSend.MtoImptoTot;
                url += '&MtoTotComp=' + dataToSend.MtoTotComp;
                /*    url += '&RefIdCompEmitido=' + dataToSend.RefIdCompEmitido;*/
                //url += '&RefTipoComprobante=' + encodeURIComponent(dataToSend.RefTipoComprobante);
                //url += '&RefFecha=' + encodeURIComponent(dataToSend.RefFecha);
                //url += '&RefSerie=' + encodeURIComponent(dataToSend.RefSerie);
                //url += '&RefNumero=' + encodeURIComponent(dataToSend.RefNumero);
                //url += '&SnChkAbierto=' + dataToSend.SnChkAbierto;
                //url += '&SnChkEnviado=' + dataToSend.SnChkEnviado;
                //url += '&TaxPor01=' + dataToSend.TaxPor01;
                //url += '&TaxPor02=' + dataToSend.TaxPor02;
                //url += '&TaxPor03=' + dataToSend.TaxPor03;
                //url += '&TaxPor04=' + dataToSend.TaxPor04;
                //url += '&TaxPor05=' + dataToSend.TaxPor05;
                //url += '&TaxPor06=' + dataToSend.TaxPor06;
                //url += '&TaxPor07=' + dataToSend.TaxPor07;
                //url += '&TaxPor08=' + dataToSend.TaxPor08;
                url += '&TaxMto01=' + dataToSend.TaxMto01;
                //url += '&TaxMto02=' + dataToSend.TaxMto02;
                //url += '&TaxMto03=' + dataToSend.TaxMto03;
                //url += '&TaxMto04=' + dataToSend.TaxMto04;
                //url += '&TaxMto05=' + dataToSend.TaxMto05;
                //url += '&TaxMto06=' + dataToSend.TaxMto06;
                //url += '&TaxMto07=' + dataToSend.TaxMto07;
                //url += '&TaxMto08=' + dataToSend.TaxMto08;
                //url += '&Info01=' + encodeURIComponent(dataToSend.Info01);
                //url += '&Info02=' + encodeURIComponent(dataToSend.Info02);
                //url += '&Info03=' + encodeURIComponent(dataToSend.Info03);
                //url += '&Info04=' + encodeURIComponent(dataToSend.Info04);
                //url += '&Info05=' + encodeURIComponent(dataToSend.Info05);
                //url += '&Info06=' + encodeURIComponent(dataToSend.Info06);
                //url += '&Info07=' + encodeURIComponent(dataToSend.Info07);
                //url += '&Info08=' + encodeURIComponent(dataToSend.Info08);
                //url += '&Info09=' + encodeURIComponent(dataToSend.Info09);
                //url += '&Info10=' + encodeURIComponent(dataToSend.Info10);
                //url += '&InfoDate01=' + encodeURIComponent(dataToSend.InfoDate01);
                //url += '&InfoDate02=' + encodeURIComponent(dataToSend.InfoDate02);
                //url += '&InfoDate03=' + encodeURIComponent(dataToSend.InfoDate03);
                //url += '&InfoDate04=' + encodeURIComponent(dataToSend.InfoDate04);
                //url += '&InfoDate05=' + encodeURIComponent(dataToSend.InfoDate05);
                //url += '&InfoMto01=' + dataToSend.InfoMto01;
                //url += '&InfoMto02=' + dataToSend.InfoMto02;
                //url += '&InfoMto03=' + dataToSend.InfoMto03;
                //url += '&InfoMto04=' + dataToSend.InfoMto04;
                //url += '&InfoMto05=' + dataToSend.InfoMto05;
                //url += '&Post=' + encodeURIComponent(dataToSend.Post);
                //url += '&PostDate=' + encodeURIComponent(dataToSend.PostDate);
                //url += '&NumComensales=' + dataToSend.NumComensales;
                //url += '&IdUsuario=' + dataToSend.IdUsuario;
                //url += '&TxtUsuario=' + encodeURIComponent(dataToSend.TxtUsuario);
                //url += '&IdUsuarioModificador=' + dataToSend.IdUsuarioModificador;
                //url += '&TxtUsuarioModificador=' + encodeURIComponent(dataToSend.TxtUsuarioModificador);
                //url += '&FechaModificacion=' + encodeURIComponent(dataToSend.FechaModificacion);
                //url += '&IdMesa=' + dataToSend.IdMesa;
                //url += '&IdTurno=' + dataToSend.IdTurno;
                url += '&productosSeleccionado=' + encodeURIComponent(productosJson);
                url += '&empleadosSeleccionados=' + encodeURIComponent(empleadosJson);
                url += '&check=' + encodeURIComponent(check);
                var confirmar = confirm("¿Seguro que desea guardar y cerrar?");

                if (confirmar) {
                    $.ajax({
                        url: url,
                        type: 'POST',
                        contentType: 'application/json',
                        /*data: JSON.stringify(dataToSend),*/
                        success: function (response) {
                            if (response.mensaje == null) {
                                window.location.href = response.redirectUrl;
                            } else {
                                alert(response.mensaje);
                            }

                        }
                    });
                }
            } else {
                alert('Por favor, complete todos los campos requeridos.');
            }
        } else {
            alert('Registre al menos un tipo de arbol por extracción.')
        }
    } else {
        alert('Registre al menos un empleado.')
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

function returndatenowWithTime() {
    // Obtiene la fecha actual en UTC
    const fechaActualUTC = new Date();

    // Ajusta la fecha para la zona horaria de Perú (Lima) (UTC-5)
    const fechaActualLima = new Date(fechaActualUTC.getTime() - 5 * 60 * 60 * 1000);

    // Obtiene las partes de la fecha y hora
    const year = fechaActualLima.getUTCFullYear();
    const month = fechaActualLima.getUTCMonth() + 1; // Meses en JavaScript se cuentan desde 0
    const day = fechaActualLima.getUTCDate();
    const hours = fechaActualLima.getUTCHours();
    const minutes = fechaActualLima.getUTCMinutes();
    const seconds = fechaActualLima.getUTCSeconds();

    // Formatea la fecha y hora en el formato yyyy-MM-dd HH:mm:ss
    const fechaHoraHoyPeru = `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')} ${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;

    return fechaHoraHoyPeru;
}

//CAMPAÑA
// Declarar el arreglo para almacenar los CampañaTipoArbol
var CampanatipoArbol = [];


//BOTONES DE TODA LA VISTA CAMPAÑA I LISTADO CAMPAÑA
$(document).ready(function () {
    $("#guardarCamp").click(function () {
        enviarDatosCTAGuardar();
    });
    $("#guardarycerrarCamp").click(function () {
        enviarDatosCTAGuardarYCerrar();
    });
    $("#cancelarCamp").click(function () {
        $.ajax({
            url: "/Pret02Campana/CerrarCampana",
            type: "GET",
            success: function (response) {
                if (response.mensaje == null) {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.mensaje);
                }
            },
            error: function (response) {
            }
        });
    });
    $("#cancelarListCamp").click(function () {
        $.ajax({
            url: "/Pret02Campana/CerrarListCampana",
            type: "GET",
            success: function (response) {
                if (response.mensaje == null) {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.mensaje);
                }
            },
            error: function (response) {
            }
        });
    });
});

//INSERCION TIPOARBOL
$(document).ready(function () {
    $('#crearTipoArbolBtn').click(function () {//autoclick
        // Validar que los campos requeridos estén llenos
        if (camposRequeridosLlenos('createTipoArbolForm')) {
            // Crear un objeto FormData
            var formData = new FormData();

            /*Obtener los valores del formulario y agregarlos a FormData*/
            formData.append('txtdesc', $('#txtdesctar').val());

            // Realizar la solicitud AJAX al controlador
            $.ajax({
                url: '/Pret02Campana/CrearTipoArbol',
                type: 'POST',
                data: formData,
                processData: false,  // No procesar los datos (dejar que FormData lo haga)
                contentType: false,  // No establecer contentType (FormData lo establece correctamente)
                success: function (response) {
                    console.log('Localizaciónto creado correctamente:', response);
                    // Clear the existing tbody content

                    $('#createTipoArbolModal').modal('hide');
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

function seleccionarTA(modalId, selectId, idcod, idtext, idPro, nombrePro) {
    event.preventDefault();
    var ProductoIdInput = document.getElementById(idcod);
    ProductoIdInput.value = idPro;
    document.getElementById(idtext).innerText = nombrePro;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var ProductoNombreSpan = document.getElementById('TipoArbolNombre');
    var removeProductoSpan = document.getElementById('removeTASpan');
    ProductoNombreSpan.innerText = nombrePro;
    ProductoNombreSpan.style.display = 'inline-block';
    removeProductoSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('tarcbo');
    selectElement.style.display = 'none';
    document.getElementById('hectareastc').value = 0;
    document.getElementById('nroarboltc').value = 1;
    document.getElementById('areatc').value = 0;
    document.getElementById('latitudtc').value = '';
    document.getElementById('longitudtc').value = '';
}


// Función para añadir un producto a la lista de productos seleccionados
function agregarCampanata() {
    event.preventDefault()
    var idtipoarbol = parseInt($("#tipoarboltc").val());
    var numarbol = parseInt($("#nroarboltc").val());
    var area = parseFloat($("#areatc").val());
    var numhectareas = parseFloat($("#hectareastc").val());
    var latitud = ($("#latitudtc").val());
    var longitud = ($("#longitudtc").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idtipoarbol) || idtipoarbol <= 0 || isNaN(numarbol) || numarbol <= 0 || isNaN(area) || isNaN(numhectareas) || numhectareas <= 0 || area<= 0) {
        alert("Por favor, ingrese valores numéricos válidos para la campaña por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }

    $.ajax({
        url: "/Pret02Campana/TipoArbol",
        type: "GET",
        data: { idtipoarbol: idtipoarbol },
        success: function (response) {
            if (response != null) {
                

                CampanatipoArbol.push({
                    idTipoArbol: response.idTipoArbol,
                    nombreTipoarbol: response.nombretipoarbol,
                    numhectareas: numhectareas,
                    numarbol: numarbol,
                    area: area,
                    latitud: latitud,
                    longitud: longitud
                });

                actualizarTablaTipoArbolSeleccionados();
                removeTipoArbol();


            } else {
                alert("Tipo de Arbol no encontrado");
            }
        },
        error: function (response) {
            alert("Error al obtener los datos del Tipo de Arbol");
        }
    });

    return false;
}




//PREDIO
function cargarProvinciasYDistritosPredio(departamentoId, provinciaId, distritoId) {
    // Realiza una solicitud AJAX para obtener las provincias
    $('#departamentopredio').val(departamentoId);
    $.ajax({
        url: '/Pret01Predio/ObtenerProvincias', // Ajusta la URL según la estructura de tu aplicación
        type: 'GET',
        data: { departamentoId: departamentoId },
        success: function (data) {
            // Actualiza el contenido del select de provincia
            $('#provinciapredio').empty();
            $.each(data, function (index, item) {
                $('#provinciapredio').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
            });

            // Selecciona la provincia existente al editar
            $('#provinciapredio').val(provinciaId);

            // Llama al evento change para actualizar los distritos
            $('#provinciapredio').change();

        },
        error: function () {
            console.error('Error al obtener provincias.');
        }
    });

    $.ajax({
        url: '/Pret01Predio/ObtenerDistritos', // Ajusta la URL según la estructura de tu aplicación
        type: 'GET',
        data: { provinciaId: provinciaId },
        success: function (data) {
            // Actualiza el contenido del select de distrito
            $('#distritopredio').empty();
            $.each(data, function (index, item) {
                $('#distritopredio').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
            });

            $('#distritopredio').val(distritoId);

            // Llama al evento change para actualizar los distritos
            $('#distritopredio').change();
        },
        error: function () {
            console.error('Error al obtener distritos.');
        }
    });
}


//TIPOARBOL
//Proceso para cerrar la ventana modal LOCATIONTO
$(document).ready(function () {
    $('#modaltar').on('hidden.bs.modal', function () {
        cargarDatosTipoArbol();
        $('#tarcbo').prop('selectedIndex', 0);
    });
});

//Proceso para cerrar la ventana modal create TipoArbol
$(document).ready(function () {
    $('#createTipoArbolModal').on('hidden.bs.modal', function () {

        cargarDatosTipoArbol();
        $('#txtdesctar').val('');

    });
});





function removeTipoArbol() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('tipoarboltc').value = '';
    document.getElementById('hectareastc').value = 0;
    document.getElementById('nroarboltc').value = 1;
    document.getElementById('areatc').value = 0;
    document.getElementById('latitudtc').value = '';
    document.getElementById('longitudtc').value = '';
    document.getElementById('TipoArbolNombre').style.display = 'none';
    document.getElementById('removeTASpan').style.display = 'none';
    var selectElement = document.getElementById('tarcbo');
    selectElement.style.display = 'inline-block';
    cargarDatosTipoArbol();
}

function actualizarTablaTipoArbolSeleccionados() {
    // Limpiar la tabla
    $("#tipoArbolSeleccionados").empty();

    // Recorrer el arreglo de tipo de árboles seleccionados y volver a agregarlos a la tabla
    CampanatipoArbol.forEach(function (tipoArbol) {
        var newRow = `
            <tr  class="text-center" id='${tipoArbol.idTipoArbol}'>
                <td  class="text-center">${tipoArbol.nombreTipoarbol}</td>
                <td  class="text-center">${tipoArbol.numarbol}</td>
                <td  class="text-center">${tipoArbol.numhectareas}</td>
                <td  class="text-center">${tipoArbol.area}</td>
                <td  class="text-center">${tipoArbol.latitud}</td>
                <td  class="text-center">${tipoArbol.longitud}</td>
                <td colspan='2'  class="text-center">
                    <button class="btn btn-primary btn-sm" type='button' onclick='editarTipoArbol(${tipoArbol.idTipoArbol})'> <i class="fas fa-edit" ></i>  </button>
                    <button  class="btn btn-danger btn-sm" type='button' onclick='eliminarTipoArbol(${tipoArbol.idTipoArbol})'> <i class="fas fa-trash alt"></i> </button>
                </td>
            </tr>`;

        $("#tipoArbolSeleccionados").append(newRow);
    });

    // Cargar datos de tipo de árbol (asumiendo que esta función se encarga de cargar esos datos)
    cargarDatosTipoArbol();
    actualizarTotalesTA();
    // Puedes agregar lógica adicional aquí según tus necesidades
}

function cargarDatosTipoArbol() {
    var TipoDeArbolIDs = CampanatipoArbol.map(function (tipoarbol) {
        return tipoarbol.idTipoArbol;
    });
    $.ajax({
        url: '/Pret02Campana/RecargarTipoDeArbol', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { tipoarbolSeleccionadosIds: TipoDeArbolIDs }, // Pasar los IDs como parámetro
        traditional: true, // Esto es importante para que los parámetros se pasen como una lista
        success: function (response) {
            // Clear the existing tbody content
            $('#bodyTipoArbol').empty();

            // Iterate through the updated tipo de arbol data and append rows to tbody
            $.each(response, function (index, tipoArbol) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + tipoArbol.txtDesc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarTA(\'modaltar\',\'tarcbo\',\'tipoarboltc\',\'TipoArbolNombre\',\'' +
                    tipoArbol.idTipoarbol + '\', \'' + tipoArbol.txtDesc + '\')">Seleccionar</button>' +
                    '</td>');

                // Append the row to the tbody
                $('#bodyTipoArbol').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del tipo de árbol:', error.responseText);
        }
    });
}


function actualizarTotalesTA() {
    let Mtototalnroarbol = 0;
    let Mtototalnrohectareas= 0;
    let Mtototalarea= 0;



    for (let i = 0; i < CampanatipoArbol.length; i++) {
        const totalnroarbol = parseFloat(CampanatipoArbol[i].numarbol);
        const totalnrohectareas= Number(CampanatipoArbol[i].numhectareas);
        const totalarea= Number(CampanatipoArbol[i].area);


        Mtototalnroarbol += totalnroarbol;
        Mtototalnrohectareas+= totalnrohectareas;
        Mtototalarea+= totalarea;

    }

    // Actualizar los campos en el documento HTML
    // Actualizar los campos en el documento HTML con dos decimales
    document.getElementById("nroarbolcampana").value = Mtototalnroarbol;
    document.getElementById("nrohectareacampana").value = Mtototalnrohectareas;
    document.getElementById("areacampana").value = Mtototalarea;
}
// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarTipoArbol(idTipoArbol) {
    var campanaTA = CampanatipoArbol.find(function (campTA) {
        return campTA.idTipoArbol === idTipoArbol;
    });

    if (campanaTA) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este registro de la lista?");

        if (confirmar) {
            var campTAindex = CampanatipoArbol.findIndex(function (campTA) {
                return campTA.idTipoArbol === idTipoArbol;
            });

            if (campTAindex !== -1) {
                CampanatipoArbol.splice(campTAindex, 1);
                actualizarTablaTipoArbolSeleccionados();
            }
        }
    } else {
        alert("Registro no encontrado en la lista.");
    }
}


//Función para abrir el modal de edición
function editarTipoArbol(idTipoArbol) {
    var campanatipoarbol = CampanatipoArbol.find(function (campTA) {
        return campTA.idTipoArbol === idTipoArbol;
    });
    if (campanatipoarbol) {
        document.getElementById("nuevonroarbolTA").value = campanatipoarbol.numarbol;
        document.getElementById("IdEditarTA").value = idTipoArbol;
        document.getElementById("TAinput").value = campanatipoarbol.nombreTipoarbol;
        document.getElementById("nuevoAreaTA").value = campanatipoarbol.area;
        document.getElementById("nuevaLongitudTA").value = campanatipoarbol.longitud;
        document.getElementById("nuevaLatitudTA").value = campanatipoarbol.latitud;
        document.getElementById("nuevanrohectareasTA").value = campanatipoarbol.numhectareas;
        openSecondModal('editarTAModal');
    } else {
        alert("Producto no encontrado en la lista.");
    }
}

// Función para aplicar los cambios desde el modal y guardarlos
function aplicarCambiosTA() {
    var idcampTA = parseInt(document.getElementById("IdEditarTA").value);
    var campanaTA = CampanatipoArbol.find(function (prod) {
        return prod.idTipoArbol === idcampTA;
    });
    var nuevanroarbol = parseInt(document.getElementById("nuevonroarbolTA").value);
    var nuevoArea = parseFloat(document.getElementById("nuevoAreaTA").value);
    var nuevaLatitud = (document.getElementById("nuevaLatitudTA").value);
    var nuevaLongitud = (document.getElementById("nuevaLongitudTA").value);
    var nuevanrohectareas= (document.getElementById("nuevanrohectareasTA").value);

    if (!(isNaN(nuevanroarbol)) || !(nuevanroarbol <= 0) || !(isNaN(nuevoArea)) || !(isNaN(nuevanrohectareas)) || !(nuevanrohectareas <= 0 )|| !(nuevoArea<= 0)) {
        var confirmar = confirm("¿Deseas aplicar los cambios y guardarlos?");

        if (confirmar) {
            // Aplicar parseFloat con toFixed(2) a las variables
            var nroarbol = parseInt(nuevanroarbol);
            var area = parseFloat(nuevoArea);
            var Latitud= (nuevaLatitud);
            var Longitud=(nuevaLongitud);
            var nrohectareas= Number(nuevanrohectareas);


            // Asignar los valores a las propiedades del objeto producto
            campanaTA.numarbol = nroarbol;
            campanaTA.area = area;
            campanaTA.latitud = Latitud;
            campanaTA.longitud= Longitud;
            campanaTA.numhectareas= nrohectareas;


            actualizarTablaTipoArbolSeleccionados();
            cerrarModal('editarTAModal');
        } else {
            alert("Edición cancelada. Los cambios no se aplicaron.");
        }
    } else {
        alert("Por favor, ingresa valores numéricos válidos para los campos.");
    }
}




//Crear Campaña
function enviarDatosCTAGuardar() {
    if (CampanatipoArbol.length > 0) {
            console.log(CampanatipoArbol);
            var campanaTA = JSON.stringify(CampanatipoArbol);
            console.log(campanaTA);
            if (camposRequeridosLlenos('FormCampana')) {
                var data = {
                    IdTipoCampana: ($('#IdTipoCampana').val()),
                    NroHectarea: parseInt($('#nrohectareacampana').val()),
                    NroArboles: parseInt($('#nroarbolcampana').val()),
                    Area: parseFloat($('#areacampana').val()),
                    Latitud: ($('#latitudcampana').val()),
                    Longitud: ($('#longitudcampana').val()),
                    FechaInicio: $('#fechainiciocampana').val(),
                    campanatipoarbolSeleccionado: campanaTA, // <-- Este es el campo que se enviará
                };


                $.ajax({
                    url: '/Pret02Campana/CrearCampana', // Ajusta la URL según la estructura de tu aplicación
                    type: 'POST',
                    data: data,
                    dataType: 'json',
                    success: function (response) {
                        if (response.mensaje == null) {
                            window.location.href = response.redirectUrl;
                        } else {
                            alert(response.mensaje);
                        }
                    },
                    error: function (error) {
                        console.error('Error al crear la campaña:', error.responseText);
                    }
                });
            } else {
                alert('Por favor, complete todos los campos requeridos.');
            }

    } else {
        alert('Registre al menos una campaña por tipo de arbol.')
    }
}

function enviarDatosCTAGuardarYCerrar() {
    
    if (CampanatipoArbol.length > 0) {
        var confirmar = confirm("¿Seguro que desea guardar y cerrar?");

        if (confirmar) {

            var campanaTA = JSON.stringify(CampanatipoArbol);

            if (camposRequeridosLlenos('FormCampana')) {
                var data = {
                    IdTipoCampana: ($('#IdTipoCampana').val()),
                    NroHectarea: parseInt($('#nrohectareacampana').val()),
                    NroArboles: parseInt($('#nroarbolcampana').val()),
                    Area: parseFloat($('#areacampana').val()),
                    Latitud: ($('#latitudcampana').val()),
                    Longitud: ($('#longitudcampana').val()),
                    FechaInicio: $('#fechainiciocampana').val(),
                    campanatipoarbolSeleccionado: campanaTA, // <-- Este es el campo que se enviará
                };

                $.ajax({
                    url: '/Pret02Campana/CrearCampanayCerrar', // Ajusta la URL según la estructura de tu aplicación
                    type: 'POST',
                    data: data,
                    dataType: 'json',
                    success: function (response) {
                        if (response.mensaje == null) {
                            window.location.href = response.redirectUrl;
                        } else {
                            alert(response.mensaje);
                        }
                    },
                    error: function (error) {
                        console.error('Error al crear la campaña:', error.responseText);
                    }
                });
            } else {
                alert('Por favor, complete todos los campos requeridos.');
            }
        }
    } else {
        alert('Registre al menos una campaña por tipo de arbol.')
    }
}

var archivos = [];

function abrirModalArchivos() {
    // Limpiar la tabla del modal y mostrar los archivos actuales
    mostrarArchivosEnTabla();
    // Mostrar el modal
    var nuevoArchivoInput = document.getElementById('nuevoArchivo');
    nuevoArchivoInput.value = '';
    $('#modalArchivos').modal('show');

}

function mostrarArchivosEnTabla() {
    var tablaArchivosBody = document.getElementById('tablaArchivosBody');
    tablaArchivosBody.innerHTML = '';

    for (var i = 0; i < archivos.length; i++) {
        agregarFilaATabla(archivos[i]);
    }
}

function agregarArchivo() {
    var nuevoArchivoInput = document.getElementById('nuevoArchivo');
    var archivosNuevos = nuevoArchivoInput.files;

    if (archivosNuevos.length == 0) {
        alert('Selecciona algún archivo');
    } else { 

    // Añadir cada nuevo archivo a la tabla
    for (var i = 0; i < archivosNuevos.length; i++) {
        var archivo = archivosNuevos[i];
        agregarFilaATabla(archivo);
        archivos.push(archivo);
    }

    // Limpiar el campo de archivos para permitir agregar otro
    nuevoArchivoInput.value = '';
}
}

function agregarFilaATabla(archivo) {
    var tablaArchivosBody = document.getElementById('tablaArchivosBody');
    var fila = document.createElement('tr');

    var celdaArchivo = document.createElement('td');
    var enlaceArchivo = document.createElement('a');
    enlaceArchivo.textContent = archivo.name;

    // Verificar si el archivo es comprimido (RAR)
    if (archivo.name.toLowerCase().endsWith('.rar')) {
        enlaceArchivo.href = '#'; // Puedes proporcionar un enlace válido si tienes una URL de descarga
        enlaceArchivo.onclick = function () {
            descargarArchivo(archivo);
        };
    } else {
        enlaceArchivo.href = URL.createObjectURL(archivo);
        enlaceArchivo.target = '_blank';
    }

    celdaArchivo.appendChild(enlaceArchivo);

    // Obtener la extensión del archivo
    var extension = obtenerExtension(archivo.name);
    var celdaExtension = document.createElement('td');
    celdaExtension.textContent = extension;

    var celdaAcciones = document.createElement('td');
    var botonEliminar = document.createElement('button');
    botonEliminar.className = 'btn btn-danger btn-sm';
    botonEliminar.textContent = 'Eliminar';
    botonEliminar.onclick = function () {
        eliminarArchivo(archivo);
    };

    celdaAcciones.appendChild(botonEliminar);

    fila.appendChild(celdaArchivo);
    fila.appendChild(celdaExtension);
    fila.appendChild(celdaAcciones);

    tablaArchivosBody.appendChild(fila);
}

function descargarArchivo(archivo) {
    // Aquí debes proporcionar la lógica para la descarga del archivo, por ejemplo, redirigiendo a una URL válida de descarga.
    // Puedes utilizar window.location.href para cambiar la ubicación actual a la URL de descarga.
    // Ejemplo: window.location.href = 'ruta/descargar.php?archivo=' + encodeURIComponent(archivo.name);
    alert('Descargando: ' + archivo.name);
}



// Función para obtener la extensión de un archivo a partir de su nombre
function obtenerExtension(nombreArchivo) {
    var partes = nombreArchivo.split('.');
    if (partes.length > 1) {
        return partes[partes.length - 1];
    } else {
        return '';
    }
}

function eliminarArchivo(archivo) {
    // Elimina el archivo de la tabla
    var index = archivos.indexOf(archivo);
    if (index !== -1) {
        archivos.splice(index, 1);
        // Actualiza la tabla en el modal
        mostrarArchivosEnTabla();
    }
}
function vaciarArregloYActualizarTabla() {
    archivos = []; // Vaciar el arreglo de archivos
    abrirModalArchivos(); // Actualizar la tabla en el modal
}

function base64toBlob(base64, type) {
    var byteCharacters = atob(base64);
    var byteNumbers = new Array(byteCharacters.length);
    for (var i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    var byteArray = new Uint8Array(byteNumbers);
    return new Blob([byteArray], { type: type });
}


//CAMPAÑAEXTRACCION

// Proceso para cerrar la ventana modal Campaña Extraccion
$(document).ready(function () {
    $('#modalcampext').on('hidden.bs.modal', function () {
        $('#campextcbo').prop('selectedIndex', 0);
        cargarDatoscampext();
    });


        

});

//Funcion general x1000
function multiplicarPorMil(input1, input2) {
    var valorInput1 = document.getElementById(input1).value;

    if (valorInput1 !== "" && !isNaN(valorInput1) && valorInput1 >0) {
        // Convertir el valor a número y multiplicar por mil
        var resultado = Number(valorInput1) * 10000;

        // Establecer el resultado en el segundo input (readonly)
        document.getElementById(input2).value = resultado;
    } else if(valorInput1<0){
        document.getElementById(input1).value = 0;
        document.getElementById(input2).value = 0;
    } else {
        // Si el input está vacío o no es un número, establecer el segundo input como 0
        document.getElementById(input2).value = 0;
    }
}


// CARGAR INVERSIONISTA EN TABLA INVERSIONISTA
function cargarDatoscampext() {
    $.ajax({
        url: '/Pret07Extraccion/Recargarcampext',
        type: 'GET',
        success: function (response) {
            // Limpia el tbody actual
            $('#bodycampext').empty();
            $.each(response, function (index, campanaext) {
                var fechaFormateada = new Date(campanaext.fechaini).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });

                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + campanaext.codigo + '</td>');
                row.append('<td align="center" class="client-cell">' + campanaext.nrositio + '</td>');
                row.append('<td align="center" class="client-cell">' + fechaFormateada + '</td>');

                var button = $('<button>').addClass('btn btn-primary')
                    .text('Seleccionar')
                    .on('click', function () {
                        seleccionarcampext('modalcampext', 'campextcbo', campanaext.id, campanaext.codigo);
                    });

                row.append($('<td>').addClass('client-cell text-center').append(button));

                // Append the row to the tbody
                $('#bodycampext').append(row);

            });

        },

        error: function (error) {
            console.error('Error al cargar los datos de la campaña:', error.responseText);
        }
    });

}


// Cargar la selección de Inversionista
function seleccionarcampext(modalId, selectId, idcampext, nombrecampext) {
    event.preventDefault();
    console.log(idcampext);
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del inversionista y el botón para remover
    var inversionistaNombreSpan = document.getElementById('campextNombre');
    var removeInversionistaSpan = document.getElementById('removeCampextSpan');
    inversionistaNombreSpan.innerText = nombrecampext;
    inversionistaNombreSpan.style.display = 'inline-block';
    removeInversionistaSpan.style.display = 'inline-block';
    // Ocultar el seleCAMPEXT
    var selectElement = document.getElementById('campextcbo');
    selectElement.style.display = 'none';
    document.getElementById('nrotrozasta').value = 1;
    document.getElementById('alturata').value = 1;
    document.getElementById('diametrota').value = 1;
    document.getElementById('comentariota').value = '';
    // Función para seleccionar la campaña de extracción
    function seleccionarCampExt(idcampext) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Pret07Extraccion/SeleccionarCampExt',
                type: 'GET',
                data: { idtCampana: idcampext },
                success: function (response) {
                    var idtemporalCampana = response;
                    console.log('Valor de idtemporalCampana:', idtemporalCampana);
                    resolve(idtemporalCampana);
                },
                error: function (error) {
                    console.error('Error al cargar los datos de la campaña de extracción:', error.responseText);
                    reject(error);
                }
            });
        });
    }
    cargarDatoscampext();
    var idsCampañas = CampanatipoArbolExt.map(function (campaña) {
        return campaña.idtipoarbol;
    });
    // Función para obtener las campañas
    function obtenerCampañas() {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Pret07Extraccion/ObtenerCampanas',
                type: 'GET',
                data: { idsCampanas: null }, // Pasar la lista de IDs de campañas al servidor
                success: function (data) {
                    $('#campanataselect').empty();

                    // Seleccionar automáticamente el primer elemento
                    var primerElemento = null;

                    $.each(data, function (index, item) {
                        if (item.nroarbol === 0) {
                        } else {
                            // Si nroarbol no es 0, agregarlo al select principal
                            $('#campanataselect').append($('<option>').text(item.txtdesc).attr('value', item.idta));

                            if (index === 0) {
                                primerElemento = item.idta;
                            }
                        }
                    });

                    // Si hay más de un elemento en el select principal, activar el evento change
                    if (data.length > 1) {
                        $('#campanataselect').change(function () {
                            var taId = $(this).val();
                            cargarDatosCampana(taId);
                        });
                    }

                    // Ejecutar la carga de datos de la campaña
                    cargarDatosCampana(primerElemento)
                        .then(resolve)  // Resuelve la promesa de obtenerCampañas
                        .catch(reject);  // Rechaza la promesa de obtenerCampañas en caso de error
                },
                error: function (error) {
                    console.error('Error al obtener campañas:', error.responseText);
                    reject(error);
                }
            });
        });
    }

    // Función para cargar datos de campaña
    function cargarDatosCampana(taId) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Pret07Extraccion/CargarDatosCampana',
                type: 'GET',
                data: { taid: taId },
                success: function (data) {
                    if (data == null) {
                        document.getElementById('nombreta').value = "";
                        document.getElementById('nroarbolta').value = "";
                    } else {
                        document.getElementById('nombreta').value = data.txtdesc;
                        document.getElementById('nroarbolta').value = data.nroarbol;
                        document.getElementById('nroarbolta').setAttribute("max", data.nroarbol);

                        resolve(data);
                    }
                },
                error: function (error) {
                    console.error('Error al cargar campaña tipo arbol:', error.responseText);
                    reject(error);
                }
            });
        });
    }

    // Lógica principal
    seleccionarCampExt(idcampext)
        .then(obtenerCampañas)
        .catch(function (error) {
            console.error('Error:', error);
        });




}

// El span inversionista para volver al select
function removecampext() {
    event.preventDefault();
    // Quitar el valor del inversionista y ocultar el span
    document.getElementById('campextNombre').innerText = '';
    document.getElementById('campextNombre').style.display = 'none';
    document.getElementById('removeCampextSpan').style.display = 'none';

    var selectElement = document.getElementById('campextcbo');
    selectElement.style.display = 'inline-block';
    document.getElementById('nombreta').value = '';
    document.getElementById('nroarbolta').value = '';
    $('#campanataselect').empty();
    CampanatipoArbolExt.splice(0, CampanatipoArbolExt.length);

    actualizarTablaTipoArbolSeleccionadosExt();
    $.ajax({
        url: "/Pret07Extraccion/removeIdTemporalCampana",
        type: "GET",
        success: function (response) {
            if (response.mensaje == null) {
            } else {
                alert(response.mensaje);
            }
        },
        error: function (response) {
        }
    });
    cargarDatoscampext();
}




//CAMPAÑA
// Declarar el arreglo para almacenar los CampañaTipoArbol
var CampanatipoArbolExt = [];

$(document).ready(function () {
    $('#confirmarEliminarEnvio').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var idEnvio = button.data('id');
        $(this).data('id', idEnvio);
    });
    $('#confirmarAnularEnvio').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var idEnvio = button.data('id');
        $(this).data('id', idEnvio);
    });
});


function eliminarEnvio() {

    var idEnvio = $('#confirmarEliminarEnvio').data('id');
    console.log('ID del predio a eliminar:', idEnvio);
    $.ajax({
        url: '/Pret10Envio/EliminarEnvio', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        data: { id: idEnvio },
        success: function (response) {
            if (response.mensaje == null) {
                $('#confirmarEliminarEnvio').modal('hide');

                window.location.href = response.redirectUrl;

            } else {


            }
        },
        error: function (error) {
            console.error('Error al intentar eliminar', error.responseText);
        }
    });
}

function anularenvio() {

    var idEnvio = $('#confirmarAnularEnvio').data('id');
    console.log('ID del predio a eliminar:', idEnvio);
    $.ajax({
        url: '/Pret10Envio/AnularEnvio', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        data: { id: idEnvio },
        success: function (response) {
            if (response.mensaje == null) {
                $('#confirmarAnularEnvio').modal('hide');

                window.location.href = response.redirectUrl;

            } else {


            }
        },
        error: function (error) {
            console.error('Error al intentar eliminar', error.responseText);
        }
    });
}
//BOTONES DE TODA LA VISTA CAMPAÑA I LISTADO CAMPAÑA
$(document).ready(function () {
    $("#guardarExt").click(function () {
        enviarDatosExtGuardar();
    });
    $("#guardarycerrarExt").click(function () {
        enviarDatosExtGuardaryCerrar();
    });
    $("#cancelarExt").click(function () {
        $.ajax({
            url: "/Pret07Extraccion/CerrarCampanaExt",
            type: "GET",
            success: function (response) {
                if (response.mensaje == null) {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.mensaje);
                }
            },
            error: function (response) {
            }
        });
    });
 
    $("#cancelarEnv").click(function () {
        $.ajax({
            url: "/Pret10Envio/CerrarEnvio",
            type: "GET",
            success: function (response) {
                if (response.mensaje == null) {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.mensaje);
                }
            },
            error: function (response) {
            }
        });
    });
    $("#cancelarRec").click(function () {
        $.ajax({
            url: "/Pret11Recepcion/CerrarRecepcion",
            type: "GET",
            success: function (response) {
                if (response.mensaje == null) {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.mensaje);
                }
            },
            error: function (response) {
            }
        });
    });
    
});

////INSERCION TIPOARBOL
//$(document).ready(function () {
//    $('#crearTipoArbolBtn').click(function () {//autoclick
//        // Validar que los campos requeridos estén llenos
//        if (camposRequeridosLlenos('createTipoArbolForm')) {
//            // Crear un objeto FormData
//            var formData = new FormData();

//            /*Obtener los valores del formulario y agregarlos a FormData*/
//            formData.append('txtdesc', $('#txtdesctar').val());

//            // Realizar la solicitud AJAX al controlador
//            $.ajax({
//                url: '/Pret02Campana/CrearTipoArbol',
//                type: 'POST',
//                data: formData,
//                processData: false,  // No procesar los datos (dejar que FormData lo haga)
//                contentType: false,  // No establecer contentType (FormData lo establece correctamente)
//                success: function (response) {
//                    console.log('Localizaciónto creado correctamente:', response);
//                    // Clear the existing tbody content

//                    $('#createTipoArbolModal').modal('hide');
//                },
//                error: function (error) {
//                    console.error('Error al crear la Localizaciónto:', error);
//                    // Aquí puedes manejar errores si es necesario
//                }
//            });
//        } else {
//            alert('Por favor, complete todos los campos requeridos.');
//        }
//    });
//});


function cargarDatosTipoArbolExt() {

    document.getElementById('nrotrozasta').value = 1;
    document.getElementById('alturata').value = 1;
    document.getElementById('diametrota').value = 1;
    document.getElementById('comentariota').value = '';
    function seleccionarCampExt() {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Pret07Extraccion/SeleccionarCampExt',
                type: 'GET',
                data: {
                    idtCampana: null
                },
                success: function (response) {
                    var idtemporalCampana = response;
                    console.log('Valor de idtemporalCampana:', idtemporalCampana);
                    resolve(idtemporalCampana);
                },
                error: function (error) {
                    console.error('Error al cargar los datos de la campaña de extracción:', error.responseText);
                    reject(error);
                }
            });
        });
    }
    var idsarreglocampana = CampanatipoArbolExt.map(function (campana) {
        return {
            idTipoArbol: campana.idTipoArbol,
            nroArboles: campana.numarbol
        };
    });
    console.log(idsarreglocampana)

    // Función para obtener las campañas
    function obtenerCampañas(idsarreglocampana) {
        $.ajax({
            url: '/Pret07Extraccion/ObtenerCampanas',
            type: 'GET',
            data: { idsCampanasJson: JSON.stringify(idsarreglocampana) },

            traditional: true,  // Añade esto para indicar que se enviará una lista tradicional de parámetros
            success: function (data) {
                $('#campanataselect').empty();

                // Seleccionar automáticamente el primer elemento
                var primerElemento = null;

                $.each(data, function (index, item) {
                    if (item.nroarbol == 0) {
                    } else {
                        // Si nroarbol no es 0, agregarlo al select principal
                        $('#campanataselect').append($('<option>').text(item.txtdesc).attr('value', item.idta));

                        if (index == 0) {
                            primerElemento = item.idta;
                        }
                    }
                });

                // Si hay más de un elemento en el select principal, activar el evento change
                if (data.length > 1) {
                    $('#campanataselect').change(function () {
                        var taId = $(this).val();
                        cargarDatosCampana(taId);
                    });
                }

                // Ejecutar la carga de datos de la campaña
                cargarDatosCampana(primerElemento)
            },
            error: function (error) {
                console.error('Error al obtener campañas:', error.responseText);
            }
        });

    }
 
    // Función para cargar datos de campaña
    function cargarDatosCampana(taId) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Pret07Extraccion/CargarDatosCampana',
                type: 'GET',
                data: { taid: taId },
                success: function (data) {
                    if (data == null) {
                        document.getElementById('nombreta').value = "";
                        document.getElementById('nroarbolta').value = "";
                        document.getElementById('nuevonroarbolTAExt').value = "";
                    } else {
                        document.getElementById('nombreta').value = data.txtdesc;
                        document.getElementById('nroarbolta').value = data.nroarbol;
                        document.getElementById('nroarbolta').setAttribute("max", data.nroarbol);
                        document.getElementById('nuevonroarbolTAExt').value = data.nroarbol;
                        document.getElementById('nuevonroarbolTAExt').setAttribute("max", data.nroarbol);

                        resolve(data);
                    }
                },
                error: function (error) {
                    console.error('Error al cargar campaña tipo arbol:', error.responseText);
                    reject(error);
                }
            });
        });
    }

    seleccionarCampExt().then(function () {
        obtenerCampañas(idsarreglocampana);
    }).catch(function (error) {
        console.error('Error:', error);
    });


}

function actualizarTablaTipoArbolSeleccionadosExt() {
    // Limpiar la tabla
    $("#tipoArbolSeleccionadosExt").empty();

    // Recorrer el arreglo de tipo de árboles seleccionados y volver a agregarlos a la tabla
    CampanatipoArbolExt.forEach(function (tipoArbol) {
        var newRow = `
            <tr class="text-center" id='${tipoArbol.idunico}'>
                <td class="text-center">${tipoArbol.nombreTipoarbol}</td>
                <td class="text-center">${tipoArbol.numarbol}</td>
                <td class="text-center">${tipoArbol.altura}</td>
                <td class="text-center">${tipoArbol.diametro}</td>
                <td class="text-center">${tipoArbol.nrotrozas}</td>
                <td class="text-center">${tipoArbol.comentario}</td>
                <td colspan='2' class="text-center">
                    <button class="btn btn-primary btn-sm" type='button' onclick='editarTipoArbolExt("${tipoArbol.idunico}")'><i class="fas fa-edit" ></i></button>
                    <button  class="btn btn-danger btn-sm" type='button' onclick='eliminarTipoArbolExt("${tipoArbol.idunico}")'><i class="fas fa-trash alt"></i></button>
                </td>
            </tr>`;

        $("#tipoArbolSeleccionadosExt").append(newRow);
    });

    // Cargar datos de tipo de árbol (asumiendo que esta función se encarga de cargar esos datos)
    cargarDatosTipoArbolExt();
    actualizarTotalesTAExt();
    //function seleccionarCampExt() {
    //    return new Promise(function (resolve, reject) {
    //        $.ajax({
    //            url: '/Pret07Extraccion/SeleccionarCampExt',
    //            type: 'GET',
    //            data: { idtCampana: null},
    //            success: function (response) {
    //                var idtemporalCampana = response;
    //                console.log('Valor de idtemporalCampana:', idtemporalCampana);
    //                resolve(idtemporalCampana);
    //            },
    //            error: function (error) {
    //                console.error('Error al cargar los datos de la campaña de extracción:', error.responseText);
    //                reject(error);
    //            }
    //        });
    //    });
    //}
    //var idsCampañas = CampanatipoArbolExt.map(function (campaña) {
    //    return campaña.idTipoArbol;
    //});
    //// Función para obtener las campañas
    //function obtenerCampañas(idsCampañas) {
    //    return new Promise(function (resolve, reject) {
    //        $.ajax({
    //            url: '/Pret07Extraccion/ObtenerCampanas',
    //            type: 'GET',
    //            data: { listaIds: idsCampañas }, // Pass the list of campaign IDs to the server
    //            success: function (data) {
    //                $('#campanataselect').empty();

    //                // Seleccionar automáticamente el primer elemento
    //                var primerElemento = null;

    //                $.each(data, function (index, item) {
    //                    $('#campanataselect').append($('<option>').text(item.txtdesc).attr('value', item.idta));

    //                    if (index === 0) {
    //                        primerElemento = item.idta;
    //                    }
    //                });

    //                // Si hay más de un elemento, activar el evento change
    //                if (data.length > 1) {
    //                    $('#campanataselect').change(function () {
    //                        var taId = $(this).val();
    //                        cargarDatosCampana(taId);
    //                    });
    //                }

    //                // Ejecutar la carga de datos de la campaña
    //                cargarDatosCampana(primerElemento)
    //                    .then(resolve)  // Resuelve la promesa de obtenerCampañas
    //                    .catch(reject);  // Rechaza la promesa de obtenerCampañas en caso de error
    //            },
    //            error: function (error) {
    //                console.error('Error al obtener campañas:', error.responseText);
    //                reject(error);
    //            }
    //        });
    //    });
   /* }*/

    // Función para cargar datos de campaña
    //function cargarDatosCampana(taId) {
    //    return new Promise(function (resolve, reject) {
    //        $.ajax({
    //            url: '/Pret07Extraccion/CargarDatosCampana',
    //            type: 'GET',
    //            data: { taid: taId },
    //            success: function (data) {
    //                console.log(data);
    //                document.getElementById('nombreta').value = data.txtdesc;
    //                document.getElementById('nroarbolta').value = data.nroarbol;
    //                resolve(data);
    //            },
    //            error: function (error) {
    //                console.error('Error al cargar campaña tipo arbol:', error.responseText);
    //                reject(error);
    //            }
    //        });
    //    });
    //}

    //// Lógica principal
    //seleccionarCampExt()
    //    .then(obtenerCampañas)
    //    .catch(function (error) {
    //        console.error('Error:', error);
    //    });
    // Puedes agregar lógica adicional aquí según tus necesidades
}

//function cargarDatosCampExt(response) {
//    // Clear the existing tbody content
//    $('#bodycampext').empty();

//    // Iterate through the updated campext data and append rows to tbody
//    $.each(response, function (index, campext) {
//        var row = $('<tr>');
//        row.append('<td align="center" class="client-cell">' + campext.codigo + '</td>');
//        row.append('<td align="center" class="client-cell">' + campext.nrositio + '</td>');
//        row.append('<td align="center" class="client-cell">' + campext.fechaini + '</td>');
//        row.append('<td align="center" class="client-cell">' +
//            '<button class="btn btn-primary" onclick="seleccionarcampext(\'modalcampext\',\'campextcbo\',\'' +
//            campext.id + '\', \'' + campext.codigo + '\')">Seleccionar</button>' +
//            '</td>');

//        // Append the row to the tbody
//        $('#bodycampext').append(row);
//    });
//}

// Función para añadir un producto a la lista de productos seleccionados
function agregarCampanataExt() {
    var idtipoarbol = parseInt($("#campanataselect").val());
    var numarbol = parseInt($("#nroarbolta").val());
    var altura= parseFloat($("#alturata").val());
    var diametro= parseFloat($("#diametrota").val());
    var nrotrozas = parseInt($("#nrotrozasta").val());
    var comentario= ($("#comentariota").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idtipoarbol) || idtipoarbol <= 0 || isNaN(altura) || isNaN(diametro) || isNaN(nrotrozas) || !Number.isInteger(numarbol) || !Number.isInteger(nrotrozas)
        || numarbol <= 0 || nrotrozas <= 0 || altura <= 0 || diametro<= 0) {
        alert("Por favor, ingrese valores numéricos para la extracción por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }
    $.ajax({
        url: "/Pret07Extraccion/TipoArbol",
        type: "GET",
        data: { idtipoarbol: idtipoarbol },
        success: function (response) {
            if (response != null) {

               
                CampanatipoArbolExt.push({
                    idunico: response.idUnico,
                    idTipoArbol: response.idTipoArbol,
                    nombreTipoarbol: response.nombretipoarbol,
                    numarbol: numarbol,
                    altura: altura,
                    diametro: diametro,
                    nrotrozas: nrotrozas,
                    comentario: comentario
                });

                actualizarTablaTipoArbolSeleccionadosExt();
            } else {
                alert("Tipo de Arbol no encontrado");
            }
        },
        error: function (response) {
            alert("Error al obtener los datos del Tipo de Arbol");
        }
    });

    return false;
}

function actualizarTotalesTAExt() {
    let Mtototalaltura= 0;
    let Mtototaldiametro= 0;
    let Mtototalnrotrozas= 0;
    let Mtototalnroarbol = 0;



    for (let i = 0; i < CampanatipoArbolExt.length; i++) {
        const totalaltura= parseFloat(CampanatipoArbolExt[i].altura);
        const totaldiametro= parseFloat(CampanatipoArbolExt[i].diametro);
        const totalnrotrozas = parseInt(CampanatipoArbolExt[i].nrotrozas);
        const totalnroarbol = parseInt(CampanatipoArbolExt[i].numarbol);


        Mtototalnroarbol += totalnroarbol;
        Mtototaldiametro+= totaldiametro;
        Mtototalnrotrozas+= totalnrotrozas;
        Mtototalaltura+= totalaltura;

    }

    // Actualizar los campos en el documento HTML
    // Actualizar los campos en el documento HTML con dos decimales
    document.getElementById("nroarboltotal").value = Mtototalnroarbol ;
    document.getElementById("altarbtotal").value = Mtototalaltura / CampanatipoArbolExt.length;
    document.getElementById("diamprototal").value = Mtototaldiametro / CampanatipoArbolExt.length;
    document.getElementById("nrotrozastotal").value = Mtototalnrotrozas ;
}

// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarTipoArbolExt(idTipoArbolExt) {
    if (typeof idTipoArbolExt !== 'string') {
        console.error('El ID no es una cadena.');
        console.error(idTipoArbolExt);
        return;
    }
    var campanaTAExt = CampanatipoArbolExt.find(function (campTA) {
        return campTA.idunico === idTipoArbolExt;
    });
    console.log(idTipoArbolExt);
    console.log("conj" + campanaTAExt);
    if (campanaTAExt) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este registro de la lista?");

        if (confirmar) {
            var campTAExtindex = CampanatipoArbolExt.findIndex(function (campTA) {
                return campTA.idunico === idTipoArbolExt;
            });

            if (campTAExtindex !== -1) {
                CampanatipoArbolExt.splice(campTAExtindex, 1);
                actualizarTablaTipoArbolSeleccionadosExt();
            }
        }
    } else {
        alert("Registro no encontrado en la lista.");
    }
}


//Función para abrir el modal de edición
function editarTipoArbolExt(idTipoArbolExt) {
    var campanatipoarbolExt = CampanatipoArbolExt.find(function (campTA) {
        return campTA.idunico === idTipoArbolExt;
    });
    if (campanatipoarbolExt) {
        document.getElementById("nuevonroarbolTAExt").value = campanatipoarbolExt.numarbol;
        document.getElementById("IdEditarTAExt").value = idTipoArbolExt;
        document.getElementById("TAExtinput").value = campanatipoarbolExt.nombreTipoarbol;
        document.getElementById("nuevaalturaTAExt").value = campanatipoarbolExt.altura;
        document.getElementById("nuevodiametroTAExt").value = campanatipoarbolExt.diametro;
        document.getElementById("nuevanrotrozasTAExt").value = campanatipoarbolExt.nrotrozas;
        document.getElementById("comentarioTAExt").value = campanatipoarbolExt.comentario;
        openSecondModal('editarTAEXTModal');
    } else {
        alert("Producto no encontrado en la lista.");
    }
}

// Función para aplicar los cambios desde el modal y guardarlos
function aplicarCambiosTAExt() {

    var idcampTA = (document.getElementById("IdEditarTAExt").value);

    var campanaTAExt = CampanatipoArbolExt.find(function (prod) {
        return prod.idunico === idcampTA;
    });
    var nuevanroarbol = parseInt(document.getElementById("nuevonroarbolTAExt").value);
    var nuevaaltura = parseFloat(document.getElementById("nuevaalturaTAExt").value);
    var nuevodiametro = parseFloat(document.getElementById("nuevodiametroTAExt").value);
    var nuevanrotrozas = parseInt(document.getElementById("nuevanrotrozasTAExt").value);
    var comentarionuevo= (document.getElementById("comentarioTAExt").value);
    if (isNaN(nuevaaltura) || isNaN(nuevodiametro) || isNaN(nuevanrotrozas) || !Number.isInteger(nuevanroarbol) || !Number.isInteger(nuevanrotrozas)
        || nuevanroarbol <= 0 || nuevanrotrozas <= 0 || nuevodiametro <= 0 || nuevaaltura<= 0) {
        alert("Por favor, ingrese valores numéricos para la extracción por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }
    if (!isNaN(nuevanroarbol) && !isNaN(nuevaaltura)) {
        var confirmar = confirm("¿Deseas aplicar los cambios y guardarlos?");

        if (confirmar) {
            // Aplicar parseFloat con toFixed(2) a las variables
            var nroarbol = Number(nuevanroarbol);
            var altura = Number(nuevaaltura);
            var diametro = Number(nuevodiametro);
            var nrotrozas = nuevanrotrozas;
            var comentario= (comentarionuevo);
            console.log(nroarbol);
            console.log(campanaTAExt);

            // Asignar los valores a las propiedades del objeto producto
            campanaTAExt.altura = altura;
            campanaTAExt.diametro = diametro;
            campanaTAExt.nrotrozas  = nrotrozas;
            campanaTAExt.comentario =comentario;
            campanaTAExt.numarbol = nroarbol;


            actualizarTablaTipoArbolSeleccionadosExt();
            cerrarModal('editarTAEXTModal');
        } else {
            alert("Edición cancelada. Los cambios no se aplicaron.");
        }
    } else {
        alert("Por favor, ingresa valores numéricos válidos para los campos.");
    }
}






//empleado extraccion

//Proceso para cerrar la ventana modal CHOFER
$(document).ready(function () {
    $('#modalempext').on('hidden.bs.modal', function () {
        $('#choempext').prop('selectedIndex', 0);

        cargarDatosempext();
    });
});



//CARGAR CHOFER EN TABLA CHOFER
function cargarDatosempext() {
    var idsempleado = arregloextemp.map(function (campaña) {
        return campaña.idempleado;
    });
    $.ajax({
        url: '/Pret07Extraccion/Recargarempext', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { listaIds: idsempleado }, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodyempext').empty();
            $.each(response, function (index, EmpleadoExt) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + EmpleadoExt.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoExt.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoExt.telefono+ '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoExt.cargo + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoExt.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarempext(\'modalempext\',\'choempext\',\'empextId\',\'empextNombre\',\'' +
                    EmpleadoExt.idempleado + '\', \'' + EmpleadoExt.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');
                $('#bodyempext').append(row);
            });
            
        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado extracción:', error.responseText);
        }
    });
}

////CBO DEPENDIENTE CHOFER
//$(document).ready(function () {
//    // Manejador de cambio para el departamento
//    $('#departamentoempext').change(function () {
//        var departamentoId = $(this).val();

//        // Realiza una solicitud AJAX para obtener las provincias
//        $.ajax({
//            url: '/Pret07Extraccion/ObtenerProvincias',
//            type: 'GET',
//            data: { departamentoId: departamentoId },
//            success: function (data) {
//                // Actualiza el contenido del select de provincia
//                $('#provinciaempext').empty();
//                $.each(data, function (index, item) {
//                    $('#provinciaempext').append($('<option>').text(item.txtDesc).attr('value', item.idProv));
//                });

//                // Llama al evento change para actualizar los distritos
//                $('#provinciaempext').change();
//            },
//            error: function () {
//                console.error('Error al obtener provincias.');
//            }
//        });
//    });
//    // Manejador de cambio para la provincia
//    $('#provinciaempext').change(function () {
//        var provinciaId = $(this).val();

//        // Realiza una solicitud AJAX para obtener los distritos
//        $.ajax({
//            url: '/Pret07Extraccion/ObtenerDistritos',
//            type: 'GET',
//            data: { provinciaId: provinciaId },
//            success: function (data) {
//                // Actualiza el contenido del select de distrito
//                $('#distritoempext').empty();
//                $.each(data, function (index, item) {
//                    $('#distritoempext').append($('<option>').text(item.txtDesc).attr('value', item.idDist));
//                });
//            },
//            error: function () {
//                console.error('Error al obtener distritos.');
//            }
//        });
//    });
//});

//Cargar la selección de Chofer
function seleccionarempext(modalId, selectId, idcod, idtext, idempext, nombreempext) {
    event.preventDefault();
    var choferIdInput = document.getElementById(idcod);
    choferIdInput.value = idempext;
    document.getElementById(idtext).innerText = nombreempext;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var choferNombreSpan = document.getElementById('empextNombre');
    var removeChoferSpan = document.getElementById('removeempextSpan');
    choferNombreSpan.innerText = nombreempext;
    choferNombreSpan.style.display = 'inline-block';
    removeChoferSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('empextcbo');
    selectElement.style.display = 'none';
    $.ajax({
        url: '/Pret07Extraccion/CargarDatosEmpleado',
        type: 'GET',
        data: { idEmpleado : idempext },
        success: function (data) {
            if (!data) {
                // Si data es null o undefined
                document.getElementById('nrodocempleado').value = "";
                document.getElementById('categoriaempleado').value = "";
                document.getElementById('condicionempleado').value = "";
                document.getElementById('telefonoempleado').value = "";
            } else {
                // Si data no es null o undefined
                document.getElementById('nrodocempleado').value = data.nrodoc|| "";
                document.getElementById('categoriaempleado').value = data.cargo || "";
                document.getElementById('condicionempleado').value = data.condicion || "";
                document.getElementById('telefonoempleado').value = data.telefono || "";

            }
        },
        error: function (error) {
            console.error('Error al cargar datos del empleado:', error.responseText);

        }
    });

}

//El span cliente para volver al select
function removeempext() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('nrodocempleado').value = '';
    document.getElementById('categoriaempleado').value = '';
    document.getElementById('telefonoempleado').value = '';
    /*document.getElementById('salarioempleado').value = 1;*/
    document.getElementById('empextId').value = 0;
    document.getElementById('condicionempleado').value = '';
;
    document.getElementById('empextNombre').style.display = 'none';
    document.getElementById('removeempextSpan').style.display = 'none';
    var selectElement = document.getElementById('empextcbo');
    selectElement.style.display = 'inline-block';
}

//AREGLO EXT EMP
var arregloextemp = [];



function agregarextemp() {
    var idempleado= parseInt($("#empextId").val());
    /*var salario= parseFloat($("#salarioempleado").val());*/
    var nrodoc= ($("#nrodocempleado").val());
    var categoria =($("#categoriaempleado").val());
    var condicion = ($("#condicionempleado").val());
    var telefono= ($("#telefonoempleado").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idempleado) || idempleado <= 0 /*|| isNaN(salario) ||   0 >(salario)*/ ) {
        alert("Por favor, ingrese valores numéricos para la extracción por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }
    $.ajax({
        url: "/Pret07Extraccion/EmpleadoExt",
        type: "GET",
        data: { empleadoID: idempleado},
        success: function (response) {
            if (response != null) {


                arregloextemp.push({
                    idempleado: idempleado,
                    txtempleado:response.txtnombre,
                    telefono:response.telefono,
/*                    salario: salario,*/
                    nrodoc: nrodoc,
                    categoria: categoria,
                    condicion: condicion
                });

                actualizartablaextemp();
                removeempext();


            } else {
                alert("Tipo de Arbol no encontrado");
            }
        },
        error: function (response) {
            alert("Error al obtener los datos del Tipo de Arbol");
        }
    });


}

// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarextemp(idextemp) {
    var campanaextemp = arregloextemp.find(function (campTA) {
        return campTA.idempleado=== idextemp;
    });

    if (campanaextemp) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este registro de la lista?");

        if (confirmar) {
            var campTAExtindex = arregloextemp.findIndex(function (campTA) {
                return campTA.idempleado=== idextemp;
            });

            if (campTAExtindex !== -1) {
                arregloextemp.splice(campTAExtindex, 1);
                actualizartablaextemp();
            }
        }
    } else {
        alert("Registro no encontrado en la lista.");
    }
}


//Función para abrir el modal de edición
function editarempleadosExtraccion(idextemp) {
    var empleadosextraccion = arregloextemp.find(function (campTA) {
        return campTA.idempleado=== idextemp;
    });
    if (empleadosextraccion) {
       /* document.getElementById("nuevosalarioextemp").value = empleadosextraccion.salario;*/
        document.getElementById("IdEditarempext").value = idextemp;
        document.getElementById("nuevacategoriaextemp").value = empleadosextraccion.categoria;
        document.getElementById("nuevacondicioneemp").value = empleadosextraccion.condicion;
        document.getElementById("nuevodocextemp").value = empleadosextraccion.nrodoc;
        openSecondModal('editarEMPEXTModal');
    } else {
        alert("Empleado no encontrado en la lista.");
    }
}

// Función para aplicar los cambios desde el modal y guardarlos
//function aplicarCambiosExtemp() {
//    var idcampTA = parseInt(document.getElementById("IdEditarempext").value);
//    var campanaTAExt = arregloextemp.find(function (prod) {
//        return prod.idempleado=== idcampTA;
//    });
//    /*var nuevosalario= parseFloat(document.getElementById("nuevosalarioextemp").value);*/


//    /*if (nuevosalario>0 ) {*/
//        var confirmar = confirm("¿Deseas aplicar los cambios y guardarlos?");

//        if (confirmar) {
//            // Aplicar parseFloat con toFixed(2) a las variables
//            var salario= parseFloat(nuevosalario);


//            // Asignar los valores a las propiedades del objeto producto
//            campanaTAExt.salario = salario;


//            actualizartablaextemp();
//            cerrarModal('editarEMPEXTModal');
//        } else {
//            alert("Edición cancelada. Los cambios no se aplicaron.");
//        }
//    //} else {
//    //    alert("Por favor, ingresa valores numéricos válidos para los campos.");
//    //}
//}



function actualizartablaextemp() {
    // Limpiar la tabla
    $("#empSeleccionadosExt").empty();

    // Recorrer el arreglo de tipo de árboles seleccionados y volver a agregarlos a la tabla
    arregloextemp.forEach(function (Empleado) {
        var newRow = `
            <tr id='${Empleado.idempleado}'>
                <td class="text-center">${Empleado.txtempleado}</td>
                <td class="text-center">${Empleado.condicion}</td>
                <td class="text-center">${Empleado.telefono}</td>
                <td class="text-center">${Empleado.nrodoc}</td>
                <td class="text-center">${Empleado.categoria}</td>
                <td colspan='2' align='center'>
                    
                    <button type='button' class="btn btn-danger btn-sm" onclick='eliminarextemp(${Empleado.idempleado})'><i class="fas fa-trash alt"></i></button>
                </td>
            </tr>`;

        $("#empSeleccionadosExt").append(newRow);
    });
    cargarDatosempext();

}
/*<button type='button' onclick='editarempleadosExtraccion(${Empleado.idempleado})'>Editar</button>*/

function enviarDatosExtGuardar() {
    if (arregloextemp.length > 0) {
        if (CampanatipoArbolExt.length > 0) {

        var arregloTAext= JSON.stringify(CampanatipoArbolExt);
        var arregloextEmp= JSON.stringify(arregloextemp);

            if (camposRequeridosLlenos('FormExtraccion')) {
                var data = {
                //EXTRACCION
                FechaExtraccion: $('#fechaextraccion').val(),
                altarbtot: parseFloat($('#altarbtotal').val()),
                nroarboltot: parseInt($('#nroarboltotal').val()),
                    nrotrozastot: parseInt($('#nrotrozastotal').val()),
                diamtot: parseFloat($('#diamprototal').val()),
                comentarioExt: ($('#comentario').val()),
                //DETALLE EXTRACCIÓN
                arregloextraccionTA: arregloTAext,
                arregloempleadosextraccion: arregloextEmp, 
            };


            $.ajax({
                url: '/Pret07Extraccion/CrearExtraccion', // Ajusta la URL según la estructura de tu aplicación
                type: 'POST',
                data: data,
                dataType: 'json',
                success: function (response) {
                    if (response.mensaje == null) {
                        window.location.href = response.redirectUrl;
                    } else {
                        alert(response.mensaje);
                    }
                },
                error: function (error) {
                    console.error('Error al crear la campaña:', error.responseText);
                }
            });
        } else {
            alert('Por favor, complete todos los campos requeridos.');
        }
        } else {
            alert('Registre al menos un tipo de arbol por extracción.')
        }
    } else {
        alert('Registre al menos un empleado.')
    }
}
function enviarDatosExtGuardaryCerrar() {
    if (arregloextemp.length > 0) {
        if (CampanatipoArbolExt.length > 0) {

            var arregloTAext = JSON.stringify(CampanatipoArbolExt);
            var arregloextEmp = JSON.stringify(arregloextemp);

            if (camposRequeridosLlenos('FormExtraccion')) {
                var data = {
                    //EXTRACCION
                    FechaExtraccion: $('#fechaextraccion').val(),
                    altarbtot: parseFloat($('#altarbtotal').val()),
                    nroarboltot: parseInt($('#nroarboltotal').val()),
                    nrotrozastot: parseInt($('#nrotrozastotal').val()),
                    diamtot: parseFloat($('#diamprototal').val()),
                    comentarioExt: ($('#comentario').val()),
                    //DETALLE EXTRACCIÓN
                    arregloextraccionTA: arregloTAext,
                    arregloempleadosextraccion: arregloextEmp,
                };


                $.ajax({
                    url: '/Pret07Extraccion/CrearExtraccionyCerrar', // Ajusta la URL según la estructura de tu aplicación
                    type: 'POST',
                    data: data,
                    dataType: 'json',
                    success: function (response) {
                        if (response.mensaje == null) {
                            window.location.href = response.redirectUrl;
                        } else {
                            alert(response.mensaje);
                        }
                    },
                    error: function (error) {
                        console.error('Error al crear la campaña:', error.responseText);
                    }
                });
            } else {
                alert('Por favor, complete todos los campos requeridos.');
            }
        } else {
            alert('Registre al menos un tipo de arbol por extracción.')
        }
    } else {
        alert('Registre al menos un empleado.')
    }
}
$(document).ready(function () {
    // ...

    // Evento que se dispara cuando el modal se está mostrando
    $('#modalEliminar').on('show.bs.modal', function (event) {
        // Botón que activó el modal
        var button = $(event.relatedTarget);

        // Obtener el idExtraccion del atributo de datos del botón
        var idExtraccion = button.data('idextraccion');

        // Asignar el idExtraccion al botón "Eliminar" dentro del modal
        $('#confirmarEliminar').data('idextraccion', idExtraccion);
    });

    // Manejar clic en el botón "Eliminar" dentro del modal
    $('#confirmarEliminar').on('click', function () {
        // Obtener el idExtraccion del botón "Eliminar" dentro del modal
        var idExtraccion = $(this).data('idextraccion');

        // Enviar solicitud AJAX al controlador para eliminar la Extracción
        $.ajax({
            url: "/Pret07Extraccion/EliminarExtraccion",
            type: "POST",
            data: { id: idExtraccion },
            success: function (response) {
                if (response != null && response.mensaje) {
                    // Éxito: hacer algo después de la eliminación
                    alert(response.mensaje);
                } else {
                    // Manejar errores si es necesario
                    alert("Error al eliminar la Extracción");
                }
            },
            error: function () {
                // Manejar errores de la solicitud AJAX
                alert("Error al comunicarse con el servidor");
            }
        });

        // Cerrar el modal
        $('#modalEliminar').modal('hide');
    });

    // ...
});
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#Predio').change(function () {
        var predioId = $(this).val();
        console.log(this, $(this));

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Pert11PagoPersonal/Obtenercampaña',
            type: 'GET',
            data: { predioId: predioId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#Campana').empty();
                $.each(data, function (index, item) {
                    $('#Campana').append($('<option>').text(item.codigo).attr('value', item.idCampana));
                });

                // Llama al evento change para actualizar los distritos
                $('#Campana').change();
            },
            error: function () {
                console.error('Error al obtener Campaña.');
            }
        });
    });
});


























//PROVEEDOR


//Proceso para cerrar la ventana modal Proveedor
$(document).ready(function () {
    $('#modalprov').on('hidden.bs.modal', function () {
        $('#provcbo').prop('selectedIndex', 0);
        $('#proveedoroptionname').prop('selectedIndex', 0);
        $('#proveedoroptionid').prop('selectedIndex', 0);
        /*        cargarDatosproveedor();*/
        // Vaciar los campos de filtro
        $('#filtroRazonProv').val('');
        $('#filtroRUCProv').val('');
    });
});


//Proceso para cerrar la ventana modal proveedor

$(document).ready(function () {
    $('#createProvModal').on('hidden.bs.modal', function () {
        // Vaciar los campos de Crear
        cargarDatosProveedor();
        $('#prim_nombrecli, #seg_nombrecli, #patapellidocli, #matapellidocli, #razonSocialcli, #nroDoccli, #celcli').val('');
        $('#provinciacli, #distritocli').empty().prop('selectedIndex', 0);
        $('#TipoNroDoccli').prop('selectedIndex', 0);
    });
});


//INSERCION Producto

$('#crearProductoBtn').click(function () {

    var totalConIGV = parseFloat(document.getElementById("txt_cigv").value);
    var precioSinIGV = totalConIGV / 1.18;
    var igv = precioSinIGV * 0.18; // Considerando un IGV del 18%

    document.getElementById("txt_igv").value = igv.toFixed(2);
    document.getElementById("txt_sigv").value = precioSinIGV.toFixed(2);

    if (camposRequeridosLlenos('FormAñadirProd')) {
        var formData = new FormData();

        formData.append('txt_desc', $('#txt_desc').val() || '');
        formData.append('id_tipo', $('#TipoProCom').val() || '');
        formData.append('id_modelo', $('#TipoMode').val() || '');


        $.ajax({
            url: '/Tnst01CompRecibido/CrearProducto',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                console.log('Producto creado correctamente:', response);
                var idProducto = response.nuevoID;
                agregarProductoC(idProducto); // Pasar el idProducto como argumento

            },
            error: function (error) {
                console.error('Error al crear el Proveedor:', error);
            }
        });
    } else {
        alert('Por favor, complete todos los campos requeridos.');
    }
});



//INSERCION Proveedor
$('#crearProveedorBtn').click(function () {
    if (camposRequeridosLlenos('createProveedorForm')) {
        var formData = new FormData();

        formData.append('txt_razon_social', $('#razonprov').val() || '');
        formData.append('txt_ruc', $('#rucprov').val() || '');
        formData.append('txt_direc', $('#direccionprov').val() || '');

        $.ajax({
            url: '/Tnst01CompRecibido/CrearProveedor',
            type: 'POST',
            data: formData,
            processData: false, // Evita que jQuery convierta los datos en una cadena
            contentType: false, // Fuerza a jQuery a no configurar el tipo de contenido
            success: function (response) {
                console.log('Proveedor creado correctamente:', response);
                cargarDatosProveedor();
                $('#createProveedorModal').modal('hide');
            },
            error: function (error) {
                console.error('Error al crear el Proveedor:', error);
            }
        });
    } else {
        alert('Por favor, complete todos los campos requeridos.');
    }
});

//CARGAR CLIENTE EN TABLA CLIENTE
function cargarDatosProveedor() {
    $.ajax({
        url: '/Tnst01CompRecibido/RecargarProveedor', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            /*console.log(Proveedor:', response);*/
            // Clear the existing tbody content
            $('#bodyprov').empty();

            // Iterate through the updated client data and append rows to tbody
            $.each(response, function (index, proveedor) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + proveedor.razon + '</td>');
                row.append('<td align="center" class="client-cell">' + proveedor.ruc + '</td>');
                /*                row.append('<td align="center" class="client-cell">' + proveedor.direccion + '</td>');*/
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarProveedor(\'modalprov\',\'provcbo\',\'proveedorID\',\'proveedorruc\',\'proveedorrazon\',\'proveedordireccion\', \'' +
                    proveedor.IdProveedor + '\', \'' + proveedor.ruc + '\', \'' + proveedor.razon + '\', \'' + proveedor.direccion + '\')">Seleccionar</button>' +
                    '</td>');


                // Append the row to the tbody
                $('#bodyprov').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del cliente:', error.responseText);
        }
    });
}

//Cargar la selección de Cliente


function seleccionarProveedor(modalId, selectId, idcod, idruc, idrazon, iddireccion, idproveedor, ruc, razon, direccion) {
    event.preventDefault();
    var proveedorIdInput = document.getElementById(idcod);
    proveedorIdInput.value = idproveedor;
    /*    document.getElementById(idcod).innerText = idCliente;*/
    document.getElementById(idrazon).innerText = razon;
    document.getElementById(iddireccion).innerText = direccion;
    document.getElementById(idruc).innerText = ruc;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover

    var proveedorNombreSpan = document.getElementById('proveedorrazon');
    var proveedorDireccionSpan = document.getElementById('proveedordireccion');
    var removeProveedorSpan = document.getElementById('removeProveedorSpan');

    proveedorNombreSpan.style.display = 'inline-block';
    proveedorDireccionSpan.style.display = 'inline-block';
    removeProveedorSpan.style.display = 'inline-block';

    // Ocultar el select
    var selectElement = document.getElementById('provcbo');
    selectElement.style.display = 'none';
}

//El span cliente para volver al select
function removeProveedor() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('proveedorrazon').innerText = '';
    document.getElementById('proveedorID').value = '';
    document.getElementById('proveedorruc').innerText = '';
    document.getElementById('proveedorrazon').style.display = 'none';
    document.getElementById('proveedordireccion').style.display = 'none';
    document.getElementById('removeProveedorSpan').style.display = 'none';
    var selectElement = document.getElementById('provcbo');
    selectElement.style.display = 'inline-block';

}


//filtro de Cliente por Nombre y Ruc
function filtrarProv() {
    var filtroNombre = document.getElementById('filtroRazonProv').value.toLowerCase();
    var filtroRUC = document.getElementById('filtroRUCProv').value.toLowerCase();
    var optcliname = document.getElementById('proveedoroptionname').value;
    var optcliruc = document.getElementById('proveedoroptionid').value;

    $('.tablaClientes tbody tr').each(function () {
        var razon = $(this).find('.client-cell:nth-child(1)').text().toLowerCase();
        var rucProveedor = $(this).find('.client-cell:nth-child(2)').text().toLowerCase();

        var nombreCoincide, rucCoincide;

        if (optcliname === '1') {
            nombreCoincide = razon.startsWith(filtroNombre);
        } else if (optcliname === '2') {
            nombreCoincide = razon.includes(filtroNombre);
        }

        if (optcliruc === '1') {
            rucCoincide = rucProveedor.startsWith(filtroRUC);
        } else if (optcliruc === '2') {
            rucCoincide = rucProveedor.includes(filtroRUC);
        }

        if ((filtroNombre === '' || nombreCoincide) && (filtroRUC === '' || rucCoincide)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

//CBO DEPENDIENTE producto
$(document).ready(function () {
    // Manejador de cambio para el departamento
    $('#TipoProCom').change(function () {
        var tipoId = $(this).val();
        console.log(this, $(this));

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst01CompRecibido/ObtenerMarca',
            type: 'GET',
            data: { tipoId: tipoId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#TipoMarc').empty();
                $.each(data, function (index, item) {
                    $('#TipoMarc').append($('<option>').text(item.txtDesc).attr('value', item.idmarca));
                });

                // Llama al evento change para actualizar los distritos
                $('#TipoMarc').change();
            },
            error: function () {
                console.error('Error al obtener Marca.');
            }
        });
    });
    var initialTipoId = 7; // Reemplaza con el ID del tipo preseleccionado
    $('#TipoProCom').val(initialTipoId).change();

    $('#TipoMarc').change(function () {
        var marcaId = $(this).val();

        // Realiza una solicitud AJAX para obtener las provincias
        $.ajax({
            url: '/Tnst01CompRecibido/ObtenerModelo',
            type: 'GET',
            data: { marcaId: marcaId },
            success: function (data) {
                // Actualiza el contenido del select de provincia
                $('#TipoMode').empty();
                $.each(data, function (index, item) {
                    $('#TipoMode').append($('<option>').text(item.txtDesc).attr('value', item.idmodelo));
                });

                // Llama al evento change para actualizar los distritos
                $('#TipoMode').change();
            },
            error: function () {
                console.error('Error al obtener Modelo.');
            }
        });
    });
    $('#TipoProCom').change();
});

//$('#crearProveedorBtn').click(function () {
//    if (camposRequeridosLlenos('createProveedorForm')) {
//        var formData = new FormData();

//        formData.append('txt_razon_social', $('#razonprov').val() || '');
//        formData.append('txt_ruc', $('#rucprov').val() || '');
//        formData.append('txt_direc', $('#direccionprov').val() || '');

//        $.ajax({
//            url: '/Tnst01CompRecibido/CrearProveedor',
//            type: 'POST',
//            data: formData,
//            processData: false, // Evita que jQuery convierta los datos en una cadena
//            contentType: false, // Fuerza a jQuery a no configurar el tipo de contenido
//            success: function (response) {
//                console.log('Proveedor creado correctamente:', response);
//                cargarDatosProveedor();
//                $('#createProveedorModal').modal('hide');
//            },
//            error: function (error) {
//                console.error('Error al crear el Proveedor:', error);
//            }
//        });
//    } else {
//        alert('Por favor, complete todos los campos requeridos.');
//    }
//});


const productosSeleccionados = [];
const productosCSeleccionados = [];
function agregarProductoC(idProducto) {
    var tipoproducto = $("#TipoProCom option:selected").text();
    var marcaproducto = $("#TipoMarc option:selected").text();
    var modeloproducto = $("#TipoMode option:selected").text();
    var nombreProducto = $("#txt_desc").val();
    var cantidad = $("#cantidad").val();
    var sigv = parseFloat($("#txt_sigv").val());
    var IGV = parseFloat($("#txt_igv").val());
    var cigv = parseFloat($("#txt_cigv").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (cantidad <= 0 || isNaN(sigv) || isNaN(IGV) || isNaN(cigv)) {
        alert("Por favor, ingrese valores numéricos válidos");
        return false; // Detener la ejecución si los campos no son válidos
    }

    // Agrega el producto a la lista local después de obtener el ID del servidor
    var idProducto = idProducto;

    // Aquí puedes realizar cualquier otra lógica específica que necesites para agregar el producto
    // Por ejemplo, agregarlo a una lista de productos seleccionados
    productosCSeleccionados.push({
        tipoproducto: tipoproducto,
        marcaproducto: marcaproducto,
        modeloproducto: modeloproducto,
        nombreProducto: nombreProducto,
        cantidad: cantidad,
        monto_si: sigv,
        igv: IGV,
        monto_ci: cigv,
        idProducto: idProducto
    });

    // Luego de agregarlo, puedes actualizar la tabla de productos seleccionados
    actualizarTablaProductosSeleccionadosC();

    // Limpia los campos y oculta el modal después de agregar el producto
    limpiarCampos();
    $('#modalpro').modal('hide');
}
function limpiarCampos() {
    $("#TipoProCom").val('');
    $("#TipoMarc").val('');
    $("#TipoMode").val('');
    $("#txt_desc").val('');
    $("#cantidad").val('');
    $("#txt_sigv").val('');
    $("#txt_igv").val('');
    $("#txt_cigv").val('');
}

function actualizarTablaProductosSeleccionadosC() {
    // Limpiar la tabla
    $("#productosSeleccionados").empty();

    // Recorrer el arreglo de productos seleccionados y volver a agregarlos a la tabla
    productosCSeleccionados.forEach(function (producto) {
        var newRow = `
            <tr>
                <td style="font-size:14px;">${producto.nombreProducto}</td>
                <td style="font-size:14px;">${producto.tipoproducto}</td>
                <td style="font-size:14px;">${producto.marcaproducto}</td>
                <td style="font-size:14px;">${producto.modeloproducto}</td>
                <td style="font-size:14px;">${producto.cantidad}</td>
                <td style="font-size:14px;">${producto.monto_si}</td>
                <td style="font-size:14px;">${producto.igv}</td>
                <td style="font-size:14px;">${producto.monto_ci}</td>
                <td colspan='2' style="font-size:14px;">
                    <button class="btn btn-danger btn-sm" type='button' onclick='eliminarProductoC(${producto.idProducto})'> <i class="fas fa-trash alt"></i></button>
                </td>
            </tr>`;

        $("#productosSeleccionados").append(newRow);

    });
    actualizarTotalesC();
}

// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarProductoC(idProducto) {
    var producto = productosCSeleccionados.find(function (prod) {
        return prod.idProducto === idProducto;
    });

    if (producto) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este producto de la lista?");

        if (confirmar) {
            var productoIndex = productosCSeleccionados.find(function (prod) {
                return prod.idProducto === idProducto;
            });

            if (productoIndex !== -1) {
                productosCSeleccionados.splice(productoIndex, 1);
                actualizarTablaProductosSeleccionadosC();
            }
        }
    } else {
        alert("Producto no encontrado en la lista.");
    }
}



//Función para abrir el modal de edición
function editarProductoC(idProducto) {
    var producto = productosSeleccionados.find(function (prod) {
        return prod.idProducto === idProducto;
    });
    if (producto) {
        document.getElementById("nuevaCantidad").value = producto.cantidad;
        document.getElementById("IdEditarProducto").value = idProducto;
        document.getElementById("Productoinput").value = producto.nombreProducto;
        document.getElementById("nuevoDescuento").value = producto.descuento * 100;
        document.getElementById("observacionP").value = producto.observacion;
        openSecondModal('editarModal');
    } else {
        alert("Producto no encontrado en la lista.");
    }
}

// Función para aplicar los cambios desde el modal y guardarlos
function aplicarCambiosC() {
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
            var mtodescuento = parseFloat((producto.monto_si * nuevaCantidad * descuento));
            var subtotal = parseFloat((nuevaCantidad * producto.monto_si * (1 - descuento)));
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

function actualizarTotalesC() {

    let Mtototal = 0;
    let Mtoigv = 0;
    let Mtoneto = 0;


    for (let i = 0; i < productosCSeleccionados.length; i++) {

        const total = parseFloat(productosCSeleccionados[i].monto_ci);
        const montoIgv = parseFloat(productosCSeleccionados[i].igv);
        const Neto = parseFloat(productosCSeleccionados[i].monto_si);

        // Sumar los valores a los totales

        Mtototal += total;
        Mtoigv += montoIgv;
        Mtoneto += Neto;

    }

    // Actualizar los campos en el documento HTML
    // Actualizar los campos en el documento HTML con dos decimales

    document.getElementById("total").value = Mtototal.toFixed(2);
    document.getElementById("igv").value = Mtoigv.toFixed(2);
    document.getElementById("neto").value = Mtoneto.toFixed(2);

}



function obtenerDatosDelComprobanteC() {
    //var checkbox = document.getElementById('checkEstado');

    //if (checkbox.checked) {
    //    var idest = 1;
    //    var nombreest = "ACTIVO";
    //} else {
    //    var idest = 2;
    //    var nombreest = "FINALIZADO";
    //}

    var data = {
        IdCompRecibido: parseInt(document.getElementById("comprobanteIdInputC").value),
        NroCompRecibido: document.getElementById("nroComp").value,
        IdTipoComp: parseInt(document.getElementById("idtipoComp").value),
        TxtSerie: "p",
        TxtNumero: "p",
        FecRegistro: returndatenow(),
        FecRegRecibido: null,
        FecEmi: document.getElementById("fechaEmision").value,
        FecVcto: null,
        FecCanc: null,
        IdTipoMoneda: 1,
        IdTipoOrden: null,
        TxtObserv: null,
        MtoTcVta: 0.00,
        MtoNeto: parseFloat(document.getElementById("total").value),
        MtoExonerado: 0.00,
        MtoNoAfecto: 0.00,
        MtoDsctoTot: 0.00,
        MtoCmsTot: 0.00,
        MtoFleteTot: 0.00,
        MtoSubTot: 0.00,
        MtoImptoTot: 0.00,
        MtoServicio: 0.00,
        MtoTotComp: parseFloat(document.getElementById("neto").value),
        RefIdCompRecibido: null,
        RefTipoComprobante: null,
        RefFecha: null,
        RefSerie: null,
        RefNumero: null,
        TaxPor01: 0.00,
        TaxPor02: 0.00,
        TaxPor03: 0.00,
        TaxPor04: 0.00,
        TaxPor05: 0.00,
        TaxPor06: 0.00,
        TaxPor07: 0.00,
        TaxPor08: 0.00,
        TaxMto01: parseFloat(document.getElementById("igv").value),
        TaxMto02: 0.00,
        TaxMto03: 0.00,
        TaxMto04: 0.00,
        TaxMto05: 0.00,
        TaxMto06: 0.00,
        TaxMto07: 0.00,
        TaxMto08: 0.00,
        Info01: null,
        Info02: null,
        Info03: null,
        Info04: null,
        Info05: null,
        Info06: null,
        Info07: null,
        Info08: null,
        Info09: null,
        Info10: null,
        InfoDate01: null,
        InfoDate02: null,
        InfoDate03: null,
        InfoDate04: null,
        InfoDate05: null,
        InfoMto01: null,
        InfoMto02: null,
        InfoMto03: null,
        InfoMto04: null,
        InfoMto05: null,
        Post: null,
        PostDate: null,
        SnCredito: false,
        SnCancelada: false,
        IdUsuarioModificador: document.getElementById("idusermodC").value,
        TxtUsuarioModificador: document.getElementById("txtusermodC").value,
        FechaModificacion: document.getElementById("idfecmodC").value,
        TxtUsuario: document.getElementById("txtuserC").value,
        IdEstado: 1,
        TxtEstado: "ACTIVO",
        IdProveedor: document.getElementById("proveedorID").value,
        IdUsuario: document.getElementById("iduserC").value,
        IdLocation: 1,
        IdCliente: 1,
        TipoCompra: document.getElementById("idtipoCompra").value,
    };
    return data;
}

$(document).ready(function () {
    // Evento click para el botón "Guardar"


    $("#guardarC").click(function () {
        var dataToSend = obtenerDatosDelComprobanteC();
        enviarDatosGuardarC(dataToSend);
    });

    // Evento click para el botón "Guardar y Cerrar"
    $("#guardarycerrarC").click(function () {
        var dataToSend = obtenerDatosDelComprobanteC();
        enviarDatosGuardarYCerrarC(dataToSend);
    });

    $("#cancelarC").click(function () {
        window.location.href = '/Tnst01CompRecibido/boleta_c_listado'; // Reemplaza 'otra_ruta' con la URL deseada
    });
});

function enviarDatosGuardarC(dataToSend) {

    var productosCJson = JSON.stringify(productosCSeleccionados);
    console.log(productosCJson);
    var url = '/Tnst01CompRecibido/Guardar?';
    url += 'comprobanteId=' + dataToSend.IdCompRecibido;
    url += '&NroCompRecibido=' + encodeURIComponent(dataToSend.NroCompRecibido);
    url += '&IdTipoComp=' + dataToSend.IdTipoComp;
    url += '&TxtSerie=' + encodeURIComponent(dataToSend.TxtSerie);
    url += '&TxtNumero=' + encodeURIComponent(dataToSend.TxtNumero);
    url += '&FecRegistro=' + encodeURIComponent(dataToSend.FecRegistro);
    url += '&FecRegRecibido=' + encodeURIComponent(dataToSend.FecRegRecibido);
    url += '&FecEmi=' + encodeURIComponent(dataToSend.FecEmi);
    url += '&FecVcto=' + encodeURIComponent(dataToSend.FecVcto);
    url += '&FecCanc=' + encodeURIComponent(dataToSend.FecCanc);
    url += '&IdTipoMoneda=' + dataToSend.IdTipoMoneda;
    url += '&IdTipoOrden=' + dataToSend.IdTipoOrden;
    url += '&TxtObserv=' + encodeURIComponent(dataToSend.TxtObserv);
    url += '&MtoTcVta=' + dataToSend.MtoTcVta;
    url += '&MtoNeto=' + dataToSend.MtoNeto;
    url += '&MtoExonerado=' + dataToSend.MtoExonerado;
    url += '&MtoNoAfecto=' + dataToSend.MtoNoAfecto;
    url += '&MtoDsctoTot=' + dataToSend.MtoDsctoTot;
    url += '&MtoCmsTot=' + dataToSend.MtoCmsTot;
    url += '&MtoFleteTot=' + dataToSend.MtoFleteTot;
    url += '&MtoSubTot=' + dataToSend.MtoSubTot;
    url += '&MtoImptoTot=' + dataToSend.MtoImptoTot;
    url += '&MtoServicio=' + dataToSend.MtoServicio;
    url += '&MtoTotComp=' + dataToSend.MtoTotComp;
    url += '&RefIdCompRecibido=' + dataToSend.RefIdCompRecibido;
    url += '&RefTipoComprobante=' + encodeURIComponent(dataToSend.RefTipoComprobante);
    url += '&RefFecha=' + encodeURIComponent(dataToSend.RefFecha);
    url += '&RefSerie=' + encodeURIComponent(dataToSend.RefSerie);
    url += '&RefNumero=' + encodeURIComponent(dataToSend.RefNumero);
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
    url += '&Post=' + dataToSend.Post;
    url += '&PostDate=' + encodeURIComponent(dataToSend.PostDate);
    url += '&SnCredito=' + (dataToSend.SnCredito ? 'true' : 'false');
    url += '&SnCancelada=' + (dataToSend.SnCancelada ? 'true' : 'false');
    url += '&IdUsuarioModificador=' + dataToSend.IdUsuarioModificador;
    url += '&TxtUsuarioModificador=' + encodeURIComponent(dataToSend.TxtUsuarioModificador);
    url += '&FechaModificacion=' + encodeURIComponent(dataToSend.FechaModificacion);
    url += '&TxtUsuario=' + encodeURIComponent(dataToSend.TxtUsuario);
    url += '&IdEstado=' + dataToSend.IdEstado;
    url += '&TxtEstado=' + encodeURIComponent(dataToSend.TxtEstado);
    url += '&IdProveedor=' + dataToSend.IdProveedor;
    url += '&IdUsuario=' + dataToSend.IdUsuario;
    url += '&IdLocation=' + dataToSend.IdLocation;
    url += '&IdCliente=' + dataToSend.IdCliente;
    url += '&TipoCompra=' + dataToSend.TipoCompra;
    url += '&productosCseleccionados=' + encodeURIComponent(productosCJson);


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
            $('#comprobanteIdInput').val(response.IdCompRecibido);

        }
    });
}

function enviarDatosGuardarYCerrarC(dataToSend) {
    var productosCJson = JSON.stringify(productosCSeleccionados);

    // Verifica si la cantidad de elementos en el arreglo es mayor que 0
    if (productosCSeleccionados.length > 0) {
        var confirmar = confirm("¿Seguro que desea guardar y cerrar?");

        if (confirmar) {
            console.log(productosCJson);
            var url = '/Tnst01CompRecibido/GuardaryCerrarC?';
            url += 'comprobanteId=' + dataToSend.IdCompRecibido;
            url += '&NroCompRecibido=' + encodeURIComponent(dataToSend.NroCompRecibido);
            url += '&IdTipoComp=' + dataToSend.IdTipoComp;
            url += '&TxtSerie=' + encodeURIComponent(dataToSend.TxtSerie);
            url += '&TxtNumero=' + encodeURIComponent(dataToSend.TxtNumero);
            url += '&FecRegistro=' + encodeURIComponent(dataToSend.FecRegistro);
            url += '&FecRegRecibido=' + encodeURIComponent(dataToSend.FecRegRecibido);
            url += '&FecEmi=' + encodeURIComponent(dataToSend.FecEmi);
            url += '&FecVcto=' + encodeURIComponent(dataToSend.FecVcto);
            url += '&FecCanc=' + encodeURIComponent(dataToSend.FecCanc);
            url += '&IdTipoMoneda=' + dataToSend.IdTipoMoneda;
            url += '&IdTipoOrden=' + dataToSend.IdTipoOrden;
            url += '&TxtObserv=' + encodeURIComponent(dataToSend.TxtObserv);
            url += '&MtoTcVta=' + dataToSend.MtoTcVta;
            url += '&MtoNeto=' + dataToSend.MtoNeto;
            url += '&MtoExonerado=' + dataToSend.MtoExonerado;
            url += '&MtoNoAfecto=' + dataToSend.MtoNoAfecto;
            url += '&MtoDsctoTot=' + dataToSend.MtoDsctoTot;
            url += '&MtoCmsTot=' + dataToSend.MtoCmsTot;
            url += '&MtoFleteTot=' + dataToSend.MtoFleteTot;
            url += '&MtoSubTot=' + dataToSend.MtoSubTot;
            url += '&MtoImptoTot=' + dataToSend.MtoImptoTot;
            url += '&MtoServicio=' + dataToSend.MtoServicio;
            url += '&MtoTotComp=' + dataToSend.MtoTotComp;
            url += '&RefIdCompRecibido=' + dataToSend.RefIdCompRecibido;
            url += '&RefTipoComprobante=' + encodeURIComponent(dataToSend.RefTipoComprobante);
            url += '&RefFecha=' + encodeURIComponent(dataToSend.RefFecha);
            url += '&RefSerie=' + encodeURIComponent(dataToSend.RefSerie);
            url += '&RefNumero=' + encodeURIComponent(dataToSend.RefNumero);
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
            url += '&Post=' + dataToSend.Post;
            url += '&PostDate=' + encodeURIComponent(dataToSend.PostDate);
            url += '&SnCredito=' + (dataToSend.SnCredito ? 'true' : 'false');
            url += '&SnCancelada=' + (dataToSend.SnCancelada ? 'true' : 'false');
            url += '&IdUsuarioModificador=' + dataToSend.IdUsuarioModificador;
            url += '&TxtUsuarioModificador=' + encodeURIComponent(dataToSend.TxtUsuarioModificador);
            url += '&FechaModificacion=' + encodeURIComponent(dataToSend.FechaModificacion);
            url += '&TxtUsuario=' + encodeURIComponent(dataToSend.TxtUsuario);
            url += '&IdEstado=' + dataToSend.IdEstado;
            url += '&TxtEstado=' + encodeURIComponent(dataToSend.TxtEstado);
            url += '&IdProveedor=' + dataToSend.IdProveedor;
            url += '&IdUsuario=' + dataToSend.IdUsuario;
            url += '&IdLocation=' + dataToSend.IdLocation;
            url += '&IdCliente=' + dataToSend.IdCliente;
            url += '&TipoCompra=' + dataToSend.TipoCompra;
            url += '&productosCseleccionados=' + encodeURIComponent(productosCJson);


            $.ajax({
                url: url,
                type: 'POST',
                contentType: 'application/json',
                /*data: JSON.stringify(dataToSend),*/
                success: function (response) {
                    if (response.mensaje) {
                        window.location.href = '/Tnst01CompRecibido/boleta_c_listado'; // Reemplaza 'otra_ruta' con la URL deseada
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
function convertirAMayusculas() {
    var input = document.getElementById("nroComp");
    input.value = input.value.toUpperCase();
}



//Empleado


//Proceso para cerrar la ventana modal Proveedor
$(document).ready(function () {
    $('#modalemp').on('hidden.bs.modal', function () {
        $('#empcbo').prop('selectedIndex', 0);
        $('#proveedoroptionname').prop('selectedIndex', 0);
        $('#proveedoroptionid').prop('selectedIndex', 0);
        /*        cargarDatosproveedor();*/
        // Vaciar los campos de filtro
        $('#filtroRazonProv').val('');
        $('#filtroRUCProv').val('');
    });
});


//Proceso para cerrar la ventana modal proveedor



//function cargarDatosEmpleado() {
//    $.ajax({
//        url: '/Pert11PagoPersonal/RecargarEmpleado', // Reemplaza 'TuControlador' con el nombre real de tu controlador
//        type: 'GET',
//        success: function (response) {
//            /*console.log(Proveedor:', response);*/
//            // Clear the existing tbody content
//            $('#bodyprov').empty();

//            // Iterate through the updated client data and append rows to tbody
//            $.each(response, function (index, proveedor) {
//                var row = $('<tr>');
//                row.append('<td align="center" class="client-cell">' + proveedor.razon + '</td>');
//                row.append('<td align="center" class="client-cell">' + proveedor.ruc + '</td>');
//                /*                row.append('<td align="center" class="client-cell">' + proveedor.direccion + '</td>');*/
//                row.append('<td align="center" class="client-cell">' +
//                    '<button class="btn btn-primary" onclick="seleccionarProveedor(\'modalprov\',\'provcbo\',\'proveedorID\',\'proveedorruc\',\'proveedorrazon\',\'proveedordireccion\', \'' +
//                    proveedor.IdProveedor + '\', \'' + proveedor.ruc + '\', \'' + proveedor.razon + '\', \'' + proveedor.direccion + '\')">Seleccionar</button>' +
//                    '</td>');


//                // Append the row to the tbody
//                $('#bodyprov').append(row);
//            });
//        },
//        error: function (error) {
//            console.error('Error al cargar los datos del cliente:', error.responseText);
//        }
//    });
//}


$('#crearConceptoBtn').click(function () {
    if (camposRequeridosLlenos('createConceptoForm')) {
        var formData = new FormData();

        formData.append('txt_desc', $('#detalleconcepto').val() || '');


        $.ajax({
            url: '/Pert11PagoPersonal/CrearConcepto',
            type: 'POST',
            data: formData,
            processData: false, // Evita que jQuery convierta los datos en una cadena
            contentType: false, // Fuerza a jQuery a no configurar el tipo de contenido
            success: function (response) {
                console.log('Concepto creado correctamente:', response);
                cargarDatosConcepto();
                $('#createConceptoModal').modal('hide');
            },
            error: function (error) {
                console.error('Error al crear el Concepto:', error);
            }
        });
    } else {
        alert('Por favor, complete todos los campos requeridos.');
    }
});



//CARGAR CLIENTE EN TABLA CLIENTE
function cargarDatosConcepto() {
    $.ajax({
        url: '/Pert11PagoPersonal/RecargarConcepto', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        success: function (response) {
            console.log('Concepto:', response);
            // Clear the existing tbody content
            $('#bodycon').empty();

            // Iterate through the updated client data and append rows to tbody
            $.each(response, function (index, concepto) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + concepto.idConcepto + '</td>');
                row.append('<td align="center" class="client-cell">' + concepto.nombre + '</td>');

                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarConcepto(\'modalcon\',\'concbo\',\'conceptoID\',\'conceptodesc\'' +
                    concepto.IdConcepto + '\', \'' + concepto.nombre + '\')">Seleccionar</button>' +
                    '</td>');


                // Append the row to the tbody
                $('#bodycon').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del cliente:', error.responseText);
        }
    });
}
//$(document).ready(function () {
//    $('#createConceptoModal').on('hidden.bs.modal', function () {
//        // Vaciar los campos de Crear
//        cargarDatosConcepto();
//    });
//});
//Empleado


//Proceso para cerrar la ventana modal Proveedor
$(document).ready(function () {
    $('#modalemp').on('hidden.bs.modal', function () {
        $('#empcbo').prop('selectedIndex', 0);
        $('#proveedoroptionname').prop('selectedIndex', 0);
        $('#proveedoroptionid').prop('selectedIndex', 0);
        /*        cargarDatosproveedor();*/
        // Vaciar los campos de filtro
        $('#filtroRazonProv').val('');
        $('#filtroRUCProv').val('');
    });
});


//Proceso para cerrar la ventana modal proveedor



//function cargarDatosEmpleado() {
//    $.ajax({
//        url: '/Pert11PagoPersonal/RecargarEmpleado', // Reemplaza 'TuControlador' con el nombre real de tu controlador
//        type: 'GET',
//        success: function (response) {
//            /*console.log(Proveedor:', response);*/
//            // Clear the existing tbody content
//            $('#bodyprov').empty();

//            // Iterate through the updated client data and append rows to tbody
//            $.each(response, function (index, proveedor) {
//                var row = $('<tr>');
//                row.append('<td align="center" class="client-cell">' + proveedor.razon + '</td>');
//                row.append('<td align="center" class="client-cell">' + proveedor.ruc + '</td>');
//                /*                row.append('<td align="center" class="client-cell">' + proveedor.direccion + '</td>');*/
//                row.append('<td align="center" class="client-cell">' +
//                    '<button class="btn btn-primary" onclick="seleccionarProveedor(\'modalprov\',\'provcbo\',\'proveedorID\',\'proveedorruc\',\'proveedorrazon\',\'proveedordireccion\', \'' +
//                    proveedor.IdProveedor + '\', \'' + proveedor.ruc + '\', \'' + proveedor.razon + '\', \'' + proveedor.direccion + '\')">Seleccionar</button>' +
//                    '</td>');


//                // Append the row to the tbody
//                $('#bodyprov').append(row);
//            });
//        },
//        error: function (error) {
//            console.error('Error al cargar los datos del cliente:', error.responseText);
//        }
//    });
//}


$('#crearConceptoBtn').click(function () {
    if (camposRequeridosLlenos('createConceptoForm')) {
        var formData = new FormData();

        formData.append('txt_desc', $('#detalleconcepto').val() || '');


        $.ajax({
            url: '/Pert11PagoPersonal/CrearConcepto',
            type: 'POST',
            data: formData,
            processData: false, // Evita que jQuery convierta los datos en una cadena
            contentType: false, // Fuerza a jQuery a no configurar el tipo de contenido
            success: function (response) {
                console.log('Concepto creado correctamente:', response);
                cargarDatosConcepto();
                $('#createConceptoModal').modal('hide');
            },
            error: function (error) {
                console.error('Error al crear el Concepto:', error);
            }
        });
    } else {
        alert('Por favor, complete todos los campos requeridos.');
    }
});

//CARGAR CLIENTE EN TABLA CLIENTE
function cargarDatosConcepto() {
    $.ajax({
        url: '/Pert11PagoPersonal/RecargarConcepto', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        success: function (response) {
            /*console.log(Proveedor:', response);*/
            // Clear the existing tbody content
            $('#bodycon').empty();

            // Iterate through the updated client data and append rows to tbody
            $.each(response, function (index, concepto) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + concepto.desc + '</td>');

                /*                row.append('<td align="center" class="client-cell">' + proveedor.direccion + '</td>');*/
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarConcepto(\'modalcon\',\'concbo\',\'conceptoID\',\'conceptodesc\'' +
                    concepto.IdConcepto + '\', \'' + concepto.nombre + '\')">Seleccionar</button>' +
                    '</td>');


                // Append the row to the tbody
                $('#bodycon').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del cliente:', error.responseText);
        }
    });
}

function seleccionarEmpleado(modalId, selectId, idcod, idrazon, idempleado, nombre) {
    event.preventDefault();
    var proveedorIdInput = document.getElementById(idcod);
    proveedorIdInput.value = idempleado;
    /*    document.getElementById(idcod).innerText = idCliente;*/
    document.getElementById(idrazon).innerText = nombre;

    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover

    var proveedorNombreSpan = document.getElementById('empleadonombre');
    var removeEmpleadoSpan = document.getElementById('removeEmpleadoSpan');

    proveedorNombreSpan.style.display = 'inline-block';
    removeEmpleadoSpan.style.display = 'inline-block';

    // Ocultar el select
    var selectElement = document.getElementById('empcbo');
    selectElement.style.display = 'none';
}
function seleccionarConcepto(modalId, selectId, idcod, idrazon, idconcepto, nombre) {
    event.preventDefault();
    var proveedorIdInput = document.getElementById(idcod);
    proveedorIdInput.value = idconcepto;
    /*    document.getElementById(idcod).innerText = idCliente;*/
    document.getElementById(idrazon).innerText = nombre;

    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover

    var proveedorNombreSpan = document.getElementById('conceptodesc');
    var removeEmpleadoSpan = document.getElementById('removeConceptoSpan');

    proveedorNombreSpan.style.display = 'inline-block';
    removeEmpleadoSpan.style.display = 'inline-block';

    // Ocultar el select
    var selectElement = document.getElementById('concbo');
    selectElement.style.display = 'none';
}
function removeEmpleado() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('empleadonombre').innerText = '';
    document.getElementById('empleadoID').value = '';
    document.getElementById('empleadonombre').style.display = 'none';
    document.getElementById('removeEmpleadoSpan').style.display = 'none';
    var selectElement = document.getElementById('empcbo');
    selectElement.style.display = 'inline-block';

}

function removeConcepto() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('conceptodesc').innerText = '';
    document.getElementById('conceptoID').value = '';
    document.getElementById('conceptodesc').style.display = 'none';
    document.getElementById('removeConceptoSpan').style.display = 'none';
    var selectElement = document.getElementById('concbo');
    selectElement.style.display = 'inline-block';

}


function obtenerDatosDelComprobanteP() {
    var data = {
        IdPagoPersonal: parseInt(document.getElementById("IdPagoPersonalInput").value),
        IdEmpleado: document.getElementById("empleadoID").value,
        Tipo: document.getElementById("idtipoPago").value,
        Fecha: document.getElementById("fechaEmision").value,
        Mes: document.getElementById("mes").value,
        IdAutorizador: 3,
        IdPredio: parseInt(document.getElementById("Predio").value),
        IdCampaña: parseInt(document.getElementById("Campana").value),
        Estado: document.getElementById("idestado").value,
        IdConcepto: document.getElementById("conceptoID").value,
        Monto: document.getElementById("monto").value,
        IdEstado: 1,
        TxtEstado: "ACTIVO"
    };
    return data;
}
$(document).ready(function () {
    // Evento click para el botón "Guardar"


    $("#guardarP").click(function () {
        var dataToSend = obtenerDatosDelComprobanteP();
        enviarDatosGuardarP(dataToSend);
    });

    // Evento click para el botón "Guardar y Cerrar"
    $("#guardarycerrarP").click(function () {
        var dataToSend = obtenerDatosDelComprobanteP();
        enviarDatosGuardarYCerrarP(dataToSend);
    });

    $("#cancelarP").click(function () {
        window.location.href = '/Pert11PagoPersonal/pago_p_listado'; // Reemplaza 'otra_ruta' con la URL deseada
    });
});

function enviarDatosGuardarP(dataToSend) {


    var url = '/Pert11PagoPersonal/Guardar?';
    url += '&pagoId=' + dataToSend.IdPagoPersonal;
    url += '&IdEmpleado=' + dataToSend.IdEmpleado;
    url += '&Tipo=' + encodeURIComponent(dataToSend.Tipo);
    url += '&Fecha=' + encodeURIComponent(dataToSend.Fecha);
    url += '&Mes=' + encodeURIComponent(dataToSend.Mes);
    url += '&IdAutorizador=' + dataToSend.IdAutorizador;
    url += '&IdPredio=' + dataToSend.IdPredio;
    url += '&IdCampaña=' + dataToSend.IdCampaña;
    url += '&Estado=' + encodeURIComponent(dataToSend.Estado);
    url += '&IdConcepto=' + dataToSend.IdConcepto;
    url += '&Monto=' + dataToSend.Monto;
    url += '&IdEstado=' + dataToSend.IdEstado;
    url += '&TxtEstado=' + encodeURIComponent(dataToSend.TxtEstado);


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
            $('#IdPagoPersonalInput').val(response.IdPagoPersonal);

        }
    });
}

function enviarDatosGuardarYCerrarP(dataToSend) {


    // Verifica si la cantidad de elementos en el arreglo es mayor que 0

    var confirmar = confirm("¿Seguro que desea guardar y cerrar?");

    if (confirmar) {

        var url = '/Pert11PagoPersonal/Guardar?';
        url += '&pagoId=' + dataToSend.IdPagoPersonal;
        url += '&IdEmpleado=' + dataToSend.IdEmpleado;
        url += '&Tipo=' + encodeURIComponent(dataToSend.Tipo);
        url += '&Fecha=' + encodeURIComponent(dataToSend.Fecha);
        url += '&Mes=' + encodeURIComponent(dataToSend.Mes);
        url += '&IdAutorizador=' + dataToSend.IdAutorizador;
        url += '&IdPredio=' + dataToSend.IdPredio;
        url += '&IdCampaña=' + dataToSend.IdCampaña;
        url += '&Estado=' + encodeURIComponent(dataToSend.Estado);
        url += '&IdConcepto=' + dataToSend.IdConcepto;
        url += '&Monto=' + dataToSend.Monto;
        url += '&IdEstado=' + dataToSend.IdEstado;
        url += '&TxtEstado=' + encodeURIComponent(dataToSend.TxtEstado);


        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            /*data: JSON.stringify(dataToSend),*/
            success: function (response) {
                if (response.mensaje) {
                    window.location.href = '/Pert11PagoPersonal/pago_p_listado'; // Reemplaza 'otra_ruta' con la URL deseada
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

}










//Proceso para cerrar la ventana modal LOCATION
$(document).ready(function () {
    $('#modallocenvio').on('hidden.bs.modal', function () {
        $('#locenvcbo').prop('selectedIndex', 0);
        $('#locationenvoptionname').prop('selectedIndex', 0);
        EleccionCargaenv();

    });
});




function EleccionCargaenv() {
    var LocalizadorIdInput = document.getElementById('Locationenvid');
    var LocalizadortoIdInput = document.getElementById('Locationtoenvid');
    var valorLoc = LocalizadorIdInput.value;
    var valorLocto = LocalizadortoIdInput.value;

    if ((valorLoc == null && valorLocto == null) ||
        (valorLoc == '' && valorLocto == '') ||
        (valorLoc != null && valorLocto != null &&
            valorLoc != '' && valorLocto != '')) {
        cargarDatosLocalizacionenv(null);
    }
    else if (valorLoc != null && valorLoc != '') {
        cargarDatosLocalizacionenv(valorLoc);
    }
    else if (valorLocto != null && valorLocto != '') {
        cargarDatosLocalizacionenv(valorLocto);
    }

}
//CARGAR LOCATION GENERAL EN TABLA LOCATION GENERAL
function cargarDatosLocalizacionenv(idcod) {
    $.ajax({
        url: '/Pret10Envio/RecargarLocalizacion?id=' + idcod,
        type: 'GET',
        //data: formData,
        //processData: false,  // No procesar los datos (dejar que FormData lo haga)
        //contentType: false,  // No establecer contentType (FormData lo establece correctamente)
        success: function (response) {
            /*console.log("Contenido:response");*/
            // Limpiar el contenido existente de ambas tablas
            $('#bodysamelocenv1').empty();
            $('#bodysamelocenv2').empty();

            // Iterar a través de los datos para la primera tabla (localización)
            $.each(response, function (index, localizacion) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + localizacion.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacion.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacion.fechaN + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacion.tipol + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarLocationenv(\'modallocenv\',\'locenvcbo\',\'Locationenvid\',\'LocalizacionenvNombre\',\'' +
                    localizacion.idLocation + '\', \'' + localizacion.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodysamelocenv1').append(row);
            });

            // Iterar a través de los datos para la segunda tabla (localizaciónto)
            $.each(response, function (index, localizacionto) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + localizacionto.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacionto.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacionto.fechaN + '</td>');
                row.append('<td align="center" class="client-cell">' + localizacionto.tipol + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarLocationtoenv(\'modalltoenv\',\'ltoenvcbo\',\'Locationtoenvid\',\'LocalizaciontoenvNombre\',\'' +
                    localizacionto.idLocation + '\', \'' + localizacionto.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodysamelocenv2').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del Localizacion:', error.responseText);
        }
    });
}

//Cargar la selección de LOCATION
function seleccionarLocationenv(modalId, selectId, idcod, idtext, idLoc, nombreLoc) {
    event.preventDefault();
    var LocalizadorIdInput = document.getElementById(idcod);
    LocalizadorIdInput.value = idLoc;
    document.getElementById(idtext).innerText = nombreLoc;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var LocalizadorNombreSpan = document.getElementById('LocalizacionenvNombre');
    var removeLocalizadorSpan = document.getElementById('removeLocalizacionenvSpan');
    LocalizadorNombreSpan.innerText = nombreLoc;
    LocalizadorNombreSpan.style.display = 'inline-block';
    removeLocalizadorSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('locenvcbo');
    selectElement.style.display = 'none';
    EleccionCargaenv();

}

//El span LOCATION para volver al select
function removeLocationenv() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('LocalizacionenvNombre').innerText = '';
    document.getElementById('Locationenvid').value = null;
    document.getElementById('LocalizacionenvNombre').style.display = 'none';
    document.getElementById('removeLocalizacionenvSpan').style.display = 'none';
    var selectElement = document.getElementById('locenvcbo');
    selectElement.style.display = 'inline-block';
    EleccionCargaenv();

}





//LOCATIONTO
//Proceso para cerrar la ventana modal LOCATIONTO
$(document).ready(function () {
    $('#modalltoenv').on('hidden.bs.modal', function () {
        $('#ltoenvcbo').prop('selectedIndex', 0);

        EleccionCargaenv();
    });
});



//Cargar la selección de LOCATIONTO
function seleccionarLocationtoenv(modalId, selectId, idcod, idtext, idLocto, nombreLocto) {
    event.preventDefault();
    var LocalizadortoIdInput = document.getElementById(idcod);
    LocalizadortoIdInput.value = idLocto;
    document.getElementById(idtext).innerText = nombreLocto;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var LocalizadortoNombreSpan = document.getElementById('LocalizaciontoenvNombre');
    var removeLocalizadortoSpan = document.getElementById('removeLocalizaciontoenvSpan');
    LocalizadortoNombreSpan.innerText = nombreLocto;
    LocalizadortoNombreSpan.style.display = 'inline-block';
    removeLocalizadortoSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('ltoenvcbo');
    selectElement.style.display = 'none';
    EleccionCargaenv();
}

//El span LOCATIONTO para volver al select
function removeLocationtoenv() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('LocalizaciontoenvNombre').innerText = '';
    document.getElementById('Locationtoenvid').value = null;
    document.getElementById('LocalizaciontoenvNombre').style.display = 'none';
    document.getElementById('removeLocalizaciontoenvSpan').style.display = 'none';
    var selectElement = document.getElementById('ltoenvcbo');
    selectElement.style.display = 'inline-block';
    EleccionCargaenv();
}



//empleado envio

//Proceso para cerrar la ventana modal CHOFER
$(document).ready(function () {
    $('#modalempenv').on('hidden.bs.modal', function () {
        $('#empenvcbo').prop('selectedIndex', 0);

        cargarDatosempenv();
    });
});



//CARGAR CHOFER EN TABLA CHOFER
function cargarDatosempenv() {
    var idsempleado = arregloenvemp.map(function (campaña) {
        return campaña.idempleado;
    });
    $.ajax({
        url: '/Pret10Envio/Recargarempenv', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { listaIds: idsempleado }, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodyempenv').empty();
            $.each(response, function (index, EmpleadoEnv) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + EmpleadoEnv.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoEnv.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoEnv.cargo + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoEnv.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoEnv.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarempenv(\'modalempenv\',\'empenvcbo\',\'empenvId\',\'empenvNombre\',\'' +
                    EmpleadoEnv.idempleado + '\', \'' + EmpleadoEnv.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');
                $('#bodyempenv').append(row);
            });

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });
}


//Cargar la selección de Chofer
function seleccionarempenv(modalId, selectId, idcod, idtext, idempext, nombreempext) {
    event.preventDefault();
    var choferIdInput = document.getElementById(idcod);
    choferIdInput.value = idempext;
    document.getElementById(idtext).innerText = nombreempext;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var choferNombreSpan = document.getElementById('empenvNombre');
    var removeChoferSpan = document.getElementById('removeempenvSpan');
    choferNombreSpan.innerText = nombreempext;
    choferNombreSpan.style.display = 'inline-block';
    removeChoferSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('empenvcbo');
    selectElement.style.display = 'none';
    $.ajax({
        url: '/Pret10Envio/CargarDatosEmpleado',
        type: 'GET',
        data: { idEmpleado: idempext },
        success: function (data) {
            if (!data) {
                // Si data es null o undefined
                document.getElementById('nrodocempleadoenv').value = "";
                document.getElementById('categoriaempleadoenv').value = "";
                document.getElementById('condicionempleadoenv').value = "";
                document.getElementById('telefonoempleadoenv').value = "";
            } else {
                // Si data no es null o undefined
                document.getElementById('nrodocempleadoenv').value = data.nrodoc || "";
                document.getElementById('categoriaempleadoenv').value = data.cargo || "";
                document.getElementById('condicionempleadoenv').value = data.condicion || "";
                document.getElementById('telefonoempleadoenv').value = data.telefono || "";

            }
        },
        error: function (error) {
            console.error('Error al cargar datos del empleado:', error.responseText);

        }
    });

}

//El span cliente para volver al select
function removeempenv() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('nrodocempleadoenv').value = '';
    document.getElementById('categoriaempleadoenv').value = '';
    /*document.getElementById('salarioempleado').value = 1;*/
    document.getElementById('empenvId').value = 0;
    document.getElementById('condicionempleadoenv').value = '';
    document.getElementById('telefonoempleadoenv').value = '';
    ;
    document.getElementById('empenvNombre').style.display = 'none';
    document.getElementById('removeempenvSpan').style.display = 'none';
    var selectElement = document.getElementById('empenvcbo');
    selectElement.style.display = 'inline-block';
}

//AREGLO EXT EMP
var arregloenvemp = [];



function agregarenvemp() {
    var idempleado = parseInt($("#empenvId").val());
    /*var salario= parseFloat($("#salarioempleado").val());*/
    var nrodoc = ($("#nrodocempleadoenv").val());
    var categoria = ($("#categoriaempleadoenv").val());
    var condicion = ($("#condicionempleadoenv").val());
    var telefono= ($("#telefonoempleadoenv").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idempleado) || idempleado <= 0 /*|| isNaN(salario) ||   0 >(salario)*/) {
        alert("Por favor, ingrese valores numéricos para la extracción por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }
    $.ajax({
        url: "/Pret10Envio/EmpleadoEnv",
        type: "GET",
        data: { empleadoID: idempleado },
        success: function (response) {
            if (response != null) {


                arregloenvemp.push({
                    idempleado: idempleado,
                    txtempleado: response.txtnombre,
                    telefono: response.telefono,
                    /*                    salario: salario,*/
                    nrodoc: nrodoc,
                    categoria: categoria,
                    condicion: condicion
                });

                actualizartablaenvemp();
                removeempenv();


            } else {
                alert("Tipo de Arbol no encontrado");
            }
        },
        error: function (response) {
            alert("Error al obtener los datos del Tipo de Arbol");
        }
    });


}

// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarenvemp(idenvemp) {
    var campanaenvemp = arregloenvemp.find(function (empleado) {
        return empleado.idempleado === idenvemp;
    });

    if (campanaenvemp) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este registro de la lista?");

        if (confirmar) {
            var campTAEnvindex = arregloenvemp.findIndex(function (campTA) {
                return campTA.idempleado === idenvemp;
            });

            if (campTAEnvindex !== -1) {
                arregloenvemp.splice(campTAEnvindex, 1);
                actualizartablaenvemp();
            }
        }
    } else {
        alert("Registro no encontrado en la lista.");
    }
}


//Función para abrir el modal de edición
function editarempleadosEnvio(idextemp) {
    var empleadosenvio = arregloenvemp.find(function (empleado) {
        return empleado.idempleado === idextemp;
    });
    if (empleadosenvio) {
        /* document.getElementById("nuevosalarioextemp").value = empleadosextraccion.salario;*/
        document.getElementById("IdEditarempenv").value = idextemp;
        document.getElementById("nuevacategoriaenvemp").value = empleadosenvio.categoria;
        document.getElementById("nuevacondicionenvemp").value = empleadosenvio.condicion;
        document.getElementById("nuevodocenvemp").value = empleadosenvio.nrodoc;
        openSecondModal('editarEMPENVModal');
    } else {
        alert("Empleado no encontrado en la lista.");
    }
}

// Función para aplicar los cambios desde el modal y guardarlos
//function aplicarCambiosExtemp() {
//    var idcampTA = parseInt(document.getElementById("IdEditarempext").value);
//    var campanaTAExt = arregloextemp.find(function (prod) {
//        return prod.idempleado=== idcampTA;
//    });
//    /*var nuevosalario= parseFloat(document.getElementById("nuevosalarioextemp").value);*/


//    /*if (nuevosalario>0 ) {*/
//        var confirmar = confirm("¿Deseas aplicar los cambios y guardarlos?");

//        if (confirmar) {
//            // Aplicar parseFloat con toFixed(2) a las variables
//            var salario= parseFloat(nuevosalario);


//            // Asignar los valores a las propiedades del objeto producto
//            campanaTAExt.salario = salario;


//            actualizartablaextemp();
//            cerrarModal('editarEMPEXTModal');
//        } else {
//            alert("Edición cancelada. Los cambios no se aplicaron.");
//        }
//    //} else {
//    //    alert("Por favor, ingresa valores numéricos válidos para los campos.");
//    //}
//}



function actualizartablaenvemp() {
    // Limpiar la tabla
    $("#empSeleccionadosEnv").empty();

    // Recorrer el arreglo de tipo de árboles seleccionados y volver a agregarlos a la tabla
    arregloenvemp.forEach(function (Empleado) {
        var newRow = `
            <tr id='${Empleado.idempleado}'>
                <td class="text-center">${Empleado.txtempleado}</td>
                <td class="text-center">${Empleado.condicion}</td>
                <td class="text-center">${Empleado.telefono}</td>
                <td class="text-center">${Empleado.nrodoc}</td>
                <td class="text-center">${Empleado.categoria}</td>
                <td colspan='2' class="text-center">
                    
                    <button type='button'  class="btn btn-danger btn-sm" onclick='eliminarenvemp(${Empleado.idempleado})'><i class="fas fa-trash alt"></i></button>
                </td>
            </tr>`;

        $("#empSeleccionadosEnv").append(newRow);
    });
    cargarDatosempenv();

}


/*<button type='button' onclick='editarempleadosEnvio(${Empleado.idempleado})'>Editar</button>*/


var archivosEnv = [];

function abrirModalArchivosenv() {
    // Limpiar la tabla del modal y mostrar los archivos actuales
    mostrarArchivosenvEnTabla();
    // Mostrar el modal
    var nuevoArchivoenvInput = document.getElementById('nuevoArchivoenv');
    nuevoArchivoenvInput.value = '';
    $('#modalArchivosenv').modal('show');

}

function mostrarArchivosenvEnTabla() {
    var tablaArchivosenvBody = document.getElementById('tablaArchivosenvBody');
    tablaArchivosenvBody.innerHTML = '';

    for (var i = 0; i < archivosEnv.length; i++) {
        agregarFilaATablaenv(archivosEnv[i]);
    }
}

function agregarArchivoenv() {
    var nuevoArchivoInput = document.getElementById('nuevoArchivoenv');
    var archivosNuevosenv = nuevoArchivoInput.files;

    if (archivosNuevosenv.length == 0) {
        alert('Selecciona algún archivo');
    } else {

        // Añadir cada nuevo archivo a la tabla
        for (var i = 0; i < archivosNuevosenv.length; i++) {
            var archivoenv = archivosNuevosenv[i];
            agregarFilaATablaenv(archivoenv);
            archivosEnv.push(archivoenv);
        }

        // Limpiar el campo de archivos para permitir agregar otro
        nuevoArchivoInput.value = '';
    }
}

function agregarFilaATablaenv(archivoenv) {
    var tablaArchivosBody = document.getElementById('tablaArchivosenvBody');
    var fila = document.createElement('tr');

    var celdaArchivo = document.createElement('td');
    var enlaceArchivo = document.createElement('a');
    enlaceArchivo.textContent = archivoenv.name;

    // Verificar si el archivo es comprimido (RAR)
    if (archivoenv.name.toLowerCase().endsWith('.rar')) {
        enlaceArchivo.href = '#'; // Puedes proporcionar un enlace válido si tienes una URL de descarga
        enlaceArchivo.onclick = function () {
            descargarArchivoenv(archivoenv);
        };
    } else {
        enlaceArchivo.href = URL.createObjectURL(archivoenv);
        enlaceArchivo.target = '_blank';
    }

    celdaArchivo.appendChild(enlaceArchivo);

    // Obtener la extensión del archivo
    var extension = obtenerExtension(archivoenv.name);
    var celdaExtension = document.createElement('td');
    celdaExtension.textContent = extension;

    var celdaAcciones = document.createElement('td');
    var botonEliminar = document.createElement('button');
    botonEliminar.className = 'btn btn-danger btn-sm';
    botonEliminar.textContent = 'Eliminar';
    botonEliminar.onclick = function () {
        eliminarArchivoenv(archivoenv);
    };

    celdaAcciones.appendChild(botonEliminar);

    fila.appendChild(celdaArchivo);
    fila.appendChild(celdaExtension);
    fila.appendChild(celdaAcciones);

    tablaArchivosBody.appendChild(fila);
}

function descargarArchivoenv(archivoenv) {
    // Aquí debes proporcionar la lógica para la descarga del archivo, por ejemplo, redirigiendo a una URL válida de descarga.
    // Puedes utilizar window.location.href para cambiar la ubicación actual a la URL de descarga.
    // Ejemplo: window.location.href = 'ruta/descargar.php?archivo=' + encodeURIComponent(archivo.name);
    alert('Descargando: ' + archivoenv.name);
}



// Función para obtener la extensión de un archivo a partir de su nombre
function obtenerExtensionenv(nombreArchivoenv) {
    var partes = nombreArchivoenv.split('.');
    if (partes.length > 1) {
        return partes[partes.length - 1];
    } else {
        return '';
    }
}

function eliminarArchivoenv(archivoenv) {
    // Elimina el archivo de la tabla
    var index = archivosEnv.indexOf(archivoenv);
    if (index !== -1) {
        archivosEnv.splice(index, 1);
        // Actualiza la tabla en el modal
        mostrarArchivosenvEnTabla();
    }
}
function vaciarArregloYActualizarTablaenv() {
    archivosEnv = []; // Vaciar el arreglo de archivos
    abrirModalArchivosenv(); // Actualizar la tabla en el modal
}
































//CAMPAÑAEXTRACCION

// Proceso para cerrar la ventana modal Campaña Extraccion
$(document).ready(function () {
    $('#modalextenv').on('hidden.bs.modal', function () {
        $('#extenvcbo').prop('selectedIndex', 0);
        cargarDatosextenv();
    });


  


});


// CARGAR INVERSIONISTA EN TABLA INVERSIONISTA
function cargarDatosextenv() {
    $.ajax({
        url: '/Pret10Envio/Recargarextenv',
        type: 'GET',
        success: function (response) {
            // Limpia el tbody actual
            $('#bodyextenv').empty();
            $.each(response, function (index, extraccionenv) {
                var fechaFormateada = new Date(extraccionenv.fechaini).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });

                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + extraccionenv.codigo + '</td>');
                row.append('<td align="center" class="client-cell">' + extraccionenv.nrositio + '</td>');
                row.append('<td align="center" class="client-cell">' + fechaFormateada + '</td>');

                var button = $('<button>').addClass('btn btn-primary')
                    .text('Seleccionar')
                    .on('click', function () {
                        seleccionarextenv('modalextenv', 'extenvcbo', extraccionenv.id, extraccionenv.codigo);
                    });

                row.append($('<td>').addClass('client-cell text-center').append(button));

                // Append the row to the tbody
                $('#bodyextenv').append(row);

            });

        },

        error: function (error) {
            console.error('Error al cargar los datos de la campaña:', error.responseText);
        }
    });

}


// Cargar la selección de Inversionista
function seleccionarextenv(modalId, selectId, idextenv, nombrecampext) {
    event.preventDefault();
    $.ajax({
        url: '/Pret10Envio/VerificarExt',
        type: 'GET',
        data: { idExt: idextenv },
        success: function (response) {
            document.getElementById('codCam').value = response.codCampana;
            document.getElementById('unCat').value = response.unCatastral;
            console.log('Valor de idtemporalCampana:', response.idtemporalExtraccion);
            resolve(response.idtemporalExtraccion);
        },
        error: function (error) {
            console.error('Error al cargar los datos de la campaña de extracción:', error.responseText);
            reject(error);
        }
    });
    console.log(idextenv);
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del inversionista y el botón para remover
    var inversionistaNombreSpan = document.getElementById('extenvNombre');
    var removeInversionistaSpan = document.getElementById('removeExtenvSpan');
    inversionistaNombreSpan.innerText = nombrecampext;
    inversionistaNombreSpan.style.display = 'inline-block';
    removeInversionistaSpan.style.display = 'inline-block';
    // Ocultar el seleCAMPEXT
    var selectElement = document.getElementById('extenvcbo');
    selectElement.style.display = 'none';
    // Función para seleccionar la campaña de extracción
    function seleccionarExtEnv(idextenv) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Pret10Envio/SeleccionarExtEnv',
                type: 'GET',
                data: { idExtraccion: idextenv },
                success: function (response) {
                    document.getElementById('codCam').value = response.codCampana;
                    document.getElementById('unCat').value = response.unCatastral;
                    console.log('Valor de idtemporalCampana:', response.idtemporalExtraccion);
                    resolve(response.idtemporalExtraccion);
                },
                error: function (error) {
                    console.error('Error al cargar los datos de la campaña de extracción:', error.responseText);
                    reject(error);
                }
            });
        });
    }
    cargarDatosextenv();
    var idsExtraccion = ExtracciontipoArbolEnv.map(function (extraccion) {
        return extraccion.idtipoarbol;
    });
    // Función para obtener las campañas
    function obtenerExtraccion() {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Pret10Envio/ObtenerExtraccion',
                type: 'GET',
                data: { idsExtracciones: null }, // Pasar la lista de IDs de campañas al servidor
                success: function (data) {
                    $('#extracciontaselect').empty();

                    // Seleccionar automáticamente el primer elemento
                    var primerElemento = null;

                    $.each(data, function (index, item) {
                        if (item.nroarbol === 0) {
                        } else {
                            // Si nroarbol no es 0, agregarlo al select principal
                            $('#extracciontaselect').append($('<option>').text(item.txtdesc).attr('value', item.idta));

                            if (index === 0) {
                                primerElemento = item.idta;
                            }
                        }
                    });

                    // Si hay más de un elemento en el select principal, activar el evento change
                    if (data.length > 1) {
                        $('#extracciontaselect').change(function () {
                            var taId = $(this).val();
                            cargarDatosExtraccion(taId);
                        });
                    }

                    // Ejecutar la carga de datos de la campaña
                    cargarDatosExtraccion(primerElemento)
                        .then(resolve)  // Resuelve la promesa de obtenerCampañas
                        .catch(reject);  // Rechaza la promesa de obtenerCampañas en caso de error
                },
                error: function (error) {
                    console.error('Error al obtener campañas:', error.responseText);
                    reject(error);
                }
            });
        });
    }

    // Función para cargar datos de campaña
    function cargarDatosExtraccion(taId) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Pret10Envio/CargarDatosExtraccion',
                type: 'GET',
                data: { taid: taId },
                success: function (data) {
                    if (data == null) {
                        document.getElementById('nombretaenv').value = "";
                        document.getElementById('nrotrozastaenv').value = "";

                    } else {
                        document.getElementById('nombretaenv').value = data.txtdesc;
                        document.getElementById('nrotrozastaenv').setAttribute("max", data.nrotrozas);
                        document.getElementById('nrotrozastaenv').value = data.nrotrozas;
                        //document.getElementById('nroarboltaenv').value = data.nroarbol;
                        //document.getElementById('nroarboltaenv').setAttribute("max", data.nroarbol);
                        //document.getElementById('nuevonroarbolTAEnv').value = data.nroarbol;
                        //document.getElementById('nuevonroarbolTAEnv').setAttribute("max", data.nroarbol);

                        // Resolve inside the success callback
                        resolve(data);
                    }
                },
                error: function (error) {
                    console.error('Error al cargar campaña tipo arbol:', error.responseText);

                    // Reject inside the error callback
                    reject(error);
                }
            });
        });
    }

    // Lógica principal
    seleccionarExtEnv(idextenv)
        .then(obtenerExtraccion)
        .catch(function (error) {
            console.error('Error:', error);
        });




}

// El span inversionista para volver al select
function removeextenv() {
    event.preventDefault();
    // Quitar el valor del inversionista y ocultar el span
    document.getElementById('extenvNombre').innerText = '';
    document.getElementById('extenvNombre').style.display = 'none';
    document.getElementById('removeExtenvSpan').style.display = 'none';
    document.getElementById('codCam').value = '';
    document.getElementById('unCat').value = '';
    var selectElement = document.getElementById('extenvcbo');
    selectElement.style.display = 'inline-block';
    document.getElementById('nombretaenv').value = '';
    $('#extracciontaselect').empty();
    ExtracciontipoArbolEnv.splice(0, ExtracciontipoArbolEnv.length);

    actualizarTablaTipoArbolSeleccionadosEnv();
    $.ajax({
        url: "/Pret10Envio/removeIdTemporalExtraccion",
        type: "GET",
        success: function (response) {
            if (response.mensaje == null) {
            } else {
                alert(response.mensaje);
            }
        },
        error: function (response) {
        }
    });
    cargarDatosextenv();
}




//CAMPAÑA
// Declarar el arreglo para almacenar los CampañaTipoArbol
var ExtracciontipoArbolEnv= [];


//BOTONES DE TODA LA VISTA CAMPAÑA I LISTADO CAMPAÑA
$(document).ready(function () {
    $("#guardarEnv").click(function () {
        enviarDatosEnvGuardar();
    });
    $("#guardarycerrarEnv").click(function () {
        enviarDatosEnvGuardaryCerrar();
    });
    $("#cancelarEnv").click(function () {
        $.ajax({
            url: "/Pret10Envio/CerrarExtraccionEnv",
            type: "GET",
            success: function (response) {
                if (response.mensaje == null) {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.mensaje);
                }
            },
            error: function (response) {
            }
        });
    });
    

    $("#cancelarRec").click(function () {
        $.ajax({
            url: "/Pret11Recepcion/CerrarRecepcion",
            type: "GET",
            success: function (response) {
                if (response.mensaje == null) {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.mensaje);
                }
            },
            error: function (response) {
            }
        });
    });

});



function cargarDatosTipoArbolEnv() {
    console.log('a');
    document.getElementById('nrotrozastaenv').value = 1;
    document.getElementById('alturataenv').value = 1;
    document.getElementById('diametrotaenv').value = 1;
    document.getElementById('comentariotaenv').value = '';
    function seleccionarExtEnv() {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Pret10Envio/SeleccionarExtEnv',
                type: 'GET',
                data: {
                    idExtraccion: null
                },
                success: function (response) {
                    var idtemporalCampana = response;
                    console.log('Valor de idtemporalCampana:', idtemporalCampana);
                    resolve(idtemporalCampana);
                },
                error: function (error) {
                    console.error('Error al cargar los datos de la campaña de extracción:', error.responseText);
                    reject(error);
                }
            });
        });
    }
    var idsarregloextraccion = ExtracciontipoArbolEnv.map(function (extraccion) {
        return {
            idTipoArbol: extraccion.idTipoArbol,
            nroTrozas: extraccion.nrotrozas
        };
    });
    console.log(idsarregloextraccion)

    // Función para obtener las campañas
    function obtenerExtraccion(idsarregloextraccion) {
        $.ajax({
            url: '/Pret10Envio/ObtenerExtraccion',
            type: 'GET',
            data: { idsExtraccionesJson: JSON.stringify(idsarregloextraccion) },

            traditional: true,  // Añade esto para indicar que se enviará una lista tradicional de parámetros
            success: function (data) {
                $('#extracciontaselect').empty();

                // Seleccionar automáticamente el primer elemento
                var primerElemento = null;

                $.each(data, function (index, item) {
                    if (item.nroarbol == 0) {
                    } else {
                        // Si nroarbol no es 0, agregarlo al select principal
                        $('#extracciontaselect').append($('<option>').text(item.txtdesc).attr('value', item.idta));

                        if (index == 0) {
                            primerElemento = item.idta;
                        }
                    }
                });

                // Si hay más de un elemento en el select principal, activar el evento change
                if (data.length > 1) {
                    $('#extracciontaselect').change(function () {
                        var taId = $(this).val();
                        cargarDatosExtraccion(taId);
                    });
                }

                // Ejecutar la carga de datos de la campaña
                cargarDatosExtraccion(primerElemento)
            },
            error: function (error) {
                console.error('Error al obtener campañas:', error.responseText);
            }
        });

    }

    // Función para cargar datos de campaña
    // ...

    // Función para cargar datos de campaña
    function cargarDatosExtraccion(taId) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: '/Pret10Envio/CargarDatosExtraccion',
                type: 'GET',
                data: { taid: taId },
                success: function (data) {
                    if (data == null) {
                        document.getElementById('nombretaenv').value = "";
                        document.getElementById('nrotrozastaenv').value = "";
                        //document.getElementById('nroarboltaenv').value = "";
                        //document.getElementById('nuevonroarbolTAEnv').value = "";

                    } else {
                        document.getElementById('nombretaenv').value = data.txtdesc;
                        document.getElementById('nrotrozastaenv').setAttribute("max", data.nrotrozas);
                        document.getElementById('nrotrozastaenv').value = data.nrotrozas;
                        //document.getElementById('nroarboltaenv').value = data.nroarbol;
                        //document.getElementById('nroarboltaenv')
                        //document.getElementById('nuevonroarbolTAEnv').value = data.nroarbol;
                        //document.getElementById('nuevonroarbolTAEnv').setAttribute("max", data.nroarbol);


                        // Resolve inside the success callback
                        resolve(data);
                    }
                },
                error: function (error) {
                    console.error('Error al cargar campaña tipo arbol:', error.responseText);

                    // Reject inside the error callback
                    reject(error);
                }
            });
        });
    }

// ...


    seleccionarExtEnv().then(function () {
        obtenerExtraccion(idsarregloextraccion);
    }).catch(function (error) {
        console.error('Error:', error);
    });


}

function actualizarTablaTipoArbolSeleccionadosEnv() {
    // Limpiar la tabla
    $("#tipoArbolSeleccionadosEnv").empty();

    // Recorrer el arreglo de tipo de árboles seleccionados y volver a agregarlos a la tabla
    ExtracciontipoArbolEnv.forEach(function (tipoArbol) {
        var newRow = `
            <tr class="text-center" id='${tipoArbol.idunico}'>
                <td class="text-center">${tipoArbol.nombreTipoarbol}</td>
                <td class="text-center">${tipoArbol.altura}</td>
                <td class="text-center">${tipoArbol.diametro}</td>
                <td class="text-center">${tipoArbol.nrotrozas}</td>
                <td class="text-center">${tipoArbol.comentario}</td>
                <td colspan='2' class="text-center">
                    <button class="btn btn-primary btn-sm" type='button' onclick='editarTipoArbolEnv("${tipoArbol.idunico}")'>  <i class="fas fa-edit"></i> </button>
                    <button type='button' class="btn btn-danger btn-sm" onclick='eliminarTipoArbolEnv("${tipoArbol.idunico}")'><i class="fas fa-trash alt"></i></button>
                </td>
            </tr>`;

        $("#tipoArbolSeleccionadosEnv").append(newRow);
    });
    

    // Cargar datos de tipo de árbol (asumiendo que esta función se encarga de cargar esos datos)
    cargarDatosTipoArbolEnv();
    actualizarTotalesTAEnv();
    //function seleccionarCampExt() {
    //    return new Promise(function (resolve, reject) {
    //        $.ajax({
    //            url: '/Pret07Extraccion/SeleccionarCampExt',
    //            type: 'GET',
    //            data: { idtCampana: null},
    //            success: function (response) {
    //                var idtemporalCampana = response;
    //                console.log('Valor de idtemporalCampana:', idtemporalCampana);
    //                resolve(idtemporalCampana);
    //            },
    //            error: function (error) {
    //                console.error('Error al cargar los datos de la campaña de extracción:', error.responseText);
    //                reject(error);
    //            }
    //        });
    //    });
    //}
    //var idsCampañas = CampanatipoArbolExt.map(function (campaña) {
    //    return campaña.idTipoArbol;
    //});
    //// Función para obtener las campañas
    //function obtenerCampañas(idsCampañas) {
    //    return new Promise(function (resolve, reject) {
    //        $.ajax({
    //            url: '/Pret07Extraccion/ObtenerCampanas',
    //            type: 'GET',
    //            data: { listaIds: idsCampañas }, // Pass the list of campaign IDs to the server
    //            success: function (data) {
    //                $('#campanataselect').empty();

    //                // Seleccionar automáticamente el primer elemento
    //                var primerElemento = null;

    //                $.each(data, function (index, item) {
    //                    $('#campanataselect').append($('<option>').text(item.txtdesc).attr('value', item.idta));

    //                    if (index === 0) {
    //                        primerElemento = item.idta;
    //                    }
    //                });

    //                // Si hay más de un elemento, activar el evento change
    //                if (data.length > 1) {
    //                    $('#campanataselect').change(function () {
    //                        var taId = $(this).val();
    //                        cargarDatosCampana(taId);
    //                    });
    //                }

    //                // Ejecutar la carga de datos de la campaña
    //                cargarDatosCampana(primerElemento)
    //                    .then(resolve)  // Resuelve la promesa de obtenerCampañas
    //                    .catch(reject);  // Rechaza la promesa de obtenerCampañas en caso de error
    //            },
    //            error: function (error) {
    //                console.error('Error al obtener campañas:', error.responseText);
    //                reject(error);
    //            }
    //        });
    //    });
    /* }*/

    // Función para cargar datos de campaña
    //function cargarDatosCampana(taId) {
    //    return new Promise(function (resolve, reject) {
    //        $.ajax({
    //            url: '/Pret07Extraccion/CargarDatosCampana',
    //            type: 'GET',
    //            data: { taid: taId },
    //            success: function (data) {
    //                console.log(data);
    //                document.getElementById('nombreta').value = data.txtdesc;
    //                document.getElementById('nroarbolta').value = data.nroarbol;
    //                resolve(data);
    //            },
    //            error: function (error) {
    //                console.error('Error al cargar campaña tipo arbol:', error.responseText);
    //                reject(error);
    //            }
    //        });
    //    });
    //}

    //// Lógica principal
    //seleccionarCampExt()
    //    .then(obtenerCampañas)
    //    .catch(function (error) {
    //        console.error('Error:', error);
    //    });
    // Puedes agregar lógica adicional aquí según tus necesidades
}

//function cargarDatosCampExt(response) {
//    // Clear the existing tbody content
//    $('#bodycampext').empty();

//    // Iterate through the updated campext data and append rows to tbody
//    $.each(response, function (index, campext) {
//        var row = $('<tr>');
//        row.append('<td align="center" class="client-cell">' + campext.codigo + '</td>');
//        row.append('<td align="center" class="client-cell">' + campext.nrositio + '</td>');
//        row.append('<td align="center" class="client-cell">' + campext.fechaini + '</td>');
//        row.append('<td align="center" class="client-cell">' +
//            '<button class="btn btn-primary" onclick="seleccionarcampext(\'modalcampext\',\'campextcbo\',\'' +
//            campext.id + '\', \'' + campext.codigo + '\')">Seleccionar</button>' +
//            '</td>');

//        // Append the row to the tbody
//        $('#bodycampext').append(row);
//    });
//}

// Función para añadir un producto a la lista de productos seleccionados
function agregarCampanataEnv() {
    var idtipoarbol = parseInt($("#extracciontaselect").val());
    var altura = parseFloat($("#alturataenv").val());
    var diametro = parseFloat($("#diametrotaenv").val());
    var nrotrozas = parseInt($("#nrotrozastaenv").val());
    var comentario = ($("#comentariotaenv").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idtipoarbol) || idtipoarbol <= 0 || isNaN(altura) || isNaN(diametro) || isNaN(nrotrozas) || !Number.isInteger(nrotrozas)
        || nrotrozas <= 0 || altura <= 0 || diametro <= 0) {
        alert("Por favor, ingrese valores numéricos para la extracción por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }
    $.ajax({
        url: "/Pret10Envio/TipoArbol",
        type: "GET",
        data: { idtipoarbol: idtipoarbol },
        success: function (response) {
            if (response != null) {


                ExtracciontipoArbolEnv.push({
                    idunico: response.idUnico,
                    idTipoArbol: response.idTipoArbol,
                    nombreTipoarbol: response.nombretipoarbol,
                    altura: altura,
                    diametro: diametro,
                    nrotrozas: nrotrozas,
                    comentario: comentario
                });

                actualizarTablaTipoArbolSeleccionadosEnv();
            } else {
                alert("Tipo de Arbol no encontrado");
            }
        },
        error: function (response) {
            alert("Error al obtener los datos del Tipo de Arbol");
        }
    });

    return false;
}

function actualizarTotalesTAEnv() {



}

// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarTipoArbolEnv(idTipoArbolEnv) {
    if (typeof idTipoArbolEnv !== 'string') {
        console.error('El ID no es una cadena.');
        console.error(idTipoArbolEnv);
        return;
    }
    var campanaTAExt = ExtracciontipoArbolEnv.find(function (campTA) {
        return campTA.idunico === idTipoArbolEnv;
    });
    console.log(idTipoArbolEnv);
    console.log("conj" + campanaTAExt);
    if (campanaTAExt) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este registro de la lista?");

        if (confirmar) {
            var campTAExtindex = ExtracciontipoArbolEnv.findIndex(function (campTA) {
                return campTA.idunico === idTipoArbolEnv;
            });

            if (campTAExtindex !== -1) {
                ExtracciontipoArbolEnv.splice(campTAExtindex, 1);
                actualizarTablaTipoArbolSeleccionadosEnv();
            }
        }
    } else {
        alert("Registro no encontrado en la lista.");
    }
}


//Función para abrir el modal de edición
function editarTipoArbolEnv(idTipoArbolEnv) {
    var extracciontipoarbolenv = ExtracciontipoArbolEnv.find(function (campTA) {
        return campTA.idunico === idTipoArbolEnv;
    });
    if (extracciontipoarbolenv) {
        document.getElementById("IdEditarTAEnv").value = idTipoArbolEnv;
        document.getElementById("TAExtinputenv").value = extracciontipoarbolenv.nombreTipoarbol;
        document.getElementById("nuevaalturaTAEnv").value = extracciontipoarbolenv.altura;
        document.getElementById("nuevodiametroTAEnv").value = extracciontipoarbolenv.diametro;
        document.getElementById("nuevanrotrozasTAEnv").value = extracciontipoarbolenv.nrotrozas;
        document.getElementById("comentarioTAEnv").value = extracciontipoarbolenv.comentario;
        openSecondModal('editarTAENVModal');
    } else {
        alert("Producto no encontrado en la lista.");
    }
}

// Función para aplicar los cambios desde el modal y guardarlos
function aplicarCambiosTAEnv() {

    var idcampTA = (document.getElementById("IdEditarTAEnv").value);

    var campanaTAExt = ExtracciontipoArbolEnv.find(function (prod) {
        return prod.idunico === idcampTA;
    });
    var nuevaaltura = parseFloat(document.getElementById("nuevaalturaTAEnv").value);
    var nuevodiametro = parseFloat(document.getElementById("nuevodiametroTAEnv").value);
    var nuevanrotrozas = parseInt(document.getElementById("nuevanrotrozasTAEnv").value);
    var comentarionuevo = (document.getElementById("comentarioTAEnv").value);
    if (isNaN(nuevaaltura) || isNaN(nuevodiametro) || isNaN(nuevanrotrozas)  || !Number.isInteger(nuevanrotrozas)
         || nuevanrotrozas <= 0 || nuevodiametro <= 0 || nuevaaltura <= 0) {
        alert("Por favor, ingrese valores numéricos para la extracción por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }
    if ( !isNaN(nuevanrotrozas)) {
        var confirmar = confirm("¿Deseas aplicar los cambios y guardarlos?");

        if (confirmar) {
            // Aplicar parseFloat con toFixed(2) a las variables
            var altura = Number(nuevaaltura);
            var diametro = Number(nuevodiametro);
            var nrotrozas = nuevanrotrozas;
            var comentario = (comentarionuevo);

            // Asignar los valores a las propiedades del objeto producto
            campanaTAExt.altura = altura;
            campanaTAExt.diametro = diametro;
            campanaTAExt.nrotrozas = nrotrozas;
            campanaTAExt.comentario = comentario;


            actualizarTablaTipoArbolSeleccionadosEnv();
            cerrarModal('editarTAENVModal');
        } else {
            alert("Edición cancelada. Los cambios no se aplicaron.");
        }
    } else {
        alert("Por favor, ingresa valores numéricos válidos para los campos.");
    }
}



function enviarDatosEnvGuardar() {
    if (arregloenvemp.length > 0) {
        if (ExtracciontipoArbolEnv.length > 0) {


            if (camposRequeridosLlenos('FormEnvio')) {
                var formData = new FormData();
                var checkbox = document.getElementById('checkEstadoEnvio');

                if (checkbox.checked) {
                    var checkb = true;
                } else {
                    var checkb = false;

                }
                formData.append('FechaEnvio', $('#fechaenv').val());
                formData.append('idlocation', parseInt($('#Locationenvid').val()));
                formData.append('idlocationto', parseInt($('#Locationtoenvid').val()));
                formData.append('nroplaca', $('#nroplacaenv').val());
                formData.append('nroguia', $('#nroguiaenv').val());
                formData.append('nroguiat', $('#nroguiatransenv').val());
                formData.append('comentarioEnv', $('#comentarioenv').val());
                formData.append('arregloenvioTA', JSON.stringify(ExtracciontipoArbolEnv));
                formData.append('arregloempleadosenvio', JSON.stringify(arregloenvemp));
                formData.append('tipoEnv', $('#tipoenvioenv').val());
                formData.append('check', checkb);


                // Agregar archivos al FormData
                for (var i = 0; i < archivosEnv.length; i++) {
                    formData.append('archivos', archivosEnv[i]);
                }


                $.ajax({
                    url: '/Pret10Envio/CrearEnvio', // Ajusta la URL según la estructura de tu aplicación
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        if (response.mensaje == null) {
                            window.location.href = response.redirectUrl;

                        } else {
                            alert(response.mensaje);
                        }
                        $.ajax({
                            url: '/Pret10Envio/CargarMood', // Reemplaza 'TuControlador' con el nombre real de tu controlador
                            type: 'GET',
                            success: function (response) {
                                if (response.id == 3) {
                                    document.getElementById('moodNameEnv').innerText = response.name;
                                    // Supongamos que tienes un input checkbox con el id "miCheckbox"
                                    $('#checkEstadoEnvio').prop('checked', true);
                                    $('#checkEstadoEnvio').prop('disabled', false);

                                } else if (response.id == 4) {
                                    document.getElementById('moodNameEnv').innerText = response.name;
                                    // Supongamos que tienes un input checkbox con el id "miCheckbox"
                                    $('#checkEstadoEnvio').prop('checked', true);
                                    $('#checkEstadoEnvio').prop('disabled', true);
                                }
                            }
                            });
                    },
                    error: function (error) {
                        console.error('Error al crear la campaña:', error.responseText);
                    }
                });
            } else {
                alert('Por favor, complete todos los campos requeridos.');
            }
        } else {
            alert('Registre al menos un tipo de arbol por envío.')
        }
    } else {
        alert('Registre al menos un empleado.')
    }
}
function enviarDatosEnvGuardaryCerrar() {
    if (arregloenvemp.length > 0) {
        if (ExtracciontipoArbolEnv.length > 0) {

            if (camposRequeridosLlenos('FormEnvio')) {
                var formData = new FormData();
                var checkbox = document.getElementById('checkEstadoEnvio');

                if (checkbox.checked) {
                    var checkb = true;
                } else {
                    var checkb = false;

                }
                formData.append('FechaEnvio', $('#fechaenv').val());
                formData.append('idlocation', parseInt($('#Locationenvid').val()));
                formData.append('idlocationto', parseInt($('#Locationtoenvid').val()));
                formData.append('nroplaca', $('#nroplacaenv').val());
                formData.append('nroguia', $('#nroguiaenv').val());
                formData.append('nroguiat', $('#nroguiatransenv').val());
                formData.append('comentarioEnv', $('#comentarioenv').val());
                formData.append('arregloenvioTA', JSON.stringify(ExtracciontipoArbolEnv));
                formData.append('arregloempleadosenvio', JSON.stringify(arregloenvemp));
                formData.append('tipoEnv', $('#tipoenvioenv').val());
                formData.append('check', checkb);

                // Agregar archivos al FormData
                for (var i = 0; i < archivosEnv.length; i++) {
                    formData.append('archivos', archivosEnv[i]);
                }

                console.log(formData);

                $.ajax({
                    url: '/Pret10Envio/CrearEnvioycerrar', // Ajusta la URL según la estructura de tu aplicación
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        if (response.mensaje == null) {
                            window.location.href = response.redirectUrl;
                        } else {
                            alert(response.mensaje);
                        }
                    },
                    error: function (error) {
                        console.error('Error al crear la campaña:', error.responseText);
                    }
                });
            } else {
                alert('Por favor, complete todos los campos requeridos.');
            }
        } else {
            alert('Registre al menos un tipo de arbol por envío.')
        }
    } else {
        alert('Registre al menos un empleado.')
    }
}





//Empleado


//Proceso para cerrar la ventana modal Proveedor
$(document).ready(function () {
    $('#modalemp').on('hidden.bs.modal', function () {
        $('#empcbo').prop('selectedIndex', 0);
        $('#proveedoroptionname').prop('selectedIndex', 0);
        $('#proveedoroptionid').prop('selectedIndex', 0);
        /*        cargarDatosproveedor();*/
        // Vaciar los campos de filtro
        $('#filtroRazonProv').val('');
        $('#filtroRUCProv').val('');
    });
});


//Proceso para cerrar la ventana modal proveedor



//function cargarDatosEmpleado() {
//    $.ajax({
//        url: '/Pert11PagoPersonal/RecargarEmpleado', // Reemplaza 'TuControlador' con el nombre real de tu controlador
//        type: 'GET',
//        success: function (response) {
//            /*console.log(Proveedor:', response);*/
//            // Clear the existing tbody content
//            $('#bodyprov').empty();

//            // Iterate through the updated client data and append rows to tbody
//            $.each(response, function (index, proveedor) {
//                var row = $('<tr>');
//                row.append('<td align="center" class="client-cell">' + proveedor.razon + '</td>');
//                row.append('<td align="center" class="client-cell">' + proveedor.ruc + '</td>');
//                /*                row.append('<td align="center" class="client-cell">' + proveedor.direccion + '</td>');*/
//                row.append('<td align="center" class="client-cell">' +
//                    '<button class="btn btn-primary" onclick="seleccionarProveedor(\'modalprov\',\'provcbo\',\'proveedorID\',\'proveedorruc\',\'proveedorrazon\',\'proveedordireccion\', \'' +
//                    proveedor.IdProveedor + '\', \'' + proveedor.ruc + '\', \'' + proveedor.razon + '\', \'' + proveedor.direccion + '\')">Seleccionar</button>' +
//                    '</td>');


//                // Append the row to the tbody
//                $('#bodyprov').append(row);
//            });
//        },
//        error: function (error) {
//            console.error('Error al cargar los datos del cliente:', error.responseText);
//        }
//    });
//}




























var archivosTim = [];

function abrirModalArchivostim() {
    // Limpiar la tabla del modal y mostrar los archivos actuales
    mostrarArchivostimEnTabla();
    // Mostrar el modal
    var nuevoArchivotimInput = document.getElementById('nuevoArchivotim');
    nuevoArchivotimInput.value = '';
    $('#modalArchivostim').modal('show');
}

function mostrarArchivostimEnTabla() {
    var tablaArchivostimBody = document.getElementById('tablaArchivostimBody');
    tablaArchivostimBody.innerHTML = '';

    for (var i = 0; i < archivosTim.length; i++) {
        agregarFilaATablatim(archivosTim[i]);
    }
}

function agregarArchivotim() {
    var nuevoArchivoInput = document.getElementById('nuevoArchivotim');
    var archivosNuevostim = nuevoArchivoInput.files;

    if (archivosNuevostim.length == 0) {
        alert('Selecciona algún archivo');
    } else {

        // Añadir cada nuevo archivo a la tabla
        for (var i = 0; i < archivosNuevostim.length; i++) {
            var archivotim = archivosNuevostim[i];
            agregarFilaATablatim(archivotim);
            archivosTim.push(archivotim);
        }

        // Limpiar el campo de archivos para permitir agregar otro
        nuevoArchivoInput.value = '';
    }
}

function agregarFilaATablatim(archivotim) {
    var tablaArchivosBody = document.getElementById('tablaArchivostimBody');
    var fila = document.createElement('tr');

    var celdaArchivo = document.createElement('td');
    var enlaceArchivo = document.createElement('a');
    enlaceArchivo.textContent = archivotim.name;

    // Verificar si el archivo es comprimido (RAR)
    if (archivotim.name.toLowerCase().endsWith('.rar')) {
        enlaceArchivo.href = '#'; // Puedes proporcionar un enlace válido si tienes una URL de descarga
        enlaceArchivo.onclick = function () {
            descargarArchivotim(archivotim);
        };
    } else {
        enlaceArchivo.href = URL.createObjectURL(archivotim);
        enlaceArchivo.target = '_blank';
    }

    celdaArchivo.appendChild(enlaceArchivo);

    // Obtener la extensión del archivo
    var extension = obtenerExtension(archivotim.name);
    var celdaExtension = document.createElement('td');
    celdaExtension.textContent = extension;

    var celdaAcciones = document.createElement('td');
    var botonEliminar = document.createElement('button');
    botonEliminar.className = 'btn btn-danger btn-sm';
    botonEliminar.textContent = 'Eliminar';
    botonEliminar.onclick = function () {
        eliminarArchivotim(archivotim);
    };

    celdaAcciones.appendChild(botonEliminar);

    fila.appendChild(celdaArchivo);
    fila.appendChild(celdaExtension);
    fila.appendChild(celdaAcciones);

    tablaArchivosBody.appendChild(fila);
}

function descargarArchivotim(archivotim) {
    // Aquí debes proporcionar la lógica para la descarga del archivo, por ejemplo, redirigiendo a una URL válida de descarga.
    // Puedes utilizar window.location.href para cambiar la ubicación actual a la URL de descarga.
    // Ejemplo: window.location.href = 'ruta/descargar.php?archivo=' + encodeURIComponent(archivo.name);
    alert('Descargando: ' + archivotim.name);
}



// Función para obtener la extensión de un archivo a partir de su nombre
function obtenerExtensiontim(nombreArchivotim) {
    var partes = nombreArchivotim.split('.');
    if (partes.length > 1) {
        return partes[partes.length - 1];
    } else {
        return '';
    }
}

function eliminarArchivotim(archivotim) {
    // Elimina el archivo de la tabla
    var index = archivosTim.indexOf(archivotim);
    if (index !== -1) {
        archivosTim.splice(index, 1);
        // Actualiza la tabla en el modal
        mostrarArchivostimEnTabla();
    }
}
function vaciarArregloYActualizarTablatim() {
    archivosTim = []; // Vaciar el arreglo de archivos
    abrirModalArchivostim(); // Actualizar la tabla en el modal
}






































































//ENVIORECEPCION

// Proceso para cerrar la ventana modal Campaña Extraccion
$(document).ready(function () {
    $('#modalenvrec').on('hidden.bs.modal', function () {
        $('#envreccbo').prop('selectedIndex', 0);
        cargarDatosenvrec();
    });




});

var EnviotipoArbolRec = [];
// CARGAR INVERSIONISTA EN TABLA INVERSIONISTA
function cargarDatosenvrec() {
    $.ajax({
        url: '/Pret11Recepcion/Recargarenvrec',
        type: 'GET',
        success: function (response) {
            // Limpia el tbody actual
            $('#bodyenvrec').empty();
            $.each(response, function (index, enviorec) {
                var fechaFormateada = new Date(enviorec.fechaini).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });

                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + enviorec.codigo + '</td>');
                row.append('<td align="center" class="client-cell">' + enviorec.nrositio + '</td>');
                row.append('<td align="center" class="client-cell">' + fechaFormateada + '</td>');

                var button = $('<button>').addClass('btn btn-primary')
                    .text('Seleccionar')
                    .on('click', function () {
                        seleccionarenvrec('modalenvrec', 'envreccbo', enviorec.id, enviorec.codigo);
                    });

                row.append($('<td>').addClass('client-cell text-center').append(button));

                // Append the row to the tbody
                $('#bodyenvrec').append(row);

            });

        },

        error: function (error) {
            console.error('Error al cargar los datos de la campaña:', error.responseText);
        }
    });

}


// Cargar la selección de Inversionista
function seleccionarenvrec(modalId, selectId, idextenv, nombrecampext) {
    event.preventDefault();

    closeModal(modalId, selectId);

    // Mostrar el span con el nombre del inversionista y el botón para remover
    var inversionistaNombreSpan = document.getElementById('envrecNombre');
    var removeInversionistaSpan = document.getElementById('removeEnvrecSpan');
    inversionistaNombreSpan.innerText = nombrecampext;
    inversionistaNombreSpan.style.display = 'inline-block';
    removeInversionistaSpan.style.display = 'inline-block';
    // Ocultar el seleCAMPEXT
    var selectElement = document.getElementById('envreccbo');
    selectElement.style.display = 'none';

    
            $.ajax({
                url: '/Pret11Recepcion/SeleccionarEnvRec',
                type: 'GET',
                data: { idEnvio: idextenv },
                success: function (response) {
                    document.getElementById('codCamrec').value = response.codCampana;
                    document.getElementById('unCatrec').value = response.unCatastral;
                    document.getElementById('locenvrec').value = response.nomLoc;
                    document.getElementById('ltoenvrec').value = response.nomLto;
                    document.getElementById('nroplacarec').value = response.nroPlaca;
                    document.getElementById('nroguiarec').value = response.nroGuia;
                    document.getElementById('nroguiatransrec').value = response.nroGuiaTransp;

                    for (var i = 0; i < response.detalle.length; i++) {
                        var detalleItem = response.detalle[i];
                        EnviotipoArbolRec.push({
                            idunico: detalleItem.idun,
                            idTipoArbol: detalleItem.idta,
                            nombreTipoarbol: detalleItem.ta,
                            altura: detalleItem.al, // Make sure to define the variables altura, diametro, nrotrozas, comentario
                            diametro: detalleItem.di,
                            nrotrozas: detalleItem.nt,
                            comentario: detalleItem.com
                        });
                    }
                    actualizarTablaTipoArbolSeleccionadosRec();
                },
                error: function (error) {
                    console.error('Error al cargar los datos de la campaña de extracción:', error.responseText);
                    reject(error);
                }
            });
    cargarDatosenvrec();


}

// El span inversionista para volver al select
function removeenvrec() {
    event.preventDefault();
    // Quitar el valor del inversionista y ocultar el span
    document.getElementById('codCamrec').value = '';
    document.getElementById('unCatrec').value = '';
    document.getElementById('locenvrec').value = '';
    document.getElementById('ltoenvrec').value = '';
    document.getElementById('nroplacarec').value = '';
    document.getElementById('nroguiarec').value = '';
    document.getElementById('nroguiatransrec').value = '';
    var selectElement = document.getElementById('envreccbo');
    selectElement.style.display = 'inline-block';
    EnviotipoArbolRec.splice(0, EnviotipoArbolRec.length);

    $.ajax({
        url: "/Pret11Recepcion/removeIdTemporalRecepcion",
        type: "GET",
        success: function (response) {
            if (response.mensaje == null) {
            } else {
                alert(response.mensaje);
            }
        },
        error: function (response) {
        }
    });
    cargarDatosenvrec();
    // Mostrar el span con el nombre del inversionista y el botón para remover
    var inversionistaNombreSpan = document.getElementById('envrecNombre');
    var removeInversionistaSpan = document.getElementById('removeEnvrecSpan');
    inversionistaNombreSpan.style.display = 'none';
    removeInversionistaSpan.style.display = 'none';
    // Ocultar el seleCAMPEXT
    var selectElement = document.getElementById('envreccbo');
    selectElement.style.display = 'inline-block';
    actualizarTablaTipoArbolSeleccionadosRec();

}


function actualizarTotalesTARec() {



}

// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarTipoArbolRec(idTipoArbolEnv) {
    if (typeof idTipoArbolEnv !== 'string') {
        console.error('El ID no es una cadena.');
        console.error(idTipoArbolEnv);
        return;
    }
    var campanaTAExt = EnviotipoArbolRec.find(function (campTA) {
        return campTA.idunico === idTipoArbolEnv;
    });
    console.log(idTipoArbolEnv);
    console.log("conj" + campanaTAExt);
    if (campanaTAExt) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este registro de la lista?");

        if (confirmar) {
            var campTAExtindex = EnviotipoArbolRec.findIndex(function (campTA) {
                return campTA.idunico === idTipoArbolEnv;
            });

            if (campTAExtindex !== -1) {
                EnviotipoArbolRec.splice(campTAExtindex, 1);
                actualizarTablaTipoArbolSeleccionadosRec();
            }
        }
    } else {
        alert("Registro no encontrado en la lista.");
    }
}


//Función para abrir el modal de edición
function editarTipoArbolRec(idTipoArbolEnv) {
    var enviotipoarbol = EnviotipoArbolRec.find(function (campTA) {
        return campTA.idunico === idTipoArbolEnv;
    });
    if (enviotipoarbol) {
        document.getElementById("IdEditarTARec").value = idTipoArbolEnv;
        document.getElementById("TAExtinputRec").value = enviotipoarbol.nombreTipoarbol;
        document.getElementById("nuevaalturaTARec").value = enviotipoarbol.altura;
        document.getElementById("nuevodiametroTARec").value = enviotipoarbol.diametro;
        document.getElementById("nuevanrotrozasTARec").value = enviotipoarbol.nrotrozas;
        document.getElementById("comentarioTARec").value = enviotipoarbol.comentario;
        openSecondModal('editarTARECModal');
    } else {
        alert("Producto no encontrado en la lista.");
    }
}

// Función para aplicar los cambios desde el modal y guardarlos
function aplicarCambiosTARec() {

    var idcampTA = (document.getElementById("IdEditarTARec").value);

    var campanaTAExt = EnviotipoArbolRec.find(function (prod) {
        return prod.idunico === idcampTA;
    });
    var nuevaaltura = parseFloat(document.getElementById("nuevaalturaTARec").value);
    var nuevodiametro = parseFloat(document.getElementById("nuevodiametroTARec").value);
    var nuevanrotrozas = parseInt(document.getElementById("nuevanrotrozasTARec").value);
    var comentarionuevo = (document.getElementById("comentarioTARec").value);
    if (isNaN(nuevaaltura) || isNaN(nuevodiametro) || isNaN(nuevanrotrozas) || !Number.isInteger(nuevanrotrozas)
        || nuevanrotrozas <= 0 || nuevodiametro <= 0 || nuevaaltura <= 0) {
        alert("Por favor, ingrese valores numéricos para la extracción por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }
    if (!isNaN(nuevaaltura) && !isNaN(nuevodiametro) && !isNaN(nuevanrotrozas)) {
        var confirmar = confirm("¿Deseas aplicar los cambios y guardarlos?");

        if (confirmar) {
            // Aplicar parseFloat con toFixed(2) a las variables
            var altura = Number(nuevaaltura);
            var diametro = Number(nuevodiametro);
            var nrotrozas = nuevanrotrozas;
            var comentario = (comentarionuevo);


            // Asignar los valores a las propiedades del objeto producto
            campanaTAExt.altura = altura;
            campanaTAExt.diametro = diametro;
            campanaTAExt.nrotrozas = nrotrozas;
            campanaTAExt.comentario = comentario;


            actualizarTablaTipoArbolSeleccionadosRec();
            cerrarModal('editarTARECModal');
        } else {
            alert("Edición cancelada. Los cambios no se aplicaron.");
        }
    } else {
        alert("Por favor, ingresa valores numéricos válidos para los campos.");
    }
}

function actualizarTablaTipoArbolSeleccionadosRec() {
    // Limpiar la tabla
    $("#tipoArbolSeleccionadosRec").empty();

    // Recorrer el arreglo de tipo de árboles seleccionados y volver a agregarlos a la tabla
    EnviotipoArbolRec.forEach(function (tipoArbol) {
        var newRow = `
            <tr class="text-center" id='${tipoArbol.idunico}'>
                <td class="text-center">${tipoArbol.nombreTipoarbol}</td>
                <td class="text-center">${tipoArbol.altura}</td>
                <td class="text-center">${tipoArbol.diametro}</td>
                <td class="text-center">${tipoArbol.nrotrozas}</td>
                <td class="text-center">${tipoArbol.comentario}</td>
                <td colspan='2' class="text-center">
                    <button class="btn btn-primary btn-sm" type='button' onclick='editarTipoArbolRec("${tipoArbol.idunico}")'>  <i class="fas fa-edit"></i> </button>
                    <button type='button' class="btn btn-danger btn-sm" onclick='eliminarTipoArbolRec("${tipoArbol.idunico}")'><i class="fas fa-trash alt"></i></button>
                </td>
            </tr>`;

        $("#tipoArbolSeleccionadosRec").append(newRow);
    });


    actualizarTotalesTARec();
}






var archivosRec = [];

function abrirModalArchivosrec() {
    // Limpiar la tabla del modal y mostrar los archivos actuales
    mostrarArchivosrecEnTabla();
    // Mostrar el modal
    var nuevoArchivorecInput = document.getElementById('nuevoArchivorec');
    nuevoArchivorecInput.value = '';
    $('#modalArchivosrec').modal('show');

}

function mostrarArchivosrecEnTabla() {
    var tablaArchivosrecBody = document.getElementById('tablaArchivosrecBody');
    tablaArchivosrecBody.innerHTML = '';

    for (var i = 0; i < archivosRec.length; i++) {
        agregarFilaATablarec(archivosRec[i]);
    }
}

function agregarArchivorec() {
    var nuevoArchivoInput = document.getElementById('nuevoArchivorec');
    var archivosNuevosrec = nuevoArchivoInput.files;

    if (archivosNuevosrec.length == 0) {
        alert('Selecciona algún archivo');
    } else {

        // Añadir cada nuevo archivo a la tabla
        for (var i = 0; i < archivosNuevosrec.length; i++) {
            var archivorec = archivosNuevosrec[i];
            agregarFilaATablarec(archivorec);
            archivosRec.push(archivorec);
        }

        // Limpiar el campo de archivos para permitir agregar otro
        nuevoArchivoInput.value = '';
    }
}

function agregarFilaATablarec(archivorec) {
    var tablaArchivosBody = document.getElementById('tablaArchivosrecBody');
    var fila = document.createElement('tr');

    var celdaArchivo = document.createElement('td');
    var enlaceArchivo = document.createElement('a');
    enlaceArchivo.textContent = archivorec.name;

    // Verificar si el archivo es comprimido (RAR)
    if (archivorec.name.toLowerCase().endsWith('.rar')) {
        enlaceArchivo.href = '#'; // Puedes proporcionar un enlace válido si tienes una URL de descarga
        enlaceArchivo.onclick = function () {
            descargarArchivorec(archivorec);
        };
    } else {
        enlaceArchivo.href = URL.createObjectURL(archivorec);
        enlaceArchivo.target = '_blank';
    }

    celdaArchivo.appendChild(enlaceArchivo);

    // Obtener la extensión del archivo
    var extension = obtenerExtension(archivorec.name);
    var celdaExtension = document.createElement('td');
    celdaExtension.textContent = extension;

    var celdaAcciones = document.createElement('td');
    var botonEliminar = document.createElement('button');
    botonEliminar.className = 'btn btn-danger btn-sm';
    botonEliminar.textContent = 'Eliminar';
    botonEliminar.onclick = function () {
        eliminarArchivorec(archivorec);
    };

    celdaAcciones.appendChild(botonEliminar);

    fila.appendChild(celdaArchivo);
    fila.appendChild(celdaExtension);
    fila.appendChild(celdaAcciones);

    tablaArchivosBody.appendChild(fila);
}

function descargarArchivorec(archivorec) {
    // Aquí debes proporcionar la lógica para la descarga del archivo, por ejemplo, redirigiendo a una URL válida de descarga.
    // Puedes utilizar window.location.href para cambiar la ubicación actual a la URL de descarga.
    // Ejemplo: window.location.href = 'ruta/descargar.php?archivo=' + encodeURIComponent(archivo.name);
    alert('Descargando: ' + archivorec.name);
}



// Función para obtener la extensión de un archivo a partir de su nombre
function obtenerExtensionrec(nombreArchivorec) {
    var partes = nombreArchivorec.split('.');
    if (partes.length > 1) {
        return partes[partes.length - 1];
    } else {
        return '';
    }
}

function eliminarArchivorec(archivorec) {
    // Elimina el archivo de la tabla
    var index = archivosRec.indexOf(archivorec);
    if (index !== -1) {
        archivosRec.splice(index, 1);
        // Actualiza la tabla en el modal
        mostrarArchivosrecEnTabla();
    }
}
function vaciarArregloYActualizarTablarec() {
    archivosRec = []; // Vaciar el arreglo de archivos
    abrirModalArchivosrec(); // Actualizar la tabla en el modal
}










//empleado recepcion

//Proceso para cerrar la ventana modal CHOFER
$(document).ready(function () {
    $('#modalemprec').on('hidden.bs.modal', function () {
        $('#empreccbo').prop('selectedIndex', 0);

        cargarDatosemprec();
    });
});



//CARGAR CHOFER EN TABLA CHOFER
function cargarDatosemprec() {
    var idsempleado = arreglorecemp.map(function (campaña) {
        return campaña.idempleado;
    });
    $.ajax({
        url: '/Pret11Recepcion/Recargaremprec', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { listaIds: idsempleado }, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodyemprec').empty();
            $.each(response, function (index, EmpleadoEnv) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + EmpleadoEnv.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoEnv.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoEnv.cargo + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoEnv.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoEnv.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionaremprec(\'modalemprec\',\'empreccbo\',\'emprecId\',\'emprecNombre\',\'' +
                    EmpleadoEnv.idempleado + '\', \'' + EmpleadoEnv.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');
                $('#bodyemprec').append(row);
            });

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });
}


//Cargar la selección de Chofer
function seleccionaremprec(modalId, selectId, idcod, idtext, idempext, nombreempext) {
    event.preventDefault();
    var choferIdInput = document.getElementById(idcod);
    choferIdInput.value = idempext;
    document.getElementById(idtext).innerText = nombreempext;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var choferNombreSpan = document.getElementById('emprecNombre');
    var removeChoferSpan = document.getElementById('removeemprecSpan');
    choferNombreSpan.innerText = nombreempext;
    choferNombreSpan.style.display = 'inline-block';
    removeChoferSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('empreccbo');
    selectElement.style.display = 'none';
    $.ajax({
        url: '/Pret11Recepcion/CargarDatosEmpleado',
        type: 'GET',
        data: { idEmpleado: idempext },
        success: function (data) {
            if (!data) {
                // Si data es null o undefined
                document.getElementById('nrodocempleadorec').value = "";
                document.getElementById('categoriaempleadorec').value = "";
                document.getElementById('condicionempleadorec').value = "";
                document.getElementById('telefonoempleadorec').value = "";
            } else {
                // Si data no es null o undefined
                document.getElementById('nrodocempleadorec').value = data.nrodoc || "";
                document.getElementById('categoriaempleadorec').value = data.cargo || "";
                document.getElementById('condicionempleadorec').value = data.condicion || "";
                document.getElementById('telefonoempleadorec').value = data.telefono || "";

            }

        },
        error: function (error) {
            console.error('Error al cargar datos del empleado:', error.responseText);

        }

    });

}

//El span cliente para volver al select
function removeemprec() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('nrodocempleadorec').value = '';
    document.getElementById('categoriaempleadorec').value = '';
    /*document.getElementById('salarioempleado').value = 1;*/
    document.getElementById('emprecId').value = 0;
    document.getElementById('condicionempleadorec').value = '';
    document.getElementById('telefonoempleadorec').value = '';
    ;
    document.getElementById('emprecNombre').style.display = 'none';
    document.getElementById('removeemprecSpan').style.display = 'none';
    var selectElement = document.getElementById('empreccbo');
    selectElement.style.display = 'inline-block';

}

//AREGLO EXT EMP
var arreglorecemp = [];



function agregarrecemp() {
    var idempleado = parseInt($("#emprecId").val());
    /*var salario= parseFloat($("#salarioempleado").val());*/
    var nrodoc = ($("#nrodocempleadorec").val());
    var categoria = ($("#categoriaempleadorec").val());
    var condicion = ($("#condicionempleadorec").val());
    var telefono = ($("#telefonoempleadorec").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idempleado) || idempleado <= 0 /*|| isNaN(salario) ||   0 >(salario)*/) {
        alert("Por favor, ingrese valores numéricos para la extracción por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }
    $.ajax({
        url: "/Pret11Recepcion/EmpleadoRec",
        type: "GET",
        data: { empleadoID: idempleado },
        success: function (response) {
            if (response != null) {


                arreglorecemp.push({
                    idempleado: idempleado,
                    txtempleado: response.txtnombre,
                    telefono: response.telefono,
                    /*                    salario: salario,*/
                    nrodoc: nrodoc,
                    categoria: categoria,
                    condicion: condicion
                });

                actualizartablarecemp();
                removeemprec();


            } else {
                alert("Tipo de Arbol no encontrado");
            }
        },
        error: function (response) {
            alert("Error al obtener los datos del Tipo de Arbol");
        }
    });


}

// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarrecemp(idenvemp) {
    var campanarecemp = arreglorecemp.find(function (empleado) {
        return empleado.idempleado === idenvemp;
    });

    if (campanarecemp) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este registro de la lista?");

        if (confirmar) {
            var campTAEnvindex = arreglorecemp.findIndex(function (campTA) {
                return campTA.idempleado === idenvemp;
            });

            if (campTAEnvindex !== -1) {
                arreglorecemp.splice(campTAEnvindex, 1);
                actualizartablarecemp();
            }
        }
    } else {
        alert("Registro no encontrado en la lista.");
    }
}


//Función para abrir el modal de edición
function editarempleadosRecepcion(idrecemp) {
    var empleadosrecepcion = arreglorecemp.find(function (empleado) {
        return empleado.idempleado === idrecemp;
    });
    if (empleadosrecepcion) {
        /* document.getElementById("nuevosalarioextemp").value = empleadosextraccion.salario;*/
        document.getElementById("IdEditaremprec").value = idrecemp;
        document.getElementById("nuevacategoriarecemp").value = empleadosrecepcion.categoria;
        document.getElementById("nuevacondicionrecemp").value = empleadosrecepcion.condicion;
        document.getElementById("nuevodocrecemp").value = empleadosrecepcion.nrodoc;
        openSecondModal('editarEMPRECModal');
    } else {
        alert("Empleado no encontrado en la lista.");
    }
}


function actualizartablarecemp() {
    // Limpiar la tabla
    $("#empSeleccionadosRec").empty();

    // Recorrer el arreglo de tipo de árboles seleccionados y volver a agregarlos a la tabla
    arreglorecemp.forEach(function (Empleado) {
        var newRow = `
            <tr id='${Empleado.idempleado}'>
                <td class="text-center">${Empleado.txtempleado}</td>
                <td class="text-center">${Empleado.condicion}</td>
                <td class="text-center">${Empleado.telefono}</td>
                <td class="text-center">${Empleado.nrodoc}</td>
                <td class="text-center">${Empleado.categoria}</td>
                <td colspan='2' class="text-center">
                    
                    <button type='button'  class="btn btn-danger btn-sm" onclick='eliminarrecemp(${Empleado.idempleado})'><i class="fas fa-trash alt"></i></button>
                </td>
            </tr>`;

        $("#empSeleccionadosRec").append(newRow);
    });
    cargarDatosemprec();


}










$(document).ready(function () {
    $("#guardarRec").click(function () {
        enviarDatosRecGuardar();
    });
    $("#guardarycerrarRec").click(function () {
        enviarDatosRecGuardaryCerrar();
    });
    $("#cancelarRec").click(function () {
        $.ajax({
            url: "/Pret11Recepcion/CerrarExtraccionRec",
            type: "GET",
            success: function (response) {
                if (response.mensaje == null) {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.mensaje);
                }
            },
            error: function (response) {
            }
        });
});













    function enviarDatosRecGuardar() {
        if (arreglorecemp.length > 0) {
            if (EnviotipoArbolRec.length > 0) {


                if (camposRequeridosLlenos('FormRecepcion')) {
                    var formData = new FormData();
                    var checkbox = document.getElementById('checkEstadoRecepcion');

                    if (checkbox.checked) {
                        var checkb = true;
                    } else {
                        var checkb = false;

                    }
                    formData.append('FechaRecepcion', $('#fecharec').val());
                    formData.append('comentarioRec', $('#comentariorec').val());
                    formData.append('arreglorecepcionTA', JSON.stringify(EnviotipoArbolRec));
                    formData.append('arregloempleadosrecepcion', JSON.stringify(arreglorecemp));
                    formData.append('tipoEnv', $('#tipoenvioenv').val());
                    formData.append('check', checkb);


                    // Agregar archivos al FormData
                    for (var i = 0; i < archivosRec.length; i++) {
                        formData.append('archivos', archivosRec[i]);
                    }


                    $.ajax({
                        url: '/Pret11Recepcion/CrearRecepcion', // Ajusta la URL según la estructura de tu aplicación
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (response) {
                            if (response.mensaje == null) {
                                window.location.href = response.redirectUrl;

                            } else {
                                alert(response.mensaje);
                            }
                            $.ajax({
                                url: '/Pret11Recepcion/CargarMood', // Reemplaza 'TuControlador' con el nombre real de tu controlador
                                type: 'GET',
                                success: function (response) {
                                    if (response.id == 3) {
                                        document.getElementById('moodNameRec').innerText = response.name;
                                        // Supongamos que tienes un input checkbox con el id "miCheckbox"
                                        $('#checkEstadoRecepcion').prop('checked', true);
                                        $('#checkEstadoRecepcion').prop('disabled', false);

                                    } else if (response.id == 4) {
                                        document.getElementById('moodNameRec').innerText = response.name;
                                        // Supongamos que tienes un input checkbox con el id "miCheckbox"
                                        $('#checkEstadoRecepcion').prop('checked', true);
                                        $('#checkEstadoRecepcion').prop('disabled', true);
                                    }
                                }
                            });
                        },
                        error: function (error) {
                            console.error('Error al crear la campaña:', error.responseText);
                        }
                    });
                } else {
                    alert('Por favor, complete todos los campos requeridos.');
                }
            } else {
                alert('Registre al menos un tipo de arbol por envío.')
            }
        } else {
            alert('Registre al menos un empleado.')
        }
    }
    function enviarDatosRecGuardaryCerrar() {
        if (arreglorecemp.length > 0) {
            if (EnviotipoArbolRec.length > 0) {


                if (camposRequeridosLlenos('FormRecepcion')) {
                    var formData = new FormData();
                    var checkbox = document.getElementById('checkEstadoRecepcion');

                    if (checkbox.checked) {
                        var checkb = true;
                    } else {
                        var checkb = false;

                    }
                    formData.append('FechaRecepcion', $('#fecharec').val());
                    formData.append('comentarioRec', $('#comentariorec').val());
                    formData.append('arreglorecepcionTA', JSON.stringify(EnviotipoArbolRec));
                    formData.append('arregloempleadosrecepcion', JSON.stringify(arreglorecemp));
                    formData.append('tipoEnv', $('#tipoenvioenv').val());
                    formData.append('check', checkb);


                    // Agregar archivos al FormData
                    for (var i = 0; i < archivosRec.length; i++) {
                        formData.append('archivos', archivosRec[i]);
                    }


                    $.ajax({
                        url: '/Pret11Recepcion/CrearRecepcionycerrar', // Ajusta la URL según la estructura de tu aplicación
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (response) {
                            if (response.mensaje == null) {
                                window.location.href = response.redirectUrl;
                            } else {
                                alert(response.mensaje);
                            }
                        },
                        error: function (error) {
                            console.error('Error al crear la campaña:', error.responseText);
                        }
                    });
                } else {
                    alert('Por favor, complete todos los campos requeridos.');
                }
            } else {
                alert('Registre al menos un tipo de arbol por envío.')
            }
        } else {
            alert('Registre al menos un empleado.')
        }
    }
});


$(document).ready(function () {
    $('#confirmarEliminarRecepcion').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var idEnvio = button.data('id');
        $(this).data('id', idEnvio);
    });
    $('#confirmarAnularRecepcion').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var idEnvio = button.data('id');
        $(this).data('id', idEnvio);
    });
});


function eliminarRecepcion() {

    var idEnvio = $('#confirmarEliminarRecepcion').data('id');
    console.log('ID del predio a eliminar:', idEnvio);
    $.ajax({
        url: '/Pret11Recepcion/EliminarRecepcion', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        data: { id: idEnvio },
        success: function (response) {
            if (response.mensaje == null) {
                $('#confirmarEliminarRecepcion').modal('hide');

                window.location.href = response.redirectUrl;

            } else {


            }
        },
        error: function (error) {
            console.error('Error al intentar eliminar', error.responseText);
        }
    });
}

function anularRecepcion() {

    var idEnvio = $('#confirmarAnularRecepcion').data('id');
    console.log('ID del predio a eliminar:', idEnvio);
    $.ajax({
        url: '/Pret11Recepcion/AnularRecepcion', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        data: { id: idEnvio },
        success: function (response) {
            if (response.mensaje == null) {
                $('#confirmarAnularRecepcion').modal('hide');

                window.location.href = response.redirectUrl;

            } else {


            }
        },
        error: function (error) {
            console.error('Error al intentar eliminar', error.responseText);
        }
    });
}



//PRODUCCION



//Extraccion
$(document).ready(function () {
    $('#modalextpro').on('hidden.bs.modal', function () {
        $('#extprocbo').prop('selectedIndex', 0);
        cargarDatosextpro();
    });
});


// CARGAR INVERSIONISTA EN TABLA INVERSIONISTA
function cargarDatosextpro() {
    $.ajax({
        url: '/Pret14Produccion/Recargarextpro',
        type: 'GET',
        success: function (response) {
            // Limpia el tbody actual
            $('#bodyextpro').empty();
            $.each(response, function (index, extraccionenv) {
                var fechaFormateada = new Date(extraccionenv.fechaini).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });

                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + extraccionenv.codigo + '</td>');
                row.append('<td align="center" class="client-cell">' + extraccionenv.nrositio + '</td>');
                row.append('<td align="center" class="client-cell">' + fechaFormateada + '</td>');

                var button = $('<button>').addClass('btn btn-primary')
                    .text('Seleccionar')
                    .on('click', function () {
                        seleccionarextpro('modalextpro', 'extprocbo', extraccionenv.id, extraccionenv.codigo);
                    });

                row.append($('<td>').addClass('client-cell text-center').append(button));

                // Append the row to the tbody
                $('#bodyextpro').append(row);

            });

        },

        error: function (error) {
            console.error('Error al cargar los datos de la campaña:', error.responseText);
        }
    });

}


// Cargar la selección de Inversionista
function seleccionarextpro(modalId, selectId, idextenv, nombrecampext) {
    event.preventDefault();
    $.ajax({
        url: '/Pret14Produccion/VerificarExt',
        type: 'GET',
        data: { idExt: idextenv },
        success: function (response) {
            //Predio
            document.getElementById('idpredioproduccion').value = response.idP;
            var CargadorNombreSpan = document.getElementById('propreNombre');
            /*var removeCargadorSpan = document.getElementById('removepropreSpan');*/
            CargadorNombreSpan.innerText = response.nrositio;
            CargadorNombreSpan.style.display = 'inline-block';
            /*removeCargadorSpan.style.display = 'inline-block';*/
            //Ocultar Select
            var selectElement = document.getElementById('proprecbo');
            selectElement.style.display = 'none';

            //Campaña
            document.getElementById('idcampanaproduccion').value = response.idC;
            var CampNombreSpan = document.getElementById('camproNombre');
           /* var removeCampSpan = document.getElementById('removecamproSpan');*/
            CampNombreSpan.innerText = response.codigo;
            CampNombreSpan.style.display = 'inline-block';
           /* removeCampSpan.style.display = 'inline-block';*/
            // Ocultar el select
            var selectElement = document.getElementById('camprocbo');
            selectElement.style.display = 'none';

            console.log('Valor de idtemporalCampana:', response.idtemporalExtraccion);

        },
        error: function (error) {
            console.error('Error al cargar los datos de la campaña de extracción:', error.responseText);
            reject(error);
        }
    });
    console.log(idextenv);
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del inversionista y el botón para remover
    var inversionistaNombreSpan = document.getElementById('extproNombre');
    var removeInversionistaSpan = document.getElementById('removeExtproSpan');
    inversionistaNombreSpan.innerText = nombrecampext;
    inversionistaNombreSpan.style.display = 'inline-block';
    removeInversionistaSpan.style.display = 'inline-block';
    // Ocultar el seleCAMPEXT
    var selectElement = document.getElementById('extprocbo');
    selectElement.style.display = 'none';

    cargarDatosextpro();

}

// El span inversionista para volver al select
function removeextpro() {
    event.preventDefault();
    // Quitar el valor del inversionista y ocultar el span
    document.getElementById('extproNombre').innerText = '';
    document.getElementById('extproNombre').style.display = 'none';
    document.getElementById('removeExtproSpan').style.display = 'none';
    removeProPre();
    var selectElement = document.getElementById('extprocbo');
    selectElement.style.display = 'inline-block';
    $.ajax({
        url: "/Pret14Produccion/removeIdTemporalExtraccion",
        type: "GET",
        success: function (response) {
            if (response.mensaje == null) {
            } else {
                alert(response.mensaje);
            }
        },
        error: function (response) {
        }
    });
    cargarDatosextpro();
}



//PREDIO

//Proceso para cerrar la ventana modal CARGADOR
$(document).ready(function () {
    $('#modalpropre').on('hidden.bs.modal', function () {
        $('#proprecbo').prop('selectedIndex', 0);
        cargarDatosProPre();
    });
});



//Cargar CARGADOR EN TABLA CARGADOR
function cargarDatosProPre() {
    $.ajax({
        url: '/Pret14Produccion/RecargarPredio', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function (response) {
            /*console.log('Cargador:', response);*/
            $('#bodypropre').empty();

            // Iterate through the updated cargador data and append rows to tbody
            $.each(response, function (index, predio) {
                var fechaFormateada = new Date(predio.fechC).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + predio.unidC + '</td>');
                row.append('<td align="center" class="client-cell">' + predio.nroS + '</td>');
                row.append('<td align="center" class="client-cell">' + fechaFormateada + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarProPredio(\'modalpropre\',\'proprecbo\',\'idpredioproduccion\',\'propreNombre\',\'' +
                    predio.id + '\', \'' + predio.unidC + '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodypropre').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del Cargador:', error.responseText);
        }
    });
}


//Cargar la selección de CARGADOR
function seleccionarProPredio(modalId, selectId, idcod, idtext, idPredio, UnidCatastral) {
    event.preventDefault();
    var PredioIdInput = document.getElementById(idcod);
    PredioIdInput.value = idPredio;
    document.getElementById(idtext).innerText = UnidCatastral;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var CargadorNombreSpan = document.getElementById('propreNombre');
    var removeCargadorSpan = document.getElementById('removepropreSpan');
    CargadorNombreSpan.innerText = UnidCatastral;
    CargadorNombreSpan.style.display = 'inline-block';
    removeCargadorSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('proprecbo');
    selectElement.style.display = 'none';
    $.ajax({
        url: '/Pret14Produccion/Recargarcampanas', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { IdPredio: idPredio }, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodycampro').empty();
            $.each(response, function (index, campanas) {
                var fechaFormateada = new Date(campanas.fechI).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });

                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + campanas.codC + '</td>');
                row.append('<td align="center" class="client-cell">' + parseFloat(campanas.area).toFixed(2) + '</td>');
                row.append('<td align="center" class="client-cell">' + fechaFormateada + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarProCampana(\'modalcampro\',\'camprocbo\',\'idcampanaproduccion\',\'camproNombre\',\'' +
                    campanas.id + '\', \'' + campanas.codC + '\')">Seleccionar</button>' +
                    '</td>');
                $('#bodycampro').append(row);
            });

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });

}

//El span CARGADOR para volver al select
function removeProPre() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('propreNombre').innerText = '';
    document.getElementById('idpredioproduccion').value = '';
    document.getElementById('propreNombre').style.display = 'none';
    document.getElementById('removepropreSpan').style.display = 'none';
    var selectElement = document.getElementById('proprecbo');
    selectElement.style.display = 'inline-block';
    $.ajax({
        url: '/Pret14Produccion/RemovePredio', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function () {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodycampro').empty();
            removeCamPro();

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });

}

//CAMPAÑA

//Proceso para cerrar la ventana modal CARGADOR
$(document).ready(function () {
    $('#modalcampro').on('hidden.bs.modal', function () {
        $('#camprocbo').prop('selectedIndex', 0);
        cargarDatosCamPro();
    });
});



//Cargar CARGADOR EN TABLA CARGADOR
function cargarDatosCamPro() {
    $.ajax({
        url: '/Pret14Produccion/Recargarcampanas', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { IdPredio: null }, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodycampro').empty();
            $.each(response, function (index, campanas) {
                var fechaFormateada = new Date(campanas.fechI).toLocaleDateString('es-ES', { day: '2-digit', month: '2-digit', year: 'numeric' });

                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + campanas.codC + '</td>');
                row.append('<td align="center" class="client-cell">' + campanas.area + '</td>');
                row.append('<td align="center" class="client-cell">' + fechaFormateada + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarProCampana(\'modalcampro\',\'camprocbo\',\'idcampanaproduccion\',\'camproNombre\',\'' +
                    campanas.id + '\', \'' + campanas.codC + '\')">Seleccionar</button>' +
                    '</td>');
                $('#bodycampro').append(row);
            });

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });
}


//Cargar la selección de CARGADOR
function seleccionarProCampana(modalId, selectId, idcod, idtext, idCampana, codC) {
    event.preventDefault();
    var PredioIdInput = document.getElementById(idcod);
    PredioIdInput.value = idCampana;
    document.getElementById(idtext).innerText = codC;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var CargadorNombreSpan = document.getElementById('camproNombre');
    var removeCargadorSpan = document.getElementById('removecamproSpan');
    CargadorNombreSpan.innerText = codC;
    CargadorNombreSpan.style.display = 'inline-block';
    removeCargadorSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('camprocbo');
    selectElement.style.display = 'none';
    $.ajax({
        url: '/Pret14Produccion/SelecCampana', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { idCampana: idCampana }, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            console.log(response);

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });

}

//El span CARGADOR para volver al select
function removeCamPro() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('camproNombre').innerText = '';
    document.getElementById('idcampanaproduccion').value = '';
    document.getElementById('camproNombre').style.display = 'none';
    document.getElementById('removecamproSpan').style.display = 'none';
    var selectElement = document.getElementById('camprocbo');
    selectElement.style.display = 'inline-block';
    $.ajax({
        url: '/Pret14Produccion/RemoveCampana', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        success: function () {

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });
}

//empleado extraccion

//Proceso para cerrar la ventana modal CHOFER
$(document).ready(function () {
    $('#modalemppro').on('hidden.bs.modal', function () {
        $('#empprocbo').prop('selectedIndex', 0);

        cargarDatosemppro();
    });
    $('#modalemppro').on('show.bs.modal', function () {

        cargarDatosemppro();
    });
});



//CARGAR CHOFER EN TABLA CHOFER
function cargarDatosemppro() {
    var idsempleado = arregloproemp.map(function (campaña) {
        return campaña.idempleado;
    });
    $.ajax({
        url: '/Pret14Produccion/Recargaremppro', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'GET',
        data: { listaIds: idsempleado }, // Asegúrate de tener la lista de IDs que quieres enviar
        traditional: true,
        success: function (response) {
            /*console.log('Chofere:', response);*/
            // Limpia el tbody actual
            $('#bodyemppro').empty();
            $.each(response, function (index, EmpleadoPro) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + EmpleadoPro.nombreCompleto + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoPro.direccion + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoPro.cargo + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoPro.telefono + '</td>');
                row.append('<td align="center" class="client-cell">' + EmpleadoPro.ruc + '</td>');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionaremppro(\'modalemppro\',\'empprocbo\',\'empproId\',\'empproNombre\',\'' +
                    EmpleadoPro.idempleado + '\', \'' + EmpleadoPro.nombreCompleto + '\')">Seleccionar</button>' +
                    '</td>');
                $('#bodyemppro').append(row);
            });

        },
        error: function (error) {
            console.error('Error al cargar los datos del empleado envío:', error.responseText);
        }
    });
}

function seleccionaremppro(modalId, selectId, idcod, idtext, idempven, nombreempven) {
    event.preventDefault();
    var choferIdInput = document.getElementById(idcod);
    choferIdInput.value = idempven;
    document.getElementById(idtext).innerText = nombreempven;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var choferNombreSpan = document.getElementById('empproNombre');
    var removeChoferSpan = document.getElementById('removeempproSpan');
    choferNombreSpan.innerText = nombreempven;
    choferNombreSpan.style.display = 'inline-block';
    removeChoferSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('empprocbo');
    selectElement.style.display = 'none';
    $.ajax({
        url: '/Pret14Produccion/CargarDatosEmpleado',
        type: 'GET',
        data: { idEmpleado: idempven },
        success: function (data) {
            if (!data) {
                // Si data es null o undefined
                document.getElementById('nrodocempleadopro').value = "";
                document.getElementById('categoriaempleadopro').value = "";
                document.getElementById('condicionempleadopro').value = "";
                document.getElementById('celularempleadopro').value = "";
            } else {
                // Si data no es null o undefined
                document.getElementById('nrodocempleadopro').value = data.nrodoc || "";
                document.getElementById('categoriaempleadopro').value = data.cargo || "";
                document.getElementById('condicionempleadopro').value = data.condicion || "";
                document.getElementById('celularempleadopro').value = (data.celular).isNaN ? "" : data.celular;

            }
        },
        error: function (error) {
            console.error('Error al cargar datos del empleado:', error.responseText);

        }
    });

}

//El span cliente para volver al select
function removeemppro() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('nrodocempleadopro').value = '';
    document.getElementById('categoriaempleadopro').value = '';
    document.getElementById('celularempleadopro').value = '';
    /*document.getElementById('salarioempleado').value = 1;*/
    document.getElementById('empproId').value = 0;
    document.getElementById('condicionempleadopro').value = '';
    document.getElementById('empproNombre').style.display = 'none';
    document.getElementById('removeempproSpan').style.display = 'none';
    var selectElement = document.getElementById('empprocbo');
    selectElement.style.display = 'inline-block';
}

//AREGLO EXT EMP
var arregloproemp = [];



function agregarproemp() {
    var idempleado = parseInt($("#empproId").val());
    /*var salario= parseFloat($("#salarioempleado").val());*/
    var nrodoc = ($("#nrodocempleadopro").val());
    var categoria = ($("#categoriaempleadopro").val());
    var condicion = ($("#condicionempleadopro").val());
    var celular =  ($("#celularempleadopro").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idempleado) || idempleado <= 0 /*|| isNaN(salario) ||   0 >(salario)*/) {
        alert("Por favor, ingrese valores numéricos para la extracción por tipo de arbol");
        return false; // Detener la ejecución si los campos no son válidos
    }
    $.ajax({
        url: "/Pret14Produccion/EmpleadoPro",
        type: "GET",
        data: { empleadoID: idempleado },
        success: function (response) {
            if (response != null) {


                arregloproemp.push({
                    idempleado: idempleado,
                    txtempleado: response.txtnombre,
                    /*                    salario: salario,*/
                    celular: celular,
                    nrodoc: nrodoc,
                    categoria: categoria,
                    condicion: condicion
                });

                actualizartablaproemp();
                removeemppro();


            } else {
                alert("Tipo de Arbol no encontrado");
            }
        },
        error: function (response) {
            alert("Error al obtener los datos del Tipo de Arbol");
        }
    });


}

// Función para eliminar un producto del arreglo de productos seleccionados
function eliminarproemp(idvenemp) {
    var campanavenemp = arregloproemp.find(function (campTA) {
        return campTA.idempleado === idvenemp;
    });

    if (campanavenemp) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este registro de la lista?");

        if (confirmar) {
            var campTAVenindex = arregloproemp.findIndex(function (campTA) {
                return campTA.idempleado === idvenemp;
            });

            if (campTAVenindex !== -1) {
                arregloproemp.splice(campTAVenindex, 1);
                actualizartablaproemp();
            }
        }
    } else {
        alert("Registro no encontrado en la lista.");
    }
}

function actualizartablaproemp() {
    // Limpiar la tabla
    $("#empSeleccionadosPro").empty();

    // Recorrer el arreglo de tipo de árboles seleccionados y volver a agregarlos a la tabla
    arregloproemp.forEach(function (Empleado) {
        var newRow = `
            <tr id='${Empleado.idempleado}'>
                <td class="text-center">${Empleado.txtempleado}</td>
                <td class="text-center">${Empleado.condicion}</td>
                <td class="text-center">${Empleado.celular}</td>
                <td class="text-center">${Empleado.nrodoc}</td>
                <td class="text-center">${Empleado.categoria}</td>
                <td colspan='2' align='center'>
                    
                    <button type='button' class="btn btn-danger btn-sm" onclick='eliminarproemp(${Empleado.idempleado})'><i class="fas fa-trash alt"></i></button>
                </td>
            </tr>`;

        $("#empSeleccionadosPro").append(newRow);
    });
    cargarDatosemppro();

}
/*<button type='button' onclick='editarempleadosExtraccion(${Empleado.idempleado})'>Editar</button>*/




//INSUMO
$(document).ready(function () {
    $('#modalinspro').on('hidden.bs.modal', function () {
        $('#insprocbo').prop('selectedIndex', 0);
        cargarDatosinspro();
    });
});


// CARGAR INVERSIONISTA EN TABLA INVERSIONISTA
function cargarDatosinspro() {
    $.ajax({
        url: '/Pret14Produccion/RecargarInsumo',
        type: 'GET',
        success: function (response) {
            // Limpia el tbody actual
            $('#bodyinspro').empty();
            $.each(response, function (index, insumopro) {
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + insumopro.nombre+ '</td>');
                row.append('<td align="center" class="client-cell">' + insumopro.um+ '</td>');

                var button = $('<button>').addClass('btn btn-primary')
                    .text('Seleccionar')
                    .on('click', function () {
                        seleccionarinspro('modalinspro', 'insprocbo', insumopro.id, insumopro.nombre,insumopro.um);
                    });

                row.append($('<td>').addClass('client-cell text-center').append(button));

                // Append the row to the tbody
                $('#bodyinspro').append(row);

            });

        },

        error: function (error) {
            console.error('Error al cargar los datos de la campaña:', error.responseText);
        }
    });

}


// Cargar la selección de Inversionista
function seleccionarinspro(modalId, selectId, idproins, nombrecampext,um) {
    event.preventDefault();
    $.ajax({
        url: '/Pret14Produccion/VerificarIns',
        type: 'GET',
        data: { idExt: idproins },
        success: function (response) {

        },
        error: function (error) {
            console.error('Error al cargar los datos de la campaña de extracción:', error.responseText);
            reject(error);
        }
    });
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del inversionista y el botón para remover
    document.getElementById('piId').value = idproins;
    var insumoNombreSpan = document.getElementById('insproNombre');
    var removeInsumoSpan = document.getElementById('removeInsproSpan');
    insumoNombreSpan.innerText = nombrecampext;
    document.getElementById('umpi').value = um;

    insumoNombreSpan.style.display = 'inline-block';
    removeInsumoSpan.style.display = 'inline-block';
    // Ocultar el seleCAMPEXT
    var selectElement = document.getElementById('insprocbo');
    selectElement.style.display = 'none';

    cargarDatosinspro();

}

// El span inversionista para volver al select
function removeinspro() {
    event.preventDefault();
    // Quitar el valor del inversionista y ocultar el span
    document.getElementById('piId').value = null;

    document.getElementById('insproNombre').innerText = '';
    document.getElementById('umpi').value = '';
    document.getElementById('insproNombre').style.display = 'none';
    document.getElementById('removeInsproSpan').style.display = 'none';
    var selectElement = document.getElementById('insprocbo');
    selectElement.style.display = 'inline-block';
    $.ajax({
        url: "/Pret14Produccion/removeIdIns",
        type: "GET",
        success: function (response) {
            if (response.mensaje == null) {
            } else {
                alert(response.mensaje);
            }
        },
        error: function (response) {
        }
    });
    cargarDatosinspro();
}








//PRODUCTO
//Proceso para cerrar la ventana modal PRODUCTO
$(document).ready(function () {
    $('#modalprop').on('hidden.bs.modal', function () {
        $('#propcbo').prop('selectedIndex', 0);
        cargarDatosProductop();
    });
});

//Cargar la selección de PRODUCTO
function seleccionarProductop(modalId, selectId, idcod, idtext, idPro, nombrePro,um) {
    event.preventDefault();
    var ProductoIdInput = document.getElementById(idcod);
    ProductoIdInput.value = idPro;
    document.getElementById('umpp').value = um;
    document.getElementById(idtext).innerText = nombrePro;
    closeModal(modalId, selectId);
    // Mostrar el span con el nombre del cliente y el botón para remover
    var ProductoNombreSpan = document.getElementById('ProductopNombre');
    var removeProductoSpan = document.getElementById('removeProductopSpan');
    ProductoNombreSpan.innerText = nombrePro;
    ProductoNombreSpan.style.display = 'inline-block';
    removeProductoSpan.style.display = 'inline-block';
    // Ocultar el select
    var selectElement = document.getElementById('propcbo');
    selectElement.style.display = 'none';
}

//El span PRODUCTO para volver al select
function removeProductop() {
    // Quitar el valor del cliente y ocultar el span
    document.getElementById('ProductopId').value = null;
    document.getElementById('umpp').value = "";
    document.getElementById('cantpp').value = 1;

    document.getElementById('ProductopNombre').style.display = 'none';
    document.getElementById('removeProductopSpan').style.display = 'none';
    var selectElement = document.getElementById('propcbo');
    selectElement.style.display = 'inline-block';
    cargarDatosProductop();
}
function cargarDatosProductop() {
    // Obtener los IDs de los productos seleccionados
    var productosSeleccionadosIDs = ListadoProductospSeleccionados.map(function (producto) {
        return producto.idProducto;
    });

    $.ajax({
        url: '/Pret14Produccion/RecargarProductop',
        type: 'GET',
        data: { productosSeleccionadosIds: productosSeleccionadosIDs }, // Pasar los IDs como parámetro
        traditional: true, // Esto es importante para que los parámetros se pasen como una lista

        success: function (response) {
            $('#bodyProductop').empty();

            // Iterar a través de los datos del servidor
            $.each(response, function (index, producto) {
                var dimension = producto.dimension;
                var peso = producto.peso;
                var unm = producto.um;
                var diametro = producto.diametro;
                var monto_ci = parseFloat(producto.monto_ci).toFixed(2);
                var NombreProducto = producto.nombreProducto;
                var row = $('<tr>');
                row.append('<td align="center" class="client-cell">' + NombreProducto + '</td>');
                row.append('<td align="center" class="client-cell">' + monto_ci + '</td>');
                row.append('<td align="center" class="client-cell">' + unm + '</td');
                row.append('<td align="center" class="client-cell">' + dimension + '</td');
                row.append('<td align="center" class="client-cell">' + diametro + '</td');
                row.append('<td align="center" class="client-cell">' + peso + '</td');
                row.append('<td align="center" class="client-cell">' +
                    '<button class="btn btn-primary" onclick="seleccionarProductop(\'modalprop\',\'propcbo\',\'ProductopId\',\'ProductopNombre\',\'' +
                    producto.idProducto + '\', \'' + producto.nombreProducto + '\', \'' + producto.um+ '\')">Seleccionar</button>' +
                    '</td>');

                $('#bodyProductop').append(row);
            });
        },
        error: function (error) {
            console.error('Error al cargar los datos del Producto:', error.responseText);
        }
    });
}

// Declarar el arreglo para almacenar los productos seleccionados
var ListadoProductospSeleccionados = [];

// Función para añadir un producto a la lista de productos seleccionados
function agregarProductop() {
    var idProducto = parseInt($("#ProductopId").val());
    var cantidad = parseFloat($("#cantpp").val());

    // Validar que los campos sean números válidos y que el idProducto sea mayor que cero
    if (isNaN(idProducto) || idProducto <= 0 || isNaN(cantidad) ||cantidad <= 0) {
        alert("Por favor, ingrese valores numéricos válidos para el ID, cantidad y descuento.");
        return false; // Detener la ejecución si los campos no son válidos
    }

    $.ajax({
        url: "/Pret14Produccion/ProductoDatos",
        type: "GET",
        data: {
            idProducto: idProducto,
            cantidad: cantidad
        },
        success: function (response) {
            if (response != null) {
                var mci = response.monto_ci;
                var um = response.um;
                var total = response.total;

                ListadoProductospSeleccionados.push({
                    idProducto: response.idProducto,
                    nombreProducto: response.nombreProducto,
                    cu: mci,
                    cantidad: cantidad,
                    um: um,
                    total: total,
                    observacion: ""
                });

                actualizarTablaProductosSeleccionadosp();
                removeProductop();

                document.getElementById('cantpp').value = 1;
                document.getElementById('umpp').value = "";

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
function eliminarProductop(idProducto) {
    var producto = ListadoProductospSeleccionados.find(function (prod) {
        return prod.idProducto === idProducto;
    });

    if (producto) {
        // Pide confirmación antes de eliminar
        var confirmar = confirm("¿Deseas eliminar este producto de la lista?");

        if (confirmar) {
            var productoIndex = ListadoProductospSeleccionados.findIndex(function (prod) {
                return prod.idProducto === idProducto;
            });

            if (productoIndex !== -1) {
                ListadoProductospSeleccionados.splice(productoIndex, 1);
                actualizarTablaProductosSeleccionadosp();
            }
        }
    } else {
        alert("Producto no encontrado en la lista.");
    }
}

// Función para actualizar la tabla de productos seleccionados
function actualizarTablaProductosSeleccionadosp() {
    // Limpiar la tabla
    $("#productosSeleccionadosp").empty();

    // Recorrer el arreglo de productos seleccionados y volver a agregarlos a la tabla
    ListadoProductospSeleccionados.forEach(function (producto) {
        // Redondear el valor del campo "total" y otros campos a dos decimales solo en la vista
        var cu = parseFloat(producto.cu).toFixed(2);
        var total = parseFloat(producto.total).toFixed(2);

        var newRow = `
            <tr style="font-size:14px;" id='${producto.idProducto}'>
                <td style="font-size:14px;">${producto.cantidad}</td>
                <td style="font-size:14px;">${producto.nombreProducto}</td>
                <td style="font-size:14px;">${producto.um}</td>
                <td style="font-size:14px;">${cu}</td>
                <td style="font-size:14px;">${total}</td>
                <td colspan='2' style="font-size:14px;">
                    <button class="btn btn-primary btn-sm" type='button' onclick='editarProductop(${producto.idProducto})'><i class='fa fa-edit'></i></button>
                    <button class="btn btn-danger btn-sm" type='button' onclick='eliminarProductop(${producto.idProducto})'><i class='fa fa-trash'></i></button>
                </td>
            </tr>`;

        $("#productosSeleccionadosp").append(newRow);
    });
    cargarDatosProductop();
    actualizarTotalesp();
}
/*
<td>${mtoigv}</td>
<td>${msi}</td>
*/
//Función para abrir el modal de edición
function editarProductop(idProducto) {
    var producto = ListadoProductospSeleccionados.find(function (prod) {
        return prod.idProducto === idProducto;
    });
    if (producto) {
        document.getElementById("nuevaCantidadp").value = producto.cantidad;
        document.getElementById("IdEditarProductop").value = idProducto;
        document.getElementById("Productopinput").value = producto.nombreProducto;
        document.getElementById("observacionPp").value = producto.observacion;
        openSecondModal('editarModalproductop');
    } else {
        alert("Producto no encontrado en la lista.");
    }
}

// Función para aplicar los cambios desde el modal y guardarlos
function aplicarCambiospp() {
    var idProducto = parseInt(document.getElementById("IdEditarProductop").value);
    var producto = ListadoProductospSeleccionados.find(function (prod) {
        return prod.idProducto === idProducto;
    });
    var nuevaCantidad = parseFloat(document.getElementById("nuevaCantidadp").value);
    var nuevaObservacion = document.getElementById("observacionPp").value;

    if (!isNaN(nuevaCantidad) ) {
        var confirmar = confirm("¿Deseas aplicar los cambios y guardarlos?");

        if (confirmar) {

            $.ajax({
                url: "/Pret14Produccion/ProductoDatos",
                type: "GET",
                data: {
                    idProducto: idProducto,
                    cantidad: nuevaCantidad,
                },
                success: function (response) {
                    if (response != null) {
                        var total = response.total;

                        producto.cantidad = nuevaCantidad;
                        producto.total = total;
                        producto.observacion = nuevaObservacion;


                        document.getElementById('nuevaCantidadp').value = 1;
                        document.getElementById('observacionPp').value = "";

                        actualizarTablaProductosSeleccionadosp();
                        cerrarModal('editarModalproductop');
                        actualizarTotalesp();
                    } else {
                        alert("Producto no encontrado");
                    }
                },
            });

        } else {
            alert("Edición cancelada. Los cambios no se aplicaron.");
        }
    } else {
        alert("Por favor, ingresa valores numéricos válidos para cantidad y descuento.");
    }
}

// Supongamos que tienes un arreglo productosSeleccionados con la estructura:
function actualizarTotalesp() {

}




$(document).ready(function () {
    $("#guardarPro").click(function () {
        enviarDatosProGuardar();
    });
    $("#guardarycerrarPro").click(function () {
        enviarDatosProGuardaryCerrar();
    });
    $("#cancelarPro").click(function () {
        $.ajax({
            url: "/Pret14Produccion/CerrarExtraccionPro",
            type: "GET",
            success: function (response) {
                if (response.mensaje == null) {
                    window.location.href = response.redirectUrl;
                } else {
                    alert(response.mensaje);
                }
            },
            error: function (response) {
            }
        });
    });
    function enviarDatosProGuardar() {
        if (arregloproemp.length > 0) {
            if (ListadoProductospSeleccionados.length > 0) {
                if (camposRequeridosLlenos('FormProduccion')) {
                    var formData = new FormData();
                    var checkbox = document.getElementById('checkEstadoProduccion');

                    if (checkbox.checked) {
                        var checkb = true;
                    } else {
                        var checkb = false;
                    }
                    formData.append('FechaPro', $('#fechaprod').val());
                    formData.append('comentarioP', $('#comentarioP').val());
                    formData.append('cinsPro', $('#cantpi').val());
                    formData.append('productosSeleccionado', JSON.stringify(ListadoProductospSeleccionados));
                    formData.append('arregloempleadospro', JSON.stringify(arregloproemp));
                    formData.append('tipopro', $('#tipoproduccionP').val());
                    formData.append('check', checkb);


                    // Agregar archivos al FormData
                    //for (var i = 0; i < archivosRec.length; i++) {
                    //    formData.append('archivos', archivosRec[i]);
                    //}


                    $.ajax({
                        url: '/Pret14Produccion/CrearProduccion', // Ajusta la URL según la estructura de tu aplicación
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (response) {
                            if (response.mensaje == null) {
                                window.location.href = response.redirectUrl;

                            } else {
                                alert(response.mensaje);
                            }
                            $.ajax({
                                url: '/Pret14Produccion/CargarMood', // Reemplaza 'TuControlador' con el nombre real de tu controlador
                                type: 'GET',
                                success: function (response) {
                                    if (response.id == 3) {
                                        document.getElementById('moodNamePro').innerText = response.name;
                                        // Supongamos que tienes un input checkbox con el id "miCheckbox"
                                        $('#checkEstadoProduccion').prop('checked', true);
                                        $('#checkEstadoProduccion').prop('disabled', false);

                                    } else if (response.id == 4) {
                                        document.getElementById('moodNamePro').innerText = response.name;
                                        // Supongamos que tienes un input checkbox con el id "miCheckbox"
                                        $('#checkEstadoProduccion').prop('checked', true);
                                        $('#checkEstadoProduccion').prop('disabled', true);
                                    }
                                }
                            });
                        },
                        error: function (error) {
                            console.error('Error al crear la producción, error.responseText');
                        }
                    });
                } else {
                    alert('Por favor, complete todos los campos requeridos.');
                }
            } else {
                alert('Registre al menos un producto en el registro de productos para producir.');
            }
        } else {
            alert('Registre al menos un empleado.');
        }
    }
    function enviarDatosProGuardaryCerrar() {
        if (arregloproemp.length > 0) {
            if (ListadoProductospSeleccionados.length > 0) {
                if (camposRequeridosLlenos('FormProduccion')) {
                    var formData = new FormData();
                    var checkbox = document.getElementById('checkEstadoProduccion');

                    if (checkbox.checked) {
                        var checkb = true;
                    } else {
                        var checkb = false;

                    }
                    formData.append('FechaPro', $('#fechaprod').val());
                    formData.append('comentarioP', $('#comentarioP').val());
                    formData.append('cinsPro', $('#cantpi').val());
                    formData.append('productosSeleccionado', JSON.stringify(ListadoProductospSeleccionados));
                    formData.append('arregloempleadospro', JSON.stringify(arregloproemp));
                    formData.append('tipopro', $('#tipoproduccionP').val());
                    formData.append('check', checkb);


                    // Agregar archivos al FormData
                    //for (var i = 0; i < archivosRec.length; i++) {
                    //    formData.append('archivos', archivosRec[i]);
                    //}


                    $.ajax({
                        url: '/Pret14Produccion/CrearProduccionyCerrar', // Ajusta la URL según la estructura de tu aplicación
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (response) {
                            if (response.mensaje == null) {
                                window.location.href = response.redirectUrl;

                            } else {
                                alert(response.mensaje);
                            }
                        },
                        error: function (error) {
                            console.error('Error al crear la producción, error.responseText');
                        }
                    });
                } else {
                    alert('Por favor, complete todos los campos requeridos.');
                }
            } else {
                alert('Registre al menos un producto en el registro de productos para producir.');
            }
        } else {
            alert('Registre al menos un empleado.');
        }
    }
});


$(document).ready(function () {
    $('#confirmarEliminarProduccion').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var idEnvio = button.data('id');
        $(this).data('id', idEnvio);
    });
    $('#confirmarAnularProduccion').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var idEnvio = button.data('id');
        $(this).data('id', idEnvio);
    });
});


function eliminarProduccion() {

    var idproduccion = $('#confirmarEliminarProduccion').data('id');
    console.log('ID del predio a eliminar:', idproduccion);
    $.ajax({
        url: '/Pret14Produccion/EliminarProduccion', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        data: { id: idproduccion },
        success: function (response) {
            if (response.mensaje == null) {
                $('#confirmarEliminarProduccion').modal('hide');

                window.location.href = response.redirectUrl;

            } else {


            }
        },
        error: function (error) {
            console.error('Error al intentar eliminar', error.responseText);
        }
    });
}

function anularProduccion() {

    var idproduccion = $('#confirmarAnularProduccion').data('id');
    console.log('ID del predio a eliminar:', idproduccion);
    $.ajax({
        url: '/Pret14Produccion/AnularProduccion', // Reemplaza 'TuControlador' con el nombre real de tu controlador
        type: 'POST',
        data: { id: idproduccion },
        success: function (response) {
            if (response.mensaje == null) {
                $('#confirmarAnularProduccion').modal('hide');

                window.location.href = response.redirectUrl;

            } else {


            }
        },
        error: function (error) {
            console.error('Error al intentar eliminar', error.responseText);
        }
    });
}
