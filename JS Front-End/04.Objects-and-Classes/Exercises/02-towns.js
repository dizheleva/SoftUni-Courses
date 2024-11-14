function printObjects(table) {
    let towns = [];

    for (const row of table) {
        const [town, latitude, longitude] = row.split(' | ');
        towns.push(
            {
                town: town,
                latitude: Number(latitude).toFixed(2),
                longitude: Number(longitude).toFixed(2)
            }
        );
    }

    towns.forEach(t => console.log(t));
}

printObjects(
    [
        'Sofia | 42.696552 | 23.32601',
        'Beijing | 39.913818 | 116.363625'
    ]
);
printObjects(['Plovdiv | 136.45 | 812.575']);