function convertObject(jsonString) {
    const obj = JSON.parse(jsonString);

    for (const key in obj) {
        console.log(`${key}: ${obj[key]}`);
    }
}

convertObject('{"name": "George", "age": 40, "town": "Sofia"}');
convertObject('{"name": "Peter", "age": 35, "town": "Plovdiv"}');