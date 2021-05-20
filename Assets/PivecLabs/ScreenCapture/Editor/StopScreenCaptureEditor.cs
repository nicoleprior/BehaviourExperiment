namespace PivecLabs.Tools
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEditor;
	
[ExecuteInEditMode]

[CustomEditor(typeof(StopScreenCapture))]
	public class StopScreenCaptureEditor : Editor 
{
	SerializedProperty invokerscript;
	SerializedProperty selectedKey;
   
	void OnEnable()
	{
		invokerscript = serializedObject.FindProperty("invokerscript");
		selectedKey = serializedObject.FindProperty("selectedKey");
		EditorUtility.SetDirty(this);

}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(selectedKey,new GUIContent("Selected Key"));
		EditorGUILayout.Space();

		EditorGUILayout.PropertyField(invokerscript,new GUIContent("ScreenCapture PreFab"));

		EditorGUILayout.Space();

		serializedObject.ApplyModifiedProperties();
		EditorUtility.SetDirty(this);

	}
	}
}