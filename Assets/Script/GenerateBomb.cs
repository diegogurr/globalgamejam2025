using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameInstance : MonoBehaviour
{
    [SerializeField] GameObject bombBubble;
    [SerializeField] GameObject obstacle;
    [SerializeField] float minDistance = 1.5f; // Distanza minima tra gli oggetti
    [SerializeField] private GameObject suddenDeathTop;
    [SerializeField] private GameObject suddenDeathBottom;
    [SerializeField] private float targetY=6;



    void Start()
    {
        Time.timeScale=1;

        List<Vector3> placedPositions = new List<Vector3>();

        // Instanzia le bombBubble
        SpawnObjects(bombBubble, Random.Range(1, 7), placedPositions);

        // Instanzia gli obstacle
        SpawnObjects(obstacle, Random.Range(1, 7), placedPositions);

        StartCoroutine(WaitForSuddenDeath(3));

    }
    IEnumerator WaitForSuddenDeath(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        float startY = suddenDeathTop.transform.position.y;
    float endY = targetY;  // La posizione finale in Y che vuoi raggiungere
    float distanceToMove = startY - endY;
    float moveSpeed = distanceToMove / 120f;  // Movimento in 30 secondi

    // Muove lentamente l'oggetto verso il basso
    while (suddenDeathTop.transform.position.y > targetY)
    {
        suddenDeathTop.transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        suddenDeathBottom.transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        yield return null;  // Aspetta un frame prima di ripetere
    }

    // Assicurati che l'oggetto arrivi esattamente a targetY
    suddenDeathTop.transform.position = new Vector3(suddenDeathTop.transform.position.x, targetY, suddenDeathTop.transform.position.z);
    suddenDeathBottom.transform.position = new Vector3(suddenDeathBottom.transform.position.x, targetY, suddenDeathBottom.transform.position.z);

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
