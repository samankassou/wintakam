/* === toggle menu mobile === */
const menuMobile = document.getElementById("menu-mobile");
const toggleMenuBtn = document.getElementById("toggle-menu-btn");

toggleMenuBtn.addEventListener("click", (e) => {
  toggleMenuBtn.classList.toggle("active");
  menuMobile.classList.toggle("active");
});

/* === load particles js */
particlesJS.load("home", "assets/particles.json");
