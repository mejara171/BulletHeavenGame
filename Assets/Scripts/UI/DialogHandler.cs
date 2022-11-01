using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogHandler : MonoBehaviour
{
    public List<DialogData> dialogDataList = new List<DialogData>();
    public Text character1Text;
    public Text character2Text;

    Canvas character1Canvas;
    Canvas character2Canvas;

    int dialogIndex = -1;

    private void Awake()
    {
        character1Text.text = "";
        character2Text.text = "";

        character1Canvas = character1Text.transform.parent.GetComponent<Canvas>();
        character2Canvas = character2Text.transform.parent.GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        NextDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextDialog();
    }

    void NextDialog()
    {
        if(dialogIndex >= dialogDataList.Count -1 )
        {
            CGUtils.DebugLog("Dialog is done");

            SceneManager.LoadScene("TestSuperTiled2Unity");

            return;
        }

        CGUtils.DebugLog("NextDialog move to next dialog");

        dialogIndex++;

        DialogData dialogData = dialogDataList[dialogIndex];

        if (dialogData.IsPerson1Speaking)
        {
            character1Canvas.transform.localScale = Vector3.one* 1.25f;
            character2Canvas.transform.localScale = Vector3.one * 0.75f;

            character1Text.text = dialogData.DialogString;
        }
        else
        {
            character1Canvas.transform.localScale = Vector3.one * 0.75f;
            character2Canvas.transform.localScale = Vector3.one * 1.25f;

            character2Text.text = dialogData.DialogString;
        }

    }
}
