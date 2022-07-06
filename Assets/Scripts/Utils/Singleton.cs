// Copyright 2021 by Hextant Studios. https://HextantStudios.com
// This work is licensed under CC BY 4.0. http://creativecommons.org/licenses/by/4.0/
using UnityEngine;

// A MonoBehaviour-based singleton for use at runtime. Add to a single 'global' scene.
// Note: OnEnable() / OnDisable() should be used to register with any global events
// to properly support domain reloads.
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // The singleton instance.
    public static T Instance => _instance ?? (Application.isPlaying ? _instance = FindObjectOfType<T>() : null);

    private static T _instance;

    // Called when the instance is created.
    protected virtual void Awake()
    {
        // Verify there is not more than one instance and assign _instance.
        Debug.Assert(_instance == null || _instance == this, "More than one singleton instance instantiated!", this);
        _instance = (T)this;
    }

    // Clear the instance field when destroyed.
    protected virtual void OnDestroy() => _instance = null;
}
