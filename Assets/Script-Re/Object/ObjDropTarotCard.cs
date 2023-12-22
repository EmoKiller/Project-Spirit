using UnityEngine;

public class ObjDropTarotCard : ObjectDropOnWorld, IPool
{
    private int _numberCard = 1;
    public int NumberCard
    {
        get { return _numberCard; }
        set { _numberCard = Mathf.Clamp(value, 1, 3); }
    }
    public string objectName => GetType().Name;

    protected override void PublishEvent()
    {
        UIManager.Instance.ShowTarotCard(_numberCard);
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        ObjectPooling.Instance.PushToPoolDropTarotCard(this);
        gameObject.SetActive(false);
    }
}
