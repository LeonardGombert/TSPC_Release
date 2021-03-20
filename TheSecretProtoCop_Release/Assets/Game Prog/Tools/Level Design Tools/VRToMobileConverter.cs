using Gameplay;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VRToMobileConverter : SerializedMonoBehaviour
{
    [SerializeField] Transform VRSwitcherParent, mobileSwitcherParent;
    [SerializeField, ReadOnly] GameObject switchersPrefab;
    [SerializeField, ReadOnly] Dictionary<SwitchableType, GameObject> prefabEquivalents;
    // save the contents of the VR container

    // FIFO list of objects to put out
    List<GameObject> convertedObjects;
    List<Transform> positions;

    [Button]
    void ReadVRContainer()
    {
        convertedObjects.Clear();
        positions.Clear();

        // check the contents of the VR container, and instantiate the mobile Switcher Containers
        for (int i = 0; i < VRSwitcherParent.childCount; i++)
        {
            SwitcherBehavior currSwitcher = VRSwitcherParent.GetChild(i).GetComponent<SwitcherBehavior>();

            // instantiate the mobile container equivalents and copy the property values
            GameObject mobileContainer = (GameObject)PrefabUtility.InstantiatePrefab(switchersPrefab, mobileSwitcherParent);
            SwitcherBehavior mobileSwitcher = mobileContainer.GetComponent<SwitcherBehavior>();
            mobileSwitcher.ID = currSwitcher.ID;
            mobileSwitcher.timer = currSwitcher.timer;
            mobileSwitcher.switchTimer = currSwitcher.switchTimer;
            mobileSwitcher.Power = currSwitcher.Power;
            mobileSwitcher.State = currSwitcher.State;

            // add each child switchable element to a list
            for (int j = 0; j < currSwitcher.nodes.Count; j++)
            {
                SwitchableElement current = currSwitcher.nodes[j];
                convertedObjects.Add(prefabEquivalents[current.prefabType]); // add equivalent object to list
                positions.Add(current.transform);
            }
        }

        // instantiate the mobile equivalent prefabs and position them
        for (int i = 0; i < convertedObjects.Count; i++)
        {
            // keeps track of which switcher container to instantiate in
            GameObject current = (GameObject)PrefabUtility.InstantiatePrefab(convertedObjects[i], mobileSwitcherParent.GetChild(Mathf.FloorToInt(i / 2)));
            current.transform.position = positions[i].position;
            current.transform.rotation = positions[i].rotation;
        }
    }
}