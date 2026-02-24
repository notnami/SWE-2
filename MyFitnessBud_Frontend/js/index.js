document.addEventListener("DOMContentLoaded", function () {

    const loggedInUser = localStorage.getItem("loggedInUser");

    const logoutLink = document.querySelector(".logout");
    const signUpBtn = document.querySelector(".btn-primary");
    const loginBtn = document.querySelector(".btn-secondary");
    const header = document.querySelector("header h1");
    const subHeader = document.querySelector("header p")

    // ----------------------------
    // USER STATE HANDLING
    // ----------------------------

    if (loggedInUser) {
        // User is logged in
        header.textContent = `Welcome back, ${loggedInUser}!`;
        subHeader.textContent = `Review your account statistics or continue to the links above.`;

        // Hide signup/login buttons
        signUpBtn.style.display = "none";
        loginBtn.style.display = "none";
    } else {
        // User is NOT logged in
        logoutLink.textContent = "Login";
        logoutLink.href = "login.html";
    }

    // ----------------------------
    // LOGOUT FUNCTIONALITY
    // ----------------------------

    logoutLink.addEventListener("click", function (e) {

        if (loggedInUser) {
            e.preventDefault(); // Prevent immediate navigation

            localStorage.removeItem("loggedInUser");

            alert("You have been logged out.");

            window.location.href = "login.html";
        }
    });

    // ----------------------------
    // BUTTON NAVIGATION
    // ----------------------------

    signUpBtn.addEventListener("click", function () {
        window.location.href = "login.html";
    });

    loginBtn.addEventListener("click", function () {
        window.location.href = "login.html";
    });

});