using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBehaviour : MonoBehaviour
{
    RectTransform rectTransform;
    Vector3 startPosition;  
    Vector3 endPosition;  
    float timeOfTravel;  
    float currentTime = 0; 
    float normalizedValue;
    
    IEnumerator setColor(){
        yield return new WaitForSeconds(.5f);
        GetComponent<Image>().color = new Color(1,1,1,1);
    }
    void Start()
    {   
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(-300,0,0);
        StartCoroutine(LerpObject());
        StartCoroutine(setColor());
        
        int coinFlip = Random.Range(0,2);
        int randomYcoord = Random.Range(-128, -134);
        int randomWalkSPeed = Random.Range(10,25);
        if (coinFlip == 1){
            startPosition = new Vector3(-367, randomYcoord, 0);
            endPosition = new Vector3(367, randomYcoord, 0);
        } else {
            startPosition = new Vector3(367, randomYcoord, 0);
            endPosition = new Vector3(-367, randomYcoord, 0);
        }

        timeOfTravel = randomWalkSPeed;  

    }  
    IEnumerator LerpObject(){ 
                
        while (currentTime <= timeOfTravel) { 
            currentTime += Time.deltaTime; 
            normalizedValue = currentTime / timeOfTravel; 
        
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition,endPosition, normalizedValue); 
            yield return null; 
        }

    }
    void Update()
    {
        
    }
}
