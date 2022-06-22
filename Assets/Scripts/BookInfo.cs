using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class BookInfo : MonoBehaviour
{
    public ReadCSV dataReceiver;
    private ImageTargetBehaviour imageTarget;
    private string bookName;
    private string genre;
    private double rating;
    private string description;
    private string recommendation1;
    private string recommendation2;
    private string recommendation3;
    private string recommImageUrlId1;
    private string recommImageUrlId2;
    private string recommImageUrlId3;
    private string imagerDownloadingLink;

    public GameObject CanvasAr; 
    public TextMeshProUGUI bookNameText;
    public TextMeshProUGUI genreText;
    public TextMeshProUGUI ratingLabelText;
    public UnityEngine.UI.Image[] ratingImages;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI recommendation1Text;
    public TextMeshProUGUI recommendation2Text;
    public TextMeshProUGUI recommendation3Text;
    public TextMeshProUGUI[] recommendationTextFields = new TextMeshProUGUI[3];
    public UnityEngine.UI.Image[] containers = new UnityEngine.UI.Image[3];
    public GameObject canvasRegular;

    public Vector3 setCanvasPosition;
    public float setCanvasScale;


    // Start is called before the first frame update
    void Start()
    {
        imagerDownloadingLink = "https://drive.google.com/uc?export=download&id=";
        imageTarget = this.GetComponent<ImageTargetBehaviour>();
        
    }

    // Update is called once per frame
    public void GetInfoAfterTrack()
    {

    }
    public void SetScale()
    {

    }
    public void SetCanvasPostion()
    {
        CanvasAr.transform.SetParent(this.transform);
        CanvasAr.transform.localRotation = Quaternion.identity;
        CanvasAr.transform.localPosition = setCanvasPosition;
    }
    public void SetCanvasInfo( string bookNameTmp)
    {
        dataReceiver.GetBookInfos();
        for (int i = 0; i < dataReceiver.GetBookInfos().Count; i++)
        {
            if (dataReceiver.GetBookInfos()[i]["bookName"].ToString() == bookNameTmp)
            {
                Debug.Log("This is the book" + bookNameTmp);
                bookName = dataReceiver.GetBookInfos()[i]["bookName"].ToString();
                genre = dataReceiver.GetBookInfos()[i]["genre"].ToString();
                rating = (double)dataReceiver.GetBookInfos()[i]["rating"];
                ratingLabelText.text = "Rating : ( " + rating+ " )";
                SetRatingImages(rating);
                description = dataReceiver.GetBookInfos()[i]["description"].ToString();
                recommendation1 = dataReceiver.GetBookInfos()[i]["recommendation1"].ToString();
                recommendation2 = dataReceiver.GetBookInfos()[i]["recommendation2"].ToString();
                recommendation3 = dataReceiver.GetBookInfos()[i]["recommendation3"].ToString();


                bookNameText.text = bookName.Replace("_"," ");
                genreText.text = genre;
                descriptionText.text = description;
                recommendation1Text.text = recommendation1;
                recommendation2Text.text = recommendation2;
                recommendation3Text.text = recommendation3;


                StartCoroutine(DownloadRecommImage(containers[0], imagerDownloadingLink, dataReceiver.GetBookInfos()[i]["recommImageUrlId1"].ToString()));
                StartCoroutine(DownloadRecommImage(containers[1], imagerDownloadingLink, dataReceiver.GetBookInfos()[i]["recommImageUrlId2"].ToString()));
                StartCoroutine(DownloadRecommImage(containers[2], imagerDownloadingLink, dataReceiver.GetBookInfos()[i]["recommImageUrlId3"].ToString()));
                Debug.Log(dataReceiver.GetBookInfos()[i]["recommImageUrlId1"].ToString());
            }
        }
    }
    public void SetRatingImages(double rating)
    {
        
        for (int i = 0; i < ratingImages.Length; i++)
        {
            ratingImages[i].fillAmount = 0;
        }
        for (int i = 0; i < rating; i++)
        {
            ratingImages[i].fillAmount = 1;
        }
        if (rating>=0 && rating<1 )
        {
            float ratingFloat = (float)rating;
          
            ratingImages[0].fillAmount = ratingFloat - Mathf.Floor(ratingFloat);
        }
        else if (rating >= 1 && rating < 2)
        {
            float ratingFloat = (float)rating;
          
            ratingImages[1].fillAmount = ratingFloat - Mathf.Floor(ratingFloat);
        }
        else if (rating >= 2 && rating < 3)
        {
            float ratingFloat = (float)rating;
           
            ratingImages[2].fillAmount = ratingFloat - Mathf.Floor(ratingFloat);
        }
        else if (rating >= 3 && rating < 4)
        {
            float ratingFloat = (float)rating;
           
            ratingImages[3].fillAmount = ratingFloat - Mathf.Floor(ratingFloat);
        }
        else if (rating >= 4 && rating <= 5)
        {
            float ratingFloat = (float)rating;
            
            ratingImages[4].fillAmount = ratingFloat - Mathf.Floor(ratingFloat);
        }

    }
    public void EnableRegularCanvas(bool value)
    {
        if (value)
        {
            canvasRegular.GetComponent<CanvasManager>().EnableRecommendationBtn();
        }
        else
        {
            canvasRegular.GetComponent<CanvasManager>().DisableRecommendationBtn();
        }
      
    }
    public void ResponseOnlost()
    {
        canvasRegular.GetComponent<CanvasManager>().RecommendationResponseOnLost();
    }

    private IEnumerator DownloadRecommImage(UnityEngine.UI.Image container,  string urlWithoutID, string imageUrlID)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(urlWithoutID + imageUrlID);
        Debug.Log(urlWithoutID + imageUrlID);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {

        }
        else {
            container.overrideSprite = Sprite.Create(((DownloadHandlerTexture)request.downloadHandler).texture, new Rect(0.0f, 0.0f, ((DownloadHandlerTexture)request.downloadHandler).texture.width, ((DownloadHandlerTexture)request.downloadHandler).texture.height), new Vector2(0.5f, 0.5f), 100.0f); 
        }
    }
}
