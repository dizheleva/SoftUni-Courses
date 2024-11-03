function solve(x1, y1, x2, y2) {
    let zeroOneDistanse = Math.sqrt(Math.pow((0 - x1), 2) + Math.pow((0 - y1), 2));
    let zeroTwoDistanse = Math.sqrt(Math.pow((0 - x2), 2) + Math.pow((0 - y2), 2));
    let oneTwoDistanse = Math.sqrt(Math.pow((x2 - x1), 2) + Math.pow((y2 - y1), 2));

    let isValidZeroOne = Number.isInteger(zeroOneDistanse) ? 'valid' : 'invalid';
    let isValidZeroTwo = Number.isInteger(zeroTwoDistanse) ? 'valid' : 'invalid';
    let isValidOneTwo = Number.isInteger(oneTwoDistanse) ? 'valid' : 'invalid';

    console.log(`{${x1}, ${y1}} to {0, 0} is ${isValidZeroOne}`);
    console.log(`{${x2}, ${y2}} to {0, 0} is ${isValidZeroTwo}`);
    console.log(`{${x1}, ${y1}} to {${x2}, ${y2}} is ${isValidOneTwo}`);
}

solve(3, 0, 0, 4);
solve(2, 1, 1, 1);