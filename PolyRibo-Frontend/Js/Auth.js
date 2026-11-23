/* PolyRibo authentication forms.
   Sends requests to the existing ASP.NET Core API with the Fetch API.
   No tokens, sessions, or passwords are stored in the browser. */

const USERNAME_PATTERN = /^[A-Za-z0-9_]{3,20}$/;
const EMAIL_PATTERN = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;
const MIN_PASSWORD_LENGTH = 8;

document.addEventListener("DOMContentLoaded", () => {
  if (window.PolyRiboUI) {
    window.PolyRiboUI.refreshIcons();
    window.PolyRiboUI.refreshApiLabels();
  }

  initPasswordToggles();
  initInfoLinks();

  const loginForm = document.getElementById("loginForm");
  if (loginForm) initLoginForm(loginForm);

  const registerForm = document.getElementById("registerForm");
  if (registerForm) initRegisterForm(registerForm);
});

/* --------------------------------------------------------------------------
   Form helpers
   -------------------------------------------------------------------------- */

function getFieldContainer(input) {
  return input.closest(".field, .check-field");
}

function showError(input, message) {
  const container = getFieldContainer(input);
  if (!container) return;

  const error = container.querySelector(".field-error");
  container.classList.add("has-error");
  input.setAttribute("aria-invalid", "true");

  if (error) {
    error.textContent = message;
    error.hidden = false;
  }
}

function clearError(input) {
  const container = getFieldContainer(input);
  if (!container) return;

  const error = container.querySelector(".field-error");
  container.classList.remove("has-error");
  input.removeAttribute("aria-invalid");

  if (error) {
    error.textContent = "";
    error.hidden = true;
  }
}

function trimInputs(form, names) {
  names.forEach((name) => {
    const input = form.elements[name];
    if (input) input.value = input.value.trim();
  });
}

function bindValidation(form, fields, status, onValid) {
  let hasSubmitted = false;

  fields.forEach(({ input, validate }) => {
    const revalidate = () => {
      const message = validate(input);
      if (message) {
        showError(input, message);
      } else {
        clearError(input);
      }
    };

    input.addEventListener("blur", () => {
      if (input.type === "checkbox") return;
      if (!input.value && !hasSubmitted) return;
      revalidate();
    });

    input.addEventListener("input", () => {
      if (input.getAttribute("aria-invalid") === "true") revalidate();
    });

    if (input.type === "checkbox") {
      input.addEventListener("change", revalidate);
    }
  });

  form.addEventListener("submit", (event) => {
    event.preventDefault();

    if (form.dataset.submitting === "true") return;

    hasSubmitted = true;

    let firstInvalid = null;

    fields.forEach(({ input, validate }) => {
      const message = validate(input);
      if (message) {
        showError(input, message);
        if (!firstInvalid) firstInvalid = input;
      } else {
        clearError(input);
      }
    });

    if (firstInvalid) {
      window.PolyRiboUI.setStatus(status, "error", "Fix the highlighted fields and try again.");
      firstInvalid.focus();
      return;
    }

    onValid(form, status);
  });
}

function getApiErrorMessage(error, fallback) {
  if (error instanceof window.PolyRiboApi.ApiError) {
    return error.message;
  }

  return error?.message || fallback;
}

/* --------------------------------------------------------------------------
   Validation rules
   -------------------------------------------------------------------------- */

function validateLoginUsername(input) {
  return input.value.trim() ? "" : "Enter your username.";
}

function validateLoginPassword(input) {
  if (!input.value) return "Enter your password.";
  if (input.value.length < MIN_PASSWORD_LENGTH) {
    return `Password must be at least ${MIN_PASSWORD_LENGTH} characters.`;
  }
  return "";
}

function validateFullName(input) {
  const value = input.value.trim();
  if (!value) return "Enter your full name.";
  if (value.length < 2) return "Use at least 2 characters.";
  return "";
}

function validateUsername(input) {
  const value = input.value.trim();
  if (!value) return "Choose a username.";
  if (!USERNAME_PATTERN.test(value)) return "Use 3–20 letters, numbers, or underscores.";
  return "";
}

function validateEmail(input) {
  const value = input.value.trim();
  if (!value) return "Enter your email address.";
  if (!EMAIL_PATTERN.test(value)) return "Enter a valid email address.";
  return "";
}

function validateCountryId(input) {
  const value = input.value.trim();
  if (!value) return "Enter the numeric country ID required by the API.";

  const countryId = Number(value);
  if (!Number.isInteger(countryId) || countryId < 1) {
    return "Use a positive whole number.";
  }

  return "";
}

function validateNewPassword(input) {
  if (!input.value) return "Create a password.";
  if (input.value.length < MIN_PASSWORD_LENGTH) {
    return `Use at least ${MIN_PASSWORD_LENGTH} characters.`;
  }
  return "";
}

function validateConfirmPassword(input, form) {
  if (!input.value) return "Confirm your password.";
  if (input.value !== form.elements.password.value) return "Passwords don’t match.";
  return "";
}

function validateTerms(input) {
  return input.checked ? "" : "Please accept the terms to continue.";
}

/* --------------------------------------------------------------------------
   Page setup
   -------------------------------------------------------------------------- */

function initLoginForm(form) {
  const status = form.querySelector("[data-form-status]");
  const button = form.querySelector("[data-submit-button]");
  const fields = [
    { input: form.elements.username, validate: validateLoginUsername },
    { input: form.elements.password, validate: validateLoginPassword },
  ];

  bindValidation(form, fields, status, async () => {
    if (!window.PolyRiboApi) {
      window.PolyRiboUI.setStatus(status, "error", "The API client did not load. Refresh the page.");
      return;
    }

    trimInputs(form, ["username"]);

    const userName = form.elements.username.value;
    const password = form.elements.password.value;

    window.PolyRiboUI.clearStatus(status);
    window.PolyRiboUI.setBusy(form, true, button, "Logging in…");

    try {
      const response = await window.PolyRiboApi.login(userName, password);
      const accountName = response?.data?.userName;
      const message = accountName
        ? `Login successful. Welcome back, ${accountName}.`
        : "Login successful.";

      form.reset();
      window.PolyRiboUI.setStatus(status, "success", message, {
        href: "Dashboard.html",
        label: "Continue to dashboard →",
      });
    } catch (error) {
      window.PolyRiboUI.setStatus(
        status,
        "error",
        getApiErrorMessage(error, "Login failed. Please try again.")
      );
    } finally {
      window.PolyRiboUI.setBusy(form, false, button, "Logging in…");
    }
  });
}

function initRegisterForm(form) {
  const status = form.querySelector("[data-form-status]");
  const button = form.querySelector("[data-submit-button]");
  const password = form.elements.password;
  const confirmPassword = form.elements.confirmPassword;

  const fields = [
    { input: form.elements.fullName, validate: validateFullName },
    { input: form.elements.username, validate: validateUsername },
    { input: form.elements.email, validate: validateEmail },
    { input: form.elements.countryId, validate: validateCountryId },
    { input: password, validate: validateNewPassword },
    { input: confirmPassword, validate: (input) => validateConfirmPassword(input, form) },
    { input: form.elements.terms, validate: validateTerms },
  ];

  bindValidation(form, fields, status, async () => {
    if (!window.PolyRiboApi) {
      window.PolyRiboUI.setStatus(status, "error", "The API client did not load. Refresh the page.");
      return;
    }

    trimInputs(form, ["fullName", "username", "email", "countryId"]);

    // Full name, confirm password, and terms stay in the browser.
    // The API request contains only properties from RegisterRequest.
    const request = {
      userName: form.elements.username.value,
      email: form.elements.email.value,
      password: form.elements.password.value,
      countryId: Number(form.elements.countryId.value),
    };

    window.PolyRiboUI.clearStatus(status);
    window.PolyRiboUI.setBusy(form, true, button, "Creating account…");

    try {
      const response = await window.PolyRiboApi.register(request);
      const userName = response?.data?.userName || request.userName;

      form.reset();
      window.PolyRiboUI.setStatus(status, "success", `Account created for ${userName}. You can log in now.`, {
        href: "Login.html",
        label: "Go to login →",
      });
    } catch (error) {
      window.PolyRiboUI.setStatus(
        status,
        "error",
        getApiErrorMessage(error, "Registration failed. Please try again.")
      );
    } finally {
      window.PolyRiboUI.setBusy(form, false, button, "Creating account…");
    }
  });

  // Keep the confirmation error in sync when the password changes.
  password.addEventListener("input", () => {
    if (!confirmPassword.value) return;

    const message = validateConfirmPassword(confirmPassword, form);
    if (message) {
      showError(confirmPassword, message);
    } else {
      clearError(confirmPassword);
    }
  });
}

function initPasswordToggles() {
  document.querySelectorAll("[data-password-toggle]").forEach((button) => {
    const input = document.getElementById(button.dataset.passwordToggle);
    if (!input) return;

    button.addEventListener("click", () => {
      const isVisible = input.type === "text";
      input.type = isVisible ? "password" : "text";
      button.setAttribute("aria-pressed", String(!isVisible));
      button.setAttribute("aria-label", isVisible ? "Show password" : "Hide password");
    });
  });
}

function initInfoLinks() {
  const messages = {
    forgot: "Password recovery is not available in the backend yet.",
    terms: "Terms and Privacy pages are not available yet.",
  };

  document.querySelectorAll("[data-demo-link]").forEach((link) => {
    link.addEventListener("click", (event) => {
      event.preventDefault();
      const status = document.querySelector("[data-form-status]");
      window.PolyRiboUI.setStatus(
        status,
        "info",
        messages[link.dataset.demoLink] || "This feature is not available yet."
      );
    });
  });
}
