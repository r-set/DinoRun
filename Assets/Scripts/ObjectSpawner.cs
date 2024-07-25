using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectPrefab;
    [SerializeField] private Transform _platformTransform;
    [SerializeField] private PlatformMovement _platformMovement;

    private float _spawnDistance = 100.0f;
    private float _destroyOffset = 5.0f;
    private float _lastSpawnTime = 0f;
    private float _spawnInterval = 1.0f;

    private List<GameObject> _spawnedObjects = new List<GameObject>();

    private void Update()
    {
        if (Time.time - _lastSpawnTime > _spawnInterval)
        {
            SpawnObject();
            _lastSpawnTime = Time.time;
        }

        MoveObject();
        RemoveOldObject();
    }

    private void SpawnObject()
    {
        float nextSpawnZ = Mathf.Floor(_platformTransform.position.z / _spawnDistance) * _spawnDistance;

        Vector3 spawnPosition = new Vector3(GetRandomXPosition(), 0.45f, nextSpawnZ + _spawnDistance);
        GameObject newBlock = Instantiate(_objectPrefab[Random.Range(0, _objectPrefab.Length)], spawnPosition, Quaternion.identity);
        _spawnedObjects.Add(newBlock);
    }

    private void MoveObject()
    {
        foreach (GameObject block in _spawnedObjects)
        {
            block.transform.position -= Vector3.forward * _platformMovement.platformSpeed * Time.deltaTime;
        }
    }

    private void RemoveOldObject()
    {
        for (int i = 0; i < _spawnedObjects.Count; i++)
        {
            if (_spawnedObjects[i].transform.position.z < _platformTransform.position.z - _destroyOffset)
            {
                Destroy(_spawnedObjects[i]);
                _spawnedObjects.RemoveAt(i);
                i--;
            }
        }
    }

    private float GetRandomXPosition()
    {
        int randomIndex = Random.Range(0, 3);
        float[] spawnPositionsX = { -2f, 0.0f, 2f };
        return spawnPositionsX[randomIndex];
    }
}