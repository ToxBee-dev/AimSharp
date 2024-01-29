using System.Linq;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Drawing;
using ToxBeeDev;
using Newtonsoft.Json;
using AimsharpWow.API;
using AimsharpWow.Modules;


namespace AimsharpWow.Modules
{

    public class Protection : Rotation      // <<<----- DON'T FORGET TO CHANGE THIS!!!
    {
        // Create Objects for Player and Target
        static Player Player = new Player();
        static Target Target = new Target();

        public override void LoadSettings()
        {
            // Aimsharp rotation settings are here
            Settings.Add(new Setting("Warrior: Protection by Bansaie"));          // <<<----- DON'T FORGET TO CHANGE THIS!!!
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
            Settings.Add(new Setting("Last Stand HP%", 1, 100, 40));
            Settings.Add(new Setting("Ignore Pain HP%", 1, 100, 20));
            Settings.Add(new Setting("Rallying Cry HP%", 1, 100, 35));
            Settings.Add(new Setting("Shield Block HP%", 1, 100, 99));
            Settings.Add(new Setting("Shield Wall HP%", 1, 100, 30));
            Settings.Add(new Setting("Battle Stance HP%", 1, 100, 40));
            Settings.Add(new Setting("Defensive Stance HP%", 1, 100, 25));
        }
        public override void Initialize()
        {
            // -------------- SETTINGS -----------------------------
            ToxBeeDev.Settings.ClientLanguage = GetDropDown("Game Client Language");
            ToxBeeDev.Settings.FolderName = "Bansaie_Warrior_Protection";                 // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.RotationName = "Warrior: Protection by Bansaie";   // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.Spec = "Warrior: Protection";                      // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.ClientVersion = "1.0";

            Helper.Initialize();

            // Erstelle ein Macro für Spear of Bastion mit wirken auf den Spieler selbst
            Helper.AddCastMacro("ChampionsSpearPlayer", "player", Helper.rootObject.GetStringById(376079));
            // Erstelle ein Macro für Ravager mit wirken auf den Spieler selbst
            Helper.AddCastMacro("RavagerPlayer", "player", Helper.rootObject.GetStringById(228920));
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
            Spell DemoralizingShout = new Spell(1160);
            Spell ShieldSlam = new Spell(23922);
            Spell ShieldCharge = new Spell(385952);
            Spell ShieldBlock = new Spell(2565, "player");
            Spell Revenge = new Spell(6572, "target", false, true);
            Spell Execute = new Spell(5308);
            Spell Devastate = new Spell(236282);

            // Cooldown
            Spell Avatar = new Spell(163249, "player");
            Spell ShieldWall = new Spell(871, "player");
            Spell Ravager = new Spell(228920, "player");
            Spell ChampionsSpear = new Spell(376080, "player", false, true);
            Spell ThunderousRoar = new Spell(384318, "player");
            Spell ThunderClap = new Spell(6343, "player");
            Spell LastStand = new Spell(12975, "player");

            // SelfHeal
            Spell BitterImmunity = new Spell(383762, "player");
            Spell ImpendingVictory = new Spell(202168);
            Spell IgnorePain = new Spell(190456, "player");
            Spell RallyingCry = new Spell(97462, "player");
            Spell DefensiveStance = new Spell(41101, "player");
            Spell BattleStance = new Spell(386164, "player");

            // Racial
            Spell BloodFury = new Spell(20572, "player");
            Spell Berserking = new Spell(26297, "player");
            Spell ArcaneTorrent = new Spell(28730, "player");
            Spell LightsJudgment = new Spell(255647, "player");
            Spell Fireblood = new Spell(265221, "player");
            Spell AncestralCall = new Spell(274738, "player");
            Spell BagOfTricks = new Spell(312411, "player");

            // Buff
            Buff BuffAvatar = new Buff(163249);
            Buff BuffFervid = new Buff(425517);
            Buff BuffShieldBlock = new Buff(2565);
            Buff BuffViolentOutburst = new Buff(386477);
            Buff BuffSuddenDeath = new Buff(29725);
            Buff BuffRevenge = new Buff(6572);
            Buff BuffLastStand = new Buff(12975);
            Buff BuffBattleStance = new Buff(386164);
            Buff BuffDefensiveStance = new Buff(386208);

            // Debuff
            Debuff DebuffRend = new Debuff(772);

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
                // Ignore Pain
                IgnorePain.UseDefensive(GetSlider("Ignore Pain HP%"));
                // Last Stand
                LastStand.UseDefensive(GetSlider("Last Stand HP%"));
                // Rallying Cry
                RallyingCry.UseDefensive(GetSlider("Rallying Cry HP%"));
                // Shield Block
                ShieldBlock.UseDefensive(GetSlider("Shield Block HP%"));
                // Shield Wall
                ShieldWall.UseDefensive(GetSlider("Shield Wall HP%"));
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
                    // avatar
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (Avatar.Cast()) return true;
                    // shield_wall,if= talent.immovable_object.enabled & buff.avatar.down
                    if (Helper.HasTalent(394307) && !BuffAvatar.HasBuff()) if (ShieldWall.Cast()) return true;
                    // blood_fury
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (BloodFury.Cast()) return true;
                    // berserking
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (Berserking.Cast()) return true;
                    // arcane_torrent
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (ArcaneTorrent.Cast()) return true;
                    // lights_judgment
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (LightsJudgment.Cast()) return true;
                    // fireblood
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (Fireblood.Cast()) return true;
                    // ancestral_call
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (AncestralCall.Cast()) return true;
                    // bag_of_tricks
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (BagOfTricks.Cast()) return true;
                    // potion,if=buff.avatar.up|buff.avatar.up&target.health.pct<=2
                    // ignore_pain,if=target.health.pct>=20&(rage.deficit<=15&cooldown.shield_slam.ready|rage.deficit<=40&cooldown.shield_charge.ready&talent.champions_bulwark.enabled|rage.deficit<=20&cooldown.shield_charge.ready|rage.deficit<=30&cooldown.demoralizing_shout.ready&talent.booming_voice.enabled|rage.deficit<=20&cooldown.avatar.ready|rage.deficit<=45&cooldown.demoralizing_shout.ready&talent.booming_voice.enabled&buff.last_stand.up&talent.unnerving_focus.enabled|rage.deficit<=30&cooldown.avatar.ready&buff.last_stand.up&talent.unnerving_focus.enabled|rage.deficit<=20|rage.deficit<=40&cooldown.shield_slam.ready&buff.violent_outburst.up&talent.heavy_repercussions.enabled&talent.impenetrable_wall.enabled|rage.deficit<=55&cooldown.shield_slam.ready&buff.violent_outburst.up&buff.last_stand.up&talent.unnerving_focus.enabled&talent.heavy_repercussions.enabled&talent.impenetrable_wall.enabled|rage.deficit<=17&cooldown.shield_slam.ready&talent.heavy_repercussions.enabled|rage.deficit<=18&cooldown.shield_slam.ready&talent.impenetrable_wall.enabled)|(rage>=70|buff.seeing_red.stack=7&rage>=35)&cooldown.shield_slam.remains<=1&buff.shield_block.remains>=4&set_bonus.tier31_2pc,use_off_gcd=1
                    if (Player.Health >= 20 && (Player.PowerDefecit <= 15 && ShieldSlam.SpellIsReady() || Player.PowerDefecit <= 40 && ShieldCharge.SpellIsReady() && Helper.HasTalent(384038) || Player.PowerDefecit <= 20 && ShieldCharge.SpellIsReady() || Player.PowerDefecit <= 30 && DemoralizingShout.SpellIsReady() && Helper.HasTalent(202743) || Player.PowerDefecit <= 20 && Avatar.SpellIsReady() || Player.PowerDefecit <= 45 && DemoralizingShout.SpellIsReady() && Helper.HasTalent(202743) && BuffLastStand.HasBuff() && Helper.HasTalent(384042) || Player.PowerDefecit <= 30 && Avatar.SpellIsReady() && BuffLastStand.HasBuff() && Helper.HasTalent(384042) || Player.PowerDefecit <= 20 || Player.PowerDefecit <= 40 && ShieldSlam.SpellIsReady() && BuffViolentOutburst.HasBuff() && Helper.HasTalent(384039) && Helper.HasTalent(384040) || Player.PowerDefecit <= 55 && ShieldSlam.SpellIsReady() && BuffViolentOutburst.HasBuff() && BuffLastStand.HasBuff() && Helper.HasTalent(384042) && Helper.HasTalent(384039) && Helper.HasTalent(384040) || Player.PowerDefecit <= 17 && ShieldSlam.SpellIsReady() && Helper.HasTalent(384040) || Player.PowerDefecit <= 18 && ShieldSlam.SpellIsReady() && Helper.HasTalent(384039))) if (IgnorePain.Cast()) return true;
                    // last_stand,if=(target.health.pct>=90&talent.unnerving_focus.enabled|target.health.pct<=20&talent.unnerving_focus.enabled)|talent.bolster.enabled|set_bonus.tier30_2pc|set_bonus.tier30_4pc
                    if ((Target.Health >= 90 && Helper.HasTalent(384042) || Target.Health <= 20 && Helper.HasTalent(384042)) || Helper.HasTalent(280001) || Helper.HasSetBonus(2) || Helper.HasSetBonus(4)) if (LastStand.Cast()) return true;
                    // ravager
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (Ravager.CastMacro("RavagerPlayer")) return true;
                    // demoralizing_shout,if=talent.booming_voice.enabled
                    if (Helper.HasTalent(202743)) if (DemoralizingShout.Cast()) return true;
                    // spear_of_bastion
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (ChampionsSpear.CastMacro("ChampionsSpearPlayer")) return true;
                    // thunderous_roar
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (ThunderousRoar.Cast()) return true;
                    // shield_slam,if=buff.fervid.up
                    if (BuffFervid.HasBuff()) if (ShieldSlam.Cast()) return true;
                    // shield_charge
                    if (!Helper.IsCustomCodeOn("NoCooldowns") && Target.TimeToDie > 9) if (ShieldCharge.Cast()) return true;
                    // shield_block,if=buff.shield_block.duration<=10
                    if (BuffShieldBlock.BuffRemaining() <= 10) if (ShieldBlock.Cast()) return true;
                    // shield_slam
                    if (ShieldSlam.Cast()) return true;
                    // thunder_clap,if=dot.rend.remains<=2&buff.violent_outburst.down
                    if (DebuffRend.DebuffRemaining() <= 2 && !BuffViolentOutburst.HasBuff()) if (ThunderClap.Cast()) return true;
                    // execute,if=buff.sudden_death.up&talent.sudden_death.enabled
                    if (BuffSuddenDeath.HasBuff() && Helper.HasTalent(29725)) if (Execute.Cast()) return true;
                    // execute
                    if (Execute.Cast()) return true;
                    // thunder_clap,if=(spell_targets.thunder_clap>1|cooldown.shield_slam.remains&!buff.violent_outburst.up)
                    if (ShieldSlam.SpellCooldown() < 0 && !BuffViolentOutburst.HasBuff()) if (ThunderClap.Cast()) return true;
                    // revenge,if=(rage>=80&target.health.pct>20|buff.revenge.up&target.health.pct<=20&rage<=18&cooldown.shield_slam.remains|buff.revenge.up&target.health.pct>20)|(rage>=80&target.health.pct>35|buff.revenge.up&target.health.pct<=35&rage<=18&cooldown.shield_slam.remains|buff.revenge.up&target.health.pct>35)&talent.massacre.enabled
                    if ((Player.Power >= 80 && Target.Health > 20 || BuffRevenge.HasBuff() && Target.Health <= 20 && Player.Power <= 18 && ShieldSlam.SpellCooldown() < 0 || BuffRevenge.HasBuff() && Target.Health > 20) || (Player.Power >= 80 && Target.Health > 35 || BuffRevenge.HasBuff() && Target.Health <= 35 && Player.Power <= 18 && ShieldSlam.SpellCooldown() < 0 || BuffRevenge.HasBuff() && Target.Health > 35) && Helper.HasTalent(281001)) if (Revenge.Cast()) return true;
                    // execute,if=spell_targets.revenge=1
                    if (Execute.Cast()) return true;
                    // revenge,if=target.health>20
                    if (Target.Health > 20) if (Revenge.Cast()) return true;
                    // thunder_clap,if=(spell_targets.thunder_clap>=1|cooldown.shield_slam.remains&buff.violent_outburst.up)
                    if (ShieldSlam.SpellCooldown() < 0 && BuffViolentOutburst.HasBuff()) if (ThunderClap.Cast()) return true;
                    // devastate
                    if (!Helper.HasTalent(236279)) if (Devastate.Cast()) return true;

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