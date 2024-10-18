document.addEventListener("DOMContentLoaded", () => {
     
    const loginForm = document.getElementById("loginForm");
    const registerForm = document.getElementById("registerForm");

    const loginShowPassword = document.getElementById("loginShowPassword");
    const registerShowPassword = document.getElementById("registerShowPassword");

    document.getElementById("showRegister").addEventListener("click", () => {
        loginForm.style.display = "none";
        registerForm.style.display = "block";
    });

    document.getElementById("showLogin").addEventListener("click", () => {
        registerForm.style.display = "none";
        loginForm.style.display = "block";
    });

    loginShowPassword.addEventListener("change", function () {
        const passwordField = document.getElementById("loginPassword");
        passwordField.type = this.checked ? "text" : "password";
    });

    registerShowPassword.addEventListener("change", function () {
        const passwordField = document.getElementById("registerPassword");
        const confirmPasswordField = document.getElementById("confirmPassword");
        passwordField.type = this.checked ? "text" : "password";
        confirmPasswordField.type = this.checked ? "text" : "password";
    });

    registerForm.addEventListener("submit", function (e) {
        e.preventDefault();
        const username = document.getElementById("registerUsername").value;
        const password = document.getElementById("registerPassword").value;
        const confirmPassword = document.getElementById("confirmPassword").value;

        if (password !== confirmPassword) {
            alert("Passwords do not match");
            return;
        }

        const users = JSON.parse(localStorage.getItem("users")) || [];
        const userExists = users.some(user => user.username === username);

        if (userExists) {
            alert("Username already exists. Please choose a different username.");
        } else {
            users.push({ username, password });
            localStorage.setItem("users", JSON.stringify(users));
            alert("Registration successful! You can now log in.");

            registerForm.style.display = "none";
            loginForm.style.display = "block";
        }
    });

    loginForm.addEventListener("submit", function (e) {
        e.preventDefault();
        const username = document.getElementById("loginUsername").value;
        const password = document.getElementById("loginPassword").value;

        const users = JSON.parse(localStorage.getItem("users")) || [];

        const user = users.find(user => user.username === username && user.password === password);

        if (user) {
            localStorage.setItem("user", JSON.stringify(user));  
            window.location.href = "home.html";  
        } else {
            alert("Invalid username or password.");
        }
    }); 
});