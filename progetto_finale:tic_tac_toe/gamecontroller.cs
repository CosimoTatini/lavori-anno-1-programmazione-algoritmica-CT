using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Gamecontroller : MonoBehaviour
{
    // Dichiarazione delle variabili pubbliche per i pulsanti, il testo del risultato e il suono della vittoria
    public Button[] buttons;
    public Text resultText;
    public AudioSource winSound;
    public AudioClip winClip;

    // Array per memorizzare i valori dei pulsanti
    private string[] buttonValues;

    // Variabile per tener traccia del turno del giocatore e dello stato del gioco
    private bool isPlayerX = true;
    private bool isGameOver = false;

    void Start()
    {
        // Inizializzazione dell'array dei valori dei pulsanti
        buttonValues = new string[buttons.Length];

        // Iterazione attraverso i pulsanti per aggiungere un listener per l'evento onClick
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Necessario per catturare il valore corretto di i nell'espressione lambda
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
            buttonValues[i] = ""; // Inizializzazione dei valori dei pulsanti
            winSound.Stop(); // Assicurati che il suono della vittoria sia stoppato all'avvio del gioco
        }
    }

    void OnButtonClick(int index)
    {
        // Se il gioco è finito o il pulsante è già stato cliccato, esci dalla funzione
        if (isGameOver || buttonValues[index] != "")
            return;

        // Assegna il valore corretto al pulsante cliccato (X o O)
        buttonValues[index] = isPlayerX ? "X" : "O";
        buttons[index].GetComponentInChildren<Text>().text = buttonValues[index];

        // Controlla se c'è un vincitore dopo il click
        if (CheckForWinner())
        {
            // Mostra il risultato e ferma il gioco
            resultText.text = "Player " + (isPlayerX ? "X" : "O") + " wins!";
            isGameOver = true;

            // Riproduci il suono della vittoria, se presente
            if (winSound != null)
            {
                winSound.clip = winClip;
                winSound.Play();
            }
        }
        // Se non c'è un vincitore e la mappa è piena, dichiara un pareggio
        else if (IsBoardFull())
        {
            resultText.text = "It's a draw!";
            isGameOver = true;
        }
        // Altrimenti, cambia il turno del giocatore
        else
        {
            isPlayerX = !isPlayerX;
            resultText.text = "Player " + (isPlayerX ? "X" : "O") + "'s turn";
        }
    }

    // Controlla se c'è un vincitore
    bool CheckForWinner()
    {
        // Controlla le righe
        for (int i = 0; i < 3; i++)
        {
            if (buttonValues[i * 3] == buttonValues[i * 3 + 1] && buttonValues[i * 3 + 1] == buttonValues[i * 3 + 2] && buttonValues[i * 3] != "")
                return true;
        }

        // Controlla le colonne
        for (int i = 0; i < 3; i++)
        {
            if (buttonValues[i] == buttonValues[i + 3] && buttonValues[i + 3] == buttonValues[i + 6] && buttonValues[i] != "")
                return true;
        }

        // Controlla le diagonali
        if (buttonValues[0] == buttonValues[4] && buttonValues[4] == buttonValues[8] && buttonValues[0] != "")
            return true;
        if (buttonValues[2] == buttonValues[4] && buttonValues[4] == buttonValues[6] && buttonValues[2] != "")
            return true;

        // Controlla la cella centrale
        if (buttonValues[4] != "" && ((buttonValues[0] == buttonValues[4] && buttonValues[4] == buttonValues[8]) || (buttonValues[2] == buttonValues[4] && buttonValues[4] == buttonValues[6])))
            return true;

        return false;
    }

    // Controlla se la mappa è piena
    bool IsBoardFull()
    {
        foreach (string value in buttonValues)
        {
            if (value == "")
                return false;
        }
        return true;
    }
}
