using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject bombBubble;
    [SerializeField] GameObject obstacle;
    [SerializeField] float minDistance = 1.5f; // Distanza minima tra gli oggetti

    void Start()
    {
        List<Vector3> placedPositions = new List<Vector3>();

        // Instanzia le bombBubble
        SpawnObjects(bombBubble, Random.Range(1, 7), placedPositions);

        // Instanzia gli obstacle
        SpawnObjects(obstacle, Random.Range(1, 7), placedPositions);
    }

    void SpawnObjects(GameObject obj, int count, List<Vector3> placedPositions)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPosition;
            bool positionValid;

            do
            {
                float randomX = Random.Range(-6f, 6f);
                float randomY = Random.Range(-4f, 4f);
                randomPosition = new Vector3(randomX, randomY, 0);

                positionValid = IsPositionValid(randomPosition, placedPositions);
            } 
            while (!positionValid);

            Instantiate(obj, randomPosition, Quaternion.identity);
            placedPositions.Add(randomPosition);
        }
    }

    bool IsPositionValid(Vector3 position, List<Vector3> placedPositions)
    {
        foreach (Vector3 placed in placedPositions)
        {
            if (Vector3.Distance(position, placed) < minDistance)
            {
                return false; // Posizione troppo vicina a un altro oggetto
            }
        }
        return true; // Posizione accettabile
    }
}
