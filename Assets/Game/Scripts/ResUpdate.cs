using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SLua;

public class ResUpdate : MonoBehaviour
{
    public static readonly string VERSION_FILE = "version.ver";
    public static readonly string LOCAL_RES_URL = "file:///" + Application.persistentDataPath+"/";
    public static readonly string SERVER_RES_URL = "file:///C:/Users/Administrator/Desktop/tmp/";
    public static readonly string LOCAL_RES_PATH =  Application.persistentDataPath + "/";

    private Dictionary<string, string> LocalResVersion;
    private Dictionary<string, string> ServerResVersion;
    private List<string> NeedDownFiles;
    private bool NeedUpdateLocalVersionFile = false;

    public GameObject demo;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => {
            checkUpdate();
        });
    }

    protected void checkUpdate()
    {
        //初始化  
        LocalResVersion = new Dictionary<string, string>();
        ServerResVersion = new Dictionary<string, string>();
        NeedDownFiles = new List<string>();

        //加载本地version配置  
        StartCoroutine(DownLoad(LOCAL_RES_URL + VERSION_FILE, delegate (WWW localVersion)
        {
            //保存本地的version  
            ParseVersionFile(localVersion.text, LocalResVersion);
            //加载服务端version配置  
            StartCoroutine(this.DownLoad(SERVER_RES_URL + VERSION_FILE, delegate (WWW serverVersion)
            {
                //保存服务端version  
                ParseVersionFile(serverVersion.text, ServerResVersion);
                //计算出需要重新加载的资源  
                CompareVersion();
                //加载需要更新的资源  
                DownLoadRes();
            }));

        }));
    }

    //依次加载需要更新的资源  
    private void DownLoadRes()
    {
        if (NeedDownFiles.Count == 0)
        {
            UpdateLocalVersionFile();
            return;
        }

        string file = NeedDownFiles[0];
        NeedDownFiles.RemoveAt(0);

        StartCoroutine(this.DownLoad(SERVER_RES_URL + file, delegate (WWW w)
        {
            //将下载的资源替换本地就的资源  
            ReplaceLocalRes(file, w.bytes);
            DownLoadRes();
        }));
    }

    private void ReplaceLocalRes(string fileName, byte[] data)
    {
        string filePath = LOCAL_RES_PATH + fileName;

        FileStream stream = new FileStream(filePath, FileMode.Create);
        stream.Write(data, 0, data.Length);
        stream.Flush();
        stream.Close();
    }

    //显示资源
    private IEnumerator Show()
    {
        using (WWW asset = new WWW(LOCAL_RES_URL + "newRes.assetbundle"))
        {
            yield return asset;

            string code = asset.assetBundle.LoadAsset<TextAsset>("newLua").text;
            LuaSvr l = demo.GetComponent<Demo>().getLuaSvr();
            l.luaState.doString(code);
            asset.Dispose();
        }

        using (WWW scene = new WWW(LOCAL_RES_URL + "newScene.unity3d"))
        {
            yield return scene;
            AssetBundle b = scene.assetBundle; //不要注释这句!!!不然加载不了场景（坑到爆炸
            SceneManager.LoadScene("myScene");
            scene.Dispose();
        }
    }

    //更新本地的version配置  
    private void UpdateLocalVersionFile()
    {
        if (NeedUpdateLocalVersionFile)
        {
            StringBuilder versions = new StringBuilder();
            foreach (var item in ServerResVersion)
            {
                versions.Append(item.Key).Append(",").Append(item.Value).Append("\n");
            }

            FileStream stream = new FileStream(LOCAL_RES_PATH + VERSION_FILE, FileMode.Create);
            byte[] data = Encoding.UTF8.GetBytes(versions.ToString());
            stream.Write(data, 0, data.Length);
            stream.Flush();
            stream.Close();

            Debug.Log("更新资源");
        }
        //加载显示对象  
        StartCoroutine(Show());
    }

    private void CompareVersion()
    {
        foreach (var version in ServerResVersion)
        {
            string fileName = version.Key;
            string serverMd5 = version.Value;
            //新增的资源  
            if (!LocalResVersion.ContainsKey(fileName))
            {
                NeedDownFiles.Add(fileName);
            }
            else
            {
                //需要替换的资源  
                string localMd5;
                LocalResVersion.TryGetValue(fileName, out localMd5);
                if (!serverMd5.Equals(localMd5))
                {
                    NeedDownFiles.Add(fileName);
                }
            }
        }

        //本次有更新，同时更新本地的version.ver  
        NeedUpdateLocalVersionFile = NeedDownFiles.Count > 0;
    }

    private void ParseVersionFile(string content, Dictionary<string, string> dict)
    {
        if (content == null || content.Length == 0)
        {
            return;
        }
        string[] items = content.Split(new char[] { '\n' });
        foreach (string item in items)
        {
            string[] info = item.Split(new char[] { ',' });
            if (info != null && info.Length == 2)
            {
                dict.Add(info[0], info[1]);
            }
        }

    }

    private IEnumerator DownLoad(string url, HandleFinishDownload finishFun)
    {
        WWW www = new WWW(url);
        yield return www;
        if (finishFun != null)
        {
            finishFun(www);
        }
        www.Dispose();
    }

    public delegate void HandleFinishDownload(WWW www);
}