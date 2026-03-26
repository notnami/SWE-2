document.addEventListener("DOMContentLoaded", function () {

    const usernameInput = document.getElementById("username");
    const passwordInput = document.getElementById("password");

    const buttons = document.querySelectorAll(".login button");
    const loginBtn = buttons[0];
    const signUpBtn = buttons[1];

    // Helper function to show alerts nicely
    function showMessage(message, type = "info") {
        alert(message); 
        // Later you could replace this with a Bootstrap alert
    }

    // LOGIN FUNCTION
    loginBtn.addEventListener("click", function () {

        const username = usernameInput.value.trim();
        const password = passwordInput.value.trim();

        if (username === "" || password === "") {
            showMessage("Please enter both username and password.", "danger");
            return;
        }

        const storedUser = localStorage.getItem("user");

        if (!storedUser) {
            showMessage("No account found. Please sign up first.", "warning");
            return;
        }

        const parsedUser = JSON.parse(storedUser);

        if (parsedUser.username === username && parsedUser.password === password) {
            showMessage("Login successful!", "success");

            // Save session state
            localStorage.setItem("loggedInUser", username);

            // Redirect to home page
            window.location.href = "index.html";
        } else {
            showMessage("Invalid username or password.", "danger");
        }
    });

    // SIGN UP BUTTON NAVIGATION
    signUpBtn.addEventListener("click", function () {
        window.location.href = "signup.html";
    });

});