public class ConfigEnemy
{
    public TypeEnemy type;
    public float value;
    public ConfigEnemy()
    {

    }
    public ConfigEnemy(TypeEnemy type, float value)
    {
        this.type = type;
        this.value = value;
    }

    public ConfigEnemy(ConfigEnemy obj)
    {
        this.type = obj.type;
        this.value = obj.value;
    }
}
