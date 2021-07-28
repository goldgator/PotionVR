using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public float pourThreshold = 45f;
    public Transform origin;
    public GameObject streamPrefab;

    private bool isPouring = false;
    private BottleStream currentStream;

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() > pourThreshold;
        //Debug.Log(CalculatePourAngle());

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            } else
            {
                EndPour();
            }
        }
    }

    private void StartPour()
    {
        Debug.Log("Start");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        Debug.Log("End");
        currentStream.End();
        currentStream = null;
    }

    private float CalculatePourAngle()
    {
        Vector3 tipAngles = new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        Quaternion tipQuat = Quaternion.Euler(tipAngles);

        return Mathf.Abs(Quaternion.Angle(tipQuat, Quaternion.identity));
    }

    private BottleStream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);

        //TODO change materials and set potion info
        Potion.PotionInfo info = GetComponent<Potion>().Info;

        streamObject.GetComponent<BottleStream>().potionName = info.colorName;
        streamObject.GetComponent<LineRenderer>().material = info.streamMaterial;
        streamObject.GetComponentInChildren<ParticleSystemRenderer>().material = info.splashMaterial;


        return streamObject.GetComponent<BottleStream>();
    }
}
