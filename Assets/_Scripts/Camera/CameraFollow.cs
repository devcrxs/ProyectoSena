using System.Collections;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float timeLerp;
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        transform.position = playerTransform.position;
    }

    private void LateUpdate()
    {
        StartCoroutine(RotateObject());
    }

    private IEnumerator RotateObject()
    {
        float startRotation = transform.localEulerAngles.y;
        float yRotation;
        float elapsedTime = 0f;
        while (elapsedTime < timeLerp)
        {
            elapsedTime += Time.deltaTime;
            yRotation = Mathf.Lerp(startRotation, playerTransform.localEulerAngles.y, (elapsedTime / timeLerp));
            transform.rotation = Quaternion.Euler(0f,yRotation,0f);
            yield return null;
        }
    }
}
