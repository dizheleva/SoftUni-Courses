function convertToJson(firstName, lastName, hairColor) {
    const jsObject = {
        name: firstName,
        lastName: lastName,
        hairColor: hairColor
    };

    console.log(JSON.stringify(jsObject));
}

convertToJson('George', 'Jones', 'Brown');
convertToJson('Peter', 'Smith', 'Blond');