using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigCenter {

    public static List<HeroData> HeroDataList { get; private set; }

    public static void Initialized() {
        LoadHeroConfig();
    }

    public static void LoadHeroConfig() {
#if RENDER_LOGIC
        TextAsset text = ResourceManager.Instance.LoadAsset<TextAsset>("Config/Hero");
        HeroDataList = JsonConvert.DeserializeObject<List<HeroData>>(text.text);
        Debugger.Log("hero count = " + HeroDataList.Count);
#else

#endif
    }

    public static HeroData GetHeroData(int heroId) {
        foreach (HeroData item in HeroDataList) {
            if (item.id == heroId) {
                return item;
            }
        }
        return null;
    }
}
