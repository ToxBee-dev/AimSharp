using System.Collections.Generic;
using System.Dynamic;
using ToxBeeDev;
using AimsharpWow.API;

namespace AimsharpWow.Modules
{

    public class Arms : Rotation            // <<<----- DON'T FORGET TO CHANGE THIS!!!
    {
        // Create Objects for Player and Target
        static Player Player = new Player();
        static Target Target = new Target();

        public override void LoadSettings()
        {
            // Aimsharp rotation settings are here
            Settings.Add(new Setting("Warrior: Arms by Bansaie"));          // <<<----- DON'T FORGET TO CHANGE THIS!!!
            Settings.Add(new Setting("Debugmode", false));
            Settings.Add(new Setting("Test on Dummy", false));
            Settings.Add(new Setting("Game Client Language", new List<string>() { "English", "Deutsch", "Español", "Français", "Italiano", "Português Brasileiro", "Русский", "한국어", "简体中文" }, "English"));

            Settings.Add(new Setting("Trinkets"));
            Settings.Add(new Setting("Use Top Trinket", false));
            Settings.Add(new Setting("Use Bottom Trinket", false));
            Settings.Add(new Setting("Top Trinket", new List<string>() { "Generic", "Friendly", "Self" }, "Generic"));
            Settings.Add(new Setting("Bottom Trinket", new List<string>() { "Generic", "Friendly", "Self" }, "Generic"));

            Settings.Add(new Setting("Interrupts"));
            Settings.Add(new Setting("Kick everything and ignore file", true));
            Settings.Add(new Setting("Random max", 50, 1500, 1087));
            Settings.Add(new Setting("Random min", 50, 1500, 543));

            Settings.Add(new Setting("Consumables"));
            Settings.Add(new Setting("Use Pot", false));
            Settings.Add(new Setting("Use Healing Potion", false));
            Settings.Add(new Setting("Use Mana Potion", false));
            Settings.Add(new Setting("Primary Potion", "Elemental Potion of Ultimate Power"));
            Settings.Add(new Setting("Health Potion", "Dreamwalker's Healing Potion"));
            Settings.Add(new Setting("Health Potion HP%", 1, 100, 20));
            Settings.Add(new Setting("Mana Potion", "Aerated Mana Potion"));
            Settings.Add(new Setting("Mana Potion Mana%", 1, 100, 80));
            Settings.Add(new Setting("Healthstone HP%", 1, 100, 50));

            Settings.Add(new Setting("Specialization Settings"));
            Settings.Add(new Setting("Bitter Immunity HP%", 1, 100, 60));
            Settings.Add(new Setting("Impending Victory HP%", 1, 100, 60));
            Settings.Add(new Setting("Die By the Sword HP%", 1, 100, 30));
            Settings.Add(new Setting("Ignore Pain HP%", 1, 100, 20));
            Settings.Add(new Setting("Rallying Cry HP%", 1, 100, 35));
            Settings.Add(new Setting("Battle Stance HP%", 1, 100, 60));
            Settings.Add(new Setting("Defensive Stance HP%", 1, 100, 50));
        }
        public override void Initialize()
        {
            // -------------- SETTINGS -----------------------------
            ToxBeeDev.Settings.ClientLanguage = GetDropDown("Game Client Language");
            ToxBeeDev.Settings.FolderName = "Bansaie_Warrior_Arms";                 // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.RotationName = "Warrior: Arms by Bansaie";   // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.Spec = "Warrior: Arms";                      // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.ClientVersion = "1.0";

            Helper.Initialize();

            // Erstelle ein Macro für Spear of Bastion mit wirken auf den Spieler selbst
            Helper.AddCastMacro("ChampionsSpearPlayer", "player", Helper.rootObject.GetStringById(376080));
            // Erstelle ein Macro für Ravager mit wirken auf den Spieler selbst
            Helper.AddCastMacro("Ravager", "player", Helper.rootObject.GetStringById(228920));
            // Erstelle ein Macro für den Pot, Healing Potion und Mana Potion
            // ItemCustom erstellen um die Items zu benutzen
            Helper.AddUseMacro("Pot", GetString("Primary Potion"));
            Helper.AddUseMacro("HealingPotion", GetString("Health Potion"));
            Helper.AddUseMacro("ManaPotion", GetString("Mana Potion"));

            // Add Spell, Buffs, Debuffs and co to Aimsharp Rotation List..
            foreach (string item in Helper.Spells_list)
            {
                Spellbook.Add(item);
            }

            foreach (string item in Helper.Talents_list)
            {
                Talents.Add(item);
            }

            foreach (string item in Helper.Items_list)
            {
                Items.Add(item);
            }

            foreach (string item in Helper.Buffs_list)
            {
                Buffs.Add(item);
            }

            foreach (string item in Helper.Debuffs_list)
            {
                Debuffs.Add(item);
            }

            foreach (string item in Helper.Totems_list)
            {
                Totems.Add(item);
            }

            foreach (string item in Helper.CustomCommands_list)
            {
                CustomCommands.Add(item);
            }

            // T-SetBonus Liste Befüllen
            Helper.SetBonus_list.Add(207182);   // Helm
            Helper.SetBonus_list.Add(207180);   // Shoulders
            Helper.SetBonus_list.Add(207185);   // Chest
            Helper.SetBonus_list.Add(207183);   // Gloves
            Helper.SetBonus_list.Add(207181);   // Legs

        }
        public override bool CombatTick()
        {
            // Das muss immer ganz oben stehen
            Player.Update();
            Target.Update();

            // Hier werden alle Spells, Buffs, Debuffs, Items und co. erstellt die in der Rotation später benutzt werden können
            #region Define Spells, Buffs, and Debuffs
            // Spell
            Spell Whirlwind = new Spell(1680, "player");
            Spell Rend = new Spell(772);
            Spell Execute = new Spell(163201);
            Spell Skullsplitter = new Spell(260643);
            Spell WreckingThrow = new Spell(384110);
            Spell ThunderClap = new Spell(6343, "player");
            Spell Slam = new Spell(1464);
            Spell MortalStrike = new Spell(12294);
            Spell HeroicThrow = new Spell(57755);

            // Cooldown
            Spell Avatar = new Spell(163249, "player");
            Spell SweepingStrikes = new Spell(260708, "player");
            Spell ColossusSmash = new Spell(208086, "player");
            Spell ChampionsSpear = new Spell(376080, "player");
            Spell Warbreaker = new Spell(262161, "player");
            Spell ThunderousRoar = new Spell(384318, "player");
            Spell Cleave = new Spell(845, "player");
            Spell Bladestorm = new Spell(389774, "player");
            Spell Overpower = new Spell(7384, "player");
            Spell Shockwave = new Spell(46968, "player");
            Spell DieByTheSword = new Spell(118038, "player");

            // SelfHeal
            Spell BitterImmunity = new Spell(383762, "player");
            Spell ImpendingVictory = new Spell(202168);
            Spell IgnorePain = new Spell(190456, "player");
            Spell RallyingCry = new Spell(97462, "player");
            Spell DefensiveStance = new Spell(386208, "player");
            Spell BattleStance = new Spell(386164, "player");

            // Racial
            Spell LightsJudgment = new Spell(255647, "player");
            Spell Berserking = new Spell(26297, "player");
            Spell BloodFury = new Spell(20572, "player");
            Spell BagOfTricks = new Spell(312411, "player");
            Spell Fireblood = new Spell(265221, "player");
            Spell ArcaneTorrent = new Spell(28730, "player");
            Spell AncestralCall = new Spell(274738, "player");

            // Buff
            Buff BuffAvatar = new Buff(163249);
            Buff BuffCollateralDamage = new Buff(334779);
            Buff BuffSuddenDeath = new Buff(52437);
            Buff BuffMartialProwess = new Buff(316440);
            Buff BuffJuggernaut = new Buff(383292);
            Buff BuffHurricane = new Buff(390563);
            Buff BuffMercilessBonegrinder = new Buff(383317);
            Buff BuffTestOfMight = new Buff(385008);
            Buff BuffBattleStance = new Buff(386164);
            Buff BuffDefensiveStance = new Buff(386208);

            // Talent's
            // Hurricane 390563
            // test_of_might  = 385008
            // Debuff
            Debuff DebuffColossusSmash = new Debuff(208086);
            Debuff DebuffColossusSmash1 = new Debuff(167105);
            Debuff DebuffRend = new Debuff(772);
            Debuff DebuffDeepWounds = new Debuff(262115);
            Debuff DebuffExecutionersPrecision = new Debuff(386634);

            // Trinkets
            Trinket TopTrinket = new Trinket(0, GetDropDown("Top Trinket"));
            Trinket BottomTrinket = new Trinket(1, GetDropDown("Bottom Trinket"));

            // Consumables
            ItemCustom Pot = new ItemCustom(GetString("Primary Potion"), "Pot", false);
            ItemCustom HealingPotion = new ItemCustom(GetString("Health Potion"), "HealingPotion", false);
            ItemCustom ManaPotion = new ItemCustom(GetString("Mana Potion"), "ManaPotion", false);
            ItemCustom Healthstone = new ItemCustom("Healthstone", "Healthstone", false);

            #endregion



            if (Helper.InFightCheck() || GetCheckBox("Test on Dummy"))
            {
                //-------- SELFHEAL ---------
                // Bitter Immunity
                BitterImmunity.UseDefensive(GetSlider("Bitter Immunity HP%"));
                // Impending Victory
                ImpendingVictory.UseDefensive(GetSlider("Impending Victory HP%"));
                // Die By the sword
                if (Player.Health <= 30) if (DieByTheSword.Cast()) return true;
                DieByTheSword.UseDefensive(GetSlider("Die By the Sword HP%"));
                // Ignore Pain
                IgnorePain.UseDefensive(GetSlider("Ignore Pain HP%"));
                // Rallying Cry
                RallyingCry.UseDefensive(GetSlider("Rallying Cry HP%")); // Auch Team Mitgliedern > 3/5 - 5/25 M+/Raid Falls HP unter 35% Kann er es Casten. FURY, Prot, Arns.
                                                                         // Battle Stance
                if (Player.Health >= GetSlider("Battle Stance HP%") && !BuffBattleStance.HasBuff()) if (BattleStance.Cast()) return true;
                // Defensive Stance
                if (Player.Health <= GetSlider("Defensive Stance HP%") && !BuffDefensiveStance.HasBuff()) if (DefensiveStance.Cast()) return true;

                //-------- CONSUMABLES ---------
                // Pot
                if (GetCheckBox("Use Pot") && BuffAvatar.HasBuff()) if (Pot.Use("Pot")) return true;
                // Healing Potion
                if (GetCheckBox("Use Healing Potion") && GetSlider("Health Potion HP%") <= Player.Health) if (HealingPotion.Use("HealingPotion")) return true;
                // Mana Potion
                if (GetCheckBox("Use Mana Potion") && GetSlider("Mana Potion Mana%") <= Player.Health) if (ManaPotion.Use("ManaPotion")) return true;
                // Healthstone
                if (GetSlider("Healthstone HP%") >= Player.Health) if (Healthstone.Use("Healthstone")) return true;

                if (Target.MeleeRange)
                {
                    //-------- INTERRUPTS ---------
                    // pummel,if=target.debuff.casting.react
                    if (!Helper.IsCustomCodeOn("NoInterrupts")) Helper.UseInterruptLogic(6552, Target, GetSlider("Random min"), GetSlider("Random max"));

                    //-------- TRINKETS ---------
                    // Trinket 1
                    if (GetCheckBox("Use Top Trinket") && !Helper.IsCustomCodeOn("NoCooldowns")) if (TopTrinket.useTrinket(BuffAvatar.HasBuff())) return true;
                    // Trinket 2
                    if (GetCheckBox("Use Bottom Trinket") && !Helper.IsCustomCodeOn("NoCooldowns")) if (BottomTrinket.useTrinket(BuffAvatar.HasBuff())) return true;

                    //-------------------- Rotation --------------------
                    // arcane_torrent,if=cooldown.mortal_strike.remains>1.5&rage<50
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && (MortalStrike.SpellCooldown() > 1500 && Player.Power < 50)) if (ArcaneTorrent.Cast()) return true;
                    // lights_judgment,if=debuff.colossus_smash.down&cooldown.mortal_strike.remains
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && (!DebuffColossusSmash.HasDebuff() || !DebuffColossusSmash1.HasDebuff() && MortalStrike.SpellCooldown() > 0)) if (LightsJudgment.Cast()) return true;
                    // bag_of_tricks,if=debuff.colossus_smash.down&cooldown.mortal_strike.remains
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && (!DebuffColossusSmash.HasDebuff() || !DebuffColossusSmash1.HasDebuff() && MortalStrike.SpellCooldown() > 0)) if (BagOfTricks.Cast()) return true;
                    // berserking,if=target.time_to_die>180&buff.avatar.up|target.time_to_die<180&(target.health.pct<35&talent.massacre|target.health.pct<20)&buff.avatar.up|target.time_to_die<20
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && (Target.TimeToDie > 180 && BuffAvatar.HasBuff() || Target.TimeToDie < 180 && (Target.Health < 35 && Helper.HasTalent(281001) || Target.Health < 20) && BuffAvatar.HasBuff() || Target.TimeToDie < 20)) if (Berserking.Cast()) return true;
                    // blood_fury,if=debuff.colossus_smash.up
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && DebuffColossusSmash.HasDebuff()) if (BloodFury.Cast()) return true;
                    // fireblood,if=debuff.colossus_smash.up
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && (DebuffColossusSmash.HasDebuff() || !DebuffColossusSmash1.HasDebuff())) if (Fireblood.Cast()) return true;
                    // ancestral_call,if=debuff.colossus_smash.up
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && (DebuffColossusSmash.HasDebuff() || !DebuffColossusSmash1.HasDebuff())) if (AncestralCall.Cast()) return true;
                    // whirlwind,if=buff.collateral_damage.up&cooldown.sweeping_strikes.remains<3
                    if (BuffCollateralDamage.HasBuff() && SweepingStrikes.SpellCooldown() < 3000) if (Whirlwind.Cast()) return true;
                    // sweeping_strikes,if=active_enemies>1
                    if (SweepingStrikes.Cast()) return true;
                    // mortal_strike,if=dot.rend.remains<=gcd&talent.bloodletting
                    if (DebuffRend.DebuffRemaining() <= Player.GCD && Helper.HasTalent(383154)) if (MortalStrike.Cast()) return true;
                    // rend,if=remains<=gcd&!talent.bloodletting&(!talent.warbreaker&cooldown.colossus_smash.remains<4|talent.warbreaker&cooldown.warbreaker.remains<4)&target.time_to_die>12
                    if (DebuffRend.DebuffRemaining() <= Player.GCD && !Helper.HasTalent(383154) && (!Helper.HasTalent(262161) && ColossusSmash.SpellCooldown() < 4000 || Helper.HasTalent(262161) && Warbreaker.SpellCooldown() < 4000) && Target.TimeToDie > 9) if (Rend.Cast()) return true;
                    // avatar,if=cooldown.colossus_smash.ready|debuff.colossus_smash.up|target.time_to_die<20
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && (ColossusSmash.SpellIsReady() || DebuffColossusSmash.HasDebuff() || !DebuffColossusSmash1.HasDebuff() || Target.TimeToDie < 20)) if (Avatar.Cast()) return true;
                    // champions_spear,if=cooldown.colossus_smash.remains<=gcd
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && ColossusSmash.SpellCooldown() <= Player.GCD) if (ChampionsSpear.CastMacro("ChampionsSpearPlayer")) return true;
                    // warbreaker,if=raid_event.adds.in>22
                    if (Warbreaker.Cast()) return true;
                    // colossus_smash
                    if(!Helper.IsCustomCodeOn("NoCooldowns")) if (ColossusSmash.Cast()) return true;
                    // execute,if=buff.sudden_death.react&dot.deep_wounds.remains
                    if (BuffSuddenDeath.HasBuff() && DebuffDeepWounds.DebuffRemaining() > 0) if (Execute.Cast()) return true;
                    // thunderous_roar,if=(talent.test_of_might&rage<40)|(!talent.test_of_might&(buff.avatar.up|debuff.colossus_smash.up)&rage<70)
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && ((Helper.HasTalent(385008) && Player.Power < 40) || (!Helper.HasTalent(385008) && (BuffAvatar.HasBuff() || DebuffColossusSmash.HasDebuff() || DebuffColossusSmash1.HasDebuff()) && Player.Power < 70))) if (ThunderousRoar.Cast()) return true;
                    // cleave,if=spell_targets.whirlwind>2&dot.deep_wounds.remains<=gcd
                    if (DebuffDeepWounds.DebuffRemaining() <= Player.GCD) if (Cleave.Cast()) return true;
                    // bladestorm,if=raid_event.adds.in>45&talent.hurricane&rage<40
                    if (Helper.HasTalent(390563) &&  Player.Power < 40) if (Bladestorm.Cast()) return true;
                    // mortal_strike,if=debuff.executioners_precision.stack=2&debuff.colossus_smash.remains<=gcd
                    if (DebuffExecutionersPrecision.DebuffStacks() == 2 && DebuffColossusSmash.DebuffRemaining() <= Player.GCD || DebuffColossusSmash1.DebuffRemaining() <= Player.GCD) if (MortalStrike.Cast()) return true;
                    // overpower,if=rage<40&buff.martial_prowess.stack<2
                    if (Player.Power < 40 && BuffMartialProwess.BuffStacks() < 2) if (Overpower.Cast()) return true;
                    // mortal_strike,if=debuff.executioners_precision.stack=2|!talent.executioners_precision&buff.martial_prowess.stack=2
                    if (DebuffExecutionersPrecision.DebuffStacks() == 2 || !Helper.HasTalent(386634) && BuffMartialProwess.BuffStacks() == 2) if (MortalStrike.Cast()) return true;
                    // skullsplitter,if=rage<40
                    if (Player.Power < 40) if (Skullsplitter.Cast()) return true;
                    // execute
                    if (Execute.Cast()) return true;
                    // shockwave,if=talent.sonic_boom
                    if (Helper.HasTalent(390725)) if (Shockwave.Cast()) return true;
                    // overpower
                    if (Overpower.Cast()) return true;
                    // bladestorm
                    if (Bladestorm.Cast()) return true;
                    // wrecking_throw
                    if (WreckingThrow.Cast()) return true;
                    // whirlwind,if=buff.collateral_damage.up&cooldown.sweeping_strikes.remains<3
                    if (BuffCollateralDamage.HasBuff() && SweepingStrikes.SpellCooldown() < 3000) if (Whirlwind.Cast()) return true;
                    // sweeping_strikes,if=active_enemies>1
                    if (SweepingStrikes.Cast()) return true;
                    // execute,if=(buff.juggernaut.up&buff.juggernaut.remains<gcd)|(buff.sudden_death.react&dot.deep_wounds.remains&set_bonus.tier31_2pc|buff.sudden_death.react&!dot.rend.remains&set_bonus.tier31_4pc)
                    if ((BuffJuggernaut.HasBuff() && BuffJuggernaut.BuffRemaining() < Player.GCD) || (BuffSuddenDeath.HasBuff() && DebuffDeepWounds.DebuffRemaining() > 0 && Helper.HasSetBonus(2) || BuffSuddenDeath.HasBuff() && !DebuffRend.HasDebuff() && Helper.HasSetBonus(4))) if (Execute.Cast()) return true;
                    // thunder_clap,if=dot.rend.remains<=gcd&talent.blood_and_thunder&talent.blademasters_torment
                    if (DebuffRend.DebuffRemaining() <= Player.GCD && Helper.HasTalent(384277) && Helper.HasTalent(390138)) if (ThunderClap.Cast()) return true;
                    // thunderous_roar,if=raid_event.adds.in>15
                    if (ThunderousRoar.Cast()) return true;
                    // colossus_smash
                    if (ColossusSmash.Cast()) return true;
                    // warbreaker,if=raid_event.adds.in>22
                    if (Warbreaker.Cast()) return true;
                    // mortal_strike
                    if (MortalStrike.Cast()) return true;
                    // thunder_clap,if=dot.rend.remains<=gcd&talent.blood_and_thunder
                    if (DebuffRend.DebuffRemaining() <= Player.GCD && Helper.HasTalent(384277)) if (ThunderClap.Cast()) return true;
                    // whirlwind,if=talent.storm_of_swords&debuff.colossus_smash.up
                    if (Helper.HasTalent(385512) && DebuffColossusSmash.HasDebuff()) if (Whirlwind.Cast()) return true;
                    // bladestorm,if=talent.hurricane&(buff.test_of_might.up|!talent.test_of_might&debuff.colossus_smash.up)&buff.hurricane.remains<2|talent.unhinged&(buff.test_of_might.up|!talent.test_of_might&debuff.colossus_smash.up)
                    if (Helper.HasTalent(390563) && (BuffTestOfMight.HasBuff() || !Helper.HasTalent(385008) && DebuffColossusSmash.HasDebuff()) && BuffHurricane.BuffRemaining() < 2000 || Helper.HasTalent(386628) && (BuffTestOfMight.HasBuff() || !Helper.HasTalent(385008) && DebuffColossusSmash.HasDebuff())) if (Bladestorm.Cast()) return true;
                    // champions_spear,if=buff.test_of_might.up|debuff.colossus_smash.up
                    if (BuffTestOfMight.HasBuff() || DebuffColossusSmash.HasDebuff() || DebuffColossusSmash1.HasDebuff()) if (ChampionsSpear.CastMacro("ChampionsSpearPlayer")) return true;
                    // skullsplitter
                    if (Skullsplitter.Cast()) return true;
                    // execute,if=buff.sudden_death.react
                    if (BuffSuddenDeath.HasBuff()) if (Execute.Cast()) return true;
                    // shockwave,if=talent.sonic_boom.enabled
                    if (Helper.HasTalent(390725)) if (Shockwave.Cast()) return true;
                    // whirlwind,if=talent.storm_of_swords&talent.test_of_might&cooldown.colossus_smash.remains>gcd*7
                    if (Helper.HasTalent(385512) && Helper.HasTalent(385008) && ColossusSmash.SpellCooldown() > Player.GCD * 7) if (Whirlwind.Cast()) return true;
                    // overpower,if=charges=2&!talent.battlelord|talent.battlelord
                    if (Overpower.SpellCharges() == 2 && !Helper.HasTalent(386630) || Helper.HasTalent(386630)) if (Overpower.Cast()) return true;
                    // whirlwind,if=talent.storm_of_swords
                    if (Helper.HasTalent(385512)) if (Whirlwind.Cast()) return true;
                    // slam,if=talent.crushing_force
                    if (Helper.HasTalent(382764)) if (Slam.Cast()) return true;
                    // whirlwind,if=buff.merciless_bonegrinder.up
                    if (BuffMercilessBonegrinder.HasBuff()) if (Whirlwind.Cast()) return true;
                    // thunder_clap
                    if (ThunderClap.Cast()) return true;
                    // slam
                    if (Slam.Cast()) return true;
                    // bladestorm
                    if (Bladestorm.Cast()) return true;
                    // cleave
                    if (Cleave.Cast()) return true;
                    // wrecking_throw
                    if (WreckingThrow.Cast()) return true;
                    // HeroicThrow
                    if (HeroicThrow.Cast()) return true;

                    return true;
                }

            }

            return false;
        }
        public override bool OutOfCombatTick()
        {
            // Das muss immer ganz oben stehen
            Player.Update();
            Target.Update();


            return false;
        }
    }

}