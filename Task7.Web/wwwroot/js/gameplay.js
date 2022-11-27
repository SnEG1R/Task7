let cross = `<svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-x-lg" viewBox="0 0 16 16">
                <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8 2.146 2.854Z"/>
            </svg>`;
let zero = `<svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-circle" viewBox="0 0 16 16">
                <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/>
            </svg>`;

let gameInfo = {
    playerChip: '',
    playingField: [],
    isMove: false
};

async function move(element) {
    console.log(gameInfo)
    
    if (gameInfo.playerChip === 'x' && gameInfo.isMove)
        element.innerHTML = cross;
    else if (gameInfo.playerChip === 'o' && gameInfo.isMove)
        element.innerHTML = zero;
    else
        return;

    gameInfo.isMove = false;
    await hubConnection.invoke("PlayerTurn", connectionId, +element.id);
}

function WriteNewField() {
    let cells = document.querySelectorAll('.cell');

    cells.forEach((cell, i) => {
        if (gameInfo.playingField[i] === 'x')
            cell.innerHTML = cross;
        else if (gameInfo.playingField[i] === 'o')
            cell.innerHTML = zero;
    });
}