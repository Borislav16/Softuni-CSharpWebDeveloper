const baseUrl = `http://localhost:3030/jsonstore/games`;

const loadButton = document.getElementById('load-games');
const gamesList = document.getElementById('games-list');
const editButton = document.getElementById('edit-game');
const addButton = document.getElementById('add-game');
const formElement = document.querySelector('#form form');

const gNameInput = document.getElementById('g-name');
const typeInput = document.getElementById('type');
const playersInput = document.getElementById('players');

loadButton.addEventListener('click', loadGames);
addButton.addEventListener('click', addGame);
editButton.addEventListener('click', editGame);

async function addGame() {
    const name = gNameInput.value;
    const type = typeInput.value;
    const players = playersInput.value;

    clearInputs();
    
    await fetch(baseUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({name, type, players})
    });

    await loadGames();

}

async function editGame() {
    const gameId = formElement.getAttribute('data-game-id');

    const name = gNameInput.value;
    const type = typeInput.value;
    const players = playersInput.value;

    clearInputs();

    await fetch(`${baseUrl}/${gameId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({name, type, players, _id: gameId})
    });

    await loadGames();

    editButton.setAttribute('disabled', 'disabled');

    addButton.removeAttribute('disabled');

    formElement.removeAttribute('data-game-id');
}

async function loadGames() {
    gamesList.innerHTML = '';

    const response = await fetch(baseUrl);
    const result = await response.json();
    const games = Object.values(result);

    const gameElements = games.map(game => createGameElement(game.name, game.type, game.players, game._id));

    gamesList.append(...gameElements);
} 

function createGameElement(name, type, players, id) {
    const nameElement = document.createElement('p');
    nameElement.textContent = name;

    const typeElement = document.createElement('p');
    typeElement.textContent = type;

    console.log(typeElement.textContent);
    const playersElement = document.createElement('p');
    playersElement.textContent = players;

    const divContentEl = document.createElement('div');
    divContentEl.classList.add('content');

    divContentEl.appendChild(nameElement);
    divContentEl.appendChild(playersElement);
    divContentEl.appendChild(typeElement);


    const changeButton = document.createElement('button');
    changeButton.classList.add('change-btn');
    changeButton.textContent = 'Change'; 
    changeButton.addEventListener('click', () => {
        gNameInput.value = name;
        typeInput.value = type;
        playersInput.value = players;

        editButton.removeAttribute('disabled');

        addButton.setAttribute('disabled', 'disabled');

        formElement.setAttribute('data-game-id', id)
    });

    const deleteButton = document.createElement('button');
    deleteButton.classList.add('delete-btn');
    deleteButton.textContent = 'Delete';
    deleteButton.addEventListener('click', async () => {
        await fetch(`${baseUrl}/${id}`, {
            method: 'DELETE',
        });

        await loadGames();
    });

    const buttonContainer = document.createElement('div');
    buttonContainer.classList.add('buttons-container');

    buttonContainer.appendChild(changeButton);
    buttonContainer.appendChild(deleteButton);

    const boardGame = document.createElement('div');
    boardGame.classList.add('board-game');

    boardGame.appendChild(divContentEl);
    boardGame.appendChild(buttonContainer);

    return boardGame;
}

function clearInputs() {
    gNameInput.value = '';
    typeInput.value = '';
    playersInput.value = '';
}