using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIElementXR : MonoBehaviour
{
    public UnityEvent OnXRPointerEnter;
    public UnityEvent OnXRPointerExit;
    private Camera xRCamera;

    void Start()
    {
        xRCamera = CameraPointerManager.instance.gameObject.GetComponent<Camera>(); //Llamas aquí a la clase CameraPointerManager como instance en UIElementXR;
    }

    void Update()
    {
    }

    public void OnPointerClickXR()
    {
        PointerEventData pointerEvent = PlacePointer(); //Este es el elemento que nos permitirá hacer clic sobre el elemento UI pero para ello se necesita una posición la cual está en la función PlacePointer
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerClickHandler); //Ejecutamos el Evento al hacer clic;
    }

    public void OnPointerEnterXR()
    {
        GazeManager.Instance.SetUpGaze(2.5f); //Reducimos el tiempo de carga del evento;
        OnXRPointerEnter?.Invoke(); //Llamamos al EventorEnterXR si no hay problemas.

        PointerEventData pointerEvent = PlacePointer(); //Este es el elemento que nos permitirá hacer clic sobre el elemento UI pero para ello se necesita una posición la cual está en la función PlacePointer
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerEnterHandler); //Ejecutamos el Evento al hacer clic;
    }

    public void OnPointerExitXR()
    {
        GazeManager.Instance.SetUpGaze(2.5f); //Reducimos el tiempo de carga del evento;
        OnXRPointerExit?.Invoke(); //Llamamos al EventorExitXR si no hay problemas.

        PointerEventData pointerEvent = PlacePointer(); //Este es el elemento que nos permitirá hacer clic sobre el elemento UI pero para ello se necesita una posición la cual está en la función PlacePointer
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerExitHandler); //Ejecutamos el Evento al hacer clic;
    }

    public PointerEventData PlacePointer()
    {
        Vector3 screenPos = xRCamera.WorldToScreenPoint(CameraPointerManager.instance.hitPoint);
        PointerEventData pointerEvent = new PointerEventData(EventSystem.current);
        pointerEvent.position = new Vector2(screenPos.x, screenPos.y);
        return pointerEvent;
    }
}
