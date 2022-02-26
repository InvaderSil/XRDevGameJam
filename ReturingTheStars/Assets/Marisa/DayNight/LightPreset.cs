using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName ="Light Preset", menuName ="Sciptables/Light Preset", order = 1)]
public class LightPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalLight;
    public Gradient FogColor;
}
