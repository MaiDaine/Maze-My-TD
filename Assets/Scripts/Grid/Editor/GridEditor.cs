using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MazeMyTD
{
    public class GridEditor : EditorWindow
    {
        public Tile.TileState tileOptions;

        [MenuItem("Window/GridEditor")]
        public static void ShowWindow()
        {
            GetWindow<GridEditor>("GridEditor");
        }

        private void OnGUI()
        {

#if UNITY_EDITOR
            //Grid Construction
            if (GUILayout.Button("Spawn Grid"))
                FindObjectOfType<GridManager>().SpawnGrid();
            if (GUILayout.Button("Refresh Grid"))
            {
                FindObjectOfType<GridManager>().UpdateTilePosition();
                SetTileStatusOnInnateBuildings();
            }
            if (GUILayout.Button("Destroy Grid"))
                FindObjectOfType<GridManager>().DestroyGrid();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            //Tile Selection
            tileOptions = (Tile.TileState)EditorGUILayout.EnumPopup("Type to select:", tileOptions);
            if (GUILayout.Button("Select"))
                TileSelection(tileOptions);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

        private void SetTileStatusOnInnateBuildings()
        {
           InnateElement[] innateElements = FindObjectsOfType<InnateElement>();
            for (int i = 0; i < innateElements.Length; i++)
                innateElements[i].SetInvalidStatusOnTile();
        }

        private void TileSelection(Tile.TileState tileState)
        {
            List<Tile> result = ArrayUtility.FindAll(FindObjectsOfType<Tile>(), (t) => t.tileState == tileState);
            GameObject[] results = new GameObject[result.Count];

            int i = 0;
            foreach (Tile tile in result)
            {
                results[i] = tile.gameObject;
                i++;
            }

            Selection.objects = results;
        }
#endif
    }
}