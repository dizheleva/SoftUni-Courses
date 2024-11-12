function validatePoints(arr) {
    const getPointsDistance = (pointOneX, pointOneY, pointTwoX, pointTwoY) => Math.sqrt(Math.pow((pointTwoX - pointOneX), 2) + Math.pow((pointTwoY - pointOneY), 2));
    const checkDistance = (distance) => Number.isInteger(distance) ? 'valid' : 'invalid';
    
    const x1 = arr[0];
    const y1 = arr[1];
    const x2 = arr[2];
    const y2 = arr[3];
    
    const zeroOneDistance = getPointsDistance(0, 0, x1, y1);
    const zeroTwoDistance = getPointsDistance(0, 0, x2, y2);
    const oneTwoDistance = getPointsDistance(x1, y1, x2, y2);

    console.log(`{${x1}, ${y1}} to {0, 0} is ${checkDistance(zeroOneDistance)}`);
    console.log(`{${x2}, ${y2}} to {0, 0} is ${checkDistance(zeroTwoDistance)}`);
    console.log(`{${x1}, ${y1}} to {${x2}, ${y2}} is ${checkDistance(oneTwoDistance)}`);
}

validatePoints([3, 0, 0, 4]);
validatePoints([2, 1, 1, 1]);