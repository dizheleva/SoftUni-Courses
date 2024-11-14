function printHeroes(arr) {
    let heroes = [];

    for (let i = 0; i < arr.length; i++) {
        const dataArgs = arr[i].split(' / ');

        heroes.push(
            {
                hero: dataArgs[0],
                level: Number(dataArgs[1]),
                items: dataArgs[2].split(', ')
            }
        );
    }

    heroes = heroes.sort((a, b) => a.level - b.level);
    heroes.forEach(h => {
        console.log(`Hero: ${h.hero}`);
        console.log(`level => ${h.level}`);
        console.log(`items => ${h.items.join(', ')}`);
    });
}

printHeroes(
    [
        'Isacc / 25 / Apple, GravityGun',
        'Derek / 12 / BarrelVest, DestructionSword',
        'Hes / 1 / Desolator, Sentinel, Antara'
    ]
);
printHeroes(
    [
        'Batman / 2 / Banana, Gun',
        'Superman / 18 / Sword',
        'Poppy / 28 / Sentinel, Antara'
    ]
);