using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ChestBonus : MonoBehaviour
{
    public ChestType type;
    private ObjectAnimator ObjAnimator => GetComponentInChildren<ObjectAnimator>();
    private void Start()
    {
        ObjAnimator.ActionOne = DropItems;
    }
    private void DropItems()
    {
        Debug.Log("Chest Type " + " " + type);
        foreach (var item in ConfigDataHelper.GameConfig.ChestConfig[type].itemsCanDrop)
        {
            Debug.Log(item.Key);
            AssetManager.Instance.ItemDropPrefab.GetComponent<ObjectDropOnWorld>().UpdateSprite(item.Key.ToString());
            for (int i = 0; i < item.Value.value; i++)
            {
                GameObject _obj = Instantiate(AssetManager.Instance.ItemDropPrefab, transform.position, transform.rotation);
                _obj.transform.DOJump(new Vector3(Random.Range(-2f, 2f), 0.5f, Random.Range(-0.1f, -2f)) + transform.position, Random.Range(0.5f, 4f), 1, 0.3f);
            }
        }
    }
}
