namespace CharacterCreator.Core;

public sealed record AbilityChoiceRule(int Count, IReadOnlyList<Ability> AllowedAbilities);

public sealed record SubraceDefinition(
    string Id,
    IReadOnlyDictionary<Ability, int> Bonuses,
    AbilityChoiceRule? ChoiceRule = null);

public sealed record AncestryDefinition(
    string Id,
    IReadOnlyDictionary<Ability, int> Bonuses,
    IReadOnlyList<SubraceDefinition> Subraces,
    AbilityChoiceRule? ChoiceRule = null);

public sealed record CharacterClassDefinition(string Id);

public interface ICharacterRuleset
{
    string Id { get; }

    string Name { get; }

    DiceRollRule AbilityScoreRule { get; }

    IReadOnlyList<AncestryDefinition> Ancestries { get; }

    IReadOnlyList<CharacterClassDefinition> Classes { get; }

    IReadOnlyDictionary<Ability, int> ResolveAbilityBonuses(
        string ancestryId,
        string? subraceId,
        IReadOnlyList<Ability> choices);
}