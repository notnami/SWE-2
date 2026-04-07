document.addEventListener("DOMContentLoaded", function () {
    const searchInput = document.getElementById("searchInput");
    const snackList = document.getElementById("snackList");
    const searchBtn = document.querySelector(".search-btn");

    const API_BASE = "http://localhost:5161";
    const loggedInUser = localStorage.getItem("loggedInUser");

    let snacks = [];
    let favoriteCodes = [];

    async function loadFavorites() {
        if (!loggedInUser) return;

        try {
            const response = await fetch(
                `${API_BASE}/api/favorites?loggedInUser=${encodeURIComponent(loggedInUser)}`
            );

            if (!response.ok) throw new Error("Could not load favorites");

            const data = await response.json();
            favoriteCodes = data.map(item => item.productCode);
        } catch (error) {
            console.error("Error loading favorites:", error);
        }
    }

    function renderList(items = []) {
        snackList.innerHTML = "";

        if (!items.length) {
            snackList.innerHTML = "<li>No snacks found.</li>";
            return;
        }

        items.forEach(snack => {
            const li = document.createElement("li");

            const name = document.createElement("span");
            name.textContent = snack.name;

            const star = document.createElement("span");
            const isFav = favoriteCodes.includes(snack.productCode);

            star.innerHTML = isFav ? "★" : "☆";
            star.classList.add("star");
            if (isFav) star.classList.add("fav");

            star.addEventListener("click", async () => {
                await toggleFavorite(snack);
            });

            li.appendChild(name);
            li.appendChild(star);
            snackList.appendChild(li);
        });
    }

    async function searchSnacks(query) {
        if (!query.trim()) {
            snacks = [];
            renderList([]);
            return;
        }

        try {
            const response = await fetch(
                `${API_BASE}/api/off/search?q=${encodeURIComponent(query)}`
            );

            if (!response.ok) throw new Error("Search failed");

            const data = await response.json();

            snacks = data.map(item => ({
                productCode: item.productCode,
                name: item.name,
                imageUrl: item.imageUrl,
                allergens: item.allergens,
                ingredientsAvailable: item.ingredientsAvailable
            }));

            renderList(snacks);
        } catch (error) {
            console.error("Error searching snacks:", error);
            snackList.innerHTML = "<li>Could not load snack results.</li>";
        }
    }

    async function toggleFavorite(snack) {
        if (!loggedInUser) {
            alert("Please log in first.");
            return;
        }

        const isFav = favoriteCodes.includes(snack.productCode);

        try {
            if (isFav) {
                const response = await fetch(
                    `${API_BASE}/api/favorites/${encodeURIComponent(snack.productCode)}?loggedInUser=${encodeURIComponent(loggedInUser)}`,
                    { method: "DELETE" }
                );

                if (!response.ok) throw new Error("Remove failed");

                favoriteCodes = favoriteCodes.filter(code => code !== snack.productCode);
            } else {
                const response = await fetch(`${API_BASE}/api/favorites`, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        loggedInUser,
                        productCode: snack.productCode,
                        name: snack.name,
                        imageUrl: snack.imageUrl,
                        allergens: snack.allergens,
                        ingredientsAvailable: snack.ingredientsAvailable
                    })
                });

                if (!response.ok) throw new Error("Save failed");

                favoriteCodes.push(snack.productCode);
            }

            renderList(snacks);
        } catch (error) {
            console.error("Favorite toggle error:", error);
            alert("Could not update favorite.");
        }
    }

    searchBtn.addEventListener("click", () => {
        searchSnacks(searchInput.value);
    });

    searchInput.addEventListener("keypress", (e) => {
        if (e.key === "Enter") {
            searchSnacks(searchInput.value);
        }
    });

    loadFavorites();
});