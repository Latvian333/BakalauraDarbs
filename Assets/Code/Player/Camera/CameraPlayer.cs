using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0.0f, 2.0f, -5.0f);
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 1.0f;

    void LateUpdate()
    {
        
        Vector3 targetPosition = playerTransform.position - playerTransform.forward * offset.z + playerTransform.up * offset.y;

        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.forward, Vector3.up);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}
