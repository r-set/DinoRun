using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Camera _mainCamera;

    private float _spawnOffset = 9.0f;
    private float _despawnOffset = 10.0f;
    private List<GameObject> _spawnedPlatforms = new List<GameObject>();

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        SpawnInitialPlatforms();
    }

    private void Update()
    {
        GetSpawnPlatform();
    }

    private void GetSpawnPlatform()
    {
        float spawnDistance = _playerTransform.position.z + _mainCamera.farClipPlane;

        if (spawnDistance > _spawnedPlatforms[_spawnedPlatforms.Count - 1].transform.position.z)
        {
            SpawnPlatform();
        }

        for (int i = 0; i < _spawnedPlatforms.Count; i++)
        {
            if (_spawnedPlatforms[i].transform.position.z < _playerTransform.position.z - _despawnOffset)
            {
                Destroy(_spawnedPlatforms[i]);
                _spawnedPlatforms.RemoveAt(i);
                i--;
            }
        }
    }

    private void SpawnInitialPlatforms()
    {
        Vector3 spawnPosition = transform.position;
        while (spawnPosition.z < _playerTransform.position.z + _mainCamera.farClipPlane)
        {
            GameObject newPlatform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity);
            _spawnedPlatforms.Add(newPlatform);
            newPlatform.AddComponent<PlatformMovement>();
            spawnPosition.z += _spawnOffset;
        }
    }

    private void SpawnPlatform()
    {
        Vector3 spawnPosition = _spawnedPlatforms[_spawnedPlatforms.Count - 1].transform.position + Vector3.forward * _spawnOffset;
        GameObject newPlatform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity);
        _spawnedPlatforms.Add(newPlatform);
        newPlatform.AddComponent<PlatformMovement>();
    }
}