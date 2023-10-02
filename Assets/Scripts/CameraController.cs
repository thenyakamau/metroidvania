using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float xOffset = 0f;
    [SerializeField] private float yOffset = 0f;
    [SerializeField] private float followSpeed = 1f;

    // Update is called once per frame
    private void Update()
    {
        Vector3 cameraPosition = transform.position;
        Vector3 position = playerTransform.position;

        float xTarget = position.x + xOffset;
        float yTarget = position.y + yOffset;

        float xNewPos = Mathf.Lerp(cameraPosition.x, xTarget, Time.deltaTime * followSpeed);
        float yNewPos = Mathf.Lerp(cameraPosition.y, yTarget, Time.deltaTime * followSpeed);

        transform.position = new Vector3(xNewPos, yNewPos, cameraPosition.z);
    }
}
