namespace azara.common.Services;

public interface IDataService
{
    event Action<string> OnData;

    void SendData(string data);

    void ClearDatas();
}

public class DataService : IDataService
{
    public event Action<string> OnData;

    public void SendData(string data) => OnData?.Invoke(data);

    public void ClearDatas() => OnData?.Invoke(null);
}