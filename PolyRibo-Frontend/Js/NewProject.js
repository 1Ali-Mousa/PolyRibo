/* PolyRibo new-project setup preview.
   Validates the configuration in the browser only.
   No project data is sent because the backend has no project/generation endpoints yet. */

document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("projectWizard");
  if (!form) return;

  const steps = Array.from(form.querySelectorAll("[data-step]"));
  const indicators = Array.from(document.querySelectorAll("[data-step-indicator]"));
  const nextButton = document.getElementById("wizardNext");
  const backButton = document.getElementById("wizardBack");
  const resetButton = document.getElementById("wizardReset");
  const reviewList = document.getElementById("reviewList");
  const status = document.getElementById("wizardStatus");

  const sourceSelect = form.elements.databaseSource;
  const connectionDetails = form.elements.connectionDetails;
  const gitEnabled = form.elements.gitEnabled;
  const dockerEnabled = form.elements.dockerEnabled;

  let currentStep = 1;

  function updateSourceFields() {
    const isBackup = sourceSelect.value === "backup";

    form.querySelectorAll('[data-source-fields="backup"]').forEach((element) => {
      element.hidden = !isBackup;
    });

    form.querySelectorAll('[data-source-fields="connection"]').forEach((element) => {
      element.hidden = isBackup;
    });

    connectionDetails.required = !isBackup;
  }

  function updateGitFields() {
    form.querySelectorAll("[data-git-fields]").forEach((element) => {
      element.hidden = !gitEnabled.checked;
    });
  }

  function updateDockerFields() {
    form.querySelectorAll("[data-docker-fields]").forEach((element) => {
      element.hidden = !dockerEnabled.checked;
    });
  }

  function updateStep(shouldScroll = false) {
    steps.forEach((step, index) => {
      step.hidden = index !== currentStep - 1;
    });

    indicators.forEach((indicator, index) => {
      const stepNumber = index + 1;
      indicator.classList.toggle("is-active", stepNumber === currentStep);
      indicator.classList.toggle("is-complete", stepNumber < currentStep);

      if (stepNumber === currentStep) {
        indicator.setAttribute("aria-current", "step");
      } else {
        indicator.removeAttribute("aria-current");
      }
    });

    backButton.hidden = currentStep === 1;
    nextButton.hidden = currentStep === steps.length;

    if (currentStep === steps.length) {
      buildReview();
    }

    if (shouldScroll) {
      form.scrollIntoView({ behavior: "smooth", block: "start" });
    }

    if (window.PolyRiboUI) {
      window.PolyRiboUI.refreshIcons();
    }
  }

  function validateCurrentStep() {
    const step = steps[currentStep - 1];
    const fields = Array.from(step.querySelectorAll("input, select, textarea")).filter((field) => {
      return field.type !== "hidden" && !field.disabled && field.offsetParent !== null;
    });

    for (const field of fields) {
      if (!field.checkValidity()) {
        field.reportValidity();
        field.focus();
        window.PolyRiboUI.setStatus(
          status,
          "error",
          "Complete the highlighted fields before continuing."
        );
        return false;
      }
    }

    window.PolyRiboUI.clearStatus(status);
    return true;
  }

  function getValue(name) {
    const field = form.elements[name];
    return field ? field.value.trim() : "";
  }

  function getPackages() {
    return getValue("packages")
      .split(/[\n,]+/)
      .map((item) => item.trim())
      .filter(Boolean)
      .join(", ");
  }

  function addReviewRow(term, value) {
    const dt = document.createElement("dt");
    const dd = document.createElement("dd");

    dt.textContent = term;
    dd.textContent = value || "—";

    reviewList.append(dt, dd);
  }

  function buildReview() {
    reviewList.replaceChildren();

    const source = getValue("databaseSource");
    const sourceLabel = source === "backup" ? "Backup file" : "Connection details";
    const backupFile = form.elements.backupFile.files[0];
    const sourceDetail = source === "backup"
      ? backupFile?.name || "No file selected (not uploaded)"
      : "Connection details provided in the form (not sent)";

    const framework = [getValue("frameworkName"), getValue("frameworkVersion")]
      .filter(Boolean)
      .join(" ");

    const gitSummary = gitEnabled.checked
      ? `Enabled${getValue("gitRemoteUrl") ? ` · ${getValue("gitRemoteUrl")}` : ""}${getValue("gitBranch") ? ` · ${getValue("gitBranch")}` : ""}`
      : "Disabled";

    const dockerParts = [];
    if (form.elements.dockerFileEnabled.checked) dockerParts.push("Dockerfile");
    if (form.elements.composeEnabled.checked) dockerParts.push("Compose");
    const dockerSummary = dockerEnabled.checked
      ? `Enabled${dockerParts.length ? ` · ${dockerParts.join(" + ")}` : ""}`
      : "Disabled";

    addReviewRow("Project name", getValue("projectName"));
    addReviewRow("Description", getValue("description"));
    addReviewRow("Database source", sourceLabel);
    addReviewRow("Database name", getValue("databaseName"));
    addReviewRow("Source detail", sourceDetail);
    addReviewRow("Framework", framework);
    addReviewRow("Language", getValue("language"));
    addReviewRow("Architecture", getValue("architecture"));
    addReviewRow("Architecture notes", getValue("architectureNotes"));
    addReviewRow("Data access", getValue("dataAccess"));
    addReviewRow("Packages", getPackages());
    addReviewRow("Git", gitSummary);
    addReviewRow("Docker", dockerSummary);
  }

  function resetWizard() {
    form.reset();
    currentStep = 1;
    reviewList.replaceChildren();
    updateSourceFields();
    updateGitFields();
    updateDockerFields();
    window.PolyRiboUI.clearStatus(status);
    updateStep();
    window.PolyRiboUI.showToast("Setup preview reset.", "info");
  }

  nextButton.addEventListener("click", () => {
    if (!validateCurrentStep()) return;

    currentStep = Math.min(currentStep + 1, steps.length);
    updateStep(true);
  });

  backButton.addEventListener("click", () => {
    currentStep = Math.max(currentStep - 1, 1);
    window.PolyRiboUI.clearStatus(status);
    updateStep(true);
  });

  resetButton.addEventListener("click", resetWizard);

  sourceSelect.addEventListener("change", updateSourceFields);
  gitEnabled.addEventListener("change", updateGitFields);
  dockerEnabled.addEventListener("change", updateDockerFields);

  updateSourceFields();
  updateGitFields();
  updateDockerFields();
  updateStep();
});
