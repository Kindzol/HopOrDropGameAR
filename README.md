# Hop or Drop AR

A 3D physics-based platformer built in Unity with AR Foundation. Place the game in your real environment, control a ball rolling and jumping across floating platforms anchored to a real surface. One goal: reach the finish without falling.

---

## How to Play

| Action | Control |
|--------|---------|
| Move | Drag the virtual joystick |
| Jump | Double tap the joystick |
| Place level | Tap a detected surface to spawn the level |

---

## Platform Types

| Platform | Effect |
|----------|--------|
| Normal | Safe to stand on |
| Hazard | Lose a life on contact |
| Disappearing | Vanishes shortly after touch |

---

## Life System

- You start with **3 lives**
- A life is lost when you **fall** below the level or **touch a hazard** platform
- Lose all lives → **Game Over**
- Reach the finish → **You Win!**

---

## AR Features

- **Plane detection** — AR Foundation detects flat real-world surfaces
- **Level anchoring** — tap to place the game level on a detected plane
- **Camera-relative movement** — ball moves relative to how you hold your device
- **Touch-based input** — virtual joystick and double tap for full control

---

## Features

- Background music that changes between scenes
- Sound effects for losing a life and completing the level
- Physics-based movement with natural rolling
- Persistent game state managed by a singleton GameManager
- Level scales to fit naturally on real surfaces (table, floor)

---

## Built With

- **Unity 6**
- **C#**
- **AR Foundation**
- **Google ARCore XR Plugin**
- **Unity Physics (Rigidbody)**
- **TextMeshPro**
- **Universal Render Pipeline (URP)**

