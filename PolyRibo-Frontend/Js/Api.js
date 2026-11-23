/* PolyRibo API client.
   Uses the existing ASP.NET Core endpoints and plain fetch.
   No tokens, sessions, or credentials are stored. */

const PolyRiboApi = (() => {
  const DEFAULT_BASE_URL = "http://localhost:5246";
  const BASE_URL_STORAGE_KEY = "polyribo.apiBaseUrl";
  const REQUEST_TIMEOUT_MS = 15000;

  class ApiError extends Error {
    constructor(message, status, kind, payload) {
      super(message);
      this.name = "ApiError";
      this.status = status;
      this.kind = kind;
      this.payload = payload;
    }
  }

  function normaliseBaseUrl(value) {
    return String(value || "").trim().replace(/\/+$/, "");
  }

  function getBaseUrl() {
    try {
      const saved = window.localStorage.getItem(BASE_URL_STORAGE_KEY);
      return normaliseBaseUrl(saved) || DEFAULT_BASE_URL;
    } catch {
      return DEFAULT_BASE_URL;
    }
  }

  function setBaseUrl(value) {
    const normalised = normaliseBaseUrl(value);

    if (!normalised) {
      throw new Error("Enter an API base URL.");
    }

    const parsed = new URL(normalised);

    if (!/^https?:$/.test(parsed.protocol)) {
      throw new Error("Use an http:// or https:// URL.");
    }

    window.localStorage.setItem(BASE_URL_STORAGE_KEY, normalised);
    return normalised;
  }

  function resetBaseUrl() {
    try {
      window.localStorage.removeItem(BASE_URL_STORAGE_KEY);
    } catch {
      // Storage can be unavailable in some browser modes; the default still works.
    }

    return DEFAULT_BASE_URL;
  }

  async function request(path, options = {}) {
    const controller = new AbortController();
    const timeout = window.setTimeout(() => controller.abort(), REQUEST_TIMEOUT_MS);
    let response;

    try {
      response = await fetch(`${getBaseUrl()}${path}`, {
        method: options.method || "GET",
        headers: {
          Accept: "application/json",
          ...(options.body ? { "Content-Type": "application/json" } : {}),
          ...(options.headers || {}),
        },
        body: options.body ? JSON.stringify(options.body) : undefined,
        signal: controller.signal,
      });
    } catch (error) {
      if (error.name === "AbortError") {
        throw new ApiError(
          "The API did not respond in time. Check that the backend is running.",
          0,
          "timeout"
        );
      }

      throw new ApiError(
        `Could not reach the API at ${getBaseUrl()}. The backend may be stopped, or CORS may be blocking this page.`,
        0,
        "network"
      );
    } finally {
      window.clearTimeout(timeout);
    }

    const rawBody = await response.text();
    let payload = null;

    if (rawBody) {
      try {
        payload = JSON.parse(rawBody);
      } catch {
        payload = { message: rawBody };
      }
    }

    if (!response.ok) {
      throw new ApiError(
        payload?.message || payload?.title || `The API returned HTTP ${response.status}.`,
        response.status,
        "http",
        payload
      );
    }

    return payload;
  }

  function login(userName, password) {
    return request("/api/Authentication/Login", {
      method: "POST",
      body: { userName, password },
    });
  }

  function register({ userName, email, password, countryId }) {
    return request("/api/Authentication/Register", {
      method: "POST",
      body: { userName, email, password, countryId },
    });
  }

  return {
    ApiError,
    DEFAULT_BASE_URL,
    getBaseUrl,
    setBaseUrl,
    resetBaseUrl,
    request,
    login,
    register,
  };
})();
