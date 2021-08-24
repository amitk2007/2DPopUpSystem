using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void PlayPopUps(PopUpScript popUp)
    {
        //foreach (PopUpScript script in popUps)
        //{
        PopUpManager.AddPopUpToQueue(popUp);
        //}
    }
}
