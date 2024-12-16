function loadCommits() {
    const username = document.querySelector('#username').value;
	const repository = document.querySelector('#repo').value;
    const commitsList = document.querySelector('#commits');

    commitsList.innerHTML = '';
	
	fetch(`https://api.github.com/repos/${username}/${repository}/commits`)
		.then((response) => response.json())
		.then((commits) => {
			commits.forEach(({ commit }) => {
				const item = document.createElement('li');
				item.textContent = `${commit.author.name}: ${commit.message}`;
				commitsList.appendChild(item);
			});
		})
		.catch((error) => {
			const item = document.createElement('li');
			item.textContent = `Error: ${error.status} (${error.message})`;

			commitsList.appendChild(item);
		});
}