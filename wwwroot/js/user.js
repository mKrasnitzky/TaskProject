const uri = '/User';
const isAdmin = () => {

    const token = localStorage.getItem("Token");

    function getUsers(token) {
        fetch(uri, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            })
            .then(response => response.json())
            .then(user => {
                console.log("in func");
                if (!user.isAdmin)
                    window.location.href = '../index.html';
            })
            .catch(error => console.error('Unable to get User.', error));
    }

    getUsers(token);

}