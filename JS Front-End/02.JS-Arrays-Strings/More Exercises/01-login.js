function solve(arr) {
    let username = arr[0];
    let reversedUsername = username.split('').reverse().join('');
    let loginAttempts = 0;
    
    for (let i = 1; i < arr.length; i++) {
        const password = arr[i];
        if (password === reversedUsername) {
            console.log(`User ${username} logged in.`);
        } else {
            if (loginAttempts === 3) {
                console.log(`User ${username} blocked!`);
                return;
            }

            loginAttempts++;
            console.log('Incorrect password. Try again.');
        }
    }
}

solve(['Acer','login','go','let me in','recA']);
solve(['momo','omom']);
solve(['sunny','rainy','cloudy','sunny','not sunny']);