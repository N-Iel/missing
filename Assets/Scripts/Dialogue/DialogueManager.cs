using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    private Animator layoutAnimator;
    
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameTest;

    [SerializeField] private Animator portraitAnimator;

    [Header("Ink Variable")]
    private Story currentStory;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    //booleano de solo lectura
    public bool dialogueIsPlaying { get; private set; }

    [Header("Tags Constants")]
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    private void Awake()
    {
        if (instance != null) 
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }


        instance = this;
    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying) 
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    #region Dialogue Methods
    public void EnterDialogueMode(TextAsset inkJSON) 
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        //resetear a los valores por defecto antes de empezar un nuevo diálogo
        displayNameTest.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");


        ContinueStory();

    }

    private void ContinueStory() 
    {
        if (currentStory.canContinue)
        {
            //muestra el texto de la actual línea de diálogo
            dialogueText.text = currentStory.Continue();
            //muestra las opciones (si las hay)
            DisplayChoices();

            //gestiona las tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
           StartCoroutine (ExitDialogueMode());
        }
    }

    //este método es una corrutina para evitar acciones simultaneas asignadas al mísmo botón
    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
    #endregion



    #region Dialogue settings Methods

    private void HandleTags(List<string> currentTags) 
    {
        
        foreach (string tag in currentTags) 
        {
            //analiza las tags y devuelve 2 arrays
            string[] splitTag = tag.Split(':');
            //preparamos un aviso de error por si no se puede analizar correctamente
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }

            //array 1 (trim limpia los espacios en blanco tanto al inicio como al final del string)
            string tagKey = splitTag[0].Trim();
            //array 2
            string tagValue = splitTag[1].Trim();

            switch (tagKey) 
            {
                case SPEAKER_TAG:
                    displayNameTest.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came but is not currently being handled: " + tag); 
                    break;

            }

        }


    }


    private void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length) 
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //escondemos las opciones de la lista que no han salido en el texto
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectedFirstChoice());
    }

    //método en el on click de los botones para desplegar la respuesta en función de la opcion elegida
    public void MakeChoice(int choiceindex) 
    {
        currentStory.ChooseChoiceIndex(choiceindex);
    }


    private IEnumerator SelectedFirstChoice() 
    {
        //Event system necesita limpiar primero, esperar un frame y luego setear la opcion escogida en un frame diferente
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }


    #endregion



}
