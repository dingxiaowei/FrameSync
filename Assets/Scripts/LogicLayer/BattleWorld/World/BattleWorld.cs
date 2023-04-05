using System.Collections.Generic;
using UnityEngine;

public class BattleWorld {

    public static bool battleEnd;

    public HeroLogicCtrl heroLogic;
    public RoundLogicCtrl roundLogic;

    public int quickMultiple = 1;
    private int maxQuickMultiple = 3;
    public bool battlePause = false;

    private float accLogicRuntime;
    private float nextLogicFrameTime;
    public static float deltaTime;

    public void OnCreateWorld(List<HeroData> playerHeroList, List<HeroData> enemyHeroList) {
        heroLogic = new HeroLogicCtrl();
        roundLogic = new RoundLogicCtrl();
        heroLogic.OnCreate(playerHeroList, enemyHeroList);
        roundLogic.OnCreate();
        LogicRandom.Instance.InitRandom(3);
        battleEnd = false;
        LogicFrameSyncConfig.LogicFrameId = 0;
        deltaTime = 0;
#if CLIENT_LOGIC
        BattleDataModel dataModel = new BattleDataModel { herolist = playerHeroList, enemylist = enemyHeroList };
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(dataModel);
        PlayerPrefs.SetString(BattleDataModel.key, json);
#endif
    }

    public void OnUpdate() {
        if (battleEnd || battlePause) {
            return;
        }
#if CLIENT_LOGIC
        accLogicRuntime += Time.deltaTime;
        while (accLogicRuntime > nextLogicFrameTime) {
            OnLogicFrameUpdate();
            nextLogicFrameTime += LogicFrameSyncConfig.LogicFrameIntvertal;
            LogicFrameSyncConfig.LogicFrameId++;
        }
        deltaTime = (accLogicRuntime + LogicFrameSyncConfig.LogicFrameIntvertal - nextLogicFrameTime) / LogicFrameSyncConfig.LogicFrameIntvertal;
#else
        OnLogicFrameUpdate();
#endif
        if (Input.GetKeyDown(KeyCode.Q)) {
            heroLogic.heroList[0].PlayAnim("Attack");
            var moveTo = new MoveToAction(heroLogic.heroList[0], heroLogic.enemyList[0].LogicPosition, new VInt(1000), () => {
                SkillEffect effect = ResourceManager.Instance.LoadObject<SkillEffect>("Prefabs/SkillEffect/Effect_RenMa_hit");
                effect.SetEffectPos(heroLogic.enemyList[0].LogicPosition);
            });
            ActionManager.Instance.RunAction(moveTo);
            LogicTimerManager.Instance.DelayCall(700, () => {
                heroLogic.enemyList[0].DamageHP(30);
            });
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            var moveTo = new MoveToAction(heroLogic.heroList[0], new VInt3(BattleWorldNodes.Instance.heroTransArr[0].position), 1000, null);
            ActionManager.Instance.RunAction(moveTo);
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            SkillManager.Instance.ReleaseSkill(1010, heroLogic.heroList[0], true);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            heroLogic.heroList[0].TryClearRage();
            SkillManager.Instance.ReleaseSkill(1011, heroLogic.heroList[0], false);
            heroLogic.heroList[0].UpdateAnger(0);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            heroLogic.heroList[3].TryClearRage();
            SkillManager.Instance.ReleaseSkill(1041, heroLogic.heroList[3], false);
            heroLogic.heroList[3].TryClearRage();
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            SkillManager.Instance.ReleaseSkill(1040, heroLogic.heroList[3], true);
        }
    }

    public void OnLogicFrameUpdate() {
        heroLogic?.OnLogicFrameUpdate();
        roundLogic?.OnLogicFrameUpdate();
        ActionManager.Instance.OnLogicFrameUpdate();
        LogicTimerManager.Instance.OnLogicFrameUpdate();
        BulletManager.Instance.OnLogicFrameUpdate();
        BuffManager.Instance.OnLogicFrameUpdate();
    }

    public void PauseBattle() {
        battlePause = !battlePause;
        Time.timeScale = battlePause ? 0 : quickMultiple;
    }

    public void QuickBattle() {
#if RENDER_LOGIC
        quickMultiple++;
        if (quickMultiple > maxQuickMultiple) {
            quickMultiple = 1;
        }
        Time.timeScale = quickMultiple;
#endif
    }

    public void BattleEnd(bool isWin) {
        battleEnd = true;
#if RENDER_LOGIC
        BattleWorldNodes.Instance.battleResultWindow.SetBattleResult(isWin);
#endif
    }

    public void OnDestroyWorld() {
        heroLogic.OnDestroy();
        roundLogic.OnDestroy();
        SkillManager.Instance.OnDestroy();
        LogicTimerManager.Instance.OnDestroy();
        ActionManager.Instance.OnDestroy();
        BuffManager.Instance.OnDestroy();
    }
}