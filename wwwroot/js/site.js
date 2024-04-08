let uri = '/Task';
let userId = 0;
let name = '';
const token = localStorage.getItem("Token");
const userName = document.getElementById("userName");
const userHTML = document.getElementById("user");
let tasks = [];

const haveToken = () => {

    if (!token) {
        window.location.href = '../html/login.html';
    }

}

const toGoUsers = (user) => {

    if (!user.isAdmin)
        document.getElementById('usersToAdmin').style.display = 'none';

}

function isAdmin() {

    uri = '/User/GetMyUser'
    fetch(uri, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(user => toGoUsers(user))
        .catch(error => console.error('Unable to get items.', error));
}

isAdmin();
const getId = (user) => {
    console.log(user.id);
    this.name = user.name;
    this.userId = user.id;

}

function updateMyUser() {

    uri = '/User/GetMyUser';
    fetch(uri, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(user => console.log(user))
        .catch(error => console.error('Unable to get items.', error));
}

updateMyUser();
console.log(userId);

function getNameAndId() {

    haveToken();
    uri = '/User/GetMyUser';
    return fetch(uri, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(user => getId(user))
        .catch(error => console.error('Unable to get items.', error));
}

getNameAndId();
userName.innerHTML = this.name;

userName.onclick = () => {
    seeUser();
}

const writeUser = (myUser) => {

    userHTML.innerHTML = `
        <div id="user">
            <div id="edit-name">name: <span id="span-name">${myUser.name}</span></div>
            <div id="edit-email">email: <span id="span-email">${myUser.email}</span></div>
            <div id="edit-password">password: <span id="span-password">${myUser.password}</span></div>
            <div id="edit-id">id: <span id="span-id">${myUser.id}</span></div>
            <button id="edit-user' type="button" onclick="editName()">edit</button><br><br>
            <h4 id="userName"></h4>
        </div>
        `;
}

const ok = () => {

    user = {
        name: document.getElementById('input-name').value,
        email: document.getElementById('input-email').value,
        password: document.getElementById('input-password').value,
        id: document.getElementById('span-id').innerHTML
    }

    writeUser(user);

    uri = '/User';
    return fetch(uri, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(user)
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to get items.', error));


}

const editName = () => {

    const edit_name = document.getElementById('span-name').innerHTML;
    const edit_email = document.getElementById('span-email').innerHTML;
    const edit_password = document.getElementById('span-password').innerHTML;

    userHTML.innerHTML = `
        <div id="user">
            <input id="input-name" type="text" value="${edit_name}">
            <div id="edit-name" style="display: none;">name: <span id="span-name">${document.getElementById('input-name')}</span></div>
            <input id="input-email" type="email" value="${edit_email}">
            <div id="edit-email" style="display: none;">email: <span id="span-email">${document.getElementById('input-email')}</span></div>
            <input id="input-password" type="text" value="${edit_password}">
            <div id="edit-password" style="display: none;">password: <span id="span-password">${document.getElementById('input-password')}</span></div>
            <div id="input-id">id: <span id="span-id">${document.getElementById('span-id').innerHTML}</span></div>
            <button id="edit-user' type="button" onclick="ok()">edit</button>
            <button id="edit-user' type="button" style="display: none;" onclick="editName()">edit</button><br><br><br><br>
            <h4 id="userName"></h4>
        </div>
    `
}

haveToken();


const seeUser = () => {
    uri = '/User/GetMyUser';
    fetch(uri, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(user => writeUser(user))
        .catch(error => console.error('Unable to get items.', error));
}



function getItems() {

    uri = '/Task';
    fetch(uri, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');

    const item = {
        id: 7,
        userId: 0,
        profession: "string",
        isDone: false,
        description: addNameTextbox.value.trim()
    };

    uri = '/Task';
    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    console.log(id);
    fetch(`${uri}/${id}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = tasks.find(item => item.id === id);

    document.getElementById('edit-name').value = item.description;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-isDone').checked = item.isDone;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    getNameAndId();
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        userId: this.userId,
        isDone: document.getElementById('edit-isDone').checked,
        description: document.getElementById('edit-name').value.trim(),
        profession: ""
    };

    uri = '/Task';
    fetch(`${uri}/${itemId}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(item)
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'task' : 'tasks';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {

    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isDoneCheckbox = document.createElement('input');
        isDoneCheckbox.type = 'checkbox';
        isDoneCheckbox.disabled = true;
        isDoneCheckbox.checked = item.isDone;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isDoneCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.description);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasks = data;
}