const saveInLocalStorage = (token) => {

    localStorage.setItem("Token", token);
    alert("התחברת!!!!")

}

const form = document.getElementById('formLogin');

form.onsubmit = (event) => {

    console.log("in the func");
    const loginUrl = 'https://localhost:7249/Login'
    event.preventDefault();
    const name = document.getElementById('name');
    const password = document.getElementById('passwprd');

    const user = {
        id: null,
        name: name,
        password: password,
        email: null,
        isAdmin: null,
    }

    fetch(loginUrl, {
            method: 'POST',
            headers: {
                'Accept': 'text/plain',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        })
        .then(response => response.json())
        .then(() => {
            saveInLocalStorage();
            Location.href = `index.html`;
        })
        .catch(error => {
            alert("user not find!!")
            console.error('Unable to add item.', error)
        });
}