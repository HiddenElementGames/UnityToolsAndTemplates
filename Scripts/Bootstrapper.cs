using UnityEngine;

/// <summary>
/// Initializes all managers before the first scene is loaded
/// </summary>
public class Bootstrapper
{
    /// <summary>
    /// Initializes all managers before the first scene is loaded
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // RuntimeInitializeLoadType can be BeforeSceneLoad, AfterSceneLoad, AfterAssembliesLoaded, BeforeSplashScreen, or SubsystemRegistration
    private static void Initialize()
    {
        // GameManager.Initialize();
        // EventManager.Initialize();
        // AudioManager.Initialize();
    }
}
