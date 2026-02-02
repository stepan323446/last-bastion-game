using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SpawnerEnemy : Enemy
{
    [Header("Spawner Enemy")]
    [SerializeField] private GameObject[] spawnerPrefabs;
    [SerializeField] private float _spawnCooldown = 5f;
    [SerializeField] private int _spawnCount = 3;
    [SerializeField] private bool _immortalWhenWave = false;
    [Space]
    [SerializeField] private Vector2 spawnerSize = new Vector2(10f, 10f);
    [SerializeField] private Vector2 spawnerOffset;
    [Space]
    [SerializeField] private TriggerCollider _detectTriggerCollider;
    
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private Coroutine _spawnCoroutine;
    
    private Vector2 SpawnerOriginPosition
    {
        get => (Vector2)transform.position + spawnerOffset;
    }

    protected override void Start()
    {
        base.Start();
        _detectTriggerCollider.OnEnter += DetectPlayer;
        _healthNPC.OnDied += DiedSpawnerHandle;
    }

    public void ResetSpawner()
    {
        foreach (GameObject enemy in _spawnedObjects)
        {
            Destroy(enemy);
        }

        _spawnedObjects.Clear();
    }

    private void DiedSpawnerHandle()
    {
        if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
    }
    private void RemoveEnemy(GameObject obj)
    {
        _spawnedObjects.Remove(obj);
    }
    
    public void DetectPlayer(Collider2D collider)
    {
        PlayerMovement playerMovement = collider.GetComponent<PlayerMovement>();
        if (playerMovement == null) return;
        
        _playerTransform = playerMovement.transform;
        StartSpawner();
    }
    private Vector2 GetRandomPointInSpawner()
    {
        float randomX = Random.Range(-spawnerSize.x / 2f, spawnerSize.x / 2f);
        float randomY = Random.Range(-spawnerSize.y / 2f, spawnerSize.y / 2f);

        return SpawnerOriginPosition + new Vector2(randomX, randomY);
    }
    private GameObject GetRandomEnemyPrefab()
    {
        return spawnerPrefabs[Random.Range(0, spawnerPrefabs.Length)];
    }
    private void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomPointInSpawner();
        GameObject prefab = GetRandomEnemyPrefab();

        GameObject enemyObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        _spawnedObjects.Add(enemyObject);
        
        HealthNPC health = enemyObject.GetComponent<HealthNPC>();
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        
        enemy.SetPlayerTransform(_playerTransform);
        health.OnDied += () =>
        {
            RemoveEnemy(enemyObject);
        };
    }

    private void StartSpawner()
    {
        if(_spawnCoroutine == null)
            _spawnCoroutine = StartCoroutine(SpawnerCoroutine());
    }

    private IEnumerator SpawnerCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                SpawnEnemy();
            }

            while (_spawnedObjects.Count > 0)
            {
                if (_immortalWhenWave)
                    _healthNPC._isImmortal = true;
                
                yield return null;
            }
            if(_immortalWhenWave)
                _healthNPC._isImmortal = false;
            
            yield return new WaitForSeconds(_spawnCooldown);   
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Vector3 center = (Vector3)SpawnerOriginPosition;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(center, spawnerSize);
    }
}
