function solve(shifts) {
    const goldGramPrice = 67.51;
    const bitcoinPrice = 11949.16;

    let bitcoins = 0;
    let firstBitcoinDay = 0;
    let money = 0.00;

    for (let i = 1; i <= shifts.length; i++) {
        const gold = i % 3 !== 0 ? shifts[i - 1] : 0.70 * shifts[i - 1];
        
        money += gold * goldGramPrice;
        
        while (money >= bitcoinPrice) {
            if (firstBitcoinDay === 0) {
                firstBitcoinDay = i;
            }

            bitcoins ++;
            money -= bitcoinPrice;
        }
    }

    console.log(`Bought bitcoins: ${bitcoins}`);
    if (firstBitcoinDay !== 0) {
        console.log(`Day of the first purchased bitcoin: ${firstBitcoinDay}`);
    }
    console.log(`Left money: ${money.toFixed(2)} lv.`);
}

solve([100, 200, 300]);
solve([50, 100]);
solve([3124.15, 504.212, 2511.124]);