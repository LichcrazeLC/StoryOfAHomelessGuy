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
    public GameObject goldAddition;
    public GameObject healthAddition;
    public Text goldAdditionText;
    public Text healthAdditionText;
    public AudioSource cashAudio;
    public AudioSource hurtAudio;
    public AudioSource pleasedAudio;
    public Animator situationAnimator;
    public Sprite[] characterImages = new Sprite[8];
    public GameObject character;

    private Node[] nodeList = new Node[]{
       new Node("DogAccident", "A dirty curly dog happily jumps in your arms and licks your cheek.", new Choice[]{
           new Choice("Feed him", "The dog stays with you and it seems he attracts people since they give you more money. +15 gold", 15, 0),
           new Choice("Push him away", "The dog bites you and leaves you with a deep wound. People are more generous out of pity. +10 gold. -30 health", 10, -30)
       }),
       new Node("ColdAccident", "Brrrr! It's getting cold outside... The weather is not kind to you.", new Choice[]{
           new Choice("Have some vodka", "You get a shot of the cheapest Vodka in town. Warms you up but you get alcohol poisoning. -1 gold. -20 health", -1, -20),
           new Choice("Go to the library", "The library security kicks you out. Why is everybody so mean...? -25 health", 0, -25)
       }),
       new Node("DiscussionAccident", "You hear a dubious discussion from nearby people.", new Choice[]{
           new Choice("Ignore them", "You keep minding your own business...", 0, 0),
           new Choice("Spy them", "You overhear about a secret stash with money. You go and pick it up. +20 gold", 20, 0)
       }),
        new Node("CrimeAccident", "You witness a man stealing a purse at night from a lady in the nearby alley.", new Choice[]{
           new Choice("Go to the police", "The police pays you for contributing. Nice job! +20 gold.", 20, 0),
           new Choice("Do nothing", "The guilt eats you up from the inside. The poor woman might have needed a witness. Why didn't you go?  -25 health", 0, -25)
       }),
        new Node("FoodAccident", "You're getting pretty hungry but you have no food. There is a trash can on the other side of the alley though.", new Choice[]{
           new Choice("Search the trash can", "You found a rotten fish. Ewww - 35 health.", 0, -35),
           new Choice("Buy from a store", "You buy a healthy looking Salad  +15 health. -15 gold", -15, 15)
       }),
       new Node("JobAccident", "You have to try to get a Job. There's three recruitment posters on a nearby pole.", new Choice[]{
           new Choice("Shoe Salesman", "You get your first salary in advance + 20 gold", 20, 0),
           new Choice("Fisherman", "While fishing, you fell into the lake. It was a polluted lake. You got your salary though. +15 gold -30 health", 15, -30),
           new Choice("Taxi Driver", "You crashed the company's car. You also got hurt in the accident. Gotta pay up! -30 gold -50 health", -50, -30)
       }),
        new Node("FoundAccident", "While wondering around the town, you find a bag with money and documents. What do you do?", new Choice[]{
           new Choice("Report to police", "You are remunerated! + 15 gold.", 15, 0),
           new Choice("Take it", " There's plenty of cash in that bag. You buy fresh food and clothes. +15 health. +15 gold", 15, 15)
       }),  
       new Node("LoveAccident", "You see a beautiful girl struggling to find her dropped glasses.", new Choice[]{
           new Choice("Give her the glasses", "The girl deeply thanks you and that's it.", 0, 0),
           new Choice("Steal the glasses", "Turns out the girl was rich and she had bodyguards. They saw you steal the glasses and beat you up. -30 health", 0, -30)
       }),  
       new Node("CabbageAccident", "You see an amazing fresh cabbage at the food stand across the street", new Choice[]{
           new Choice("Steal it and eat it", "You are quite the thief! The saleswoman didn't even notice you. +10 health", 0, 10),
           new Choice("Pay for it", "Wow, that cabbage was super expensive. Tasty, though. +10 health, -10 gold", -10, +10)
       }),  
        new Node("OneChoiceAccident", "You see an old lady trying to cross the street. What do you do?", new Choice[]{
           new Choice("The right choice", "You helped the granny, she smiles and rewards you. +15 gold", 15, 0),
       }), 
        new Node("SewerAccident", "You have an opportunity to clean up the city sewers for some money.", new Choice[]{
           new Choice("Do it", "You catch a very bad disease. It's not curable. You get the job reward though. -100 health. +3 gold", +3, -100),
           new Choice("No way in hell", "Ok, but you really need some money though...", 0, 0)
       }),    
       
    };

    private void Start(){
        healthText.gameObject.SetActive(false);
        goldText.gameObject.SetActive(false);
        StartCoroutine(spawnCharacter());
        StartCoroutine(introduction());
    }

    public IEnumerator spawnCharacter(){
        yield return new WaitForSeconds(5);
        InstantiateCharacter();
        StartCoroutine(spawnCharacter());
    }

    public void InstantiateCharacter(){
        var newChar = Instantiate(character, transform.position, Quaternion.identity);
        newChar.transform.SetParent(gameObject.transform,false);
        newChar.GetComponent<Image>().sprite = characterImages[Random.Range(0, characterImages.Length)];
    }

    private IEnumerator introduction(){
        situationText.text = @"Tom is a young boy, he lives in a small town with his poor family,
        trying every day to survive. . . . . ";

        yield return new WaitForSeconds(10);

        situationText.text = @"When his father died and his brother had a car accident,
        there seemed to be no way to save his family by staying at home. . . . ";

        yield return new WaitForSeconds(10);

        situationText.text = @"Tom decided to leave the house to be sure that at least he can save his family. 
        Being only 15 years old, he left home, with nothing but some clothes he was wearing. . . . .";

        yield return new WaitForSeconds(10);

        situationText.text = @"Tom came to one of the biggest cities in the country hoping that 
        there he cand find a way to make more money. . . . .";

        yield return new WaitForSeconds(10);
        situationText.text = @" It wasn’t the dream he thought it was, he felt only pain because his family was apart and there were only strangers around … 
        Every day was a FIGHT for survival. . . . . ";

        yield return new WaitForSeconds(10);

        situationText.text = @"On cold winter days there was no work, it means no money, no food … 
        Many nights Tom fell asleep hungry and frozen. What would you do in his place?. . . . .";

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

        if (healthGain != 0){
            healthAddition.SetActive(true);
            if (healthGain > 0){
                healthAdditionText.text = "+" + healthGain;
                pleasedAudio.Play();
            }
            else if(healthGain < 0){
                healthAdditionText.text = "" + healthGain;
                hurtAudio.Play();
            }
        }
        if (goldGain != 0){
            goldAddition.SetActive(true);            
            if (goldGain > 0){
                goldAdditionText.text = "+" + goldGain;
                cashAudio.Play();
            }
            else if(goldGain < 0)
                goldAdditionText.text = "" + goldGain;
        }

        situationText.text = resultText;
        choiceInitCoordY = 0;
        foreach (GameObject choice in spawnedButtons){
            choice.SetActive(false);
        }
        StartCoroutine(nodeSpawner());
    }

    public IEnumerator nodeSpawner(){
        yield return new WaitForSeconds(7);
        if(currentHealth >= 1 && currentGold <= 99){
            situationAnimator.SetTrigger("ActionStart");
            healthText.gameObject.SetActive(true);
            goldText.gameObject.SetActive(true);
            var newNode = nodeList[Random.Range(0,nodeList.Length)];
                goldAddition.SetActive(false);
                healthAddition.SetActive(false);
            nodeInstantiation(newNode);
        } 
        if (currentHealth <= 0){
            healthText.gameObject.SetActive(false);
            goldText.gameObject.SetActive(false);
            situationText.text = "Life on the streets is rough. Tom died.........";
        }
        if (currentGold >= 100){
            healthText.gameObject.SetActive(false);
            goldText.gameObject.SetActive(false);
            situationText.text = @"Hooooray! Tom managed to gain 100 gold. He can now return to his family and 
            try to sustain them at least for some time. Good for you, Tom!
            ";            
        }
    }

}