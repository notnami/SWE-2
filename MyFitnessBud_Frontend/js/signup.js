document.addEventListener("DOMContentLoaded", function () {
    const usernameInput = document.getElementById("username");
    const passwordInput = document.getElementById("password");
    const weightInput = document.getElementById("weight");
    const heightInput = document.getElementById("height");
    const signUpBtn = document.querySelector(".signup button");

    function showMessage(msg) {
        alert(msg);
    }

    signUpBtn.addEventListener("click", function () {
        const username = usernameInput.value.trim();
        const password = passwordInput.value.trim();
        const weight = weightInput.value.trim();
        const height = heightInput.value.trim();

        if (!username || !password || !weight || !height) {
            showMessage("Please complete all signup fields.");
            return;
        }

        const user = {
            username: username,
            password: password,
            weight: Number(weight),
            height: Number(height)
        };

        localStorage.setItem("user", JSON.stringify(user));
        localStorage.setItem("loggedInUser", username);

        showMessage("Account created successfully! Redirecting to home page...");
        window.location.href = "index.html";
    });
});
