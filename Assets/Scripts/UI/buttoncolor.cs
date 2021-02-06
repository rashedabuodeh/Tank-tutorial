using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buttoncolor : MonoBehaviour
{

    public Sprite ONSprite;
    public Sprite OFFSprite;

    public void changeimage()
    {
        GetComponent<Image>().sprite = ONSprite;
    }
    public void resimage()
    {
        GetComponent<Image>().sprite = OFFSprite;
    }
}
