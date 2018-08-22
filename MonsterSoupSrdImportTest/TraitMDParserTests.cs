using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterSoupSrdImport;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace MonsterSoupSrdImportTest
{
    [TestClass]
    public class TraitMDParserTests
    {
        [DataTestMethod, DynamicData(nameof(TemplateArgs_TestCases), DynamicDataSourceType.Method)]
        public void Should_CallArgExtractorExtractArgs_ForEachTrait(MonsterTestData monsterTestData, Monster monster, MonsterTrait[] expectedTraits)
        {
            var argExtractor = new Mock<IArgExtractor>();

            var traitParser = new TraitMDParser(argExtractor.Object);

            traitParser.ConvertTraits(monster);

            argExtractor
                .Verify(ae =>
                    ae.ExtractArgs(It.IsAny<string>(), It.IsAny<string>()),
                    Times.Exactly(monsterTestData.Traits.Count)
                );

            foreach (var trait in monsterTestData.Traits)
            {
                argExtractor
                    .Verify(ae =>
                        ae.ExtractArgs(trait.Value.TraitTemplate, trait.Value.MonsterTraitString),
                        Times.Once
                    );
            }
        }

        [DataTestMethod, DynamicData(nameof(TemplateArgs_TestCases), DynamicDataSourceType.Method)]
        public void Should_ParseTraitsProperly(MonsterTestData monsterTestData, Monster monster, MonsterTrait[] expectedTraits)
        {
            var traits = new TraitMDParser(new ArgExtractor()).ConvertTraits(monster);

            var matches = from t in traits
                          join et in expectedTraits on t.Name equals et.Name
                          select new { t, et };

            Assert.AreEqual(expectedTraits.Length, traits.Length);
            Assert.AreEqual(expectedTraits.Length, matches.Count());

            foreach (var match in matches)
            {
                var expectedTrait = match.et;
                var trait = match.t;

                var replaceMatches = from et in expectedTrait.Replaces
                                     join t in trait.Replaces on et.Key equals t.Key
                                     select new { et = et.Value, t = t.Value };

                Assert.AreEqual(expectedTrait.Replaces.Count, trait.Replaces.Count);
                Assert.AreEqual(expectedTrait.Replaces.Count, replaceMatches.Count());

                foreach (var replMatch in replaceMatches)
                {
                    var expectedReplace = replMatch.et;
                    var replace = replMatch.t;

                    Assert.AreEqual(expectedReplace.key, replace.key);
                    Assert.AreEqual(expectedReplace.argType, replace.argType);
                    Assert.That.ElementsAreEqual(expectedReplace.flags, replace.flags);
                    Assert.That.ValueObjectsAreEqual(expectedReplace.value, replace.value);
                }
            }
        }
        
        public static IEnumerable<object[]> TemplateArgs_TestCases()
        {
            // Test Setup Functions //

            object[] TestMonster(MonsterTestData monster, string whatsLeft, string[] traits)
            {
                return new object[]
                {
                    monster,
                    new Monster
                    {
                        Name = monster.GetType().Name,
                        WhatsLeft = whatsLeft,
                    },
                    traits.Select(traitName => TraitForMonster(monster, traitName)).ToArray(),
                };
            }

            MonsterTrait TraitForMonster(MonsterTestData monster, string traitName)
            {
                return new MonsterTrait
                {
                    Name = traitName,
                    Replaces = monster.Traits[traitName].ExpectedArgsOutput,
                };
            }

            // Test Data // 

            // Aboleth
            yield return TestMonster(
                new Aboleth(), @"
                    ***Amphibious.*** The aboleth can breathe air and water.
                    ***Mucous Cloud.*** While underwater, the aboleth is surrounded by transformative mucus. A creature that touches the aboleth or that hits it with a melee attack while within 5 feet of it must make a DC 14 Constitution saving throw. On a failure, the creature is diseased for 1d4 hours. The diseased creature can breathe only underwater.
                    ***Probing Telepathy.*** If a creature communicates telepathically with the aboleth, the aboleth learns the creature’s greatest desires if the aboleth can see the creature.",
                new[] { "Amphibious", "Mucous Cloud", "Probing Telepathy" }
            );
            
            // Bugbear
            yield return TestMonster(
                new Bugbear(), @"
                    ***Brute.*** A melee weapon deals one extra die of its damage when the bugbear hits with it (included in the attack).
                    ***Surprise Attack.*** If the bugbear surprises a creature and hits it with an attack during the first round of combat, the target takes an extra 7 (2d6) damage from the attack.",
                new[] { "Brute", "Surprise Attack" }
            );

            // Bulette
            yield return TestMonster(
                new Bulette(), @"
                    ***Standing Leap.*** The bulette’s long jump is up to 30 feet and its high jump is up to 15 feet, with or without a running start.",
                new[] { "Standing Leap" }
            );

            // Centaur
            yield return TestMonster(
                new Centaur(), @"
                    ***Charge.*** If the centaur moves at least 30 feet straight toward a target and then hits it with a pike attack on the same turn, the target takes an extra 10 (3d6) piercing damage.",
                new[] { "Charge" }
            );

            // Minotaur
            yield return TestMonster(
                new Minotaur(), @"
                    ***Charge.*** If the minotaur moves at least 10 feet straight toward a target and then hits it with a gore attack on the same turn, the target takes an extra 9 (2d8) piercing damage. If the target is a creature, it must succeed on a DC 14 Strength saving throw or be pushed up to 10 feet away and knocked prone.
                    ***Labyrinthine Recall.*** The minotaur can perfectly recall any path it has traveled.
                    ***Reckless.*** At the start of its turn, the minotaur can gain advantage on all melee weapon attack rolls it makes during that turn, but attack rolls against it have advantage until the start of its next turn.",
                new[] { "Charge", "Labyrinthine Recall", "Reckless" }
            );

            // Chuul
            yield return TestMonster(
                new Chuul(), @"
                    ***Amphibious.*** The chuul can breathe air and water.
                    ***Sense Magic.*** The chuul senses magic within 120 feet of it at will. This trait otherwise works like the *detect magic* spell but isn’t itself magical.",
                new[] { "Amphibious", "Sense Magic" }
            );

            // Cloaker
            yield return TestMonster(
                new Cloaker(), @"
                    ***Damage Transfer - Cloaker.*** While attached to a creature, the cloaker takes only half the damage dealt to it (rounded down), and that creature takes the other half.
                    ***False Appearance.*** While the cloaker remains motionless without its underside exposed, it is indistinguishable from a dark leather cloak.
                    ***Light Sensitivity.*** While in bright light, the cloaker has disadvantage on attack rolls and Wisdom (Perception) checks that rely on sight.",
                new[] { "Damage Transfer - Cloaker", "False Appearance", "Light Sensitivity" }
            );

            // Rug of Smothering
            yield return TestMonster(
                new RugOfSmothering(), @"
                    ***Antimagic Susceptibility.*** The rug is incapacitated while in the area of an *antimagic field.* If targeted by *dispel magic*, the rug must succeed on a Constitution saving throw against the caster’s spell save DC or fall unconscious for 1 minute.
                    ***Damage Transfer - Rug of Smothering.*** While it is grappling a creature, the rug takes only half the damage dealt to it, and the creature grappled by the rug takes the other half.
                    ***False Appearance.*** While the rug remains motionless, it is indistinguishable from a normal rug.",
                new[] { "Antimagic Susceptibility", "Damage Transfer - Rug of Smothering", "False Appearance" }
            );

            // Couatl
            yield return TestMonster(
                new Couatl(), @"
                    ***Magic Weapons.*** The couatl’s weapon attacks are magical.
                    ***Shielded Mind.*** The couatl is immune to scrying and to any effect that would sense its emotions, read its thoughts, or detect its location.",
                new[] { "Magic Weapons", "Shielded Mind" }
            );

            // Darkmantle
            yield return TestMonster(
                new Darkmantle(), @"
                    ***Echolocation.*** The darkmantle can’t use its blindsight while deafened.
                    ***False Appearance.*** While the darkmantle remains motionless, it is indistinguishable from a cave formation such as a stalactite or stalagmite.",
                new[] { "Echolocation", "False Appearance" }
            );

            // Drider
            yield return TestMonster(
                new Drider(), @"
                    ***Fey Ancestry.*** The drider has advantage on saving throws against being charmed, and magic can’t put the drider to sleep.
                    ***Spider Climb.*** The drider can climb difficult surfaces, including upside down on ceilings, without needing to make an ability check.
                    ***Sunlight Sensitivity.*** While in sunlight, the drider has disadvantage on attack rolls, as well as on Wisdom (Perception) checks that rely on sight.
                    ***Web Walker.*** The drider ignores movement restrictions caused by webbing.",
                new[] { "Fey Ancestry", "Spider Climb", "Sunlight Sensitivity", "Web Walker" }
            );

            // Ettercap
            yield return TestMonster(
                new Ettercap(), @"
                    ***Spider Climb.*** The ettercap can climb difficult surfaces, including upside down on ceilings, without needing to make an ability check.
                    ***Web Sense.*** While in contact with a web, the ettercap knows the exact location of any other creature in contact with the same web.
                    ***Web Walker.*** The ettercap ignores movement restrictions caused by webbing.",
                new[] { "Spider Climb", "Web Sense", "Web Walker" }
            );

            // Ettin
            yield return TestMonster(
                new Ettin(), @"
                    ***Two Heads.*** The ettin has advantage on Wisdom (Perception) checks and on saving throws against being blinded, charmed, deafened, frightened, stunned, and knocked unconscious.
                    ***Wakeful.*** When one of the ettin’s heads is asleep, its other head is awake.",
                new[] { "Two Heads", "Wakeful" }
            );

            // Ghost
            yield return TestMonster(
                new Ghost(), @"
                    ***Ethereal Sight.*** The ghost can see 60 feet into the Ethereal Plane when it is on the Material Plane, and vice versa.
                    ***Incorporeal Movement.*** The ghost can move through other creatures and objects as if they were difficult terrain. It takes 5 (1d10) force damage if it ends its turn inside an object.",
                new[] { "Ethereal Sight", "Incorporeal Movement" }
            );

            // Gibbering Mouther
            yield return TestMonster(
                new GibberingMouther(), @"
                    ***Aberrant Ground.*** The ground in a 10-foot radius around the mouther is doughlike difficult terrain. Each creature that starts its turn in that area must succeed on a DC 10 Strength saving throw or have its speed reduced to 0 until the start of its next turn.
                    ***Gibbering.*** The mouther babbles incoherently while it can see any creature and isn’t incapacitated. Each creature that starts its turn within 20 feet of the mouther and can hear the gibbering must succeed on a DC 10 Wisdom saving throw. On a failure, the creature can’t take reactions until the start of its next turn and rolls a d8 to determine what it does during its turn. On a 1 to 4, the creature does nothing. On a 5 or 6, the creature takes no action or bonus action and uses all its movement to move in a randomly determined direction. On a 7 or 8, the creature makes a melee attack against a randomly determined creature within its reach or does nothing if it can’t make such an attack.",
                new[] { "Aberrant Ground", "Gibbering" }
            );

            // Gnoll
            yield return TestMonster(
                new Gnoll(), @"
                    ***Rampage.*** When the gnoll reduces a creature to 0 hit points with a melee attack on its turn, the gnoll can take a bonus action to move up to half its speed and make a bite attack.",
                new[] { "Rampage" }
            );
        }
    }
}
