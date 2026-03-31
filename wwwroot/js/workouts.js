const workoutName = document.getElementById("workout-name");
const durationInput = document.getElementById("duration");
const addBtn = document.getElementsByClassName("addWorkoutBtn")[0];
const workoutList = document.getElementById("workoutList");
const totalCaloriesDisplay = document.getElementById("totalCalories");

// Load user weight (ASSUMING saved during signup)
let userWeight = localStorage.getItem("weight");

// fallback if not found
if (!userWeight) {
    userWeight = 60; // default weight in kg
}

// Load workouts
let workouts = JSON.parse(localStorage.getItem("workouts")) || [];

// MET calculation
function calculateCalories(met, durationMinutes) {
    const hours = durationMinutes / 60;
    return Math.round(met * userWeight * hours);
}

// Render
function renderWorkouts() {
    workoutList.innerHTML = "";
    let totalCalories = 0;

    workouts.forEach((workout, index) => {

        const li = document.createElement("li");

        const calories = calculateCalories(workout.met, workout.duration);
        totalCalories += calories;

        const text = document.createElement("span");
        text.textContent = `${workout.name} - ${workout.duration} min = ${calories} kcal`;

        const deleteBtn = document.createElement("button");
        deleteBtn.textContent = "X";
        deleteBtn.classList.add("delete-btn");

        deleteBtn.addEventListener("click", () => {
            deleteWorkout(index);
        });

        li.appendChild(text);
        li.appendChild(deleteBtn);
        workoutList.appendChild(li);
    });

    totalCaloriesDisplay.textContent = totalCalories + " kcal";
}

// Add workout
addBtn.addEventListener("click", () => {

    const selectedOption = workoutName.options[workoutName.selectedIndex];

    const name = selectedOption.text;
    const met = parseFloat(selectedOption.value);
    const duration = parseInt(durationInput.value);

    if (isNaN(duration) || duration <= 0) return;

    workouts.push({ name, met, duration });

    localStorage.setItem("workouts", JSON.stringify(workouts));

    durationInput.value = "";

    renderWorkouts();
});

// Delete
function deleteWorkout(index) {
    workouts.splice(index, 1);
    localStorage.setItem("workouts", JSON.stringify(workouts));
    renderWorkouts();
}

// Init
renderWorkouts();