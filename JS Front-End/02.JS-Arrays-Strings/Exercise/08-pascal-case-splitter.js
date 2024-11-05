function solve(input) {
    let result = [];
    const inputToLower = input.toLowerCase();

    let start = 0;

    for (let i = 1; i < input.length; i++) {        
        if (input[i] !== inputToLower[i]) {
            const end = i;
            result.push(input.substring(start, end)); 
            start = end;         
        }
    }

    result.push(input.substring(start));

    console.log(result.join(', '));
}

solve('SplitMeIfYouCanHaHaYouCantOrYouCan');
solve('HoldTheDoor');
solve('ThisIsSoAnnoyingToDo');