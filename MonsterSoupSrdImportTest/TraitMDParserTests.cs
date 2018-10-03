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
                var traitTestData = monster.Traits[traitName];

                return new MonsterTrait
                {
                    Name = traitName,
                    Requirements = traitTestData.Requirements,
                    Replaces = traitTestData.ExpectedArgsOutput,
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
                    ***Damage Transfer.*** While attached to a creature, the cloaker takes only half the damage dealt to it (rounded down), and that creature takes the other half.
                    ***False Appearance.*** While the cloaker remains motionless without its underside exposed, it is indistinguishable from a dark leather cloak.
                    ***Light Sensitivity.*** While in bright light, the cloaker has disadvantage on attack rolls and Wisdom (Perception) checks that rely on sight.",
                new[] { "Damage Transfer", "False Appearance", "Light Sensitivity" }
            );

            // Rug of Smothering
            yield return TestMonster(
                new RugOfSmothering(), @"
                    ***Antimagic Susceptibility.*** The rug is incapacitated while in the area of an *antimagic field.* If targeted by *dispel magic*, the rug must succeed on a Constitution saving throw against the caster’s spell save DC or fall unconscious for 1 minute.
                    ***Damage Transfer.*** While it is grappling a creature, the rug takes only half the damage dealt to it, and the creature grappled by the rug takes the other half.
                    ***False Appearance.*** While the rug remains motionless, it is indistinguishable from a normal rug.",
                new[] { "Antimagic Susceptibility", "Damage Transfer", "False Appearance" }
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

            // Deep Gnome
            yield return TestMonster(
                new DeepGnome(), @"
                    ***Stone Camouflage.*** The gnome has advantage on Dexterity (Stealth) checks made to hide in rocky terrain.
                    ***Gnome Cunning.*** The gnome has advantage on Intelligence, Wisdom, and Charisma saving throws against magic.",
                new[] { "Stone Camouflage", "Gnome Cunning" }
            );

            // Goblin
            yield return TestMonster(
                new Goblin(), @"
                    ***Nimble Escape.*** The goblin can take the Disengage or Hide action as a bonus action on each of its turns.",
                new[] { "Nimble Escape" }
            );

            // Gorgon
            yield return TestMonster(
                new Gorgon(), @"
***Trampling Charge.*** If the gorgon moves at least 20 feet straight toward a creature and then hits it with a gore attack on the same turn, that target must succeed on a DC 16 Strength saving throw or be knocked prone.
If the target is prone, the gorgon can make one attack with its hooves against it as a bonus action.",
                new[] { "Trampling Charge" }
            );

            // Triceratops
            yield return TestMonster(
                new Triceratops(), @"
***Trampling Charge.*** If the triceratops moves at least 20 feet straight toward a creature and then hits it with a gore attack on the same turn, that target must succeed on a DC 13 Strength saving throw or be knocked prone.
If the target is prone, the triceratops can make one stomp attack against it as a bonus action.",
                new[] { "Trampling Charge" }
            );

            // Griffon
            yield return TestMonster(
                new Griffon(), @"
                    ***Keen Sight.*** The griffon has advantage on Wisdom (Perception) checks that rely on sight.",
                new[] { "Keen Sight" }
            );

            // Grimlock
            yield return TestMonster(
                new Grimlock(), @"
                    ***Blind Senses.*** The grimlock can’t use its blindsight while deafened and unable to smell.
                    ***Keen Hearing and Smell.*** The grimlock has advantage on Wisdom (Perception) checks that rely on hearing or smell.
                    ***Stone Camouflage.*** The grimlock has advantage on Dexterity (Stealth) checks made to hide in rocky terrain.",
                new[] { "Blind Senses", "Keen Hearing and Smell", "Stone Camouflage" }
            );

            // Hell Hound
            yield return TestMonster(
                new HellHound(), @"
                    ***Keen Hearing and Smell.*** The hound has advantage on Wisdom (Perception) checks that rely on hearing or smell.
                    ***Pack Tactics.*** The hound has advantage on an attack roll against a creature if at least one of the hound’s allies is within 5 feet of the creature and the ally isn’t incapacitated.",
                new[] { "Keen Hearing and Smell", "Pack Tactics" }
            );

            // Hobgoblin
            yield return TestMonster(
                new Hobgoblin(), @"
                    ***Martial Advantage.*** Once per turn, the hobgoblin can deal an extra 7 (2d6) damage to a creature it hits with a weapon attack if that creature is within 5 feet of an ally of the hobgoblin that isn’t incapacitated.",
                new[] { "Martial Advantage" }
            );

            // SHAPECHANGERS //

            // Mimic
            yield return TestMonster(
                new Mimic(), @"
                    ***Shapechanger.*** The mimic can use its action to polymorph into an object or back into its true, amorphous form. Its statistics are the same in each form. Any equipment it is wearing or carrying isn’t transformed. It reverts to its true form if it dies.
                    ***Adhesive (Object Form Only).*** The mimic adheres to anything that touches it. A Huge or smaller creature adhered to the mimic is also grappled by it (escape DC 13). Ability checks made to escape this grapple have disadvantage.
                    ***False Appearance (Object Form Only).*** While the mimic remains motionless, it is indistinguishable from an ordinary object.
                    ***Grappler.*** The mimic has advantage on attack rolls against any creature grappled by it.",
                new[] { "Shapechanger", "Adhesive", "False Appearance", "Grappler" }
            );

            // Lycanthrope
            yield return TestMonster(
                new Werewolf(), @"
                    ***Shapechanger.*** The werewolf can use its action to polymorph into a wolf-humanoid hybrid or into a wolf, or back into its true form, which is humanoid. Its statistics, other than its AC, are the same in each form. Any equipment it is wearing or carrying isn’t transformed. It reverts to its true form if it dies.",
                new[] { "Shapechanger" }
            );

            yield return TestMonster(
                new Wereboar(), @"
                    ***Shapechanger.*** The wereboar can use its action to polymorph into a boar-humanoid hybrid or into a boar, or back into its true form, which is humanoid. Its statistics, other than its AC, are the same in each form. Any equipment it is wearing or carrying isn’t transformed. It reverts to its true form if it dies.
                    ***Charge (Boar or Hybrid Form Only).*** If the wereboar moves at least 15 feet straight toward a target and then hits it with its tusks on the same turn, the target takes an extra 7 (2d6) slashing damage. If the target is a creature, it must succeed on a DC 13 Strength saving throw or be knocked prone.
                    ***Relentless (Recharges after a Short or Long Rest).*** If the wereboar takes 14 damage or less that would reduce it to 0 hit points, it is reduced to 1 hit point instead.",
                new[] { "Shapechanger", "Charge", "Relentless" }
            );

            // Vampire
            yield return TestMonster(
                new Vampire(), @"
                    ***Shapechanger.*** If the vampire isn’t in sunlight or running water, it can use its action to polymorph into a Tiny bat or a Medium cloud of mist, or back into its true form.\r\nWhile in bat form, the vampire can’t speak, its walking speed is 5 feet, and it has a flying speed of 30 feet. Its statistics, other than its size and speed, are unchanged. Anything it is wearing transforms with it, but nothing it is carrying does. It reverts to its true form if it dies.\r\nWhile in mist form, the vampire can’t take any actions, speak, or manipulate objects. It is weightless, has a flying speed of 20 feet, can hover, and can enter a hostile creature’s space and stop there. In addition, if air can pass through a space, the mist can do so without squeezing, and it can’t pass through water. It has advantage on Strength, Dexterity, and Constitution saving throws, and it is immune to all nonmagical damage, except the damage it takes from sunlight.
                    ***Misty Escape - Vampire.*** When it drops to 0 hit points outside its resting place, the vampire transforms into a cloud of mist (as in the Shapechanger trait) instead of falling unconscious, provided that it isn’t in sunlight or running water. If it can’t transform, it is destroyed.\r\nWhile it has 0 hit points in mist form, it can’t revert to its vampire form, and it must reach its resting place within 2 hours or be destroyed. Once in its resting place, it reverts to its vampire form. It is then paralyzed until it regains at least 1 hit point. After spending 1 hour in its resting place with 0 hit points, it regains 1 hit point.
                    ***Regeneration.*** The vampire regains 20 hit points at the start of its turn if it has at least 1 hit point and isn’t in sunlight or running water. If the vampire takes radiant damage or damage from holy water, this trait doesn’t function at the start of the vampire’s next turn.
                    ***Spider Climb.*** The vampire can climb difficult surfaces, including upside down on ceilings, without needing to make an ability check.
                    ***Vampire Weaknesses.*** The vampire has the following flaws:\r\n*Forbiddance.* The vampire can’t enter a residence without an invitation from one of the occupants.\r\n*Harmed by Running Water.* The vampire takes 20 acid damage if it ends its turn in running water.\r\n*Stake to the Heart.* If a piercing weapon made of wood is driven into the vampire’s heart while the vampire is incapacitated in its resting place, the vampire is paralyzed until the stake is removed.\r\n*Sunlight Hypersensitivity.* The vampire takes 20 radiant damage when it starts its turn in sunlight. While in sunlight, it has disadvantage on attack rolls and ability checks.",
                new[] { "Shapechanger", "Misty Escape - Vampire", "Regeneration", "Spider Climb", "Vampire Weaknesses" }
            );

            // //SHAPECHANGERS //

            // Troll
            yield return TestMonster(
                new Troll(), @"
                    ***Keen Smell.*** The troll has advantage on Wisdom (Perception) checks that rely on smell.
                    ***Regeneration.*** The troll regains 10 hit points at the start of its turn. If the troll takes acid or fire damage, this trait doesn’t function at the start of the troll’s next turn. The troll dies only if it starts its turn with 0 hit points and doesn’t regenerate.",
                new[] { "Keen Smell", "Regeneration" }
            );

            // Hydra
            yield return TestMonster(
                new Hydra(), @"
***Hold Breath.*** The hydra can hold its breath for 1 hour.
***Multiple Heads.*** The hydra has five heads. While it has more than one head, the hydra has advantage on saving throws against being blinded, charmed, deafened, frightened, stunned, and knocked unconscious.
Whenever the hydra takes 25 or more damage in a single turn, one of its heads dies. If all its heads die, the hydra dies.
At the end of its turn, it grows two heads for each of its heads that died since its last turn, unless it has taken fire damage since its last turn. The hydra regains 10 hit points for each head regrown in this way.
***Reactive Heads.*** For each head the hydra has beyond one, it gets an extra reaction that can be used only for opportunity attacks.
***Wakeful.*** While the hydra sleeps, at least one of its heads is awake.",
                new[] { "Hold Breath", "Multiple Heads", "Reactive Heads", "Wakeful" }
            );

            // Homunculus
            yield return TestMonster(
                new Homunculus(), @"
                    ***Telepathic Bond.*** While the homunculus is on the same plane of existence as its master, it can magically convey what it senses to its master, and the two can communicate telepathically.",
                new[] { "Telepathic Bond" }
            );

            // Invisible Stalker
            yield return TestMonster(
                new InvisibleStalker(), @"
                    ***Invisibility.*** The stalker is invisible.
                    ***Faultless Tracker.*** The stalker is given a quarry by its summoner. The stalker knows the direction and distance to its quarry as long as the two of them are on the same plane of existence. The stalker also knows the location of its summoner.",
                new[] { "Invisibility", "Faultless Tracker" }
            );

            // Kraken
            yield return TestMonster(
                new Kraken(), @"
                    ***Amphibious.*** The kraken can breathe air and water.
                    ***Freedom of Movement.*** The kraken ignores difficult terrain, and magical effects can’t reduce its speed or cause it to be restrained. It can spend 5 feet of movement to escape from nonmagical restraints or being grappled.
                    ***Siege Monster.*** The kraken deals double damage to objects and structures.",
                new[] { "Amphibious", "Freedom of Movement", "Siege Monster" }
            );

            // Lich
            yield return TestMonster(
                new Lich(), @"
                    ***Rejuvenation.*** If it has a phylactery, a destroyed lich gains a new body in 1d10 days, regaining all its hit points and becoming active again. The new body appears within 5 feet of the phylactery.
                    ***Turn Resistance.*** The lich has advantage on saving throws against any effect that turns undead.",
                new[] { "Rejuvenation", "Turn Resistance" }
            );

            // Mummy Lord
            yield return TestMonster(
                new MummyLord(), @"
                    ***Magic Resistance.*** The mummy lord has advantage on saving throws against spells and other magical effects.
                    ***Rejuvenation.*** A destroyed mummy lord gains a new body in 24 hours if its heart is intact, regaining all its hit points and becoming active again. The new body appears within 5 feet of the mummy lord’s heart.",
                new[] { "Magic Resistance", "Rejuvenation" }
            );

            // Magmin
            yield return TestMonster(
                new Magmin(), @"
                    ***Death Burst.*** When the magmin dies, it explodes in a burst of fire and magma. Each creature within 10 feet of it must make a DC 11 Dexterity saving throw, taking 7 (2d6) fire damage on a failed save, or half as much damage on a successful one. Flammable objects that aren’t being worn or carried in that area are ignited.
                    ***Ignited Illumination.*** As a bonus action, the magmin can set itself ablaze or extinguish its flames. While ablaze, the magmin sheds bright light in a 10-foot radius and dim light for an additional 10 feet.",
                new[] { "Death Burst", "Ignited Illumination" }
            );

            // Mephits
            yield return TestMonster(
                new DustMephit(), @"
                    ***Death Burst.*** When the mephit dies, it explodes in a burst of dust. Each creature within 5 feet of it must then succeed on a DC 10 Constitution saving throw or be blinded for 1 minute. A blinded creature can repeat the saving throw on each of its turns, ending the effect on itself on a success.",
                new[] { "Death Burst" }
            );
            yield return TestMonster(
                new IceMephit(), @"
                    ***Death Burst.*** When the mephit dies, it explodes in a burst of jagged ice. Each creature within 5 feet of it must make a DC 10 Dexterity saving throw, taking 4 (1d8) slashing damage on a failed save, or half as much damage on a successful one.
                    ***False Appearance.*** While the mephit remains motionless, it is indistinguishable from an ordinary shard of ice.",
                new[] { "Death Burst", "False Appearance" }
            );
            yield return TestMonster(
                new SteamMephit(), @"
                    ***Death Burst.*** When the mephit dies, it explodes in a cloud of steam. Each creature within 5 feet of the mephit must succeed on a DC 10 Dexterity saving throw or take 4 (1d8) fire damage.",
                new[] { "Death Burst" }
            );

            // Rust Monster
            yield return TestMonster(
                new RustMonster(), @"
                    ***Iron Scent.*** The rust monster can pinpoint, by scent, the location of ferrous metal within 30 feet of it.
                    ***Rust Metal.*** Any nonmagical weapon made of metal that hits the rust monster corrodes. After dealing damage, the weapon takes a permanent and cumulative −1 penalty to damage rolls. If its penalty drops to −5, the weapon is destroyed. Nonmagical ammunition made of metal that hits the rust monster is destroyed after dealing damage.",
                new[] { "Iron Scent", "Rust Metal" }
            );

            // Sahaguin
            yield return TestMonster(
                new Sahuagin(), @"
                    ***Blood Frenzy.*** The sahuagin has advantage on melee attack rolls against any creature that doesn’t have all its hit points.
                    ***Limited Amphibiousness.*** The sahuagin can breathe air and water, but it needs to be submerged at least once every 4 hours to avoid suffocating.
                    ***Shark Telepathy.*** The sahuagin can magically command any shark within 120 feet of it, using a limited telepathy.",
                new[] { "Blood Frenzy", "Limited Amphibiousness", "Shark Telepathy" }
            );
        }
    }
}
