using System.Collections.Generic;
using System.Dynamic;
using ToxBeeDev;
using AimsharpWow.API;

namespace AimsharpWow.Modules
{

    public class ToxBeeDevWarriorProtection : Rotation        // <<<----- DON'T FORGET TO CHANGE THIS!!!
    {
        // Create Objects for Player and Target
        static Player Player = new Player();
        static Target Target = new Target();

        public override void LoadSettings()
        {
            // Aimsharp rotation settings are here
            Settings.Add(new Setting("Warrior: Protection by ToxBeeDev"));          // <<<----- DON'T FORGET TO CHANGE THIS!!!
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

            // Settings exclusive to the class
            Settings.Add(new Setting("Specialization Settings"));


        }
        public override void Initialize()
        {
            // -------------- SETTINGS -----------------------------
            ToxBeeDev.Settings.Debug = GetCheckBox("Debugmode");
            ToxBeeDev.Settings.ClientLanguage = GetDropDown("Game Client Language");
            ToxBeeDev.Settings.FolderName = "ToxBeeDev Warrior Protection";                 // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.RotationName = "Warrior: Protection by ToxBeeDev";   // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.Spec = "Warrior: Protection";                      // <<<----- DON'T FORGET TO CHANGE THIS!!!
            ToxBeeDev.Settings.ClientVersion = "0.1";

            Helper.Initialize();

            // Create a macro for the Pot, Healing Potion and Mana Potion
            // Create ItemCustom to use the items
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

            // Fill T-SetBonus list

        }
        public override bool CombatTick()
        {

            // This always has to be at the top
            Player.Update();
            Target.Update();

            // All spells, buffs, debuffs, items and co. are stored here. created that can be used later in the rotation
            #region Define Spells, Buffs, and Debuffs
            // Spell

            // Cooldown

            // SelfHeal

            // Racial

            // Buff

            // Debuffs

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
                
                    // Interrupt --> Die ID vom Spell eintragen der zum unterbrechen, benutzt wird
                    if (!Helper.IsCustomCodeOn("NoInterrupts")) Helper.UseInterruptLogic(6552, Target, GetSlider("Random min"), GetSlider("Random max"));
                    //-------- SELFHEAL ---------
                    
                    //-------- CONSUMABLES ---------
                    // Pot
                    if (GetCheckBox("Use Pot")) if (Pot.Use("Pot")) return true;
                    // Healing Potion
                    if (GetCheckBox("Use Healing Potion") && GetSlider("Health Potion HP%") <= Player.Health) if(HealingPotion.Use("HealingPotion")) return true;
                    // Mana Potion
                    if (GetCheckBox("Use Mana Potion") && GetSlider("Mana Potion Mana%") <= Player.Health) if(ManaPotion.Use("ManaPotion")) return true;
                    // Healthstone
                    if (GetSlider("Healthstone HP%") >= Player.Health) if (Healthstone.Use("Healthstone")) return true;

                    //-------- TRINKETS ---------
                    // Trinket 1
                    if (GetCheckBox("Use Top Trinket") && !Helper.IsCustomCodeOn("NoCooldowns")) if (TopTrinket.useTrinket()) return true;
                    // Trinket 2
                    if (GetCheckBox("Use Bottom Trinket") && !Helper.IsCustomCodeOn("NoCooldowns")) if (BottomTrinket.useTrinket()) return true;

                    //-------------------- Rotation --------------------
            }

            return false;
        }
        public override bool OutOfCombatTick()
        {

            // This always has to be at the top
            Player.Update();
            Target.Update();

            return false;
        }

    }

}
