using UnityEngine;
using System.Collections;

public class EnemyBoss : Enemy
{
    [Header("弹幕设置")]
    [SerializeField] int barrageBulletCount = 12;
    [SerializeField] float barrageInterval = 3f;

    // protected override void Start()
    // {
    //     base.Start();
    //     StartCoroutine(BarrageRoutine());
    // }

    IEnumerator BarrageRoutine()
    {
        while(true)
        {
            yield return StartCoroutine(CircleBarrage());
            yield return new WaitForSeconds(barrageInterval);
        }
    }

    IEnumerator CircleBarrage()
    {
        float angleStep = 360f / barrageBulletCount;
        for(int i=0; i<barrageBulletCount; i++)
        {
            float angle = i * angleStep;
            Vector2 dir = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );
            BulletPool.Instance.GetBullet(false, transform.position, dir);
            yield return new WaitForSeconds(0.1f);
        }
    }
}