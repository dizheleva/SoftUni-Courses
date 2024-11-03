function solve(numberString, a1, a2, a3, a4, a5) {
    let number = Number(numberString);
    let actions = [a1, a2, a3, a4, a5];

    for (const action of actions) {
        switch (action) {
            case 'chop':
                number /= 2;
                console.log(number);
                break;
            case 'dice':
                number = Math.sqrt(number);
                console.log(number);
                break;
            case 'spice':
                number++;
                console.log(number);
                break;
            case 'bake':
                number *= 3;
                console.log(number);
                break;
            case 'fillet':
                number -= 0.2 * number;
                console.log(number);
                break;
        }
    }
}

solve('32', 'chop', 'chop', 'chop', 'chop', 'chop');
solve('9', 'dice', 'spice', 'chop', 'bake', 'fillet');