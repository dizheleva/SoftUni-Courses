function lockedProfile() {
    const BASE_URL = 'http://localhost:3030/jsonstore/advanced/profiles/';
    const main = document.getElementById('main');
    main.innerHTML = '';


    fetch(BASE_URL)
        .then((response) => response.json())
        .then((profiles) => {
            Object.values(profiles).forEach((profile, index) => {
                const profileDiv = document.createElement('div');
                profileDiv.classList.add('profile');

                profileDiv.innerHTML = `
                    <img src="./iconProfile2.png" class="userIcon" />
                    <label>Lock</label>
                    <input type="radio" name="user${index + 1}Locked" value="lock" checked>
                    <label>Unlock</label>
                    <input type="radio" name="user${index + 1}Locked" value="unlock">
                    <br>
                    <hr>
                    <label>Username</label>
                    <input type="text" name="user${index + 1}Username" value="${profile.username}" disabled readonly />
                    <div class="hidden-fields" style="display: none;">
                        <hr>
                        <label>Email:</label>
                        <input type="email" name="user${index + 1}Email" value="${profile.email}" disabled readonly />
                        <label>Age:</label>
                        <input type="number" name="user${index + 1}Age" value="${profile.age}" disabled readonly />
                    </div>
                    <button>Show more</button>
                `;


                const button = profileDiv.querySelector('button');
                button.addEventListener('click', () => {
                    const isLocked = profileDiv.querySelector(`input[name="user${index + 1}Locked"]:checked`).value === 'lock';
                    const hiddenFields = profileDiv.querySelector('.hidden-fields');

                    if (!isLocked) {
                        if (hiddenFields.style.display === 'none') {
                            hiddenFields.style.display = 'block';
                            button.textContent = 'Hide it';
                        } else {
                            hiddenFields.style.display = 'none';
                            button.textContent = 'Show more';
                        }
                    }
                });

                main.appendChild(profileDiv);
            });
        })
        .catch((error) => console.error('Error fetching profiles:', error));
}