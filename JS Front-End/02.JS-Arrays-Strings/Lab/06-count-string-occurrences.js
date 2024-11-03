function solve(text, searchedWord) {
    console.log(text.split(' ').filter(w => w === searchedWord).length);
}

solve('This is a word and it also is a sentence', 'is');
solve('softuni is great place for learning new programming languages', 'softuni');