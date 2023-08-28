//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class InputTileTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
//{

//    Tile tile;

//    float timePointerDown = 0.0f;

//    bool isDrag;

//    private void Awake()
//    {
//        tile = GetComponent<Tile>();
//    }

//    public void OnPointerDown(PointerEventData eventData)
//    {
//        timePointerDown = Time.time;

//        StopAllCoroutines();
//        StartCoroutine(DetectLongPressCoroutine());
//    }

//    IEnumerator DetectLongPressCoroutine()
//    {
//        float elapsedTime = 0.0f;

//        while (true)
//        {
//            elapsedTime += Time.deltaTime;

//            if (isDrag == false && elapsedTime > 0.5f)
//            {
//                Debug.Log("Keep");
              
//                yield break;
//            }

//            yield return null;
//        }
//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        StopAllCoroutines();

//        if (isDrag)
//        {
//            isDrag = false;
//            return;
//        }

//        float currentTime = Time.time;
//        float elapsed = currentTime - timePointerDown;

//        //Normal click
//        if (elapsed < 0.5f)
//        {
//            //MessageSystem.Send<ITileClickListener>(listener =>
//            //{
//            //    listener.OnTileClick(tile);
//            //});
//            Debug.Log("Don't keep");
//        }
//    }

//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        isDrag = true;
//    }

//    public void OnDrag(PointerEventData eventData) { }
//}
