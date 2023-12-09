public class RoundConfig 
{
    public TypeEnemy type;
    public float value;
    public RoundConfig()
    {

    }
    public RoundConfig(TypeEnemy type, float value)
    {
        this.type = type;
        this.value = value;
    }

    public RoundConfig(RoundConfig obj)
    {
        this.type = obj.type;
        this.value = obj.value;
    }
}
