const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/game-hub")
    .build();

hubConnection.on("RemoveLoader", function () {
    let loader = document.querySelector('.loader');
    console.log("www")
    loader.remove();
});