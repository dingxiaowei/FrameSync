using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundLogicCtrl : ILogicBehaviour {

    public int RoundId { get; private set; }
    public int MaxRoundId { get; private set; }

    private Queue<HeroLogic> heroAttackQueue = null;

    private HeroLogicCtrl heroLogicCtrl;

    public void OnCreate() {
        heroLogicCtrl = WorldManager.BattleWorld.heroLogic;
#if RENDER_LOGIC
        BattleWorldNodes.Instance.roundWindow.RoundStart(RoundId);
#endif
        LogicTimerManager.Instance.DelayCall(2000, NextRoundStart);
    }

    public void NextRoundStart() {
        RoundId++;
#if RENDER_LOGIC
        BattleWorldNodes.Instance.roundWindow.NextRound(RoundId);
#endif
        foreach (var item in heroLogicCtrl.allHeroList) {
            item.RoundStartEvent(RoundId);
        }
        heroAttackQueue = heroLogicCtrl.CalcuAttackSort();
        StartNextHeroAttack();
    }

    public void StartNextHeroAttack() {
        if (CheckBattleOver() || BattleWorld.battleEnd) {
            return;
        }

        if (heroAttackQueue.Count == 0) {
            NextRoundStart();
            RoundEnd();
            return;
        }
        HeroLogic heroLogic = heroAttackQueue.Dequeue();
        heroLogic.OnActionEnd = HeroActionEnd;
        heroLogic.ActionStart();
    }

    public void HeroActionEnd() {
        StartNextHeroAttack();
    }

    public bool CheckBattleOver() {
        if (heroLogicCtrl.HeroIsAllDead(HeroTeamEnum.Self)) {
            Debugger.Log("我方全挂");
            WorldManager.BattleWorld.BattleEnd(true);
            return true;
        }
        if (heroLogicCtrl.HeroIsAllDead(HeroTeamEnum.Enemy)) {
            Debugger.Log("敌人全灭");
            WorldManager.BattleWorld.BattleEnd(false);
            return true;
        }
        return false;
    }

    private void RoundEnd() {
        foreach (var item in heroLogicCtrl.allHeroList) {
            item.RoundEndEvent();
        }
    }

    public void OnLogicFrameUpdate() {

    }

    public void OnDestroy() {
        
    }

    
}
