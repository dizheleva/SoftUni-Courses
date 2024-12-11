document.addEventListener('DOMContentLoaded', solve);

function solve() {
    const contentEl = document.getElementById('content');
    const formEl = document.getElementById('task-input');

    formEl.addEventListener('submit', (e) => {
        e.preventDefault();

        const sections = formEl.querySelector('input[type="text"]').value.split(', ');

        sections.forEach(el => {
            const newDivEl = document.createElement('div');
            const newPEl = document.createElement('p');

            newPEl.textContent = el;
            newPEl.style.display = 'none';

            newDivEl.append(newPEl);
            newDivEl.addEventListener('click', (e) => {
                e.target.querySelector('p').style.display = 'block';
            });

            contentEl.append(newDivEl);
        });
    });
}