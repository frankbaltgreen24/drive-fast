# Game Design Document — Drive Fast

## 1. High-level Vision

Short pitch
- Arcade street racing with realistic presentation and aggressive police chases across large U.S.-inspired city maps. Fast, responsive handling with a grip-first drifting system and cinematic police encounters.

Tone / Style
- Arcade gameplay feel (NFS-like) with realistic visuals and audio. Cinematic, high-energy, summer-night street racing aesthetic.

## 2. Target Platform & Tech

- Platform: PC (Windows).
- Target framerate targets: scalable presets targeting 30 FPS (low), 60 FPS (recommended), and higher where possible. Default target profile: 60 FPS on mid-range PC hardware.
- Engine: Unity (LTS). Render pipeline: URP recommended for performance/visual balance.

## 3. Driving Feel

Design choice
- Arcade-focused controls with realistic presentation: weighty vehicle feel, rapid responsiveness, but forgiving handling compared to sims.
- Drifting approach: "grip-first" drifting. Cars should maintain traction and drift primarily when the player intentionally inputs drift (handbrake/drift input) — less slide-by-default, more controlled drifts.
- Steering: tight & responsive with clear feedback (camera FOV, tire SFX, visual cues).
- AI: aggressive opponents and police drivers that actively intercept, PIT, and coordinate.

Tuning variables (for designers)
- Top speed, acceleration curve, steering sensitivity curve, lateral grip multiplier, weight transfer factor, handbrake effectiveness, nitro boost magnitude and cooldown.

## 4. Map & Levels

Scope & approach
- Initial plan: 4 distinct map levels representing stylized U.S. cities (e.g., City A: dense downtown, City B: industrial waterfront, City C: suburban sprawl, City D: desert/hwy). Each map will support day/night variants (summer-only weather).
- Long-term aspiration: a federated U.S. map (many cities) — note this is very large scope and will be phased.

Map features
- Each map supports free roam, event start locations, police spawn points, traffic networks, and performance-friendly streaming/chunking.
- Lighting: day/night cycle or discrete day and night scene variants (start with discrete night/day variants for MVP).

## 5. Police & Chase Mechanics

Wanted system
- Wanted levels 0–5. Wanted increases quickly during illegal driving and collisions with police; decays slowly when player is out of sight.

Police types & behaviors
- Patrol cars: routine patrol, pursue on sight, attempt PIT at higher wanted levels.
- Helicopter: appears at wanted >= 3 to track and spotlight player (provides position updates to ground units).
- SWAT / Heavy response: deploy at very high wanted levels (roadblocks, spike strips).
- Roadblocks & spike strips: used at high wanted levels to slow/disable player car.
- Spike strips: reduce tire effectiveness / cause flat behavior; PIT maneuvers risk collision/damage.

Consequences when caught
- Options: respawn at nearest checkpoint, vehicle impounded requiring payment/time, or mission failure. (Default MVP: respawn at last checkpoint and wanted reset.)

## 6. Progression & Game Loop

- Game loop: Career mode with events + free-roam between events.
- Currency/Progression: money to buy cars and parts, reputation/wanted notoriety tied to events.
- Unlocks: new cars, performance parts (engine, tires, suspension), and visual customizations.

## 7. Cars & Customization

- Long-term target: 150 base cars.
- Licensing: user requested real cars. Important legal note: using real-world car make/model likenesses and trademarked names may require licensing agreements. For public distribution you must obtain licenses or use generic/unbranded lookalikes. Recommendation: start with fictional/unbranded vehicles or obtain permission for a small set of marquee cars, and expand licensed roster later.
- Customization: visual (wraps, bodykits, rims) and performance (engine, transmission, tires, NOS). Separate visual-only mods from performance-affecting parts.

## 8. Damage & Recovery

- Damage: implement both visual and functional handling degradation over time for realism. For MVP, begin with visual-only damage and a simple handling penalty (e.g., decreased top speed or grip on heavy damage).
- Recovery: respawn to last checkpoint on capture or serious stuck state. Implement garages (repair/save) in progression later.

## 9. UI & Audio

- HUD: speedometer, minimap, wanted meter, current mission/objective, lap/time.
- Audio: dynamic soundtrack with chase music layers; engine SFX based on RPM mapping; police radio SFX.
- Radio: optional in-game radio and dynamic chase music crossfades.

## 10. Multiplayer / Online

- MVP: single-player only.
- Long-term: online multiplayer/events planned as post-MVP feature.

## 11. Art & Assets

- For development: use Asset Store placeholders and freely licensed models for iteration.
- For release with real cars: source licensed models or contract modelers. Consider using high-quality placeholders for up to 150 cars initially.

## 12. Controls & Accessibility

- Input: keyboard + gamepad (full controller support). Rebindable keys and basic assists (steering assist, braking assist) to help accessibility.

## 13. Monetization / Publishing

- Undecided. Recommendations: paid release or early access on PC platforms. Avoid monetization tied to licensed car content until legal clearances are in place.

## 14. Timeline & Scope

Realistic phasing
- MVP (prototype): 8–12 weeks (solo dev) — single map, 1–3 drivable cars, basic police behavior, UI, and core handling.
- Early playable (small team/single dev): 6–12 months — multiple maps (4 small maps), AI improvements, several cars (10–30), basic progression and customization.
- Full vision (150 licensed cars, multiple cities): 2+ years and a team with licensing, art, and QA resources.

Risks & notes
- 150 real cars + multiple U.S. cities is huge; prioritize a smaller, tuned set for MVP.
- Licensing for real car models is nontrivial and can be costly/time-consuming.

## 15. Must-have Features (initial)

- Tight, responsive arcade handling with grip-based drifting.
- Aggressive AI racers & police with PIT/roadblock mechanics at higher wanted levels.
- Four distinct map levels (stylized U.S. cities) with day/night support (summer).
- Support for adding many cars later (data-driven car definitions).

## Implementation Roadmap & Milestones (high level)

Phase 0 — Setup (week 0)
- Project repo, Unity project baseline, packages (Input System, Cinemachine, URP), Git + LFS setup.

Phase 1 — Core driving prototype (weeks 1–4)
- Implement PlayerCarController (arcade/grip drift), Cinemachine camera, engine audio hook.
- Create one prototype map and 1–2 placeholder cars.
- Basic UI: speedometer, wanted meter, simple minimap.

Phase 2 — AI & Police (weeks 5–8)
- Waypoint AI for racers, police pursuit, wanted system, spike strip & roadblock prototypes.
- Aggressive AI tuning and PIT behavior prototyping.

Phase 3 — Maps & progression (weeks 9–16)
- Build additional maps (total 4), event system, garages, simple economy & car shop (placeholder cars).

Phase 4 — Expansion & polish (months)
- Add more cars, visual customization, licensed assets (as legal), audio polish, optimization.

## Next immediate steps (what I will do now)

1. Create this GDD file in the repository root as `GDD.md`.  
2. If you want, I will also:
   - Create a GitHub project board with milestones and detailed tasks for the MVP and Phase 1–3.
   - Update `README.md` with a short project summary referencing the GDD.
   - Push initial starter scripts into `Assets/Scripts/` (player controller, AI, police, wanted, respawn) if not already pushed.

Tell me which follow-ups you want me to run now: `create board`, `push starter code`, `update README`, or `all`. If you want edits to the GDD content above before I add it to the repo, say `edit GDD` and list changes.