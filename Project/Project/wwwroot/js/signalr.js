function showToast(message) {
  const toastBody = document.querySelector("#liveToast .toast-body");
  toastBody.textContent = message;
  const toast = document.getElementById('liveToast');

  toast.classList.add('show');

  setTimeout(() => {
    hideToast();
  }, 10000);
}

function hideToast() {
  const toast = document.getElementById('liveToast');
  toast.classList.remove('show');
}

const connection = new signalR.HubConnectionBuilder()
  .withUrl("/toasthub")
  .build();

connection.on("ReceiveMessage", function (message) {
  showToast(message);
});

connection.start().catch(function (err) {
  return console.error(err.toString());
});