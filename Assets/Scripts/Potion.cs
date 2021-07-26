using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [Serializable]
    public class PotionInfo
    {
        public string colorName;
        public List<string> componentNames;
        public Material fillMaterial;
        public Material splashMaterial;
        public Material streamMaterial;

        public GameObject potionEffect;
    }


    public GameObject fill;

    [SerializeField]
    public PotionInfo Info;

    public void SetInfo(PotionInfo info)
    {
        fill.GetComponent<MeshRenderer>().material = info.fillMaterial;
        Info = info;
    }


}
