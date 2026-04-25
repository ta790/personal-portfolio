const navToggle = document.getElementById("nav-toggle");
const navLinks = document.getElementById("nav-links");
const navItems = document.querySelectorAll(".nav-link");
const sections = document.querySelectorAll("main section");
const revealItems = document.querySelectorAll(".reveal");
const filterButtons = document.querySelectorAll(".filter-btn");
const projectCards = document.querySelectorAll(".project-card");
const themeToggle = document.getElementById("theme-toggle");
const contactForm = document.getElementById("contact-form");
const formStatus = document.getElementById("form-status");
const heroCanvas = document.getElementById("hero-canvas");

const savedTheme = localStorage.getItem("tsolution-theme");
if (savedTheme === "light") {
  document.body.classList.add("light-theme");
}

document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
  anchor.addEventListener("click", (event) => {
    const targetId = anchor.getAttribute("href");
    if (!targetId || targetId === "#") return;

    const target = document.querySelector(targetId);
    if (!target) return;

    event.preventDefault();
    target.scrollIntoView({ behavior: "smooth", block: "start" });

    navLinks.classList.remove("open");
    navToggle.setAttribute("aria-expanded", "false");
    document.body.classList.remove("menu-open");
  });
});

navToggle.addEventListener("click", () => {
  const isOpen = navLinks.classList.toggle("open");
  navToggle.setAttribute("aria-expanded", String(isOpen));
  document.body.classList.toggle("menu-open", isOpen);
});

navItems.forEach((item) => {
  item.addEventListener("click", () => {
    navLinks.classList.remove("open");
    navToggle.setAttribute("aria-expanded", "false");
    document.body.classList.remove("menu-open");
  });
});

const highlightActiveSection = () => {
  const scrollPosition = window.scrollY + 150;

  sections.forEach((section) => {
    const top = section.offsetTop;
    const height = section.offsetHeight;
    const id = section.getAttribute("id");

    if (scrollPosition >= top && scrollPosition < top + height) {
      navItems.forEach((link) => link.classList.remove("active"));
      const activeLink = document.querySelector(`.nav-link[href="#${id}"]`);
      if (activeLink) activeLink.classList.add("active");
    }
  });
};

const revealObserver = new IntersectionObserver(
  (entries) => {
    entries.forEach((entry) => {
      if (!entry.isIntersecting) return;

      entry.target.classList.add("visible");

      if (entry.target.classList.contains("skill-group")) {
        entry.target.querySelectorAll(".skill-progress").forEach((bar) => {
          bar.style.width = bar.dataset.width;
        });
      }

      revealObserver.unobserve(entry.target);
    });
  },
  { threshold: 0.18 }
);

revealItems.forEach((item) => revealObserver.observe(item));

filterButtons.forEach((button) => {
  button.addEventListener("click", () => {
    const filter = button.dataset.filter;

    filterButtons.forEach((btn) => btn.classList.remove("active"));
    button.classList.add("active");

    projectCards.forEach((card) => {
      const category = card.dataset.category || "";
      const shouldShow = filter === "all" || category.includes(filter);
      card.classList.toggle("hidden", !shouldShow);
    });
  });
});

themeToggle.addEventListener("click", () => {
  const isLight = document.body.classList.toggle("light-theme");
  localStorage.setItem("tsolution-theme", isLight ? "light" : "dark");
});

contactForm.addEventListener("submit", (event) => {
  event.preventDefault();

  const nameInput = document.getElementById("name");
  const emailInput = document.getElementById("email");
  const messageInput = document.getElementById("message");
  const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  let isValid = true;

  const setError = (input, message) => {
    const group = input.closest(".form-group");
    group.classList.add("error");
    group.querySelector(".error-message").textContent = message;
    isValid = false;
  };

  const clearError = (input) => {
    const group = input.closest(".form-group");
    group.classList.remove("error");
    group.querySelector(".error-message").textContent = "";
  };

  [nameInput, emailInput, messageInput].forEach(clearError);
  formStatus.textContent = "";

  if (nameInput.value.trim().length < 2) {
    setError(nameInput, "Please enter at least 2 characters.");
  }

  if (!emailPattern.test(emailInput.value.trim())) {
    setError(emailInput, "Please enter a valid email address.");
  }

  if (messageInput.value.trim().length < 10) {
    setError(messageInput, "Please write a message with at least 10 characters.");
  }

  if (!isValid) return;

  formStatus.textContent = "Thanks. Your message looks good and is ready to send.";
  contactForm.reset();
});

const setupHeroCanvas = () => {
  if (!heroCanvas) return;

  const context = heroCanvas.getContext("2d");
  if (!context) return;

  let width = 0;
  let height = 0;
  let particles = [];

  const createParticles = () => {
    const total = Math.max(24, Math.floor(width / 55));
    particles = Array.from({ length: total }, () => ({
      x: Math.random() * width,
      y: Math.random() * height,
      radius: Math.random() * 2.2 + 0.6,
      speedX: (Math.random() - 0.5) * 0.35,
      speedY: (Math.random() - 0.5) * 0.35,
      alpha: Math.random() * 0.55 + 0.15,
    }));
  };

  const resizeCanvas = () => {
    const section = heroCanvas.parentElement;
    width = section.clientWidth;
    height = section.clientHeight;
    heroCanvas.width = width;
    heroCanvas.height = height;
    createParticles();
  };

  const draw = () => {
    context.clearRect(0, 0, width, height);

    particles.forEach((particle, index) => {
      particle.x += particle.speedX;
      particle.y += particle.speedY;

      if (particle.x < 0 || particle.x > width) particle.speedX *= -1;
      if (particle.y < 0 || particle.y > height) particle.speedY *= -1;

      context.beginPath();
      context.fillStyle = `rgba(142, 200, 255, ${particle.alpha})`;
      context.arc(particle.x, particle.y, particle.radius, 0, Math.PI * 2);
      context.fill();

      for (let next = index + 1; next < particles.length; next += 1) {
        const target = particles[next];
        const dx = particle.x - target.x;
        const dy = particle.y - target.y;
        const distance = Math.sqrt(dx * dx + dy * dy);

        if (distance < 110) {
          context.beginPath();
          context.strokeStyle = `rgba(90, 160, 255, ${0.16 - distance / 900})`;
          context.lineWidth = 1;
          context.moveTo(particle.x, particle.y);
          context.lineTo(target.x, target.y);
          context.stroke();
        }
      }
    });

    window.requestAnimationFrame(draw);
  };

  resizeCanvas();
  draw();
  window.addEventListener("resize", resizeCanvas);
};

setupHeroCanvas();
window.addEventListener("scroll", highlightActiveSection);
window.addEventListener("load", highlightActiveSection);
