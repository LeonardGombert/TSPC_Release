using Sirenix.OdinInspector;
using UnityEngine;

public class TransitionDoorBehaviour : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] UnityEngine.Events.UnityEvent _OnTurnOn;
    [SerializeField] UnityEngine.Events.UnityEvent _OnTurnOff;
    [SerializeField] GameObject[] doorMeshes;

    [Button("Lock")]
    public void Lock()
    {
        _OnTurnOn.Invoke();

        anim.ResetTrigger("Open");
        anim.SetTrigger("Close");
    }

    [Button("Unlock")]
    public void Unlock()
    {
        _OnTurnOff.Invoke();
        anim.ResetTrigger("Close");
        anim.SetTrigger("Open");
    }

    public void DisableDoor()
    {
        for (int i = 0; i < doorMeshes.Length; i++) doorMeshes[i].SetActive(false);
    }

    public void EnableDoor()
    {
        for (int i = 0; i < doorMeshes.Length; i++) doorMeshes[i].SetActive(true);
    }
}
