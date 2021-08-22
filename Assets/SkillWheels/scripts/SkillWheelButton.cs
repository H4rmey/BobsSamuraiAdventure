using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum BUTTON_TYPE
{
    //Stats
    HEALTH = 0,
    SPEED = 1,
    AIRBORNE = 2,
    SWINGS = 3,
    DMG_DONE = 4,

    //Behaviours
    HEALING = 5,
    ACCEL = 6,
    HEIGHT = 7,
    DMG = 8,
    RANGE = 9,
    NIKS = 10
};

public class SkillWheelButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public      BUTTON_TYPE     type;
    public      string          itemName;
    public      Sprite          Icon;

    public UnityEvent leftClickEvent;
    public UnityEvent rightClickEvent;
    public UnityEvent mouseHoverEnter;
    public UnityEvent mouseHoverExit;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (leftClickEvent != null)
            {
                leftClickEvent.Invoke();
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (rightClickEvent != null)
            {
                rightClickEvent.Invoke();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mouseHoverEnter != null)
        {
            mouseHoverEnter.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseHoverExit != null)
        {
            mouseHoverExit.Invoke();
        }
    }
}

