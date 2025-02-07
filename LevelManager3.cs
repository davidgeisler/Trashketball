using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager3 : BaseLevelManager
{
    public static LevelManager3 Instance;

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
        Debug.Log("🏆 All trash sorted in Level 3! Loading Win Scene...");
        SceneManager.LoadScene("WinScene"); // 🔹 Now goes to WinScene when done!
    }

    public override void TrashIncorrectlySorted() // 🔹 Added override for mistake tracking
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
