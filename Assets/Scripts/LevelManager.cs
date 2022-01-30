using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Rigidbody originalLight;
    public Material HurtMaterial;
    public GameObject LightLayer, DarkLayer;
    private void Start() => LevelManager.instance = this;
    public void ToggleWorld(bool isDark)
    {
        LightLayer.SetActive(!isDark);
        DarkLayer.SetActive(isDark);
    }
}
