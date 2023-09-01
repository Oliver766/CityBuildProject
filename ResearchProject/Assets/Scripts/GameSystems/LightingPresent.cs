using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// script by Oliver lancashire
// sid 1901981
[System.Serializable]
[CreateAssetMenu(fileName ="Lighting Present", menuName ="Scriptable/Lighting Present",order =1)]
public class LightingPresent : ScriptableObject
{
    [Header("Gradients")]
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;


}
