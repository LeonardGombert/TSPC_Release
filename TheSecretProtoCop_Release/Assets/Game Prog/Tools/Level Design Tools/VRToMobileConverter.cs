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
    [SerializeField, ReadOnly] GameObject electricalLinePrefab;
    [SerializeField] Dictionary<SwitchableType, GameObject> switcherConversionTable;
    [SerializeField] Dictionary<PropType, GameObject> propConversionTable;
    // save the contents of the VR container

    // FIFO list of objects to put out
    List<GameObject> convertedSwitchers = new List<GameObject>(), convertedProps = new List<GameObject>();
    List<Transform> switcherPositions = new List<Transform>(), propPositions = new List<Transform>();
    List<int> switcherPowerStates = new List<int>();

    [Button]
    void ConvertSwitchers()
    {
        convertedSwitchers.Clear();
        switcherPositions.Clear();
        switcherPowerStates.Clear();

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
                switcherPowerStates.Add(current.Power);
            }
        }

        // instantiate the mobile equivalent prefabs and position them
        for (int i = 0; i < convertedSwitchers.Count; i++)
        {
            // keeps track of which switcher container to instantiate in
            GameObject current = (GameObject)PrefabUtility.InstantiatePrefab(convertedSwitchers[i], mobileSwitcherParent.GetChild(Mathf.FloorToInt(i / 2)));
            current.transform.position = switcherPositions[i].position;
            current.transform.rotation = switcherPositions[i].rotation;
            current.GetComponent<SwitchableElement>().power = switcherPowerStates[i];

            GameObject electricalLine = (GameObject)PrefabUtility.InstantiatePrefab(electricalLinePrefab, mobileSwitcherParent.GetChild(Mathf.FloorToInt(i / 2)));
            electricalLine.GetComponent<SwitchableElement>().power = switcherPowerStates[i];
        }
    }

    [Button]
    void ConvertProps()
    {
        convertedProps.Clear();
        propPositions.Clear();

        // check the contents of the VR container, and instantiate the mobile Switcher Containers
        for (int i = 0; i < VRPropsParent.childCount; i++)
        {
            try
            {
                PropTypeInterface currProp = VRPropsParent.GetChild(i).GetComponent<PropTypeInterface>();

                convertedProps.Add(propConversionTable[currProp.propType]);
                propPositions.Add(currProp.transform);
            }
            finally { }
        }

        // instantiate the mobile equivalent prefabs and position them
        for (int i = 0; i < convertedProps.Count; i++)
        {
            // keeps track of which switcher container to instantiate in
            GameObject current = (GameObject)PrefabUtility.InstantiatePrefab(convertedProps[i], mobilePropsParent);
            current.transform.position = propPositions[i].position;
            current.transform.rotation = propPositions[i].rotation;
        }
    }
}