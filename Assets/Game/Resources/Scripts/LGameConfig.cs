using UnityEngine;
using System.IO;
using System.Xml;

public class LGameConfig  {

    // The config file path.
    public static readonly string CONFIG_FILE = "config.xml";
    // The lua data folder name.
    public static readonly string DATA_CATAGORY_LUA = "Lua";
    // The lua file affix.
    public static readonly string FILE_AFFIX_LUA = ".lua";
    // The lua files zip name.
    public static readonly string UPDATE_FILE_ZIP = "data.zip";
    // is activate debug
    public bool isDebug = false;

    // The local file url prefix. (For assetbundle.)
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    public static readonly string LOCAL_URL_PREFIX = "file:///";
#else
	public static readonly string LOCAL_URL_PREFIX = "file://";
#endif

    // The asset path in persistent asset path.
    private string m_strPersistAssetPath = string.Empty;

    // The asset path in streaming asset path.
    private string m_strStreamAssetPath = string.Empty;

    // The asset path in caching path.
    private string m_strCachingAssetPath = string.Empty;

    // The global instance.
    private static LGameConfig m_cInstance = null;

    /**
     * Constructor.
     * 
     * @param void.
     * @return void.
     */
    private LGameConfig()
    {
        if (null != m_cInstance)
        {
            init();
            return;
        }

        m_cInstance = this;
    }

    /**
     * initialization
     * 
     */
    private void init()
    {
        LoadConfig();
    }

    /**
     * Destructor.
     * 
     * @param void.
     * @return void.
     */
    ~LGameConfig()
    {
        m_cInstance = null;
    }

    public static LGameConfig GetInstance()
    {
        if (null == m_cInstance)
        {
            new LGameConfig();
        }
        return m_cInstance;
    }

    // Get persistent assets path.
    public string PersistentAssetsPath
    {
        get
        {
            if (string.IsNullOrEmpty(m_strPersistAssetPath))
            {
                m_strPersistAssetPath = Application.persistentDataPath + Path.DirectorySeparatorChar;
            }

            return m_strPersistAssetPath;
        }
    }

    // Get streaming assets path.
    public string StreamingAssetsPath
    {
        get
        {
            if (string.IsNullOrEmpty(m_strStreamAssetPath))
            {
                m_strStreamAssetPath = Application.streamingAssetsPath + Path.DirectorySeparatorChar;
            }

            return m_strStreamAssetPath;
        }
    }

    // Get caching assets path.
    public string CachingAssetsPath
    {
        get
        {
            if (string.IsNullOrEmpty(m_strCachingAssetPath))
            {
                m_strCachingAssetPath = Application.temporaryCachePath + Path.DirectorySeparatorChar;
            }

            return m_strCachingAssetPath;
        }
    }

    /**
     * Get the final load url.
     * 
     * @param string strPathName - The path name of the file with dir except the base url.
     * @return string - The final full url load string.
     */
    public string GetLoadUrl(string strPathName)
    {
        string strFilePath = PersistentAssetsPath + strPathName;
        if (File.Exists(strFilePath))
        {
            return strFilePath;
        }
        else
        {
            strFilePath = StreamingAssetsPath + strPathName;
            return strFilePath;
        }
    }

    /**
     * Get the final load url for directory.
     * 
     * @param string strPathName - The path dir name of the file with dir except the base url.
     * @return string - The final full url load string for the path dir.
     */
    public string GetLoadUrlForDir(string strPathName)
    {
        string strFilePath = PersistentAssetsPath + strPathName;
        if (Directory.Exists(strFilePath))
        {
            return strFilePath;
        }
        else
        {
            strFilePath = StreamingAssetsPath + strPathName;
            return strFilePath;
        }
    }

    private void LoadConfig()
    {
        XmlDocument doc = new XmlDocument();
        string path = PersistentAssetsPath + Path.DirectorySeparatorChar + CONFIG_FILE;
        if (!File.Exists(path))
        {
            path = StreamingAssetsPath + Path.DirectorySeparatorChar + CONFIG_FILE;
        }
        doc.Load(path);    //加载Xml文件  

        XmlElement rootElem = doc.DocumentElement;   //获取根节点  

        XmlNodeList debugs = rootElem.GetElementsByTagName("Debug"); 
        isDebug = debugs[0].InnerText == "1";

    }

}
