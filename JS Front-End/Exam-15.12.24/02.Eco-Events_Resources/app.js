window.addEventListener("load", solve);

function solve() {
    const listPreviewEl = document.querySelector('#preview-list');
    const eventListEl = document.querySelector('#event-list');

    const emailField = document.querySelector('#email');
    const eventField = document.querySelector('#event');
    const locationField = document.querySelector('#location');
    const btnNextEl = document.querySelector('#next-btn');

    btnNextEl.addEventListener('click', (e) => {
        e.preventDefault();

        const email = emailField.value;
        const event = eventField.value;
        const location = locationField.value;

        if (email == '' || event == '' || location == '') return;

        const li = document.createElement('li');
        li.className = 'application';

        const article = document.createElement('article');

        const h4 = document.createElement('h4');
        h4.textContent = email;
        article.appendChild(h4);

        // Event Paragraph
        const eventP = document.createElement("p");
        const strongEvent = document.createElement("strong");
        strongEvent.textContent = "Event:";
        eventP.appendChild(strongEvent);
        eventP.appendChild(document.createElement("br"));
        eventP.appendChild(document.createTextNode(event));
 
        // Location Paragraph
        const locationP = document.createElement("p");
        const strongLocation = document.createElement("strong");
        strongLocation.textContent = "Location:";
        locationP.appendChild(strongLocation);
        locationP.appendChild(document.createElement("br"));
        locationP.appendChild(document.createTextNode(location));
               
        article.appendChild(eventP);        
        article.appendChild(locationP);
        li.appendChild(article);

        // const html = `
        // <article>
        //   <h4>${email}</h4>
        //   <p>
        //     <strong>Event:</strong>
        //     <br>
        //     "${event}"
        //   </p>
        //   <p>
        //     <strong>Location:</strong>
        //     <br>
        //     "${location}"
        //   </p>
        // </article>    
        // `;

        // li.innerHTML = html;

        const editBtn = document.createElement('button');
        editBtn.classList = 'action-btn edit';
        editBtn.textContent = 'edit';
        editBtn.addEventListener('click', editHandler);
        li.appendChild(editBtn);

        const applyBtn = document.createElement('button');
        applyBtn.classList = 'action-btn apply';
        applyBtn.textContent = 'apply';
        applyBtn.addEventListener('click', applyHandler);
        li.appendChild(applyBtn);

        listPreviewEl.appendChild(li);
        e.target.disabled = true;
        emailField.value = '';
        eventField.value = '';
        locationField.value = '';
    });

    function editHandler(e) {
        const email = document.querySelector('article h4').textContent;

        const eventText = document.querySelectorAll('article p')[0].textContent;
        const event = eventText.substring('Event:'.length).trim();

        const locationText = document.querySelectorAll('article p')[1].textContent;
        const location = locationText.substring('Location:'.length).trim();

        li = e.target.closest('li');
        li.remove();

        emailField.value = email;
        eventField.value = event;
        locationField.value = location;

        btnNextEl.disabled = false;
    }

    function applyHandler(e) {
        const li = e.target.closest('li');
        listPreviewEl.remove(li);
        const buttons = li.querySelectorAll('button');
        for (var i = 0; i < buttons.length; i++) {
            buttons[i].remove();
        }

        eventListEl.appendChild(li);
        btnNextEl.disabled = false;
    }
}
