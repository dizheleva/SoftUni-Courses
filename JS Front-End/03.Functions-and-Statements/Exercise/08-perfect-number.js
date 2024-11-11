function checkPerfectNumber(number) {
    if (number <= 0) {
        console.log('It\'s not so perfect.');
        return;
    }

    let aliquotSum = 0;
    for (let i = 1; i < number; i++) {
        if (number % i === 0) {
            aliquotSum += i;
        }
    }
    
    if (aliquotSum === number) {
        console.log('We have a perfect number!');
    } else {
        console.log('It\'s not so perfect.');
    }
}

checkPerfectNumber(6);
checkPerfectNumber(28);
checkPerfectNumber(1236498);