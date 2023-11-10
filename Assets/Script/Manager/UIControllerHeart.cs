using UnityEngine;

public class UIControllerHeart : MonoBehaviour
{

    //public int numberHp;
    //public int Heart;
    //public int currentHp;
    //public List<GameObject> sizeObj = null;
    //[SerializeField] List<GrSpriteHeart> grHeartSpt = new List<GrSpriteHeart>();
    //[SerializeField] List<GrSpriteHeart> grHeartHaftSpt = new List<GrSpriteHeart>();
    //[SerializeField] List<GrHeart> grHeart = new List<GrHeart>();
    //public void Start()
    //{

    //}
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        Creatdtheart(EnemGrHeart.Red, HeartType.Full);
    //        Creatdtheart(EnemGrHeart.Add, HeartType.Half);
    //        Creatdtheart(EnemGrHeart.Blue, HeartType.Full);
    //        Creatdtheart(EnemGrHeart.Black, HeartType.Full);
    //    }
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {

    //        //UpdateStartRound();

    //        CheckHeartInListGr();

    //    }
    //}

    //public void UpdateStartRound()
    //{
    //    currentHp = numberHp;
    //    Heart = currentHp / 2;
    //    if (numberHp % 2 == 0)
    //        UpdateHeart();
    //    else
    //    {
    //        UpdateHeart();
    //        Creatdtheart(EnemGrHeart.Red, HeartType.Half);
    //    }
    //}
    //public void UpdateHeart()
    //{
    //    for (int i = 0; i < Heart; i++)
    //    {
    //        Creatdtheart(EnemGrHeart.Red, HeartType.Full);
    //    }
    //}

    //public void Creatdtheart(EnemGrHeart grType, HeartType heartType)
    //{

    //    CreatdHeartFull(grType, heartType);

    //}
    //public UIsizeObj CheckSize(EnemGrHeart grType, HeartType heartType)
    //{
    //    if (heartType == HeartType.Half)
    //    {
    //        return UIsizeObj.Half;
    //    }
    //    if (grType != EnemGrHeart.Add)
    //    {
    //        return UIsizeObj.Nomal;
    //    }
    //    else 
    //    {
    //        return UIsizeObj.Large;
    //    }

    //}
    //public void CreatdHeartFull(EnemGrHeart grType, HeartType heartType)
    //{
    //    GameObject obj = Instantiate(sizeObj[(int)CheckSize(grType, heartType)], grHeart[(int)grType].rectGr.transform);
    //    grHeart[(int)grType].heart.Add(obj);
    //    RectTransform rec = obj.GetComponent<RectTransform>();
    //    grHeart[(int)grType].rectGr.sizeDelta = new Vector2(grHeart[(int)grType].rectGr.sizeDelta.x + rec.sizeDelta.x,0);
    //    SetImage(obj, grType, heartType);

    //}
    //public void SetImage(GameObject obj, EnemGrHeart grType, HeartType heartType)
    //{
    //    UIHeart scr = obj.GetComponent<UIHeart>();
    //    Image img = obj.GetComponentInChildren<Image>();
    //    scr.heartType = heartType;
    //    if (scr.heartType == HeartType.Full)
    //    {
    //        scr.heartInfo = HeartInfo.Full;
    //        img.sprite = grHeartSpt[(int)grType].sprites[(int)scr.heartInfo];
    //    }
    //    else
    //    {
    //        scr.heartInfo = HeartInfo.Half;
    //        img.sprite = grHeartHaftSpt[(int)grType].sprites[(int)scr.heartInfo];
    //    }
    //}

    //public void CheckHeartInListGr()
    //{
    //    for (int i = 3; i > -1; i--)
    //    {
    //        for (int j = grHeart[i].heart.Count; j > 0; j--)
    //        {
    //            UIHeart scr = grHeart[i].heart[j-1].GetComponent<UIHeart>();
    //            Debug.Log(grHeart[i].heart[j-1] + " Type: " + scr.heartInfo);
    //        }
    //    }
    //}
}



