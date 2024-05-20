using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType 
{
    suelo, pocion, pocionCora, text, rejilla, portal, none
}

public class Interactable : MonoBehaviour
{
    public ObjectType objectType;

}
