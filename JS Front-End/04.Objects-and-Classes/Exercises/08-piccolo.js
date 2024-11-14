function getParkingData(arr) {
    let carNumbers = {};

    for (let i = 0; i < arr.length; i++) {
        const [direction, carNumber] = arr[i].split(', ');

        if (carNumbers.hasOwnProperty(carNumber)) {
            carNumbers[carNumber] = direction;
        } else {
            if (direction === 'IN') {
                carNumbers[carNumber] = direction;
            }
        }
    }

    carNumbers = Object.entries(carNumbers)
    .filter(car => car[1] === 'IN')
    .map(car => car[0])
    .sort((a, b) => a.localeCompare(b));
    
    console.log(carNumbers.length > 0 ? carNumbers.join('\n') : 'Parking Lot is Empty');
}

getParkingData(
    [
        'IN, CA2844AA',
        'IN, CA1234TA',
        'OUT, CA2844AA',
        'IN, CA9999TT',
        'IN, CA2866HI',
        'OUT, CA1234TA',
        'IN, CA2844AA',
        'OUT, CA2866HI',
        'IN, CA9876HH',
        'IN, CA2822UU'
    ]
);
getParkingData(
    [
        'IN, CA2844AA',
        'IN, CA1234TA',
        'OUT, CA2844AA',
        'OUT, CA1234TA'
    ]
);