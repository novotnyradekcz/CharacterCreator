namespace CharacterCreator.Core;

public sealed record DiceRollRule(int DiceCount, int DieSides, int DropLowest, int RollCount);

public sealed record AbilityRoll(int Id, IReadOnlyList<int> Dice, int Total);

public static class AbilityScoreRoller
{
    public static IReadOnlyList<AbilityRoll> Roll(DiceRollRule rule, Random? random = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rule.DiceCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rule.DieSides);
        ArgumentOutOfRangeException.ThrowIfNegative(rule.DropLowest);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rule.RollCount);

        if (rule.DropLowest >= rule.DiceCount)
        {
            throw new ArgumentException("The number of discarded dice must be lower than the number rolled.", nameof(rule));
        }

        random ??= Random.Shared;

        return Enumerable.Range(0, rule.RollCount)
            .Select(id =>
            {
                var dice = Enumerable.Range(0, rule.DiceCount)
                    .Select(_ => random.Next(1, rule.DieSides + 1))
                    .ToArray();
                var total = dice.Order().Skip(rule.DropLowest).Sum();
                return new AbilityRoll(id, dice, total);
            })
            .ToArray();
    }
}