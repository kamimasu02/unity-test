using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("escape")) Application.Quit();
    }
}
