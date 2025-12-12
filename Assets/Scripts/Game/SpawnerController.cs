using UnityEngine;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private List<GameObject> gameObjectsToSpawn;
    [SerializeField] private float spawnInterval = 3f;

    private float timer;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnObject();
            timer = spawnInterval;
        }
    }

    void SpawnObject()
    {
        if (gameObjectsToSpawn == null || gameObjectsToSpawn.Count == 0) return;

        int randomIndex = Random.Range(0, gameObjectsToSpawn.Count);
        GameObject prefab = gameObjectsToSpawn[randomIndex];

        float distToCamera = -cam.transform.position.z;
        
        Vector3 leftPoint  = cam.ViewportToWorldPoint(new Vector3(0f, 1f, distToCamera));
        Vector3 rightPoint = cam.ViewportToWorldPoint(new Vector3(1f, 1f, distToCamera));

        float randomX = Random.Range(leftPoint.x, rightPoint.x);
    
        Vector3 spawnPos = new Vector3(randomX, leftPoint.y + 1f, 0f);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    } 
}