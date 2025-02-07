using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager2 : BaseLevelManager
{
    public static LevelManager2 Instance;

    private new int mistakes = 0; // 🔹 Fixed mistake tracking

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

    protected override void LoadNextLevel()
    {
        Debug.Log("✅ All trash sorted in Level 2! Loading Level 3...");
        SceneManager.LoadScene("Level 3"); // 🔹 Fixed: Now implements LoadNextLevel()
    }

    public override void TrashIncorrectlySorted() // 🔹 Added override
    {
        mistakes++;
        Debug.Log($"❌ Mistake {mistakes}/3");

        if (mistakes >= 3)
        {
            Debug.Log("💀 Too many mistakes! Loading Game Over scene...");
            SceneManager.LoadScene("Game Over");
        }
    }
}
