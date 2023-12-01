using UnityEngine;

public class CameraController : MonoBehaviour
{    
    [Header("Tracking Settings")]
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector3 baseFollowOffset = new Vector3(0f, 4f, -6f);
    [SerializeField, Range(0f, 180f)] private float orbitSpeed = 90f;
    [SerializeField, Range(0f, 100f)] private float zoomSpeed = 10f;
    [SerializeField, Range(0f, 1f)] private float minZoom = 0.5f;
    [SerializeField, Range(1f, 5f)] private float maxZoom = 2f;

    [Header("Enabled Actions")]
    [SerializeField] private bool orbit = true;
    [SerializeField] private bool zoom = true;

    private Vector3 currentOffset;
    private float currentZoom;


    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;

        currentOffset = baseFollowOffset;
        currentZoom = 1f;
    }

    void LateUpdate ()
    {
        if (!followTarget)
            return;
        
        ProcessOrbit();
        ProcessZoom();

        transform.position = followTarget.position + currentOffset * currentZoom;

        transform.LookAt(followTarget.position);
    }


    private void ProcessOrbit ()
    {
        if (!orbit)
            return;

        float inputValue = Input.GetAxis("Mouse X");
        currentOffset = Quaternion.AngleAxis(inputValue * orbitSpeed * Time.deltaTime, Vector3.up) * currentOffset;
    }
    
    private void ProcessZoom ()
    {
        if (!zoom)
            return;

        float inputValue = Input.GetAxis("Mouse ScrollWheel");
        currentZoom = Mathf.Clamp(currentZoom - inputValue * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
    }
}
