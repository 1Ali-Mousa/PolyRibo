/* Shared application shell: sidebar, top bar, breadcrumbs, and mobile navigation. */

const PolyRiboApp = (() => {
  const PAGES = {
    dashboard: { label: "Dashboard", section: "Workspace" },
    projects: { label: "Projects", section: "Workspace" },
    databases: { label: "Databases", section: "Workspace" },
    "new-project": { label: "New project", section: "Generation" },
    history: { label: "Generation history", section: "Generation" },
    settings: { label: "Settings", section: "Preferences" },
    profile: { label: "Profile", section: "Preferences" },
  };

  const NAV_GROUPS = [
    {
      label: "Workspace",
      links: [
        { page: "dashboard", href: "Dashboard.html", icon: "layout-dashboard", label: "Dashboard" },
        { page: "projects", href: "Projects.html", icon: "folder-kanban", label: "Projects" },
        { page: "databases", href: "Databases.html", icon: "database", label: "Databases" },
      ],
    },
    {
      label: "Generation",
      links: [
        { page: "new-project", href: "NewProject.html", icon: "wand-sparkles", label: "New project" },
        { page: "history", href: "GenerationHistory.html", icon: "history", label: "Generation history" },
      ],
    },
    {
      label: "Preferences",
      links: [
        { page: "settings", href: "Settings.html", icon: "settings", label: "Settings" },
        { page: "profile", href: "Profile.html", icon: "user-round", label: "Profile" },
      ],
    },
  ];

  function brandMarkup() {
    return `
      <a class="sidebar-brand" href="Dashboard.html" aria-label="PolyRibo dashboard">
        <span class="sidebar-mark" aria-hidden="true">
          <svg viewBox="0 0 24 24" fill="none">
            <rect x="4" y="5" width="16" height="3.2" rx="1.6" fill="#171717"/>
            <rect x="4" y="10.4" width="11" height="3.2" rx="1.6" fill="#171717"/>
            <rect x="4" y="15.8" width="7" height="3.2" rx="1.6" fill="#171717"/>
            <circle cx="17.5" cy="17.4" r="2.8" fill="#171717"/>
          </svg>
        </span>
        <span class="sidebar-brand-text">
          <strong>PolyRibo</strong>
          <small>Database → Project</small>
        </span>
      </a>
    `;
  }

  function navMarkup(activePage) {
    return NAV_GROUPS.map((group) => `
      <section class="nav-group" aria-label="${group.label}">
        <p class="nav-heading">${group.label}</p>
        ${group.links.map((link) => `
          <a class="nav-link${link.page === activePage ? " is-active" : ""}"
             href="${link.href}"
             data-nav="${link.page}"
             ${link.page === activePage ? 'aria-current="page"' : ""}>
            <i data-lucide="${link.icon}" aria-hidden="true"></i>
            <span>${link.label}</span>
          </a>
        `).join("")}
      </section>
    `).join("");
  }

  function sidebarMarkup(activePage) {
    return `
      ${brandMarkup()}
      <nav class="sidebar-nav" aria-label="Main navigation">
        ${navMarkup(activePage)}
      </nav>
      <div class="sidebar-foot">
        <span class="badge badge--soft">Frontend</span>
        <p>API<br><code data-api-base-url>http://localhost:5246</code></p>
      </div>
    `;
  }

  function topbarMarkup(meta) {
    return `
      <div class="topbar-left">
        <button class="icon-button mobile-menu-toggle"
                type="button"
                data-sidebar-toggle
                aria-label="Open navigation"
                aria-expanded="false"
                aria-controls="appSidebar">
          <i data-lucide="menu" aria-hidden="true"></i>
        </button>
        <div class="topbar-context" aria-hidden="true">
          <span class="topbar-section">${meta.section}</span>
          <span class="topbar-divider">/</span>
          <span class="topbar-page">${meta.label}</span>
        </div>
      </div>
      <div class="topbar-right">
        <span class="api-pill" title="Configured API base URL">
          <span class="api-dot" aria-hidden="true"></span>
          <code data-api-base-url>http://localhost:5246</code>
        </span>
        <a class="btn btn--ghost btn--small" href="Login.html">
          <i data-lucide="log-in" aria-hidden="true"></i>
          Sign in
        </a>
      </div>
    `;
  }

  function breadcrumbMarkup(meta) {
    return `
      <span>${meta.section}</span>
      <i data-lucide="chevron-right" aria-hidden="true"></i>
      <span aria-current="page">${meta.label}</span>
    `;
  }

  function initSidebarToggle() {
    const sidebar = document.getElementById("appSidebar");
    const toggle = document.querySelector("[data-sidebar-toggle]");
    if (!sidebar || !toggle) return;

    const setOpen = (isOpen) => {
      document.body.classList.toggle("sidebar-open", isOpen);
      toggle.setAttribute("aria-expanded", String(isOpen));
      toggle.setAttribute("aria-label", isOpen ? "Close navigation" : "Open navigation");
    };

    toggle.addEventListener("click", () => {
      setOpen(!document.body.classList.contains("sidebar-open"));
    });

    document.querySelectorAll("[data-sidebar-close], .nav-link").forEach((element) => {
      element.addEventListener("click", () => setOpen(false));
    });

    document.addEventListener("keydown", (event) => {
      if (event.key === "Escape") setOpen(false);
    });
  }

  function init() {
    const page = document.body.dataset.page;
    const meta = PAGES[page];

    if (!meta) return;

    const sidebar = document.getElementById("appSidebar");
    const topbar = document.getElementById("appTopbar");
    const breadcrumbs = document.querySelector("[data-breadcrumbs]");

    if (sidebar) sidebar.innerHTML = sidebarMarkup(page);
    if (topbar) topbar.innerHTML = topbarMarkup(meta);
    if (breadcrumbs) breadcrumbs.innerHTML = breadcrumbMarkup(meta);

    const scrim = document.createElement("div");
    scrim.className = "sidebar-scrim";
    scrim.dataset.sidebarClose = "";
    scrim.setAttribute("aria-hidden", "true");
    document.body.append(scrim);

    initSidebarToggle();

    if (window.PolyRiboUI) {
      window.PolyRiboUI.refreshIcons();
      window.PolyRiboUI.refreshApiLabels();
    }
  }

  document.addEventListener("DOMContentLoaded", init);

  return { init };
})();
