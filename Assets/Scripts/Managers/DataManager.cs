using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public PlayerData playerData;

    static DataManager()
    {
        isThrowNullInstance = true;
    }
}
