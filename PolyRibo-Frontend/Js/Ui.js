/* Small shared UI helpers used by the auth pages and the application pages. */

const PolyRiboUI = (() => {
  function refreshIcons() {
    if (window.lucide) {
      window.lucide.createIcons();
    }
  }

  function refreshApiLabels() {
    if (!window.PolyRiboApi) return;

    document.querySelectorAll("[data-api-base-url]").forEach((node) => {
      node.textContent = window.PolyRiboApi.getBaseUrl();
    });
  }

  function setStatus(element, state, message, link) {
    if (!element) return;

    element.replaceChildren();

    const text = document.createElement("span");
    text.textContent = message;
    element.append(text);

    if (link) {
      const anchor = document.createElement("a");
      anchor.className = "text-link";
      anchor.href = link.href;
      anchor.textContent = link.label;
      element.append(" ", anchor);
    }

    element.dataset.state = state;
    element.hidden = false;
  }

  function clearStatus(element) {
    if (!element) return;

    element.textContent = "";
    element.hidden = true;
    delete element.dataset.state;
  }

  function setBusy(form, isBusy, button, busyLabel) {
    if (!form || !button) return;

    const label = button.querySelector("[data-submit-label]") || button.querySelector("span");

    if (isBusy) {
      button.dataset.idleLabel = label ? label.textContent : button.textContent.trim();
    }

    form.dataset.submitting = String(isBusy);
    form.setAttribute("aria-busy", String(isBusy));
    form.querySelectorAll("input, button, select, textarea").forEach((control) => {
      control.disabled = isBusy;
    });

    if (label) {
      label.textContent = isBusy ? busyLabel : button.dataset.idleLabel || label.textContent;
    }
  }

  function showToast(message, state = "info") {
    let region = document.getElementById("toastRegion");

    if (!region) {
      region = document.createElement("div");
      region.id = "toastRegion";
      region.className = "toast-region";
      region.setAttribute("aria-live", "polite");
      document.body.append(region);
    }

    const toast = document.createElement("p");
    toast.className = `toast toast--${state}`;
    toast.textContent = message;
    region.append(toast);

    window.setTimeout(() => toast.remove(), 4200);
  }

  return {
    refreshIcons,
    refreshApiLabels,
    setStatus,
    clearStatus,
    setBusy,
    showToast,
  };
})();
