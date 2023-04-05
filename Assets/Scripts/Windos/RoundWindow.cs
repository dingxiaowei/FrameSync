using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundWindow : MonoBehaviour {

    public GameObject roundStartAnim;
    public Text roundText;
    public Text logicFrameText;
    public Text quickMultipleText;
    private int maxRoundId = 15;

    public void RoundStart(int roundId) {
        roundStartAnim.SetActive(true);
        gameObject.SetActive(true);
        roundStartAnim.transform.DOScale(1, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() => {
            roundStartAnim.transform.DOScale(0, 0f).SetDelay(0.6f);
        });
        roundText.text = roundId + "/" + maxRoundId;
    }

    public void NextRound(int roundId) {
        roundText.text = roundId + "/" + maxRoundId;
    }

    private void Update() {
        UpdateLogicFrameCount();
    }

    public void UpdateLogicFrameCount() {
        logicFrameText.text = "LogicFrame:" + LogicFrameSyncConfig.LogicFrameId; 
    }

    public void GamePause() {
        WorldManager.BattleWorld.PauseBattle();
    }

    public void OnQuickGame() {
        WorldManager.BattleWorld.QuickBattle();
        quickMultipleText.text = "x" + WorldManager.BattleWorld.quickMultiple;
    }
}
