function raisedPower(number, power) {
    let result = 1;
    for (let i = 0; i < power; i++) {
        result *=number;     
    }

    console.log(result);
}

raisedPower(2, 8);
raisedPower(3, 4);