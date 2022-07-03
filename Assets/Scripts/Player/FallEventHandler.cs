using System;
using UnityEngine;

namespace Player
{
    public class FallEventHandler : MonoBehaviour
    {
        private Material material;

        private void Awake()
        {
            material = GetComponent<MeshRenderer>().material;
            GlobalEventManager.PlayerFell.AddListener(Handle);
        }

        private void Handle()
        {
            material.color = Color.black;
        }
    }
}