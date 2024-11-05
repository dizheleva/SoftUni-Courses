function decrypt(input) {
    const pattern = /#([A-z]+)/g;

    const matches = input.matchAll(pattern);

    for (const match of matches) {
        console.log(match[1]);
    }
}

decrypt('Nowadays everyone uses # to tag a #special word in #socialMedia');
decrypt('The symbol # is known #variously in English-speaking #regions as the #number sign');