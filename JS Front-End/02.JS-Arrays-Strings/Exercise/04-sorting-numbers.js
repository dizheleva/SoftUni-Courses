function solve(arr) {
    arr.sort((a, b) => a - b);
    let isOddSize = arr.length % 2 !== 0;
    let cycles = isOddSize ? (arr.length - 1) / 2 : arr.length / 2;

    let outputArr = [];
    for (let i = 0; i < cycles; i++) {
        outputArr.push(arr[i]); 
        outputArr.push(arr[arr.length - 1 - i]);

        if (isOddSize && i === cycles - 1) {
            outputArr.push(arr[cycles]);
        } 
    }

    console.log(outputArr.join(', '));
}

solve([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]);