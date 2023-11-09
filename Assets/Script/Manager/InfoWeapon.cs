using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class InfoWeapon : MonoBehaviour
{
    [SerializeField] RectTransform imageL;
    [SerializeField] RectTransform imageR;
    [SerializeField] TMP_Text textNameWeapon;
    [SerializeField] TMP_Text textQuoteWeapon;
    [SerializeField] TMP_Text textDescriptionWeapon;
    [SerializeField] TMP_Text texrDamage;
    [SerializeField] TMP_Text textSpeed;

    private void Awake()
    {
        EventDispatcher.Addlistener<string, string, string, string, string>(ListScript.InfoWeapon, Events.UpdateValue, UpdateInfoWeapon);
    }
    private void UpdateInfoWeapon(string nameWeapon,string quoteWeapon,string descriptionWeapon,string damage, string speed)
    {
        imageL.anchoredPosition = new Vector2(nameWeapon.Length * -9, 0); ;
        imageR.anchoredPosition = new Vector2(nameWeapon.Length * 9, 0);
        textNameWeapon.text = nameWeapon;
        textQuoteWeapon.text = quoteWeapon;
        textDescriptionWeapon.text = descriptionWeapon;
        texrDamage.text = damage;
        textSpeed.text = speed;
    }

}
