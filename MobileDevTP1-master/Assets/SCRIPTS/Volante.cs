using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Volante : MonoBehaviour
{
    public Graphic UI_Element;
    RectTransform recT;
    Vector2 centerPoint;

    public float maxSteeringAngle = 200f;
    public float wheelReleaseSpeed = 200f;

    [Range(0,50)]
    public float distanceToInteract = 25f;

    public float wheelAngle = 0f;
    private float wheelPrevAngle = 0f;

    bool wheelBeingHeld = false;
    
    private void Start(){
        UI_Element = GameObject.FindWithTag("SteeringWheel").GetComponent<Image>();
        recT = UI_Element.rectTransform;
        InitEventSystem();
        UpdateRect();
    }

    private void Update(){
        if (!wheelBeingHeld&&!Mathf.Approximately(0f, wheelAngle)){
            float delta = wheelReleaseSpeed * Time.deltaTime;

            if(Mathf.Abs(delta)>Mathf.Abs(wheelAngle))
                wheelAngle = 0f;
                else if (wheelAngle > 0f)
                    wheelAngle -=delta;
                else
                    wheelAngle +=delta;

        }

        recT.localEulerAngles = Vector3.back * wheelAngle;

    }

    private void UpdateRect(){
        Vector3[] corners = new Vector3[4];
        recT.GetWorldCorners(corners);

        for (int i = 0; i <4; i++)
        {
            corners[i] = RectTransformUtility.WorldToScreenPoint(null, corners[i]);
        }

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];

        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        Rect r = new Rect(bottomLeft.x, topRight.y, width, height);
        centerPoint = new Vector2(r.x + r.width*0.5f, r.y - r.height * 0.5f);

    }

    private void InitEventSystem(){
        EventTrigger events = UI_Element.gameObject.GetComponent<EventTrigger>();

        if (events==null){
            events = UI_Element.gameObject.AddComponent<EventTrigger>();            
        }
        if (events.triggers==null) {
            events.triggers = new List<EventTrigger.Entry>();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();

        UnityAction<BaseEventData> functionalCall = new UnityAction<BaseEventData>(PressEvent);
        callback.AddListener(functionalCall);
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = callback;

        events.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        callback = new EventTrigger.TriggerEvent();
        functionalCall = new UnityAction<BaseEventData>(ReleaseEvent);
        callback.AddListener(functionalCall);
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = callback;

        events.triggers.Add(entry);
    }

    private void PressEvent (BaseEventData eventData){
        Vector2 PointerPos = ((PointerEventData)eventData).position;
        wheelBeingHeld = true;
        wheelPrevAngle = Vector2.Angle(Vector2.up, PointerPos - centerPoint);
    }

    private void ReleaseEvent(BaseEventData eventData){
        DragEvent(eventData);
        wheelBeingHeld = false;
    }

    private void DragEvent(BaseEventData eventData){
        Vector2 pointerPos = ((PointerEventData)eventData).position;
        float newAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);

        if(Vector2.Distance(pointerPos, centerPoint)>distanceToInteract){
            if (pointerPos.x > centerPoint.x)
                wheelAngle += newAngle - wheelPrevAngle;
            else
                wheelAngle -= newAngle - wheelPrevAngle;

        }

        wheelAngle = Mathf.Clamp(wheelAngle, -maxSteeringAngle, maxSteeringAngle);
        wheelPrevAngle = newAngle;

    }

}
