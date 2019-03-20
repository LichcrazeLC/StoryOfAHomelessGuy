using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice
{   
    Manager manager;
    private int goldGain;
    private int healthGain;
    private string choiceText;
    private string resultText;

    public Choice(string choiceText, string resultText, int goldGain, int healthGain){
        this.choiceText = choiceText;
        this.goldGain = goldGain;
        this.healthGain = healthGain;
        this.resultText = resultText;
    }

    public string getChoiceText(){
        return this.choiceText;
    }

    public string getResultText(){
        return this.resultText;
    }

    public int getGoldGain(){
        return this.goldGain;
    }

    public int getHealthGain(){
        return this.healthGain;
    }

}