function attachEvents() {
    const locationUrl = 'http://localhost:3030/jsonstore/forecaster/locations';
    const currentConditionsUrl = 'http://localhost:3030/jsonstore/forecaster/today/';
    const threeDaysUrl = 'http://localhost:3030/jsonstore/forecaster/upcoming/';

    const forecast = document.querySelector('#forecast');
    const submitButton = document.querySelector('#submit');

    const forecastSymbols = {
        'Sunny': '☀', // ☀ &#x2600;
        'PartlySunny': '&#x26C5;', // ⛅ &#x26C5;
        'Overcast': '☁', // ☁ &#x2601; 
        'Rain': '☂', // ☂ &#x2614; 
        degrees: '°', // ° &#176;
    }

    submitButton.addEventListener('click', async () => {
        const locationName = document.querySelector('#location').value;
        forecast.querySelector('#current').innerHTML = '';
        forecast.querySelector('#upcoming').innerHTML = '';
        document.querySelector('#location').value = '';

        try {
            const locationsResponse = await fetch(locationUrl);

            if (!locationsResponse.ok) {
                throw new Error('Failed to fetch locations');
            }

            const locations = await locationsResponse.json();
            const location = locations.find(l => l.name.toLowerCase() === locationName.toLowerCase());

            if (!location) {
                throw new Error('Location not found');
            }

            const code = location.code;

            const [currentWeatherResponse, upcomingWeatherResponse] = await Promise.all([
                fetch(currentConditionsUrl + location.code),
                fetch(threeDaysUrl + location.code)
            ]);

            if (!currentWeatherResponse.ok || !upcomingWeatherResponse.ok) {
                throw new Error('Failed to fetch weather data');
            }

            const currentWeather = await currentWeatherResponse.json();
            const upcomingWeather = await upcomingWeatherResponse.json();
            
            const symbolSpan = document.createElement('span');
            symbolSpan.classList.add('condition', 'symbol');
            symbolSpan.textContent = forecastSymbols[currentWeather.forecast.condition];
            
            const dataSpan = document.createElement('span');
            dataSpan.classList.add('condition');

            const locationSpan = document.createElement('span');
            locationSpan.classList.add('forecast-data');
            locationSpan.textContent = currentWeather.name;
            dataSpan.appendChild(locationSpan);
            
            const temperatureSpan = document.createElement('span');
            temperatureSpan.classList.add('forecast-data');
            temperatureSpan.textContent = `${currentWeather.forecast.low}${forecastSymbols.degrees}/${currentWeather.forecast.high}${forecastSymbols.degrees}`;
            dataSpan.appendChild(temperatureSpan);
            
            const conditionSpan = document.createElement('span');
            conditionSpan.classList.add('forecast-data');
            conditionSpan.textContent = currentWeather.forecast.condition;
            dataSpan.appendChild(conditionSpan);
         
            const currentDivEl = document.createElement('div');
            currentDivEl.classList.add('forecasts');
            currentDivEl.appendChild(symbolSpan);
            currentDivEl.appendChild(dataSpan);

            forecast.querySelector('#current').appendChild(currentDivEl);

            const divEl = document.createElement('div');
            divEl.classList.add('forecast-info');

            upcomingWeather.forecast.forEach(day => {
                const upcomingSpan = document.createElement('span');
                upcomingSpan.classList.add('upcoming');

                const symbolSpan = document.createElement('span');
                symbolSpan.classList.add('symbol');
                symbolSpan.textContent = forecastSymbols[day.condition];
                upcomingSpan.appendChild(symbolSpan);

                const temperatureSpan = document.createElement('span');
                temperatureSpan.classList.add('forecast-data');
                temperatureSpan.textContent = `${day.low}${forecastSymbols.degrees}/${day.high}${forecastSymbols.degrees}`;
                upcomingSpan.appendChild(temperatureSpan);

                const conditionSpan = document.createElement('span');
                conditionSpan.classList.add('forecast-data');
                conditionSpan.textContent = day.condition;
                upcomingSpan.appendChild(conditionSpan);

                divEl.appendChild(upcomingSpan);
            });

            forecast.querySelector('#upcoming').appendChild(divEl);

            forecast.style.display = 'block';            
        } catch (error) {
            forecast.innerHTML = 'Error';
            forecast.style.display = 'block';
        }
    });
}

attachEvents();