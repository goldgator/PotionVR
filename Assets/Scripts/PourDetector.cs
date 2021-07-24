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
        return Mathf.Abs(transform.forward.y * Mathf.Rad2Deg);
    }

    private BottleStream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<BottleStream>();
    }
}
