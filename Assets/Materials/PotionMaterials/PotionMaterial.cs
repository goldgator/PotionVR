using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionMaterial", menuName = "Material/PotionMaterial")]
public class PotionMaterial : ScriptableObject
{
    public Material splashMaterial;
    public Material streamMaterial;
}
