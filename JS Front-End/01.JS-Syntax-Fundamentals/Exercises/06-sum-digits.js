function solve(num) {
    let sum = 0;

    while (num >= 1) {
        sum += num % 10;
        num = Math.trunc(num / 10);
    }

    console.log(sum);
}

solve(245678);
solve(97561);
solve(543);