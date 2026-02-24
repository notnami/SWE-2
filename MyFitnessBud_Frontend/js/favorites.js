const favoritesList = document.getElementById("favoritesList");

// Load favorites from localStorage
let favorites = JSON.parse(localStorage.getItem("favorites")) || [];

function renderFavorites() {
    favoritesList.innerHTML = "";

    if (favorites.length === 0) {
        favoritesList.innerHTML = "<li>No favorites added yet.</li>";
        return;
    }

    favorites.forEach((snack, index) => {
        const li = document.createElement("li");
        li.textContent = snack;

        const removeBtn = document.createElement("button");
        removeBtn.textContent = "Remove";
        removeBtn.classList.add("remove-btn");

        removeBtn.addEventListener("click", () => {
            removeFavorite(index);
        });

        li.appendChild(removeBtn);
        favoritesList.appendChild(li);
    });
}

function removeFavorite(index) {
    favorites.splice(index, 1);
    localStorage.setItem("favorites", JSON.stringify(favorites));
    renderFavorites();
}

renderFavorites();