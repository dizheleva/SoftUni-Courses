function printProvisions(arr1, arr2) {
    let stock = {};

    for (let i = 0; i < arr1.length; i++) {
        let name = '';
        let quantity = 0;

        if (i % 2 !== 0) {
            name = arr1[i - 1];
            quantity = Number(arr1[i]);
            stock[name] = quantity;
        }
    }

    for (let i = 0; i < arr2.length; i++) {
        let name;
        let quantity;

        if (i % 2 !== 0) {
            name = arr2[i - 1];
            quantity = Number(arr2[i]);

            if (stock.hasOwnProperty(name)) {
                stock[name] += quantity;
            } else {
                stock[name] = quantity;
            }
        }        
    }
    
    Object.entries(stock).forEach(([key, value]) => console.log(`${key} -> ${value}`));
}

printProvisions(
    [
        'Chips', '5', 'CocaCola', '9', 'Bananas', '14', 'Pasta', '4', 'Beer', '2'
    ],
    [
        'Flour', '44', 'Oil', '12', 'Pasta', '7', 'Tomatoes', '70', 'Bananas', '30'
    ]
);
printProvisions(
    [
        'Salt', '2', 'Fanta', '4', 'Apple', '14', 'Water', '4', 'Juice', '5'
    ],
    [
        'Sugar', '44', 'Oil', '12', 'Apple', '7', 'Tomatoes', '7', 'Bananas', '30'
    ]
);