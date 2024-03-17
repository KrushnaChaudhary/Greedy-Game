using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "NewTemplateData", menuName = "CustomTemplates/TemplateData")]
public class CustomTemplateData : ScriptableObject
{
    public string customTemplateName;
    public List<CustomObjectData> customObjects = new List<CustomObjectData>();

    public void ExportToJson(string destinationPath)
    {
        string jsonData = JsonUtility.ToJson(this, true);
        File.WriteAllText(destinationPath, jsonData);
    }

    public void ImportFromJson(CustomTemplateDataJson templateDataJson, ref CustomTemplateData data)
    {
        data.customTemplateName = templateDataJson.customTemplateName;
        data.customObjects = templateDataJson.customObjects;
    }

}

[Serializable]
public class CustomTemplateDataJson
{
    public string customTemplateName;
    public List<CustomObjectData> customObjects = new List<CustomObjectData>();
}

[Serializable]
public class CustomObjectData
{
    public string customObjectName;
    public Vector3 customPosition;
    public Quaternion customRotation;
    public Vector3 customScale;
    public Color customColor;
    public List<CustomObjectData> customChildren = new List<CustomObjectData>(); // Renamed from 'chields' to 'children'
}

public class CustomTemplateEditor : EditorWindow
{
    private CustomTemplateData _customTemplate;
    private TextAsset _jsonDataFile;

    [MenuItem("Window/Custom Template Editor")]
    public static void ShowCustomWindow()
    {
        GetWindow<CustomTemplateEditor>("Custom Template Editor"); // Renamed window title
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        _jsonDataFile = (TextAsset)EditorGUILayout.ObjectField("JSON File", _jsonDataFile, typeof(TextAsset), false);
        _customTemplate = (CustomTemplateData)EditorGUILayout.ObjectField("CustomTemplateData", _customTemplate, typeof(CustomTemplateData), false);
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            CreateNewCustomTemplate(); // Create new TemplateData
        }
        EditorGUILayout.EndHorizontal();

        if (_customTemplate == null) return;

        // String Field

        // Display CustomTemplateData inspector
        var editor = UnityEditor.Editor.CreateEditor(_customTemplate);
        editor.OnInspectorGUI();

        // Button
        if (GUILayout.Button("Save as JSON template"))
        {
            SaveCustomTemplateAsJson();
        }

        GUILayout.Label("Load JSON File:", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load JSON"))
        {
            LoadJsonDataFile();
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Instantiate Image"))
        {
            GameObject canvasGameObject = GameObject.Find("Canvas");
            foreach (var rootObject in _customTemplate.customObjects)
            {
                InstantiateCustomImage(canvasGameObject.transform, rootObject);
            }
        }
    }

    // Create new CustomTemplateData instance
    private void CreateNewCustomTemplate()
    {
        _customTemplate = CreateInstance<CustomTemplateData>();
        _customTemplate.customTemplateName = "New Custom Template"; // Set default template name
    }

    // Save CustomTemplateData as JSON file
    private void SaveCustomTemplateAsJson()
    {
        string fileName = $"{_customTemplate.customTemplateName}.json";
        string filePath = Path.Combine(Application.dataPath, "CustomTemplates", fileName);
        _customTemplate.ExportToJson(filePath);
        Debug.Log($"Custom template saved as {fileName}");
    }

    private void LoadJsonDataFile()
    {
        if (_jsonDataFile == null)
            return;
        Debug.Log(_jsonDataFile.text);
        var dataLoaded = JsonUtility.FromJson<CustomTemplateDataJson>(_jsonDataFile.text);
        _customTemplate.ImportFromJson(dataLoaded, ref _customTemplate);
    }

    private void InstantiateCustomImage(Transform parentTransform, CustomObjectData objectData)
    {
        // Instantiate Image prefab
        GameObject imagePrefab = Resources.Load<GameObject>("Prefab"); // Load your Image prefab
        if (imagePrefab == null)
        {
            Debug.LogError("Custom Image prefab not found. Make sure you have a Prefab in your Resources folder.");
            return;
        }

        GameObject imageInstance = Instantiate(imagePrefab, parentTransform);
        imageInstance.name = objectData.customObjectName;
        // Set image properties based on objectData
        // Example:
        Image imageComponent = imageInstance.GetComponent<Image>();
        imageComponent.color = objectData.customColor;
        imageComponent.rectTransform.localPosition = objectData.customPosition;
        imageComponent.transform.localScale = objectData.customScale;
        // Instantiate children recursively
        foreach (var childData in objectData.customChildren)
        {
            InstantiateCustomImage(imageInstance.transform, childData);
        }
    }
}
