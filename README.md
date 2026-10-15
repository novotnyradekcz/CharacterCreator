# Ateliér postav

A Czech-language character creator for **Jeskyně a draci (JaD)**, a tabletop role-playing game. Choose a character's ancestry and class, roll ability scores, assign the results, and see the final scores with ancestry bonuses applied.

The current version focuses on JaD character creation. It does not save characters between visits, and it does not yet include Dungeons & Dragons rules.

## Features

- Czech-language character creation for JaD.
- The documented JaD ancestry, subrace, and class options.
- Six ability-score rolls using 4d6, dropping the lowest die from each roll.
- One-to-one assignment of the six results to the character's abilities. Equal totals remain separate rolls and can each be assigned once.
- Ancestry and subrace bonuses, including choices such as a human's two +1 bonuses.
- A final character sheet with base scores and applied bonuses.

## Run locally

Requires the .NET 10 SDK.

From the repository root:

```sh
dotnet restore CharacterCreator.sln
dotnet run --project src/CharacterCreator.Web/CharacterCreator.Web.csproj --urls http://localhost:5178
```

Open <http://localhost:5178>. If that port is already in use, change `5178` in the command and URL to another available port.

## Build and test

```sh
dotnet build CharacterCreator.sln
dotnet test CharacterCreator.sln
```

## Tech stack

- **.NET 10 and C#** for the application and domain logic.
- **Blazor Web App with Interactive Server rendering** for the browser UI; the frontend and backend are both C#.
- **xUnit** for tests of dice rolling, ruleset data, bonuses, and character building.

## Project layout

```text
.
├── src/
│   ├── CharacterCreator.Core/
│   │   ├── Ability.cs
│   │   ├── CharacterBuildService.cs
│   │   ├── DiceRollRule.cs
│   │   ├── JaDRuleset.cs
│   │   ├── RulesetModels.cs
│   │   └── CharacterCreator.Core.csproj
│   └── CharacterCreator.Web/    # Blazor UI and application startup
├── tests/
│   └── CharacterCreator.Tests/  # xUnit test suite
├── CharacterCreator.sln
└── README.md
```

The core uses English identifiers and the versioned ruleset ID `jad-v1`. Czech names are kept in the UI layer so additional rulesets or languages can reuse the same character model.

## Possible next steps

1. **Add D&D support.** Implement another `ICharacterRuleset` with its own stable, versioned ID and catalog, then add a ruleset selector to the UI. Keep game-specific options and bonus resolution inside each ruleset rather than branching on game names throughout the interface.
2. **Save and export characters.** Add persistence or downloadable character sheets. Store the ruleset ID with each character so saved characters keep the rules they were created with.
3. **Expand character details.** Add game-specific proficiencies, features, equipment, and other creation steps as the rulesets require them.
4. **Add more languages.** Move UI labels into localization resources while keeping domain identifiers and rules independent of display text.
5. **Add an API if needed.** The current app does not need a separate API. An ASP.NET Core API could be introduced if another frontend, mobile client, or external integration needs to consume the core services.