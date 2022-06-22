using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{
    public GameObject recommendBtn;
    public RectTransform baseLayout;
    public CanvasGroup recommendationLayout;
    private bool recomEnabled;
    private float startyPosY;

    public void Start()
    {
        startyPosY = recommendBtn.transform.position.y;
    }
    public void EnableRecommendationBtn()
    {
        recommendBtn.transform.DOKill();
        recommendBtn.GetComponent<CanvasGroup>().DOKill();

        recommendBtn.transform.DOScale(1, .2f);
        recommendBtn.transform.DOMoveY(0, .2f);
        recommendBtn.GetComponent<CanvasGroup>().DOFade(1, .2f);
        recommendBtn.GetComponent<CanvasGroup>().interactable = true;
    }
    public void DisableRecommendationBtn()
    {
        recommendBtn.transform.DOKill();
        recommendBtn.GetComponent<CanvasGroup>().DOKill();

        recommendBtn.transform.DOScale(0, .2f);
        recommendBtn.transform.DOMoveY(startyPosY, .2f);
        recommendBtn.GetComponent<CanvasGroup>().DOFade(0, .2f);
        recommendBtn.GetComponent<CanvasGroup>().interactable = false;
    }
    public void RecommendationAppear()
    {
        baseLayout.DOKill();
        recommendationLayout.DOKill();

        baseLayout.DOAnchorMax(new Vector2(0, .5f), .5f);
        baseLayout.DOAnchorMin(new Vector2(0, .5f), .5f);
        recommendationLayout.DOFade(1,.5f).SetDelay(.5f);
    }
    public void RecommendationDisappear()
    {
        baseLayout.DOKill();
        recommendationLayout.DOKill();

        baseLayout.DOAnchorMax(new Vector2(.5f, .5f), .5f).SetDelay(.5f);
        baseLayout.DOAnchorMin(new Vector2(.5f, .5f), .5f).SetDelay(.5f);
        recommendationLayout.DOFade(0, .5f);
    }

    
    public void RecommendationResponse() //This function is connected to the "See Recommendation" button event in the unity inspector
    {
        if (!recomEnabled)
        {
            RecommendationAppear();
            recomEnabled = true;
        }
        else
        {
            RecommendationDisappear();
            recomEnabled = false;
        }
    }

    public void RecommendationResponseOnLost()
    {
        RecommendationDisappear();
       
        recomEnabled = false;
    }

}
