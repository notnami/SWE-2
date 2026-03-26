// Get elements
const consumedEl = document.getElementById("consumed");
const burnedEl = document.getElementById("burned");
const maintenanceEl = document.getElementById("maintenance");
const deficitEl = document.getElementById("deficit");

// Load data
let intake = JSON.parse(localStorage.getItem("intake")) || [];
let workouts = JSON.parse(localStorage.getItem("workouts")) || [];
let weight = localStorage.getItem("weight") || 60;

// ------------------
// CALORIES CONSUMED
// ------------------
let totalConsumed = 0;

intake.forEach(item => {
    totalConsumed += item.calories;
});

// ------------------
// CALORIES BURNED
// ------------------
function calculateCalories(met, duration) {
    const hours = duration / 60;
    return met * weight * hours;
}

let totalBurned = 0;

workouts.forEach(workout => {
    totalBurned += calculateCalories(workout.met, workout.duration);
});

// ------------------
// MAINTENANCE CALORIES (BMR estimate)
// ------------------
// Simple formula: weight * 22 (basic daily needs)
let maintenance = weight * 22;

// ------------------
// DEFICIT CALCULATION
// ------------------
let deficit = maintenance + totalBurned - totalConsumed;

// ------------------
// DISPLAY
// ------------------
consumedEl.textContent = Math.round(totalConsumed) + " kcal";
burnedEl.textContent = Math.round(totalBurned) + " kcal";
maintenanceEl.textContent = Math.round(maintenance) + " kcal";
deficitEl.textContent = Math.round(deficit) + " kcal";