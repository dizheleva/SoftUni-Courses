function printAddressBook(arr) {
    let addressBook = [];
    for (const element of arr) {
        const [name, address] = element.split(':');

        if (addressBook.some(x => x.name === name)) {
            let record = addressBook.find(x => x.name === name);
            record.address = address;
        } else {
            const record = {
                name: name,
                address: address
            };
            addressBook.push(record);
        }
    }

    for (const record of addressBook.sort((a, b) => a.name.localeCompare(b.name))) {
        console.log(`${record.name} -> ${record.address}`);
        
    }
}

printAddressBook(
    [
        'Tim:Doe Crossing',
        'Bill:Nelson Place',
        'Peter:Carlyle Ave',
        'Bill:Ornery Rd'
    ]
);
printAddressBook(
    [
        'Bob:Huxley Rd',
        'John:Milwaukee Crossing',
        'Peter:Fordem Ave',
        'Bob:Redwing Ave',
        'George:Mesta Crossing',
        'Ted:Gateway Way',
        'Bill:Gateway Way',
        'John:Grover Rd',
        'Peter:Huxley Rd',
        'Jeff:Gateway Way',
        'Jeff:Huxley Rd'
    ]
);