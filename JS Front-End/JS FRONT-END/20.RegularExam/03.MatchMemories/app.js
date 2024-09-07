const baseUrl = `http://localhost:3030/jsonstore/matches`;

const loadButton = document.getElementById('load-matches');
const matchesList = document.getElementById('list');
const addButton = document.getElementById('add-match');
const editButton = document.getElementById('edit-match');
const formElement = document.querySelector('#form form');

const hostInput = document.getElementById('host');
const scoreInput = document.getElementById('score');
const guestInput = document.getElementById('guest');

loadButton.addEventListener('click', loadMatches);
addButton.addEventListener('click', addMatch);
editButton.addEventListener('click', editMatch);

async function editMatch() {
    const matchId = formElement.getAttribute('data-match-id');

    const host = hostInput.value;
    const score = scoreInput.value;
    const guest = guestInput.value;

    clearInputs();

    await fetch(`${baseUrl}/${matchId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({host, score, guest, _id: matchId})
    });

    await loadMatches();

    editButton.setAttribute('disabled', 'disabled');

    addButton.removeAttribute('disabled');

    formElement.removeAttribute('data-match-id');
}

async function addMatch() {
    const host = hostInput.value;
    const score = scoreInput.value;
    const guest = guestInput.value;

    clearInputs();
    
    await fetch(baseUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({host, score, guest})
    });

    await loadMatches();

}

async function loadMatches() {
    matchesList.innerHTML = '';

    const response = await fetch(baseUrl);
    const result = await response.json();
    const matches = Object.values(result);

    const matchElements = matches.map(match => createMatchElement(match.host, match.score, match.guest, match._id));

    matchesList.append(...matchElements);
} 

function createMatchElement(host, score, guest, id) {
    const hostElement = document.createElement('p');
    hostElement.textContent = host;

    const scoreElement = document.createElement('p');
    scoreElement.textContent = score;

    const guestElement = document.createElement('p');
    guestElement.textContent = guest;

    const divInfoEl = document.createElement('div');
    divInfoEl.classList.add('info');

    divInfoEl.appendChild(hostElement);
    divInfoEl.appendChild(guestElement);
    divInfoEl.appendChild(scoreElement);

    const changeButton = document.createElement('button');
    changeButton.classList.add('change-btn');
    changeButton.textContent = 'Change'; 
    changeButton.addEventListener('click', () => {
        hostInput.value = host;
        scoreInput.value = score;
        guestInput.value = guest;

        editButton.removeAttribute('disabled');

        addButton.setAttribute('disabled', 'disabled');

        formElement.setAttribute('data-match-id', id)
    });

    const deleteButton = document.createElement('button');
    deleteButton.classList.add('delete-btn');
    deleteButton.textContent = 'Delete';
    deleteButton.addEventListener('click', async () => {
        await fetch(`${baseUrl}/${id}`, {
            method: 'DELETE',
        });

        await loadMatches();
    });

    const buttonWrapper = document.createElement('div');
    buttonWrapper.classList.add('btn-wrapper');

    buttonWrapper.appendChild(changeButton);
    buttonWrapper.appendChild(deleteButton);

    const match = document.createElement('li');
    match.classList.add('match');

    match.appendChild(divInfoEl);
    match.appendChild(buttonWrapper);

    return match;
}

function clearInputs() {
    hostInput.value = '';
    scoreInput.value = '';
    guestInput.value = '';
}