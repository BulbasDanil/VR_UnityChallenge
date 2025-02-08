using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> events;
    [SerializeField] private string tagFilter = "Untagged";
    
    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.CompareTag(tagFilter))
            return;

        foreach (var e in events)
        {
            e?.Invoke();
        }
        
    }
}