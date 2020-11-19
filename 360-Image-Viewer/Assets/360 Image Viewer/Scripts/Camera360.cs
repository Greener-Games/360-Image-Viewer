#region

using UnityEngine;

#endregion

public class Camera360 : MonoBehaviour
{
    public float rotateSpeed = 100.0f;

    public bool invertHorozontal;
    public bool invertVertical;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // If left mouse button down:
            float rotateAboutX = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed; // Mouse movement up/down.
            float rotateAboutY = -Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed; // Mouse movement right/left.

            if (invertHorozontal)
            {
                rotateAboutY = -rotateAboutY;
            }

            if (invertVertical)
            {
                rotateAboutX = -rotateAboutX;
            }
            
            Vector3 newRotation = transform.rotation.eulerAngles;
            newRotation.x += rotateAboutX;
            newRotation.y += rotateAboutY;
            newRotation.z = 0;
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}