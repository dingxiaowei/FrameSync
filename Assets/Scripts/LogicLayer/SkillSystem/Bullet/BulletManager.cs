using System;
using System.Collections.Generic;

public class BulletManager : SingletonMono<BulletManager>, ILogicBehaviour {

    public List<BulletLogic> bulletList = new List<BulletLogic>();
    public void OnCreate() {

    }

    public void CreateBullet(string bulletPrefab, LogicObject attacker, LogicObject target, VInt flyTime, Action onHitComplete) {
        BulletLogic bulletLogic = new BulletLogic(attacker, target, flyTime, onHitComplete);
#if RENDER_LOGIC
        BulletRender bulletRender = ResourceManager.Instance.LoadObject<BulletRender>(AssetPathConfig.SKILL_EFFECT + bulletPrefab);
        bulletRender.SetLogicObject(bulletLogic);
        bulletLogic.SetRenderObject(bulletRender);
#endif
        bulletLogic.OnCreate();
        bulletList.Add(bulletLogic);
    }

    public void OnDestroy() {
        
    }

    internal void RemoveBullet(BulletLogic bulletLogic) {
        for (int i = bulletList.Count - 1; i >= 0; i--) {
            if (bulletLogic == bulletList[i]) {
                bulletLogic.OnDestroy();
                bulletList.RemoveAt(i);
            }
        }
    }

    public void OnLogicFrameUpdate() {
        for (int i = bulletList.Count - 1; i >= 0; i--) {
            bulletList[i].OnLogicFrameUpdate();
        }
    }
}
