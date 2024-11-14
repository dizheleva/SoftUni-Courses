function printEmployees(arr) {
    let list = [];
    
    for (const data of arr) {
        const employeeObj = {
            name: data,
            number: data.length
        };
        list.push(employeeObj);
    }

    list.forEach(e => console.log(`Name: ${e.name} -- Personal Number: ${e.number}`));
}

printEmployees(
    [
        'Silas Butler',
        'Adnaan Buckley',
        'Juan Peterson',
        'Brendan Villarreal'
    ]
);
printEmployees(
    [
        'Samuel Jackson',
        'Will Smith',
        'Bruce Willis',
        'Tom Holland'
    ]
);