namespace PivecLabs.Tools
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEditor;
	
[ExecuteInEditMode]

[CustomEditor(typeof(ScreenCapture))]
	public class ScreenCaptureEditor : Editor 
{
	
	private int headerWidthCorrectionForScaling = 38;
	public string headerFlexibleStyle = "Box";

	private Texture2D header;

	public Color backgroundColorByDefault;

	
	SerializedProperty imageformat;
	SerializedProperty hires;
	SerializedProperty imagePath;
	SerializedProperty logdata;
	SerializedProperty displayCanvas;
	SerializedProperty canvas;
	SerializedProperty timer;
	SerializedProperty repeat;
	SerializedProperty camera;
	SerializedProperty selectedKey;
   
	void OnEnable()
	{
		backgroundColorByDefault = EditorGUIUtility.isProSkin
			? new Color(56, 56, 56, 255)
			: Color.white;

		header = Resources.Load("piveclabs") as Texture2D;


		
		imageformat = serializedObject.FindProperty("imageformat");
		hires = serializedObject.FindProperty("hires");
		imagePath = serializedObject.FindProperty("imagePath");
		displayCanvas = serializedObject.FindProperty("displayCanvas");
		canvas = serializedObject.FindProperty("canvas");
		logdata = serializedObject.FindProperty("logdata");
		timer = serializedObject.FindProperty("timer");
		repeat = serializedObject.FindProperty("repeat");
		camera = serializedObject.FindProperty("camera");
		selectedKey = serializedObject.FindProperty("selectedKey");
		EditorUtility.SetDirty(this);

}

	public override void OnInspectorGUI()
	{
	
		DrawEditorByDefaultWithHeaderAndHelpBox();


		serializedObject.Update();
		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(selectedKey,new GUIContent("Selected Key"));
		EditorGUILayout.Space();

		EditorGUILayout.PropertyField(imageformat,new GUIContent("Image Format"));
		EditorGUILayout.PropertyField(imagePath,new GUIContent("Image Prefix"));
		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(timer,new GUIContent("Delay start (sec)"));
		EditorGUILayout.PropertyField(repeat,new GUIContent("Repeat every (sec)"));
		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(hires,new GUIContent("Hi Res"));
		if (hires.boolValue == true)
		{
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(camera,new GUIContent("Hi Res Camera"));
			EditorGUI.indentLevel--;
	
		}
		EditorGUILayout.Space();
		EditorGUILayout.PropertyField(displayCanvas,new GUIContent("Display Canvas"));
		if (displayCanvas.boolValue == true)
		{
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(canvas,new GUIContent("Canvas to Use"));
			EditorGUI.indentLevel--;
	
		}
		EditorGUILayout.Space();

		EditorGUILayout.PropertyField(logdata,new GUIContent("Log Data"));

		serializedObject.ApplyModifiedProperties();
		EditorUtility.SetDirty(this);

	}
	
	
	public void DrawEditorByDefaultWithHeaderAndHelpBox()
	{

		DrawHeaderFlexible(header, Color.black);

		GUI.backgroundColor = backgroundColorByDefault;

		DrawHelpBox();

	}

	public void DrawHeaderFlexible(Texture2D header, Color backgroundColor)
	{
		if (header)
		{
			GUI.backgroundColor = backgroundColor;

			if (header.width + headerWidthCorrectionForScaling < EditorGUIUtility.currentViewWidth)
			{
				EditorGUILayout.BeginVertical(headerFlexibleStyle);

				DrawHeader(header);

				EditorGUILayout.EndVertical();
			}
			else
			{
				DrawHeaderIfScrollbar(header);
			}
		}
	}

	public void DrawHeaderIfScrollbar(Texture2D header)
	{
		EditorGUI.DrawTextureTransparent(
			GUILayoutUtility.GetRect(
			EditorGUIUtility.currentViewWidth - headerWidthCorrectionForScaling,
			header.height),
			header,
			ScaleMode.ScaleToFit);
	}

	public void DrawHeader(Texture2D header)
	{
		EditorGUI.DrawTextureTransparent(
			GUILayoutUtility.GetRect(
			header.width,
			header.height),
			header,
			ScaleMode.ScaleToFit);
	}


	public void DrawHelpBox()
	{
		GUI.backgroundColor = backgroundColorByDefault;

		LinkButton("https://docs.piveclabs.com");


	}

	private void LinkButton(string url)
	{
		var style = GUI.skin.GetStyle("HelpBox");
		style.richText = true;
		style.alignment = TextAnchor.MiddleCenter;

		bool bClicked = GUILayout.Button("<b>Online Documentation can be found at https://docs.piveclabs.com</b>", style);

		var rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);
		if (bClicked)
			Application.OpenURL(url);
	}
	}
}