function colorize() {
    let rows = document.querySelectorAll("table tr");

    for (let index = 0; index < rows.length; index++) {
        const row = rows[index];
        
        if (index % 2 == 0) {
            row.style.background = "teal";
        }
    }
}