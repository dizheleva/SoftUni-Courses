function modificateNumber(number) {
    const getDigitsAverage = (n) => Array.from(String(n)).reduce((avg, digit, idx) => (avg * idx + Number(digit)) / (idx + 1), 0);

    let digitsAverage = getDigitsAverage(number);
    while (digitsAverage <= 5) {
        number = Number(String(number) + '9');
        digitsAverage = getDigitsAverage(number);
    }
    
    console.log(number);
}

modificateNumber(101);
modificateNumber(5835);