function extractElements(sentence) {
    let words = {};
    const sentenceArr = sentence.toLowerCase().split(' ');

    for (const word of sentenceArr) {
        if (words.hasOwnProperty(word)) {
            words[word]++;
        } else {
            words[word] = 1;
        }
    }

    const filteredKeys = Object.entries(words).filter(a => a[1] % 2 !== 0).map(a => a[0]);    
    console.log(filteredKeys.join(' '));
}

extractElements('Java C# Php PHP Java PhP 3 C# 3 1 5 C#');
extractElements('Cake IS SWEET is Soft CAKE sweet Food');