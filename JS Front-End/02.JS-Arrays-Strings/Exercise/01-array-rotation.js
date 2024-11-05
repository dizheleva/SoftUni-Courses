function solve(arr, n) {
    while (n > 0) {
        arr.push(arr.shift());
        n--;
    }

    console.log(arr.join(' '));
}

solve([51, 47, 32, 61, 21], 2);
solve([32, 21, 61, 1], 4);
solve([2, 4, 15, 31], 5);