function solve(startingYield) {
    let currentYield = startingYield;
    let totalSpice = 0;
    let days = 0;

    while (currentYield >= 100) {
        totalSpice += currentYield;
        totalSpice -= 26;
        currentYield -= 10;
        days++;
    }

    if (totalSpice >= 26) {
        totalSpice -= 26;
    }

    console.log(days);
    console.log(totalSpice);
}

solve(111);
solve(450);