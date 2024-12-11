function addItem() {
    let newElement = document.getElementById("newItemText").value;
    let list = document.getElementById("items");

    if (newElement.length === 0) return;

    let listItem = document.createElement("li");
    listItem.textContent = newElement;

    let removeButton = document.createElement("a");
    let linkText = document.createTextNode("[Delete]");

    removeButton.appendChild(linkText);
    removeButton.href = "#";
    removeButton.addEventListener("click", () => listItem.remove());

    listItem.appendChild(removeButton);
    list.appendChild(listItem);
}
