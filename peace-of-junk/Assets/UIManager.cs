using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image Screw1;
    public Image Screw2;
    public Image Screw3;

    private int _screwsPickedUp = 0;
    void Start()
    {
        Screw1.enabled = false;
        Screw2.enabled = false;
        Screw3.enabled = false;
    }

    public void pickupScrew()
    {
        _screwsPickedUp++;

        switch (_screwsPickedUp)
        {
            case 1:
                Screw1.enabled = true;
                break;
            case 2:
                Screw3.enabled = true;
                break;
            case 3:
                Screw3.enabled = true;
                break;
        }
    }

}
