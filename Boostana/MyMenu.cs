using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Boostana
{
    class TristanaMenu
    {
        public static Menu MyMenu, MyCombo, MyDraw, MyHarass, MyActivator, MySpells, MyFarm, MyOtherFunctions;
        public static void loadMenu()
        {
            MyTristanaPage();
            MyDrawPage();
            MyComboPage();
            MyFarmPage();
            MyHarassPage();
            MyActivatorPage();
            MyOtherFunctionsPage();
        }

        public static void MyTristanaPage()
        {
            MyMenu = MainMenu.AddMenu("Boostana", "main");
            MyMenu.AddGroupLabel("About this script:");
            MyMenu.AddLabel(" Boostana - " + Program.version);
            MyMenu.AddLabel(" Made by -iRaxe");
            MyMenu.AddSeparator();
            MyMenu.AddGroupLabel("Hotkeys");
            MyMenu.AddLabel(" - Use SpaceBar for Combo");
            MyMenu.AddLabel(" - Use the key V For LaneClear/JungleClear");
            MyMenu.AddLabel(" - Use the key T For Flee");
        }

        public static void MyDrawPage()
        {
            MyDraw = MyMenu.AddSubMenu("Draw  settings", "Draw");
            MyDraw.AddGroupLabel("Draw Settings:");
            MyDraw.Add("nodraw", new CheckBox("No Display Drawing", false));
            MyDraw.Add("onlyReady", new CheckBox("Display only Ready", true));
            MyDraw.AddSeparator();
            MyDraw.Add("draw.Q", new CheckBox("Draw Rapid Fire Range (Q Spell)", true));
            MyDraw.Add("draw.W", new CheckBox("Draw Rocket Jump Range (W Spell)", true));
            MyDraw.Add("draw.E", new CheckBox("Draw Explosive Charge Range (E Spell)", true));
            MyDraw.Add("draw.R", new CheckBox("Draw Buster Shot Range (R Spell)", true));
            MyDraw.AddSeparator();
            MyDraw.AddGroupLabel("Pro Tips");
            MyDraw.AddLabel(" - Uncheck the boxeses if you wish to dont see a specific draw");
        }

        public static void MyComboPage()
        {
            MyCombo = MyMenu.AddSubMenu("Combo settings", "Combo");
            MyCombo.AddGroupLabel("Combo settings:");
            MyCombo.Add("combo.Q", new CheckBox("Use Rapid Fire (Q Spell)"));
            MyCombo.Add("combo.W", new CheckBox("Use Rocket Jump (W Spell)", false));
            MyCombo.Add("combo.E", new CheckBox("Use Explosive Charge (E Spell)"));
            MyCombo.Add("combo.R", new CheckBox("Use Buster Shot (R Spell)"));
            MyCombo.AddSeparator();
            MyCombo.AddGroupLabel("Combo preferences:");
            MyCombo.Add("combo.CC", new CheckBox("Use Explosive Charge (E Spell) on CC"));
            MyCombo.Add("combo.ER", new CheckBox("Use Explosive Charge + Buster Shot Finisher"));
            MyCombo.Add("combo.ER1", new Slider("Explosive Charge + Buster Shot Overkill", 60, 0, 500));
            MyCombo.Add("combo.R1", new Slider("Buster Shot OverKill", 50, 0, 500));
            MyCombo.Add("combo.W1", new Slider("Max enemies for the Rocket Jump (W Spell)", 3, 0, 5));
            MyCombo.AddSeparator();
            MyCombo.Add("insecPositionMode", new Slider("Insec Postion Mode", 2, 0, 2));
            MyCombo.Add("insecDistancee", new Slider("Insec Distance", 200,100,350));
            MyCombo.Add("combo.W3", new Slider("Rocket Jump Overkill", 50, 0, 500));
            MyCombo.AddSeparator();
            MyCombo.Add("combo.WR", new KeyBind("Use Rocket Jump + Buster Shot for Insec", false, KeyBind.BindTypes.HoldActive, 92));
            MyCombo.AddSeparator();
            MyCombo.AddGroupLabel("Pro Tips");
            MyCombo.AddLabel(" -Uncheck the boxes if you wish to dont use a specific spell while you are pressing the Combo Key");
        }
        public static void MyFarmPage()
        {
            MyFarm = MyMenu.AddSubMenu("Lane Clear Settings", "laneclear");
            MyFarm.AddGroupLabel("Lane clear settings:");
            MyFarm.Add("lc.Q", new CheckBox("Use Rapid Fire (Q Spell)"));
            MyFarm.Add("lc.Q1", new Slider("Min. Minions for Rapid Fire ", 3, 0, 10));
            MyFarm.AddSeparator();
            MyFarm.Add("lc.W", new CheckBox("Use Rocket Jump (W Spell)", false));
            MyFarm.Add("lc.W1", new Slider("Min. Minions for Rocket Jump", 3, 0, 10));
            MyFarm.AddSeparator();
            MyFarm.Add("lc.E", new CheckBox("Use Explosive Charge (E Spell)", false));
            MyFarm.Add("lc.E1", new CheckBox("Use Explosive Charge (E Spell) on Tower", false));
            MyFarm.Add("lc.E2", new Slider("Min. Minions for Explosive Charge ", 3, 0, 10));
            MyFarm.Add("lc.M", new Slider("Min. Mana for Laneclear Spells %", 30, 0, 100));
            MyFarm.AddSeparator();
            MyFarm.AddGroupLabel("Jungle Settings");
            MyFarm.Add("jungle.Q", new CheckBox("Use Rapid Fire in Jungle (Q Spell)"));
            MyFarm.Add("jungle.W", new CheckBox("Use Rocket Jump in Jungle (W Spell)"));
            MyFarm.Add("jungle.E", new CheckBox("Use Explosive Charge in Jungle (E Spell)"));
            MyFarm.AddSeparator();
            MyFarm.AddGroupLabel("Pro Tips");
            MyFarm.AddLabel(" -Uncheck the boxes if you wish to dont use a specific spell while you are pressing the Jungle/LaneClear Key");
        }
        public static void MyHarassPage()
        {
            MyHarass = MyMenu.AddSubMenu("Harass/Killsteal Settings", "hksettings");
            MyHarass.AddGroupLabel("Harass Settings:");
            MyHarass.AddSeparator();
            MyHarass.Add("harass.Q", new CheckBox("Use Rapid Fire (Q Spell)", false));
            MyHarass.Add("harass.E", new CheckBox("Use Explosive Charge (E Spell)", false));
            MyHarass.Add("harass.QE", new Slider("Min. Mana for Harass Spells %", 35, 0, 100));
            MyHarass.AddSeparator();
            MyHarass.AddGroupLabel("KillSteal Settings:");
            MyHarass.Add("killsteal.W", new CheckBox("Use Rocket Jump (W Spell)", false));
            MyHarass.Add("killsteal.R", new CheckBox("Use Buster Shot (R Spell)"));
            MyHarass.AddSeparator();
            MyHarass.AddGroupLabel("Pro Tips");
            MyHarass.AddLabel(" -Remember to play safe and don't be a teemo");
        }
        public static void MyActivatorPage()
        {
            MyActivator = MyMenu.AddSubMenu("Items Settings", "Items");
            MyActivator.AddGroupLabel("Items usage:");
            MyActivator.AddSeparator();
            MyActivator.Add("bilgewater", new CheckBox("Use Bilgewater Cutlass"));
            MyActivator.Add("bilgewater.HP", new Slider("Use Bilgewater Cutlass if hp is lower than {0}(%)", 60, 0, 100));
            MyActivator.AddSeparator();
            MyActivator.Add("botrk", new CheckBox("Use Blade of The Ruined King"));
            MyActivator.Add("botrk.HP", new Slider("Use Blade of The Ruined King if hp is lower than {0}(%)", 60, 0, 100));
            MyActivator.AddSeparator();
            MyActivator.Add("youmus", new CheckBox("Use Youmus Ghostblade"));
            MyActivator.Add("items.Youmuss.HP", new Slider("Use Youmuss Ghostblade if hp is lower than {0}(%)", 60, 1, 100));
            MyActivator.Add("youmus.Enemies", new Slider("Use Youmus Ghostblade when there are {0} enemies in range", 3, 1, 5));
            MyActivator.AddSeparator();
            MySpells = MyMenu.AddSubMenu("Spells Settings");
            MySpells.AddGroupLabel("Spells settings:");
            MySpells.AddGroupLabel("Heal settings:");
            MySpells.Add("spells.Heal.Hp", new Slider("Use Heal when HP is lower than {0}(%)", 30, 1, 100));
            MySpells.AddGroupLabel("Ignite settings:");
            MySpells.Add("spells.Ignite.Focus", new Slider("Use Ignite when target HP is lower than {0}(%)", 10, 1, 100));
        }
        public static void MyOtherFunctionsPage()
        {
            MyOtherFunctions = MyMenu.AddSubMenu("Misc Menu", "othermenu");
            MyOtherFunctions.AddGroupLabel("Anti Gap Closer/Interrupt");
            MyOtherFunctions.Add("gapcloser.R", new CheckBox("Buster Shot (R Spell)"));
            MyOtherFunctions.Add("gapcloser.R1", new CheckBox("Buster Shot (R Spell) to Interrupt"));
            MyOtherFunctions.Add("gapcloser.R2", new CheckBox("Buster Shot (R Spell) to Peel from Khazix"));
            MyOtherFunctions.Add("gapcloser.R3", new CheckBox("Buster Shot (R Spell) to Peel from Rengar"));
            MyOtherFunctions.AddSeparator();
            MyOtherFunctions.AddGroupLabel("Level Up Function");
            MyOtherFunctions.Add("lvlup", new CheckBox("Auto Level Up Spells:", false));
            MyOtherFunctions.AddSeparator();
            MyOtherFunctions.AddGroupLabel("Skin settings");
            MyOtherFunctions.Add("skin.Id", new Slider("Skin Editor", 3, 1, 4));
        }
        public static bool nodraw()
        {
            return MyDraw["nodraw"].Cast<CheckBox>().CurrentValue;
        }
        public static bool onlyReady()
        {
            return MyDraw["onlyready"].Cast<CheckBox>().CurrentValue;
        }
        public static bool drawingsQ()
        {
            return MyDraw["draw.Q"].Cast<CheckBox>().CurrentValue;
        }
        public static bool drawingsW()
        {
            return MyDraw["draw.W"].Cast<CheckBox>().CurrentValue;
        }
        public static bool drawingsE()
        {
            return MyDraw["draw.E"].Cast<CheckBox>().CurrentValue;
        }
        public static bool drawingsR()
        {
            return MyDraw["draw.R"].Cast<CheckBox>().CurrentValue;
        }
        public static bool comboQ()
        {
            return MyCombo["combo.Q"].Cast<CheckBox>().CurrentValue;
        }
        public static bool comboW()
        {
            return MyCombo["combo.W"].Cast<CheckBox>().CurrentValue;
        }
        public static float comboW1()
        {
            return MyCombo["combo.W1"].Cast<Slider>().CurrentValue;
        }
        public static bool comboE()
        {
            return MyCombo["combo.E"].Cast<CheckBox>().CurrentValue;
        }
        public static bool comboR()
        {
            return MyCombo["combo.R"].Cast<CheckBox>().CurrentValue;
        }
        public static float comboR1()
        {
            return MyCombo["combo.R1"].Cast<Slider>().CurrentValue;
        }
        public static bool comboER()
        {
            return MyCombo["combo.ER"].Cast<CheckBox>().CurrentValue;
        }
        public static float comboER1()
        {
            return MyCombo["combo.ER1"].Cast<Slider>().CurrentValue;
        }
        public static float comboW3()
        {
            return MyCombo["combo.W3"].Cast<Slider>().CurrentValue;
        }
        public static bool lcQ()
        {
            return MyFarm["lc.Q"].Cast<CheckBox>().CurrentValue;
        }
        public static bool lcW()
        {
            return MyFarm["lc.W"].Cast<CheckBox>().CurrentValue;
        }
        public static bool lcE()
        {
            return MyFarm["lc.E"].Cast<CheckBox>().CurrentValue;
        }
        public static bool lcE1()
        {
            return MyFarm["lc.E1"].Cast<CheckBox>().CurrentValue;
        }
        public static float lcE2()
        {
            return MyFarm["lc.E2"].Cast<Slider>().CurrentValue;
        }
        public static float lcQ1()
        {
            return MyFarm["lc.Q1"].Cast<Slider>().CurrentValue;
        }
        public static float lcW1()
        {
            return MyFarm["lc.W1"].Cast<Slider>().CurrentValue;
        }
        public static float lcM()
        {
            return MyFarm["lc.M"].Cast<Slider>().CurrentValue;
        }
        public static bool jungleQ()
        {
            return MyFarm["jungle.Q"].Cast<CheckBox>().CurrentValue;
        }
        public static bool jungleW()
        {
            return MyFarm["jungle.W"].Cast<CheckBox>().CurrentValue;
        }
        public static bool jungleE()
        {
            return MyFarm["jungle.E"].Cast<CheckBox>().CurrentValue;
        }
        public static bool harassQ()
        {
            return MyHarass["harass.Q"].Cast<CheckBox>().CurrentValue;
        }
        public static bool harassE()
        {
            return MyHarass["harass.E"].Cast<CheckBox>().CurrentValue;
        }
        public static float harassQE()
        {
            return MyHarass["harass.QE"].Cast<Slider>().CurrentValue;
        }
        public static bool killstealW()
        {
            return MyHarass["killsteal.W"].Cast<CheckBox>().CurrentValue;
        }
        public static bool killstealR()
        {
            return MyHarass["killsteal.R"].Cast<CheckBox>().CurrentValue;
        }
        public static bool bilgewater()
        {
            return MyActivator["bilgewater"].Cast<CheckBox>().CurrentValue;
        }
        public static float bilgewaterHP()
        {
            return MyActivator["bilgewater.HP"].Cast<Slider>().CurrentValue;
        }
        public static bool botrk()
        {
            return MyActivator["botrk"].Cast<CheckBox>().CurrentValue;
        }
        public static float botrkHP()
        {
            return MyActivator["botrk.HP"].Cast<Slider>().CurrentValue;
        }
        public static bool youmus()
        {
            return MyActivator["youmus"].Cast<CheckBox>().CurrentValue;
        }
        public static float youmusEnemies()
        {
            return MyActivator["youmus.Enemies"].Cast<Slider>().CurrentValue;
        }
        public static float itemsYOUMUShp()
        {
            return MyActivator["items.Youmuss.HP"].Cast<Slider>().CurrentValue;
        }
        public static float spellsHealHP()
        {
            return MySpells["spells.Heal.HP"].Cast<Slider>().CurrentValue;
        }
        public static float spellsIgniteFocus()
        {
            return MySpells["spells.Ignite.Focus"].Cast<Slider>().CurrentValue;
        }
        public static int skinId()
        {
            return MyOtherFunctions["skin.Id"].Cast<Slider>().CurrentValue;
        }
        public static bool lvlup()
        {
            return MyOtherFunctions["lvlup"].Cast<CheckBox>().CurrentValue;
        }
        public static bool gapcloserR()
        {
            return MyOtherFunctions["gapcloser.R"].Cast<CheckBox>().CurrentValue;
        }
        public static bool gapcloserR1()
        {
            return MyOtherFunctions["gapcloser.R1"].Cast<CheckBox>().CurrentValue;
        }
        public static bool gapcloserR2()
        {
            return MyOtherFunctions["gapcloser.R2"].Cast<CheckBox>().CurrentValue;
        }
        public static bool gapcloserR3()
        {
            return MyOtherFunctions["gapcloser.R3"].Cast<CheckBox>().CurrentValue;
        }
    }
}
