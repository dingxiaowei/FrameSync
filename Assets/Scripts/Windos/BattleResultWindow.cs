using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultWindow : MonoBehaviour {

    public Text resultText;

    public void SetBattleResult(bool isWin) {
        gameObject.SetActive(true);
        resultText.text = isWin ? "胜利" : "失败";
    }

    public void OnBackPlayButtonClick() {
        gameObject.SetActive(false);
        string json = PlayerPrefs.GetString(BattleDataModel.key);
        BattleDataModel battleDataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleDataModel>(json);
        WorldManager.CreateBattleWorld(battleDataModel.herolist, battleDataModel.enemylist);
    }

    public void OnRestGameButtonClick() {
        gameObject.SetActive(false);
    }
}
