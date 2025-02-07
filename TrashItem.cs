using UnityEngine;

// Enum to define trash categories
public enum TrashType { Recyclable, Biodegradable, NonBiodegradable }

public class TrashItem : MonoBehaviour
{
    [SerializeField] private TrashType trashType; // Make it visible in the Inspector

    public TrashType GetTrashType()
    {
        return trashType;
    }
}
