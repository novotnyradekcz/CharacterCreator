# Tvorba postavy

A Czech-language character creator for **Jeskyně a draci (JaD)**, a tabletop role-playing game. Choose a character's ancestry and class, roll ability scores, assign the results, and see the final scores with ancestry bonuses applied.

The current version focuses on JaD character creation. It does not save characters between visits, and it does not yet include Dungeons & Dragons rules.

## Features

- Czech-language character creation for JaD.
- The documented JaD ancestry, subrace, and class options.
- Separate ancestry and class accordions with keyboard-accessible expandable entries and explicit selection buttons. Browsing an entry does not change the character.
- Species with subraces offer individual subrace buttons inside their accordion card; choosing one selects both the species and its subrace.
- Brief Czech summaries paraphrased from the JaD 1.3.3 rulebook (chapter 3, **Rasy**, pp. 58–94; chapter 4, **Povolání**, pp. 98–188), with printed-page references and ancestry/subrace ability bonuses drawn from the ruleset.
- Six ability-score rolls using 4d6, dropping the lowest die from each roll.
- One-to-one assignment of the six results to the character's abilities. Equal totals remain separate rolls and can each be assigned once.
- Drag scores from the pool on the right onto abilities on the left, or select a score and then an ability using click, tap, or keyboard. Assigned scores can be moved or returned to the pool. Dropping onto an occupied ability swaps assigned scores, or returns the previous score to the pool if the new score came from there.
- Ancestry and subrace bonuses, including choices such as a human's two +1 bonuses.
- The finish button brings the final character sheet into view, showing all six base scores, applied ancestry/subrace bonuses, final scores, and signed ability modifiers (`floor((final score - 10) / 2)`).

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