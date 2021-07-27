using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BottleStream : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private ParticleSystem splashParticle;

    public string potionName;
    private float timer = 0.0f;

    private Coroutine pourRoutine;
    private Vector3 targetPosition;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while(gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();

            MoveToPosition(0, transform.position);
            //MoveToPosition(1, targetPosition);
            AnimateToPosition(1, targetPosition);

            yield return null;
        }
    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
    }

    private IEnumerator EndPour()
    {
        while(!HasReachPosition(0, targetPosition))
        {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);

            yield return null;
        }

        Destroy(gameObject);
    }

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 2.0f);
        Vector3 endPoint;

        if (hit.collider) {
            endPoint = hit.point;
            timer += Time.deltaTime;
            Debug.Log(hit.collider.name);

            if (timer > 1.0f)
            {
                Potion potion = hit.collider.GetComponent<Potion>();
                if (potion) {
                    Potion.PotionInfo info = PotionController.Instance.FindMix(potion.Info.colorName, potionName);
                    if (info != null) potion.SetInfo(info);
                    Debug.Log("Info changed: " + info);
                    //Debug.Log("Info changed: " + potion.Info.colorName + " / " + potionName);
                }
            }
        } else {
            endPoint = ray.GetPoint(2.0f);
            timer = 0;
        }

        return endPoint;
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPosition);
    }

    private bool HasReachPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        return currentPosition == targetPosition;
    }

    private IEnumerator UpdateParticle()
    {
        while(gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = targetPosition;

            bool isHitting = HasReachPosition(1, targetPosition);
            splashParticle.gameObject.SetActive(isHitting);

            yield return null;
        }
    }
}
