function showMessage() {
  const messages = document.querySelector('#messages').textContent.trim();
  const messagesJson = JSON.parse(messages)
  const status = messagesJson.status
  const message = messagesJson.message
  $.notify(message, status);
}