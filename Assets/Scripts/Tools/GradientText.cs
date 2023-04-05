using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ************字体颜色渐变****************************挂在Text上调节颜色即可********************************
/// </summary>
[AddComponentMenu("UI/Effects/GradientText")]
public class GradientText : BaseMeshEffect {
    [SerializeField]
    private Color32 topColor = Color.white;

    [SerializeField]
    private Color32 bottomColor = Color.black;

    public override void ModifyMesh(VertexHelper vh) {
        if (!IsActive() || vh.currentVertCount == 0)
            return;
        List<UIVertex> vertices = new List<UIVertex>();
        vh.GetUIVertexStream(vertices);
        float bottomY = vertices[0].position.y;
        float topY = vertices[0].position.y;
        for (int i = 1; i < vertices.Count; i++) {
            if (vertices[i].position.y > topY) {
                topY = vertices[i].position.y;
            } else if (vertices[i].position.y < bottomY) {
                bottomY = vertices[i].position.y;
            }
        }
        float uiElementHeight = topY - bottomY;
        UIVertex v = new UIVertex();
        for (int i = 0; i < vh.currentVertCount; i++) {
            vh.PopulateUIVertex(ref v, i);
            v.color = Color32.Lerp(bottomColor, topColor, (v.position.y - bottomY) / uiElementHeight);
            vh.SetUIVertex(v, i);
        }
    }
}
