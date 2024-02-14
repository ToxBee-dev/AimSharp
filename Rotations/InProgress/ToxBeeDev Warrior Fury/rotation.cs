using System.Collections.Generic;
using System.Dynamic;
using ToxBeeDev;
using AimsharpWow.API;

namespace AimsharpWow.Modules
{

    public class Fury : Rotation        // <<<----- DON'T FORGET TO CHANGE THIS!!!
    {
        // Create Objects for Player and Target
        static Player Player = new Player();
        static Target Target = new Target();

        public override void LoadSettings()
        {
            // Aimsharp rotation settings are here
            Settings.Add(new Setting("Warrior: Fury by Bansaie"));          // <<<----- DON'T FORGET TO CHANGE THIS!!!
            Settings.Add(new Setting("Debugmode", false));
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
            Settings.Add(new Setting("Enraged Regeneration HP%", 1, 100, 30));
            Settings.Add(new Setting("Ignore Pain HP%", 1, 100, 20));
            Settings.Add(new Setting("Rallying Cry HP%", 1, 100, 35));
            Settings.Add(new Setting("Berserker Stance HP%", 1, 100, 40));
            Settings.Add(new Setting("Defensive Stance HP%", 1, 100, 25));

        }
        public override void Initialize()
        {
            // -------------- SETTINGS -----------------------------
            ToxBeeDev.Settings.Debug = GetCheckBox("Debugmode");
            ToxBeeDev.Settings.ClientLanguage = GetDropDown("Game Client Language");
            ToxBeeDev.Settings.FolderName = "Bansaie_Warrior_Fury";                 // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.RotationName = "Warrior: Fury by Bansaie";   // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.Spec = "Warrior: Fury";                      // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.ClientVersion = "1.0";

            Helper.Initialize();

            // Erstelle ein Macro für Spear of Bastion mit wirken auf den Spieler selbst
            Helper.AddCastMacro("ChampionsSpearPlayer", "player", Helper.rootObject.GetStringById(376079));
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
            Spell Execute = new Spell(5308);
            Spell Rampage = new Spell(184367);
            Spell Bloodthirst = new Spell(23881);
            Spell CrushingBlow = new Spell(335097);
            Spell Whirlwind = new Spell(1680, "player");
            Spell Onslaught = new Spell(315720);
            Spell Bloodbath = new Spell(335096);
            Spell RagingBlow = new Spell(85288);
            Spell WreckingThrow = new Spell(384110);
            Spell Slam = new Spell(1464);
            Spell StormBolt = new Spell(107570);
            Spell HeroicThrow = new Spell(57755);

            // Cooldown
            Spell Ravager = new Spell(228920, "player");
            Spell Recklessness = new Spell(1719, "player");
            Spell Avatar = new Spell(163249, "player");
            Spell ChampionsSpear = new Spell(376079, "player", false, true);
            Spell OdynsFury = new Spell(385059, "player");
            Spell ThunderousRoar = new Spell(384318, "player");

            // SelfHeal
            Spell BitterImmunity = new Spell(383762, "player");
            Spell ImpendingVictory = new Spell(202168);
            Spell EnragedRegeneration = new Spell(184364, "player");
            Spell IgnorePain = new Spell(190456, "player");
            Spell RallyingCry = new Spell(97462, "player"); 
            Spell DefensiveStance = new Spell(41101, "player");
            Spell BerserkerStance = new Spell(386196, "player");

            // Racial
            Spell LightsJudgment = new Spell(255647, "player");
            Spell Berserking = new Spell(26297, "player");
            Spell BloodFury = new Spell(20572, "player");
            Spell Fireblood = new Spell(265221, "player");
            Spell AncestralCall = new Spell(274738, "player");
         
            // Buff
            Buff BuffAvatar = new Buff(163249);
            Buff BuffEnrage = new Buff(184362);
            Buff BuffRecklessness = new Buff(1719);
            Buff BuffMercilessAssault = new Buff(409983);
            Buff BuffBloodCraze = new Buff(393951);
            Buff BuffFuriousBloodthirst = new Buff(423211);
            Buff BuffAshenJuggernaut = new Buff(335232);
            Buff BuffDancingBlades = new Buff(391683);
            Buff BuffSuddenDeath = new Buff(280721);
            Buff BuffElysianMight = new Buff(311193);
            Buff BuffBerserkerStance = new Buff(386196);
            Buff BuffDefensiveStance = new Buff(41101);

            // Debuffs
            Debuff DebuffGushingWound = new Debuff(385042);

            // Trinkets
            Trinket TopTrinket = new Trinket(0, GetDropDown("Top Trinket"));
            Trinket BottomTrinket = new Trinket(1, GetDropDown("Bottom Trinket"));

            // Consumables
            ItemCustom Pot = new ItemCustom(GetString("Primary Potion"), "Pot", false);
            ItemCustom HealingPotion = new ItemCustom(GetString("Health Potion"), "HealingPotion", false);
            ItemCustom ManaPotion = new ItemCustom(GetString("Mana Potion"), "ManaPotion", false);
            ItemCustom Healthstone = new ItemCustom("Healthstone", "Healthstone", false);

            #endregion

            // Fury functions
            float crit_pct_current = Player.Crit + (BuffRecklessness.BuffStacks() * 20) + (BuffMercilessAssault.BuffStacks() * 10) + (BuffBloodCraze.BuffStacks() * 15);

            if (Helper.InFightCheck())
            {
                //-------- SELFHEAL ---------
                // Bitter Immunity
                BitterImmunity.UseDefensive(GetSlider("Bitter Immunity HP%"));
                // Impending Victory
                ImpendingVictory.UseDefensive(GetSlider("Impending Victory HP%"));
                // Enraged Regeneration
                EnragedRegeneration.UseDefensive(GetSlider("Enraged Regeneration HP%"));
                // Ignore Pain
                IgnorePain.UseDefensive(GetSlider("Ignore Pain HP%"));
                // Rallying Cry
                RallyingCry.UseDefensive(GetSlider("Rallying Cry HP%"));
                // Berserker Stance
                if (Player.Health >= GetSlider("Berserker Stance HP%") && !BuffBerserkerStance.HasBuff()) if (BerserkerStance.Cast()) return true;
                // Defensive Stance
                if (Player.Health <= GetSlider("Defensive Stance HP%") && !BuffDefensiveStance.HasBuff()) if (DefensiveStance.Cast()) return true;

                //-------- CONSUMABLES ---------
                // Pot
                if (GetCheckBox("Use Pot") && BuffRecklessness.HasBuff()) if (Pot.Use("Pot")) return true;
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
                    if (GetCheckBox("Use Top Trinket") && !Helper.IsCustomCodeOn("SaveCooldowns")) if (TopTrinket.useTrinket(BuffRecklessness.HasBuff())) return true;
                    // Trinket 2
                    if (GetCheckBox("Use Bottom Trinket") && !Helper.IsCustomCodeOn("SaveCooldowns")) if (BottomTrinket.useTrinket(BuffRecklessness.HasBuff())) return true;

                    //-------- VARIABLES ---------
                    // variable,name=trinket_1_manual,value=trinket.1.is.algethar_puzzle_box
                    bool trinket_1_manual = TopTrinket.TrinketName("Algethar Puzzle Box");
                    // variable,name=trinket_2_manual,value=trinket.2.is.algethar_puzzle_box
                    bool trinket_2_manual = BottomTrinket.TrinketName("Algethar Puzzle Box");
                    // use_item,name=algethar_puzzle_box
                   

                    //-------------------- Rotation --------------------
                    // lights_judgment,if=buff.recklessness.down
                    if (!Helper.IsCustomCodeOn("SaveCooldowns") && !BuffRecklessness.HasBuff()) if (LightsJudgment.Cast()) return true;
                    // berserking,if=buff.recklessness.up
                    if (!Helper.IsCustomCodeOn("SaveCooldowns") && BuffRecklessness.HasBuff()) if (Berserking.Cast()) return true;
                    // blood_fury
                    if (!Helper.IsCustomCodeOn("SaveCooldowns") && Target.TimeToDie > 9) if (BloodFury.Cast()) return true;
                    // fireblood
                    if (!Helper.IsCustomCodeOn("SaveCooldowns")) if (Fireblood.Cast()) return true;
                    // ancestral_call
                    if (!Helper.IsCustomCodeOn("SaveCooldowns")) if (AncestralCall.Cast()) return true;

                    if (!Helper.IsCustomCodeOn("SaveCooldowns") && (Helper.HasTalent(390135) && BuffEnrage.HasBuff() && !BuffAvatar.HasBuff() && OdynsFury.SpellCooldown() < 1000 || Helper.HasTalent(390123) && BuffEnrage.HasBuff() && !BuffAvatar.HasBuff() && !Helper.HasTalent(390135) && Target.TimeToDie > 9)) if (Avatar.Cast()) return true;
                    // recklessness,if=talent.annihilator&cooldown.spear_of_bastion.remains<1|cooldown.avatar.remains>40|!talent.avatar|target.time_to_die<12
                    if (!Helper.IsCustomCodeOn("SaveCooldowns") && (Helper.HasTalent(383916) && ChampionsSpear.SpellCooldown() < 1000 || Avatar.SpellCooldown() > 40000 || !Helper.HasTalent(107574) || Target.TimeToDie > 9)) if (Recklessness.Cast()) return true;
                    // recklessness,if=!talent.annihilator|target.time_to_die<12
                    if (!Helper.IsCustomCodeOn("SaveCooldowns") && (!Helper.HasTalent(383916) || Target.TimeToDie > 9)) if (Recklessness.Cast()) return true;
                    // ravager,if=cooldown.recklessness.remains<3|buff.recklessness.up
                    if (!Helper.IsCustomCodeOn("SaveCooldowns") && (Ravager.SpellCooldown() < 3000 || BuffRecklessness.HasBuff())) if (Ravager.CastMacro("Ravager")) return true;
                    // spear_of_bastion,if=buff.enrage.up&(buff.furious_bloodthirst.up&talent.titans_torment)|!talent.titans_torment|target.time_to_die<20|active_enemies>1|!set_bonus.tier31_2pc)
                    if (!Helper.IsCustomCodeOn("SaveCooldowns") && BuffEnrage.HasBuff() && (BuffFuriousBloodthirst.HasBuff() && Helper.HasTalent(390135)) || !Helper.HasTalent(390135) || Target.TimeToDie > 9 || Player.EnemiesInMelee > 1 || !Helper.HasSetBonus(2)) if (ChampionsSpear.CastMacro("ChampionsSpearPlayer")) return true;
                    // whirlwind,if=talent.improved_whirlwind&talent.improved_whirlwind
                    if (Helper.HasTalent(12950) && Helper.HasTalent(12950)) if (Whirlwind.Cast()) return true;
                    // execute,if=buff.ashen_juggernaut.up&buff.ashen_juggernaut.remains<gcd
                    if (BuffAshenJuggernaut.HasBuff() && BuffAshenJuggernaut.BuffRemaining() < Player.GCD) if (Execute.Cast()) return true;
                    // odyns_fury,if=buff.enrage.up&(talent.dancing_blades&buff.dancing_blades.remains<5|!talent.dancing_blades))
                    if (BuffEnrage.HasBuff() && Target.TimeToDie > 9 && (Helper.HasTalent(391683) && BuffDancingBlades.BuffRemaining() < 5000 || !Helper.HasTalent(391683))) if (OdynsFury.Cast()) return true;
                    // rampage,if=talent.anger_management&(buff.recklessness.up|buff.enrage.remains<gcd|rage.pct>85)
                    if (Helper.HasTalent(152278) && (BuffRecklessness.HasBuff() || BuffEnrage.BuffRemaining() < Player.GCD || Player.Power > 85)) if (Rampage.Cast()) return true;
                    // bloodbath,if=set_bonus.tier30_4pc&action.bloodthirst.crit_pct_current>=95
                    if (Helper.HasSetBonus(4) && crit_pct_current >= 95) if (Bloodbath.Cast()) return true;
                    // bloodthirst,if=(set_bonus.tier30_4pc&action.bloodthirst.crit_pct_current>=95)|(!talent.reckless_abandon&buff.furious_bloodthirst.up&buff.enrage.up&(!dot.gushing_wound.remains|buff.elysian_might.up))
                    if ((Helper.HasSetBonus(4) && crit_pct_current >= 95) || (!Helper.HasTalent(396749) && BuffFuriousBloodthirst.HasBuff() && BuffEnrage.HasBuff() && (!DebuffGushingWound.HasDebuff() || BuffElysianMight.HasBuff()))) if (Bloodthirst.Cast()) return true;
                    // bloodbath,if=set_bonus.tier31_2pc
                    if (Helper.HasSetBonus(2)) if (Bloodbath.Cast()) return true;
                    // thunderous_roar,if=buff.enrage.up&(spell_targets.whirlwind>1)
                    if (!Helper.IsCustomCodeOn("SaveCooldowns") && BuffEnrage.HasBuff() && Target.TimeToDie > 9) if (ThunderousRoar.Cast()) return true;
                    // onslaught,if=buff.enrage.up|talent.tenderize
                    if (BuffEnrage.HasBuff() || Helper.HasTalent(388933)) if (Onslaught.Cast()) return true;
                    // crushing_blow,if=talent.wrath_and_fury&buff.enrage.up&!buff.furious_bloodthirst.up
                    if (Helper.HasTalent(392936) && BuffEnrage.HasBuff() && !BuffFuriousBloodthirst.HasBuff()) if (CrushingBlow.Cast()) return true;
                    // execute,if=buff.enrage.up&!buff.furious_bloodthirst.up&buff.ashen_juggernaut.up|buff.sudden_death.remains<=gcd&(target.health.pct>35&talent.massacre|target.health.pct>20)
                    if (BuffEnrage.HasBuff() && !BuffFuriousBloodthirst.HasBuff() && BuffAshenJuggernaut.HasBuff() || BuffSuddenDeath.BuffRemaining() <= Player.GCD && (Target.Health > 35 && Helper.HasTalent(206315) || Target.Health > 20)) if (Execute.Cast()) return true;
                    // rampage,if=talent.reckless_abandon&(buff.recklessness.up|buff.enrage.remains<gcd|rage.pct>85)
                    if (Helper.HasTalent(396749) && (BuffRecklessness.HasBuff() || BuffEnrage.BuffRemaining() < Player.GCD || Player.Power > 85)) if (Rampage.Cast()) return true;
                    // execute,if=buff.enrage.up
                    if (BuffEnrage.HasBuff()) if (Execute.Cast()) return true;
                    //rampage,if=talent.anger_management
                    if (Helper.HasTalent(152278)) if (Rampage.Cast()) return true;
                    // execute
                    if (Execute.Cast()) return true;
                    // bloodbath,if=buff.enrage.up&talent.reckless_abandon&!talent.wrath_and_fury
                    if (BuffEnrage.HasBuff() && Helper.HasTalent(396749) && !Helper.HasTalent(392936)) if (Bloodbath.Cast()) return true;
                    // rampage,if=target.health.pct<35&talent.massacre.enabled
                    if (Target.Health < 35 && Helper.HasTalent(206315)) if (Rampage.Cast()) return true;
                    // bloodthirst,if=(buff.enrage.down|(talent.annihilator&!buff.recklessness.up))&!buff.furious_bloodthirst.up
                    if ((!BuffEnrage.HasBuff() || (Helper.HasTalent(383916) && !BuffRecklessness.HasBuff())) && !BuffFuriousBloodthirst.HasBuff()) if (Bloodthirst.Cast()) return true;
                    // raging_blow,if=charges>1&talent.wrath_and_fury
                    if (RagingBlow.SpellCharges() > 1 && Helper.HasTalent(392936) && !Helper.HasTalent(383916)) if (RagingBlow.Cast()) return true;
                    // crushing_blow,if=charges>1&talent.wrath_and_fury&!buff.furious_bloodthirst.up
                    if (CrushingBlow.SpellCharges() > 1 && Helper.HasTalent(392936) && !BuffFuriousBloodthirst.HasBuff()) if (CrushingBlow.Cast()) return true;
                    // bloodbath,if=buff.enrage.down|!talent.wrath_and_fury
                    if (!BuffEnrage.HasBuff() || !Helper.HasTalent(392936)) if (Bloodbath.Cast()) return true;
                    // crushing_blow,if=buff.enrage.up&talent.reckless_abandon&!buff.furious_bloodthirst.up
                    if (BuffEnrage.HasBuff() && Helper.HasTalent(396749) && !BuffFuriousBloodthirst.HasBuff()) if (CrushingBlow.Cast()) return true;
                    //bloodthirst,if=!talent.wrath_and_fury&!buff.furious_bloodthirst.up
                    if (!Helper.HasTalent(392936) && !BuffFuriousBloodthirst.HasBuff()) if (Bloodthirst.Cast()) return true;
                    // raging_blow,if=charges>1
                    if (RagingBlow.SpellCharges() > 1 && !Helper.HasTalent(383916)) if (RagingBlow.Cast()) return true;
                    // rampage
                    if (Rampage.Cast()) return true;
                    // slam,if=talent.annihilator
                    if (Helper.HasTalent(383916)) if (Slam.Cast()) return true;
                    // bloodbath
                    if (Bloodbath.Cast()) return true;
                    // raging_blow
                    if (!Helper.HasTalent(383916)) if (RagingBlow.Cast()) return true;
                    // crushing_blow,if=!buff.furious_bloodthirst.up
                    if (!BuffFuriousBloodthirst.HasBuff()) if (CrushingBlow.Cast()) return true;
                    // bloodthirst
                    if (Bloodthirst.Cast()) return true;
                    // whirlwind
                    if (Whirlwind.Cast()) return true;
                    // wrecking_throw
                    if (WreckingThrow.Cast()) return true;
                    // HeroicThrow
                    if (HeroicThrow.Cast()) return true;
                    // storm_bolt
                    if (StormBolt.Cast()) return true;

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

            // Spell
            Spell BattleShout = new Spell(6673, "player");

            // Buff
            Buff BuffBattleShout = new Buff(6673);

            // Rotation

            // Battle Shout
            if (!BuffBattleShout.HasBuff()) if (BattleShout.Cast()) return true;

            return false;
        }

    }

}