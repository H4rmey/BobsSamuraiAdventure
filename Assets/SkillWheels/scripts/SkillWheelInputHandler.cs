using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWheelInputHandler : MonoBehaviour
{
    public GameObject           skillWheelObj;
    public SkillWheelHandler    skillWheel;

    public GameObject           player;
    public Camera               cam;
    public RectTransform        trans;
    public Vector3              screenPos;
    private TimeStopper         timeStopper;

    public Player attr;

    public BaseSkill[] baseSkills = {
        new HealthSkill(),
        new SpeedSkill(),
        new AirtimeSkill(),
        new SwingsPsSkill(),
        new DamageDealtSkill()
    };

    // Start is called before the first frame update
    void Awake()
    {
        skillWheel      = skillWheelObj.GetComponent<SkillWheelHandler>();
        attr            = player.GetComponent<Player>();
        timeStopper     = GetComponent<TimeStopper>();

        skillWheelObj.SetActive(false);


        foreach (BaseSkill skill in baseSkills)
        {
            skill.player = attr;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*open skillwheel*/
        bool inv = Input.GetKeyDown(KeyCode.LeftShift);
        if (inv)
        {
            StartCoroutine(attr.ResetAttributes());

            skillWheel.actionWheel.type     = BUTTON_TYPE.NIKS;
            skillWheel.statWheel.type       = BUTTON_TYPE.NIKS;

            skillWheelObj.SetActive(true);
            skillWheel.Init();

            /*start time stop*/
            timeStopper.doStopTimePlease = true;
        }

        /*set position of the skillwheel*/
        screenPos = cam.WorldToScreenPoint(player.transform.position);
        float diffx, diffy;
        diffx = transform.position.x - screenPos.x;
        diffy = transform.position.y - screenPos.y;
        transform.Translate(-diffx, -diffy, 0);

        /*runs the skils*/
        baseSkills[(int)skillWheel.actionWheel.type].DoTheSkill(skillWheel.statWheel.type, skillWheel.actionWheel.type);
    }

    public void StopWorking()
    {
        this.skillWheelObj.SetActive(false);

        /*reset time stopping*/
        timeStopper.doStopTimePlease = false;
        StartCoroutine(attr.ResetAttributes());
    }
}
