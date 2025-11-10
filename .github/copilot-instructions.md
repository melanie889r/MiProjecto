## Copilot instructions for Boomerang 2D Framework

Treat this repository as a Unity engine framework (not a standalone app). The goal is to help contributors make small, safe changes, implement features, and fix engine-level bugs.

- Big picture (what to know first)
  - This project is a Unity framework under `Assets/Boomerang2DFramework/Framework` that provides reusable subsystems: Actors, Camera, MapGeneration, GameEvents, GameFlagManagement, UiManagement, and a content indexing system (`BoomerangDatabase`). See `Boomerang2D.cs` and `StartDemoGame.cs` for the initialization flow.
  - Initialization: call `Boomerang2D.InitializeFramework()` (see `StartDemoGame.cs`). That creates the main camera and attaches required manager components to the GameObject named by `GameProperties.InitializingGameObjectName` (default `Boomerang2D`). `BoomerangDatabase` indexes project content from a `BoomerangReferenceDatabase` component on that GameObject.
  - Game events are data-driven: `GameEventBuilder` deserializes JSON into `GameEventProperties` and instantiates a `GameEvent` implementation by string name. Changing class or namespace names will break serialized JSON references.

- Where change is safe and where to be cautious
  - Safe: editor-only tooling, example/demo scripts (e.g., `StartDemoGame.cs`), and adding new `CustomScripts` namespaces as listed in `GameProperties.cs` (there are explicit namespaces the framework expects for custom hooks).
  - Caution: renaming core classes/namespaces or changing JSON schema used in `GameEventBuilder` or `BoomerangDatabase` (e.g., `GameFlags.json`, actor/map JSON entries). Those names are referenced by string in save files and the reference database.

- Key files to reference (examples used in instructions)
  - `Assets/Boomerang2DFramework/Framework/Boomerang2D.cs` — framework bootstrap and global references
  - `Assets/Boomerang2DFramework/Framework/StartDemoGame.cs` — minimal start example
  - `Assets/Boomerang2DFramework/Framework/GameEvents/GameEventBuilder.cs` — how events are constructed from JSON
  - `Assets/Boomerang2DFramework/Framework/GameFlagManagement/GameFlags.cs` — runtime flag access and save format (`GameFlags.json`)
  - `Assets/Boomerang2DFramework/Framework/BoomerangDatabaseManagement/BoomerangDatabase.cs` — how content gets indexed (requires `BoomerangReferenceDatabase` on the `InitializingGameObjectName` GameObject)
  - `Assets/Boomerang2DFramework/Framework/GameProperties.cs` — many conventions and namespace strings for extending engine behavior

- Common patterns and conventions AI should follow
  - Namespaces: prefer engine namespaces (Engine.*) and place custom code under `Boomerang2DFramework.CustomScripts.*` as configured in `GameProperties.cs`. This keeps runtime lookups (Type.GetType) predictable.
  - Data-driven design: many systems read JSON(TextAsset) at runtime. When adding new Serializable types, include a small migration note if the JSON schema changes.
  - Assembly: the framework uses an asmdef `B2DFramework` at `Framework/B2DFramework.asmdef`. Keep refactor-aware changes compatible with assembly boundaries.

- Run / edit / debug notes (practical steps)
  - Open the project in Unity Editor. Use the Tools -> Boomerang2D menu for helper actions (the package registers editor menu items). For building the reference database, use Tools -> Boomerang2D -> Build Reference Database (or call `BoomerangDatabase.PopulateDatabase()` in editor code).
  - To test a small change: attach a GameObject named `Boomerang2D` to an empty Scene, add the `BoomerangReferenceDatabase` component and assign the project's TextAssets/Assets in the inspector, then run Play to exercise indexing and runtime behavior.
  - There are no automated unit tests in the repo; prefer manual test in Editor/Play mode for quick validation. Keep changes small and verify Play-mode bootstrapping.

- Examples of pitfalls to avoid (concrete)
  - Do not rename classes that are referenced as strings in `GameEventBuilder.GameEventClass` or in saved JSON files without updating saved content.
  - `GameFlags` are persisted to `Assets/Boomerang2DFramework/Framework/GameFlagManagement/GameFlags.json` (see `GameFlags.SaveGameFlags()`); editing the in-memory dictionaries without persisting will not change the file.
  - `ToggleGameFlagBool.cs` and similar event classes operate on `GameFlags`; check `Get`/`Set` semantics in `GameFlags.cs` when fixing logic.

- If you need more context
  - Look at `Assets/Boomerang2DFramework/Getting Started.txt` and the `Tools > Boomerang2D` editor menu for project-specific editor utilities.

If anything above is unclear or you want the instructions tailored to a narrower agent role (e.g., refactor-only, test-writer, bug-fixer), tell me which role and I'll iterate.
