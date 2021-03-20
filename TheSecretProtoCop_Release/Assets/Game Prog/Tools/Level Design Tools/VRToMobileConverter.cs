using Gameplay;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VRToMobileConverter : SerializedMonoBehaviour
{
    [SerializeField] Transform VRSwitcherParent, mobileSwitcherParent, mobileSwitcherUIParent, VRPropsParent, mobilePropsParent;
    [SerializeField, ReadOnly] GameObject switchersPrefab;
    [SerializeField, ReadOnly] GameObject switchersUIPrefab;
    [SerializeField, ReadOnly] Dictionary<SwitchableType, GameObject> switcherConversionTable;
    [SerializeField] Dictionary<PropType, GameObject> propConversionTable;
    // save the contents of the VR container

    // FIFO list of objects to put out
    List<GameObject> convertedSwitchers, convertedProps;
    List<Transform> switcherPositions, propPositions;

    [Button]
    [ExecuteInEditMode]
    void ConvertVRLevelToMobile()
    {
        convertedSwitchers.Clear();
        switcherPositions.Clear();

        // clear out any existing switchers
        foreach (Transform child in mobileSwitcherParent.GetComponentsInChildren<Transform>()) DestroyImmediate(child.gameObject);
        foreach (Transform child in mobileSwitcherUIParent.GetComponentsInChildren<Transform>()) DestroyImmediate(child.gameObject);
        foreach (Transform child in mobilePropsParent.GetComponentsInChildren<Transform>()) DestroyImmediate(child.gameObject);

        #region Convert Switchers
        // check the contents of the VR container, and instantiate the mobile Switcher Containers
        for (int i = 0; i < VRSwitcherParent.childCount; i++)
        {
            SwitcherBehavior currSwitcher = VRSwitcherParent.GetChild(i).GetComponent<SwitcherBehavior>();

            // instantiate the mobile container equivalents and copy the property values
            GameObject mobileContainer = (GameObject)PrefabUtility.InstantiatePrefab(switchersPrefab, mobileSwitcherParent);
            SwitcherBehavior mobileSwitcher = mobileContainer.GetComponent<SwitcherBehavior>();
            // instantiate the mobile UI prefab and copy its values too
            GameObject mobileUI = (GameObject)PrefabUtility.InstantiatePrefab(switchersUIPrefab, mobileSwitcherUIParent);
            SwitcherBehavior mobileSwitcherUI = mobileUI.GetComponent<SwitcherBehavior>();

            mobileSwitcherUI.ID = mobileSwitcher.ID = currSwitcher.ID;
            mobileSwitcherUI.timer = mobileSwitcher.timer = currSwitcher.timer;
            mobileSwitcherUI.switchTimer = mobileSwitcher.switchTimer = currSwitcher.switchTimer;
            mobileSwitcherUI.Power = mobileSwitcher.Power = currSwitcher.Power;
            mobileSwitcherUI.State = mobileSwitcher.State = currSwitcher.State;

            // add each child switchable element to a list
            for (int j = 0; j < currSwitcher.nodes.Count; j++)
            {
                SwitchableElement current = currSwitcher.nodes[j];
                convertedSwitchers.Add(switcherConversionTable[current.prefabType]); // add equivalent object to list
                switcherPositions.Add(current.transform);
            }

        }

        // instantiate the mobile equivalent prefabs and position them
        for (int i = 0; i < convertedSwitchers.Count; i++)
        {
            // keeps track of which switcher container to instantiate in
            GameObject current = (GameObject)PrefabUtility.InstantiatePrefab(convertedSwitchers[i], mobileSwitcherParent.GetChild(Mathf.FloorToInt(i / 2)));
            current.transform.position = switcherPositions[i].position;
            current.transform.rotation = switcherPositions[i].rotation;
        }
        #endregion

        #region Convert Props
        // check the contents of the VR container, and instantiate the mobile Switcher Containers
        for (int i = 0; i < VRPropsParent.childCount; i++)
        {
            try
            {
                PropTypeInterface currProp = VRPropsParent.GetChild(i).GetComponent<PropTypeInterface>();

                convertedProps.Add(propConversionTable[currProp.propType]);
                propPositions.Add(currProp.transform);
            }finally { }
        }

        // instantiate the mobile equivalent prefabs and position them
        for (int i = 0; i < convertedProps.Count; i++)
        {
            // keeps track of which switcher container to instantiate in
            GameObject current = (GameObject)PrefabUtility.InstantiatePrefab(convertedProps[i], mobilePropsParent);
            current.transform.position = propPositions[i].position;
            current.transform.rotation = propPositions[i].rotation;
        }
        #endregion
    }
}