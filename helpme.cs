// API Documentation
// ToxBeeDev Framework class accessed through using ToxBeeDev;

// In this documentation, I would like to provide you with a brief introduction to the functions that are currently available.
// It's important to note that all AimSharp commands work as usual, and I don't intend to reinvent the wheel.
// My goal is to create functions that make your life easier and could potentially save you a lot of work.

using System.Collections.Generic;
using ToxBeeDev;

class Settings
{
    string ClientLanguage;
    // Here, the language is stored that the framework then uses to load spells, items, and other elements in the correct language.
    // "English", "Deutsch", "Español", "Français", "Italiano", "Português Brasileiro", "Русский", "한국어", "简体中文"

    string FolderName;
    // Here, the name of the folder is stored where you can find the rotation.cs. Important for the spells that are available in every language.

    string RotationName;
    // Here, the name of the rotation is stored, and it will be displayed in the console when the rotation is loaded.

    string Spec;
    // There are two ways to load the language of the spells.
    // 1. You can create a language.json file with all the spells and place it next to the rotation.cs file. Or
    // 2. you specify the spec here, and the spells will be loaded from the database, assuming they are already in the database ;)
    // Example: Warrior: Fury, Paladin: Retribution, etc.

    bool Debug;
    // If this is set to true, the console will output a lot of information that can be useful for debugging.

    string ClientVersion;
    // Here, the version of the rotation is stored, and it will be displayed in the console when the rotation is loaded.
}

class Helper
{
    void Initialize();
    // In this function, all essential settings are loaded beforehand.
    // This is where the decision is made regarding the language of the rotation.
    // All default LUA functions are created and loaded here.
    // Additionally, all default macros are created and loaded, and the console output is performed.
    // Here it is also decided that if a language.json is present, it will always be preferred; if not, an attempt will be made to retrieve all spells from the database.

    void AddCastMacro(string MacroName, string CastOn, string SpellName);
    // This function creates a macro that can be used to cast a spell on a target.

    void AddUseMacro(string MacroName, string ItemName);
    // This function creates a macro that can be used to use an item.
    // The item is also automatically added to the item list.
    // It is important that when you create a macro, it always comes before integration into the lists.

    int GetRandomNumber(int min, int max);
    // Return a random number, between min and max
    
    bool UnitIsFocus(string unit);
    // Check if the "unit" is selected as focus and return a true/false

    bool UnitBelowThreshold(int check);
    // Checks whether a member of the group has less life than the threshold

    bool UseInterruptLogic(int SpellID, Target Obj, int MinTime, int MaxTime);
    // This function handles the logic for interrupting, inserting the spell ID of the ability that can interrupt,
    // loading the target object, and more. Enter a minimum and maximum number in milliseconds, and you're done.

    bool IsCustomCodeOn(string Code);
    // This function returns true if the custom code is on.
    // By default, the following are already created: "NoCooldowns", "NoInterrupts", "NoCycle"

    bool HasTalent(int TalentID);
    // This function returns true if the talent is selected.

    bool HasSetBonus(int Piece);
    // This function returns true if the set bonus is active.
    // Before that must be added. Example: Helper.SetBonus_list.Add(207182); under Aimsharp Initialize();

    int GetSpellQueueWindow();
    // This function returns the spell queue window.

    bool TargetIsMouseover();
    // This function returns true if the target is a mouseover.

    bool HasDebuffCheck(bool Magic, bool Poison, bool Disease, bool Curse);
    // This function returns true if the target has a debuff of the specified type.

    int GetLatency();
    // This function returns the latency.

    bool HasTankingAggro();
    // This function returns true if you have aggro.

    bool UnitIsDead();
    // This function returns true if the unit is dead.

    void SetSpellQueue();
    // This function sets the spell queue.

    bool TargetSwitch();
    // Checks whether a targetSwitch can be carried out, if so change the target

    bool InFightCheck();
    // This function returns true if you are in combat.

    bool CheckListForID(int ID, List<int> List);
    // This function returns true if the ID is in the list.

    List<int> ReadListFromFile(string filename);
    // This function reads a list from a file and returns it.
    // in a rotation folder, you can create a file called "list.txt" and fill it with IDs, one per line.
    // 35432=Kill Command
    // 19574=Bestial Wrath
    // 19386=Wyvern Sting

    bool AffixMouseoverNPC(int IdNPC);
    // Check mouseover for affix and return true/false
    // Check for Spiteful Shade => 174773, Afflicted Soul => 204773, Incorporeal Being => 204560

}

class RootObject
{
    List<LanguageItem> Spells;
    // This is the list of spells that are loaded from the language.json file or the database.
    // If you want to create your own JSON file with the language, it should look like this:
    // {
    // "Spells": [
    //  {
    //    "id": "0",
    //    "spellid": "5512",
    //    "spec": "Generel",
    //    "spellbook": "0",             Here indicates whether it is true, 1 for true, 0 for false.
    //    "talents": "0",               Here indicates whether it is true, 1 for true, 0 for false.
    //    "buffs": "0",                 Here indicates whether it is true, 1 for true, 0 for false.
    //    "debuffs": "0",               Here indicates whether it is true, 1 for true, 0 for false.
    //    "totems": "0",                Here indicates whether it is true, 1 for true, 0 for false.
    //    "items": "1",                 Here indicates whether it is true, 1 for true, 0 for false.
    //    "de": "Gesundheitsstein",
    //    "es": "Piedra de salud",
    //    "fr": "Pierre de soins",
    //    "it": "Pietra della Salute",
    //    "pt": "Pedra de Vida",
    //    "ru": "Камень здоровья",
    //    "ko": "생명석",
    //    "cn": "治疗石",
    //    "en": "Healthstone"
    //   }
    //  ]
    // }

    string GetStringById(int id);
    // This function returns the spell name by ID.

}

class Player
{
    // This object contains everything related to the player, such as health points, energy, etc.
    int GCD;
    int GCDMAX;
    float Haste;
    float Crit;
    int Latency;
    int MapID;
    int CombatTime;
    string LastCast;
    bool IsMoving;
    bool IsMounted;
    bool IsChanneling;
    int EnemiesInMelee;
    int Health;
    int Power;
    int MaxPower;
    int PowerDefecit;
    int SecondaryPower;
    int Mana;
    bool IsDead;
    bool InCombat;
    int CastingID;  
    int CastTimeRemaining;          
    bool Casting;
    bool InRaid;
    bool InParty;
    bool IsPvP;
    bool HasPet;
    bool InVehicle;
    bool IsOutdoors;
    int Level;
    string Race;
    string Spec;
    int GroupSize;
    bool LineOfSighted;
    bool NotFacing;

    void Update();
    // With this object, the player is created, and all its values are kept up to date with the update function.

}

class Target
{
    // This object contains everything related to the target, such as health points, energy, etc.
    bool MeleeRange;
    bool FightRange;
    bool PullingRange;
    bool IsChanneling;
    int Health;
    bool InCombat;
    int CastingID;
    int CastTimeRemaining;
    int CastingElapsed;
    bool IsEnemy;
    bool IsDead;
    int Range;
    int MaxHealth;
    int CurrentHealth;
    int CombatTime;
    int TimeToDie;
    bool IsInterruptable;
    bool IsBoss;
    int UnitTargetID;
    int EnemiesNearTarget;

    void Update();
    // With this object, the target is created, and all its values are kept up to date with the update function.
}

class Party
{
    // With this object, the party is created, and all its values are kept up to date with the update function.
    void Update();

    // Return a group member with the lowest health points based on a spell if it is within range.
    Party FindBestHealingTarget(string SpellInRange)

    // Search for a debuff on a group member and return its unit.
    // Prio = player, tank, heal, any
    string FindDebuffOnUnit(string DebuffName, string prio = "player")
}

class Spell
{
    // Create a new spell that can be used later with Cast()
    Spell(int spellId, string unit = "target", bool checkRange = true, bool checkCasting = false);

    // Cast a spell and make an output in the console.
    bool Cast();

    // Cast a spell using a previously created macro
    bool CastMacro(string Macroname);

    // Cast a spell using a previously created macro. with a separate check for a unit
    bool CastMacro(string Macroname, string Unit);

    // Use the spell as a defensive cooldown with a health check
    bool UseDefensive(int threshold);

    // Cast a spell on a group member based on a debuff, for example, the Paladin's Freedom.
    // Prio player, tank, heal, any
    bool CastBestUnit(string Macroname, string DebuffName, string Prio = "player", bool delay = true)

    // Returns spell cooldown
    int SpellCooldown();

    // Returns spell charges
    int SpellCharges();

    // Returns max charges
    int MaxCharges();

    // Returns recharge time
    int RechargeTime();

    // Return spell enabled status
    bool SpellEnabled();
    
    // returns true if the spell is within range
    bool SpellInRange();

    // Returns true if the spell can be used and is not on cd
    bool SpellIsReady();
}

class Heal
{
    // Create a new heal spell that can be used later
    Spell(int spellId, string unit = "player", bool checkRange = true, bool checkCasting = false);

    // Heal the player itself based on a threshold.
    bool Player(int threshold);

    // Heal a group member based on the lowest health points.
    bool BestHealingUnit(string MacroName, int UnitBelowThreshold, bool IsMovingCheck = true);

    // Heal a group member based on the lowest health points.
    bool BestHealingUnit(int UnitBelowThreshold, bool IsMovingCheck = true);

    // Heal based on the average health points of the group.
    bool AverageHealth(int healthThreshold, bool IsMovingCheck = true);

    // Function to dispel a debuff in the 5-member group.
    bool DispelBestUnit(string Macroname, int minTime, int maxTime, bool Magic, bool Curse = false, bool Disease = false, bool Poison = false);

    // Function to dispel a debuff in the 5-member group.
    bool DispelMouseover(string Macroname, int minTime, int maxTime, bool Magic, bool Curse = false, bool Disease = false, bool Poison = false);

    // Revive a dead teammate through mouseover.
    bool RessDeadUnit(string Macroname);

    // Check if the spell is ready.
    bool SpellIsReady();
    
}
class  Buff
{
    // Create a new buff
    Buff(int BuffId, string Unit = "player", bool ByPlayer = true, string Type = "");

    // Check if the buff is active and return a true/false
    bool HasBuff();

    // Check if the buff is active, whether by the player or someone else and return a true/false
    bool HasBuffByAny();

    // Return the number of buff stacks
    int BuffStacks();

    // Return the buff remaining time
    int BuffRemaining();

    // Check whether the buff can be renewed based on the remaining time(ms) "timeleft"
    bool BuffRefresh(int timeleft);
}

class  Debuff
{
    // Create a new debuff
    Debuff(int DebuffId, string Unit = "target", bool ByPlayer = true, string Type = "");
    
    // Check if the debuff is active and return a true/false
    bool HasDebuff();

    // Return the number of debuff stacks
    int DebuffStacks();

    // Return the debuff remaining time
    int DebuffRemaining();

    // Check whether the debuff can be renewed based on the remaining time(ms) "timeleft"
    bool DebuffRefresh(int timeleft);

}

class  Item
{
    // Create a new item based on the ItemID. The name of the item is then loaded from the language.json
    Item(int ItemId, string Macroname, bool CheckIfEquipped = true);

    // User the previously created item based on the macro name
    bool Use(string Macroname);
}

class  ItemCustom
{
    // Create a new custom item, ItemName must contain the correct name of the item
    ItemCustom(string ItemName, string Macroname, bool CheckIfEquipped = true);

    // User the previously created item based on the macro name
    bool Use(string Macroname);
}

class  Trinket
{
    // Create a trinket. 
    // TrinketSlot= 0 Top, 1 Bottom
    // TrinketType can come from the settings, for example, general, self, frindly
    // TrinketItem can come from the settings, This allows you to check whether a certain trinket is dressed.
    Trinket(int TrinketSlot, string TrinketType, string TrinketItem);

    // Returns the cooldown from the trinket
    int Cooldown();
    
    // This checks whether the trinket can be used and is not on cd
    bool isReady();
    
    // Returns a true/false if the "TrinketName" is the equipped
    bool TrinketName(string TrinketName);

    // User of the trinket, with “trigger” you can give a direct review. e.g. SaveCooldowns
    bool useTrinket(bool trigger = false);
}
