function visualizeLoading(n) {
    let symbolString = '%'.repeat(n / 10) + '.'.repeat(10 - n / 10); 

    if (n < 100) {
        console.log(`${n}% [${symbolString}]`);
        console.log('Still loading...');
    } else {
        console.log(`${n}% Complete!`);
        console.log(`[${symbolString}]`);
    }
}

visualizeLoading(30);
visualizeLoading(50);
visualizeLoading(100);