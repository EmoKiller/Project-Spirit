using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public string ack;
    private GameObject ItemDropObject;
    public GameObject ItemDropPrefab
    {
        get { return ItemDropObject; }
    }

    private void Start()
    {
        InstantiateObj2();
    }
    public void InstantiateObj()
    {
        Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Object/ItemDropObject.prefab").Completed += (handle) =>
        {
            ItemDropObject = handle.Result;
        };
    }
    public void InstantiateObj2()
    {
        ItemDropObject = Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Object/ItemDropObject.prefab").WaitForCompletion();
        
    }
    public void InstantiateItems(string str, Transform pos , Vector3 dir)
    {
        GameObject item = null;
        Addressables.LoadAssetAsync<GameObject>(str).Completed += (handle) =>
        {
            item = handle.Result;
            GameObject obj = Instantiate(item, pos);
            obj.transform.position = dir + new Vector3(0, 1f, 0);
        };
    }
    public Sprite Instantia()
    {
        Sprite item = null;
        Addressables.LoadAssetAsync<Sprite>("Assets/Prefabs/UI/Hearts/Red/Empty.asset").Completed += (handle) =>
        {
            item = handle.Result;
        };
        return item;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
}
