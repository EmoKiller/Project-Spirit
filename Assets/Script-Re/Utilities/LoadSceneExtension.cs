using UnityEngine.SceneManagement;

public class LoadSceneExtension 
{
    public static string sceneToLoad;



    public static void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}
