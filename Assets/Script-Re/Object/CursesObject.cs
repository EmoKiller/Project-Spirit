using UnityEngine;
[CreateAssetMenu(fileName = "NewCurses", menuName = "GameUtilities/CreateCurses")]
public class CursesObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;
    [Header("Comfinguration")]
    public Sprite IconCurses;
    public TypeCurses TypeCurses;
    public LevelRomanNumerals LevelCurses;
    public NameCurses TypeNameCurses;
    public string NameCurses = "";
    public string QuoteCurses = "";
    public string DescriptionCurses = "";
    public float TimeUseSKill = 1f;
    public float Damage = 2;
    public float Speed = 10f;
    public float AttackRange = 30f;
    public float UseAngry = 10;
    public float multipleObj = 4;
    public CursesObject()
    {

    }
}
