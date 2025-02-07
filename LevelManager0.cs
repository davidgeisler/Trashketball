using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager0 : MonoBehaviour
{
    public static LevelManager0 Instance; // Singleton instance

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1"); // Ensure correct scene name
    }
}
