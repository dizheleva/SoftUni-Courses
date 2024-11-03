function solve(speed, area) {
    let limit;

    switch (area) {
        case 'motorway':
            limit = 130;
            break;
        case 'interstate':
            limit = 90;
            break;
        case 'city':
            limit = 50;
            break;
        case 'residential':
            limit = 20;
            break;
        default:
            break;
    }

    if (speed <= limit) {
        console.log(`Driving ${speed} km/h in a ${limit} zone`);
    } else {
        let difference = speed - limit;
        let status;

        switch (true) {
            case difference <= 20:
                status = 'speeding';
                break;
            case difference <= 40:
                status = 'excessive speeding';
                break;
            default:
                status = 'reckless driving';
                break;
        }

        console.log(`The speed is ${difference} km/h faster than the allowed speed of ${limit} - ${status}`);
    }
}

solve(40, 'city');
solve(21, 'residential');
solve(120, 'interstate');
solve(200, 'motorway');