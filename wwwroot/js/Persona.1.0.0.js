$(document).ready(function() {
    $("#formulario").on("submit", function(event) {
        event.preventDefault();
        if (Guardar()) {
            enviarFormularioAjax();
        }
    });
});

function Guardar() {
    console.log("probando");
    if ($("#ProvinciaID").val() === "") {
        alert("El campo Provincia no puede estar vacío.");
        $("#ProvinciaID").focus();
        return false;
    }
    if ($("#LocalidadID").val() === "") {
        alert("El campo Localidad no puede estar vacío.");
        $("#LocalidadID").focus();
        return false;
    }
    if ($("#nombre").val() === "") {
        alert("El campo Nombre no puede estar vacío.");
        $("#nombre").focus();
        return false;
    }
    if ($("#apellido").val() === "") {
        alert("El campo Apellidos no puede estar vacío.");
        $("#apellido").focus();
        return false;
    }
    if ($("#edad").val() === "") {
        alert("El campo edad no puede estar vacío.");
        $("#edad").focus();
        return false;
    }
    if ($("#telefono").val() === "") {
        alert("El campo telefono no puede estar vacío.");
        $("#telefono").focus();
        return false;
    }
    if ($("#documento").val() === "") {
        alert("El campo documento no puede estar vacío.");
        $("#documento").focus();
        return false;
    }
  
    return true;
}

function enviarFormularioAjax() {
    console.log("probando2");
    let formulario = $("#formulario").serialize(); // Serializa el formulario

    $.ajax({
        url: '../../Persona/Guardar',
        data: formulario,
        type: 'POST',
        dataType: 'json',
        success: function(resultado) {
            alert(resultado.mensaje);
            console.log("Formulario guardado exitosamente");
            $("#formulario").fadeOut("slow");   // Hacemos desaparecer el div "formulario" con un efecto fadeOut lento.
            if (resultado.exito) {  // Verifica si el resultado contiene un campo indicando éxito
                $("#exito").delay(500).fadeIn("slow");  // Si hemos tenido éxito, hacemos aparecer el div "exito" con un efecto fadeIn lento tras un delay de 0,5 segundos.
            } else {
                $("#fracaso").delay(500).fadeIn("slow");  // Si no, lo mismo, pero haremos aparecer el div "fracaso".
            }
        },
        error: function(xhr, status) {
            alert('Disculpe, existió un problema al guardar el formulario');
        }
    });
}


function AbrirModalEditar(PersonaID) {

    $.ajax({
        url: '../../Persona/TraerDatosPersonal',
        data: { PersonaID: 4 },
        type: 'POST',
        dataType: 'json',
        success: function (personas) {
            let persona = personas[0];

            document.getElementById("PersonaID").value = PersonaID;
            // $("#ModalTitulo").text("Editar Tipo De ejercico");
            document.getElementById("nombre").value = persona.nombre;
            document.getElementById("apellido").value = persona.apellido;
            document.getElementById("edad").value = persona.edad;
            document.getElementById("telefono").value = persona.telefono;
            document.getElementById("documento").value = persona.documento;
          

            $("#ModalFormulario").modal("show");

        },
        error: function (xhr, status) {

            alert('Disculpe, existió un problema ');
        }

    });

}





