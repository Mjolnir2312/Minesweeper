using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInZoomOutCamera : MonoBehaviour
{
    Camera mainCamera;

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    [SerializeField]
    float zoomModifierSpeed = 0.1f;
    [SerializeField]
    float panSpeed = 0.1f;


    [SerializeField] private GameObject frameUI;

    private float camSize;
    private Vector3 camPos;

    // Use this for initialization
    void Start()
    {
        mainCamera = GetComponent<Camera>();

        camSize = mainCamera.orthographicSize;
        camPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ZoomingCamera();
        
        if(mainCamera.orthographicSize < camSize)
        {
            frameUI.SetActive(false);
            MoveCameraHorizontally();
        }
        else
        {
            frameUI.SetActive(true);
            ResetCameraPositionIfNeeded();
        }
    }

    void ResetCameraPositionIfNeeded()
    {
        

        if (mainCamera.transform.position != camPos)
        {
            mainCamera.transform.position = camPos;
        }
    }

    void ZoomingCamera()
    {
        bool isZoom = GamePlaySettingsUI.isZooming;
        bool isPlay = GameManager.IsGameOver;
        if (isZoom && !isPlay)
        {
            if (Input.touchCount == 2)
            {
                Touch firstTouch = Input.GetTouch(0);
                Touch secondTouch = Input.GetTouch(1);

                //GameManager.Instance.DisableTile();

                firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
                secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

                touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
                touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

                zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed * Time.deltaTime;

                if (touchesPrevPosDifference > touchesCurPosDifference)
                    mainCamera.orthographicSize += zoomModifier;
                if (touchesPrevPosDifference < touchesCurPosDifference)
                    mainCamera.orthographicSize -= zoomModifier;

            }
            else
                //GameManager.Instance.EnableTile();

            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 5f, 14f);
        }
    }

    void MoveCameraHorizontally()
    {
        bool isDrag = GamePlaySettingsUI.isDragging;

        if (isDrag && (Input.touchCount == 1 || Input.GetMouseButton(0)))
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    float deltaX = touch.deltaPosition.x;
                    float deltaY = touch.deltaPosition.y;
                    Vector3 cameraPos = mainCamera.transform.position;
                    cameraPos.x -= deltaX * panSpeed * Time.deltaTime;
                    cameraPos.y -= deltaY * panSpeed * Time.deltaTime;

                    float cameraHalfHeight = mainCamera.orthographicSize;
                    float cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
                    float bottomBound = -cameraHalfHeight / 2;
                    float topBound = cameraHalfHeight / 2;
                    float leftBound = -cameraHalfWidth / 2;
                    float rightBound = cameraHalfWidth / 2;

                    cameraPos.x = Mathf.Clamp(cameraPos.x, leftBound, rightBound);
                    cameraPos.y = Mathf.Clamp(cameraPos.y, bottomBound, topBound);

                    mainCamera.transform.position = cameraPos;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                float deltaX = Input.GetAxis("Mouse X");
                float deltaY = Input.GetAxis("Mouse Y");
                Vector3 cameraPos = mainCamera.transform.position;
                cameraPos.x -= deltaX * panSpeed * Time.deltaTime;
                cameraPos.y -= deltaY * panSpeed * Time.deltaTime;

                float cameraHalfHeight = mainCamera.orthographicSize;
                float cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
                float bottomBound = -cameraHalfHeight / 2;
                float topBound = cameraHalfHeight / 2;
                float leftBound = -cameraHalfWidth / 2;
                float rightBound = cameraHalfWidth / 2;

                cameraPos.x = Mathf.Clamp(cameraPos.x, leftBound, rightBound);
                cameraPos.y = Mathf.Clamp(cameraPos.y, bottomBound, topBound);

                mainCamera.transform.position = cameraPos;
            }
        }
    }
}
