# Drive Fast

Drive Fast is an arcade street racing prototype (Unity) focused on a realistic presentation with an arcade driving feel and aggressive police chases. This repository contains starter scripts and a Game Design Document (GDD) to guide development.

See GDD.md for the full design document.

Quick start
1. Open Unity (2021 LTS or later recommended) and create a new project using the URP template.
2. In the Project, create these folders: Assets/Scripts, Assets/Scenes, Assets/Prefabs.
3. Copy the scripts from Assets/Scripts in this repo into your Unity project.
4. Create a Player GameObject with a Rigidbody and attach the `PlayerCarController` and `WantedSystem` scripts.
5. Create police and AI prefabs and attach `PoliceAI` and `WaypointAI` respectively.
6. Install Cinemachine from the Package Manager and add a Virtual Camera that follows your Player.

Notes
- The repository currently uses placeholder scripts and no licensed real-car models. Using real makes/models in a public release requires licensing; see GDD.md for details. Use placeholder/unbranded models for development.

Next steps
- Import placeholder car models, create a prototype scene (night downtown), and test driving feel.
- Tune PlayerCarController values and police detection to match the desired arcade/grip feel.
