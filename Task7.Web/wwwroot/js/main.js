let connectionId = window.location.search.split('=').slice(-1)[0];

window.onload = async function () {
    await hubConnection.start();
    await playerHub.start();
    await playerHub.invoke("GetPlayerName");
    await hubConnection.invoke("Connect", connectionId);

    showTurningPlayer();
}