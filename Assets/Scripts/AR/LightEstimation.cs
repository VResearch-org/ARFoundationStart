using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    public ARCameraManager aRCameraManager;
    private Light _currentLight;

    private void Awake()
    {
        _currentLight = GetComponent<Light>();
        _currentLight.transform.rotation = Quaternion.identity;
    }

    private void OnEnable()
    {
        aRCameraManager.frameReceived += FrameUpdated;
    }

    private void OnDisable()
    {
        aRCameraManager.frameReceived -= FrameUpdated;
    }

    private void FrameUpdated(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
            _currentLight.intensity = args.lightEstimation.averageBrightness.Value;

        if (args.lightEstimation.averageColorTemperature.HasValue)
            _currentLight.colorTemperature = args.lightEstimation.averageColorTemperature.Value;

        if (args.lightEstimation.colorCorrection.HasValue)
            _currentLight.color = args.lightEstimation.colorCorrection.Value;

        if (args.lightEstimation.mainLightDirection.HasValue)
            _currentLight.transform.rotation = Quaternion.LookRotation(args.lightEstimation.mainLightDirection.Value);

        if (args.lightEstimation.mainLightColor.HasValue)
            _currentLight.color = args.lightEstimation.mainLightColor.Value;

        if (args.lightEstimation.mainLightIntensityLumens.HasValue)
            _currentLight.intensity = args.lightEstimation.averageMainLightBrightness.Value;

        if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = args.lightEstimation.ambientSphericalHarmonics.Value;
        }
    }
}
