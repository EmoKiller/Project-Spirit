using System;

public class ConfigEnemy
{
    public LevelRomanNumerals LevelEnemy;
    public string type;
    public float value;


    public TypeEnemy Type => (TypeEnemy)Enum.Parse(typeof(TypeEnemy), type);
    public ConfigEnemy()
    {

    }
    public ConfigEnemy(LevelRomanNumerals LevelEnemy, string type, float value)
    {
        this.LevelEnemy = LevelEnemy;
        this.type = type;
        this.value = value;
    }

    public ConfigEnemy(ConfigEnemy obj)
    {
        this.LevelEnemy = obj.LevelEnemy;
        this.type = obj.type;
        this.value = obj.value;
    }
}
