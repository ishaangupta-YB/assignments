document.addEventListener("DOMContentLoaded", () => {
    const user = JSON.parse(localStorage.getItem("user"));
    if(!user) window.location.href = "index.html";
    else  document.getElementById("greeting").textContent = `Hello, ${user.username}`;
    document.getElementById("logoutBtn").addEventListener("click",  () => {
        localStorage.removeItem("user");
        window.location.href = "index.html";
    });
});