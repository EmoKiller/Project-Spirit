// Những vật thể nào muốn đưa vào pool và để sử dụng trong pooling thì kế thừa interface này
public interface IPool
{
    public string objectName { get; }

    /// <summary>
    /// Được chạy sau khi đối tượng này đã được lấy từ object pooling ra
    /// </summary>
    public void Show();

    /// <summary>
    /// Khi chạy thằng này song song với việc đưa nó vào pooling 
    /// </summary>
    public void Hide();
}
