function solve(arr, step) {
    let outputArr = [];
    for (let i = 0; i < arr.length; i+=step) {
        outputArr.push(arr[i]);
    }
    
    return outputArr;
}

solve(['5', '20', '31', '4', '20'], 2);
solve(['dsa', 'asd', 'test', 'tset'], 2);
solve(['1', '2', '3', '4', '5'], 6);