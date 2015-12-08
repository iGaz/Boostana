using EloBuddy;
using EloBuddy.SDK;

namespace Boostana
{
    internal static class MyActivator
    {
        public static Spell.Targeted Ignite;

        public static Item Youmus,
            Botrk,
            Bilgewater,
            CorruptPot,
            HuntersPot,
            RefillPot,
            Biscuit,
            HPPot,
            Qss,
            Mercurial,
            WardingTotem,
            PinkVision,
            GreaterVisionTotem,
            GreaterStealthTotem,
            FarsightAlteration;
        public static Spell.Active Heal, Barrier;
        private const float WardRange = 1000f;
        public static void LoadSpells()
        {
            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("dot"))
                Ignite = new Spell.Targeted(SpellSlot.Summoner1, 580);
            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("dot"))
                Ignite = new Spell.Targeted(SpellSlot.Summoner2, 580);
            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("barrier"))
                Barrier = new Spell.Active(SpellSlot.Summoner1);
            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("barrier"))
                Barrier = new Spell.Active(SpellSlot.Summoner2);
            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("heal"))
                Heal = new Spell.Active(SpellSlot.Summoner1);
            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("heal"))
                Heal = new Spell.Active(SpellSlot.Summoner2);
            Youmus = new Item((int)ItemId.Youmuus_Ghostblade);
            Botrk = new Item((int)ItemId.Blade_of_the_Ruined_King);
            Bilgewater = new Item((int)ItemId.Bilgewater_Cutlass);
            Qss = new Item((int)ItemId.Quicksilver_Sash);
            Mercurial = new Item((int)ItemId.Mercurial_Scimitar);
            HPPot = new Item(2003);
            Biscuit = new Item(2010);
            RefillPot = new Item(2031);
            HuntersPot = new Item(2032);
            CorruptPot = new Item(2033);
            PinkVision = new Item(2043, WardRange);
            GreaterStealthTotem = new Item(3361, WardRange);
            GreaterVisionTotem = new Item(3362, WardRange);
            FarsightAlteration = new Item(3363, WardRange);
            WardingTotem = new Item(3340, WardRange);

        }
    }
}
