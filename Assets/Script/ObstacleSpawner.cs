using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject horizontalPrefab;
    public GameObject verticalDownPrefab;
    public GameObject verticalUpPrefab;
    
    public float spawnRate = 2f;
    public float obstacleSpeed = 3f;
    public Vector2 spawnRangeX = new Vector2(-8f, 8f);
    public Vector2 spawnRangeY = new Vector2(-4f, 4f);

    private List<Vector3> occupiedPositions = new List<Vector3>();

    private enum ObstacleDirection { LeftToRight, RightToLeft, BottomToTop, TopToBottom }

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnRate);
    }

    void SpawnObstacle()
    {
        ObstacleDirection direction = (ObstacleDirection)Random.Range(0, 4);
        GameObject prefab = GetPrefabForDirection(direction);
        if (prefab == null) return;

        Vector3 spawnPos = GetValidSpawnPosition(direction);
        if (spawnPos == Vector3.zero) return;

        GameObject obstacle = Instantiate(prefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Il prefab dell'ostacolo non ha un Rigidbody2D!");
            return;
        }

        rb.isKinematic = false;
        rb.gravityScale = 0;

        switch (direction)
        {
            case ObstacleDirection.LeftToRight:
                rb.linearVelocity = new Vector2(obstacleSpeed, 0);
                break;
            case ObstacleDirection.RightToLeft:
                rb.linearVelocity = new Vector2(-obstacleSpeed, 0);
                break;
            case ObstacleDirection.BottomToTop:
                rb.linearVelocity = new Vector2(0, obstacleSpeed);
                break;
            case ObstacleDirection.TopToBottom:
                rb.linearVelocity = new Vector2(0, -obstacleSpeed);
                break;
        }

        occupiedPositions.Add(spawnPos);
    }

    GameObject GetPrefabForDirection(ObstacleDirection direction)
    {
        switch (direction)
        {
            case ObstacleDirection.LeftToRight:
            case ObstacleDirection.RightToLeft:
                return horizontalPrefab;
            case ObstacleDirection.BottomToTop:
                return verticalUpPrefab;
            case ObstacleDirection.TopToBottom:
                return verticalDownPrefab;
            default:
                return null;
        }
    }

    Vector3 GetValidSpawnPosition(ObstacleDirection direction)
    {
        int attempts = 10;
        while (attempts > 0)
        {
            Vector3 newPos = GenerateRandomSpawn(direction);
            if (!IsPositionOccupied(newPos))
            {
                return newPos;
            }
            attempts--;
        }
        return Vector3.zero;
    }

    Vector3 GenerateRandomSpawn(ObstacleDirection direction)
    {
        switch (direction)
        {
            case ObstacleDirection.LeftToRight:
                return new Vector3(spawnRangeX.x, Random.Range(spawnRangeY.x, spawnRangeY.y), 0);
            case ObstacleDirection.RightToLeft:
                return new Vector3(spawnRangeX.y, Random.Range(spawnRangeY.x, spawnRangeY.y), 0);
            case ObstacleDirection.BottomToTop:
                return new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), spawnRangeY.x, 0);
            case ObstacleDirection.TopToBottom:
                return new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), spawnRangeY.y, 0);
            default:
                return Vector3.zero;
        }
    }

    bool IsPositionOccupied(Vector3 pos)
    {
        foreach (Vector3 occupied in occupiedPositions)
        {
            if (Vector2.Distance(pos, occupied) < 1.5f)
            {
                return true;
            }
        }
        return false;
    }
}
