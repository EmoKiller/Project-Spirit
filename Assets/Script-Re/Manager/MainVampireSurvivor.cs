using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainVampireSurvivor : MonoBehaviour
{
    [SerializeField] GameObject objMainSelect;
    [SerializeField] Button buttonStartGame;
    public Button ButttonStartGame { get { return ButttonStartGame; } }
    public GameObject ObjMainSelect { get { return objMainSelect; } }
    [Header("PowerUP")]
    [SerializeField] private PowerUP _PowerUP = null;
    private void Awake()
    {
        PowerUP.ShowButton.onClick.AddListener(OnBuy);
        buttonStartGame.onClick.AddListener(OnClickButtonStart);
    }
    public PowerUP PowerUP
    {
        get => this.TryGetMonoComponentInChildren(ref _PowerUP);
    }
    private void OffMainSelect()
    {
        objMainSelect.SetActive(false);
    }
    private void OnBuy()
    {
        AttributePowerUP attri = PowerUP.AttributePowerUPs.Find(e => e.AttributeAdded.Equals(PowerUP.AttributeAdded));
        if (PowerUP.Price > InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.CurrentCoin) && attri.Check.Count - 1 > attri.NumberTick)
        {
            PowerUP.PopupShow.SetActive(false);
            return;
        }
        attri.AddTick(PowerUP.SprActive);
        InfomationPlayerManager.Instance.MinusValueOf(AttributeType.CurrentCoin, PowerUP.Price);
        InfomationPlayerManager.Instance.PowerIncreaseValueOf(PowerUP.AttributeAdded, PowerUP.ValueAdded);
        PowerUP.PopupShow.SetActive(false);
    }
    private void OnClickButtonStart()
    {
        ObseverConstants.OnClickButtonStart?.Invoke();
        LoadSceneExtension.LoadScene(OnScenes.VampireSurvivor.ToString());
        OffMainSelect();
    }
}
