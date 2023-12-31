public class ConfigEnemy
{
    public LevelRomanNumerals LevelEnemy;
    public TypeEnemy type;
    public float value;
    public ConfigEnemy()
    {

    }
    public ConfigEnemy(LevelRomanNumerals LevelEnemy, TypeEnemy type, float value)
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
