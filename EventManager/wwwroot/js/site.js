// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function successInfo(infoKey, sessionKey) {
    if (sessionStorage.getItem(sessionKey) === "true") {
        toastr.success(sessionStorage.getItem(infoKey));
        sessionStorage.clear();
    }
}

function errorInfo(infoKey, sessionKey) {
    if (sessionStorage.getItem(sessionKey) === "true") {
        toastr.error(sessionStorage.getItem(infoKey));
        sessionStorage.clear();
    }
}
