let connectionId = window.location.search.split('=').slice(-1)[0];
let url = location.href;

window.onload = async function () {
    await hubConnection.start();
    await playerHub.start();
    await playerHub.invoke("GetPlayerName");
    await hubConnection.invoke("SendAllGame");
    await hubConnection.invoke("Connect", connectionId);

    showTurningPlayer();

    history.pushState({page: 1}, "title 1", location.href);
}

window.onpopstate = async function () {
    history.pushState({page: 1}, "title 1", '/');
    location.reload();
    await hubConnection.invoke("LeaveGame", connectionId);
}

window.onclick = function () {
    if (gameInfo.gameStatus === 'Completed') {
        if (window.confirm("The second player left the game!")) {
            document.location.href = '/';
        } else {
            document.location.href = '/';
        }
    }
}