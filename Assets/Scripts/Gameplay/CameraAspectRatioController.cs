using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatioController : MonoBehaviour
{
    private Camera cam;
    private float targetaspect = 16f / 9f;

    private Assets.Scripts.Data.Resolution currentResolution;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        currentResolution = OptionsManager.GetResolution();
        AdjustCamera();
    }

    private void Update()
    {
        Assets.Scripts.Data.Resolution newResolution = OptionsManager.GetResolution();
        if (newResolution.Id != currentResolution.Id)
        {
            currentResolution = newResolution;
            AdjustCamera();
        }
    }

    private void AdjustCamera()
    {
        // determine the game window's current aspect ratio
        float windowaspect = (float)currentResolution.Width / (float)currentResolution.Height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            cam.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = cam.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}
