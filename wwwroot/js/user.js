const uri = '/User/GetMyUser';
const isAdmin = () => {

    const token = localStorage.getItem("Token");

    function getMyUser(token) {
        fetch(uri, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer${token}`
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

    getMyUser(token);

}