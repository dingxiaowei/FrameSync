using UnityEngine;

public class BattleWorldNodes : SingletonMono<BattleWorldNodes> {
    public Transform[] heroTransArr;
    public Transform[] enemyTransArr;
    public Transform HUDWindowTrans;
    public Camera camera3D;
    public Camera uiCamera;

    public RoundWindow roundWindow;
    public BattleResultWindow battleResultWindow;

    public Transform centerTrans;
    public Transform enemyCenterTrans;
    public Transform selfHeroCenterTrans;
}
