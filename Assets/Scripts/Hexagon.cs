using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    public Color spriteNewColor = Color.blue;

    public void ColorSprite(Color color)
    {
        sr.color = color;
    }
    void Start()
    {

    }


    void Update()
    {

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Hexagon))]
    public class HexagonEditor : Editor
    {
        private SerializedProperty displayColor;

        private void OnEnable()
        {
            displayColor = serializedObject.FindProperty("spriteNewColor");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Hexagon hex = (Hexagon)target;
            if (GUILayout.Button("apply color", GUILayout.Width(90f)))
            {
                hex.ColorSprite(displayColor.colorValue);
            }
        }
    }
#endif

}