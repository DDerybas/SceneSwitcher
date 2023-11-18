#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Overlays;

[Overlay(typeof(SceneView), "Scene Switcher")]
public class SceneSwitcherOverlay : ToolbarOverlay
{
    // ToolbarOverlay implements a parameterless constructor, passing the EditorToolbarElementAttribute ID.
    // This is the only code required to implement a toolbar Overlay. Unlike panel Overlays, the contents are defined
    // as standalone pieces that will be collected to form a strip of elements.

    SceneSwitcherOverlay() : base(SceneSwitcherDropdown.ID) { }
}
#endif