using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWeapon : MonoBehaviour
{
    public enum Script
    {
        InfoWeapon
    }
    [SerializeField] RectTransform imageL;
    [SerializeField] RectTransform imageR;
    [SerializeField] GameObject imageUpDownDamage;
    [SerializeField] GameObject imageUpDownSpeed;
    [SerializeField] TMP_Text textNameWeapon;
    [SerializeField] TMP_Text textQuoteWeapon;
    [SerializeField] TMP_Text textDescriptionWeapon;
    [SerializeField] TMP_Text texrDamage;
    [SerializeField] TMP_Text textSpeed;
    private void Start()
    {
        gameObject.SetActive(false);
        EventDispatcher.Addlistener<string, string, string, float, float>(Script.InfoWeapon, Events.UpdateInfoWeapon, UpdateInfoWeapon);
        EventDispatcher.Addlistener(Script.InfoWeapon, Events.SetDefault, SetDefault);
    }
    private void UpdateInfoWeapon(string nameWeapon,string quoteWeapon,string descriptionWeapon, float damage, float speed)
    {
        gameObject.SetActive(true);
        imageL.anchoredPosition = new Vector2(nameWeapon.Length * -12, 0);
        imageR.anchoredPosition = new Vector2(nameWeapon.Length * 12, 0);
        textNameWeapon.text = nameWeapon;
        textQuoteWeapon.text = quoteWeapon;
        textDescriptionWeapon.text = descriptionWeapon;
        texrDamage.text = damage.ToString();
        SetUpDownValue(imageUpDownDamage, damage);
        textSpeed.text = speed.ToString();
        SetUpDownValue(imageUpDownSpeed, speed);

    }
    private void SetDefault()
    {
        gameObject.SetActive(false);
        imageL.anchoredPosition = new Vector2(0, 0);
        imageR.anchoredPosition = new Vector2(0, 0);
        imageUpDownDamage.transform.DORotate(new Vector3(0,0,0),0);
        imageUpDownSpeed.transform.DORotate(new Vector3(0, 0, 0), 0);
        
    }
    private void SetUpDownValue(GameObject trans, float damage)
    {
        Image img = trans.GetComponent<Image>();
        
        if (damage > 0)
        {
            trans.transform.DORotate(new Vector3(0, 0, -90), 0);
            img.color = Color.green;
            return;
        }
        if (damage < 0)
        {
            trans.transform.DORotate(new Vector3(0, 0, 90), 0);
            img.color = Color.red;
            return;
        }
        img.color = Color.white;
    }
}
