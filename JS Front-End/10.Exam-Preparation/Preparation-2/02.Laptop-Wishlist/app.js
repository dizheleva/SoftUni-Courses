window.addEventListener("load", solve);

function solve() {
    function createElement(tag, properties, container) {
        const element = document.createElement(tag);

        Object.keys(properties).forEach(key => {
            if (typeof properties[key] === 'object') {
                Object.assign(element[key], properties[key]);
            } else {
                element[key] = properties[key];
            }
        });

        if (container) {
            container.append(element);
        }

        return element;
    }

    const list = document.querySelector('#check-list');
    const fields = Array.from(document.querySelectorAll('#laptop-model, #storage, #price'));
    const addButton = document.querySelector('#add-btn');
    addButton.addEventListener('click', addHandler);

    function addHandler(e) {
        e.preventDefault();

        const [model, storage, price] = fields.map(field => field.value);

        if (!model || !storage || !price) return;

        const li = createElement('li', { className: 'laptop-item' }, list);
        const article = createElement('article', {}, li);
        createElement('p', { name: model, textContent: model }, article);
        createElement('p', { name: storage, textContent: `Memory: ${storage} TB` }, article);
        createElement('p', { name: price, textContent: `Price: ${price}$` }, article);
        createElement('button', { classList: 'btn edit', textContent: 'edit', onclick: editHandler }, li);
        createElement('button', { classList: 'btn ok', textContent: 'ok', onclick: doneHandler }, li);

        addButton.disabled = true;
        fields.forEach(field => field.value = '');
    }

    function editHandler(e) {
        e.preventDefault();

        const el = e.target.closest('li');
        el.remove();

        let values = Array.from(el.querySelectorAll('p')).map(p => p.name);        
        fields.forEach((field, index) => field.value = values[index]);

        addButton.disabled = false;
    }

    function doneHandler(e) {
        e.preventDefault();

        el = e.target.closest('li');
        el.querySelectorAll('button').forEach(b => b.remove());
        el.remove();

        document.querySelector('#laptops-list').append(el);
        addButton.disabled = false;
    }

    document.querySelector('.clear').addEventListener('click', reloadHandler);

    function reloadHandler(e) {
        e.preventDefault();
        window.location.reload();
    }
}