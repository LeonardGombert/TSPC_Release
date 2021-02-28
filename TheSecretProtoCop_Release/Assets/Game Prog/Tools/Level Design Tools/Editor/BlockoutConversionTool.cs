using UnityEditor;
using UnityEngine;

public class BlockoutConversionTool : EditorWindow
{
    GameObject ceilingObject, wallsObject, groundObject;
    GameObject targetCeiling, targetWalls, targetGround;


    [MenuItem("Tools/Conversion Window")]
    public static void ShowWindow()
    {
        GetWindow(typeof(BlockoutConversionTool));
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        ceilingObject = (GameObject) EditorGUILayout.ObjectField(new GUIContent("Ceiling", "The Probuilder GameObject that represents the ceiling"), ceilingObject, typeof(GameObject), true);
        wallsObject = (GameObject) EditorGUILayout.ObjectField(new GUIContent("Walls", "The Probuilder GameObject that contains the walls as its children"), wallsObject, typeof(GameObject), true);
        groundObject = (GameObject) EditorGUILayout.ObjectField(new GUIContent("Ground", "The Probuilder GameObject that represents the floor"), groundObject, typeof(GameObject), true);
        EditorGUILayout.EndVertical();

        if (ceilingObject != null && wallsObject != null && groundObject != null)
        {
            targetCeiling = GameObject.Find("ProB_Ceiling");
            targetWalls = GameObject.Find("ProB_Walls");
            targetGround = GameObject.Find("ProB_Floor");

            //GUILayout.Label("'Ceiling' is " + targetCeiling.name + ", 'Walls' is " + targetWalls.name + ", 'Ground' is " + targetGround.name);

            if (GUILayout.Button("Convert"))
            {
                // copy ceiling components to Prob_Ceiling
                foreach (Component component in ceilingObject.GetComponents<Component>())
                {
                    UnityEditorInternal.ComponentUtility.CopyComponent(component);
                    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetCeiling);
                }

                // copy all of the walls into ProB_Walls
                for (int i = 0; i < wallsObject.transform.childCount; i++)
                {
                    Instantiate(wallsObject.transform.GetChild(i), targetWalls.transform);
                }

                // copy floor components to Prob_Floor
                foreach (Component component in groundObject.GetComponents<Component>())
                {
                    UnityEditorInternal.ComponentUtility.CopyComponent(component);
                    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetGround);
                }
            }
        }
    }
}