using UnityEditor;
using UnityEngine;

// Must be placed in the Editor folder

/// <summary>
/// Custom editor window for modifying the game speed in the Unity editor before/during play mode
/// </summary>
public class GameSpeedModifier : EditorWindow
{
    private float gameSpeed = 1.0f; // Default game speed
    private int minSpeed = 0; // Default minimum value
    private int maxSpeed = 5; // Default maximum value
    private bool useIntegerValues = true; // Toggle for using integer values

    /// <summary>
    /// Shows the Game Speed Modifier window in the Unity editor
    /// </summary>
    [MenuItem("Window/Game Speed Modifier")]
    public static void ShowWindow()
    {
        GetWindow<GameSpeedModifier>("Game Speed Modifier");
    }

    /// <summary>
    /// Displays the contents of the Game Speed Modifier window
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("Modify Game Speed", EditorStyles.boldLabel);

        // Input fields for min and max values
        minSpeed = Mathf.Max(0, EditorGUILayout.IntField("Min Speed", minSpeed)); // Ensure minSpeed is at least 0
        maxSpeed = Mathf.Max(1, EditorGUILayout.IntField("Max Speed", maxSpeed)); // Ensure maxSpeed is at least 1

        // Ensure minSpeed is not greater than maxSpeed
        if (minSpeed > maxSpeed)
        {
            minSpeed = maxSpeed;
        }

        // Ensure maxSpeed is not less than minSpeed
        if (maxSpeed < minSpeed)
        {
            maxSpeed = minSpeed;
        }

        // Toggle for using integer values for game speed
        useIntegerValues = EditorGUILayout.Toggle("Use Integer Values", useIntegerValues);

        // Create a slider between the user-defined min and max values to adjust game speed
        if (useIntegerValues)
        {
            gameSpeed = EditorGUILayout.IntSlider("Game Speed", (int)gameSpeed, minSpeed, maxSpeed);
        }
        else
        {
            gameSpeed = EditorGUILayout.Slider("Game Speed", gameSpeed, minSpeed, maxSpeed);
        }

        // Apply the new game speed
        Time.timeScale = gameSpeed;

        // Button to reset game speed to 1
        if (GUILayout.Button("Reset Game Speed"))
        {
            gameSpeed = 1.0f;
            Time.timeScale = gameSpeed;
        }
    }
}
