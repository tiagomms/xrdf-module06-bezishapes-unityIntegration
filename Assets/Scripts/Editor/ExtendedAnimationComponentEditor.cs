using UnityEditor;
using UnityEngine;
using Animation = Bezi.Bridge.Animation;

[CustomEditor(typeof(ExtendedAnimationComponent))]
public class ExtendedAnimationComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ExtendedAnimationComponent component = (ExtendedAnimationComponent)target;

        if (component.animationComponent == null)
        {
            EditorGUILayout.HelpBox("Base Animation is not assigned.", MessageType.Warning);
            return;
        }

        // Draw the original Animation properties
        EditorGUILayout.LabelField("Base Animation", EditorStyles.boldLabel);
        Editor.CreateEditor(component.animationComponent).OnInspectorGUI();

        // Draw the new fields
        if (component.extendedAnimation != null)
        {
            EditorGUILayout.LabelField("Extended Properties", EditorStyles.boldLabel);
            component.extendedAnimation.updateScale = EditorGUILayout.Toggle("Update Scale", component.extendedAnimation.updateScale);
            component.extendedAnimation.materialFadeIn = EditorGUILayout.Toggle("Material Fade In", component.extendedAnimation.materialFadeIn);
            component.extendedAnimation.materialFadeOut = EditorGUILayout.Toggle("Material Fade Out", component.extendedAnimation.materialFadeOut);
            component.extendedAnimation.notTransformAnimation = EditorGUILayout.Toggle("Not Transform Animation", component.extendedAnimation.notTransformAnimation);
            component.extendedAnimation.materialCustomAnimation = EditorGUILayout.Toggle("Material Custom Animation", component.extendedAnimation.materialCustomAnimation);
        }

        // Save any changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}