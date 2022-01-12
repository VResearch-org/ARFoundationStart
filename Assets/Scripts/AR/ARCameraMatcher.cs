using UnityEngine;
/// <summary>
/// To be used whenever there are more cameras parented under the main camera (For layered rendering etc.)
/// Should be on any parented camera
/// </summary>
public class ARCameraMatcher : MonoBehaviour
{
    private Camera _sourceCamera, _myCamera;

    private void OnEnable()
    {
        _myCamera = GetComponent<Camera>();
        _sourceCamera = Camera.main;
    }

    private void Update()
    {
        if (_myCamera == null)
            return;
        if (_sourceCamera == null)
            return;

        _myCamera.projectionMatrix = _sourceCamera.projectionMatrix;
    }
}