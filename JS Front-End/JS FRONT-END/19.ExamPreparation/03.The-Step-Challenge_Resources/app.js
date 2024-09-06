const baseUrl = `http://localhost:3030/jsonstore/records`;

const loadButton = document.getElementById('load-records');
const recordsList = document.getElementById('list');
const addButton = document.getElementById('add-record');
const editButton = document.getElementById('edit-record');
const formElement = document.getElementById('form');

const pNameInput = document.getElementById('p-name');
const stepsInput = document.getElementById('steps');
const caloriesInput = document.getElementById('calories');

loadButton.addEventListener('click', loadRecords);
addButton.addEventListener('click', addRecord);
editButton.addEventListener('click', editRecord);

async function editRecord() {
    const id = formElement.getAttribute('data-record-id');

    const name = pNameInput.value;
    const steps = stepsInput.value;
    const calories = caloriesInput.value;

    clearInputs();

    await fetch(`${baseUrl}/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name, steps, calories, _id: id }),
    });

    await loadRecords();

    editButton.setAttribute('disabled', 'disabled');

    addButton.removeAttribute('disabled');

    formElement.removeAttribute('data-record-id');
}

async function addRecord() {
    const name = pNameInput.value;
    const steps = stepsInput.value;
    const calories = caloriesInput.value;

    clearInputs();

    await fetch(baseUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ name, steps, calories }),
    });

    await loadRecords();
}

async function loadRecords() {
    recordsList.innerHTML = '';

    const response = await fetch(baseUrl);
    const result = await response.json();
    const records = Object.values(result);
    
    const recordsElements = records.map(record => createRecordElement(record.name, record.steps, record.calories, record._id));

    recordsList.append(...recordsElements);
}


function  createRecordElement(name, steps, calories, id) {
    const pNameEl = document.createElement('p');
    pNameEl.textContent = name;

    const pStepsEl = document.createElement('p');
    pStepsEl.textContent = steps;

    const pCaloriesEl = document.createElement('p');
    pCaloriesEl.textContent = calories;

    const divInfo = document.createElement('div');
    divInfo.classList.add('info');

    divInfo.appendChild(pNameEl);
    divInfo.appendChild(pStepsEl);
    divInfo.appendChild(pCaloriesEl);

    const changeButton = document.createElement('button');
    changeButton.classList.add('change-btn');
    changeButton.textContent = 'Change';
    changeButton.addEventListener('click', () => {
        
        pNameInput.value = name;
        stepsInput.value = steps;
        caloriesInput.value = calories;

        editButton.removeAttribute('disabled');

        addButton.setAttribute('disabled', 'disabled');

        formElement.setAttribute('data-record-id', id);
    });

    const deleteButton = document.createElement('button');
    deleteButton.classList.add('delete-btn');
    deleteButton.textContent = 'Delete';
    deleteButton.addEventListener('click', async () => {
        await fetch(`${baseUrl}/${id}`, {
            method: 'DELETE',
        });

        await loadRecords();
    });

    const divWrapper = document.createElement('div');
    divWrapper.classList.add('btn-wrapper');

    divWrapper.appendChild(changeButton);
    divWrapper.appendChild(deleteButton);

    const liElement = document.createElement('li');
    liElement.classList.add('record');

    liElement.appendChild(divInfo);
    liElement.appendChild(divWrapper);

    return liElement;
}

function clearInputs() {
    pNameInput.value = '';
    stepsInput.value = '';
    caloriesInput.value = '';
}