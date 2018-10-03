using System.Collections.Generic;
using System.Linq;

namespace MonsterSoupSrdImport
{
    public class TraitTemplates
    {
        public static readonly Dictionary<string, Trait> StandardTraits = new Trait[]
        {
            // from Aboleth
            new Trait
            {
                Name = "Amphibious",
                Template = "{ShortName} can breathe air and water."
            },
            new Trait
            {
                Name = "Mucous Cloud",
                Template =
                "While underwater, {shortName} is surrounded by transformative mucus. " +
                "A creature that touches {shortName} or that hits it with a melee attack while " +
                "within 5 feet of it must make a {save:SavingThrow}. On a failure, " +
                "the creature is diseased for {diceRoll:DiceRoll} hours. The diseased creature can breathe only " +
                "underwater."
            },
            new Trait
            {
                Name = "Probing Telepathy",
                Template =
                "If a creature communicates telepathically with {shortName}, {shortName} learns " +
                "the creature’s greatest desires if {shortName} can see the creature."
            },

            // from Bugbear
            new Trait
            {
                Name = "Brute",
                Template =
                "A melee weapon deals one extra die of its damage when {shortName} hits with it " +
                "(included in the attack)."
            },
            new Trait
            {
                Name = "Surprise Attack",
                Template =
                "If {shortName} surprises a creature and hits it with an attack during the " +
                "first round of combat, the target takes an extra {damage:Damage} from the attack."
            },

            // from Bulette
            new Trait
            {
                Name = "Standing Leap",
                Template =
                "{ShortName}’s long jump is up to {longJump:Number} feet and its high jump is up " +
                "to {highJump:Number} feet, with or without a running start."
            },

            // from Minotaur
            new Trait
            {
                Name = "Charge",
                Template =
                "If {shortName} moves at least {distance:Number} feet straight toward a target and " +
                "then hits it with {anAttack:Attack} " +
                "on the same turn, the target takes an extra {damage:Damage:Typed}." +
                "{hasSavingThrow:YesNo}[hasSavingThrow=Yes  If the target is a creature, it must " +
                "succeed on a {save:SavingThrow} or be {affected:MultiOption}.]"
            },
            new Trait
            {
                Name = "Labyrinthine Recall",
                Template =
                "{ShortName} can perfectly recall any path it has traveled."
            },
            new Trait
            {
                Name = "Reckless",
                Template =
                "At the start of its turn, {shortName} can gain advantage on all " +
                "melee weapon attack rolls it makes during that turn, but attack rolls against it " +
                "have advantage until the start of its next turn."
            },

            // from Chuul
            new Trait
            {
                Name = "Sense Magic",
                Template =
                "{ShortName} senses magic within {distance:Number} feet of it at will. This trait otherwise works like the " +
                "*detect magic* spell but isn’t itself magical."
            },

            // from Cloaker
            new Trait
            {
                Name = "Damage Transfer",
                Template = 
                "{template:Dropdown:[Cloaker,RugOfSmothering]}" +
                "[template=Cloaker While attached to a creature, {shortName} takes only half the damage dealt to it " +
                "(rounded down), and that creature takes the other half.]" +
                "[template=RugOfSmothering While it is grappling a creature, {shortName} takes only half the damage " +
                "dealt to it, and the creature grappled by {shortName} takes the other half.]"
            },
            new Trait
            {
                Name = "False Appearance",
                Template =
                "While {shortName} remains motionless{more:YesNo}[more=Yes  {moreRequirements:Text}], " +
                "it is indistinguishable from {description:Text}."
            },
            new Trait
            {
                Name = "Light Sensitivity",
                Template =
                "While in bright light, {shortName} has disadvantage on attack rolls " +
                "and Wisdom (Perception) checks that rely on sight."
            },

            // from Rug of Smothering
            new Trait
            {
                Name = "Antimagic Susceptibility",
                Template =
                "{ShortName} is incapacitated while in the area of an *antimagic field.* " +
                "If targeted by *dispel magic*, {shortName} must succeed on a Constitution saving throw " +
                "against the caster’s spell save DC or fall unconscious for 1 minute."
            },

            // from Couatl
            new Trait
            {
                Name = "Magic Weapons",
                Template =
                "{ShortName}’s weapon attacks are magical."
            },
            new Trait
            {
                Name = "Shielded Mind",
                Template =
                "{ShortName} is immune to scrying and to any effect that would sense its emotions, " +
                "read its thoughts, or detect its location."
            },

            // from Darkmantle
            new Trait
            {
                Name = "Echolocation",
                Template =
                "{ShortName} can’t use its blindsight while deafened."
            },

            // from Drider
            new Trait
            {
                Name = "Fey Ancestry",
                Template =
                "{ShortName} has advantage on saving throws against being charmed, and " +
                "magic can’t put {shortName} to sleep."
            },
            new Trait
            {
                Name = "Spider Climb",
                Template =
                "{ShortName} can climb difficult surfaces, including upside down on ceilings, " +
                "without needing to make an ability check."
            },
            new Trait
            {
                Name = "Sunlight Sensitivity",
                Template =
                "While in sunlight, {shortName} has disadvantage on attack rolls, as well as " +
                "on Wisdom (Perception) checks that rely on sight."
            },
            new Trait
            {
                Name = "Web Walker",
                Template =
                "{ShortName} ignores movement restrictions caused by webbing."
            },

            // from Ettercap
            new Trait
            {
                Name = "Web Sense",
                Template =
                "While in contact with a web, {shortName} knows the exact location of " +
                "any other creature in contact with the same web."
            },

            // from Ettin
            new Trait
            {
                Name = "Two Heads",
                Template =
                "{ShortName} has advantage on Wisdom (Perception) checks and on saving throws " +
                "against being blinded, charmed, deafened, frightened, stunned, and knocked unconscious."
            },
            new Trait
            {
                Name = "Wakeful",
                Template =
                "{template:Dropdown:[Ettin,Hydra]}" +
                "[template=Ettin When one of {shortName}’s heads is asleep, its other head is awake.]" +
                "[template=Hydra While {shortName} sleeps, at least one of its heads is awake.]"
            },

            // from Ghost
            new Trait
            {
                Name = "Ethereal Sight",
                Template =
                "{ShortName} can see 60 feet into the Ethereal Plane when it is " +
                "on the Material Plane, and vice versa."
            },
            new Trait
            {
                Name = "Incorporeal Movement",
                Template =
                "{ShortName} can move through other creatures and objects as if they were difficult terrain. " +
                "It takes {damage:Damage:Typed} if it ends its turn inside an object."
            },

            // from Gibbering Mouther
            new Trait
            {
                Name = "Aberrant Ground",
                Template =
                "The ground in a {radius:Number}-foot radius around {shortName} is doughlike difficult terrain. " +
                "Each creature that starts its turn in that area must succeed on a {save:SavingThrow} or " +
                "have its speed reduced to 0 until the start of its next turn."
            },
            new Trait
            {
                Name = "Gibbering",
                Template =
                "{ShortName} babbles incoherently while it can see any creature and isn’t incapacitated. " +
                "Each creature that starts its turn within {distance:Number} feet of {shortName} and " +
                "can hear the gibbering must succeed on a {save:SavingThrow}. On a failure, the creature " +
                "can’t take reactions until the start of its next turn and rolls a d8 to determine what it does " +
                "during its turn. On a 1 to 4, the creature does nothing. On a 5 or 6, the creature takes " +
                "no action or bonus action and uses all its movement to move in a randomly determined direction. " +
                "On a 7 or 8, the creature makes a melee attack against a randomly determined creature within " +
                "its reach or does nothing if it can’t make such an attack."
            },

            // from Gnoll
            new Trait
            {
                Name = "Rampage",
                Template =
                "When {shortName} reduces a creature to 0 hit points with a melee attack on its turn, " +
                "{shortName} can take a bonus action to move up to half its speed and make {anAttack:Attack}."
            },

            // from Deep Gnome
            new Trait
            {
                Name = "Stone Camouflage",
                Template =
                "{ShortName} has advantage on Dexterity (Stealth) checks made to hide in rocky terrain."
            },
            new Trait
            {
                Name = "Gnome Cunning",
                Template =
                "{ShortName} has advantage on Intelligence, Wisdom, and Charisma saving throws against magic."
            },

            // from Goblin
            new Trait
            {
                Name = "Nimble Escape",
                Template =
                "{ShortName} can take the Disengage or Hide action as a bonus action on each of its turns."
            },

            // from Gorgon
            new Trait
            {
                Name = "Trampling Charge",
                Template =
                "If {shortName} moves at least {distance:Number} feet straight toward a creature and then " +
                "hits it with {anAttack:Attack} on the same turn, that target must succeed on a " +
                "{save:SavingThrow} or be knocked prone.\r\n" +

                "If the target is prone, {shortName} can make " +
                "one {extraAttack:Attack} against it as a bonus action."
            },

            // from Griffon
            new Trait
            {
                Name = "Keen Sight",
                Template =
                "{ShortName} has advantage on Wisdom (Perception) checks that rely on sight."
            },

            // from Grimlock
            new Trait
            {
                Name = "Blind Senses",
                Template =
                "{ShortName} can’t use its blindsight while deafened and unable to smell."
            },
            new Trait
            {
                Name = "Keen Hearing and Smell",
                Template =
                "{ShortName} has advantage on Wisdom (Perception) checks that rely on hearing or smell."
            },

            // from Hell Hound
            new Trait
            {
                Name = "Pack Tactics",
                Template =
                "{ShortName} has advantage on an attack roll against a creature if at least one of " +
                "{shortName}’s allies is within 5 feet of the creature and the ally isn’t incapacitated."
            },

            // from Hobgoblin
            new Trait
            {
                Name = "Martial Advantage",
                Template =
                "Once per turn, {shortName} can deal an extra {damage:Damage} to a creature it hits with " +
                "a weapon attack if that creature is within 5 feet of an ally of {shortName} that isn’t " +
                "incapacitated."
            },
            
            // from Vampire
            new Trait
            {
                Name = "Misty Escape - Vampire",
                Template =
                "When it drops to 0 hit points outside its resting place, {shortName} transforms into a " +
                "cloud of mist (as in the Shapechanger trait) instead of falling unconscious, provided that " +
                "it isn’t in sunlight or running water. If it can’t transform, it is destroyed.\r\n" +

                "While it has 0 hit points in mist form, it can’t revert to its {regularForm:Text} form, and it must " +
                "reach its resting place within 2 hours or be destroyed. Once in its resting place, it reverts " +
                "to its {regularForm:Text} form. It is then paralyzed until it regains at least 1 hit point. " +
                "After spending 1 hour in its resting place with 0 hit points, it regains 1 hit point."
            },
            new Trait
            {
                Name = "Regeneration",
                Template =
                "{ShortName} regains {amount:Number} hit points at the start of its turn" +
                "{hasConditions:YesNo}[hasConditions=Yes  if it {conditions:MultiOption}]." +
                "{canBeShutOff:YesNo}[canBeShutOff=Yes  If {shortName} takes {damage:Text}, this trait doesn’t " +
                "function at the start of {shortName}’s next turn.]" +
                "{hasExtraDeathCondition:YesNo}[hasExtraDeathCondition=Yes  {ShortName} dies only if it starts its turn " +
                "with 0 hit points and doesn’t regenerate.]"
            },
            new Trait
            {
                Name = "Vampire Weaknesses",
                Template =
                "{ShortName} has the following flaws:\r\n" +

                "*Forbiddance.* {ShortName} can’t enter a residence without an invitation from one of the occupants.\r\n" +

                "*Harmed by Running Water.* {ShortName} takes 20 acid damage {ifWhen:Dropdown:[if,when]}[ifWhen=if if][ifWhen=when when] it ends its turn in running water.\r\n" +

                "*Stake to the Heart.* {stakeEffect:Dropdown:[Destroys,Paralyzes]}" +
                    "[stakeEffect=Paralyzes If a piercing weapon made of wood is driven into {shortName}’s heart while " +
                    "{shortName} is incapacitated in its resting place, {shortName} is paralyzed until the stake is removed.\r\n]" +
                    "[stakeEffect=Destroys {ShortName} is destroyed if a piercing weapon made of wood is driven into its heart " +
                    "while it is incapacitated in its resting place.\r\n]" +

                "*Sunlight Hypersensitivity.* {ShortName} takes 20 radiant damage when it starts its turn in sunlight. " +
                "While in sunlight, it has disadvantage on attack rolls and ability checks."
            },

            // from Troll
            new Trait
            {
                Name = "Keen Smell",
                Template =
                "{ShortName} has advantage on Wisdom (Perception) checks that rely on smell."
            },

            // from Hydra
            new Trait
            {
                Name = "Hold Breath",
                Template = "{ShortName} can hold its breath for {duration:Text}."
            },
            new Trait
            {
                Name = "Multiple Heads",
                Template =
                "{ShortName} has {headCount:Text} heads. While it has more than one head, " +
                "{shortName} has advantage on saving throws against being blinded, charmed, " +
                "deafened, frightened, stunned, and knocked unconscious.\r\n" +

                "Whenever {shortName} takes {minimumDamage:Number} or more damage in a " +
                "single turn, one of its heads dies. If all its heads die, the hydra dies.\r\n" +

                "At the end of its turn, it grows two heads for each of its heads that died " +
                "since its last turn, unless it has taken {damageType:Text} since its last turn. " +
                "{ShortName} regains {healAmount:Number} hit points for each head regrown " +
                "in this way."
            },
            new Trait
            {
                Name = "Reactive Heads",
                Template =
                "For each head {shortName} has beyond one, it gets an extra reaction that " +
                "can be used only for opportunity attacks."
            },

            // from Homunculus
            new Trait
            {
                Name = "Telepathic Bond",
                Template =
                "{template:Dropdown:[Homunculus,Succubus]}" +
                
                "[template=Homunculus While {shortName} is on the same plane of existence as its master, " +
                "it can magically convey what it senses to its master, and the two can " +
                "communicate telepathically.]" +

                "[template=Succubus {ShortName} ignores the range restriction on its telepathy when communicating " +
                "with a creature it has charmed. The two don’t even need to be on the same plane of existence.]"
            },

            // from Invisible Stalker
            new Trait
            {
                Name = "Invisibility",
                Template =
                "{ShortName} is invisible."
            },
            new Trait
            {
                Name = "Faultless Tracker",
                Template =
                "{ShortName} is given a quarry by its summoner. {ShortName} knows " +
                "the direction and distance to its quarry as long as the two of them " +
                "are on the same plane of existence. {ShortName} also knows the " +
                "location of its summoner."
            },

            // from Kraken
            new Trait
            {
                Name = "Freedom of Movement",
                Template =
                "{ShortName} ignores difficult terrain, and magical effects can’t " +
                "reduce its speed or cause it to be restrained. It can spend 5 feet " +
                "of movement to escape from nonmagical restraints or being grappled."
            },
            new Trait
            {
                Name = "Siege Monster",
                Template =
                "{ShortName} deals double damage to objects and structures."
            },

            // from Lich
            new Trait
            {
                Name = "Rejuvenation",
                Template =
                "{template:Dropdown:[Lich,MummyLord,Naga]}" +

                "[template=Lich If it has a phylactery, a destroyed {shortShortName} gains a new body in " +
                "{diceRoll:DiceRoll} days, regaining all its hit points and becoming active again. The new body " +
                "appears within 5 feet of the phylactery.]" +

                "[template=MummyLord A destroyed {shortShortName} gains a new body in 24 hours if its heart is " +
                "intact, regaining all its hit points and becoming active again. The new body appears within " +
                "5 feet of {shortName}’s heart.]" +
               
                "[template=Naga If it dies, {shortName} returns to life in {diceRoll:DiceRoll} days and regains " +
                "all its hit points. Only a *wish* spell can prevent this trait from functioning.]"
            },
            new Trait
            {
                Name = "Turn Resistance",
                Template =
                "{ShortName} has advantage on saving throws against any effect that turns undead."
            },

            // from Mummy Lord
            new Trait
            {
                Name = "Magic Resistance",
                Template =
                "{ShortName} has advantage on saving throws against spells and other magical effects."
            },

            // from Magmin
            new Trait
            {
                Name = "Death Burst",
                Template =
                "When {shortName} dies, it explodes in {anEffect:Text}. Each creature within {radius:Number} feet of " +
                "[saveType=NoDamage {shortName}][saveType!=NoDamage it] must " +
                "[saveType=HalfDamage make][saveType=NoDamage succeed on][saveType=Effect then succeed on] a " +
                "{save:SavingThrow}" +
                "{saveType:Dropdown:[NoDamage,HalfDamage,Effect]}" +
                //"[saveType=NoDamage|HalfDamage {ignitesObjects:YesNo}]" + //TODO: Nested Conditional Args
                "[saveType=NoDamage  or take {damage:Damage:Typed}.]" +
                "[saveType=HalfDamage , taking {damage:Damage:Typed} on a failed save, or half as much damage on " +
                "a successful one.]" +
                "[saveType=Effect  or be {affected:Text} for 1 minute. A {affected:Text} creature can repeat the " +
                "saving throw on each of its turns, ending the effect on itself on a success.]" +
                "{ignitesObjects:YesNo}" +
                "[ignitesObjects=Yes  Flammable objects that aren’t being worn or carried in that area are ignited.]"
            },
            new Trait
            {
                Name = "Ignited Illumination",
                Template =
                "As a bonus action, {shortName} can set itself ablaze or extinguish its flames. " +
                "While ablaze, {shortName} sheds bright light in a {radius:Number}-foot radius and " +
                "dim light for an additional {radius:Number} feet."
            },

            // from Manticore
            new Trait
            {
                Name = "Tail Spike Regrowth",
                Template =
                "{ShortName} has {amount:Text} tail spikes. Used spikes regrow when {shortName} finishes a long rest."
            },

            // from Medusa
            new Trait
            {
                Name = "Petrifying Gaze",
                Template =
                "When a creature that can see {shortName}’s eyes starts its turn within 30 feet of {shortName}, " +
                "{shortName} can force it to make a {save:SavingThrow} if {shortName} isn’t incapacitated and can " +
                "see the creature. If the saving throw fails by 5 or more, the creature is instantly petrified. " +
                "Otherwise, a creature that fails the save begins to turn to stone and is restrained. The restrained " +
                "creature must repeat the saving throw at the end of its next turn, becoming petrified on a failure " +
                "or ending the effect on a success. The petrification lasts until the creature is freed by the " +
                "*greater restoration* spell or other magic.\r\n" +

                "Unless surprised, a creature can avert its eyes to avoid the saving throw at the start of its turn. " +
                "If the creature does so, it can’t see {shortName} until the start of its next turn, when it can " +
                "avert its eyes again. If the creature looks at {shortName} in the meantime, it must immediately " +
                "make the save.\r\n" +

                "If {shortName} sees itself reflected on a polished surface within 30 feet of it and in an area of " +
                "bright light, {shortName} is, due to its curse, affected by its own gaze."
            },

            new Trait
            {
                Name = "Shapechanger",
                Template =
                "{template:Dropdown:[Doppelganger,Fiend,Lycanthrope,Mimic,Succubus,Vampire]}" +
                
                "[template!=Vampire "+
                    "{ShortName} can use its action to polymorph into " +

                    "[template=Doppelganger a Small or Medium humanoid it has seen, or back into its true form. " +
                    "Its statistics, other than its size, are the same in each form.]" +

                    "[template=Fiend a beast form that resembles {shapes:BeastShapes}, or back into its true form. " +
                    "Its statistics are the same in each form, except for the speed changes noted.]" +

                    "[template=Lycanthrope a {hybridType:Text}-humanoid hybrid or into a {animalType:Text}, " +
                    "or back into its true form, which is humanoid. Its statistics, other than its " +
                    "{statistics:MultiOption}, are the same in each form.]" +

                    "[template=Mimic an object or back into its true, amorphous form. " +
                    "Its statistics are the same in each form.]" +

                    "[template=Succubus a Small or Medium humanoid, or back into its true form. Without wings, " +
                    "{shortName} loses its flying speed. Other than its size and speed, its statistics are the " +
                    "same in each form.]" +
                    
                    " Any equipment it is wearing or carrying isn’t transformed. It reverts to its true form if it dies." +
                "]" +

                "[template=Vampire If {shortName} isn’t in sunlight or running water, it can use its action to polymorph " +
                "into a Tiny bat or a Medium cloud of mist, or back into its true form.\r\n" +

                "While in bat form, {shortName} can’t speak, its walking speed is 5 feet, and it has a " +
                "flying speed of 30 feet. Its statistics, other than its size and speed, are unchanged. " +
                "Anything it is wearing transforms with it, but nothing it is carrying does. It reverts " +
                "to its true form if it dies.\r\n" +

                "While in mist form, {shortName} can’t take any actions, speak, or manipulate objects. It is " +
                "weightless, has a flying speed of 20 feet, can hover, and can enter a hostile creature’s space " +
                "and stop there. In addition, if air can pass through a space, the mist can do so without " +
                "squeezing, and it can’t pass through water. It has advantage on Strength, Dexterity, and " +
                "Constitution saving throws, and it is immune to all nonmagical damage, except the damage it " +
                "takes from sunlight.]"
            },

            // from Mimic
            new Trait
            {
                Name = "Adhesive",
                Template =
                "{ShortName} adheres to anything that touches it. A Huge or smaller creature adhered to {shortName} is " +
                "also grappled by it (escape DC {dc:Number}). Ability checks made to escape this grapple have disadvantage."
            },
            new Trait
            {
                Name = "Grappler",
                Template =
                "{ShortName} has advantage on attack rolls against any creature grappled by it."
            },

            // from Wereboar
            new Trait
            {
                Name = "Relentless",
                Template =
                "If {shortName} takes {amount:Number} damage or less that would reduce it to 0 hit points, " +
                "it is reduced to 1 hit point instead."
            },

            // from Weretiger
            new Trait
            {
                Name = "Pounce",
                Template =
                "If {shortName} moves at least {distance:Number} feet straight " +
                "toward a creature and then hits it with {anAttack:Attack} on the same turn, that target must " +
                "succeed on a {save:SavingThrow} or be knocked prone.\r\n" +
                "If the target is prone, {shortName} can make one {extraAttack:Attack} against it as a bonus action."
            },

            // from Nightmare
            new Trait
            {
                Name = "Confer Fire Resistance",
                Template =
                "{ShortName} can grant resistance to fire damage to anyone riding it."
            },
            new Trait
            {
                Name = "Illumination",
                Template =
                "{ShortName} sheds bright light in a {radius:Number}-foot radius and dim light " +
                "{forIn:Dropdown:[for,in]}[forIn=for for][forIn=in in] an " +
                "additional {radius:Number} feet."
            },

            // from Orc
            new Trait
            {
                Name = "Aggressive",
                Template =
                "As a bonus action, {shortName} can move up to its speed toward a hostile creature " +
                "that it can see."
            },

            new Trait
            {
                Name = "Keen Sight and Smell",
                Template =
                "{ShortName} has advantage on Wisdom (Perception) checks that rely on sight or smell."
            },
            new Trait
            {
                Name = "Keen Senses",
                Template =
                "{ShortName} has advantage on Wisdom (Perception) checks that rely on sight, hearing, or smell."
            },

            // from Otyugh & Pseudeodragon
            new Trait
            {
                Name = "Limited Telepathy",
                Template =
                "{template:Dropdown:[Otyugh,Pseudodragon]}" +
                "[template=Otyugh {ShortName} can magically transmit simple messages and images to any creature " +
                "within {range:Number} feet of it that can understand a language. This form of telepathy doesn’t allow the " +
                "receiving creature to telepathically respond.]" +
                "[template=Pseudodragon {ShortName} can magically communicate simple ideas, emotions, and " +
                "images telepathically with any creature within {range:Number} feet of it that can understand a language.]"
            },

            // from Rakshasa
            new Trait
            {
                Name = "Limited Magic Immunity",
                Template =
                "{ShortName} can’t be affected or detected by spells of {level:Text} level or lower unless " +
                "it wishes to be. It has advantage on saving throws against all other spells and magical effects."
            },

            // from Remorhaz
            new Trait
            {
                Name = "Heated Body",
                Template =
                "A creature that touches {shortName} or hits it with a melee attack while within 5 feet of " +
                "it takes {damage:Damage:Typed}."
            },

            // from Roper
            new Trait
            {
                Name = "Grasping Tendrils",
                Template =
                "{ShortName} can have up to {count:Text} tendrils at a time. Each tendril can be attacked " +
                "{stats:ShortStats}. Destroying a tendril deals " +
                "no damage to {shortName}, which can extrude a replacement tendril on its next turn. " +
                "A tendril can also be broken if a creature takes an action and succeeds on " +
                "a DC {dc:Number} Strength check against it."
            },

            // from Rust Monster
            new Trait
            {
                Name = "Iron Scent",
                Template =
                "{ShortName} can pinpoint, by scent, the location of ferrous metal within {range:Number} feet of it."
            },
            new Trait
            {
                Name = "Rust Metal",
                Template =
                "Any nonmagical weapon made of metal that hits {shortName} corrodes. After dealing damage, " +
                "the weapon takes a permanent and cumulative −1 penalty to damage rolls. If its penalty drops " +
                "to −5, the weapon is destroyed. Nonmagical ammunition made of metal that hits {shortName} " +
                "is destroyed after dealing damage."
            },

            // from Sahuagin
            new Trait
            {
                Name = "Blood Frenzy",
                Template =
                "{ShortName} has advantage on melee attack rolls against any creature that " +
                "doesn’t have all its hit points."
            },
            new Trait
            {
                Name = "Limited Amphibiousness",
                Template =
                "{ShortName} can breathe air and water, but it needs to be submerged at least " +
                "once every {hours:Number} hours to avoid suffocating."
            },
            new Trait
            {
                Name = "Shark Telepathy",
                Template =
                "{ShortName} can magically command any shark within {range:Number} feet of it, " +
                "using a limited telepathy."
            },

            // from Salamander & Azer
            new Trait
            {
                Name = "Heated Weapons",
                Template =
                "{variant:Dropdown:[Azer,Salamander]}" +
                    "[variant=Azer When {shortName} hits with a]" +
                    "[variant=Salamander Any]" +
                " metal melee weapon" +
                    "[variant=Azer , it]" +
                    "[variant=Salamander  {shortName} wields]" +
                " deals an extra {damage:Damage:Typed} " +
                    "[variant=Salamander on a hit ]" +
                "(included in the attack).",
                AppliesEffects = new Dictionary<string, string> { { "toAttacks", "damage" } }
            },

            // from Shadow
            new Trait
            {
                Name = "Amorphous",
                Template =
                "{ShortName} can move through a space as narrow as 1 inch wide without squeezing."
            },
            new Trait
            {
                Name = "Shadow Stealth",
                Template =
                "While in dim light or darkness, {shortName} can take the Hide action as a bonus action."
            },
            new Trait
            {
                Name = "Sunlight Weakness",
                Template =
                "While in sunlight, {shortName} has disadvantage on attack rolls, ability checks, and saving throws."
            },

            // Shield Guardian
            new Trait
            {
                Name = "Bound",
                Template =
                "{ShortName} is magically bound to an amulet. As long as {shorterName:Text} and its amulet " +
                "are on the same plane of existence, the amulet’s wearer can telepathically call {shorterName:Text} " +
                "to travel to it, and {shorterName:Text} knows the distance and direction to the amulet. If " +
                "{shorterName:Text} is within {range:Number} feet of the amulet’s wearer, half of any damage the wearer takes " +
                "(rounded up) is transferred to {shorterName:Text}."
            },

            // Tarrasque
            new Trait
            {
                Name = "Reflective Carapace",
                Template =
                "Any time {shortName} is targeted by a *magic missile* spell, a line spell, or a spell that " +
                "requires a ranged attack roll, roll a d6. On a 1 to 5, {shortName} is unaffected. On a 6, " +
                "{shortName} is unaffected, and the effect is reflected back at the caster as though it " +
                "originated from {shortName}, turning the caster into the target."
            },

            // Will-o'-Wizsp
            new Trait
            {
                Name = "Consume Life",
                Template =
                "As a bonus action, {shortName} can target one creature it can see within 5 feet of it " +
                "that has 0 hit points and is still alive. The target must succeed on a {save:SavingThrow} " +
                "against this magic or die. If the target dies, {shortName} regains " +
                "{amount:DiceRoll} hit points."
            },
            new Trait
            {
                Name = "Ephemeral",
                Template =
                "{ShortName} can’t wear or carry anything."
            },
            new Trait
            {
                Name = "Variable Illumination",
                Template =
                "{ShortName} sheds bright light in a {min:Number}- to {max:Number}-foot radius and dim light for an " +
                "additional number of feet equal to the chosen radius. {ShortName} can alter the " +
                "radius as a bonus action."
            },
        }.OrderBy(kvp => kvp.Name).ToDictionary(kvp => kvp.Name);
    }
}