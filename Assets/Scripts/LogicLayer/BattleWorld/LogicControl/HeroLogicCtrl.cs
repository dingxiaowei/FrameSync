using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HeroTeamEnum {
    None,
    Self,
    Enemy
}

public class HeroLogicCtrl : ILogicBehaviour {
    public List<HeroLogic> allHeroList = new List<HeroLogic>();
    public List<HeroLogic> heroList = new List<HeroLogic>();
    public List<HeroLogic> enemyList = new List<HeroLogic>();

    public void OnCreate() {
        
    }

    public void OnCreate(List<HeroData> playerHeroList, List<HeroData> enemyHeroList) {
#if CLIENT_LOGIC
        CreateHeroByList(playerHeroList, heroList, BattleWorldNodes.Instance.heroTransArr, HeroTeamEnum.Self);
        CreateHeroByList(enemyHeroList, enemyList, BattleWorldNodes.Instance.enemyTransArr, HeroTeamEnum.Enemy);
#else
        CreateHeroByList(playerHeroList, null, HeroTeamEnum.Self);
        CreateHeroByList(enemyHeroList, null, HeroTeamEnum.Enemy);
#endif
    }

    public void CreateHeroByList(List<HeroData> heroList, List<HeroLogic> heroLogicList, Transform[] parents, HeroTeamEnum heroTeam) {
        foreach (HeroData item in heroList) {
            var heroLogic = new HeroLogic(item, heroTeam);
#if CLIENT_LOGIC
            GameObject hero = ResourceManager.Instance.LoadObject("Prefabs/Hero/" + item.id, parents[item.seatid], true, false, true);
            HeroRender heroRender = hero.GetComponent<HeroRender>();
            heroLogic.SetRenderObject(heroRender);
            heroRender.SetLogicObject(heroLogic);
            heroRender.SetHeroData(item, heroTeam);
#endif
            heroLogic.OnCreate();
            heroLogicList.Add(heroLogic);
            allHeroList.Add(heroLogic);
        }
    }

    public List<HeroLogic> GetHeroListByTeam(HeroLogic attacker, HeroTeamEnum attackTeam) {
        switch (attacker.HeroTeam) {
            case HeroTeamEnum.Self:
                return attackTeam == HeroTeamEnum.Self ? heroList : enemyList;
            case HeroTeamEnum.Enemy:
                return attackTeam == HeroTeamEnum.Enemy ? heroList : enemyList;
        }
        return null;
    }

    public Queue<HeroLogic> CalcuAttackSort() {
        Queue<HeroLogic> queue = new Queue<HeroLogic>();
        allHeroList.Sort((x, y) => y.Agl.CompareTo(x.Agl));
        foreach (var item in allHeroList) {
            queue.Enqueue(item);
        }
        return queue;
    }

    public bool HeroIsAllDead(HeroTeamEnum heroTeam) {
        var list = heroTeam == HeroTeamEnum.Self ? heroList : enemyList;
        return !list.Any(t => t.objectState == LogicObjectState.Survival);
    }
        
    public void OnDestroy() {
        foreach (var item in allHeroList) {
            item.OnDestroy();
        }
        allHeroList.Clear();
        heroList.Clear();
        enemyList.Clear();
    }

    public void OnLogicFrameUpdate() {
        
    }
}