using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public float zoomSpeed = 5f, minZoom = 2f, maxZoom = 5f;

    public Vector2 minPanLimit = new Vector2(0.15f, 0.8f);
    public Vector2 maxPanLimit = new Vector2(5.4f, 3.8f);
    public float newSize;
    public Camera cam;
    public Vector3 dragOrigin;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isTabOpen)
            return;
        

        HandlePan();
        HandleZoom();
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            newSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
            cam.orthographicSize = newSize;
        }
    }

    void HandlePan()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = transform.position + difference;

            float t = Mathf.InverseLerp(minZoom, maxZoom, cam.orthographicSize);
            Vector2 currentPanLimit = Vector2.Lerp(maxPanLimit, minPanLimit, t);

            newPosition.x = Mathf.Clamp(newPosition.x, -currentPanLimit.x, currentPanLimit.x);
            newPosition.y = Mathf.Clamp(newPosition.y, -currentPanLimit.y, currentPanLimit.y);

            transform.position = newPosition;
        }
    }
}
