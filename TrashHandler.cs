using UnityEngine;
using TMPro;

public class TrashHandler : MonoBehaviour
{
    public Transform PosOverHead;           // Position for holding trash overhead
    public Transform[] TrashBinTargets;    // Array of reference points (RedTarget, YellowTarget, BlueTarget)
    public float ThrowDuration = 0.66f;    // Duration for throwing the trash
    public Transform CharacterModel;       // Character's model or root that will look at the target

    private Transform CurrentTrash;        // The trash currently being handled
    private int currentTargetIndex = 0;    // Index of the selected target
    private bool isTrashInHands = false;   // Whether the trash is in the player's hands
    private bool isTrashFlying = false;    // Whether the trash is flying
    private float throwTimer = 0;          // Timer for the throw animation

    [SerializeField] private TextMeshProUGUI floatingTextPrefab; // Floating text prefab
    private TextMeshProUGUI floatingTextInstance; // Instance of floating text

    void Update()
    {
        // Cycle through trash bin targets with Tab
        if (Input.GetKeyDown(KeyCode.Tab) && isTrashInHands)
        {
            currentTargetIndex = (currentTargetIndex + 1) % TrashBinTargets.Length;
            Debug.Log("Switched target to: " + TrashBinTargets[currentTargetIndex].name);

            // Make the character look at the new target
            LookAtTarget();
        }

        // Hold trash while pressing Space
        if (Input.GetKey(KeyCode.Space) && CurrentTrash != null && !isTrashFlying)
        {
            HoldAndAimTrash();
        }

        // Throw trash when Space is released
        if (Input.GetKeyUp(KeyCode.Space) && isTrashInHands)
        {
            ThrowTrash();
        }

        // Handle trash in the air
        if (isTrashFlying)
        {
            HandleTrashFlying();
        }
    }

    private void LookAtTarget()
    {
        if (TrashBinTargets.Length > 0 && TrashBinTargets[currentTargetIndex] != null)
        {
            // Rotate the character model to face the current target
            Vector3 directionToTarget = TrashBinTargets[currentTargetIndex].position - CharacterModel.position;
            directionToTarget.y = 0; // Ignore vertical rotation
            CharacterModel.forward = directionToTarget.normalized;
        }
    }

    private void HoldAndAimTrash()
    {
        isTrashInHands = true; // Trash is now in the player's hands
        CurrentTrash.GetComponent<Rigidbody>().isKinematic = true; // Disable physics while holding
        CurrentTrash.position = PosOverHead.position; // Move trash to overhead position
        LookAtTarget(); // Continuously face the target

        ShowFloatingText(CurrentTrash.name); // Show floating text
    }

    private void ThrowTrash()
    {
        if (TrashBinTargets.Length == 0 || TrashBinTargets[currentTargetIndex] == null)
        {
            Debug.LogError("No valid target to throw!");
            return;
        }

        isTrashInHands = false; // Trash is no longer in the player's hands
        isTrashFlying = true;   // Trash is now flying
        throwTimer = 0;         // Reset throw animation timer
        Debug.Log("Throwing trash to: " + TrashBinTargets[currentTargetIndex].name);

        HideFloatingText(); // Hide floating text when thrown
    }

    private void HandleTrashFlying()
    {
        if (CurrentTrash == null)
        {
            Debug.LogError("Cannot handle flying trash: CurrentTrash is null!");
            isTrashFlying = false; // Stop flying logic
            return;
        }

        if (TrashBinTargets.Length == 0 || TrashBinTargets[currentTargetIndex] == null)
        {
            Debug.LogError("Cannot handle flying trash: Target is null!");
            isTrashFlying = false; // Stop flying logic
            return;
        }

        throwTimer += Time.deltaTime;
        float t01 = throwTimer / ThrowDuration;

        // Calculate positions
        Vector3 startPosition = PosOverHead.position;
        Vector3 targetPosition = TrashBinTargets[currentTargetIndex].position;
        Vector3 linearPosition = Vector3.Lerp(startPosition, targetPosition, t01);

        // Add arc
        Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * Mathf.PI); // Adjust arc height as needed
        CurrentTrash.position = linearPosition + arc;

        // Finalize throw when animation completes
        if (t01 >= 1)
        {
            isTrashFlying = false;
            CurrentTrash.GetComponent<Rigidbody>().isKinematic = false; // Re-enable physics
            Debug.Log("Trash landed in: " + TrashBinTargets[currentTargetIndex].name);
            CurrentTrash = null; // Clear reference to the trash
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash") && !isTrashInHands && !isTrashFlying)
        {
            CurrentTrash = other.transform;
            Debug.Log("CurrentTrash assigned: " + CurrentTrash.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trash") && !isTrashFlying)
        {
            CurrentTrash = null;
            Debug.Log("Exited trash range.");
            HideFloatingText(); // Hide floating text when dropping trash
        }
    }

    // Function to show floating text with trash name
    private void ShowFloatingText(string trashName)
    {
        if (floatingTextPrefab != null && CurrentTrash != null)
        {
            if (floatingTextInstance == null)
            {
                floatingTextInstance = Instantiate(floatingTextPrefab, CurrentTrash.position + new Vector3(0, 1.5f, 0), Quaternion.identity, CurrentTrash);
            }
            floatingTextInstance.text = trashName; // Set the trash name
            floatingTextInstance.gameObject.SetActive(true);
        }
    }


    // Function to hide floating text
    private void HideFloatingText()
    {
        if (floatingTextInstance != null)
        {
            Destroy(floatingTextInstance.gameObject); // ✅ Destroy instead of disabling
            floatingTextInstance = null;
        }
    }
}
