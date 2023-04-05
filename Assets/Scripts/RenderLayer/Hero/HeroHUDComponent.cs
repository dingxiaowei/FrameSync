using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HeroHUDComponent : MonoBehaviour
{
    private HeroRender heroRender;

    public Slider hpSlider;
    public Slider hpDamageAnimSlider;
    public Slider angerSlider;
    public Transform buffParent;

    public void Init(HeroRender heroRender) {
        this.heroRender = heroRender;
    }

    public void UpdateHPSlider(float value) {
        hpSlider.value = value;
        hpDamageAnimSlider.DOValue(value, 0.5f).SetDelay(0.4f);
        if (value <= 0) {
            gameObject.SetActive(false);
        }
    }

    public void UpdateAngerSlider(float value) {
        angerSlider.value = value;
        angerSlider.gameObject.SetActive(value != 0);
    }

    public void OnRelease() {
        Destroy(gameObject);
    }
}
