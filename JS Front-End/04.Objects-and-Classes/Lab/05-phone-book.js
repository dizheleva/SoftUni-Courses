function printPhoneBook(arr) {
    let phoneBook = [];
    for (const element of arr) {
        const [name, phone] = element.split(' ');
        
        if (phoneBook.some(x => x.name === name)) {
            let record = phoneBook.find(x => x.name === name);
            record.phone = phone;
        } else {
            const record = {
                name: name,
                phone: phone
            };
            phoneBook.push(record);
        }
    }

    for (const record of phoneBook) {
        console.log(`${record.name} -> ${record.phone}`);
    }
}

printPhoneBook(
    [
        'Tim 0834212554',
        'Peter 0877547887',
        'Bill 0896543112',
        'Tim 0876566344'
    ]
);
printPhoneBook(
    [
        'George 0552554',
        'Peter 087587',
        'George 0453112',
        'Bill 0845344'
    ]
);