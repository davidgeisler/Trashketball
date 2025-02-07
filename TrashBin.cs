using UnityEngine;

public class TrashBin : MonoBehaviour
{
    [SerializeField] private TrashType acceptedType;
    [SerializeField] private GameObject niceTextPrefab; // Floating "Nice!"
    [SerializeField] private GameObject failedTextPrefab; // Floating "Failed"

    private void OnTriggerEnter(Collider other)
    {
        TrashItem trash = other.GetComponent<TrashItem>();

        if (trash != null)
        {
            // ✅ Get the correct LevelManager dynamically
            BaseLevelManager levelManager = Object.FindFirstObjectByType<BaseLevelManager>();

            if (trash.GetTrashType() == acceptedType)
            {
                Debug.Log($"✅ Correct bin! {trash.name} sorted.");
                Destroy(other.gameObject);

                if (levelManager != null)
                {
                    Debug.Log("🔄 Notifying LevelManager of Correct Sorting...");
                    levelManager.TrashCorrectlySorted(); // ✅ Notify LevelManager
                }
                else
                {
                    Debug.LogError("❌ ERROR: LevelManager NOT FOUND in this scene!");
                }
            }
            else
            {
                Debug.Log("❌ Wrong bin! This trash doesn't belong here.");

                if (levelManager != null)
                {
                    Debug.Log("🔴 Notifying LevelManager of Mistake...");
                    levelManager.TrashIncorrectlySorted(); // ✅ Notify incorrect sorting
                }
                else
                {
                    Debug.LogError("❌ ERROR: LevelManager NOT FOUND in this scene!");
                }
            }
        }
    }

    // Function to show floating text (Nice! or Failed)
    private void ShowFloatingText()
    {

        GameObject nicefloatingText = Instantiate(niceTextPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        GameObject failedfloatingText = Instantiate(failedTextPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        Destroy(nicefloatingText, 1.5f); // Destroy after 1.5 seconds
        Destroy(failedfloatingText, 1.5f); // Destroy after 1.5 seconds

    }
}

