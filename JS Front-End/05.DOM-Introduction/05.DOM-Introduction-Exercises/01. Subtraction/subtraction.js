function subtract() {
    let first = Number(document.getElementById('firstNumber').value);
    let second = Number(document.getElementById('secondNumber').value);
    document.getElementById('result').append(first - second);
}