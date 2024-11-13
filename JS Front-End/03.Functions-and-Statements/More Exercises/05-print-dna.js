function printDna(length) {
    let sequence = 'ATCGTTAGGG'.split('');

    for (let i = 1; i <= length; i++) {
        let [a, b] = sequence.splice(0, 2);

        if (i % 4 === 1) {
            console.log(`**${a}${b}**`);
        }
        else if (i % 4 === 2) {
            console.log(`*${a}--${b}*`);
        }
        else if (i % 4 === 3) {
            console.log(`${a}----${b}`);
        }
        else if (i % 4 === 0) {
            console.log(`*${a}--${b}*`);
        }

        sequence.push(a);
        sequence.push(b);
    }
}

printDna(4);
printDna(10);