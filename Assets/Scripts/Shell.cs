using System;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public int moveSpeed = 200;
    public int lifeTimeSeconds = 2;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start ()
    {
        Destroy (gameObject, lifeTimeSeconds);
        
        
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
         
        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
         
        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
 
        //Ta Daaa
        transform.rotation =  Quaternion.Euler (new Vector3(0f,-angle, 0f));
    }
 
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    
    private void FixedUpdate()
    {
        rigidbody.velocity = transform.TransformDirection(transform.forward * (moveSpeed * Time.fixedDeltaTime));
    }
}
