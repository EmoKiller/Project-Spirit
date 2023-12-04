using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIDialogBox : MonoBehaviour
{
    public enum Script
    {
        UIDialogBox
    }
    [Header("Line")]
    [SerializeField] RectTransform lineTop;
    [SerializeField] RectTransform lineBottom;
    [SerializeField] TMP_Text showText;
    [SerializeField] GameObject dialogBox;
    [SerializeField] PopUpTalkObject talkScript = null;
    [SerializeField] int index = 0;
    //[SerializeField] bool End

    private void Start()
    {
        dialogBox.SetActive(false);
        gameObject.SetActive(false);
        EventDispatcher.Addlistener<string>(Script.UIDialogBox,Events.DialogBoxChangeTalkScript, ChangeTalkScript);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (showText.text == talkScript.TextList[index])
            {
                NextScript();
                return;
            }
            StopAllCoroutines();
            showText.text = talkScript.TextList[index];
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeTalkScript("IntroGame1");
        }
    }
    private void ChangeTalkScript(string nameScript)
    {
        gameObject.SetActive(true);
        dialogBox.SetActive(true);
        talkScript = Resources.Load<PopUpTalkObject>(string.Format("ScriptTalk/{0}", nameScript));
        index = 0;
        showText.text = string.Empty;
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraChangeTarget, talkScript.pointTalk[index]);
        StartCoroutine(TypeLine());
    }
    private void NextScript()
    {
        if (index == talkScript.TextList.Count - 1)
        {
            EventDispatcher.Publish(TriggerWaitAction.Script.TriggerTalk, Events.TheScriptTalkEnd);
            gameObject.SetActive(false);
            return;
        }
        showText.text = string.Empty;
        index++;
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraChangeTarget, talkScript.pointTalk[index]);
        StartCoroutine(TypeLine());

    }
    IEnumerator TypeLine()
    {
        foreach (char c in talkScript.TextList[index].ToCharArray())
        {
            showText.text += c;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void Removed()
    {
        gameObject.SetActive(false);
    }
    private void Open()
    {
        lineTop.gameObject.SetActive(true);
        lineBottom.gameObject.SetActive(true);
        lineTop.DOAnchorPos3DY(-38f,2);
        lineBottom.DOAnchorPos3DY(38,2);
    }
    private void Close()
    {
        lineTop.DOAnchorPos3DY(38f, 2);
        lineBottom.DOAnchorPos3DY(-38, 2);
        this.DelayCall(2f, () =>
        {
            lineTop.gameObject.SetActive(false);
            lineBottom.gameObject.SetActive(false);
        });
    }
}

