using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab;
    [SerializeField] float spawnInterval = 5f;      // How often to spawn powerups
    [SerializeField] float spawnRangeX = 9f;        // X range within which powerups can spawn (KEEP EQUAL TO CAMERA WIDTH)
    [SerializeField] float spawnHeight = 1f;        // How far above/below the camera's top y value powerups spawn
    [SerializeField] int maxPowerups = 5;
    [SerializeField] float minDistance = 2f; // how much space each powerup should be from another
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating(nameof(SpawnPowerup), 2f, spawnInterval);
    }

    void SpawnPowerup()
    {
        int currentPowerups = GameObject.FindGameObjectsWithTag("Powerup").Length;

        if (currentPowerups >= maxPowerups)
            return;

        float cameraTopY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        for (int attempt = 0; attempt < 10; attempt++)
        {
            float x = Random.Range(-spawnRangeX, spawnRangeX);
            float y = Random.Range(cameraTopY + spawnHeight, cameraTopY + spawnHeight * 6);
            Vector2 spawnPos = new Vector2(x, y);

            if (IsFarEnoughFromOthers(spawnPos))
            {
                Instantiate(powerupPrefab, new Vector3(x, y, 0), Quaternion.identity);
                return;
            }
        }
    }

    // helper method to distance powerups from each other
    bool IsFarEnoughFromOthers(Vector2 newPos)
    {
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");

        foreach (GameObject powerup in powerups)
        {
            float dist = Vector2.Distance(newPos, powerup.transform.position);
            if (dist < minDistance)
                return false;
        }

        return true;
    }

}
