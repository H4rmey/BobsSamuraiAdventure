using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillWheelController : MonoBehaviour
{
    public      GameObject              self;
    public      GameObject              next;
    public      BUTTON_TYPE             type;
    public      string                  typename;

    public      SkillWheelButton[]      buttons     = new SkillWheelButton[5];
    public      SkillWheelInputHandler  skillWheelInputHandler;
    public      Image                   indexImage;

    // Start is called before the first frame update
    void Start()
    {
        this.CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        //left click
        if (Input.GetMouseButtonDown(0))
        {
            self.gameObject.SetActive(false);
            next.gameObject.SetActive(true);

            if (this.gameObject.name == "StatWheel")
            {
                skillWheelInputHandler.StopWorking();
            }
        }
    }

    void CreateSlots()
    {
        buttons = GetComponentsInChildren<SkillWheelButton>();
        foreach (SkillWheelButton button in buttons)
        {
            button.leftClickEvent.AddListener   (delegate   { this.OnInventoryLeftClick     (button); });
            button.rightClickEvent.AddListener  (delegate   { this.OnInventoryRightClick    (button); });
            button.mouseHoverEnter.AddListener  (delegate   { this.OnSlotEnter              (button); });
            button.mouseHoverExit.AddListener   (delegate   { this.OnSlotExit               (button); });
        }
    }

    public void OnInventoryLeftClick(SkillWheelButton button)
    {
    }

    public void OnInventoryRightClick(SkillWheelButton button) 
    { 
    }

    public void OnSlotEnter(SkillWheelButton button)
    {
        this.typename = button.itemName;
        indexImage.sprite = button.Icon;
        this.type = button.type;
    }

    public void OnSlotExit(SkillWheelButton button)
    {
    }
}
