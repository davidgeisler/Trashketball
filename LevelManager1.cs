using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager1 : BaseLevelManager
{
    public static LevelManager1 Instance;
    private AudioManager audioManager; // Ensure the correct reference

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

        // ✅ Corrected way to get AudioManager
        GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");
        if (audioObject != null)
        {
            audioManager = audioObject.GetComponent<AudioManager>();
        }
        else
        {
            Debug.LogError("❌ AudioManager not found! Make sure the AudioManager exists in the scene.");
        }
    }

    protected override void LoadNextLevel()
    {
        Debug.Log("✅ All trash sorted in Level 1! Loading Level 2...");

        // ✅ Ensure the AudioManager plays LevelUp sound
        if (audioManager != null && audioManager.LevelUp != null)
        {
            audioManager.PlaySFX(audioManager.LevelUp);
        }
        else
        {
            Debug.LogError("❌ Cannot play sound: AudioManager or LevelUp clip is missing.");
        }

        // ✅ Load Level 2 after playing the sound
        SceneManager.LoadScene("Level 2");
    }
}
