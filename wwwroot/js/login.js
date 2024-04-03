const saveInLocalStorage = (token) => {

    localStorage.setItem("Token", token);

}

const form = document.getElementById('formLogin');

form.onsubmit = (event) => {

    console.log("in the func");
    const loginUrl = '/Login'
    event.preventDefault();
    const name = document.getElementById('name').value;
    const password = document.getElementById('password').value;
    console.log();

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