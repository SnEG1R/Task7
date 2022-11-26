let btnCreateGame = document.querySelector('.btn-create-game');
let btnJoinToGame = document.querySelector('.btn-join-game');
let inputCodeConnectionText = document.getElementById('code-connection-text');
let inputCodeConnection = document.getElementById('connection-code-input');

btnCreateGame.addEventListener('click', async () => {
    inputCodeConnectionText.value = await createGame();
});

btnJoinToGame.addEventListener('click', async () => {
    await joinToGame();
});

async function createGame() {
    let response = await fetch('Menu/CreateGame/', {
        method: 'post'
    });

    return await response.text();
}