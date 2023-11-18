#if UNITY_EDITOR
using UnityEngine;
using UnityEditor.Toolbars;
using UnityEditor;
using UnityEditor.SceneManagement;

[EditorToolbarElement(ID, typeof(SceneView))]
class SceneSwitcherDropdown : EditorToolbarDropdown
{
    // Editor toolbar dropdown ID. 
    public const string ID = "SceneToolbar/Dropdown";
    
    // Path to the scenes folder.
    const string SCENES_PATH = "Assets/Scenes/";
    
    // The name of the currently selected scene.
    static string dropChoice = null;

    public SceneSwitcherDropdown()
    {
        clicked += ShowDropdown;
        text = EditorSceneManager.GetActiveScene().name;
        icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/SceneSwitcher/Editor/Icons/Unity.png");

        dropChoice = EditorSceneManager.GetActiveScene().name;
    }

    // Displays a drop-down list with scenes to choose from.
    private void ShowDropdown()
    {
        var menu = new GenericMenu();
        var guids = AssetDatabase.FindAssets("t:scene", new string[] { SCENES_PATH });
        foreach (var guid in guids)
        {
            var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(AssetDatabase.GUIDToAssetPath(guid));
            menu.AddItem(new GUIContent(scene.name), dropChoice == scene.name, () => { dropChoice = OpenScene(scene.name) ? scene.name : dropChoice; });
        }
        menu.ShowAsContext();
    }

    // Opens a scene after selecting it in the drop-down list. If the scene is modified — offers to save it.
    private bool OpenScene(string sceneName)
    {
        if (EditorSceneManager.GetActiveScene().isDirty)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                OpenSceneInternal();
            else
                return false;
        }
        else
            OpenSceneInternal();

        void OpenSceneInternal()
        {
            EditorSceneManager.OpenScene(SCENES_PATH + sceneName + ".unity", OpenSceneMode.Single);
            text = sceneName;
        }

        return true;
    }
}
#endif