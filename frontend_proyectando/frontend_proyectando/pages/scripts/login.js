$(document).ready(function () {
    $("#btnLogin").click(function () {
        let username = $("#usuario").val().trim();
        let password = $("#clave").val().trim();

        if (username === "" || password === "") {
            showError("Debe ingresar usuario y contraseña.");
            return;
        }
        // AJAX permite a las aplicaciones web validar información específica en formularios antes de que los usuarios los envíen
        $.ajax({
            url: "http://localhost:53850/api/user/login",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ username, password }),
            success: function (data) {
                if (data.mensaje === "OK") {
                    // Guardamos documento para luego consultar empleado
                    localStorage.setItem("documentoEmp", data.documentoEmp);
                    window.location.href = "inicio.html";
                } else {
                    showError("Credenciales inválidas.");
                }
            },
            error: function (xhr) {
                if (xhr.status !== 200 && xhr.responseJSON?.Message) {
                    showError(xhr.responseJSON.Message);
                } else {
                    showError("Error al conectar con el servidor.");
                }
            }
        });
    });

    function showError(msg) {
        $("#msgError").removeClass("d-none").text(msg);
    }
});
