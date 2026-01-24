// Confirmaciones con JavaScript
document.addEventListener('DOMContentLoaded', function () {
    
    // ========================================
    // 1. CONFIRMACIÓN AL ELIMINAR OBRAS
    // ========================================
    const formsEliminar = document.querySelectorAll('.form-eliminar');
    
    formsEliminar.forEach(function (form) {
        form.addEventListener('submit', function (event) {
            // Mostrar confirmación
            const confirmar = confirm('¿Está seguro de que desea eliminar esta obra? Esta acción no se puede deshacer.');
            
            if (!confirmar) {
                // Cancelar el envío del formulario si el usuario hace clic en "Cancelar"
                event.preventDefault();
            }
        });
    });

    // ========================================
    // 2. CONFIRMACIÓN AL BLOQUEAR USUARIO
    // ========================================
    const btnsBloquear = document.querySelectorAll('.btnBloquear');
    
    btnsBloquear.forEach(function (btn) {
        btn.addEventListener('click', function (event) {
            // Obtener el email del usuario desde el elemento hermano
            const emailUsuario = this.parentElement.querySelector('.email-usuario').textContent;
            
            const confirmar = confirm(`¿Está seguro de que desea bloquear al usuario ${emailUsuario}?\n\nEste usuario no podrá acceder al sistema.`);
            
            if (!confirmar) {
                // Cancelar la navegación si el usuario hace clic en "Cancelar"
                event.preventDefault();
            }
        });
    });

    // ========================================
    // 3. CONFIRMACIÓN AL DESBLOQUEAR USUARIO
    // ========================================
    const btnsDesbloquear = document.querySelectorAll('.btnDesbloquear');
    
    btnsDesbloquear.forEach(function (btn) {
        btn.addEventListener('click', function (event) {
            // Obtener el email del usuario desde el elemento hermano
            const emailUsuario = this.parentElement.querySelector('.email-usuario').textContent;
            
            const confirmar = confirm(`¿Desea desbloquear al usuario ${emailUsuario}?\n\nEste usuario podrá volver a acceder al sistema.`);
            
            if (!confirmar) {
                // Cancelar la navegación si el usuario hace clic en "Cancelar"
                event.preventDefault();
            }
        });
    });
});