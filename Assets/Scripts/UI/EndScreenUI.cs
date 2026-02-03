using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public struct DataLoad
{
    public EndGameDatatype dataType;
    public TextMeshProUGUI text;
}
public class EndScreenUI : MonoBehaviour
{
    public DataLoad[] dataLoad;
    public string startSceneName;

    private void Awake()
    {
        Dictionary<EndGameDatatype, int> endDataByManager = EndGameManager.Instance.endGameInfo;

        foreach (DataLoad data in dataLoad)
        {
            if (endDataByManager.ContainsKey(data.dataType))
            {
                data.text.text = endDataByManager[data.dataType].ToString();
            }
            else
            {
                Debug.LogWarning("Not found data type for end screen: " + data.dataType);
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startSceneName);
    }
}
