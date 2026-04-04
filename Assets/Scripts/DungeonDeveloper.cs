using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine.XR;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class DungeonDeveloper : EditorWindow
{
    string objectName = "Wall";
    int objectID = 1;
    float objectScale;
    float spawnRadius = 5f;
    GameObject objectToSpawn;

    [System.Serializable]
    public struct DungeonTile { 
        public string objectName;
        GameObject objectToSpawn;


        public DungeonTile(string name,GameObject obj)
        {
            this.objectName = name;
            this.objectToSpawn = obj;
        }
    }
    public DungeonTile m;
    Vector2 mousepos;
    Ray ray;

    bool mouseClick;
    bool rightClick;
    float distance;
    Vector3 spawnPos;

    GameObject dungeon;
    GameObject floors;
    GameObject walls;


    private void OnSceneGUI(SceneView sceneView)
    {
        Vector3 mousePosition = Event.current.mousePosition;
        mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y; // Flip y
        ray = sceneView.camera.ScreenPointToRay(mousePosition);

        distance = sceneView.camera.ScreenToWorldPoint(mousePosition).magnitude;

        mouseClick = Event.current.type==EventType.MouseDown;
        rightClick = Event.current.button == 1;

    }

    

    [MenuItem("Tools/Dungeon Developer")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DungeonDeveloper));
    }

    private void OnGUI()
    {
        /*
        // Create a two-pane view with the left pane being fixed.
        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

        // Add the view to the visual tree by adding it as a child to the root element.
        rootVisualElement.Add(splitView);

        // A TwoPaneSplitView needs exactly two child elements.
        var leftPane = new VisualElement();
        splitView.Add(leftPane);
        var rightPane = new VisualElement();
        splitView.Add(rightPane);
        */
        SceneView.duringSceneGui += OnSceneGUI;
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Welcome to Dungeon Developer!",EditorStyles.largeLabel);
        EditorGUILayout.Space(6);

        GUILayout.Label("Spawn Object", EditorStyles.boldLabel);
        objectName = EditorGUILayout.TextField("Object Name", objectName);
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
        if (dungeon == null)
        {

            dungeon = new GameObject("Dungeon");
            floors = new GameObject("Floors");
            floors.transform.parent = dungeon.transform;
            walls = new GameObject("Walls");
            walls.transform.parent = dungeon.transform;
        }

        testobj = Instantiate(objectToSpawn, spawnPos, objectToSpawn.transform.rotation,floors.transform);
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
            spawnPos = ray.GetPoint(distance);
            testobj.transform.position = new Vector3(Mathf.Round(spawnPos.x), spawnPos.y, Mathf.Round(spawnPos.z));

        }

        if (mouseClick)
        {
            if (!test)
                return;
            foreach (Transform child in floors.transform)
            {
                if (child.transform.position == testobj.transform.position && child!=testobj.transform)
                    DestroyImmediate(testobj);
            }
            testobj = Instantiate(objectToSpawn, spawnPos, objectToSpawn.transform.rotation, floors.transform);
            testobj.name = objectName + objectID;
            testobj.transform.localScale = Vector3.one * objectScale;

            objectID++;
        }

        if (rightClick)
        {
            test = false;

        }
    }
}
