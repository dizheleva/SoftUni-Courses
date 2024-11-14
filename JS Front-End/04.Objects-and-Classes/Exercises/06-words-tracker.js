function countWords(arr) {
    let words = {};
    arr.shift().split(' ').forEach(word => words[word] = 0 );

    for (const word of arr) {
        if (words.hasOwnProperty(word)) {
            words[word]++;
        }
    }

    sortedWords = Object.entries(words).sort((a, b) => b[1] - a[1]);
    sortedWords.forEach(w => console.log(`${w[0]} - ${w[1]}`));
}

countWords(
    [
        'this sentence',
        'In', 'this', 'sentence', 'you', 'have', 'to', 'count', 'the', 'occurrences', 'of', 'the', 'words', 'this', 'and', 'sentence', 'because', 'this', 'is', 'your', 'task'
    ]
);
countWords(
    [
        'is the',
        'first', 'sentence', 'Here', 'is', 'another', 'the', 'And', 'finally', 'the', 'the', 'sentence'
    ]
);