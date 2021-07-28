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

    public float breakVelocity = 2f;
    public GameObject fill;

    [SerializeField]
    public PotionInfo Info;

    public void SetInfo(PotionInfo info)
    {
        fill.GetComponent<MeshRenderer>().material = info.fillMaterial;
        Info = info;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > breakVelocity)
        {
            Instantiate(Info.potionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
