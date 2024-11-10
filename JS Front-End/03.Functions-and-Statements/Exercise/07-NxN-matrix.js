function printNxNMatrix(n) {
    let row = (n.toString() + ' ').repeat(n).trimEnd();
    for (let r = 0; r < n; r++) {
        console.log(row);
    }
}

printNxNMatrix(3);
printNxNMatrix(7);
printNxNMatrix(2);