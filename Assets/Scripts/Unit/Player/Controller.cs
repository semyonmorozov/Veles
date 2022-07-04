using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Unit.Player
{
    public class Controller : MonoBehaviour
    {
        public enum ControllerState
        {
            InMenu,
            ExploreWorld
        }

        public float speed = 1;

        public ControllerState state = ControllerState.ExploreWorld;

        private new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            
            //TODO отписаться от события смерти, когда появится меню, должно быть реализовано иначе
            GlobalEventManager.PlayerDeath.AddListener(() => state = ControllerState.InMenu); 
        }

        private void FixedUpdate()
        {
            switch(state)
            {
                case ControllerState.InMenu:
                    break;
                case ControllerState.ExploreWorld:
                    MovePlayer();
                    break;
                default:
                    break;
            }
        }

        private void MovePlayer()
        {
            var horizontalDelta = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
            var verticalDelta = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
            var velocityY = rigidbody.velocity.y;
            rigidbody.velocity = transform.TransformDirection(new Vector3(verticalDelta, velocityY, -horizontalDelta));
        }
    }
}