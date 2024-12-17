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

    const baseUrl = 'http://localhost:3030/jsonstore/workout/';

    const inputs = Array.from(document.querySelectorAll('#workout, #location, #date'));
    const listEl = document.querySelector('#list');

    //Load Data
    const loadButton = document.querySelector('#load-workout');
    loadButton.addEventListener('click', loadData);

    function loadData(e) {
        listEl.innerHTML = '';
        getData(baseUrl, (result) => {
            Object.values(result).forEach(createMatch);
        });
        editButtonEl.disabled = true;
    }

    //Create data entry

    const addButtonEl = document.querySelector('#add-workout');
    addButtonEl.addEventListener('click', createHandler);

    function createHandler(e) {
        e.preventDefault();

        const [workout, location, date] = inputs.map(input => input.value);

        if (!workout || !location || !date) return;

        const entry = { workout: workout, location: location, date: date };

        createDataEntry(baseUrl, entry, (result) => {
            createMatch(result);
        });

        inputs.forEach(input => input.value = '');
    }

    function createMatch({ workout, location, date, _id }) {
        const entryEl = createElement('div', { className: 'container', id: _id, dataset: { workout, location, date, _id } }, listEl);
        createElement('h2', { textContent: workout }, entryEl);
        createElement('h3', { textContent: date }, entryEl);
        createElement('h3', { id: 'location', textContent: location }, entryEl);
        const buttonsDiv = createElement('div', { id: 'buttons-container' }, entryEl);
        createElement('button', { className: 'change-btn', textContent: 'Change', onclick: changeHandler }, buttonsDiv);
        createElement('button', { className: 'delete-btn', textContent: 'Done', onclick: deleteHandler }, buttonsDiv);
    }

    //Edit data entry    

    function changeHandler(e) {
        const entryEl = e.target.closest('div.container');
        entryEl.classList.add('active');

        inputs[0].value = entryEl.querySelector('h2').textContent;
        inputs[1].value = entryEl.querySelectorAll('h3')[1].textContent;
        inputs[2].value = entryEl.querySelectorAll('h3')[0].textContent;

        editButtonEl.disabled = false;
        addButtonEl.disabled = true;
    }

    //Delete data entry     

    function deleteHandler(e) {
        const entryEl = e.target.closest('div.container');

        const _id = entryEl.id;
        const [workout, location, date] = Array.from(entryEl.querySelectorAll('h2, h3')).map(h => h.textContent);
        const entry = { workout, location, date, _id };

        deleteDataEntry(baseUrl, entry, (result) => {
            deleteMatch(result);
        });
    }

    function deleteMatch({ workout, location, date, _id }) {
        listEl.querySelector(`div[data-_id="${_id}]`).remove();
    }

    //Update data

    const editButtonEl = document.querySelector('#edit-workout');
    editButtonEl.addEventListener('click', updateHandler);

    function updateHandler(e) {
        e.preventDefault();

        const [workout, location, date] = inputs.map(field => field.value);

        if (!workout || !location || !date) return;

        const entryEl = document.querySelector('div.active');

        const entry = { workout, location, date, _id: entryEl.id };

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