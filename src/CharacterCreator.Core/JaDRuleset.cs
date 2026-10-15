namespace CharacterCreator.Core;

public sealed class JaDRuleset : ICharacterRuleset
{
    private static readonly IReadOnlyList<Ability> AllAbilities = Enum.GetValues<Ability>();

    public string Id => "jad-v1";

    public string Name => "Jeskyně a draci";

    public DiceRollRule AbilityScoreRule { get; } = new(4, 6, 1, 6);

    public IReadOnlyList<AncestryDefinition> Ancestries { get; } =
    [
        new("human", Bonuses(), [], new(2, AllAbilities)),
        new("elf", Bonuses((Ability.Dexterity, 2)),
        [
            new("wild", Bonuses((Ability.Charisma, 1))),
            new("high", Bonuses((Ability.Intelligence, 1))),
            new("night", Bonuses((Ability.Wisdom, 1))),
            new("blood", Bonuses(), new(1, [Ability.Intelligence, Ability.Charisma]))
        ]),
        new("dwarf", Bonuses((Ability.Constitution, 2)),
        [
            new("underground", Bonuses((Ability.Wisdom, 1))),
            new("stone", Bonuses((Ability.Strength, 2))),
            new("light", Bonuses((Ability.Dexterity, 1), (Ability.Intelligence, 1))),
            new("fire", Bonuses((Ability.Charisma, 1)))
        ]),
        new("halfling", Bonuses((Ability.Dexterity, 2), (Ability.Charisma, 1)), []),
        new("half-orc", Bonuses((Ability.Strength, 2), (Ability.Constitution, 1)), []),
        new("gnome", Bonuses((Ability.Intelligence, 2), (Ability.Constitution, 1)), []),
        new("dragonborn", Bonuses((Ability.Strength, 2), (Ability.Charisma, 1)), []),
        new("tiefling", Bonuses((Ability.Charisma, 2), (Ability.Intelligence, 1)), [])
    ];

    public IReadOnlyList<CharacterClassDefinition> Classes { get; } =
    [
        new("alchemist"),
        new("barbarian"),
        new("bard"),
        new("fighter"),
        new("sorcerer"),
        new("warlock"),
        new("druid"),
        new("cleric"),
        new("wizard"),
        new("monster-hunter"),
        new("rogue")
    ];

    public IReadOnlyDictionary<Ability, int> ResolveAbilityBonuses(
        string ancestryId,
        string? subraceId,
        IReadOnlyList<Ability> choices)
    {
        var ancestry = Ancestries.SingleOrDefault(option => option.Id == ancestryId)
            ?? throw new ArgumentException("Unknown ancestry.", nameof(ancestryId));
        var bonuses = new Dictionary<Ability, int>(ancestry.Bonuses);

        if (ancestry.Subraces.Count > 0)
        {
            var subrace = ancestry.Subraces.SingleOrDefault(option => option.Id == subraceId)
                ?? throw new ArgumentException("Choose a valid subrace.", nameof(subraceId));
            AddBonuses(bonuses, subrace.Bonuses);
            ApplyChoices(bonuses, subrace.ChoiceRule, choices);
        }
        else if (subraceId is not null)
        {
            throw new ArgumentException("This ancestry has no subraces.", nameof(subraceId));
        }

        if (ancestry.ChoiceRule is not null)
        {
            ApplyChoices(bonuses, ancestry.ChoiceRule, choices);
        }
        else if (ancestry.Subraces.Count == 0 && choices.Count > 0)
        {
            throw new ArgumentException("This ancestry has no ability choices.", nameof(choices));
        }

        return bonuses;
    }

    private static IReadOnlyDictionary<Ability, int> Bonuses(params (Ability Ability, int Value)[] bonuses) =>
        bonuses.ToDictionary(bonus => bonus.Ability, bonus => bonus.Value);

    private static void ApplyChoices(
        IDictionary<Ability, int> bonuses,
        AbilityChoiceRule? rule,
        IReadOnlyList<Ability> choices)
    {
        if (rule is null)
        {
            if (choices.Count > 0)
            {
                throw new ArgumentException("No ability choices are available.", nameof(choices));
            }

            return;
        }

        if (choices.Count != rule.Count || choices.Distinct().Count() != rule.Count ||
            choices.Any(ability => !rule.AllowedAbilities.Contains(ability)))
        {
            throw new ArgumentException("The selected ability bonuses are invalid.", nameof(choices));
        }

        foreach (var ability in choices)
        {
            bonuses[ability] = bonuses.TryGetValue(ability, out var current) ? current + 1 : 1;
        }
    }

    private static void AddBonuses(IDictionary<Ability, int> target, IReadOnlyDictionary<Ability, int> source)
    {
        foreach (var (ability, value) in source)
        {
            target[ability] = target.TryGetValue(ability, out var current) ? current + value : value;
        }
    }
}