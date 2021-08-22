using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SkillWheelHandler : MonoBehaviour
{
    public GameObject       skillWheel;
    public GameObject       actionWheelObj;
    public GameObject       statWheelObj;

    public SkillWheelController     actionWheel;
    public SkillWheelController     statWheel;

    public void Init()
    {
        actionWheelObj.SetActive(true);
        statWheelObj.SetActive(false);
    }
}
