using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{   
    public string nodeName;
    private string entryText;
    public Choice[] choices;

    public string getText(){
        return this.entryText;
    }

    public Node(string nodeName, string entryText, Choice[] choices){
        this.nodeName = nodeName;
        this.entryText = entryText;
        this.choices = choices;
    }

}
