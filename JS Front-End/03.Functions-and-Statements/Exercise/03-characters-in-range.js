function getCharsInRange(a, b) {
    const asciiCodeA = a.charCodeAt(0);
    const asciiCodeB = b.charCodeAt(0);

    let result = '';
    for (let i = Math.min(asciiCodeA, asciiCodeB) + 1; i < Math.max(asciiCodeA, asciiCodeB); i++) {
        result += String.fromCharCode(i) + ' ';      
    }

    console.log(result.trimEnd());
}

getCharsInRange('a', 'd');
getCharsInRange('#', ':');
getCharsInRange('C', '#');