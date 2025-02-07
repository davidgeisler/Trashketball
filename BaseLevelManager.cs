using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseLevelManager : MonoBehaviour
{
    protected int totalTrash;
    protected int trashSorted = 0;
    protected int mistakes = 0; // ✅ Track incorrect sorting
    private const int maxMistakes = 3; // ✅ Game over after 3 mistakes

    protected virtual void Start()
    {
        CountTotalTrash();
    }

    protected void CountTotalTrash()
    {
        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");
        totalTrash = trashObjects.Length;
        Debug.Log($"📦 Total Trash Counted in {SceneManager.GetActiveScene().name}: {totalTrash}");
    }

    public virtual void TrashCorrectlySorted()
    {
        trashSorted++;
        Debug.Log($"🟢 Trash Sorted Count: {trashSorted}/{totalTrash}");

        if (trashSorted >= totalTrash)
        {
            Debug.Log("🔄 All trash sorted! Calling LoadNextLevel()");
            LoadNextLevel();
        }
    }

    public virtual void TrashIncorrectlySorted()
    {
        mistakes++;
        Debug.Log($"❌ Mistake {mistakes}/{maxMistakes}");

        if (mistakes >= maxMistakes)
        {
            Debug.Log("💀 Too many mistakes! Loading Game Over scene...");
            SceneManager.LoadScene("Game Over"); // ✅ Load Game Over
        }
    }

    protected abstract void LoadNextLevel();
}
