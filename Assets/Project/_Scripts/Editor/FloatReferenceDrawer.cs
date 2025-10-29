using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        SerializedProperty useConstant = property.FindPropertyRelative("useConstant");

        SerializedProperty constValue = property.FindPropertyRelative("constantValue");

        SerializedProperty variable = property.FindPropertyRelative("variable");


  
        EditorGUI.BeginProperty(position, label, property);

        Rect r;
        // Draw name        
        r = EditorGUI.PrefixLabel(position, label); // returns the space is going the label takes      

        r.width = 90;
        useConstant.boolValue = EditorGUI.ToggleLeft(r, "Use Constant", useConstant.boolValue);
  
        r.xMin += 95;
        r.xMax = position.xMax;
        r.height = EditorGUIUtility.singleLineHeight;

        if (useConstant.boolValue)
        {
            constValue.floatValue = EditorGUI.FloatField(r, constValue.floatValue);
        }
        else
        {
            //EditorGUI.ObjectField(r, variable);
            variable.objectReferenceValue = EditorGUI.ObjectField(r, "", variable.objectReferenceValue, typeof(FloatVariable), false);
        }

        EditorGUI.EndProperty();

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight ;
    }
}
