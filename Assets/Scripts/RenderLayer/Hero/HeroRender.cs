using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HeroRender : RenderObject
{
    public HeroData HeroData { get; private set; }
    public HeroTeamEnum HeroTeam { get; private set; }

    private Animator animator;
    private HeroHUDComponent heroHUDComponent;
    private Transform hudParent;

    public void SetHeroData(HeroData heroData, HeroTeamEnum heroTeam) {
        HeroData = heroData;
        HeroTeam = heroTeam;
        Initlizate();
    }

    public void Initlizate() {
        animator = transform.GetComponentInChildren<Animator>();
        hudParent = transform.Find("HUDParent");
        heroHUDComponent = ResourceManager.Instance.LoadObject<HeroHUDComponent>(AssetPathConfig.HUD + "HPObject" + HeroTeam.ToString(),
            BattleWorldNodes.Instance.HUDWindowTrans);
        heroHUDComponent.Init(this);
    }

    public override void Update() {
        base.Update();
        UpdateHeroHUD();
    }

    public void UpdateHeroHUD() {
        if (heroHUDComponent != null && LogicObj != null && hudParent != null) {
            heroHUDComponent.transform.localPosition = World3DToCanvasPos(hudParent.position);
        }
    }

    public void PlayAnim(string animName) {
        animator.SetTrigger(animName);
    }

    public void SetAnimState(AnimState state) {
        animator.speed = state == AnimState.StopAnim ? 0 : 1;
    }

    public void UpdateHPHud(int damage, float hpRate) {
        GameObject damageTextObj = ResourceManager.Instance.LoadObject(AssetPathConfig.HUD + (damage > 0 ? "DamageText" : "RestoreHPText"), BattleWorldNodes.Instance.HUDWindowTrans, resetScale:true);
        Vector2 pos = World3DToCanvasPos(transform.position);
        damageTextObj.transform.localPosition = new Vector2(pos.x, pos.y + 40);
        damageTextObj.GetComponent<Text>().text = (damage > 0 ? "-" : "+") + Mathf.Abs(damage);
        damageTextObj.transform.DOLocalMoveY(damageTextObj.transform.localPosition.y + 100, 1f);
        damageTextObj.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetDelay(1.2f);
        Destroy(damageTextObj, 3);
        heroHUDComponent.UpdateHPSlider(hpRate);
    }

    public void UpdateAngerHud(float rate) {
        if (heroHUDComponent != null) {
            heroHUDComponent.UpdateAngerSlider(rate);
        }
    }

    public Vector3 World3DToCanvasPos(Vector3 targetPos) {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(BattleWorldNodes.Instance.camera3D, targetPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(BattleWorldNodes.Instance.HUDWindowTrans as RectTransform,
            screenPos, BattleWorldNodes.Instance.uiCamera, out Vector2 uguiLocalPos);
        return uguiLocalPos;
    }

    public void Death() {
        PlayAnim("Death");
        heroHUDComponent.gameObject.SetActive(false);
    }

    public override void OnRelease() {
        base.OnRelease();
        heroHUDComponent.OnRelease();
    }
}
