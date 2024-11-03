function solve(people, groupType, day) {
    let price;
    let result;

    switch (groupType) {
        case 'Students':
            switch (day) {
                case 'Friday':
                    price = 8.45;
                    break;
                case 'Saturday':
                    price = 9.80;
                    break;
                case 'Sunday':
                    price = 10.46;
                    break;
                default:
                    result = 'Error!'
                    break;
            }

            if (people >= 30) {
                price *= 0.85;
            }
            break;
        case 'Business':
            switch (day) {
                case 'Friday':
                    price = 10.90;
                    break;
                case 'Saturday':
                    price = 15.60;
                    break;
                case 'Sunday':
                    price = 16;
                    break;
                default:
                    result = 'Error!'
                    break;
            }

            if (people >= 100) {
                people -= 10;
            }
            break;
        case 'Regular':
            switch (day) {
                case 'Friday':
                    price = 15;
                    break;
                case 'Saturday':
                    price = 20;
                    break;
                case 'Sunday':
                    price = 22.50;
                    break;
                default:
                    result = 'Error!'
                    break;
            }

            if (people >= 10 && people <= 20) {
                price *= 0.95;
            }
            break;
        default:
            result = 'Error!';
            break;
    }

    result = people * price;
    console.log(`Total price: ${result.toFixed(2)}`);
}

solve(30, "Students", "Sunday");
solve(40, "Regular", "Saturday");