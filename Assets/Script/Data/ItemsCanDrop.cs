public class ItemsCanDrop
{
    public TypeItemsCanDrop type;
    public float value;
    public float ratio;
    public ItemsCanDrop()
    {

    }
    public ItemsCanDrop(TypeItemsCanDrop type, float value, float ratio)
    {
        this.type = type;
        this.value = value;
        this.ratio = ratio;
    }

    public ItemsCanDrop(ItemsCanDrop obj)
    {
        this.type = obj.type;
        this.value = obj.value;
        this.ratio = obj.ratio;
    }
}
