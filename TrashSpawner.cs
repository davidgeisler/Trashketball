using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashPrefabs;       // Array of trash prefabs to spawn

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int randomIndex = Random.Range(0, trashPrefabs.Length);
            Vector3 randomSpawnPosition = new Vector3(
            Random.Range(-5 * transform.localScale.x, 5 * transform.localScale.x), // X-axis range
            7, // Y-axis remains the same
            Random.Range(-5 * transform.localScale.z, 5 * transform.localScale.z)  // Z-axis range
            );


            Instantiate(trashPrefabs[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }
}