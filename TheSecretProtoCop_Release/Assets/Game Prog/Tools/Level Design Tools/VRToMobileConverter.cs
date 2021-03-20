using Gameplay;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class VRToMobileConverter : SerializedMonoBehaviour
{
    [SerializeField] Transform VRSwitcherParent, mobileSwitcherParent;
    [SerializeField] Dictionary<VRPrefabTypes, GameObject> prefabEquivalents;

    // save the contents of the VR container

    [Button]
    void ReadVRContainer()
    {
        for (int i = 0; i < VRSwitcherParent.childCount; i++)
        {
            SwitcherBehavior currSwitcher = VRSwitcherParent.GetChild(i).GetComponent<SwitcherBehavior>();
            for (int j = 0; j < currSwitcher.nodes.Count; j++)
            {
                Debug.Log(currSwitcher.transform.GetChild(j).gameObject);
                Debug.Log(prefabEquivalents[currSwitcher.nodes[j].prefabType]);
            }
        }
    }
}
