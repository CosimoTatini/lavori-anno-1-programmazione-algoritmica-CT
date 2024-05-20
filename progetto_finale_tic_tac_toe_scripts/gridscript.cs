using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Gridscript : MonoBehaviour
{
    // Riferimento al bottone della cella
    public Button button;
    // Riferimento al testo del bottone della cella
    public Text buttontext;
    // Stringa che rappresenta il lato del giocatore (es. "X" o "O")
    public string playerSide;

    // Questo metodo imposta il testo del bottone e lo rende non interagibile
    public void SetSpace()
    {
        // Imposta il testo del bottone con il simbolo del giocatore
        buttontext.text = playerSide;
        // Rende il bottone non interagibile (disabilita il bottone)
        button.interactable = false;
    }
