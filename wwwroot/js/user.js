let uri = '/User';
const token = localStorage.getItem("Token");
let users = [];

const isAdmin = () => {
    if(!token)
        window.location.href = '../index.html';
    uri = '/User/GetMyUser'
    fetch(uri, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    })
        .then(response =>
            { 
            if(response.status() === 401)
                throw new Error();
            else 
                return response.json();
        })
        .then(user => {
            console.log("in func");
            if (!user.isAdmin)
                window.location.href = '../index.html';
        })
        .catch(error => console.error('Unable to get User.', error));


}


function getUsers() {
    uri = '/User'
    fetch(uri, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    })
    .then(response =>
        { 
        if(response.status() === 401)
            throw new Error();
        else 
            return response.json();
    })
        .then(user => {
            console.log("in func");
            _displayItems(user);
        })
        .catch(error => console.error('Unable to get User.', error));
}


function addUser() {
    const addNameTextbox = document.getElementById('add-name');
    const addPasswordTextbox = document.getElementById('add-password');
    const addEmailTextbox = document.getElementById('add-email'); 

    const item = {
        id: 0,
        name: addNameTextbox.value.trim(),//this.userId,
        password: addPasswordTextbox.value.trim(),
        email: addEmailTextbox.value.trim(),
        isAdmin: false
    };

    uri = '/User'
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
            getUsers();
            addNameTextbox.value = '';
            addPasswordTextbox.value = '';
            addEmailTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    console.log(id);
    uri ='/User'
    fetch(`${uri}/${id}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
    })
        .then(() => getUsers())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = users.find(item => item.id === id);
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-password').value = item.password;
    document.getElementById('edit-email').value = item.email;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        name: document.getElementById('edit-name').value.trim(),
        password: document.getElementById('edit-password').value.trim(),
        email: document.getElementById('edit-email').value.trim(),
        isAdmin: false
    };
    console.log(item);
    uri = '/User'
    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(item)
    })
        .then(() => getUsers())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}




function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'user' : 'users';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    console.log(data);
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td2 = tr.insertCell(0);
        let textNode2 = document.createTextNode(item.id);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(1);
        let textNode3 = document.createTextNode(item.name);
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(2);
        let textNode4 = document.createTextNode(item.password);
        td4.appendChild(textNode4);

        let td5 = tr.insertCell(3);
        let textNode5 = document.createTextNode(item.email);
        td5.appendChild(textNode5);

        let td6 = tr.insertCell(4);
        td6.appendChild(editButton);

        let td7 = tr.insertCell(5);
        td7.appendChild(deleteButton);
    });

    users = data;
}