namespace CharacterCreator.Core;

public sealed record CharacterBuildRequest(
    string Name,
    string AncestryId,
    string? SubraceId,
    string ClassId,
    IReadOnlyList<AbilityRoll> Rolls,
    IReadOnlyDictionary<Ability, int> Assignments,
    IReadOnlyList<Ability> BonusChoices);

public sealed record CharacterSheet(
    string Name,
    string RulesetId,
    string AncestryId,
    string? SubraceId,
    string ClassId,
    IReadOnlyDictionary<Ability, int> BaseScores,
    IReadOnlyDictionary<Ability, int> Bonuses,
    IReadOnlyDictionary<Ability, int> Scores);

public sealed class CharacterBuildService(ICharacterRuleset ruleset)
{
    public CharacterSheet Build(CharacterBuildRequest request)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Name);

        if (!ruleset.Classes.Any(characterClass => characterClass.Id == request.ClassId))
        {
            throw new ArgumentException("Choose a valid class.", nameof(request));
        }

        var abilities = Enum.GetValues<Ability>();
        if (request.Rolls.Count != abilities.Length || request.Assignments.Count != abilities.Length ||
            abilities.Any(ability => !request.Assignments.ContainsKey(ability)))
        {
            throw new ArgumentException("Assign one roll to each ability.", nameof(request));
        }

        var assignedRollIds = request.Assignments.Values.ToArray();
        if (assignedRollIds.Distinct().Count() != abilities.Length)
        {
            throw new ArgumentException("Each roll can only be assigned once.", nameof(request));
        }

        var rollsById = request.Rolls.ToDictionary(roll => roll.Id);
        if (assignedRollIds.Any(id => !rollsById.ContainsKey(id)))
        {
            throw new ArgumentException("Assignments must refer to the current rolls.", nameof(request));
        }

        var baseScores = request.Assignments.ToDictionary(
            assignment => assignment.Key,
            assignment => rollsById[assignment.Value].Total);
        var bonuses = ruleset.ResolveAbilityBonuses(request.AncestryId, request.SubraceId, request.BonusChoices);
        var scores = abilities.ToDictionary(
            ability => ability,
            ability => baseScores[ability] + bonuses.GetValueOrDefault(ability));

        return new CharacterSheet(
            request.Name.Trim(),
            ruleset.Id,
            request.AncestryId,
            request.SubraceId,
            request.ClassId,
            baseScores,
            bonuses,
            scores);
    }
}