using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "BuffConfig", menuName = "BuffConfig", order = 0)]
public class BuffConfig : ScriptableObject {

    [HideInInspector]
    public bool hideDamagePercentage = true;

    [LabelText("Buff图标"), LabelWidth(0.1f), PreviewField(70, ObjectFieldAlignment.Left), SuffixLabel("Buff图标")]
    public Sprite buffIcon;
    [LabelText("BuffID")]
    public int buffId;
    [LabelText("Buff名称")]
    public string buffName;
    [LabelText("最大叠加层数")]
    public int maxStack;
    [LabelText("持续时间ms")]
    public int buffDurationTimeMs;
    [LabelText("持续回合")]
    public int buffDurationRound;
    [LabelText("触发间隔")]
    public int buffTriggerIntervalMs;
    [LabelText("触发概率")]
    public int buffTriggerProbability;

    [LabelText("Buff类型")]
    public BuffType buffType;
    [LabelText("Buff状态")]
    public BuffState buffState;
    [LabelText("Buff触发方式")]
    public BuffTriggerType triggerType;
    [LabelText("Buff伤害类型"), OnValueChanged("DamageTypeChange")]
    public BuffDamageType damageType;
    [LabelText("Buff伤害百分比"), ProgressBar(0, 500), HideIf("hideDamagePercentage")]
    public int damagePercentage;

    // 渲染层数据
    [LabelText("Buff音效")]
    public AudioClip buffAudio;
    [LabelText("Buff特效")]
    public string buffEffect;
    [LabelText("Buff描述:"), MultiLineProperty(4), HideLabel]
    public string buffDes;

    public void DamageTypeChange(BuffDamageType damageType) {
        hideDamagePercentage = damageType != BuffDamageType.AtkPercentage && damageType != BuffDamageType.HpPercentage
            && damageType != BuffDamageType.None;
    }
}

public enum BuffType {
    [LabelText("伤害Buff")] DamageBuff, // 伤害
    [LabelText("增益Buff")] Buff, // 增益
    [LabelText("减益Buff")] Debuff, // 减益
    [LabelText("控制Buff")] Control // 控制
}

public enum BuffState {
    [LabelText("无配置")] None,
    [LabelText("百分比减伤")] PercentageReduceDamage,
    [LabelText("伤害加深")] DamageDeepening,
    [LabelText("生命值回复增加")] HpRecoveryIncrease,
    [LabelText("生命值回复减少")] HpRecoveryReduce,
    [LabelText("灼烧")] Burn,
    [LabelText("净化")] Purify,
    [LabelText("冰冻")] Forzen,
    [LabelText("攻击力增加")] AtkAdd,
    [LabelText("防御力增加")] DefAdd
}

public enum BuffTriggerType {
    [LabelText("即时性，一次伤害")] OneDamageRealTime,
    [LabelText("即时性，多段伤害")] MultiDamageRealTime,
    [LabelText("回合开始时伤害")] DamgeRoundStart,
    [LabelText("回合结束时伤害")] DamgeRoundEnd
}

public enum BuffDamageType {
    [LabelText("无配置")] None,
    [LabelText("普通伤害")] NormalAttack,
    [LabelText("真实伤害")] Real,
    [LabelText("攻击力百分比伤害")] AtkPercentage,
    [LabelText("生命值百分比伤害")] HpPercentage,
    [LabelText("无伤害，控制型")] NoDamageControl
}
