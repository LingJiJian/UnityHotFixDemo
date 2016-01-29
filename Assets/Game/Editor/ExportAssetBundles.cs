using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text;

public class ExportAssetBundles : Editor
{
    //[MenuItem("热更打包/Build Asset Bundles")]
    static void BuildABs()
    {
        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        buildMap[0].assetBundleName = "prefabBundles";
        string[] names = {
            "Assets/Prefabs/Capsule.prefab",
            "Assets/Prefabs/Cube.prefab"};
        buildMap[0].assetNames = names;
        /*
        buildMap[1].assetBundleName = "sceneBundles";
        string[] names2 = {
            "Assets/Scenes/scene.unity"};
        buildMap[1].assetNames = names2;
        */

        BuildPipeline.BuildAssetBundles("Assets/Abs", buildMap);
    }

    [MenuItem("热更打包/打包资源")]
    static void ExportResource()
    {
        // 打开保存面板，获得用户选择的路径  
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "assetbundle");

        if (path.Length != 0)
        {
            // 选择的要保存的对象  
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            //打包  
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows);
        }
    }

    [MenuItem("热更打包/打包场景")]
    static void ExportScene()
    {
        // 打开保存面板，获得用户选择的路径  
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");

        if (path.Length != 0)
        {
            
            //打包  
            BuildPipeline.BuildPlayer(null, path, BuildTarget.StandaloneWindows, BuildOptions.BuildAdditionalStreamedScenes);
        }
    }

    [MenuItem("热更打包/创建版本文件")]
    static void createVersionFile()
    {
        string resPath = "C:/Users/Administrator/Desktop/tmp/";
        // 获取Res文件夹下所有文件的相对路径和MD5值  
        string[] files = Directory.GetFiles(resPath, "*", SearchOption.AllDirectories);
        StringBuilder versions = new StringBuilder();

        for (int i = 0, len = files.Length; i < len; i++)
        {
            string filePath = files[i];
            string extension = filePath.Substring(files[i].LastIndexOf("."));
            if (extension == ".unity3d" ||
                extension == ".assetbundle")
            {
                string relativePath = filePath.Replace(resPath, "").Replace("\\", "/");
                string md5 = ExportAssetBundles.MD5File(filePath);
                versions.Append(relativePath).Append(",").Append(md5).Append("\n");
            }
        }
        // 生成配置文件  
        FileStream stream = new FileStream(resPath + "version.ver", FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(versions.ToString());
        stream.Write(data, 0, data.Length);
        stream.Flush();
        stream.Close();

        Debug.Log(" 版本文件： " + resPath + "version.ver");
    }

    public static string MD5File(string file)
    {
        try
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("md5file() fail, error:" + ex.Message);
        }
    }
}
