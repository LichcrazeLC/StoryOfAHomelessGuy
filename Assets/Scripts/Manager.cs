using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{   
    private int currentHealth = 100;
    private int currentGold = 0;
    public Text healthText;
    public Text goldText;
    public Text situationText;
    public Vector3 choiceInitVector = new Vector3(0,0,0);
    public float choiceInitCoordX = 0.0f;
    public float choiceInitCoordY = 0.0f;
    public GameObject choiceButton;

    public List<GameObject> spawnedButtons;

    private Node[] nodeList = new Node[]{
       new Node("DogAccident", "this is the dog node", new Choice[]{
           new Choice("Choice Text", "Result Text Sample", 0, 5),
           new Choice("Choice Text", "Result Text Sample", 1, 4)
       }),
       new Node("ColdAccident", "this is the cold weather node", new Choice[]{
           new Choice("Choice Text 2", "Result Text Sample 2", 0, 5)
       }),
    };

    private void Start(){
        StartCoroutine(nodeSpawner());
    }
    
    public void addGold(int amount){
        this.currentGold += amount;
        goldText.text = "Gold: " + this.currentGold;
    }

    public void addHealth(int amount){
        this.currentHealth += amount;
        healthText.text = "Health: " + this.currentHealth;
    }

    public void nodeInstantiation(Node node){
        situationText.text = node.getText();
        foreach (Choice choice in node.choices){
            choiceInitVector = new Vector3(choiceInitCoordX, choiceInitCoordY, 0);
            var newChoice = Instantiate (choiceButton, choiceInitVector, Quaternion.identity);
            newChoice.transform.SetParent(gameObject.transform,false);
            spawnedButtons.Add(newChoice);
            Color newColor = new Color(Random.value, Random.value, Random.value);
            newChoice.GetComponent<Image>().color = newColor;
            newChoice.GetComponentInChildren<Text>().text = choice.getChoiceText();
            newChoice.GetComponent<Button>().
                                     onClick.AddListener(() => OnChoose(choice.getGoldGain(), 
                                                                        choice.getHealthGain(),
                                                                        choice.getResultText()));
            choiceInitCoordY -= 30;
        }
    }

    public void OnChoose(int goldGain, int healthGain, string resultText){
        addGold(goldGain);
        addHealth(healthGain);
        situationText.text = resultText;
        choiceInitCoordY = 0;
        foreach (GameObject choice in spawnedButtons){
            choice.SetActive(false);
        }
        StartCoroutine(nodeSpawner());
    }

    public IEnumerator nodeSpawner(){
        yield return new WaitForSeconds(5);
        var newNode = nodeList[Random.Range(0,nodeList.Length)];
        nodeInstantiation(newNode);
    }

}