using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    public void SetEffectPos(VInt3 logicPosition) {
        transform.position = logicPosition.vec3;
        Destroy(gameObject, 3);
    }
}
