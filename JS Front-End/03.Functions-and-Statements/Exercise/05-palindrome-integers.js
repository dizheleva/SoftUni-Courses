function checkForPalindrome(arr) {
    let resultArr = [];

    for (let i = 0; i < arr.length; i++) {
        let currentNum = arr[i];
        let reversedNum = reverseNumber(arr[i]);
        let isPalindrome = reversedNum === currentNum;
        resultArr.push(isPalindrome);
    }

    for (const element of resultArr) {
        console.log(element);
    }
}

function reverseNumber(n) {
    let reversed = '';

    while (n >= 1) {
        reversed += (n % 10);
        n = Math.floor(n / 10);
    }

    reversed = Number(reversed);
    return reversed;
}

checkForPalindrome([123, 323, 421, 121]);
checkForPalindrome([32, 2, 232, 1010]);