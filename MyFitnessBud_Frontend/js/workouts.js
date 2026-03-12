document.addEventListener("DOMContentLoaded", function () {
    const loggedInUser = localStorage.getItem("loggedInUser");
    
    const outLink = document.querySelector(".logoutLink");

    //Code here

    outLink.addEventListener("click", function (e) {

        if (loggedInUser) {
            e.preventDefault(); // Prevent immediate navigation

            localStorage.removeItem("loggedInUser");

            alert("You have been logged out.");

            window.location.href = "login.html";
        }
    });
});