using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicByMap
{
    public string name;
    public AudioClip audioClip;
}


[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private MusicByMap[] musicByMap;
    
    private Dictionary<string, AudioClip> _musicByMapDictionary = new Dictionary<string, AudioClip>();
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        GameEvents.OnMapChanged += MusicByMapHandle;

        foreach (MusicByMap m in musicByMap)
        {
            _musicByMapDictionary.Add(m.name, m.audioClip);
        }
    }

    void MusicByMapHandle(string mapName)
    {
        if (_musicByMapDictionary.ContainsKey(mapName))
        {
            audioSource.clip = _musicByMapDictionary[mapName];
            audioSource.Play();
        }
        else
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }

    public void MusicSliderValueChanged(float value)
    {
        audioSource.volume = value;
    }
}

