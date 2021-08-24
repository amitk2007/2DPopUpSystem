using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum PopUpSides
{
    inOut,
    side,
    buttom,
    top,
}

public class PopUpScript : MonoBehaviour
{
    /// <summary>
    /// Enter side for the animation (currently have only in/out animation)
    /// </summary>
    public PopUpSides popUpSide;

    Animator animator;
    Transform background;

    #region custom pop up
    /// <summary>
    /// Is the pop-up was pre made -> need only to use the Show/Hide functions of this component
    /// </summary>
    public bool isPreMade = false;

    public string titleText;
    public string contentText;
    public string buttonText;

    public bool isBGURL;
    public Sprite BGImage;
    public string BGURL;
    public Color BGColor = Color.white;

    public bool isButtonURL;
    public Sprite buttonImage;
    public string buttonURL;
    #endregion

    void Awake()
    {
        //Hide the pop-up, not using SetActive because we still need to change things in it's components
        this.transform.localScale = Vector3.zero;

        if (isPreMade == false)
        {
            SetUpPopUp();
        }
        animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Pre set the pop-up with all the variables givven in the inspector 
    /// </summary>
    void SetUpPopUp()
    {
        background = this.transform.Find("Background");
        background.Find("Particle System").GetComponent<ParticleSystem>().Stop();
        background.Find("Title").GetComponent<Text>().text = titleText;
        background.Find("Content Text").GetComponent<Text>().text = contentText;

        #region Set up buttons
        if (isBGURL)
            StartCoroutine(LoadImageFromURL(BGURL, background.GetComponent<Image>()));
        else
            background.GetComponent<Image>().sprite = BGImage;
        background.GetComponent<Image>().color = BGColor;

        if (isButtonURL)
            StartCoroutine(LoadImageFromURL(buttonURL, background.Find("Button").GetComponent<Image>()));
        else
            background.Find("Button").GetComponent<Image>().sprite = buttonImage;
        #endregion

        background.Find("Button").GetChild(0).GetComponent<Text>().text = buttonText;

    }

    #region load images
    /// <summary>
    /// Loads the image from the URL, change is to sprite using 'SpriteFromTexture2D' and applay it to the image component
    /// </summary>
    /// <param name="URL">The URL of the image</param>
    /// <param name="img">The image component to change the image of</param>
    /// <returns></returns>
    IEnumerator LoadImageFromURL(string URL, Image img)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL);

        www.timeout = 1;
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D webTexture = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
            Sprite webSprite = SpriteFromTexture2D(webTexture);
            img.sprite = webSprite;
        }
    }
    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    #endregion

    #region show & hide pop up
    /// <summary>
    /// Activate and show the pop-up to the user
    /// </summary>
    public void ShowPopUp()
    {
        this.gameObject.transform.localScale = Vector3.one;
        background.Find("Particle System").GetComponent<ParticleSystem>().Play();
        PlayPopUpAnimation(true);
    }

    /// <summary>
    /// Starting the ClosePopUp animation -> at the end of the animation there is a trigger attacked to 'ClosePopUpAnimainFinished'
    /// </summary>
    public void ClosePopUp()
    {
        PlayPopUpAnimation(false);
        background.Find("Particle System").GetComponent<ParticleSystem>().Stop();
    }
    /// <summary>
    /// Closeing the pop-up and using the PopUpManager, starting the next pop-up if in queue 
    /// </summary>
    public void ClosePopUpAnimainFinished()
    {
        this.transform.localScale = Vector3.zero;
        PopUpManager.PopUpClosed();
    }

    void PlayPopUpAnimation(bool isEntering)
    {
        switch (popUpSide)
        {
            case PopUpSides.inOut:
                if (isEntering) animator.Play("SimpleShowAnimation"); else animator.Play("SimpleHideAnimation");
                break;
            case PopUpSides.side:
                if (isEntering) animator.Play(""); else animator.Play("");
                break;
            case PopUpSides.buttom:
                if (isEntering) animator.Play("ShowDownAnimation"); else animator.Play("HideDownAnimation");
                break;
            case PopUpSides.top:
                if (isEntering) animator.Play(""); else animator.Play("");
                break;
            default:
                if (isEntering) animator.Play(""); else animator.Play("");
                break;
        }
    }
    #endregion

    private void OnValidate()
    {
        ///Validate only one time - compare Strings
        //if (BGURL != "" && BGURL.Contains("http") == false/*&& contatins(png/bmp/jpg/...)==false*/)
        //{
        //    EditorUtility.DisplayDialog("URL validation", "The url in not valid, please make sure you enterd a valid URL", "ok");
        //}
        //if (buttonURL != "" && buttonURL.Contains("http") == false/*&& contatins(png/bmp/jpg/...)==false*/)
        //{
        //    EditorUtility.DisplayDialog("URL validation", "The url in not valid, please make sure you enterd a valid URL", "ok");
        //}
    }
}
