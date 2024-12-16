function solve(input) {
    const farmersCount = input.shift();
    const farmersInput = input.splice(0, farmersCount);

    const farmers = farmersInput.reduce((farmers, farmer) => {
        let [name, workArea, tasks] = farmer.split(' ');
        tasks = tasks.split(',');
        farmers[name] = { workArea: workArea, tasks: tasks };

        return farmers;
    }, {});

    input.forEach(entry => {
        const line = entry.split(' / ');
        const command = line.shift();

        let name = line.shift();
        switch (command) {
            case 'Execute':
                let [inputArea, givenTask] = line;
                
                if (farmers[name].workArea == inputArea && farmers[name].tasks.includes(givenTask)) {
                    console.log(`${name} has executed the task: ${givenTask}!`);
                } else {
                    console.log(`${name} cannot execute the task: ${givenTask}.`);
                }

                break;

            case 'Change Area':
                let newArea = line[0];

                farmers[name].workArea = newArea;
                console.log(`${name} has changed their work area to: ${farmers[name].workArea}`);

                break;

            case 'Learn Task':
                let learnedTask = line[0];

                if (farmers[name].tasks.includes(learnedTask)) {
                    console.log(`${name} already knows how to perform ${learnedTask}.`);
                } else {
                    farmers[name].tasks.push(learnedTask);
                    console.log(`${name} has learned a new task: ${learnedTask}.`);
                }

                break;
        }
    });

    Object.keys(farmers).forEach(name => {
        let output = `Farmer: ${name}, Area: ${farmers[name].workArea}, Tasks: ${farmers[name].tasks.sort().join(', ')}`;
        console.log(output);
    });
}

solve([
    "2",
    "John garden watering,weeding",
    "Mary barn feeding,cleaning",
    "Execute / John / garden / watering",
    "Execute / Mary / garden / feeding",
    "Learn Task / John / planting",
    "Execute / John / garden / planting",
    "Change Area / Mary / garden",
    "Execute / Mary / garden / cleaning",
    "End"
]);

// John has executed the task: watering!
// Mary cannot execute the task: feeding.
// John has learned a new task: planting.
// John has executed the task: planting!
// Mary has changed their work area to: garden
// Mary has executed the task: cleaning!
// Farmer: John, Area: garden, Tasks: planting, watering, weeding
// Farmer: Mary, Area: garden, Tasks: cleaning, feeding

solve([
    "3",
    "Alex apiary harvesting,honeycomb",
    "Emma barn milking,cleaning",
    "Chris garden planting,weeding",
    "Execute / Alex / apiary / harvesting",
    "Learn Task / Alex / beeswax",
    "Execute / Alex / apiary / beeswax",
    "Change Area / Emma / apiary",
    "Execute / Emma / apiary / milking",
    "Execute / Chris / garden / watering",
    "Learn Task / Chris / pruning",
    "Execute / Chris / garden / pruning",
    "End"
]);

// Alex has executed the task: harvesting!
// Alex has learned a new task: beeswax.
// Alex has executed the task: beeswax!
// Emma has changed their work area to: apiary
// Emma has executed the task: milking!
// Chris cannot execute the task: watering.
// Chris has learned a new task: pruning.
// Chris has executed the task: pruning!
// Farmer: Alex, Area: apiary, Tasks: beeswax, harvesting, honeycomb
// Farmer: Emma, Area: apiary, Tasks: cleaning, milking
// Farmer: Chris, Area: garden, Tasks: planting, pruning, weeding