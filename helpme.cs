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
    // This function returns a random number between min and max.

    bool UnitIsFocus(string unit);
    // This function returns true if the unit is the focus.

    bool UnitBelowThreshold(int check);
    // This function returns true if the unit is below the threshold.

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

class Spell
{

}

class  Buff
{
    
}

class  Debuff
{
    
}

class  Item
{
    
}

class  ItemCustom
{
    
}

class  Trinket
{
    
}