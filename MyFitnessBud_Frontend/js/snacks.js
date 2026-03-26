const searchInput = document.getElementById("searchInput");
const snackList = document.getElementById("snackList");
const addInput = document.getElementById("food-name");
const addBtn = document.getElementById("add-btn");


// Example: const API_BASE = "https://localhost:5001";
const API_BASE = "http://localhost:5161";

let snacks = [];
let favorites = JSON.parse(localStorage.getItem("favorites")) || [];

function renderList(items = []) {
    snackList.innerHTML = "";

    if (!items.length) {
        snackList.innerHTML = "<li>No snacks found</li>";
        return;
    }

    items.forEach(snack => {
        const li = document.createElement("li");

        const left = document.createElement("div");
        left.style.display = "flex";
        left.style.alignItems = "center";
        left.style.gap = "10px";

        if (snack.imageUrl) {
            const img = document.createElement("img");
            img.src = snack.imageUrl;
            img.alt = snack.name;
            img.width = 50;
            img.height = 50;
            img.style.objectFit = "cover";
            img.style.borderRadius = "6px";
            left.appendChild(img);
        }

        const nameWrap = document.createElement("div");

        const name = document.createElement("div");
        name.textContent = snack.name;
        name.style.fontWeight = "600";

        const meta = document.createElement("small");
        meta.textContent =
            `Code: ${snack.productCode}` +
            (snack.ingredientsAvailable ? " • Ingredients available" : "");

        nameWrap.appendChild(name);
        nameWrap.appendChild(meta);
        left.appendChild(nameWrap);

        const star = document.createElement("span");
        star.innerHTML = favorites.includes(snack.productCode) ? "★" : "☆";
        star.classList.add("star");

        if (favorites.includes(snack.productCode)) {
            star.classList.add("fav");
        }

        star.addEventListener("click", () => toggleFavorite(snack.productCode));

        li.style.display = "flex";
        li.style.justifyContent = "space-between";
        li.style.alignItems = "center";

        li.appendChild(left);
        li.appendChild(star);
        snackList.appendChild(li);
    });
}

function toggleFavorite(productCode) {
    if (favorites.includes(productCode)) {
        favorites = favorites.filter(item => item !== productCode);
    } else {
        favorites.push(productCode);
    }

    localStorage.setItem("favorites", JSON.stringify(favorites));
    renderList(snacks);
}

async function searchSnacks(query) {
    if (!query.trim()) {
        snacks = [];
        renderList([]);
        return;
    }

    try {
        const response = await fetch(`${API_BASE}/api/off/search?q=${encodeURIComponent(query)}`);
        if (!response.ok) {
            throw new Error("Search failed");
        }

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

searchInput.addEventListener("keypress", (e) => {
    if (e.key === "Enter") {
        searchSnacks(searchInput.value);
    }
});

// Optional manual add for local-only custom entries
addBtn.addEventListener("click", () => {
    const newSnack = addInput.value.trim();
    if (!newSnack) return;

    const customSnack = {
        productCode: `custom-${Date.now()}`,
        name: newSnack,
        imageUrl: "",
        allergens: "",
        ingredientsAvailable: false
    };

    snacks.unshift(customSnack);
    addInput.value = "";
    renderList(snacks);
});

addInput.addEventListener("keypress", (e) => {
    if (e.key === "Enter") {
        addBtn.click();
    }
});

renderList([]);

