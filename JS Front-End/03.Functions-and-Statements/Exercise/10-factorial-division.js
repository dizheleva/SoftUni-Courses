function divideFactorials(first, second) {
    let getFactorial = ((number) => {
        if (number === 0) { 
            return 1; 
        } else { 
            return number * getFactorial(number - 1); 
        }
    });

    console.log((getFactorial(first) / getFactorial(second)).toFixed(2));
}

divideFactorials(5, 2);
divideFactorials(6, 2);