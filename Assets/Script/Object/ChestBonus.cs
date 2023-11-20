using DG.Tweening;
using UnityEngine;

public class ChestBonus : MonoBehaviour
{
    public ChestType type;
    [SerializeField] ChestObject chest;
    private ObjectAnimator ObjAnimator => GetComponentInChildren<ObjectAnimator>();
    private void Start()
    {
        ObjAnimator.ActionOne = DropItems;
    }
    private void DropItems()
    {
        switch (type)
        {
            case ChestType.Wood:
                RatioDropWood();
                break;
            case ChestType.Platium:
                RatioDropPlatium();
                break;
            case ChestType.Gold:
                RatioDropGold();
                break;
            case ChestType.KillBoss:
                RatioDropKillBoss();
                break;
        }
    }
    private void RatioDropWood()
    {
        ObjQuantity(chest.Coin,3);
        ObjQuantity(chest.HeartBlue, 1);
        ObjQuantity(chest.HeartRed, 1);
    }
    private void RatioDropPlatium()
    {
        ObjQuantity(chest.Coin, 10);
        ObjQuantity(chest.HeartBlue, 1);
        ObjQuantity(chest.HeartRed, 1);
    }
    private void RatioDropGold()
    {
        ObjQuantity(chest.Coin, 18);
        ObjQuantity(chest.Tarot, 1);
        ObjQuantity(chest.Necklace, 1);
        ObjQuantity(chest.BluePrint, 1);
    }
    private void RatioDropKillBoss()
    {
        ObjQuantity(chest.Coin, 25);
        ObjQuantity(chest.Bone, 12);
        ObjQuantity(chest.CommandmentStone, 1);
        ObjQuantity(chest.BluePrint, 1);
        ObjQuantity(chest.Necklace, 1);
    }
    private void ObjQuantity(GameObject obj,int quantity)
    {
        if (obj == null || quantity == 0)
            return;
        this.WaitDelayCall(quantity, 0f, () =>
        {
            GameObject _obj = Instantiate(obj, transform.position, transform.rotation);
            _obj.transform.DOJump(new Vector3(Random.Range(-2f, 2f), 0.5f, Random.Range(-0.1f, -2f)) + transform.position, Random.Range(0.5f, 4f), 1, 0.3f);
        });
    }
}
