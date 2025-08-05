using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : FlyingEnemy
{
    public override void Awake()
    {
        base.Awake();
    }

    public override bool CheckPlayerInCloseRangeAction()
    {
        return base.CheckPlayerInCloseRangeAction();
    }

    public override bool CheckPlayerInLongRangeAction()
    {
        return base.CheckPlayerInLongRangeAction();
    }

    public override bool CheckPlayerInMaxRange()
    {
        return base.CheckPlayerInMaxRange();
    }

    public override bool CheckPlayerInMinRange()
    {
        return base.CheckPlayerInMinRange();
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void Update()
    {
        base.Update();
    }
}
