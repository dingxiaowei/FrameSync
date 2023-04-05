using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SkillConfig", menuName = "SkillConfig", order = 0)]
public class SkillConfig : ScriptableObject {

    [HideInInspector]
    public bool hideBullet = true;
    [HideInInspector]
    public bool hideDamagePercentage = true;

    [LabelText("技能图标"), LabelWidth(0.1f), PreviewField(70, ObjectFieldAlignment.Left), SuffixLabel("技能图标")]
    public Sprite skillIcon;
    [LabelText("技能id")]
    public int skillId;
    [LabelText("技能名称")]
    public string skillName;
    [LabelText("技能所需怒气"), ProgressBar(0, 150)]
    public int needRageValue = 100;
    [LabelText("技能前摇时间ms"), Tooltip("比如：从当前位置移动到目标位置所需时间")]
    public int skillShakeBeforeTimeMs;
    [LabelText("技能攻击持续时间"), Tooltip("比如：英雄播放一个连击动画，这个攻击时长 = 连击动画时长，或者是子弹飞行时间")]
    public int skillAttackDurationMs;
    [LabelText("技能后摇时间ms")]
    public int skillShakeAfterTimeMs;
    [LabelText("技能类型"), OnValueChanged("SkillTypeChange")]
    public SkillType skillType;
    [LabelText("技能生效目标")]
    public TargetType targetType;
    [LabelText("技能攻击类型")]
    public SkillAttackType skillAttackType;
    [LabelText("技能伤害类型"), OnValueChanged("DamageTypeChange")]
    public DamageType damageType;
    [LabelText("伤害百分比"), ProgressBar(0, 500), HideIf("hideDamagePercentage")]
    public int damagePercentage;

    // 渲染层数据
    [LabelText("子弹预制体"), HideIf("hideBullet")]
    public string bullet;
    [LabelText("技能动画"), TitleGroup("技能渲染", "所有英雄渲染数据会在技能开始释放时触发")]
    public string skillAnim;
    [LabelText("技能音效")]
    public AudioClip skillAudio;
    [LabelText("技能特效")]
    public string skillEffect;
    [LabelText("技能命中特效")]
    public string skillHitEffect;
    [TitleGroup("附加buff", "技能生效的一瞬间 附加技能指定的多个buff")]
    public int[] addBuffs;
    [LabelText("技能描述:"), MultiLineProperty(4), HideLabel]
    public string skillDes;

    public void SkillTypeChange(SkillType skillType) {
        hideBullet = skillType != SkillType.Ballistic;
    }

    public void DamageTypeChange(DamageType damageType) { 
        hideDamagePercentage = damageType != DamageType.AtkPercentage && damageType != DamageType.HpPercentage;
    }
}

public enum SkillType {
    [LabelText("普攻型")] MoveToAttack,
    [LabelText("移动到敌人中心攻击")] MoveToEnemyCenter,
    [LabelText("移动到中心")] MoveToCenter,
    [LabelText("吟唱型")] Chant,
    [LabelText("弹道型")] Ballistic
}

public enum TargetType {
    [LabelText("未配置")] None,
    [LabelText("队友")] Teammate,
    [LabelText("敌人")] Enemy
}

public enum SkillAttackType {
    [LabelText("单体")] SingleTarget,
    [LabelText("所有")] AllHero,
    [LabelText("后排")] BackRowHero,
    [LabelText("前排")] FrontRowHero,
    [LabelText("同列")] SameColumnHero
}

public enum DamageType {
    [LabelText("无伤害")] None,
    [LabelText("普通伤害")] NormalDamage,
    [LabelText("真实伤害")] RealDamage,
    [LabelText("攻击百分比伤害")] AtkPercentage,
    [LabelText("生命值百分比伤害")] HpPercentage
}
