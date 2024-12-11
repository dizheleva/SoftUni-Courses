document.addEventListener('DOMContentLoaded', solve);

function solve() {    
    document.querySelector('#input').addEventListener('submit', (e) => {
        e.preventDefault();

        const input = e.target.querySelector('textarea').value;
        const products = JSON.parse(input);
        const tbody = document.querySelector('table tbody');

        products.forEach(product => {
            let tr = document.createElement('tr');

            const img = document.createElement('img');
            img.setAttribute('src', product.img);
            addTd(tr, img);

            const pName = document.createElement('p');
            pName.textContent = product.name;
            addTd(tr, pName);

            const pPrice = document.createElement('p');
            pPrice.textContent = product.price;
            addTd(tr, pPrice);

            const pDecFactor = document.createElement('p');
            pDecFactor.textContent = product.decFactor;
            addTd(tr, pDecFactor);

            const input = document.createElement('input');
            input.type = 'checkbox';
            addTd(tr, input);

            tbody.appendChild(tr);
        });
    });

    function addTd(tr, element) { 
        const td = document.createElement('td');
        td.appendChild(element);
        tr.appendChild(td);
    }

    document.querySelector('#shop').addEventListener('submit', (e) => {
        e.preventDefault();

        const checkedProducts = Array.from(document.querySelectorAll('tr:has(input:checked)'));
        const data = checkedProducts.map(product => ({
            name: product.children[1].textContent.trim(),
            price: Number(product.children[2].textContent),
            decFactor: Number(product.children[3].textContent)
        }));

        let output = `Bought furniture: ${data.map(product => product.name).join(', ')} \n`;
        output += `Total price: ${data.reduce((total, product) => total + product.price, 0)} \n`;
        output += `Average decoration factor: ${data.reduce((factor, product) => factor + product.decFactor, 0) / data.length} \n`;

        e.target.querySelector('textarea').value = output;
    });
}