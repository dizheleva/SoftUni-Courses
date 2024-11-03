function solve(lostFights, helmetPrice, swordPrice, shieldPrice, armorPrice) {
    
    let headsetsCost = 0;
    let mousesCost = 0;
    let keyboardsCost = 0;
    let displaysCost = 0;
    
    let keyboardsCounter = 0;
    for (let game = 1; game <= lostFights; game++) {
        
        if (game % 2 === 0) {
            headsetsCost += helmetPrice;
        }
        if (game % 3 === 0) {
            mousesCost += swordPrice;
        } 
        if (game % 2 === 0 && game % 3 === 0) {
            keyboardsCost += shieldPrice;
            keyboardsCounter++;
            if (keyboardsCounter % 2 === 0) {
                displaysCost += armorPrice;
                keyboardsCounter = 0;
            } 
        }        
    }
    let totalCost = headsetsCost + mousesCost + keyboardsCost + displaysCost;
    
    console.log(`Gladiator expenses: ${totalCost.toFixed(2)} aureus`);
}

solve(7, 2, 3, 4, 5);
solve(23, 12.50, 21.50, 40, 200);