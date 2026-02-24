// Get elements
const foodInput = document.getElementById("food-name");
const calorieInput = document.getElementById("calories");
const addBtn = document.getElementById("add-btn");
const entryList = document.getElementById("entry-list");
const totalCaloriesEl = document.getElementById("total-calories");

// Load saved data
let entries = JSON.parse(localStorage.getItem("calorieEntries")) || [];
let totalCalories = 0;

// Initialize page
function init() {
    entryList.innerHTML = "";
    totalCalories = 0;

    entries.forEach(entry => {
        addEntryToList(entry.food, entry.calories);
        totalCalories += entry.calories;
    });

    totalCaloriesEl.textContent = totalCalories;
}

// Add new entry
addBtn.addEventListener("click", () => {
    const food = foodInput.value.trim();
    const calories = parseInt(calorieInput.value);

    if (food === "" || isNaN(calories) || calories <= 0) {
        alert("Please enter a valid food name and calorie amount.");
        return;
    }

    const entry = { food, calories };
    entries.push(entry);

    localStorage.setItem("calorieEntries", JSON.stringify(entries));

    addEntryToList(food, calories);

    totalCalories += calories;
    totalCaloriesEl.textContent = totalCalories;

    foodInput.value = "";
    calorieInput.value = "";
});

// Helper function
function addEntryToList(food, calories) {
    const li = document.createElement("li");
    li.textContent = `${food} - ${calories} cal`;
    entryList.appendChild(li);
}

// Initialize on load
init();
