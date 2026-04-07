document.addEventListener("DOMContentLoaded", function () {
    const loggedInUser = localStorage.getItem("loggedInUser");
    const favoritesList = document.getElementById("favoritesList");
    const API_BASE = "http://localhost:5161";

    async function loadFavorites() {
        favoritesList.innerHTML = "";

        if (!loggedInUser) {
            favoritesList.innerHTML = "<li>Please log in first.</li>";
            return;
        }

        try {
            const response = await fetch(
                `${API_BASE}/api/favorites?loggedInUser=${encodeURIComponent(loggedInUser)}`
            );

            if (!response.ok) throw new Error("Could not load favorites");

            const favorites = await response.json();

            if (!favorites.length) {
                favoritesList.innerHTML = "<li>No favorites added yet.</li>";
                return;
            }

            favorites.forEach(snack => {
                const li = document.createElement("li");

                const textSpan = document.createElement("span");
                textSpan.textContent = snack.name;
                li.appendChild(textSpan);

                const removeBtn = document.createElement("button");
                removeBtn.textContent = "Remove";
                removeBtn.classList.add("btn", "btn-outline-danger", "btn-sm", "remove-btn");

                removeBtn.addEventListener("click", async () => {
                    try {
                        const response = await fetch(
                            `${API_BASE}/api/favorites/${encodeURIComponent(snack.productCode)}?loggedInUser=${encodeURIComponent(loggedInUser)}`,
                            { method: "DELETE" }
                        );

                        if (!response.ok) throw new Error("Remove failed");
                        loadFavorites();
                    } catch (error) {
                        console.error(error);
                        alert("Could not remove favorite.");
                    }
                });

                li.appendChild(removeBtn);
                favoritesList.appendChild(li);
            });
        } catch (error) {
            console.error(error);
            favoritesList.innerHTML = "<li>Could not load favorites.</li>";
        }
    }

    loadFavorites();
});