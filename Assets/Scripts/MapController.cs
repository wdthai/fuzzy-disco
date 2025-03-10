using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5 0.1, 0.8
// 5.5 0.1 0.3
// 
//
//
// 2 5.7 3.8

public class MapController : MonoBehaviour
{
    public float zoomSpeed = 5f, minZoom = 2f, maxZoom = 5f;

    public Vector2 minPanLimit = new Vector2(0.15f, 0.8f);
    public Vector2 maxPanLimit = new Vector2(5.4f, 3.8f);
    public float newSize;


    public Camera cam;
    public Vector3 dragOrigin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetMouseButtonDown(1)) // Left-click to start dragging
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1)) // While holding left-click
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
