function processCrystal(arr) {
    const target = arr[0];

    for (let i = 1; i < arr.length; i++) {
        let chunk = arr[i];
        console.log(`Processing chunk ${chunk} microns`);
        
        let cutCount = 0;
        let lapCount = 0;
        let grindCount = 0;
        let etchCount = 0;

        while (chunk / 4 >= target) {
            chunk = chunk / 4;
            cutCount++;
        }
        chunk = Math.floor(chunk);

        while (chunk * 0.8 >= target) {
            chunk = chunk * 0.8;
            lapCount++;
        }
        chunk = Math.floor(chunk);

        while (chunk - 20 >= target) {
            chunk -= 20;
            grindCount++;
        }
        
        while (chunk - 2 >= target - 1) {
            chunk -= 2;
            etchCount++;
        }
                        
        if (cutCount > 0) {
            console.log(`Cut x${cutCount}`);
            console.log('Transporting and washing');
        }

        if (lapCount > 0) {
            console.log(`Lap x${lapCount}`);
            console.log('Transporting and washing');
        }

        if (grindCount > 0) {
            console.log(`Grind x${grindCount}`);
            console.log('Transporting and washing');
        }

        if (etchCount > 0) {
            console.log(`Etch x${etchCount}`);
            console.log('Transporting and washing');
        }

        if (chunk < target) {
            chunk++;
            console.log(`X-ray x1`);
        }

        console.log(`Finished crystal ${chunk} microns`);
    }
}

processCrystal([1375, 50000]);
processCrystal([1000, 4000, 8100]);