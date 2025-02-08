using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleCollider : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> events;
    [SerializeField] private string tagFilter = "Untagged";

    private void OnCollisionEnter(Collision collision)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !collision.gameObject.CompareTag(tagFilter))
            return;

        foreach (var e in events)
        {
            e?.Invoke();
        }
    }
}
