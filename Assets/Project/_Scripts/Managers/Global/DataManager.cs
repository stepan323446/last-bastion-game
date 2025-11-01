using Project._Scripts.Gameplay.Player.Data;
using UnityEngine;

public class DataManager : SingletonPersistence<DataManager>
{
    public PlayerData playerData;

    static DataManager()
    {
        isThrowNullInstance = true;
    }
}
