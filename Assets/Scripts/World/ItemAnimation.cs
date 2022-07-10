using System;
using System.Collections;
using UnityEngine;

namespace World
{
    public class ItemAnimation : MonoBehaviour
    {
        private new Animation animation;
        private const string AnimationName = "StandardItemAnimation";

        private void Awake()
        {
            animation = GetComponent<Animation>();
        }

        private IEnumerator Start()
        {
            while (true)
            {
                animation.PlayQueued(AnimationName);
                yield return new WaitForSeconds(animation.GetClip(AnimationName).length-1);
            }
        }
    }
}
