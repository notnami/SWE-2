const searchInput = document.getElementById("searchInput");
const snackList = document.getElementById("snackList");
const addInput = document.getElementById("addInput");
const addBtn = document.getElementById("add-btn");

// Sample snack data (replace with API later)
const snacks = [
    "Apple",
    "Banana",
    "Granola Bar",
    "Trail Mix",
    "Yogurt",
    "Protein Bar",
    "Popcorn",
    "Peanut Butter Sandwich",
    "Energy Bar",
    "Chips"
];

// Load favorites
let favorites = JSON.parse(localStorage.getItem("favorites")) || [];

// Render list
function renderList(filter = "") {
    snackList.innerHTML = "";

    snacks
        .filter(snack => snack.toLowerCase().includes(filter.toLowerCase()))
        .forEach(snack => {
            const li = document.createElement("li");

            const name = document.createElement("span");
            name.textContent = snack;

            const star = document.createElement("span");
            star.innerHTML = favorites.includes(snack) ? "★" : "☆";
            star.classList.add("star");

            if (favorites.includes(snack)) {
                star.classList.add("fav");
            }

            star.addEventListener("click", () => toggleFavorite(snack));

            li.appendChild(name);
            li.appendChild(star);
            snackList.appendChild(li);
        });

    if (snackList.innerHTML === "") {
        snackList.innerHTML = "<li>No snacks found</li>";
    }
}

// Add snack to list
addBtn.addEventListener("click", () => {
    const newSnack = addInput.value.trim();

    if (newSnack === "") return;

    if (!snacks.includes(newSnack)) {
        snacks.push(newSnack);
    }

    addInput.value = "";
    renderList(searchInput.value);
});

// Toggle favorite
function toggleFavorite(snack) {
    if (favorites.includes(snack)) {
        favorites = favorites.filter(item => item !== snack);
    } else {
        favorites.push(snack);
    }

    localStorage.setItem("favorites", JSON.stringify(favorites));
    renderList(searchInput.value);
}

// Search listener
searchInput.addEventListener("input", () => {
    renderList(searchInput.value);
});

// Init
renderList();
