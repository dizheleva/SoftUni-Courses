function carWash(commands) {
    let cleanPercentage = 0;

    for (const command of commands) {
        switch (command) {
            case 'soap':
                cleanPercentage += 10;
                break;
            case 'water':
                cleanPercentage += 0.2 * cleanPercentage;
                break;
            case 'vacuum cleaner':
                cleanPercentage += 0.25 * cleanPercentage;
                break;
            case 'mud':
                cleanPercentage -= 0.1 * cleanPercentage;
                break;
        }
    }

    console.log(`The car is ${cleanPercentage.toFixed(2)}% clean.`)
}

carWash(['soap', 'soap', 'vacuum cleaner', 'mud', 'soap', 'water']);
carWash(["soap", "water", "mud", "mud", "water", "mud", "vacuum cleaner"]);