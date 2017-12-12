﻿using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Unity Official Samples at:
//    - https://docs.unity3d.com/560/Documentation/Manual/editor-EditorWindows.html
//    - https://docs.unity3d.com/ScriptReference/EditorGUILayout.ColorField.html


public class AuXtraUI : EditorWindow
{
	//	string myString = "Hello World";
	//	float myFloat = 1.23f;

	Color auXtraUiNormalColor = new Color(0.9F, 0.8F, 0.7F, 1F);
	Color auXtraUiHighlightedColor = new Color(0.8F, 0.7F, 0.6F, 1F);
	Color auXtraUiPressedColor = new Color(0.8F, 0.7F, 0.6F, 1F);
	Color auXtraUiBackgroundColor = new Color(0F, 0F, 0F, 0.65F);
	bool auXtraUiEditAll = false;
	public UILogin UILogin;
	Texture2D aUimage;
	public SceneAsset gameScene;
	void Start() {
		gameScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Artwork/mymodel.fbx");

	}

	// Add menu category "ADDon uMMORPG" and item named "Xtra UI" to the Window menu
	[MenuItem("Window/ADDon uMMORPG/Xtra UI")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow (typeof(AuXtraUI));
	}

	void OnEnable()
	{	
		aUimage = Resources.Load("ADDon-uMMORPG", typeof(Texture2D)) as Texture2D;
		OnHierarchyChange ();
	}


	void OnHierarchyChange()
	{
		var all = Resources.FindObjectsOfTypeAll(typeof(UILogin));

		if (all.Where (obj => (obj.hideFlags & HideFlags.HideInHierarchy) != HideFlags.HideInHierarchy).Count () == 1) {
			UILogin = (UILogin)all[0];
		}

//		UILogin AuUILogin = (UILogin) gameScene.FindObjectOfType(typeof(UILogin));
	}
	void OnGUI()
	{
		GUILayout.Label (aUimage);

		UILogin = EditorGUILayout.ObjectField ("UILogin GameObject", UILogin, typeof(UILogin)) as UILogin;

		//		GUILayout.Label ("Base UI Color Settings", EditorStyles.boldLabel);

		//		myString = EditorGUILayout.TextField ("Text Field", myString);
		auXtraUiEditAll = EditorGUILayout.BeginToggleGroup ("XtraUI Login Panel", auXtraUiEditAll);
		auXtraUiNormalColor = EditorGUILayout.ColorField ("UI Normal Color", auXtraUiNormalColor);
		auXtraUiHighlightedColor = EditorGUILayout.ColorField ("UI Highlighted Color", auXtraUiHighlightedColor);
		auXtraUiPressedColor = EditorGUILayout.ColorField ("UI Pressed Color", auXtraUiPressedColor);
		auXtraUiBackgroundColor = EditorGUILayout.ColorField ("UI Background Color", auXtraUiBackgroundColor);
		//		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
		if (GUILayout.Button ("Apply Change")) {
			UILogin.setBaseColors (auXtraUiNormalColor, auXtraUiHighlightedColor, auXtraUiPressedColor, auXtraUiBackgroundColor);

			EditorUtility.SetDirty(UILogin);
			SceneView.RepaintAll ();
		}
		EditorGUILayout.EndToggleGroup ();

	}
}