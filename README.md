# The Trail
### *An Interactive Encyclopedia of Human History*

> *"Follow the trail of human history."*

The Trail is an interactive history encyclopedia where users journey through seven eras of human history — reading chapters, taking quizzes, and earning collectibles along the way.

---

## 🎬 Demo

📺 [Watch the full demo on YouTube](#) *(link coming soon)*

---

## ✨ Features

### 🌍 Seven Eras of History
From the Age of Dinosaurs to the Digital Age — each era has a cinematic hero, five chapters of curated content, and its own visual identity.

### 📖 Chapter Reading Experience
Each chapter is a richly formatted reading experience with paragraphs, fact blocks, quotes, timelines, and inline images — rendered from a structured JSON content format.

### 🧠 Quiz System
Every chapter has a five-question quiz. Pass with 60%+ to earn a **Common** collectible. Score a perfect 5/5 to earn a **Rare** collectible.

### 🏆 Collectible System
77 collectibles across three tiers:
- **Common** — earned by passing a chapter quiz
- **Rare** — earned by scoring 5/5 on a chapter quiz
- **Legendary** — earned by achieving a perfect score on all five chapters in an era (Era Grandmaster)

### 🎉 Era Grandmaster Celebration
When a user earns a Legendary collectible, a full-screen celebration modal fires with a particle burst animation, spinning amber rings, and a dramatic reveal of the Legendary collectible.

### 👤 Profile Page
A personal profile with hero stats, a trophy cabinet accordion organised by era, and an interactive atlas book showing era progress on a winding trail map.

### 🔍 Wikipedia Dive Deeper
Every chapter includes a Wikipedia summary card at the bottom, fetched live from the Wikipedia REST API — no key required.

### 🔐 Authentication
Full JWT-based authentication with ASP.NET Identity. Unauthenticated users can read chapters but see a styled "Join The Trail" prompt instead of the quiz.

---

## 🛠 Tech Stack

| Layer | Technology |
|---|---|
| Backend API | ASP.NET Core 10 |
| ORM | Entity Framework Core 10 |
| Database | PostgreSQL |
| Auth | ASP.NET Identity + JWT |
| Architecture | Clean Architecture (5 projects) |
| Frontend | React 18 + Vite + TypeScript |
| Styling | Tailwind CSS v4 |
| Animation | Framer Motion |
| Fonts | Cinzel + EB Garamond |

---

## 🏗 Architecture

The backend follows Clean Architecture with five projects:

```
TheTrail.sln
├── TheTrail.Api           ← Controllers, Program.cs
├── TheTrail.Data          ← DbContext, Repository, Migrations, Seeding
├── TheTrail.Domain        ← Entities, Enums, ValidationConstants
├── TheTrail.Services      ← Service implementations
└── TheTrail.Services.Core ← Interfaces + DTOs
```

The frontend is a React + Vite SPA with an Axios client that attaches JWT tokens automatically via an interceptor.

---

## 🚀 Local Setup

### Prerequisites
- .NET 10 SDK
- Node.js 18+
- PostgreSQL
- Git

### 1. Clone the repository
```bash
git clone https://github.com/krasimir-paunov/the-trail.git
cd the-trail
```

### 2. Configure the database
Create a PostgreSQL database named `the_trail`.

In `TheTrail.Api/appsettings.json`, update the connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=the_trail;Username=your_user;Password=your_password"
}
```

### 3. Run the API
```bash
cd TheTrail.Api
dotnet run
```

The API will start at `https://localhost:7134`. On first run, the database seeders will automatically populate all 7 eras, 35 chapters, 35 quizzes, and 77 collectibles.

You can explore the API via Scalar at `https://localhost:7134/scalar/v1`.

### 4. Run the frontend
```bash
cd TheTrail.Web
npm install --legacy-peer-deps
npm run dev
```

The frontend will start at `http://localhost:5173`.

### 5. Default admin account
```
Email:    admin@thetrail.com
Password: Admin123!
```

---

## 📸 Screenshots

> *(Screenshots coming soon — see the YouTube demo for a full walkthrough)*

| Page | Description |
|---|---|
| Homepage | Cinematic era accordion with full-screen panels |
| Era Page | 75vh hero, chapter list with progress indicators |
| Chapter Page | Parchment reading layout with sidebar progress |
| Quiz | Full-screen overlay with animated answer feedback |
| Reward Screen | Collectible reveal with glow effects |
| Era Grandmaster | Particle burst celebration modal |
| Profile Page | Hero stats + trophy cabinet + atlas book |

---

## 🗺 Roadmap

Features planned for future development:

- [ ] **User avatar system** — set any earned collectible as your profile avatar
- [ ] **Bulgarian language support** — full translation of all content and UI
- [ ] **Collectible image pipeline** — generate and compress all 77 collectible images
- [ ] **Era accent colors on ChapterPage** — reading progress bar and fact blocks use the era's own accent color
- [ ] **Social features** — compare progress with other explorers
- [ ] **Search** — search across all chapters and eras
- [ ] **Mobile optimization** — responsive improvements for smaller screens

---

*Built with passion for history — and code.*
