using UnityEngine;

public class Rotate : MonoBehaviour
{
    // This variable controls the rotation speed of the model.
    public float rotationSpeed = 10.0f;

    // Private variable to store horizontal mouse movement.
    private float mouseX;

    private void Update()
    {
        // Check if the left mouse button is pressed and held.
        if (Input.GetMouseButton(0))
        {
            // Get the horizontal mouse movement.
            mouseX = Input.GetAxis("Mouse X");

            // Rotate the model around the Y axis based on the mouse movement.
            transform.Rotate(Vector3.up, mouseX * rotationSpeed);
        }
    }
}

