using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
   /* [SerializeField] private float touchDamage, touchDamageCoolDownTime;

    [SerializeField] private float touchBoxWidth, touchBoxHeight;

    private float lastTouchDamageTime;

    private Vector2 touchBoxBotLeft, touchboxTopRight;

    [SerializeField] private Transform touchedPlayerCheck;

    private bool touchedPlayer;

    [SerializeField] private LayerMask isPlayer;

    private float[] damageInfo = new float[2];
    private void Awake()
    {
        touchBoxBotLeft = new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth/2), touchedPlayerCheck.position.y-(touchBoxHeight/2));
        touchboxTopRight = new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2));
        touchedPlayer = false;
    }

    private void Update()
    {
        touchBoxBotLeft = new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2));
        touchboxTopRight = new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2));
        CheckTouchDamage();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2)),
           new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2)));
        Gizmos.DrawLine(new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2)),
           new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2)));
        Gizmos.DrawLine(new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y - (touchBoxHeight / 2)),
           new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2)));
        Gizmos.DrawLine(new Vector2(touchedPlayerCheck.position.x - (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2)),
           new Vector2(touchedPlayerCheck.position.x + (touchBoxWidth / 2), touchedPlayerCheck.position.y + (touchBoxHeight / 2)));
    }
    private void CheckTouchDamage()
    {
        Collider2D hit = Physics2D.OverlapArea(touchBoxBotLeft, touchboxTopRight, isPlayer);
        if(hit!=null && Time.time > lastTouchDamageTime+touchDamageCoolDownTime)
        {
            lastTouchDamageTime = Time.time;
            Debug.Log("HIT!!!");
            damageInfo[0] = touchDamage;
            damageInfo[1] = transform.position.x;
            //hit.transform.parent.SendMessage("Damage", damageInfo);
            hit.SendMessage("Damage", damageInfo);
        }
    }*/
}
