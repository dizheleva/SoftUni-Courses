function solve(a, b, operation) {
    let result;

    switch (operation) {
        case '+':
            result = a + b;
            break;
        case '-':
            result = a - b;
            break;
        case '*':
            result = a * b;
            break;
        case '/':
            result = a / b;
            break;
        case '%':
            result = a % b;
            break;
        case '**':
            result = a ** b;
            break;
        default:
            result = 'Error!';
            break;
    }

    console.log(result);
}

solve(5, 6, '+');
solve(3, 5.5, '*');