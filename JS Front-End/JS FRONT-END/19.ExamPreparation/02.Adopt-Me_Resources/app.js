window.addEventListener("load", solve);

function solve() {
    const adoptButton = document.getElementById('adopt-btn');
    const adoptionInfo = document.getElementById('adoption-info');
    const adoptedList = document.getElementById('adopted-list');

    const typeInput = document.getElementById('type');
    const ageInput = document.getElementById('age');
    const genderInput = document.getElementById('gender');

    adoptButton.addEventListener('click', (event) => {
        event.preventDefault();

        if (
          typeInput.value == "" ||
          ageInput.value == "" ||
          genderInput.value == ""
        ) {
          return;
        }
  
        const type = typeInput.value;
        const age = ageInput.value;
        const gender = genderInput.value;

        const li = createLiElement(type, age, gender);

        adoptionInfo.appendChild(li);

        clearInputs();
    });

    function createLiElement(type, age, gender) {
        const pTypeEl = document.createElement('p');
        pTypeEl.textContent = `Pet:${type}`;

        const pGenderEl = document.createElement('p');
        pGenderEl.textContent = `Gender:${gender}`;

        const pAgeEl = document.createElement('p');
        pAgeEl.textContent = `Age:${age}`;

        const articleEl = document.createElement('article');
        articleEl.appendChild(pTypeEl);
        articleEl.appendChild(pGenderEl);
        articleEl.appendChild(pAgeEl);

        const editButton = document.createElement('button');
        editButton.classList.add('edit-btn');
        editButton.textContent = 'Edit';
        
        
        const doneButton = document.createElement('button');
        doneButton.classList.add('done-btn');
        doneButton.textContent = 'Done';
        
        const divEl = document.createElement('div');
        divEl.classList.add('buttons');
        divEl.appendChild(editButton);
        divEl.appendChild(doneButton);
        
        const liElement = document.createElement('li');
        liElement.appendChild(articleEl);
        liElement.appendChild(divEl);
        
        editButton.addEventListener('click', () => {
            typeInput.value = type;
            ageInput.value = age;
            genderInput.value = gender;

            liElement.remove(divEl); 
        })
        
        doneButton.addEventListener('click', () => {
            liElement.removeChild(divEl);

            const buttonClear = document.createElement('button');
            buttonClear.textContent = 'Clear';
            buttonClear.classList.add('clear-btn');

            liElement.appendChild(buttonClear);
            adoptedList.appendChild(liElement);

            buttonClear.addEventListener('click', () => {
                liElement.remove();
            });
        });

        return liElement;
    }

    function clearInputs() {
        typeInput.value = '';
        ageInput.value = '';
        genderInput.value = '';
    }
}
  