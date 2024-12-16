function solve() {
    const baseUrl = `http://localhost:3030/jsonstore/bus/schedule/`;

    const infoBox = document.querySelector('.info');
    const departButton = document.querySelector('#depart');
    const arriveButton = document.querySelector('#arrive');

    let currentStop = { id: 'depot' };

    async function depart() {
        try {
            const response = await fetch(baseUrl + currentStop.id);

            if (!response.ok) {
                throw new Error('Failed to fetch bus ID schedule');
            }

            const data = await response.json();

            currentStop.name = data.name;
            currentStop.next = data.next;

            infoBox.textContent = `Next stop ${currentStop.name}`;

            departButton.disabled = true;
            arriveButton.disabled = false;
        } catch (error) { handleError() }
    }

    async function arrive() {
        try {
            infoBox.textContent = `Arriving at ${currentStop.name}`;
            currentStop.id = currentStop.next;
            departButton.disabled = false;
            arriveButton.disabled = true;
        } catch (error) { handleError() }
    }

    function handleError() {
        infoBox.textContent = 'Error';
        departButton.disabled = true;
        arriveButton.disabled = true;
    }

    return {
        depart,
        arrive
    };
}

let result = solve();