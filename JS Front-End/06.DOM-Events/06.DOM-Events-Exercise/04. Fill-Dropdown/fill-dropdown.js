document.addEventListener('DOMContentLoaded', solve);

function solve() {
    document.querySelector('form').addEventListener('submit', (e) => {
        e.preventDefault();

        const newElementText = e.target.querySelector('#newItemText').value;
        const newElementValue = e.target.querySelector('#newItemValue').value;

        if (newElementText.length === 0 || newElementValue.length === 0) return;

        const optionElement = document.createElement('option');

        optionElement.textContent = newElementText;
        optionElement.setAttribute('value', newElementValue);

        document.querySelector('#menu').appendChild(optionElement);

        e.target.reset();
        e.target.querySelector('#newItemText').focus();
    });
}