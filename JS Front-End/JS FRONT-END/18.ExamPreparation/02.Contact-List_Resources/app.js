window.addEventListener("load", solve);

function solve() {
    const button = document.getElementById('add-btn');
    const checkList = document.getElementById('check-list');
    const contactList = document.getElementById('contact-list');

    const nameInput = document.getElementById('name');
    const phoneInput = document.getElementById('phone');
    const categoryInput = document.getElementById('category');

    button.addEventListener('click', () => {

        const name = nameInput.value;
        const phone = phoneInput.value;
        const category = categoryInput.value;

        const liElement = createCheckListElements(name, phone, category);

        checkList.appendChild(liElement);

        clearInputs();  
    })

    function clearInputs() {
        nameInput.value = '';
        phoneInput.value = '';
        categoryInput.value = '';
    }

    function createCheckListElements(name, phoneNumber, category) {
        const pNameEl = document.createElement('p');
        pNameEl.textContent = `name:${name}`;

        const pPhoneEl = document.createElement('p');
        pPhoneEl.textContent = `phone:${phoneNumber}`;

        const pCategoryEl = document.createElement('p');
        pCategoryEl.textContent = `category:${category}`;

        const articleEl = document.createElement('article');
        articleEl.appendChild(pNameEl);
        articleEl.appendChild(pPhoneEl);
        articleEl.appendChild(pCategoryEl);

        const editButton = document.createElement('button');
        editButton.classList.add('edit-btn');

        const saveButton = document.createElement('button');
        saveButton.classList.add('save-btn'); 

        const divElement = document.createElement('div');
        divElement.classList.add('buttons');
        divElement.appendChild(editButton);
        divElement.appendChild(saveButton);

        const liElement = document.createElement('li');
        liElement.appendChild(articleEl);
        liElement.appendChild(divElement);

        editButton.addEventListener('click', () => {
            nameInput.value = name;
            phoneInput.value = phoneNumber;
            categoryInput.value = category;

            liElement.remove();
        });

        saveButton.addEventListener('click', () => { 
            divElement.remove();

            const deleteButton = document.createElement('button');
            deleteButton.classList.add('del-btn');

            deleteButton.addEventListener('click', () => {
                liElement.remove();
            });
            
            contactList.appendChild(liElement);
            liElement.appendChild(deleteButton);
        })

        return liElement;
    }
}
  