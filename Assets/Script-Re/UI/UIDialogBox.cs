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
    [SerializeField] string saveText = "";
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
            if (showText.text == saveText)
            {
                ResetSizeDialogBox();
                NextScript();
                return;
            }
            StopAllCoroutines();
            showText.text = saveText;
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
        FullText();
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
        index++;
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraChangeTarget, talkScript.pointTalk[index]);
        FullText();
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
        bool isTextColor = false;
        foreach (char c in talkScript.TextList[index].ToCharArray())
        {
            if(c == '*') isTextColor = !isTextColor;
            if (c == '*') continue;
            if(isTextColor)
            {
                string textColor = $"<color=orange>{c}</color>";
                showText.text += textColor;
                yield return new WaitForSeconds(0.1f);
                continue;
            }
            showText.text += c;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void FullText()
    {
        saveText = string.Empty;
        bool isTextColor = false;
        foreach (char c in talkScript.TextList[index].ToCharArray())
        {
            if (c == '*') isTextColor = !isTextColor;
            if (c == '*') continue;
            if (isTextColor)
            {
                string textColor = $"<color=orange>{c}</color>";
                saveText += textColor;
                continue;
            }
            saveText += c;
        }
        
    }
}