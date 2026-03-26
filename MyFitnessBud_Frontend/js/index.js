document.addEventListener("DOMContentLoaded", function () {

    const loggedInUser = localStorage.getItem("loggedInUser");

    const outLink = document.querySelector(".logoutLink");
    const logOut = document.querySelector(".main");
    const logIn = document.querySelector(".mainLogin");
    const signUpBtn = document.querySelector(".btn-primary");
    const loginBtn = document.querySelector(".btn-secondary");
    const header = document.querySelector("header h1");
    const subHeader = document.querySelector("header p");
    let loginTime = new Date();

    // ----------------------------
    // USER STATE HANDLING
    // ----------------------------

    function updateDisplay() {
        let now = new Date();
        document.getElementById("currentDate").textContent = now.toLocaleString();

        let elapsed = now - loginTime;
        let seconds = Math.floor((elapsed / 1000) % 60);
        let minutes = Math.floor((elapsed / (1000 * 60)) % 60);
        let hours = Math.floor((elapsed / (1000 * 60 * 60)));

        document.getElementById("loginDuration").textContent = hours + "h " + minutes + "m " + seconds + "s";
    }

    if (loggedInUser) {
        // User is logged in
        const storedUser = localStorage.getItem("user");
        let userData = null;

        if (storedUser) {
            try {
                userData = JSON.parse(storedUser);
            } catch (err) {
                console.error("User data parse failed", err);
            }
        }

        if (userData && userData.username === loggedInUser) {
            header.textContent = `Welcome back, ${userData.username}!`;
            subHeader.textContent = `Weight: ${userData.weight || "N/A"} lbs | Height: ${userData.height || "N/A"} in`;

            document.getElementById("profileUsername").textContent = `Username: ${userData.username}`;
            document.getElementById("profileWeight").textContent = `Weight: ${userData.weight || "N/A"} lbs`;
            document.getElementById("profileHeight").textContent = `Height: ${userData.height || "N/A"} in`;
        } else {
            header.textContent = `Welcome back, ${loggedInUser}!`;
            subHeader.textContent = `Feel free to continue to the links above, and start your health journey!`;
            document.getElementById("profileUsername").textContent = `Username: ${loggedInUser}`;
            document.getElementById("profileWeight").textContent = "Weight: (Not set)";
            document.getElementById("profileHeight").textContent = "Height: (Not set)";
        }

        // Hide signup/login buttons and show other things
        logOut.style.display = "none";
        signUpBtn.style.display = "none";
        loginBtn.style.display = "none";
        setInterval(updateDisplay, 1000);
        updateDisplay();
    } else {
        // User is NOT logged in
        logIn.style.display = "none";
        outLink.textContent = "Login";
        outLink.href = "login.html";
    }

    // ----------------------------
    // LOGOUT FUNCTIONALITY
    // ----------------------------

    outLink.addEventListener("click", function (e) {

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
        window.location.href = "signup.html";
    });

    loginBtn.addEventListener("click", function () {
        window.location.href = "login.html";
    });

});