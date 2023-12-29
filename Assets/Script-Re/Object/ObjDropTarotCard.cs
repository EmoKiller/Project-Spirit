using UnityEngine;

public class ObjDropTarotCard : ObjectDropOnWorld
{
    private int _numberCard = 1;
    public int NumberCard
    {
        get { return _numberCard; }
        set { _numberCard = Mathf.Clamp(value, 1, 3); }
    }
    public override string objectName => GetType().Name;

    protected override void PublishEvent()
    {
        UIManager.Instance.ShowTarotCard(_numberCard);
        Hide();
    }

}
