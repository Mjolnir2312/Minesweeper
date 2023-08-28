using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHolding = false;
    private float holdTime = 1f;
    private float currentTimeHeld = 0f;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        currentTimeHeld = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        currentTimeHeld = 0f;
    }

    private void Update()
    {
        if (isHolding)
        {
            currentTimeHeld += Time.deltaTime;

            if (currentTimeHeld >= holdTime)
            {
                Debug.Log("keep");
                currentTimeHeld = 0f;
            }
        }
    }
}
