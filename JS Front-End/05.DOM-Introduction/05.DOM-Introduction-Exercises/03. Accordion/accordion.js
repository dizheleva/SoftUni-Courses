function toggle() {
    const button = document.getElementsByClassName("button")[0];
    const content = document.getElementById("extra");

    if (content.style.display === "block") {
        button.textContent = "More";
        content.style.display = "none";
    } else {
        content.style.display = "block";
        button.textContent = "Less";
    }
}