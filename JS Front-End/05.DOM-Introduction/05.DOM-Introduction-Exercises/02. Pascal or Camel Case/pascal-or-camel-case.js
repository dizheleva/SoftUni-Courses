function solve() {
    const text = document.querySelector('#text').value;
    const namingConvention = document.querySelector('#naming-convention').value;

    const words = text.toLowerCase().split(' ');

    function capitaliseWord(word) {
        return word[0].toUpperCase() + word.slice(1);
    }

    const resultEl = document.querySelector('#result');

    switch (namingConvention.split(' ')[0]) {
        case 'Camel':
            resultEl.textContent = words[0] + words.slice(1).map(capitaliseWord).join('');
            break;

        case 'Pascal':
            resultEl.textContent = words.map(capitaliseWord).join('');
            break;

        default:
            resultEl.textContent = 'Error!';
            break;
    }
}