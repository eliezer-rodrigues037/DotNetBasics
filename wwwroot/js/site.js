// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Sucesso(data) {

    Swal.fire({
        position: 'center',
        icon: 'success',
        title: `${data.msg}`,
        showConfirmButton: true,
        timer: 1500
    }).then(() => {
        //if(data.url)
            window.location.href = `${data.url}`;
    });

    
}

function Falha() {
    Swal.fire(
        'Falha',
        'Deu bode! Tente Novamente mais tarde.',
        'error'
    );
}