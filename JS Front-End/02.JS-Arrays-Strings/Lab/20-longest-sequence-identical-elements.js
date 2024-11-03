function solve(arr) {
    let newArr = [];   
    let currentLength = 0;
    let repeatNumber;
    let counter = 0;

    for (let i = 0; i < arr.length - 1; i++) {        
        if (arr[i] === arr[i + 1]) {
            counter++;
            if (counter > currentLength) {
                currentLength = counter;
                repeatNumber = arr[i];
            }
        } else {
            counter = 0;
        }
    }

    for (let j = 0; j < currentLength + 1; j++) {
        newArr.push(repeatNumber);
    }

    console.log(newArr.join(' '));
}

solve([2, 2, 2, 3, 4, 4, 2, 2, 2, 1]);
solve([1, 1, 1, 2, 3, 1, 3, 3, 1, 1]);
solve([1, 1, 2, 3, 4, 5, 6, 2, 2]);
solve([4, 4, 4, 4]);