using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;

    [SerializeField] TextMeshProUGUI txt_Dialogue;
    [SerializeField] TextMeshProUGUI txt_Name;
    Dialogue[] dialogues;

    bool isDialogue = false;//��ȭ�� T/F
    bool isNext = false;   //�Է´��
    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;
    int lineCount = 0;      //��ȭ ī��Ʈ
    int contextCount = 0;   //��� ī��Ʈ
    void Update()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isNext = false;
                    txt_Dialogue.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;       //���� �ι��� ����
                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine (TypeWriter());
                        }
                        else                    //��糡����
                        {
                            EndDialogue();
                        }
                    }
                }
            }
        }
    }
    public void ShowDialogue(Dialogue[] P_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";
        dialogues = P_dialogues;
        SettingUI(true);
        StartCoroutine(TypeWriter());

    }
    void EndDialogue()
    {
        isDialogue=false;
        contextCount = 0;
        lineCount = 0;
        dialogues=null;
        SettingUI(false);
    }
    IEnumerator TypeWriter()
    {
        SettingUI(true);
        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ","); //�����󿡼� ' -> ,

        
        txt_Name.text=dialogues[lineCount].name;
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            txt_Dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }
        isNext = true;
       

    }
    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        go_DialogueNameBar.SetActive(p_flag);
    }

   
  
}
