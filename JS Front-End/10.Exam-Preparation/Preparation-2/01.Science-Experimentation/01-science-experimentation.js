function solve(input) {
    const chemicalsCount = input.shift();
    const chemicals = input.splice(0, chemicalsCount).reduce((chemicals, chemical) => {
        const [name, quantity] = chemical.split(' # ');
        chemicals[name] = { quantity: Number(quantity) };

        return chemicals;
    }, {});

    input.forEach(entry => {
        const line = entry.split(' # ');
        const command = line.shift();

        const name = line.shift();
        switch (command) {
            case 'Mix':
                let [secondName, mixAmount] = line;
                mixAmount = Number(mixAmount);

                if (chemicals[name].quantity >= mixAmount && chemicals[secondName].quantity >= mixAmount) {
                    chemicals[name].quantity -= mixAmount;
                    chemicals[secondName].quantity -= mixAmount;
                    console.log(`${name} and ${secondName} have been mixed. ${mixAmount} units of each were used.`);
                } else {
                    console.log(`Insufficient quantity of ${name}/${secondName} to mix.`);
                }

                break;

            case 'Replenish':
                const replenishAmount = Number(line[0]);

                if (!chemicals[name]) {
                    console.log(`The Chemical ${name} is not available in the lab.`);
                } else {
                    if (chemicals[name].quantity + replenishAmount > 500) {
                        const addedAmount = 500 - chemicals[name].quantity;
                        chemicals[name].quantity = 500;

                        console.log(`${name} quantity increased by ${addedAmount} units, reaching maximum capacity of 500 units!`);
                    } else {
                        chemicals[name].quantity += replenishAmount;

                        console.log(`${name} quantity increased by ${replenishAmount} units!`);
                    }
                }

                break;

            case 'Add Formula':
                const formula = line[0];

                if (!chemicals[name]) {
                    console.log(`The Chemical ${name} is not available in the lab.`);
                } else {
                    chemicals[name].formula = formula;

                    console.log(`${name} has been assigned the formula ${formula}.`);
                }

                break;
        }
    });

    Object.keys(chemicals).forEach(name => {
        let output = `Chemical: ${name}, Quantity: ${chemicals[name].quantity}`;
        
        if (Object.keys(chemicals[name]).includes('formula')) {
            output += `, Formula: ${chemicals[name].formula}`;
        }

        console.log(output);
    });
}

solve([
    '4',
    'Water # 200',
    'Salt # 100',
    'Acid # 50',
    'Base # 80',
    'Mix # Water # Salt # 50',
    'Replenish # Salt # 150',
    'Add Formula # Acid # H2SO4',
    'End'
]);

solve([
    '3',
    'Sodium # 300',
    'Chlorine # 100',
    'Hydrogen # 200',
    'Mix # Sodium # Chlorine # 200',
    'Replenish # Sodium # 250',
    'Add Formula # Sulfuric Acid # H2SO4',
    'Add Formula # Sodium # Na',
    'Mix # Hydrogen # Chlorine # 50',
    'End'
]);