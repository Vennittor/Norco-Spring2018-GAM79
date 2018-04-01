using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(IntRange), true)]
public class IntRangeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        //Ensure the properties are not idented
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        //Calculate the rects
        Rect minText = new Rect(position.x, position.y, 30, position.height);
        Rect minRect = new Rect(position.x + 30, position.y, 50, position.height);
        Rect maxText = new Rect(position.x + 85, position.y, 30, position.height);
        Rect maxRect = new Rect(position.x + 115, position.y, 50, position.height);

        //Draw fields
        EditorGUI.LabelField(minText, "Min ");
        EditorGUI.LabelField(maxText, "Max ");
        EditorGUI.PropertyField(minRect, property.FindPropertyRelative("m_Min"), GUIContent.none);
        EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("m_Max"), GUIContent.none);

        //Set it back
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
