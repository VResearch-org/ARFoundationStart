using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceContent : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GraphicRaycaster raycaster;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsClickOverUI())
        {
            List<ARRaycastHit> hitPoints = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.mousePosition, hitPoints, TrackableType.Planes);

            if(hitPoints.Count > 0)
            {
                Pose pose = hitPoints[0].pose;
                transform.rotation = pose.rotation;
                transform.position = pose.position;
            }
        }
    }

    bool IsClickOverUI()
    {
        PointerEventData data = new PointerEventData(EventSystem.current) {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);
        return results.Count > 0;
    }

    //alternatively, to put in extensions class (no need to keep track of canvases)
    public static bool IsPointerOverGameObject()
    {
#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
#else

        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }
        }
#endif
        return false;
    }
}
