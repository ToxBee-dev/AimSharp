using System.Collections.Generic;
using System.Dynamic;
using ToxBeeDev;
using AimsharpWow.API;
using System.Linq;
using System.Net.NetworkInformation;

namespace AimsharpWow.Modules
{

    public class ProRet : Rotation        // <<<----- DON'T FORGET TO CHANGE THIS!!!
    {
        // Create Objects for Player and Target
        static Player Player = new Player();
        static Target Target = new Target();
        static Party Party = new Party();

        // Liste für Freedom
        List<string> FreedomList = Helper.AddDebuffsFromFile("\\Rotations\\Toxay_Paladin_ProRet\\txt\\Freedom.txt");

        #region Spells, Buffs, Debuffs, Items, Totems, CustomCommands

        // Spells
        Spell ExecutionSentence;
        Spell JusticarsVengeance;
        Spell TemplarsVerdict;
        Spell TemplarSlash;
        Spell Judgment;
        Spell HammerOfWrath;
        Spell CrusaderStrike;
        Spell TemplarStrike;
        Spell DivineToll;
        Spell FinalVerdict;
        Spell Consecration;
        Spell MomentOfGlory;
        Spell BastionOfLight;
        Spell ShieldOfTheRighteous;
        Spell BlessedHammer;
        Spell HammerOfTheRighteous;
        Spell EyeOfTyr;
        Spell BlessingOfFreedom;
        Spell BlessingOfProtection;
        Spell DivineProtection;
        Spell Repentance;
        Spell FistOfJustice;
        Spell TurnEvil;

        Spell ArcaneTorrent;

        // Cooldown
        Spell ShieldOfVengeance;
        Spell Crusade;
        Spell AvengingWrath;
        Spell FinalReckoning;
        Spell WakeOfAshes;
        Spell DivineStorm;
        Spell BladeOfJustice;
        Spell DivineHammer;
        Spell AvengersShield;
        Spell Sentinel;
        Spell ArdentDefender;
        Spell GuardianOfAncientKings;

        // SelfHeal
        //Spell CleanseToxins;
        Heal CleanseToxins;
        Spell DivineShield;
        //Spell LayOnHands;
        Heal LayOnHands;
        //Spell WordOfGlory;
        Heal WordOfGlory;
        //Spell FlashOfLight;
        Heal FlashOfLight;
        Heal Intercession;

        // Buff
        Buff BuffCrusade;
        Buff BuffAvengingWrath;
        Buff BuffEmpyreanPower;
        Buff BuffEmpyreanLegacy;
        Buff BuffDivineArbiter;
        Buff BuffEchoesOfWrath;
        Buff BuffTemplarStrikes;
        Buff BuffConsecration;
        Buff BuffShiningLightFree;
        Buff BuffBastionOfLight;
        Buff BuffMomentOfGlory;
        Buff BuffDivinePurpose;
        Buff BuffDivineResonance;
        Buff BuffSanctification;
        Buff BuffSentinel;
        Buff BuffShieldOfTheRighteous;
        Buff BuffArdentDefender;
        Buff BuffGuardianOfAncientKings;
        Buff BuffDivineShield;

        Buff BuffBulwarkOfRighteousFury;

        // Debuffs
        Debuff DebuffExecutionSentence;
        Debuff DebuffExpurgation;
        Debuff DebuffJudgment;
        Debuff DebuffMarkOfFyralath;
        Debuff DebuffConsecration;
        Debuff DebuffEntangled;

        Debuff DebuffTurnEvil;
        Debuff DebuffRepentance;
        Debuff DebuffFistOfJustice;

        // Trinkets
        Trinket TopTrinket;
        Trinket BottomTrinket;
        
        // Items
        Item Weapon;

        // Consumables
        ItemCustom Pot;
        ItemCustom HealingPotion;
        ItemCustom ManaPotion;
        ItemCustom Healthstone;

        void Initial()
        {
            // Spells
            ExecutionSentence = new Spell(343527);
            JusticarsVengeance = new Spell(215661);
            TemplarsVerdict = new Spell(85256);
            TemplarSlash = new Spell(406647);
            Judgment = new Spell(20271);
            HammerOfWrath = new Spell(24275);
            TemplarStrike = new Spell(407480);
            DivineToll = new Spell(375576);
            WakeOfAshes = new Spell(255937);
            DivineStorm = new Spell(53385, "player");
            DivineHammer = new Spell(198034, "player");
            BladeOfJustice = new Spell(184575);
            FinalVerdict = new Spell(383328);
            Consecration = new Spell(26573, "player");
            MomentOfGlory = new Spell(327193, "player");
            BastionOfLight = new Spell(378974, "player");
            ShieldOfTheRighteous = new Spell(53600, "player");
            BlessedHammer = new Spell(204019);
            HammerOfTheRighteous = new Spell(88263);
            EyeOfTyr = new Spell(387174, "player");
            AvengersShield = new Spell(31935);
            BlessingOfFreedom = new Spell(1044, "player");
            BlessingOfProtection = new Spell(1022, "player");
            DivineProtection = new Spell(403876, "player");
            Repentance = new Spell(20066);
            FistOfJustice = new Spell(234299);
            TurnEvil = new Spell(10326);

            ArcaneTorrent = new Spell(155145, "player");

            // Cooldown
            Crusade = new Spell(231895, "player");
            AvengingWrath = new Spell(31884, "player");
            FinalReckoning = new Spell(343721, "player");
            Sentinel = new Spell(389539, "player");
            ArdentDefender = new Spell(31850, "player");
            GuardianOfAncientKings = new Spell(86659, "player");

            // SelfHeal
            //CleanseToxins = new Spell(213644);
            CleanseToxins = new Heal(213644);
            ShieldOfVengeance = new Spell(184662, "player");
            DivineShield = new Spell(642, "player");
            //LayOnHands = new Spell(633, "player");
            LayOnHands = new Heal(633);
            //WordOfGlory = new Spell(85673, "player");
            WordOfGlory = new Heal(85673);
            //FlashOfLight = new Spell(19750, "player");
            FlashOfLight = new Heal(19750);
            Intercession = new Heal(391054);

            // Buff
            BuffCrusade = new Buff(231895);
            BuffAvengingWrath = new Buff(31884);
            BuffEmpyreanPower = new Buff(326733);
            BuffEmpyreanLegacy = new Buff(387170);
            BuffDivineArbiter = new Buff(404306);
            BuffEchoesOfWrath = new Buff(423590);
            BuffTemplarStrikes = new Buff(406646);
            BuffConsecration = new Buff(26573);
            BuffShiningLightFree = new Buff(327510);
            BuffBastionOfLight = new Buff(378974);
            BuffMomentOfGlory = new Buff(327193);
            BuffDivinePurpose = new Buff(223819);
            BuffDivineResonance = new Buff(386738);
            BuffSanctification = new Buff(424616);
            BuffSentinel = new Buff(389539);
            BuffShieldOfTheRighteous = new Buff(53600);
            BuffArdentDefender = new Buff(31850);
            BuffGuardianOfAncientKings = new Buff(86659);
            BuffDivineShield = new Buff(642);

            BuffBulwarkOfRighteousFury = new Buff(386652);

            // Debuffs
            DebuffExecutionSentence = new Debuff(343527);
            DebuffExpurgation = new Debuff(383346);
            DebuffJudgment = new Debuff(20271);
            DebuffMarkOfFyralath = new Debuff(414532);
            DebuffConsecration = new Debuff(26573);
            DebuffEntangled = new Debuff(408556);
            DebuffTurnEvil = new Debuff(10326);
            DebuffRepentance = new Debuff(20066);
            DebuffFistOfJustice = new Debuff(234299);


            // Trinkets
            TopTrinket = new Trinket(0, GetDropDown("Top Trinket"), GetString("Top Trinket Name"));
            BottomTrinket = new Trinket(1, GetDropDown("Bottom Trinket"), GetString("Bottom Trinket Name"));

            // Items
            Weapon = new Item(206448, "Weapon");

            // Consumables
            Pot = new ItemCustom(GetString("Primary Potion"), "Pot", false);
            HealingPotion = new ItemCustom(GetString("Health Potion"), "HealingPotion", false);
            ManaPotion = new ItemCustom(GetString("Mana Potion"), "ManaPotion", false);
            Healthstone = new ItemCustom("Healthstone", "Healthstone", false);
        }

        #endregion

        #region Protection

        // Executed every time the actor is available.
        // Default action list
        bool DefaultActionListProtection()
        {
            //call_action_list,name=cooldowns
            if (CooldownsProtection()) return true;
            //call_action_list,name=mitigation
            if (MitigationProtection()) return true;
            //call_action_list,name=trinkets
            if (TrinketsProtection()) return true;
            //call_action_list,name=standard
            if (StandardProtection()) return true;

            return false;
        }

        // actions.cooldowns
        bool CooldownsProtection()
        {
            //avengers_shield,if=time=0&set_bonus.tier29_2pc
            if (Player.CombatTime == 0 && Helper.HasSetBonus(2)) if (AvengersShield.Cast()) return true;
            //Use Avenger's Shield as first priority before anything else, if t29 2pc is equipped.
            //lights_judgment,if=spell_targets.lights_judgment>=2|!raid_event.adds.exists|raid_event.adds.in>75|raid_event.adds.up
            //avenging_wrath
            if (AvengingWrath.Cast()) return true;
            //potion,if=buff.avenging_wrath.up
            if (GetCheckBox("Use Pot") && (BuffAvengingWrath.HasBuff() || BuffSentinel.HasBuff())) if (Pot.Use("Pot")) return true;
            //moment_of_glory,if=(buff.avenging_wrath.remains<15|(time>|(cooldown.avenging_wrath.remains>15))&(cooldown.avengers_shield.remains&cooldown.judgment.remains&cooldown.hammer_of_wrath.remains))
            if ((BuffAvengingWrath.BuffRemaining() < 15000 || BuffSentinel.BuffRemaining() < 15000) || (Player.CombatTime > 10000 || Sentinel.SpellCooldown() > 15000 || AvengingWrath.SpellCooldown() > 15000) && (AvengersShield.SpellIsReady() && Judgment.SpellIsReady() && HammerOfWrath.SpellIsReady())) if (MomentOfGlory.Cast()) return true;
            //divine_toll,if=spell_targets.shield_of_the_righteous>=3
            if (Player.EnemiesInMelee >= 3) if (DivineToll.Cast()) return true;
            //bastion_of_light,if=buff.avenging_wrath.up|cooldown.avenging_wrath.remains<=30
            if ((BuffAvengingWrath.HasBuff() || BuffSentinel.HasBuff()) || (Helper.HasTalent(389539) && Sentinel.SpellCooldown() <= 30000) || (!Helper.HasTalent(389539) && AvengingWrath.SpellCooldown() <= 30000)) if (BastionOfLight.Cast()) return true;
            //fireblood,if=buff.avenging_wrath.remains>8

            return false;
        }

        // action.mitigration
        bool MitigationProtection()
        {
            //Shield of the Righteous if=tanking & buff.shield_of_the_righteous.down & ( holy_power.deficit = 0 | buff.divine_purpose.up )
            if (Helper.HasTankingAggro() && !BuffShieldOfTheRighteous.HasBuff() && (Player.SecondaryPower == 5 || BuffDivinePurpose.HasBuff())) if (ShieldOfTheRighteous.Cast()) return true;
            // Shining Word of Glory
            if (BuffShiningLightFree.HasBuff()) if (WordOfGlory.BestHealingUnit("WOG_FOC", GetSlider("Word of Glory Shining HP%"))) return true;
            // Self Word of Glory
            if (WordOfGlory.Player(GetSlider("Selfless Healer HP%"))) return true;
            //Divine Shield if=talent.final_stand.enabled & tanking & incoming_damage_5s > ds_damage & ! ( buff.ardent_defender.up | buff.guardian_of_ancient_kings.up | buff.divine_shield.up | buff.potion.up )
            if (Helper.HasTalent(204077) && Helper.HasTankingAggro() && !(BuffArdentDefender.HasBuff() || BuffGuardianOfAncientKings.HasBuff() || BuffDivineShield.HasBuff())) if (DivineShield.UseDefensive(GetSlider("Divine Shield HP%"))) return true;
            // Guardian of Ancient Kings if=tanking & incoming_damage_5s > goak_damage & ! ( buff.ardent_defender.up | buff.guardian_of_ancient_kings.up | buff.divine_shield.up | buff.potion.up )
            if (Helper.HasTankingAggro() && !(BuffArdentDefender.HasBuff() | BuffGuardianOfAncientKings.HasBuff() || BuffDivineShield.HasBuff())) if (GuardianOfAncientKings.UseDefensive(GetSlider("Guardian of Ancient Kings HP%"))) return true;
            //Avenging Wrath if=defensive_sentinel & tanking & incoming_damage_5s > goak_damage & ! ( buff.ardent_defender.up | buff.guardian_of_ancient_kings.up | buff.divine_shield.up | buff.potion.up )
            if (Helper.HasTankingAggro() && !(BuffArdentDefender.HasBuff() | BuffGuardianOfAncientKings.HasBuff() || BuffDivineShield.HasBuff())) if (AvengingWrath.UseDefensive(GetSlider("Avenging Wrath Defensives HP%"))) return true;
            //Ardent Defender if=tanking & incoming_damage_5s > goak_damage & ! ( buff.ardent_defender.up | buff.guardian_of_ancient_kings.up | buff.divine_shield.up | buff.potion.up )
            if (Helper.HasTankingAggro() && !(BuffArdentDefender.HasBuff() | BuffGuardianOfAncientKings.HasBuff() || BuffDivineShield.HasBuff())) if (ArdentDefender.UseDefensive(GetSlider("Ardent Defender HP%"))) return true;
            //Lay of Hands if=health.pct < 15
            if (LayOnHands.Player(GetSlider("Lay On Hands HP%"))) return true;

            return false;
        }

        // actions.standard
        bool StandardProtection()
        {
            //consecration,if=buff.sanctification.stack=buff.sanctification.max_stack
            if (BuffSanctification.BuffStacks() == 5) if (Consecration.Cast()) return true;
            //shield_of_the_righteous,if=(((!talent.righteous_protector.enabled|cooldown.righteous_protector_icd.remains=0)&holy_power>2)|buff.bastion_of_light.up|buff.divine_purpose.up) & (!buff.sanctification.up|buff.sanctification.stack<buff.sanctification.max_stack)
            if ((!Helper.HasTalent(204074) && Player.SecondaryPower > 2 || BuffBastionOfLight.HasBuff() || BuffDivinePurpose.HasBuff()) && (!BuffSanctification.HasBuff() || BuffSanctification.BuffStacks() < 5)) if (ShieldOfTheRighteous.Cast()) return true;
            //Use Shield of the Righteous according to Righteous Protector's ICD, but use it asap if it's a free proc (Bugged interaction, this ignores ICD). Don't use it when on max Sanctification Stacks (Very next GCD will trigger Consecration, so we want the bonus damage)
            //judgment,target_if=min:debuff.judgment.remains,if=spell_targets.shield_of_the_righteous>3&buff.bulwark_of_righteous_fury.stack>=3&holy_power<3
            if (Player.EnemiesInMelee > 3 && BuffBulwarkOfRighteousFury.BuffStacks() >= 3 && Player.SecondaryPower < 3) if (Judgment.Cast()) return true;
            //judgment,target_if=min:debuff.judgment.remains,if=!buff.sanctification_empower.up&set_bonus.tier31_2pc
            //Use Judgment with higher priority if we need to build Sanctification Stacks
            if (!BuffSanctification.HasBuff() && Helper.HasSetBonus(2)) if (Judgment.Cast()) return true;
            //hammer_of_wrath
            if (HammerOfWrath.Cast()) return true;
            //judgment,target_if=min:debuff.judgment.remains,if=charges>=2|full_recharge_time<=gcd.max
            if (Judgment.SpellCharges() >= 2 || Judgment.FullRechargeTime() <= 1080) if (Judgment.Cast()) return true;
            //avengers_shield,if=spell_targets.avengers_shield>2|buff.moment_of_glory.up
            if (Player.EnemiesInMelee > 2 || BuffMomentOfGlory.HasBuff()) if (AvengersShield.Cast()) return true;
            //divine_toll,if=(!raid_event.adds.exists|raid_event.adds.in>10)
            //avengers_shield
            if (AvengersShield.Cast()) return true;
            //Hammer of Wrath
            if (HammerOfWrath.Cast()) return true;
            //judgment,target_if=min:debuff.judgment.remains
            if (Judgment.Cast()) return true;
            //consecration,if=!consecration.up&(!buff.sanctification.stack=buff.sanctification.max_stack|!set_bonus.tier31_2pc)
            if (!BuffConsecration.HasBuff() && (BuffSanctification.BuffStacks() < 5 || !Helper.HasSetBonus(2))) if (Consecration.Cast()) return true;
            //eye_of_tyr,if=talent.inmost_light.enabled&raid_event.adds.in>=45|spell_targets.shield_of_the_righteous>=3
            if (Helper.HasTalent(405757) && Player.EnemiesInMelee >= 3) if (EyeOfTyr.Cast()) return true;
            //blessed_hammer
            if (BlessedHammer.Cast()) return true;
            //hammer_of_the_righteous
            if (HammerOfTheRighteous.Cast()) return true;
            //eye_of_tyr,if=!talent.inmost_light.enabled&raid_event.adds.in>=60|spell_targets.shield_of_the_righteous>=3
            if (!Helper.HasTalent(405757) && Player.EnemiesInMelee >= 3) if (EyeOfTyr.Cast()) return true;
            //word_of_glory,if=buff.shining_light_free.up
            if (BuffShiningLightFree.HasBuff()) if (WordOfGlory.BestHealingUnit("WOG_FOC", GetSlider("Word of Glory Shining HP%"))) return true;
            //arcane_torrent,if=holy_power<5
            if (Player.SecondaryPower < 5) if (ArcaneTorrent.Cast()) return true;
            //consecration,if=!buff.sanctification_empower.up
            if (!BuffSanctification.HasBuff()) if (Consecration.Cast()) return true;

            return false;
        }

        // actions.trinkets
        bool TrinketsProtection()
        {
            //use_items,slots=trinket1,if=(variable.trinket_sync_slot=1&(buff.avenging_wrath.up|fight_remains<=40)|(variable.trinket_sync_slot=2&(!trinket.2.cooldown.ready|!buff.avenging_wrath.up))|!variable.trinket_sync_slot)
            if (GetCheckBox("Use Top Trinket") && !Helper.IsCustomCodeOn("SaveCooldowns")) if (TopTrinket.useTrinket((BuffAvengingWrath.HasBuff() || BuffSentinel.HasBuff()) || (Target.IsBoss && Player.CombatTime < 40000))) return true;
            //use_items,slots=trinket2,if=(variable.trinket_sync_slot=2&(buff.avenging_wrath.up|fight_remains<=40)|(variable.trinket_sync_slot=1&(!trinket.1.cooldown.ready|!buff.avenging_wrath.up))|!variable.trinket_sync_slot)
            if (GetCheckBox("Use Bottom Trinket") && !Helper.IsCustomCodeOn("SaveCooldowns")) if (BottomTrinket.useTrinket((BuffAvengingWrath.HasBuff() || BuffSentinel.HasBuff()) || (Target.IsBoss && Player.CombatTime < 40000))) return true;
            return false;
        }

        #endregion

        #region Retribution

        // Executed every time the actor is available
        // Default action list
        bool DefaultActionListRetribution()
        {
            //call_action_list, name=migration
            if (MitigationRetribution()) return true;
            //call_action_list,name=cooldowns
            if (CooldownsRetribution()) return true;
            //call_action_list,name=generators
            if (GeneratorsRetribution()) return true;

            return false;
        }

        // actions.cooldowns
        bool CooldownsRetribution()
        {
            //potion,if=buff.avenging_wrath.up|buff.crusade.up&buff.crusade.stack=10
            if (GetCheckBox("Use Pot") && (BuffAvengingWrath.HasBuff() || BuffCrusade.HasBuff() && BuffCrusade.BuffStacks() == 10)) if (Pot.Use("Pot")) return true;
            //invoke_external_buff, name=power_infusion,if=buff.avenging_wrath.up|buff.crusade.up
            //lights_judgment,if=spell_targets.lights_judgment>=2|!raid_event.adds.exists|raid_event.adds.in>75|raid_event.adds.up
            //fireblood,if=buff.avenging_wrath.up|buff.crusade.up&buff.crusade.stack=10
            //use_item,name=algethar_puzzle_box,if=(cooldown.avenging_wrath.remains<5&!talent.crusade|cooldown.crusade.remains<5&talent.crusade)&(holy_power>=4&time<5|holy_power>=3&time>5)
            //use_item,slot=trinket1,if=(buff.avenging_wrath.up&cooldown.avenging_wrath.remains>40|buff.crusade.up&buff.crusade.stack=10)&(!trinket.2.has_cooldown|trinket.2.cooldown.remains|variable.trinket_priority=1)|trinket.1.proc.any_dps.duration>=fight_remains
            if (GetCheckBox("Use Top Trinket") && !Helper.IsCustomCodeOn("SaveCooldowns")) if (TopTrinket.useTrinket(BuffAvengingWrath.HasBuff() && AvengingWrath.SpellCooldown() > 40000 || BuffCrusade.HasBuff() && BuffCrusade.BuffStacks() == 10)) return true;
            //use_item,slot=trinket2,if=(buff.avenging_wrath.up&cooldown.avenging_wrath.remains>40|buff.crusade.up&buff.crusade.stack=10)&(!trinket.1.has_cooldown|trinket.1.cooldown.remains|variable.trinket_priority=2)|trinket.2.proc.any_dps.duration>=fight_remains
            if (GetCheckBox("Use Bottom Trinket") && !Helper.IsCustomCodeOn("SaveCooldowns")) if (BottomTrinket.useTrinket(BuffAvengingWrath.HasBuff() && AvengingWrath.SpellCooldown() > 40000 || BuffCrusade.HasBuff() && BuffCrusade.BuffStacks() == 10)) return true;
            //use_item,slot=trinket1,if=!variable.trinket_1_buffs&(trinket.2.cooldown.remains|!variable.trinket_2_buffs|!buff.crusade.up&cooldown.crusade.remains>20|!buff.avenging_wrath.up&cooldown.avenging_wrath.remains>20)
            if (GetCheckBox("Use Top Trinket") && !Helper.IsCustomCodeOn("SaveCooldowns")) if (TopTrinket.useTrinket(!BuffCrusade.HasBuff() && Crusade.SpellCooldown() > 20000 || !BuffAvengingWrath.HasBuff() && AvengingWrath.SpellCooldown() > 20000)) return true;
            //use_item,slot=trinket2,if=!variable.trinket_2_buffs&(trinket.1.cooldown.remains|!variable.trinket_1_buffs|!buff.crusade.up&cooldown.crusade.remains>20|!buff.avenging_wrath.up&cooldown.avenging_wrath.remains>20)
            if (GetCheckBox("Use Bottom Trinket") && !Helper.IsCustomCodeOn("SaveCooldowns")) if (BottomTrinket.useTrinket(!BuffCrusade.HasBuff() && Crusade.SpellCooldown() > 20000 || !BuffAvengingWrath.HasBuff() && AvengingWrath.SpellCooldown() > 20000)) return true;
            //use_item,name=shadowed_razing_annihilator,if=(trinket.2.cooldown.remains|!variable.trinket_2_buffs)&(trinket.2.cooldown.remains|!variable.trinket_2_buffs)
            //use_item,name=fyralath_the_dreamrender,if=dot.mark_of_fyralath.ticking&!buff.avenging_wrath.up&!buff.crusade.up
            if(DebuffMarkOfFyralath.HasDebuff() && !BuffAvengingWrath.HasBuff() && !BuffCrusade.HasBuff() && !Helper.IsCustomCodeOn("SaveCooldowns")) if(Weapon.Use()) return true;
            //shield_of_vengeance,if=fight_remains>15&(!talent.execution_sentence|!debuff.execution_sentence.up)
            if (GetCheckBox("Use SoV Burst") && Player.CombatTime > 15000 && (!Helper.HasTalent(184662) || !DebuffExecutionSentence.HasDebuff())) if (ShieldOfVengeance.Cast()) return true;
            //execution_sentence,if=(!buff.crusade.up&cooldown.crusade.remains>15|buff.crusade.stack=10|cooldown.avenging_wrath.remains<0.75|cooldown.avenging_wrath.remains>15)&(holy_power>=4&time<5|holy_power>=3&time>5|holy_power>=2&talent.divine_auxiliary)&(target.time_to_die>8&!talent.executioners_will|target.time_to_die>12)
            if ((!BuffCrusade.HasBuff() && Crusade.SpellCooldown() > 15000 || BuffCrusade.BuffStacks() == 10 || AvengingWrath.SpellCooldown() < 750 || AvengingWrath.SpellCooldown() > 15000) && (Player.Power >= 4 && Player.CombatTime < 5000 || Player.Power >= 3 && Player.CombatTime > 5000 || Player.Power >= 2 && Helper.HasTalent(406158)) && (Target.TimeToDie > 8 && !Helper.HasTalent(406940) || Target.TimeToDie > 12)) if (ExecutionSentence.Cast()) return true;
            //avenging_wrath,if=holy_power>=4&time<5|holy_power>=3&time>5|holy_power>=2&talent.divine_auxiliary&(cooldown.execution_sentence.remains=0|cooldown.final_reckoning.remains=0)
            if (!Helper.IsCustomCodeOn("SaveCooldowns") && (Player.SecondaryPower >= 4 && Player.CombatTime < 5000 || Player.SecondaryPower >= 3 && Player.CombatTime > 5000 || Player.SecondaryPower >= 2 && Helper.HasTalent(406158) && (ExecutionSentence.SpellIsReady() || FinalReckoning.SpellIsReady()))) if (AvengingWrath.Cast()) return true;
            //crusade,if=holy_power>=5&time<5|holy_power>=3&time>5
            if (Player.SecondaryPower >= 5 && Player.CombatTime < 5000 || Player.SecondaryPower >= 3 && Player.CombatTime > 5000) if (Crusade.Cast()) return true;
            //final_reckoning,if=(holy_power>=4&time<8|holy_power>=3&time>=8|holy_power>=2&talent.divine_auxiliary)&(cooldown.avenging_wrath.remains>10|cooldown.crusade.remains&(!buff.crusade.up|buff.crusade.stack>=10))&(time_to_hpg>0|holy_power=5|holy_power>=2&talent.divine_auxiliary)&(!raid_event.adds.exists|raid_event.adds.up|raid_event.adds.in>40)potion,if=buff.avenging_wrath.up|buff.crusade.up&buff.crusade.stack=10|fight_remains<25
            if ((Player.Power >= 4 && Player.CombatTime < 8000 || Player.SecondaryPower >= 3 && Player.CombatTime >= 8000 || Player.SecondaryPower >= 2 && Helper.HasTalent(406158)) && (!Helper.HasTalent(231895) && (BuffAvengingWrath.HasBuff() || AvengingWrath.SpellCooldown() > 30000) || Helper.HasTalent(231895) && (Crusade.SpellCooldown() > 30000 && (!BuffCrusade.HasBuff() || BuffCrusade.BuffStacks() >= 10))) && (Player.SecondaryPower == 5 || Player.SecondaryPower >= 2 && Helper.HasTalent(406158))) if (FinalReckoning.CastMacro("FinalReckoningPlayer")) return true;

            return false;
        }

        // action.mitigation
        bool MitigationRetribution()
        {
            //-------- DEFENSIVE ---------
            // Use Shield of Vengeance
            if (GetCheckBox("Use Shield of Vengeance")) if (ShieldOfVengeance.UseDefensive(GetSlider("Use SoV HP%"))) return true;
            // Justicar`s Vengeance
            if (JusticarsVengeance.UseDefensive(GetSlider("Justicar`s Vengeance HP%"))) return true;
            // Divine Shield
            if (GetCheckBox("Use Divine Shield")) if (DivineShield.UseDefensive(GetSlider("Divine Shield HP%"))) return true;
            // Divine Protection
            if (GetCheckBox("Use Divine Protection")) if (DivineProtection.UseDefensive(GetSlider("Divine Protection HP%"))) return true;

            //-------- HEALING ---------
            //Aimsharp.PrintMessage("Range: " + Party.FindBestHealingTarget().PlayerID);
            // Lay on Handy
            if (GetCheckBox("Use Lay On Hands")) if (LayOnHands.BestHealingUnit("LOH_FOC", GetSlider("Lay On Hands HP%"))) return true;
            // Word of Glory
            // Shining Word of Glory
            if (BuffShiningLightFree.HasBuff()) if (WordOfGlory.BestHealingUnit("WOG_FOC", GetSlider("Word of Glory Shining HP%"))) return true;
            // Party Word of Glory
            if (Helper.IsCustomCodeOn("HealMode")) if (WordOfGlory.BestHealingUnit("WOG_FOC", GetSlider("Word of Glory HP%"))) return true;
            // Self Word of Glory
            if (WordOfGlory.Player(GetSlider("Selfless Healer HP%"))) return true;
            return false;
        }

        // actions.finishers
        bool FinishersRetribution()
        {
            //variable,name=ds_castable,value=(spell_targets.divine_storm>=3|spell_targets.divine_storm>=2&!talent.divine_arbiter|buff.empyrean_power.up)&!buff.empyrean_legacy.up&!(buff.divine_arbiter.up&buff.divine_arbiter.stack>24)
            bool ds_castable = (Player.EnemiesInMelee >= 3 || Player.EnemiesInMelee >= 2 && !Helper.HasTalent(404306) || BuffEmpyreanPower.HasBuff()) && !BuffEmpyreanLegacy.HasBuff() && !(BuffDivineArbiter.HasBuff() && BuffDivineArbiter.BuffStacks() > 24);
            //divine_storm,if=variable.ds_castable&(!talent.crusade|cooldown.crusade.remains>gcd*3|buff.crusade.up&buff.crusade.stack<10)
            if (ds_castable && (!Helper.HasTalent(231895) || Crusade.SpellCooldown() > Player.GCD * 3 || BuffCrusade.HasBuff() && BuffCrusade.BuffStacks() < 10)) if (DivineStorm.Cast()) return true;
            //justicars_vengeance,if=!talent.crusade|cooldown.crusade.remains> gcd*3|buff.crusade.up&buff.crusade.stack<10
            if (!Helper.HasTalent(231895) || Crusade.SpellCooldown() > Player.GCD * 3 || BuffCrusade.HasBuff() && BuffCrusade.BuffStacks() < 10) if (JusticarsVengeance.Cast()) return true;
            //templars_verdict,if=!talent.crusade|cooldown.crusade.remains> gcd*3|buff.crusade.up&buff.crusade.stack<10
            if (!Helper.HasTalent(231895) || Crusade.SpellCooldown() > Player.GCD * 3 || BuffCrusade.HasBuff() && BuffCrusade.BuffStacks() < 10) if (TemplarsVerdict.Cast()) return true;

            return false;
        }

        //	actions.generators
        bool GeneratorsRetribution()
        {
            //call_action_list,name=finishers,if=holy_power=5|buff.echoes_of_wrath.up&set_bonus.tier31_4pc&talent.crusading_strikes|(debuff.judgment.up|holy_power=4)&buff.divine_resonance.up&!set_bonus.tier31_2pc
            if (Player.SecondaryPower == 5 || BuffEchoesOfWrath.HasBuff() && Helper.HasSetBonus(4) && Helper.HasTalent(404542) || (DebuffJudgment.HasDebuff() || Player.Power == 4) && BuffDivineResonance.HasBuff() && !Helper.HasSetBonus(2)) if (FinishersRetribution()) return true;
            //wake_of_ashes,if=holy_power<=2&(cooldown.avenging_wrath.remains|cooldown.crusade.remains)&(!talent.execution_sentence|cooldown.execution_sentence.remains>4|target.time_to_die<8)&(!raid_event.adds.exists|raid_event.adds.in>20|raid_event.adds.up)
            if (Player.SecondaryPower <= 2 && (!Helper.HasTalent(231895) || ExecutionSentence.SpellCooldown() > 4000 || Target.TimeToDie < 8)) if (WakeOfAshes.Cast()) return true;
            //blade_of_justice,if=!dot.expurgation.ticking&set_bonus.tier31_2pc
            if (!DebuffExpurgation.HasDebuff() && Helper.HasSetBonus(2)) if (BladeOfJustice.Cast()) return true;
            //divine_toll,if=holy_power<=2&(!raid_event.adds.exists|raid_event.adds.in>30|raid_event.adds.up)&(cooldown.avenging_wrath.remains>15|cooldown.crusade.remains>15|fight_remains<8)
            if (Player.SecondaryPower <= 2 && (AvengingWrath.SpellCooldown() > 15000 || Crusade.SpellCooldown() > 15000 || Player.CombatTime < 8000)) if (DivineToll.Cast()) return true;
            //judgment,if=dot.expurgation.ticking&!buff.echoes_of_wrath.up&set_bonus.tier31_2pc
            if (DebuffExpurgation.HasDebuff() && !BuffEchoesOfWrath.HasBuff() && Helper.HasSetBonus(2)) if (Judgment.Cast()) return true;
            //call_action_list, name=finishers,if=holy_power>=3&buff.crusade.up&buff.crusade.stack<10
            if (Player.SecondaryPower >= 3 && BuffCrusade.HasBuff() && BuffCrusade.BuffStacks() < 10) if (FinishersRetribution()) return true;
            //templar_slash,if=buff.templar_strikes.remains<gcd&spell_targets.divine_storm>=2
            if (BuffTemplarStrikes.BuffRemaining() < Player.GCD && Player.EnemiesInMelee >= 2) if (TemplarSlash.Cast()) return true;
            //blade_of_justice,if=(holy_power<=3|!talent.holy_blade)&(spell_targets.divine_storm>=2&!talent.crusading_strikes|spell_targets.divine_storm>=4)
            if ((Player.SecondaryPower <= 3 || !Helper.HasTalent(383342)) && (Player.EnemiesInMelee >= 2 && !Helper.HasTalent(404542) || Player.EnemiesInMelee >= 4)) if (BladeOfJustice.Cast()) return true;
            //hammer_of_wrath,if=(spell_targets.divine_storm<2|!talent.blessed_champion|set_bonus.tier30_4pc)&(holy_power<=3|target.health.pct>20|!talent.vanguards_momentum)
            if ((Player.EnemiesInMelee < 2 || !Helper.HasTalent(403010) || Helper.HasSetBonus(4)) && (Player.Power <= 3 || Target.Health > 20 || !Helper.HasTalent(383314))) if (HammerOfWrath.Cast()) return true;
            //templar_slash,if=buff.templar_strikes.remains<gcd
            if (BuffTemplarStrikes.BuffRemaining() < Player.GCD) if (TemplarSlash.Cast()) return true;
            //judgment,if=!debuff.judgment.up&(holy_power<=3|!talent.boundless_judgment)
            if (!DebuffJudgment.HasDebuff() && (Player.SecondaryPower <= 3 || !Helper.HasTalent(405278))) if (Judgment.Cast()) return true;
            //blade_of_justice,if=holy_power<=3|!talent.holy_blade
            if (Player.SecondaryPower <= 3 || !Helper.HasTalent(383342)) if (BladeOfJustice.Cast()) return true;
            //call_action_list,name=finishers,if=(target.health.pct<=20|buff.avenging_wrath.up|buff.crusade.up|buff.empyrean_power.up)
            if (Target.Health <= 20 || BuffAvengingWrath.HasBuff() || BuffCrusade.HasBuff() || BuffEmpyreanPower.HasBuff()) if (FinishersRetribution()) return true;

            //Aimsharp.PrintMessage("Talent: " + !Helper.HasTalent(404834));

            //consecration,if=!consecration.up&spell_targets.divine_storm>=2
            if (!BuffConsecration.HasBuff() && Player.EnemiesInMelee >= 2 && !Helper.HasTalent(404834)) if (Consecration.Cast()) return true;
            //divine_hammer,if=spell_targets.divine_storm>=2
            if (Player.EnemiesInMelee >= 2) if (DivineHammer.Cast()) return true;
            //call_action_list,name=finishers
            if (FinishersRetribution()) return true;
            //templar_slash
            if (TemplarSlash.Cast()) return true;
            //templar_strike
            if (TemplarStrike.Cast()) return true;
            //judgment,if=holy_power<=3|!talent.boundless_judgment
            if (Player.SecondaryPower <= 3 || !Helper.HasTalent(405278)) if (Judgment.Cast()) return true;
            //hammer_of_wrath,if=holy_power<=3|target.health.pct>20|!talent.vanguards_momentum
            if (Player.SecondaryPower <= 3 || Target.Health > 20 || !Helper.HasTalent(383314)) if (HammerOfWrath.Cast()) return true;
            //arcane_torrent
            if(Player.SecondaryPower < 5) if (ArcaneTorrent.Cast()) return true;
            //consecration
            if (!Helper.HasTalent(404834)) if (Consecration.Cast()) return true;
            //divine_hammer
            if (DivineHammer.Cast()) return true;

            return false;
        }

        #endregion

        public override void LoadSettings()
        {
            // Aimsharp rotation settings are here
            Settings.Add(new Setting("Paladin: ProRet by Toxay"));          // <<<----- DON'T FORGET TO CHANGE THIS!!!
            Settings.Add(new Setting("Debugmode", false));
            Settings.Add(new Setting("Game Client Language", new List<string>() { "English", "Deutsch", "Español", "Français", "Italiano", "Português Brasileiro", "Русский", "한국어", "简体中文" }, "English"));

            Settings.Add(new Setting("Trinkets"));
            Settings.Add(new Setting("Use Top Trinket", false));
            Settings.Add(new Setting("Use Bottom Trinket", false));
            Settings.Add(new Setting("Top Trinket", new List<string>() { "Generic", "Friendly", "Self" }, "Generic"));
            Settings.Add(new Setting("Top Trinket Name", "none"));
            Settings.Add(new Setting("Bottom Trinket", new List<string>() { "Generic", "Friendly", "Self" }, "Generic"));
            Settings.Add(new Setting("Bottom Trinket Name", "none"));

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

            Settings.Add(new Setting("Dispel"));
            Settings.Add(new Setting("Use Dispel", true));
            Settings.Add(new Setting("Wait X ms min to Dispel", 1, 5000, 700));
            Settings.Add(new Setting("Wait X ms max to Dispel", 1, 5000, 1200));

            Settings.Add(new Setting("Defensives"));
            Settings.Add(new Setting("Use Shield of Vengeance", true));
            Settings.Add(new Setting("Use SoV HP%", 1, 100, 65));
            Settings.Add(new Setting("Use SoV Burst", true)); // OFFEN weiß nicht ob es geht
            Settings.Add(new Setting("Justicar`s Vengeance HP%", 1, 100, 50));
            Settings.Add(new Setting("Use Divine Shield", true));
            Settings.Add(new Setting("Divine Shield HP%", 1, 100, 15));
            Settings.Add(new Setting("Use Divine Protection", true));
            Settings.Add(new Setting("Divine Protection HP%", 1, 100, 80));
            Settings.Add(new Setting("Guardian of Ancient Kings HP%", 1, 100, 40));
            Settings.Add(new Setting("Avenging Wrath Defensives HP%", 1, 100, 50));
            Settings.Add(new Setting("Ardent Defender HP%", 1, 100, 80));

            Settings.Add(new Setting("Healing"));
            Settings.Add(new Setting("Use Lay On Hands", true));
            Settings.Add(new Setting("Lay On Hands HP%", 1, 100, 15));
            Settings.Add(new Setting("Word of Glory HP%", 1, 100, 20));
            Settings.Add(new Setting("Word of Glory Shining HP%", 1, 100, 70));
            Settings.Add(new Setting("Selfless Healer HP%", 1, 100, 40));

            Settings.Add(new Setting("Utility"));
            Settings.Add(new Setting("Use Blessing of Freedom", true));
            Settings.Add(new Setting("Use Blessing of Sacrifice", true));
            Settings.Add(new Setting("Blessing of Sacrifice Tank only", false));
            Settings.Add(new Setting("Blessing of Sacrifice HP%", 1, 100, 40));
            Settings.Add(new Setting("Use Blessing of Protection", true));
            Settings.Add(new Setting("Blessing of Protection HP%", 1, 100, 30));

        }
        public override void Initialize()
        {
            // -------------- SETTINGS -----------------------------
            ToxBeeDev.Settings.Debug = GetCheckBox("Debugmode");
            ToxBeeDev.Settings.ClientLanguage = GetDropDown("Game Client Language");
            ToxBeeDev.Settings.FolderName = "Toxay_Paladin_ProRet";               // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.RotationName = "Paladin: ProRet by Toxay";         // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.Spec = "Paladin: Retributation";                     // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.ClientVersion = "0.1";

            Helper.Initialize();

            // Load Language for all Spells , Buffs, Debuffs, Items and do it globally
            Initial();

            // Create a macro for the Pot, Healing Potion and Mana Potion
            // Create ItemCustom to use the items
            Helper.AddUseMacro("Pot", GetString("Primary Potion"));
            Helper.AddUseMacro("HealingPotion", GetString("Health Potion"));
            Helper.AddUseMacro("ManaPotion", GetString("Mana Potion"));

            // Create a macro for Final Reckoning
            Helper.AddCastMacro("FinalReckoningPlayer", "player", Helper.rootObject.GetStringById(343721));

            // Focus Spells
            Helper.AddCastMacro("CLE_FOC", "focus", CleanseToxins.SpellName);
            Helper.AddCastMacro("FOL_FOC", "focus", FlashOfLight.SpellName);
            Helper.AddCastMacro("WOG_FOC", "focus", WordOfGlory.SpellName);
            Helper.AddCastMacro("LOH_FOC", "focus", LayOnHands.SpellName);
            Helper.AddCastMacro("BOF_FOC", "focus", BlessingOfFreedom.SpellName);
            Helper.AddCastMacro("BOP_FOC", "focus", BlessingOfProtection.SpellName);

            // Mouseover Spells
            Helper.AddCastMacro("CLE_M", "mouseover,exists", CleanseToxins.SpellName);
            Helper.AddCastMacro("INT_M", "mouseover,exists", Intercession.SpellName);
            Helper.AddCastMacro("TUR_M", "mouseover,exists", TurnEvil.SpellName);
            Helper.AddCastMacro("REP_M", "mouseover,exists", Repentance.SpellName);
            Helper.AddCastMacro("FIS_M", "mouseover,exists", FistOfJustice.SpellName);

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
            
            // Fill T-SetBonus list
            Helper.SetBonus_list.Add(207192);
            Helper.SetBonus_list.Add(207191);
            Helper.SetBonus_list.Add(207194);
            Helper.SetBonus_list.Add(207189);
            Helper.SetBonus_list.Add(207190);
        }
        public override bool CombatTick()
        {

            // This always has to be at the top
            Player.Update();
            Target.Update();
            Party.Update();

            if (Helper.InFightCheck())
            {

                // Interrupt --> Die ID vom Spell eintragen der zum unterbrechen, benutzt wird
                if (!Helper.IsCustomCodeOn("NoInterrupts")) Helper.UseInterruptLogic(96231, Target, GetSlider("Random min"), GetSlider("Random max"));

                //-------- CONSUMABLES ---------
                // Healing Potion
                if (GetCheckBox("Use Healing Potion") && GetSlider("Health Potion HP%") <= Player.Health) if (HealingPotion.Use("HealingPotion")) return true;
                // Mana Potion
                if (GetCheckBox("Use Mana Potion") && GetSlider("Mana Potion Mana%") <= Player.Health) if (ManaPotion.Use("ManaPotion")) return true;
                // Healthstone
                if (GetSlider("Healthstone HP%") >= Player.Health) if (Healthstone.Use("Healthstone")) return true;

                //-------- M+ Affix ---------
                // Afflicted Soul
                if (Helper.AffixMouseoverNPC(204773)) if (CleanseToxins.DispelMouseoverTarget("CT_M")) return true;

                // Entangling
                // no unbound freedom talent
                if(GetCheckBox("Use Blessing of Freedom") && DebuffEntangled.HasDebuff() && !Helper.HasTalent(305394)) if (BlessingOfFreedom.Cast()) return true;
                // unbound freedom talent
                if (GetCheckBox("Use Blessing of Freedom") && Helper.HasTalent(305394)) if (BlessingOfFreedom.CastBestUnit("BOF_FOC", DebuffEntangled.debuffName, "any")) return true;

                // Incorporeal Being
                // Überprüfe ob ich einen der Debuffs habe, um zu verhindern das ich einen Stun zuviel raushaue
                bool StunDebuff = DebuffTurnEvil.HasDebuff() || DebuffRepentance.HasDebuff() || DebuffFistOfJustice.HasDebuff();
                if (Helper.AffixMouseoverNPC(204560) && Helper.HasTalent(20066) && !StunDebuff) if (Repentance.CastMacro("REP_M")) return true;
                if (Helper.AffixMouseoverNPC(204560) && Helper.HasTalent(10326) && !StunDebuff) if (TurnEvil.CastMacro("TUR_M")) return true;
                if (Helper.AffixMouseoverNPC(204560) && Helper.HasTalent(234299) && !StunDebuff) if (FistOfJustice.CastMacro("FIS_M")) return true;

                //-------- Dispel ---------
                // Gruppe
                //if (GetCheckBox("Use Dispel") && !Helper.IsCustomCodeOn("NoDispel")) if (CleanseToxins.Cleanse("CT_FOC", GetSlider("Wait X ms min to Dispel"), GetSlider("Wait X ms max to Dispel"), true, true)) return true;
                if (GetCheckBox("Use Dispel") && !Helper.IsCustomCodeOn("NoDispel")) if (CleanseToxins.DispelUnits("CLE_FOC", GetSlider("Wait X ms min to Dispel"), GetSlider("Wait X ms max to Dispel"))) return true;
                //Mouseover
                if(CleanseToxins.DispelMouseoverTarget("CLE_M")) return true;

                //-------- Unilitys ---------
                if (Intercession.RessDeadUnit("INT_FOC")) return true;

                //-------- AimSharp Console ---------
                //Aimsharp.PrintMessage("DebuffMarkOfFyralath: " + DebuffMarkOfFyralath.HasDebuff());

                //-------------------- Rotation --------------------
                if (Player.Spec == "Paladin: Retribution" && Target.MeleeRange) if (DefaultActionListRetribution()) return true;

                if (Player.Spec == "Paladin: Protection" && Target.MeleeRange) if (DefaultActionListProtection()) return true;
            }

            return false;
        }
        public override bool OutOfCombatTick()
        {

            // This always has to be at the top
            Player.Update();
            Target.Update();

            // Set SQW
            Helper.SetSpellQueueWindow();

            return false;
        }
        public override void OnStop()
        {
            // Clear all Caches in the rotation
            Helper.ClearLists();

            // Say Goodbye in the Aimsharp Console
            Aimsharp.PrintMessage("Goodbye from ToxBeeDev");
        }

    }

}
