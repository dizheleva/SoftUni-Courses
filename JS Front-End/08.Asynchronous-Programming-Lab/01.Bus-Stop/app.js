function getInfo() {
    const busId = document.querySelector('#stopId').value;
    const buses = document.querySelector('#buses');

    buses.innerHTML = '';

    fetch(`http://localhost:3030/jsonstore/bus/businfo/${busId}`) 
        .then((response) => response.json())
        .then((result) => {
            document.querySelector('#stopName').textContent = result.name;
            Object.entries(result.buses).map(([busId, time]) => {
                const item = document.createElement('li');
                item.textContent = `Bus ${busId} arrives in ${time} minutes`;
                buses.appendChild(item);
            }); 
        })
        .catch((error) => {
            document.querySelector('#stopName').textContent = 'Error';
        });
}