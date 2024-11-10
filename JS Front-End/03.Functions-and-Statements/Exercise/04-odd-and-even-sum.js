function printOddEvenDigitSum(number) {
    let oddSum = 0;
    let evenSum = 0;

    while(number > 0) {
        let lastDigit = number % 10;

        if (lastDigit % 2 === 0) {
            evenSum += lastDigit;
        } else {
            oddSum += lastDigit;
        }

        number = Math.trunc(number / 10);
    } 

    console.log(`Odd sum = ${oddSum}, Even sum = ${evenSum}`);
}

printOddEvenDigitSum(1000435);
printOddEvenDigitSum(3495892137259234);