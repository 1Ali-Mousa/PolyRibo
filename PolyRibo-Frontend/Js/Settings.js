/* PolyRibo settings page.
   Stores only the API base URL in localStorage and can test browser reachability. */

document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("apiSettingsForm");
  if (!form) return;

  const input = form.elements.apiBaseUrl;
  const status = form.querySelector("[data-form-status]");
  const testButton = document.getElementById("testApiButton");
  const resetButton = document.getElementById("resetApiButton");
  const originLabel = document.querySelector("[data-current-origin]");

  input.value = window.PolyRiboApi.getBaseUrl();

  if (originLabel) {
    originLabel.textContent = window.location.origin === "null"
      ? "file:// (open the page with Live Server)"
      : window.location.origin;
  }

  function refreshLabels() {
    window.PolyRiboUI.refreshApiLabels();
  }

  form.addEventListener("submit", (event) => {
    event.preventDefault();

    try {
      const savedUrl = window.PolyRiboApi.setBaseUrl(input.value);
      input.value = savedUrl;
      refreshLabels();
      window.PolyRiboUI.setStatus(status, "success", `API base URL saved: ${savedUrl}`);
      window.PolyRiboUI.showToast("API base URL saved.", "success");
    } catch (error) {
      window.PolyRiboUI.setStatus(
        status,
        "error",
        error.message === "Invalid URL" ? "Enter a valid http:// or https:// URL." : error.message
      );
    }
  });

  resetButton.addEventListener("click", () => {
    input.value = window.PolyRiboApi.resetBaseUrl();
    refreshLabels();
    window.PolyRiboUI.setStatus(status, "info", "API base URL reset to the default.");
    window.PolyRiboUI.showToast("API base URL reset.", "info");
  });

  testButton.addEventListener("click", async () => {
    const baseUrl = window.PolyRiboApi.getBaseUrl();
    const controller = new AbortController();
    const timeout = window.setTimeout(() => controller.abort(), 8000);

    testButton.disabled = true;
    testButton.setAttribute("aria-busy", "true");
    window.PolyRiboUI.setStatus(status, "info", `Testing ${baseUrl}…`);

    try {
      // An empty login request is harmless and exercises the real CORS path.
      const response = await fetch(`${baseUrl}/api/Authentication/Login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ userName: "", password: "" }),
        signal: controller.signal,
      });

      window.PolyRiboUI.setStatus(
        status,
        "success",
        `API reached (HTTP ${response.status}). CORS allows this origin to read the response.`
      );
    } catch (error) {
      const reason = error.name === "AbortError"
        ? "The request timed out."
        : "The browser could not read the response. The API may be stopped, or CORS is blocking this origin.";

      window.PolyRiboUI.setStatus(
        status,
        "error",
        `${reason} Origin: ${window.location.origin}.`
      );
    } finally {
      window.clearTimeout(timeout);
      testButton.disabled = false;
      testButton.removeAttribute("aria-busy");
    }
  });
});
