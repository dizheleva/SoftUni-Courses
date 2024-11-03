function solve(m, n) {
    let sum = 0;
    let numbers = '';

    for (let index = m; index <= n; index++) {
        numbers += index + ' ';
        sum += index;        
    }
    
    console.log(numbers.trimEnd()); 
    console.log(`Sum: ${sum}`); 
}

solve(5, 10);
solve(0, 26);
solve(50, 60);