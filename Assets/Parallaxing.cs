using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour
{

    public Transform[] backgrounds; 				// Array of all the back and foregrounds to be parallaxed
    private float[] parallaxScales;					// Proportion of camera's movement to move the backgrounds by
    public float Smoothing = 1;						// How smooth the parallax is going to be, make sure to set this above 0.

    private Transform cam;							// Reference to the main camera's transform
    private Vector3 previousCamPosition;			// The position of the camera in the previous frame

    // Called before Start, great for references
    void Awake()
    {
        // Set up the camera reference
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start()
    {
        // The previous frame had the current frame's camera position
        previousCamPosition = cam.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            // set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // create a target position which is the background's current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between current position and the target position using Lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos,
                Smoothing*Time.deltaTime);
        }

        previousCamPosition = cam.position;
    }
}
