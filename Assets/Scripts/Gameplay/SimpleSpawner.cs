using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private string _currentMapName;
    
    private GameObject _currentGameObject;
    private Coroutine _spawnCooldown;
    private bool _canSpawn = true;

    private void Start()
    {
        GameEvents.OnMapChanged += MapChangedHandler;
        _canSpawn = true;
    }

    public void ResetSpawner()
    {
        if (_currentGameObject != null)
        {
            SpawnerEnemy spawnerEnemy = _currentGameObject.GetComponent<SpawnerEnemy>();
            
            if(spawnerEnemy !=null) spawnerEnemy.ResetSpawner();
            
            Destroy(_currentGameObject);
        };
    }

    public void Spawn()
    {
        if(_canSpawn)
            CreateObject();
    }
    public void StartCoolDown()
    {
        if(_spawnCooldown == null)
            StartCoroutine(CooldownCoroutine());
    }

    private void MapChangedHandler(string newMapName)
    {
        ResetSpawner();
        if(newMapName == _currentMapName)
            Spawn();
    }

    private void CreateObject()
    {
        _currentGameObject = Instantiate(_spawnPrefab, transform.position, Quaternion.identity);

        HealthNPC health = _currentGameObject.GetComponent<HealthNPC>();
        if (health != null)
        {
            health.OnDied += StartCoolDown;
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        _canSpawn = false;
        yield return new WaitForSeconds(_spawnInterval);
        _canSpawn = true;
    }
}
