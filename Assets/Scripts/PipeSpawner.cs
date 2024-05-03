using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PipeSpawner pipePrefab;

    [Header("Settings")]
    [SerializeField] private float gapSize = 4f;
    [SerializeField] private float secondsBetweenSpawns = 2f;

    private float spawnTimer;
    private readonly List<PipeSpawner> pipes = new List<PipeSpawner>();
  

    public void ResetPipes()
    {
        foreach (var pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }
        pipes.Clear();
        spawnTimer = 0f;
    }

    private void RemoveOldPipes()
    {
        for (int i = pipes.Count - 1; i >= 0; i--)
        {
            if (pipes[i].transform.position.x < -20f)
            {
                Destroy(pipes[i].gameObject);
                pipes.RemoveAt(i);
            }
        }
    }

    private void SpawnNewPipes()
    {
        if (spawnTimer > 0f) { return; }
        //Spawn 2 pipes but rotate one 180 Degrees
        PipeSpawner topPipe = Instantiate(pipePrefab, transform.position, Quaternion.Euler(0f, 0f, 180f));
        PipeSpawner bottomPipe = Instantiate(pipePrefab, transform.position, Quaternion.identity);

        float centerHeight = UnityEngine.Random.Range(-2f, 4f);

        topPipe.transform.Translate(Vector3.up * (centerHeight + (gapSize / 2)), Space.World);
        bottomPipe.transform.Translate(Vector3.up * (centerHeight - (gapSize / 2)), Space.World);

        pipes.Add(topPipe);
        pipes.Add(bottomPipe);

        spawnTimer = secondsBetweenSpawns;


    }
    void Start()
    {
        
    }


    void Update()
    {
        RemoveOldPipes();
        SpawnNewPipes();
    }
}
