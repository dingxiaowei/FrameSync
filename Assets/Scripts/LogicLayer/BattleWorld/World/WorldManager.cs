using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager {

    public static BattleWorld BattleWorld { get; private set; }
    public static void Initialize() {
        ConfigCenter.Initialized();
    }

    public static void CreateBattleWorld(List<HeroData> playerHeroList, List<HeroData> enemyHeroList) {
        BattleWorld?.OnDestroyWorld();
        BattleWorld = new BattleWorld();
        BattleWorld.OnCreateWorld(playerHeroList, enemyHeroList);
    }

    public static void OnUpdate() {
        if (BattleWorld != null) {
            BattleWorld.OnUpdate();
        }
    }

    public static void DestroyWorld() {
        BattleWorld.OnDestroyWorld();
    }
}
