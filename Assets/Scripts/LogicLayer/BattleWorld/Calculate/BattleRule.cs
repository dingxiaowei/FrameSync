using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleRule {

    public static VInt CalculateDamgae(SkillConfig skillConfig, HeroLogic attacker, HeroLogic attackTarget) {
        var rawDamage = new VInt(0);
        VInt damageRate = attackTarget.Def / (attacker.Atk + attackTarget.Def);
        switch (skillConfig.damageType) {
            case DamageType.None:
                break;
            case DamageType.NormalDamage:
                rawDamage = attacker.Atk - attacker.Atk * damageRate;
                break;
            case DamageType.RealDamage:
                rawDamage = attacker.Atk;
                break;
            case DamageType.AtkPercentage:
                VInt atkMultiple = skillConfig.damagePercentage / new VInt(100);
                VInt totalDamage = attacker.Atk * atkMultiple;
                rawDamage = totalDamage - (totalDamage * damageRate);
                break;
            case DamageType.HpPercentage:
                VInt hpMultiple = skillConfig.damagePercentage / new VInt(100);
                VInt hpTotalDamage = attacker.HP * hpMultiple;
                rawDamage = hpTotalDamage - (hpTotalDamage * damageRate);
                break;
        }
        return rawDamage;
    }

    public static VInt CalculateDamgae(BuffConfig buffConfig, HeroLogic attacker, HeroLogic attackTarget) {
        var rawDamage = new VInt(0);
        VInt damageRate = attackTarget.Def / (attacker.Atk + attackTarget.Def);
        switch (buffConfig.damageType) {
            case BuffDamageType.None:
                break;
            case BuffDamageType.NormalAttack:
                rawDamage = attacker.Atk - attacker.Atk * damageRate;
                break;
            case BuffDamageType.Real:
                rawDamage = attacker.Atk;
                break;
            case BuffDamageType.AtkPercentage:
                VInt atkMultiple = buffConfig.damagePercentage / new VInt(100);
                VInt totalDamage = attacker.Atk * atkMultiple;
                rawDamage = totalDamage - (totalDamage * damageRate);
                break;
            case BuffDamageType.HpPercentage:
                VInt hpMultiple = buffConfig.damagePercentage / new VInt(100);
                VInt hpTotalDamage = attacker.HP * hpMultiple;
                rawDamage = hpTotalDamage - (hpTotalDamage * damageRate);
                break;
        }
        return rawDamage;
    }

    public static VInt CalculateAddDef(int value, HeroLogic attackTarget) {
        VInt rate = new VInt(value) / new VInt(100);
        var addDef = attackTarget.def * rate;
        return addDef;
    }

    public static List<HeroLogic> GetAttackListByAttackType(SkillAttackType attackType, List<HeroLogic> herolist, int attackSeatId) {
        var attackList = new List<HeroLogic>();
        switch (attackType) {
            case SkillAttackType.SingleTarget:
                attackList.Add(GetNormalAttackTarget(herolist, attackSeatId));
                return attackList;
            case SkillAttackType.AllHero:
                return GetSurvivalList(herolist);
            case SkillAttackType.BackRowHero:
                attackList = GetBackRowAndSurvivalHeroList(herolist);
                if (attackList.Count == 0) {
                    attackList = GetFrontRowAndSurvivalHeroList(herolist);
                }
                return attackList;
            case SkillAttackType.FrontRowHero:
                attackList = GetFrontRowAndSurvivalHeroList(herolist);
                if (attackList.Count == 0) {
                    attackList = GetBackRowAndSurvivalHeroList(herolist);
                }
                return attackList;
            case SkillAttackType.SameColumnHero:
                int[] targetArr = GetAttackSeatArr(attackSeatId);
                attackList.Add(herolist[targetArr[0]]);
                attackList.Add(herolist[targetArr[1]]);
                attackList = GetSurvivalList(attackList);
                if (attackList.Count == 0) {
                    attackList.Add(herolist[targetArr[2]]);
                    attackList.Add(herolist[targetArr[3]]);
                    attackList = GetSurvivalList(attackList);
                    if (attackList.Count == 0) {
                        attackList.Add(herolist[targetArr[4]]);
                    }
                }
                return attackList;
        }
        Debugger.LogError("没有查询到有效攻击目标");
        return attackList;
    }

    public static List<HeroLogic> GetBackRowAndSurvivalHeroList(List<HeroLogic> herolist) {
        return GetSurvivalList(GetBackRowHeroList(herolist));
    }

    public static List<HeroLogic> GetBackRowHeroList(List<HeroLogic> herolist) {
        return new List<HeroLogic> {
            herolist[herolist.Count - 1],
            herolist[herolist.Count - 2]
        };
    }

    public static List<HeroLogic> GetFrontRowAndSurvivalHeroList(List<HeroLogic> herolist) {
        return GetSurvivalList(GetFrontRowHeroList(herolist));
    }

    public static List<HeroLogic> GetFrontRowHeroList(List<HeroLogic> herolist) {
        return new List<HeroLogic> {
            herolist[0],
            herolist[1],
            herolist[2]
        };
    }

    public static List<HeroLogic> GetSurvivalList(List<HeroLogic> herolist) {
        return herolist.Where(t => t.objectState == LogicObjectState.Survival).ToList();
    }

    public static HeroLogic GetNormalAttackTarget(List<HeroLogic> heroList, int heroSeatId) {
        if (heroList[0].objectState == LogicObjectState.Survival) {
            return heroList[0];
        }
        int[] attackOrder = GetAttackSeatArr(heroSeatId);
        for (int i = 0; i < attackOrder.Length; i++) {
            int heroIndex = attackOrder[i];
            if (heroList[heroIndex].objectState == LogicObjectState.Survival) {
                return heroList[heroIndex];
            }
        }
        return null;
    }

    public static int[] GetAttackSeatArr(int startSeatId) {
        if (startSeatId == 0) {
            return new int[] { 0, 1, 2, 3, 4 };
        }
        if (startSeatId == 1 || startSeatId == 4) {
            return new int[] { 1, 2, 4, 3, 0 };
        }
        if (startSeatId == 2 || startSeatId == 3) {
            return new int[] { 2, 1, 3, 4, 0 };
        }
        return new int[] { };
    }
}