using UnityEngine;
[CreateAssetMenu(fileName = "NewCurses", menuName = "GameUtilities/CreateCurses")]
public class CursesObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;

    [Header("Comfinguration")]
    public Sprite IconCurses;
    public TypeCurses TypeCurses;
    public LevelItems LevelCurses;
    public string NameCurses = "";
    public string QuoteCurses = "";
    public string DescriptionCurses = "";
    public float Speed = 1;
    public float AttackRange = 2f;
    public CursesObject()
    {

    }
}
