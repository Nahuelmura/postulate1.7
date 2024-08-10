$(document).ready(function() {
    $('#verMasBtn').on('click', function() {
        var servicioID = $(this).data('id');
        cargarPerfil(servicioID);
    });
});

function cargarPerfil(servicioID) {
    $.ajax({
        url: `/VistaServicio/PerfilServicio/${servicioID}`,
        type: 'POST',
        dataType: 'json',
        success: function(response) {
            if (response.success) {
                var perfil = response.data;
                var contenidoTabla = `
                    <tr>
                        <td>${perfil.nombrePersona}</td>
                        <td>${perfil.apellidoPersona}</td>
                        <td>${perfil.telefonoPersona}</td>
                        <td>${perfil.edadPersona}</td>
                        <td>${perfil.documentoPersona}</td>
                        <td>${perfil.nombreProfesion}</td>
                        <td>${perfil.imagenID}</td>
                        <td class="text-center">${perfil.Herramienta ? 'Sí' : 'No'}</td>
                        <td>${perfil.descripcion}</td>
                        <td>${perfil.titulo}</td>
                        <td>${perfil.institucion}</td>
                        <td>${perfil.emailPersona}</td>
                        <td class="text-center">
                            <button type="button" class="btn btn-success" onclick="AbrirModalEditar(${perfil.ServicioID})">
                                <i class="fa-regular fa-pen-to-square"></i> Editar
                            </button>
                            <button type="button" class="btn btn-danger" onclick="EliminarServicio(${perfil.ServicioID})">
                                <i class="fa-regular fa-trash-can"></i> Eliminar
                            </button>
                        </td>
                    </tr>`;
                $('#perfilTableBody').html(contenidoTabla);
                $('#perfilContainer').show();
            } else {
                console.error(response.message);
                alert(response.message);
            }
        },
        error: function(xhr, status, error) {
            console.error('Error al cargar el perfil del servicio:', error);
        }
    });
}
