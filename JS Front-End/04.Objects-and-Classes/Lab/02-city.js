function getProps(obj) {
    for (const prop in obj) {
        console.log(`${prop} -> ${obj[prop]}`);
    }
}

getProps(
    {
        name: "Sofia",
        area: 492,
        population: 1238438,
        country: "Bulgaria",
        postCode: "1000"
    }
);
getProps(
    {
        name: "Plovdiv",
        area: 389,
        population: 1162358,
        country: "Bulgaria",
        postCode: "4000"
    }
);