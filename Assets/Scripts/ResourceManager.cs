using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager> {

    public GameObject LoadObject(string path, Transform parent = null, bool resetPos = false, bool resetScale = false, bool resetRotate = false) {
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>(path), parent);
        if (resetPos) {
            obj.transform.localPosition = Vector3.zero;
        }
        if (resetScale) {
            obj.transform.localScale = Vector3.one;
        }
        if (resetRotate) {
            obj.transform.localRotation = Quaternion.identity;
        }
        return obj;
    }

    public T LoadObject<T>(string path, Transform parent = null) {
        GameObject obj = Object.Instantiate(Resources.Load<GameObject>(path), parent);
        T t = obj.GetComponent<T>();
        return t;
    }

    public T LoadAsset<T>(string path) where T : Object {
        return Resources.Load<T>(path);
    }
}
