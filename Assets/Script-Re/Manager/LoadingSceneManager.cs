using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] RectTransform circle;
    [SerializeField] TMP_Text loadingText = null;
    Tween myTween;


    private void Start()
    {
        StartCoroutine(LoadSenceAsync());
        myTween = circle.DORotate(new Vector3(0f, 0f, 360f), 3, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
    }

    public IEnumerator LoadSenceAsync()
    {
        AsyncOperation handle = SceneManager.LoadSceneAsync(LoadSceneExtension.sceneToLoad);
        while (!handle.isDone)
        {
            loadingText.text = "Loading... " + handle.progress + "%";
            yield return new WaitForSeconds(0.5f);
        }
        myTween.Kill(); 
    }
}
