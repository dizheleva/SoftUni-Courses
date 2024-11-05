function decrypt(wordList, text) {
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

decrypt('great', 'softuni is ***** place for learning new programming languages');
decrypt('great, learning', 'softuni is ***** place for ******** new programming languages');