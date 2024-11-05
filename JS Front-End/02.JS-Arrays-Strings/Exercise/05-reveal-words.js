function solve(wordList, text) {
    let words = wordList
        .split(', ')
        .sort((a, b) => b.length - a.length);

    let result = text; 

    for (const word of words) {
        let search = '*'.repeat(word.length);
        result = result.replaceAll(search, word);
    }

    console.log(result);
}

solve('great', 'softuni is ***** place for learning new programming languages');
solve('great, learning', 'softuni is ***** place for ******** new programming languages');