function getSmallestOfThree(a, b, c) {
    let result = Math.min(a, Math.min(b, c));
    console.log(result);
}

getSmallestOfThree(2, 5, 3);
getSmallestOfThree(600, 342, 123);
getSmallestOfThree(25, 21, 4);
getSmallestOfThree(2, 2, 2);