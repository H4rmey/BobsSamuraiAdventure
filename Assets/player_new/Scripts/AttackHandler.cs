using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackHandler : MonoBehaviour
{
    public Player player;
    public GameObject hitobject;
    public CapsuleCollider2D hitcol;
    public float restoreFactor = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        float posx = 0.25f;
        Vector2 offset = new Vector2(posx * 0.5f, 0);
        Vector2 size = new Vector2(posx, posx * 0.5f);
        hitcol.offset = offset;
        hitcol.size = size;

        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 180f));
        hitobject.transform.rotation = rotation;
    } 

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "AttackableHitBox" && player.input.pSlashPressed)
        {
            BossAttributes ba = collision.GetComponentInParent<BossAttributes>();

            /*set different sprite*/
            //SpriteRenderer sprite = collision.GetComponentInParent<SpriteRenderer>();
            //sprite.color = new Color(0, 255, 255, 1.0f);
            ba.currentHealth -= player.attackForce;

            player.hitTarget = true;
            player.input.pSlashPressed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.hitTarget = false;
    }
}
