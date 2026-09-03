# 🏴‍☠️ Grow Your Ship: 40075 — The Lost Captain

> A touch-and-idle grinding game! Upgrade your pirate ship and search for your lost captain!

A solo-developed touch-based idle game. I handled every stage of the project — design, development, and release — **entirely on my own**, using Unity and C#.

---

## 📖 About the Game

The protagonist sets sail on a ship, only to lose their captain along the way. To find him, they must voyage **40,075 km** — roughly the circumference of the Earth — gathering materials and using them to upgrade the ship into something faster and stronger. This upgrade loop is the core of the game.

- Collect a wide variety of materials and collectibles through touch interactions
- Use materials to upgrade the ship, making it faster and more powerful
- Restock materials through a gacha system, exchange shop, store, and mini-games
- A mysterious crew of pirates accompanies the voyage, driving the story forward

---

## 🛠 Tech Stack

- **Engine**: Unity
- **Language**: C# (Visual Studio)
- **Save System**: Local device storage (no cloud/online save)

---

## 🔧 Key Systems Implemented

**Real-Time Resource Production & UI Synchronization**
Designed a `Coroutine`-based system in which multiple material types accumulate simultaneously at different rates, updating every 0.1–1 seconds. Because material quantities, purchases (consumption), and UI display all had to stay in sync in real time, the core challenge was architecting the data flow so that when a player made a purchase, materials were deducted instantly and the UI updated correctly without conflicting with the resource-production logic running in parallel.

**Local Data Save & Recovery System**
With no cloud server and all data stored purely on-device, a sync mismatch between save timing and the constantly changing real-time resource data caused a recurring bug where player progress would reset. It took about a month of tracing the root cause before I redesigned the save logic and built a custom save/recovery system that guarantees data integrity.

**In-App Purchase (IAP) Integration**
Integrated the Google Play and App Store payment APIs directly. With limited official documentation and reference material available, I designed and implemented the in-game currency reward logic and payment verification flow from scratch.

**Performance Optimization**
Minimized `Update()` method calls throughout the codebase to prevent frame drops and lag, even with multiple real-time systems running concurrently.

---

## 👤 Background & Role

- **Developer**: Solo developer — handled design, programming, and UI entirely on my own
- **Development period**: ~6 months (averaging 6–7 hours per day)
- **Motivation**: After enjoying a simulation game called *Grow a Bar*, I wanted to build a similar idle-style game of my own
- **Timing**: Built at age 20, before AI coding tools were widely available. I self-taught the fundamentals of physics engines and coding through YouTube tutorials (GoldMetal) and general research, then wrote the rest of the code myself

### Major Technical Problems & How I Solved Them

**1. Performance Optimization**
Because several systems (resource production, UI updates, purchase processing) had to run simultaneously in real time — a defining trait of idle games — I minimized `Update()` method calls to prevent frame drops and lag.

**2. Local Data Save & Recovery System**
Storing data purely on-device with no cloud backend led to a recurring bug where sync errors between save timing and constantly changing real-time resource data would reset player progress. After about a month of tracing the root cause, I redesigned the save logic and built a custom save/recovery system that guarantees data integrity.

**3. Real-Time Resource Production & UI Sync**
With materials updating every 0.1–1 seconds, I used `Coroutine`s sparingly to allow multiple currencies to accumulate simultaneously at different rates. The core challenge was designing the data flow so that a purchase would instantly deduct materials and update the UI without conflicting with the resource-production logic running in parallel.

**4. Store Launch Preparation & IAP Integration**
Made extensive changes to project settings to meet the file format and configuration requirements of Google Play and the App Store. In-app purchase integration in particular was difficult, since the implementation approach varies from game to game and reference material was scarce — I had to work through the currency-reward logic and payment verification flow largely through trial and error.

---

## 🚀 Release History

| Item | Details |
|---|---|
| Platforms | Google Play Store, Apple App Store |
| Post-launch | Continued updates based on user reviews and feedback after release |
| Revenue | ₩170,700 (Google Play) / ₩4,155,174 (Apple App Store) |
| Current status | Removed from both stores due to a policy change that took effect while the developer was serving mandatory military service; currently not listed on any storefront |

> Note: The game runs and launches correctly from the Unity Editor.

---

## 📸 Screenshots

![Main Screen](screenshots/2.png)
![Core Gameplay Screen](screenshots/3.png)
![Material Exchange](screenshots/1.png)
![Ship Upgrade Screen](screenshots/4.png)
![Material Inventory](screenshots/5.png)
![Gacha Screen](screenshots/6.png)
![Treasure Screen](screenshots/7.png)
![Shop Screen](screenshots/8.png)

---

## 📩 Contact

kimjy5191112@gmail.com

---

## ⚠️ Notice

This repository is shared for portfolio purposes to document my game development experience. No signing keys (keystores), API keys, or other sensitive credentials are included.
