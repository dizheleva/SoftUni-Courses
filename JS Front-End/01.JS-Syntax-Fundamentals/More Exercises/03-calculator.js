function solve(a, operation, b) {
    let result;

    switch (operation) {
        case '+':
            result = a + b;
            break;
        case '-':
            result = a - b;
            break;
        case '/':
            result = a / b;
            break;
        case '*':
            result = a * b;
            break;        
    }

    console.log(result.toFixed(2));
}    

solve(5, '+', 10);
solve(25.5, '-', 3);