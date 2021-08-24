using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    /// <summary>
    /// The pop-ups waiting to be shown
    /// </summary>
    public static Stack<PopUpScript> popUpsQueue = new Stack<PopUpScript>();

    public static bool isPopUpActive = false;

    public static void PopUpClosed()
    {
        isPopUpActive = false;
        ShowNextPopUp();
    }

    public static void ShowNextPopUp()
    {
        if (popUpsQueue.Count > 0 && isPopUpActive == false)
        {
            GameObject.FindGameObjectWithTag("Back").GetComponent<UnityEngine.UI.Image>().enabled = true;
            popUpsQueue.Pop().ShowPopUp();
            isPopUpActive = true;
        }
        else
            GameObject.FindGameObjectWithTag("Back").GetComponent<UnityEngine.UI.Image>().enabled = false;
    }

    public static void AddPopUpToQueue(PopUpScript popUp)
    {
        popUpsQueue.Push(popUp);
        ShowNextPopUp();
    }
}
