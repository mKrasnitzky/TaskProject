const form = document.getElementById('formLogin');
const loginUrl = '/Login'

const saveInLocalStorage = (token) => {

    localStorage.setItem("Token", token);

}

form.onsubmit = (event) => {

    event.preventDefault();
    const name = document.getElementById('name').value;
    const password = document.getElementById('password').value;

    const user = {
        id: 0,
        name: name,
        password: password,
        email: "",
        isAdmin: false,
    }

    fetch(loginUrl, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        })
        .then(response => response.json())
        .then((token) => {
            saveInLocalStorage(token);
            window.location.href = `../index.html`;
        })
        .catch(error => {
            alert("user not find!!")
            console.error('Unable to add item.', error)
        });
}

function decodeJwtResponse(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}


function handleCredentialResponse(response) {

    const responsePayload = decodeJwtResponse(response.credential);

    const user = {
        id: 0,
        name: responsePayload.name,
        password: responsePayload.sub,
        email: responsePayload.email,
        isAdmin: false,
    }

    console.log("ID: " + responsePayload.sub);
    console.log('Full Name: ' + responsePayload.name);
    console.log('Given Name: ' + responsePayload.given_name);
    console.log('Family Name: ' + responsePayload.family_name);
    console.log("Image URL: " + responsePayload.picture);
    console.log("Email: " + responsePayload.email);

    fetch(loginUrl, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        })
        .then(response => {
            if (response.status() === 401)
                throw new Error();
            else
                return response.json();
        })
        .then((token) => {
            console.log(token);
            saveInLocalStorage(token);
            window.location.href = `../index.html`;
        })
        .catch(error => {
            alert("user not find!!")
            console.error('Unable to add item.', error)
        });


}