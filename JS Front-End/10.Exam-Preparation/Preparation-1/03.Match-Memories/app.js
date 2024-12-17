//API Code:
//  Load / Read resources

//  Create single resource
//  Read single resource
//  Update single resource
//  Delete single resource

//DOM Code:

//  Load Data
//  Create Entry
//  Delete Entry
//  Update Data = delete entry + create entry

//  Event Handlers:

//      getData => GET request
//      createHandler => POST request
//      editHandler => go into edit mode
//      updateHandler => submit PUT request
//      deleteHandler => => DELETE request


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

    const baseUrl = 'http://localhost:3030/jsonstore/matches/';

    const inputs = Array.from(document.querySelectorAll('#host, #score, #guest'));
    const listEl = document.querySelector('#list');

    //Load Data
    const loadButton = document.querySelector('#load-matches');
    loadButton.addEventListener('click', loadData);

    function loadData(e) {
        listEl.innerHTML = '';
        getData(baseUrl, (result) => {
            Object.values(result).forEach(createMatch);
        });
        editButtonEl.disabled = true;
    }

    //Create data entry

    const addButtonEl = document.querySelector('#add-match');
    addButtonEl.addEventListener('click', createHandler);

    function createHandler(e) {
        e.preventDefault();

        const [host, score, guest] = inputs.map(input => input.value);

        if (!host || !score || !guest) return;

        const entry = { host, score, guest };

        createDataEntry(baseUrl, entry, (result) => {
            createMatch(result);
        });

        inputs.forEach(input => input.value = '');
    }

    function createMatch({ host, score, guest, _id }) {
        const entryEl = createElement('li', { className: 'match', id: _id, dataset: { host, score, guest, _id } }, listEl);
        const infoDiv = createElement('div', { className: 'info' }, entryEl);
        createElement('p', { textContent: host }, infoDiv);
        createElement('p', { textContent: score }, infoDiv);
        createElement('p', { textContent: guest }, infoDiv);
        const buttonsDiv = createElement('div', { className: 'btn-wrapper' }, entryEl);
        createElement('button', { className: 'change-btn', textContent: 'Change', onclick: changeHandler }, buttonsDiv);
        createElement('button', { className: 'delete-btn', textContent: 'Delete', onclick: deleteHandler }, buttonsDiv);
    }

    //Edit data entry    

    function changeHandler(e) {
        const entryEl = e.target.closest('li');
        entryEl.classList.add('active');

        //without using dataset
        const values = Array.from(entryEl.querySelectorAll('p')).map(p => p.textContent);
        //const values = Object.values(entryEl.dataset);
        inputs.forEach((input, index) => input.value = values[index]);

        editButtonEl.disabled = false;
        addButtonEl.disabled = true;
    }

    //Delete data entry     

    function deleteHandler(e) {
        const entryEl = e.target.closest('li');

        //without using dataset
        const _id = entryEl.id;
        const [host, score, guest] = Array.from(entryEl.querySelectorAll('p')).map(p => p.textContent);
        const match = { host, score, guest, _id };
        //const match = Object.assign({}, entryEl.dataset);

        deleteDataEntry(baseUrl, match, (result) => {
            deleteMatch(result);
        });
    }

    function deleteMatch({ host, score, guest, _id }) {
        listEl.querySelector(`li[data-_id="${_id}]`).remove();
    }

    //Update data

    const editButtonEl = document.querySelector('#edit-match');
    editButtonEl.addEventListener('click', updateHandler);

    function updateHandler(e) {
        e.preventDefault();

        const [host, score, guest] = inputs.map(field => field.value);

        if (!host || !score || !guest) return;

        const entryEl = document.querySelector('li.active');

        const entry = { host, score, guest, _id: entryEl.id };

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