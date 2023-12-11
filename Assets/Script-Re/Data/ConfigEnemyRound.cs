public class ConfigEnemyRound
{
    public TypeEnemy type;
    public float value;
    public ConfigEnemyRound()
    {

    }
    public ConfigEnemyRound(TypeEnemy type, float value)
    {
        this.type = type;
        this.value = value;
    }

    public ConfigEnemyRound(ConfigEnemyRound obj)
    {
        this.type = obj.type;
        this.value = obj.value;
    }
}
