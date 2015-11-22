using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;
using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace Boostana
{
    public static class Program
    {
        public static AIHeroClient Player
        {
            get { return ObjectManager.Player; }
        }
        public static string version = "1.0.4.7";
        public static AIHeroClient Target = null;
        public static int qOff = 0, wOff = 0, eOff = 0, rOff = 0;
        public static int[] AbilitySequence;
        public static Spell.Active Q;
        public static Spell.Skillshot W;
        public static Spell.Targeted E;
        public static Spell.Targeted R;
        public static Obj_AI_Base AllyTarget;
        public static AIHeroClient EnemyTarget;
        public static Vector3 InsecPos;
        public static bool InsecActive;
        public static bool WtfSecActive;
        public static long LastUpdate;
        public static bool ShouldFlash;

        public static AIHeroClient InsecTarget
        {
            get { return EnemyTarget; }
        }

        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
        internal static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
            Bootstrap.Init(null);
        }
        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.ChampionName != "Tristana") return;
            AbilitySequence = new int[] { 3, 2, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            Chat.Print("Boostana Loaded!", Color.CornflowerBlue);
            Chat.Print("Enjoy the game and DONT FLAME!", Color.Red);
            TristanaMenu.loadMenu();
            Game.OnTick += GameOnTick;
            MyActivator.loadSpells();
            Game.OnUpdate += OnGameUpdate;

            #region Skill
            Q = new Spell.Active(SpellSlot.Q, 550);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 450, int.MaxValue, 180);
            E = new Spell.Targeted(SpellSlot.E, 550);
            R = new Spell.Targeted(SpellSlot.R, 550);
            #endregion

            Obj_AI_Base.OnLevelUp += Obj_AI_Base_OnLevelUp;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += AntiGapCloser;
            GameObject.OnCreate += GameObject_OnCreate;
            Drawing.OnDraw += GameOnDraw;
        }
        private static void Obj_AI_Base_OnLevelUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args)
        {
            if (sender.IsMe)
            {
                Q = new Spell.Active(SpellSlot.Q, 543 + (7 * (uint)Player.Level));
                E = new Spell.Targeted(SpellSlot.E, 543 + (7 * (uint)Player.Level));
                R = new Spell.Targeted(SpellSlot.R, 543 + (7 * (uint)Player.Level));
            }
        }


        public static void GameOnDraw(EventArgs args)
        {
            if (TristanaMenu.nodraw()) return;

            if (InsecTarget.IsValidTarget())
            {
                Circle.Draw(SharpDX.Color.Red, InsecTarget.BoundingRadius + 100, InsecTarget.Position);
            }
            if (AllyTarget.IsValidTarget())
            {
                Circle.Draw(SharpDX.Color.BlueViolet, AllyTarget.BoundingRadius + 100, AllyTarget.Position);
            }

            if (!TristanaMenu.onlyReady())
            {
                if (TristanaMenu.drawingsQ())
                {
                    new Circle() { Color = Color.AliceBlue, Radius = Q.Range, BorderWidth = 2f }.Draw(Player.Position);
                }
                if (TristanaMenu.drawingsW())
                {
                    new Circle() { Color = Color.OrangeRed, Radius = W.Range, BorderWidth = 2f }.Draw(Player.Position);
                }
                if (TristanaMenu.drawingsE())
                {
                    new Circle() { Color = Color.Cyan, Radius = E.Range, BorderWidth = 2f }.Draw(Player.Position);
                }
                if (TristanaMenu.drawingsR())
                {
                    new Circle() { Color = Color.SkyBlue, Radius = R.Range, BorderWidth = 2f }.Draw(Player.Position);
                }

            }
            else
            {
                if (!Q.IsOnCooldown && TristanaMenu.drawingsQ())
                {

                    new Circle() { Color = Color.AliceBlue, Radius = 340, BorderWidth = 2f }.Draw(Player.Position);
                }
                if (!W.IsOnCooldown && TristanaMenu.drawingsW())
                {

                    new Circle() { Color = Color.OrangeRed, Radius = 800, BorderWidth = 2f }.Draw(Player.Position);
                }
                if (!E.IsOnCooldown && TristanaMenu.drawingsE())
                {

                    new Circle() { Color = Color.Cyan, Radius = 500, BorderWidth = 2f }.Draw(Player.Position);
                }
                if (!R.IsOnCooldown && TristanaMenu.drawingsR())
                {

                    new Circle() { Color = Color.SkyBlue, Radius = 500, BorderWidth = 2f }.Draw(Player.Position);
                }
            }
        }
        private static void OnGameUpdate(EventArgs args)
        {
            if (MyActivator.heal != null)
                Heal();
            if (MyActivator.ignite != null)
                ignite();
            Player.SetSkinId(TristanaMenu.skinId());
        }
        public static void LevelUpSpells()
        {
            int qL = Player.Spellbook.GetSpell(SpellSlot.Q).Level + qOff;
            int wL = Player.Spellbook.GetSpell(SpellSlot.W).Level + wOff;
            int eL = Player.Spellbook.GetSpell(SpellSlot.E).Level + eOff;
            int rL = Player.Spellbook.GetSpell(SpellSlot.R).Level + rOff;
            if (qL + wL + eL + rL < ObjectManager.Player.Level)
            {
                int[] level = new int[] { 0, 0, 0, 0 };
                for (int i = 0; i < ObjectManager.Player.Level; i++)
                {
                    level[AbilitySequence[i] - 1] = level[AbilitySequence[i] - 1] + 1;
                }
                if (qL < level[0]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);
                if (wL < level[1]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
                if (eL < level[2]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);
                if (rL < level[3]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.R);
            }
        }

        private static void AntiGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!e.Sender.IsValidTarget() || !TristanaMenu.gapcloserR() || e.Sender.Type != Player.Type || !e.Sender.IsEnemy)
            {
                return;
            }

            R.Cast(e.Sender);
        }
        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!sender.IsValidTarget(Q.Range) || e.DangerLevel != DangerLevel.High || e.Sender.Type != Player.Type || !e.Sender.IsEnemy)
            {
                return;
            }
            if (R.IsReady() && R.IsInRange(sender) && TristanaMenu.gapcloserR1())
            {
                R.Cast(sender);
            }

        }
        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var rengar = EntityManager.Heroes.Enemies.Find(r => r.ChampionName.Equals("Rengar"));
            var khazix = EntityManager.Heroes.Enemies.Find(z => z.ChampionName.Equals("Khazix"));
            if (khazix != null)
            {
                if (sender.Name == ("Khazix_Base_E_Tar.troy") && TristanaMenu.gapcloserR2() && sender.Position.Distance(Player) <= 400)
                    R.Cast(khazix);
            }
            if (rengar != null)
            {
                if (sender.Name == ("Rengar_LeapSound.troy") && TristanaMenu.gapcloserR3() && sender.Position.Distance(Player) < R.Range)
                    R.Cast(rengar);
            }
        }

        public static void ignite()
        {
            var autoIgnite = TargetSelector.GetTarget(MyActivator.ignite.Range, DamageType.True);
            if (autoIgnite != null && autoIgnite.Health <= DamageLibrary.GetSpellDamage(Player, autoIgnite, MyActivator.ignite.Slot) || autoIgnite != null && autoIgnite.HealthPercent <= TristanaMenu.spellsIgniteFocus())
                MyActivator.ignite.Cast(autoIgnite);

        }
        public static void Heal()
        {
            if (MyActivator.heal.IsReady() && Player.HealthPercent <= TristanaMenu.spellsHealHP())
                MyActivator.heal.Cast();
        }
        private static void GameOnTick(EventArgs args)
        {
            if (!InsecActive || LastUpdate + 200 <= Environment.TickCount)
            {
                InsecPos = new Vector3();
            }

            if (TristanaMenu.lvlup()) LevelUpSpells();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) OnCombo();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) OnHarrass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)) OnLaneClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear)) OnJungle();
            if (TristanaMenu.MyCombo["combo.WR"].Cast<KeyBind>().CurrentValue)
            {
                Insec();
            }
            KillSteal();
            AutoE();

        }
        private static Vector3 InterceptionPoint(List<Obj_AI_Base> heroes)
        {
            var result = new Vector3();
            result = heroes.Aggregate(result, (current, hero) => current + hero.Position);
            result.X /= heroes.Count;
            result.Y /= heroes.Count;
            return result;
        }

        public static AIHeroClient GetTargetForInsec()
        {
            return TargetSelector.GetTarget(1400, DamageType.Physical);
        }

        public static Vector3 GetBestInsecPos()
        {
            switch (TristanaMenu.MyCombo["insecPositionMode"].Cast<Slider>().CurrentValue)
            {
                case 0:
                    var b =
                        ObjectManager.Get<Obj_AI_Base>()
                            .Where(
                                a =>
                                    (a is AIHeroClient || a is Obj_AI_Turret) && a.IsAlly &&
                                    a.Distance(InsecTarget.Position) < 2000 && !a.IsMe && a.Health > 0).ToList();

                    return b.Any() ? InterceptionPoint(b.ToList()) : Game.CursorPos;
                case 1:
                    return Game.CursorPos;
                case 2:
                    return AllyTarget.Position;
            }
            return new Vector3();
        }
    
        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (args.Msg != 0x202) return;
            var enemyT =
            EntityManager.Heroes.Enemies
                .Where(
                    a =>
                        a.IsValid && a.Health > 0 && (a.IsEnemy) && a.Distance(Game.CursorPos) < 200)
                .ToList()
                .OrderBy(a => a.Distance(Game.CursorPos))
                .FirstOrDefault();

            if (enemyT != null)
            {
                EnemyTarget = enemyT;
                return;
            }

            var allyT =
                ObjectManager.Get<Obj_AI_Base>()
                    .Where(
                        a =>
                            a.IsValid && a.Health > 0 && (a.IsAlly) && a.Distance(Game.CursorPos) < 200 &&
                            (a is AIHeroClient || a is Obj_AI_Minion || a is Obj_AI_Turret) && !a.IsMe)
                    .ToList()
                    .OrderBy(a => a.Distance(Game.CursorPos))
                    .FirstOrDefault();
            if (allyT != null && TristanaMenu.MyCombo["insecPositionMode"].Cast<Slider>().CurrentValue == 2)
            {
                AllyTarget = allyT;
                return;
            }

            AllyTarget = null;
            EnemyTarget = null;
        }
    

        public static void Insec()
        {
            var target = EnemyTarget;

            Orbwalker.OrbwalkTo(TristanaMenu.MyCombo["insecPositionMode"].Cast<Slider>().CurrentValue == 1 && target != null || GetBestInsecPos() == Game.CursorPos && target != null ? target.Position : Game.CursorPos);


            if (target == null || !target.IsValidTarget())
                return;
            var allyPos = GetBestInsecPos();
            if (InsecPos == new Vector3())
            {
                var insecPos = allyPos.Extend(target.Position, target.Distance(allyPos) + TristanaMenu.MyCombo["insecDistance"].Cast<Slider>().CurrentValue).To3D();
                InsecPos = insecPos;
                LastUpdate = Environment.TickCount;
            }
            if (!Program.R.IsReady())
            {
                OnCombo();
                return;
            }

            if (_Player.Distance(InsecPos) < 200)
            {
                Program.R.Cast(target);
                return;
            }
            if(_Player.Distance(InsecPos) > 200)
            {
                Program.R.Cast(InsecPos);
                return;
            }
        }

        private static void KillSteal()
        {
            foreach (var Target in EntityManager.Heroes.Enemies.Where(hero => hero.IsValidTarget(W.Range) && !hero.IsDead && !hero.IsZombie && hero.HealthPercent <= 25))
            {
                var Tawah = EntityManager.Turrets.Enemies.FirstOrDefault(a => !a.IsDead && a.Distance(Target) <= 775 + Player.BoundingRadius + (Target.BoundingRadius / 2) + 44.2);
                if (TristanaMenu.killstealR() && R.IsReady() && Target.Health + Target.AttackShield < Player.GetSpellDamage(Target, SpellSlot.R, DamageLibrary.SpellStages.Default))
                {
                    R.Cast(Target);
                }

                if (TristanaMenu.killstealW() && W.IsReady() && Target.Health + Target.AttackShield < Player.GetSpellDamage(Target, SpellSlot.W, DamageLibrary.SpellStages.Default) && Target.Position.CountEnemiesInRange(800) == 1 && Tawah == null && Player.Mana >= 120)
                {
                    W.Cast(Target.Position);
                }

            }
        }
        private static void AutoE()
        {
            if (!TristanaMenu.MyCombo["combo.CC"].Cast<CheckBox>().CurrentValue)
            {
                return;
            }
            var autoETarget = EntityManager.Heroes.Enemies.FirstOrDefault(x =>x.HasBuffOfType(BuffType.Charm) || x.HasBuffOfType(BuffType.Knockup) || x.HasBuffOfType(BuffType.Stun) || x.HasBuffOfType(BuffType.Suppression) || x.HasBuffOfType(BuffType.Snare));
            if (autoETarget != null && !autoETarget.HasBuff("tristanaecharge"))
            {
                Q.Cast(autoETarget);
            }
        }


        public static void OnLaneClear()
        {
            var count = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.ServerPosition, Player.AttackRange, false).Count();
            var Tawah = EntityManager.Turrets.Enemies.FirstOrDefault(t => !t.IsDead && t.IsInRange(Player, 800));
            var source = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.ServerPosition, Player.AttackRange).OrderByDescending(a => a.MaxHealth).FirstOrDefault();
            var sourceE = EntityManager.MinionsAndMonsters.GetLaneMinions().FirstOrDefault(m => m.IsValidTarget(Player.AttackRange) && m.GetBuffCount("tristanaecharge") > 0);
            if (count == 0) return;
            if (E.IsReady() && TristanaMenu.lcE() && TristanaMenu.lcE2() <= count && Player.ManaPercent >= TristanaMenu.lcM())
            {
                E.Cast(source);
            }
             if (Q.IsReady() && TristanaMenu.lcQ() && TristanaMenu.lcQ1() <= count && Player.ManaPercent >= TristanaMenu.lcM())
            {
                Q.Cast();
            }
            if (W.IsReady() && TristanaMenu.lcW() && TristanaMenu.lcW1() <= count && Player.ManaPercent >= TristanaMenu.lcM())
            {
                W.Cast(source.Position);
            }
            if (sourceE != null)
            {
                Orbwalker.ForcedTarget = sourceE;
            }
            if (Tawah != null)
            {

                if (TristanaMenu.lcE1() && Tawah.IsInRange(Player, E.Range) && E.IsReady() && Player.ManaPercent >= TristanaMenu.lcM())
                {
                    E.Cast(Tawah);
                }

                if (TristanaMenu.lcQ() && Tawah.IsInRange(Player, Q.Range) && Q.IsReady() && Player.ManaPercent >= TristanaMenu.lcM())
                {
                    Q.Cast();
                }
            }

        }
        public static void OnJungle()
        {
            var source = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.ServerPosition, Q.Range).OrderByDescending(a => a.MaxHealth).FirstOrDefault();
            var sourceE = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.ServerPosition, Q.Range).FirstOrDefault(m => m.IsValidTarget(Player.AttackRange) && m.GetBuffCount("tristanaecharge") > 0);

            if (Q.IsReady() && TristanaMenu.jungleQ() && source.Distance(Player) <= Q.Range)
            {
                Q.Cast();
            }
            if (W.IsReady() && TristanaMenu.jungleW() && source.Distance(Player) <= W.Range)
            {
                W.Cast(source.Position);
            }
            if (E.IsReady() && TristanaMenu.jungleE() && source.Distance(Player) <= E.Range)
            {
                E.Cast(source);
            }
            if (sourceE != null)
            {
                Orbwalker.ForcedTarget = sourceE;
            }
        }
        private static void OnHarrass()
        {
            var Target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (!Target.IsValidTarget())
            {
                return;
            }

            Orbwalker.ForcedTarget = null;

            if (TristanaMenu.harassE() && E.IsReady() && Target.IsValidTarget(E.Range) && Player.ManaPercent >= TristanaMenu.harassQE())
            {
                E.Cast(Target);
            }

            if (TristanaMenu.harassQ() && Target.IsValidTarget(Q.Range) && Player.ManaPercent >= TristanaMenu.harassQE() && Target.GetBuffCount("tristanaecharge") > 0)
            {
                Q.Cast();
            }
        }
        private static void OnCombo()
        {
            var Target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            var TargetBoom = EntityManager.Heroes.Enemies.FirstOrDefault(a => a.HasBuff("tristanaecharge") && a.Distance(Player) < Player.AttackRange);
            var Tawah = EntityManager.Turrets.Enemies.FirstOrDefault(a => !a.IsDead && a.Distance(Target) <= 775 + Player.BoundingRadius + (Target.BoundingRadius / 2) + 44.2);
            if (!Target.IsValidTarget(Q.Range) || Target == null)
            {
                return;
            }
            if (TristanaMenu.comboE() && E.IsReady() && Target.IsValidTarget(E.Range))
            {
                E.Cast(Target);
                Orbwalker.ForcedTarget = Target;
            }
            if (TristanaMenu.comboQ() && Q.IsReady() && Target.IsValidTarget(Q.Range))
            {
                    Q.Cast();
            }
            if (TristanaMenu.comboW() && W.IsReady() && Target.IsValidTarget(W.Range) && Target.Position.CountEnemiesInRange(800) <= TristanaMenu.comboW1() && Tawah == null)
            {
                W.Cast(Target.Position);
            }

            if (TargetBoom!= null)
                if (TristanaMenu.comboER() && !E.IsReady() && R.IsReady() && TargetBoom.IsValidTarget(R.Range) && (TargetBoom.Health + TargetBoom.AllShield + TristanaMenu.comboER1()) - (Player.GetSpellDamage(TargetBoom, SpellSlot.E, DamageLibrary.SpellStages.Default) + (TargetBoom.Buffs.Find(a => a.Name == "tristanaecharge").Count * Player.GetSpellDamage(TargetBoom, SpellSlot.E, DamageLibrary.SpellStages.Detonation))) < Player.GetSpellDamage(TargetBoom, SpellSlot.R))
                {
                    R.Cast(TargetBoom);
                }

            if (R.IsReady() && TristanaMenu.comboR() && Target.IsValidTarget(R.Range) && Target.Health + Target.AttackShield + TristanaMenu.comboR1() < Player.GetSpellDamage(Target, SpellSlot.R, DamageLibrary.SpellStages.Default))
            {
                R.Cast(Target);
            }

            if ((ObjectManager.Player.CountEnemiesInRange(ObjectManager.Player.AttackRange) >= TristanaMenu.youmusEnemies() || Player.HealthPercent >= TristanaMenu.itemsYOUMUShp()) && MyActivator.youmus.IsReady() && TristanaMenu.youmus() && MyActivator.youmus.IsOwned())
            {
                MyActivator.youmus.Cast();
                return;
            }
            if (Player.HealthPercent <= TristanaMenu.bilgewaterHP() && TristanaMenu.bilgewater() && MyActivator.bilgewater.IsReady() && MyActivator.bilgewater.IsOwned())
            {
                MyActivator.bilgewater.Cast(Target);
                return;
            }

            if (Player.HealthPercent <= TristanaMenu.botrkHP() && TristanaMenu.botrk() && MyActivator.botrk.IsReady() && MyActivator.botrk.IsOwned())
            {
                MyActivator.botrk.Cast(Target);
                return;
            }

        }
    }
    }


