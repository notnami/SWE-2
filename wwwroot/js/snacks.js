document.addEventListener("DOMContentLoaded", function () {

    const searchInput = document.getElementById("searchInput");
    const snackList = document.getElementById("snackList");
    const addInput = document.getElementById("food-name");
    const addBtn = document.getElementsByClassName("add-btn");
    const searchBtn = document.getElementsByClassName("search-btn");

    const intakeList = document.getElementById("intakeList");
    const totalCaloriesDisplay = document.getElementById("totalCalories");

    // Snack list
    const snacks = [
        "Apple", "Banana", "Granola Bar", "Trail Mix",
        "Yogurt", "Protein Bar", "Popcorn", "Chips"
    ];

    // Calories database
    const calorieMap = {
        "apple": 95,
        "banana": 105,
        "granola bar": 150,
        "trail mix": 200,
        "yogurt": 120,
        "protein bar": 220,
        "popcorn": 100,
        "chips": 240
    };

    // Load data
    let favorites = JSON.parse(localStorage.getItem("favorites")) || [];
    let intake = JSON.parse(localStorage.getItem("intake")) || [];

    // Render snacks
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
    }

    // Favorites
    function toggleFavorite(snack) {
        if (favorites.includes(snack)) {
            favorites = favorites.filter(item => item !== snack);
        } else {
            favorites.push(snack);
        }

        localStorage.setItem("favorites", JSON.stringify(favorites));
        renderList(searchInput.value);
    }

    // Add intake
    addBtn.addEventListener("click", () => {
        const food = addInput.value.trim().toLowerCase();
        if (food === "") return;

        const calories = calorieMap[food] || 100;

        intake.push({ name: food, calories });
        localStorage.setItem("intake", JSON.stringify(intake));

        addInput.value = "";
        renderIntake();
    });

    // Search snacks
    searchBtn.addEventListener("click", () => {
        renderList(searchInput.value);
    });

    // Render intake
    function renderIntake() {
        intakeList.innerHTML = "";
        let total = 0;

        intake.forEach((item, index) => {
            const li = document.createElement("li");

            const text = document.createElement("span");
            text.textContent = `${item.name} = ${item.calories} cal`;

            total += item.calories;

            const deleteBtn = document.createElement("button");
            deleteBtn.textContent = "X";
            deleteBtn.classList.add("delete-btn");

            deleteBtn.addEventListener("click", () => {
                intake.splice(index, 1);
                localStorage.setItem("intake", JSON.stringify(intake));
                renderIntake();
            });

            li.appendChild(text);
            li.appendChild(deleteBtn);
            intakeList.appendChild(li);
        });

        totalCaloriesDisplay.textContent = total + " kcal";
    }

    // Search
    searchInput.addEventListener("input", () => {
        renderList(searchInput.value);
    });

    // Init
    renderList();
    renderIntake();
});