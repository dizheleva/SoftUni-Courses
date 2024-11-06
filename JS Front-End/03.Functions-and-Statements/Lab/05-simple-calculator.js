function calculate(a, b, operation) {
    let sum = (a, b) => a + b;
    let subtract = (a, b) => a - b;
    let multiply = (a, b) => a * b;
    let divide = (a, b) => a / b;

    let result = operation === 'add' ? sum(a, b)
    : operation === 'subtract' ? subtract(a, b)
    : operation === 'multiply' ? multiply(a, b)
    : divide(a, b);   

    console.log(result);
}    

calculate(5, 5, 'multiply');
calculate(40, 8, 'divide');
calculate(12, 19, 'add');
calculate(50, 13, 'subtract');