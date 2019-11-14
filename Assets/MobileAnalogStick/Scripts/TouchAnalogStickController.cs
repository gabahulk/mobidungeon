using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TouchAnalogStickController : MonoBehaviour, ICharacterController
{

    private float horizontal;
    private float vertical;
    private float analogStickBackgroundRadius;
    private Image analogStickImage;
    private RectTransform analogStickRectTransform;
    private Vector2 uiOffset;
    private RectTransform CanvasRect;
    private RectTransform rectTransform;

    public GameObject analogStickControler;
    public Image analogStickBackgroundImage;
    public GameObject analogStick;
    public Canvas canvas;

    public float Horizontal { get => horizontal; set => horizontal = value; }
    public float Vertical { get => vertical; set => vertical = value; }


    // Start is called before the first frame update
    void Start()
    {
        CanvasRect = canvas.GetComponent<RectTransform>();
        // Get the rect transform
        this.rectTransform = analogStickBackgroundImage.rectTransform;
 
         // Calculate the screen offset
        this.uiOffset = new Vector2((float)CanvasRect.sizeDelta.x / 2f, (float)CanvasRect.sizeDelta.y / 2f);
        analogStickRectTransform = analogStick.GetComponent<RectTransform>();
        analogStickImage = analogStick.GetComponent<Image>();
        analogStickBackgroundRadius = analogStickBackgroundImage.sprite.rect.width / 2 - analogStickImage.sprite.rect.width / 2;
        analogStickControler.SetActive(false);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsPointerOverUIObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowAnalogStickAtPosition(Input.mousePosition);
            }
        }

        if (analogStickControler.activeSelf)
        {
            if (Input.GetMouseButton(0))
            {
                MoveStick(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                horizontal = 0;
                vertical = 0;
                HideAnalogStick();
            }
        }


        //else
        //{
        //    foreach (Touch touch in Input.touches)
        //    {
        //        if (touch.phase == TouchPhase.Began)
        //        {
        //            // Construct a ray from the current touch coordinates
        //            Ray ray = Camera.main.ScreenPointToRay(touch.position);
        //            if (Physics.Raycast(ray))
        //            {
        //                Debug.LogWarning("NYI :(");
        //            }
        //        }
        //    }
        //}
    }

    void ShowAnalogStickAtPosition(Vector3 position)
    {
        // Get the position on the canvas
        Vector2 viewportPosition = Camera.main.ScreenToViewportPoint(position);
        Vector2 proportionalPosition = new Vector2(viewportPosition.x * CanvasRect.sizeDelta.x, viewportPosition.y * CanvasRect.sizeDelta.y);

        // Set the position and remove the screen offset
        this.rectTransform.localPosition = proportionalPosition - uiOffset;
        analogStickControler.SetActive(true);
    }

    void MoveStick(Vector3 position)
    {
        // Get the position on the canvas
        Vector2 viewportPosition = Camera.main.ScreenToViewportPoint(position);
        Vector2 proportionalPosition = new Vector2(viewportPosition.x * CanvasRect.sizeDelta.x, viewportPosition.y * CanvasRect.sizeDelta.y);
        proportionalPosition = (proportionalPosition - uiOffset);
        Vector2 direction = proportionalPosition - (Vector2)rectTransform.localPosition;
        if (Vector2.Distance(rectTransform.localPosition, proportionalPosition) > analogStickBackgroundRadius)
        {
            proportionalPosition = (Vector2)rectTransform.localPosition + (direction.normalized * analogStickBackgroundRadius);
        }
        horizontal = Mathf.Clamp(direction.x / analogStickBackgroundRadius, -1, 1);
        vertical = Mathf.Clamp(direction.y / analogStickBackgroundRadius, -1, 1);
        analogStickRectTransform.localPosition = proportionalPosition;
    }



    void HideAnalogStick()
    {
        analogStickControler.SetActive(false);
    }
}
