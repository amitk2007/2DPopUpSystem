using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ImageOrWeb
{
    image,
    web,
}

[CustomEditor(typeof(PopUpScript))]
[CanEditMultipleObjects]
public class PopUpEditorScript : Editor
{
    PopUpScript popUp;
    ImageOrWeb isBGImageWeb;
    ImageOrWeb isButtonImageWeb;
    bool ShowBG = true;
    bool ShowButton = true;
    #region SerializedPropertys
    SerializedProperty BGImage;
    SerializedProperty buttonImage;
    #endregion

    void OnEnable()
    {
        popUp = (PopUpScript)target;

        #region Set SerializedPropertys
        BGImage = serializedObject.FindProperty("BGImage");
        buttonImage = serializedObject.FindProperty("buttonImage");
        #endregion
    }

    public override void OnInspectorGUI()
    {
        popUp.popUpSide = (PopUpSides)EditorGUILayout.EnumPopup("Pop Up Animation", popUp.popUpSide);
        popUp.isPreMade = EditorGUILayout.Toggle("Is pop up pre made", popUp.isPreMade);
        if (EditorGUILayout.BeginFadeGroup(!popUp.isPreMade ? 1 : 0))
        {
            popUp.titleText = EditorGUILayout.TextField("Title", popUp.titleText);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Content Text", GUILayout.ExpandWidth(false));

            popUp.contentText = EditorGUILayout.TextArea(popUp.contentText, GUILayout.Height(50), GUILayout.ExpandWidth(true));

            EditorGUILayout.EndHorizontal();


            ShowBG = EditorGUILayout.BeginFoldoutHeaderGroup(ShowBG, "Background");
            if (ShowBG)
            {
                ShowBGImage();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            ShowButton = EditorGUILayout.BeginFoldoutHeaderGroup(ShowButton, "Button");
            if (ShowButton)
            {
                ShowButtonImage();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        EditorGUILayout.EndFadeGroup();

        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Shows the GUI layout for the background settings
    /// </summary>
    void ShowBGImage()
    {
        isBGImageWeb = popUp.isBGURL ? ImageOrWeb.web : ImageOrWeb.image;
        isBGImageWeb = (ImageOrWeb)EditorGUILayout.EnumPopup("Source", isBGImageWeb);
        popUp.isBGURL = (isBGImageWeb == ImageOrWeb.web);

        if (isBGImageWeb == ImageOrWeb.image)
            EditorGUILayout.PropertyField(BGImage, new GUIContent("Background Image"));
        else
            popUp.BGURL = EditorGUILayout.TextField("Background URL", popUp.BGURL);

        popUp.BGColor = EditorGUILayout.ColorField("Background Color", popUp.BGColor);
    }

    /// <summary>
    /// Shows the GUI layout for the button settings
    /// </summary>
    void ShowButtonImage()
    {
        popUp.buttonText = EditorGUILayout.TextField("Button text", popUp.buttonText);

        isButtonImageWeb = popUp.isButtonURL ? ImageOrWeb.web : ImageOrWeb.image;
        isButtonImageWeb = (ImageOrWeb)EditorGUILayout.EnumPopup("Source", isButtonImageWeb);
        popUp.isButtonURL = (isButtonImageWeb == ImageOrWeb.web);

        if (isButtonImageWeb == ImageOrWeb.image)
            EditorGUILayout.PropertyField(buttonImage, new GUIContent("Button Image"));
        else
            popUp.buttonURL = EditorGUILayout.TextField("Button URL", popUp.buttonURL);
    }

    void DrawGUILine(int i_height = 1)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, i_height);
        rect.height = i_height;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
    }
}
