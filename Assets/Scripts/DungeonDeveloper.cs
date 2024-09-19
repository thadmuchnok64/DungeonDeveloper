using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine.XR;

public class DungeonDeveloper : EditorWindow
{
    string objectName = "Wall";
    int objectID = 1;
    GameObject objectToSpawn;
    float objectScale;
    float spawnRadius = 5f;

    Vector2 mousepos;
    Ray ray;

    bool mouseClick;
    float distance;

    private void OnSceneGUI(SceneView sceneView)
    {
        Vector3 mousePosition = Event.current.mousePosition;
        mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y; // Flip y
        ray = sceneView.camera.ScreenPointToRay(mousePosition);

        distance = sceneView.camera.ScreenToWorldPoint(mousePosition).magnitude;

        mouseClick = Event.current.type==EventType.MouseDown;
    }

    

    [MenuItem("Tools/Dungeon Developer")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DungeonDeveloper));
    }

    private void OnGUI()
    {

        SceneView.duringSceneGui += OnSceneGUI;

        GUILayout.Label("Spawn Object", EditorStyles.boldLabel);

        objectName = EditorGUILayout.TextField("Object Name",objectName);
        objectID = EditorGUILayout.IntField("Object ID", objectID);
        objectScale = EditorGUILayout.Slider("Object Name", objectScale,.5f,3f);
        spawnRadius = EditorGUILayout.FloatField("Spawn radius", spawnRadius);
        objectToSpawn = EditorGUILayout.ObjectField("Object Name", objectToSpawn,typeof(GameObject),false) as GameObject;

        if(GUILayout.Button("Spawn Tile"))
        {
            SpawnObject();
        }
    }


    private void SpawnObject()
    {
        if (objectToSpawn == null)
        {
            return;
        }
        if (objectName == string.Empty)
        {
            return;
        }

        var ray = HandleUtility.GUIPointToWorldRay(new Vector2(50, 50));

        Vector3 spawnPos = ray.GetPoint(5);

        if (test == true)
            return;
        test = true;

        testobj = Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
        testobj.name = objectName + objectID;
        testobj.transform.localScale = Vector3.one * objectScale;

        objectID++;
    }

    GameObject testobj;
    bool test = false;
    private void Update()
    {

        if (test) {

            Plane plane = new Plane(Vector3.up, 0);
            plane.Raycast(ray, out distance);
            Vector3 spawnPos = ray.GetPoint(distance);
            testobj.transform.position = new Vector3(Mathf.Round(spawnPos.x), spawnPos.y, Mathf.Round(spawnPos.z));

        }

        if (mouseClick)
        {
            test = false;
        }
    }
}
