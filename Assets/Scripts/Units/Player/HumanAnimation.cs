using System;
using System.Collections;
using UnityEngine;

namespace External.Blink
{
    public enum MovingState
    {
        Idle = 0,
        RunForward = 1,
        RunRight = 2,
        RunLeft = 3,
        RunBackward = 4,
        RunBackwardLeft = 5,
        RunBackwardRight = 6,
        StrafeRight = 7,
        StrafeLeft = 8
    }

    public enum CastingState
    {
        NonCasting,
        Casting,
        SuccessCast
    }

    public class HumanAnimation : MonoBehaviour
    {
        private float Cooldown => 1;
        private Animator animator;
        private static readonly int MovingTriggerName = Animator.StringToHash("MovingState");
        private static readonly int CastingTriggerName = Animator.StringToHash("CastingState");
        private Coroutine prepareSpell;
        private bool isReloaded = true;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private IEnumerator PrepareSpell()
        {
            yield return new WaitForSeconds(3);
            animator.SetInteger(CastingTriggerName, (int)CastingState.SuccessCast);
            prepareSpell = null;
            StartCoroutine(StartCooldown());
        }
        private IEnumerator StartCooldown()
        {
            isReloaded = false;
            yield return new WaitForSeconds(Cooldown);
            isReloaded = true;
        }

        private void FixedUpdate()
        {
            Attack();
            var horizontalDelta = Input.GetAxis("Horizontal");
            var verticalDelta = Input.GetAxis("Vertical");
            var newMovingState = horizontalDelta switch
            {
                > 0 => verticalDelta switch
                {
                    > 0 => MovingState.RunLeft,
                    < 0 => MovingState.RunBackwardLeft,
                    _ => MovingState.StrafeLeft
                },
                < 0 => verticalDelta switch
                {
                    > 0 => MovingState.RunRight,
                    < 0 => MovingState.RunBackwardRight,
                    _ => MovingState.StrafeRight
                },
                _ => verticalDelta switch
                {
                    > 0 => MovingState.RunForward,
                    < 0 => MovingState.RunBackward,
                    _ => MovingState.Idle
                }
            };

            animator.SetInteger(MovingTriggerName, (int)newMovingState);
        }

        private void Attack()
        {
            if (!isReloaded)
            {
                return;
            }
            if (Input.GetMouseButton(0))
            {
                animator.SetInteger(CastingTriggerName, (int)CastingState.Casting);
                prepareSpell ??= StartCoroutine(PrepareSpell());
            }
            else
            {
                animator.SetInteger(CastingTriggerName, (int)CastingState.NonCasting);
                if (prepareSpell != null)
                {
                    StopCoroutine(prepareSpell);
                    prepareSpell = null;
                }
            }
        }
    }
}