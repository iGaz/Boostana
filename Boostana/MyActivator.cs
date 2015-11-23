using EloBuddy;
using EloBuddy.SDK;

namespace Boostana
{
    class MyActivator
    {
        public static Spell.Targeted Ignite;
        public static Item Youmus, Botrk, Bilgewater;
        public static Spell.Targeted Smite;
        public static Spell.Active Heal;
        public static void LoadSpells()
        {
            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("smite"))
                Smite = new Spell.Targeted(SpellSlot.Summoner1, 570);
            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("smite"))
                Smite = new Spell.Targeted(SpellSlot.Summoner2, 570);

            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("dot"))
                Ignite = new Spell.Targeted(SpellSlot.Summoner1, 580);
            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("dot"))
                Ignite = new Spell.Targeted(SpellSlot.Summoner2, 580);

            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("heal"))
                Heal = new Spell.Active(SpellSlot.Summoner1);
            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("heal"))
                Heal = new Spell.Active(SpellSlot.Summoner2);
            Youmus = new Item((int)ItemId.Youmuus_Ghostblade);
            Botrk = new Item((int)ItemId.Blade_of_the_Ruined_King);
            Bilgewater = new Item((int)ItemId.Bilgewater_Cutlass);
        }
    }
}
