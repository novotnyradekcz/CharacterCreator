using CharacterCreator.Core;

namespace CharacterCreator.Tests;

public class CharacterRulesTests
{
    private readonly JaDRuleset _ruleset = new();

    [Fact]
    public void RollsSixAbilityScoresUsingFourDiceAndDroppingTheLowest()
    {
        var rolls = AbilityScoreRoller.Roll(_ruleset.AbilityScoreRule, new Random(42));

        Assert.Equal(6, rolls.Count);
        Assert.All(rolls, roll =>
        {
            Assert.Equal(4, roll.Dice.Count);
            Assert.All(roll.Dice, die => Assert.InRange(die, 1, 6));
            Assert.Equal(roll.Dice.Sum() - roll.Dice.Min(), roll.Total);
            Assert.InRange(roll.Total, 3, 18);
        });
    }

    [Fact]
    public void HumanCanChooseTwoDifferentAbilitiesForBonuses()
    {
        var bonuses = _ruleset.ResolveAbilityBonuses(
            "human",
            null,
            [Ability.Strength, Ability.Wisdom]);

        Assert.Equal(1, bonuses[Ability.Strength]);
        Assert.Equal(1, bonuses[Ability.Wisdom]);
        Assert.Throws<ArgumentException>(() => _ruleset.ResolveAbilityBonuses(
            "human",
            null,
            [Ability.Strength, Ability.Strength]));
    }

    [Fact]
    public void ElfSubraceAndChoiceBonusesAreCombined()
    {
        var bonuses = _ruleset.ResolveAbilityBonuses("elf", "blood", [Ability.Charisma]);

        Assert.Equal(2, bonuses[Ability.Dexterity]);
        Assert.Equal(1, bonuses[Ability.Charisma]);
        Assert.False(bonuses.ContainsKey(Ability.Intelligence));
    }

    [Fact]
    public void JadCatalogContainsEveryDocumentedAncestryAndClass()
    {
        Assert.Equal(
            ["human", "elf", "dwarf", "halfling", "half-orc", "gnome", "dragonborn", "tiefling"],
            _ruleset.Ancestries.Select(ancestry => ancestry.Id));
        Assert.Equal(
            ["alchemist", "barbarian", "bard", "fighter", "sorcerer", "warlock", "druid", "cleric", "wizard", "monster-hunter", "rogue"],
            _ruleset.Classes.Select(characterClass => characterClass.Id));
    }

    [Fact]
    public void JadAncestryAndSubraceBonusesMatchTheCatalog()
    {
        AssertBonuses("human", null, [Ability.Strength, Ability.Wisdom],
            (Ability.Strength, 1), (Ability.Wisdom, 1));
        AssertBonuses("elf", "wild", [], (Ability.Dexterity, 2), (Ability.Charisma, 1));
        AssertBonuses("elf", "high", [], (Ability.Dexterity, 2), (Ability.Intelligence, 1));
        AssertBonuses("elf", "night", [], (Ability.Dexterity, 2), (Ability.Wisdom, 1));
        AssertBonuses("elf", "blood", [Ability.Intelligence], (Ability.Dexterity, 2), (Ability.Intelligence, 1));
        AssertBonuses("dwarf", "underground", [], (Ability.Constitution, 2), (Ability.Wisdom, 1));
        AssertBonuses("dwarf", "stone", [], (Ability.Constitution, 2), (Ability.Strength, 2));
        AssertBonuses("dwarf", "light", [], (Ability.Constitution, 2), (Ability.Dexterity, 1), (Ability.Intelligence, 1));
        AssertBonuses("dwarf", "fire", [], (Ability.Constitution, 2), (Ability.Charisma, 1));
        AssertBonuses("halfling", null, [], (Ability.Dexterity, 2), (Ability.Charisma, 1));
        AssertBonuses("half-orc", null, [], (Ability.Strength, 2), (Ability.Constitution, 1));
        AssertBonuses("gnome", null, [], (Ability.Intelligence, 2), (Ability.Constitution, 1));
        AssertBonuses("dragonborn", null, [], (Ability.Strength, 2), (Ability.Charisma, 1));
        AssertBonuses("tiefling", null, [], (Ability.Charisma, 2), (Ability.Intelligence, 1));
    }

    [Fact]
    public void BuildUsesDistinctRollIdsAndAppliesAncestryBonuses()
    {
        var rolls = Enumerable.Range(0, 6)
            .Select(id => new AbilityRoll(id, [4, 4, 4, 4], 12))
            .ToArray();
        var assignments = Enum.GetValues<Ability>()
            .Select((ability, id) => (ability, id))
            .ToDictionary(item => item.ability, item => item.id);
        var service = new CharacterBuildService(_ruleset);

        var sheet = service.Build(new CharacterBuildRequest(
            "  Mira  ", "halfling", null, "bard", rolls, assignments, []));

        Assert.Equal("Mira", sheet.Name);
        Assert.Equal("jad-v1", sheet.RulesetId);
        Assert.Equal(14, sheet.Scores[Ability.Dexterity]);
        Assert.Equal(13, sheet.Scores[Ability.Charisma]);
        Assert.Equal(2, sheet.GetAbilityModifier(Ability.Dexterity));
        Assert.Equal(1, sheet.GetAbilityModifier(Ability.Charisma));
    }

    [Theory]
    [InlineData(3, -4)]
    [InlineData(8, -1)]
    [InlineData(9, -1)]
    [InlineData(10, 0)]
    [InlineData(11, 0)]
    [InlineData(12, 1)]
    [InlineData(17, 3)]
    [InlineData(18, 4)]
    [InlineData(20, 5)]
    public void AbilityModifiersRoundDownForFinalScores(int score, int expectedModifier)
    {
        var rolls = Enumerable.Range(0, 6)
            .Select(id => new AbilityRoll(id, [], score))
            .ToArray();
        var assignments = Enum.GetValues<Ability>()
            .Select((ability, id) => (ability, id))
            .ToDictionary(item => item.ability, item => item.id);
        var sheet = new CharacterBuildService(_ruleset).Build(new CharacterBuildRequest(
            "Mira", "halfling", null, "bard", rolls, assignments, []));

        Assert.Equal(expectedModifier, sheet.GetAbilityModifier(Ability.Strength));
    }

    [Theory]
    [InlineData("elf", "blood", Ability.Intelligence,
        new[] { 8, 11, 10, 12, 12, 13 }, new[] { -1, 0, 0, 1, 1, 1 })]
    [InlineData("dwarf", "stone", Ability.Strength,
        new[] { 10, 9, 12, 11, 12, 13 }, new[] { 0, -1, 1, 0, 1, 1 })]
    [InlineData("human", null, Ability.Wisdom,
        new[] { 9, 9, 10, 11, 13, 13 }, new[] { -1, -1, 0, 0, 1, 1 })]
    public void BuildShowsAllFinalScoresAndModifiersWithFixedAndChosenBonuses(
        string ancestryId, string? subraceId, Ability choice,
        int[] expectedScores, int[] expectedModifiers)
    {
        var totals = new[] { 8, 9, 10, 11, 12, 13 };
        var rolls = totals.Select((total, id) => new AbilityRoll(id, [], total)).ToArray();
        var assignments = Enum.GetValues<Ability>()
            .Select((ability, id) => (ability, id))
            .ToDictionary(item => item.ability, item => item.id);
        Ability[] choices = ancestryId switch
        {
            "elf" => [choice],
            "human" => [Ability.Strength, choice],
            _ => []
        };
        var sheet = new CharacterBuildService(_ruleset).Build(new CharacterBuildRequest(
            "Mira", ancestryId, subraceId, "bard", rolls, assignments, choices));

        Assert.Equal(6, sheet.Scores.Count);
        foreach (var (ability, rollId) in assignments)
        {
            Assert.Equal(totals[rollId], sheet.BaseScores[ability]);
            Assert.Equal(expectedScores[rollId], sheet.Scores[ability]);
            Assert.Equal(expectedModifiers[rollId], sheet.GetAbilityModifier(ability));
        }
    }

    private void AssertBonuses(
        string ancestryId,
        string? subraceId,
        IReadOnlyList<Ability> choices,
        params (Ability Ability, int Value)[] expected)
    {
        var actual = _ruleset.ResolveAbilityBonuses(ancestryId, subraceId, choices);

        Assert.Equal(expected.Length, actual.Count);
        foreach (var (ability, value) in expected)
        {
            Assert.Equal(value, actual[ability]);
        }
    }
}