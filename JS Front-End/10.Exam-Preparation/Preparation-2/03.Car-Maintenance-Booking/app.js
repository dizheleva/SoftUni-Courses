function getData(baseUrl, onSuccess) {
    fetch(baseUrl)
        .then(response => response.json())
        .then(onSuccess)
        .catch(error => console.error('Error: ', error));
}

function createDataEntry(baseUrl, entry, onSuccess) {
    fetch(baseUrl, {
        method: 'POST',
        body: JSON.stringify(entry)
    })
        .then(response => response.json())
        .then(onSuccess)
        .catch(error => console.error('Error: ', error));
}

function updateDataEntry(baseUrl, entry, onSuccess) {
    fetch(baseUrl + entry._id, {
        method: 'PUT',
        body: JSON.stringify(entry)
    })
        .then(response => response.json())
        .then(onSuccess)
        .catch(error => console.error('Error: ', error));
}

function deleteDataEntry(baseUrl, entry, onSuccess) {
    fetch(baseUrl + entry._id, {
        method: 'DELETE',
        body: JSON.stringify(entry)
    })
        .then(response => response.json())
        .then(onSuccess)
        .catch(error => console.error('Error: ', error));
}

function createElement(tag, properties, container = null) {

    const element = document.createElement(tag);

    Object.keys(properties).forEach(key => {
        if (typeof properties[key] === 'object') {
            Object.assign(element.dataset, properties[key])
        } else {
            element[key] = properties[key];
        }
    });

    if (container) container.append(element);

    return element;
}


function init() {

    const baseUrl = 'http://localhost:3030/jsonstore/appointments/';

    const inputs = Array.from(document.querySelectorAll('#car-model, #car-service, #date'));
    const listEl = document.querySelector('#appointments-list');

    //Load Data
    const loadButton = document.querySelector('#load-appointments');
    loadButton.addEventListener('click', loadData);

    function loadData(e) {
        listEl.innerHTML = '';

        getData(baseUrl, (result) => {
            Object.values(result).forEach(createDomEntry);
        });

        editButtonEl.disabled = true;
    }

    //Create data entry

    const addButtonEl = document.querySelector('#add-appointment');
    addButtonEl.addEventListener('click', createHandler);

    function createHandler(e) {
        e.preventDefault();

        const [model, service, date] = inputs.map(input => input.value);

        if (!model || !service || !date) return;

        const entry = { model, service, date };

        createDataEntry(baseUrl, entry, (result) => {
            createDomEntry(result);
        });

        inputs.forEach(input => input.value = '');
    }

    function createDomEntry({ model, service, date, _id }) {
        const entryEl = createElement('li', { className: 'appointment', id: _id, dataset: { model, service, date, _id } }, listEl);
        createElement('h2', { textContent: model }, entryEl);
        createElement('h3', { textContent: date }, entryEl);
        createElement('h3', { textContent: service }, entryEl);
        const buttonsDiv = createElement('div', { className: 'buttons-appointment' }, entryEl);
        createElement('button', { className: 'change-btn', textContent: 'Change', onclick: changeHandler }, buttonsDiv);
        createElement('button', { className: 'delete-btn', textContent: 'Delete', onclick: deleteHandler }, buttonsDiv);
    }

    //Edit data entry    

    function changeHandler(e) {
        const entryEl = e.target.closest('li');
        entryEl.classList.add('active');

        //without using dataset
        //const values = Array.from(entryEl.querySelectorAll('h2, h3')).map(p => p.textContent);
        const values = Object.values(entryEl.dataset);
        inputs.forEach((input, index) => input.value = values[index]);

        editButtonEl.disabled = false;
        addButtonEl.disabled = true;
    }

    //Delete data entry     

    function deleteHandler(e) {
        const entryEl = e.target.closest('li');

        //without using dataset
        const _id = entryEl.id;
        const [model, date, service] = Array.from(entryEl.querySelectorAll('h2, h3')).map(p => p.textContent);
        const entry = { model, service, date, _id };
        //const entry = Object.assign({}, entryEl.dataset);

        deleteDataEntry(baseUrl, entry, (result) => {
            deleteMatch(result);
        });
    }

    function deleteMatch({ host, score, guest, _id }) {
        listEl.querySelector(`li[data-_id="${_id}]`).remove();
    }

    //Update data

    const editButtonEl = document.querySelector('#edit-appointment');
    editButtonEl.addEventListener('click', updateHandler);

    function updateHandler(e) {
        e.preventDefault();

        const [model, service, date] = inputs.map(field => field.value);

        if (!model || !service || !date) return;

        const entryEl = document.querySelector('li.active');

        const entry = { model: model, service: service, date: date, _id: entryEl.id };

        updateDataEntry(baseUrl, entry, (result) => {
            loadData();
            inputs.forEach(input => input.value = '');
            addButtonEl.disabled = false;
            editButtonEl.disabled = true;
        });
    }

    loadData();
}

document.addEventListener('DOMContentLoaded', init);