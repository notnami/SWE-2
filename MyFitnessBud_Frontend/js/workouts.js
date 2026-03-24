const workoutName = document.getElementById("workout-name");
const durationInput = document.getElementById("duration");
const addBtn = document.getElementById("addWorkoutBtn");
const workoutList = document.getElementById("workoutList");
const totalCaloriesDisplay = document.getElementById("totalCalories");

// Load workouts//
let workouts = JSON.parse(localStorage.getItem("workouts")) || [];
// Calorie formula //
function calculateCalories(duration) {
    return duration * 5; // 5 kcal per minute (simple estimate)
}
// Render workouts
function renderWorkouts() {
    workoutList.innerHTML = "";
    let totalCalories = 0;

    workouts.forEach((workout, index) => {
        const li = document.createElement("li");

        const text = document.createElement("span");
        text.textContent = `${workout.name} - ${workout.duration} min`;

        const calories = calculateCalories(workout.duration);
        totalCalories += calories;

        const deleteBtn = document.createElement("button");
        deleteBtn.textContent = "X";
        deleteBtn.classList.add("delete-btn");

        deleteBtn.addEventListener("click", () => deleteWorkout(index));

        li.appendChild(text);
        li.appendChild(deleteBtn);
        workoutList.appendChild(li);
    });

    totalCaloriesDisplay.textContent = totalCalories + " kcal";
}

// Add workout
addBtn.addEventListener("click", () => {
    const name = workoutName.value.trim();
    const duration = parseInt(durationInput.value);

    if (name === "" || isNaN(duration)) return;

    workouts.push({ name, duration });

    localStorage.setItem("workouts", JSON.stringify(workouts));

    workoutName.value = "";
    durationInput.value = "";

    renderWorkouts();
});

// Delete workout
function deleteWorkout(index) {
    workouts.splice(index, 1);
    localStorage.setItem("workouts", JSON.stringify(workouts));
    renderWorkouts();
}

// Init
renderWorkouts();