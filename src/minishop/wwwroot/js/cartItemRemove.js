function removeCartItem(id) {
  const item = document.querySelector(`#cartItem-${id}`)
  if (item) {
    item.remove()
  } else {
    $.notify("Can't remove item, please refresh the page", "error")
  }
}