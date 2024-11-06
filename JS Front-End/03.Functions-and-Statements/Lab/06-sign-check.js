function checkSign(numOne, numTwo, numThree) {
    let result;
    if (numOne < 0) {
        if (numTwo < 0 && numThree < 0 || numTwo >= 0 && numThree >= 0) {
            result = 'Negative';
        } else {
            result = 'Positive';
        } 
    } else {
        if (numTwo < 0 && numThree < 0 || numTwo >= 0 && numThree >= 0) {
            result = 'Positive';
        } else {
            result = 'Negative';
        } 
    }

    console.log(result);
}

checkSign(5, 12, -15);
checkSign(-6, -12, 14);
checkSign(-1, -2, -3);
checkSign(-5, 1, 1);