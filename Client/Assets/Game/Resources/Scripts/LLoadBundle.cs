using UnityEngine;
using System.Collections;

public class LLoadBundle : MonoBehaviour {

    public static Hashtable bundles = new Hashtable();

    public void UnLoadAllBundle()
    {
        foreach (AssetBundle bundle in bundles.Values)
        {
            if (bundle != null)
                bundle.Unload(false);
        }
        bundles.Clear();
    }

    //释放某AssetBundle
    public void UnLoadBundle(AssetBundle bundle)
    {
        string key = "";
        foreach (DictionaryEntry de in bundles)
        {
            if (bundle == (AssetBundle)de.Value)
            {
                key = de.Key.ToString();
                break;
            }
        }

        if (bundles.ContainsKey(key))
        {
            bundles.Remove(key);
        }
        if (bundle != null)
            bundle.Unload(false);
    }
    //释放某AssetBundle
    public void UnLoadBundle(string key)
    {
        if (bundles.ContainsKey(key))
        {
            AssetBundle bundle = bundles[key] as AssetBundle;
            if (bundle != null)
            {
                bundle.Unload(false);
            }
            bundles.Remove(key);
        }
    }
}
