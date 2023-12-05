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
    [SerializeField] TMP_Text nameSpeaking;
    [SerializeField] TMP_Text showText;
    [SerializeField] RectTransform dialogBox;
    [SerializeField] PopUpTalkObject talkScript = null;
    [SerializeField] int index = 0;
    private void Start()
    {
        dialogBox.gameObject.SetActive(false);
        gameObject.SetActive(false);
        EventDispatcher.Addlistener<string>(Script.UIDialogBox, Events.DialogBoxChangeTalkScript, ChangeTalkScript);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (showText.text == talkScript.TextList[index])
            {
                ResetSizeDialogBox();
                Debug.Log(talkScript.TextList[index].Length);
                NextScript();
                return;
            }
            StopAllCoroutines();
            showText.text = talkScript.TextList[index];
        }
    }
    private void ChangeTalkScript(string nameScript)
    {
        gameObject.SetActive(true);
        dialogBox.gameObject.SetActive(true);
        index = 0;
        talkScript = Resources.Load<PopUpTalkObject>(string.Format("ScriptTalk/{0}", nameScript));
        CheckStringLength();
        nameSpeaking.text = talkScript.NameSpeaking;
        showText.text = string.Empty;
        StartCoroutine(TypeLine());
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraChangeTarget, talkScript.pointTalk[index]);
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
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraChangeTarget, talkScript.pointTalk[index]);
        index++;
        StartCoroutine(TypeLine());
        CheckStringLength();
    }
    private void CheckStringLength()
    {
        if (talkScript.NameSpeaking == "")
            nameSpeaking.gameObject.SetActive(false);
        else
        {
            dialogBox.sizeDelta += new Vector2(0, 40);
            nameSpeaking.gameObject.SetActive(true);
        } 
        if (talkScript.TextList[index].Length >= 75)
        {
            dialogBox.sizeDelta += new Vector2(0, 80);
            return;
        }
        if (talkScript.TextList[index].Length >= 40)
        {
            dialogBox.sizeDelta += new Vector2(0, 50);
            return;
        }
    }
    private void ResetSizeDialogBox()
    {
        dialogBox.sizeDelta = new Vector2(924.5f, 100);
    }
    IEnumerator TypeLine()
    {
        foreach (char c in talkScript.TextList[index].ToCharArray())
        {
            showText.text += c;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void Open()
    {
        lineTop.gameObject.SetActive(true);
        lineBottom.gameObject.SetActive(true);
        lineTop.DOAnchorPos3DY(-38f, 2);
        lineBottom.DOAnchorPos3DY(38, 2);
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

