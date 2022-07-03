using UnityEngine;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private new Rigidbody rigidbody;

        public float speed = 1;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            GlobalEventManager.PlayerDeath.AddListener(() => speed = 0);
        }

        private void FixedUpdate()
        {
            var horizontalDelta = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
            var verticalDelta = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
            var velocityY = rigidbody.velocity.y;
            rigidbody.velocity = transform.TransformDirection(new Vector3(verticalDelta, velocityY, -horizontalDelta));
        }
    }
}