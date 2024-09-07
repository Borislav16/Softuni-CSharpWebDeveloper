window.addEventListener("load", solve);

function solve() {
    const addButton = document.getElementById('add-btn');
    const previewList = document.getElementById('preview-list');
    const archiveList = document.getElementById('archive-list');

    const nameInput = document.getElementById('name');
    const timeInput = document.getElementById('time');
    const descriptionTextArea = document.getElementById('description');

    addButton.addEventListener('click', (event) => {
        event.preventDefault();
      
        if (
          nameInput.value == "" ||
          timeInput.value == "" ||
          descriptionTextArea.value == ""
        ) {
          return;
        }

        const name = nameInput.value;
        const time = timeInput.value;
        const description = descriptionTextArea.value;

        const li = createLiElement(name, time, description);

        previewList.appendChild(li);

        clearInputs();

        addButton.setAttribute('disabled', 'disabled');
    });

    function createLiElement(name, time, description) {
        const pNameEl = document.createElement('p');
        pNameEl.textContent = `${name}`;

        const pTimeEl = document.createElement('p');
        pTimeEl.textContent = `${time}`;

        const pDescriptionEl = document.createElement('p');
        pDescriptionEl.textContent = `${description}`;

        const articleEl = document.createElement('article');
        articleEl.appendChild(pNameEl);
        articleEl.appendChild(pTimeEl);
        articleEl.appendChild(pDescriptionEl);

        const editButton = document.createElement('button');
        editButton.classList.add('edit-btn');
        editButton.textContent = 'Edit';
        
        
        const nextButton = document.createElement('button');
        nextButton.classList.add('next-btn');
        nextButton.textContent = 'Next';

        const divEl = document.createElement('div');
        divEl.classList.add('buttons');
        divEl.appendChild(editButton);
        divEl.appendChild(nextButton);

        const liElement = document.createElement('li');
        liElement.appendChild(articleEl);
        liElement.appendChild(divEl);

        editButton.addEventListener('click', () => {
            nameInput.value = name;
            timeInput.value = time;
            descriptionTextArea.value = description;

            liElement.remove(); 
            addButton.removeAttribute('disabled');
        });

        nextButton.addEventListener('click', () => {
            liElement.removeChild(divEl);

            const archiveButton = document.createElement('button');
            archiveButton.textContent = 'Archive';
            archiveButton.classList.add('archive-btn');

            liElement.appendChild(archiveButton);
            archiveList.appendChild(liElement);

            archiveButton.addEventListener('click', () => {
                liElement.remove();
            });

            addButton.removeAttribute('disabled');
        });
        return liElement;
    }

    function clearInputs() {
      nameInput.value = '';
      timeInput.value = '';
      descriptionTextArea.value = '';
    }
}

// function solve() {
//   const addButton = document.getElementById('add-btn');
//   const previewList = document.getElementById('preview-list');
//   const archiveList = document.getElementById('archive-list');

//   const nameInput = document.getElementById('name');
//   const timeInput = document.getElementById('time');
//   const descriptionTextArea = document.getElementById('description');

//   addButton.addEventListener('click', (event) => {
//       event.preventDefault();

//       if (
//         nameInput.value == "" ||
//         timeInput.value == "" ||
//         descriptionTextArea.value == ""
//       ) {
//         return;
//       }

//       const name = nameInput.value;
//       const time = timeInput.value;
//       const description = descriptionTextArea.value;

//       const li = createLiElement(name, time, description);

//       previewList.appendChild(li);

//       clearInputs();
//       addButton.setAttribute('disabled', 'disabled');
//   });

//   function createLiElement(name, time, description) {
//       const pNameEl = document.createElement('p');
//       pNameEl.textContent = `${name}`;

//       const pTimeEl = document.createElement('p');
//       pTimeEl.textContent = `${time}`;

//       const pDescriptionEl = document.createElement('p');
//       pDescriptionEl.textContent = `${description}`;

//       const articleEl = document.createElement('article');
//       articleEl.appendChild(pNameEl);
//       articleEl.appendChild(pTimeEl);
//       articleEl.appendChild(pDescriptionEl);

//       const editButton = document.createElement('button');
//       editButton.classList.add('edit-btn');
//       editButton.textContent = 'Edit';
      
//       const nextButton = document.createElement('button');
//       nextButton.classList.add('next-btn');
//       nextButton.textContent = 'Next';

//       const divEl = document.createElement('div');
//       divEl.classList.add('buttons');
//       divEl.appendChild(editButton);
//       divEl.appendChild(nextButton);

//       const liElement = document.createElement('li');
//       liElement.appendChild(articleEl);
//       liElement.appendChild(divEl);

//       editButton.addEventListener('click', () => {
//           nameInput.value = name;
//           timeInput.value = time;
//           descriptionTextArea.value = description;

//           liElement.remove();
//           addButton.removeAttribute('disabled');
//       });

//       nextButton.addEventListener('click', () => {
//           liElement.removeChild(divEl);

//           const archiveButton = document.createElement('button');
//           archiveButton.textContent = 'Archive';
//           archiveButton.classList.add('archive-btn');

//           liElement.appendChild(archiveButton);
//           archiveList.appendChild(liElement);

//           archiveButton.addEventListener('click', () => {
//               liElement.remove();
//           });

//           addButton.removeAttribute('disabled');
//       });

//       return liElement;
//   }

//   function clearInputs() {
//       nameInput.value = '';
//       timeInput.value = '';
//       descriptionTextArea.value = '';
//   }
// }