using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

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
        public float rotationSpeed = 100;

        public ControllerState state = ControllerState.ExploreWorld;

        public Rigidbody shell;
        
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
                    //RotateToCursor();
                    break;
            }
        }

        private void RotateToCursor()
        {
            var lookRotation = Quaternion.LookRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        private void Update()
        {
            switch(state)
            {
                case ControllerState.InMenu:
                    break;
                case ControllerState.ExploreWorld:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Attack();
                    }
                    break;
            }
        }

        private void Attack()
        {
            var playerTransform = transform;
            Instantiate(shell, playerTransform.position, playerTransform.rotation);
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