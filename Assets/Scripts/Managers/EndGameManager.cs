using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EndGameDatatype
{
    KILLED_ENEMIES,
    COINS,
    COMPLETED_QUESTS
}
public class EndGameManager : SingletonPersistence<EndGameManager>
{
    
    public string endSceneName;
    
    public Dictionary<EndGameDatatype, int> endGameInfo = new Dictionary<EndGameDatatype, int>();
    
    public void StartEndScreen()
    {
        endGameInfo.Add(EndGameDatatype.KILLED_ENEMIES, DataManager.Instance.playerData.KilledEnemiesCount);
        endGameInfo.Add(EndGameDatatype.COINS, CurrencyController.Instance.GetGold());
        endGameInfo.Add(EndGameDatatype.COMPLETED_QUESTS, QuestController.Instance.handinQuestIDs.Count);
        
        SceneManager.LoadScene(endSceneName);
    }
}
