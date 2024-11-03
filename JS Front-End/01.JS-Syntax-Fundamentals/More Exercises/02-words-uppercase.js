function solve(text) {    
    console.log(text.toUpperCase().match(/\w+/g).join(', '));
}

solve('Hi, how are you?');
solve('hello');