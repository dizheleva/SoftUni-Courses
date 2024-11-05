function solve(word, text) {
    let words = text.toLowerCase().split(' ');
    for (let el of words) {
        if (word === el) {
            console.log(word);
            return;
        }
    }

    console.log(`${word} not found!`);
}

solve('javascript', 'JavaScript is the best programming language');
solve('python', 'JavaScript is the best programming language');