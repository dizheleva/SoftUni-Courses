sum = ((a, b) => a + b);
subtract = ((a, b) => a - b);

function getResult(a, b, c) {
    let result = subtract(sum(a, b), c);
    console.log(result);
}

getResult(23, 6, 10);
getResult(1, 17, 30);
getResult(42, 58, 100);