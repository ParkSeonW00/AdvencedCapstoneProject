using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    [Tooltip("��� ġ�� ĳ���� �̸�")]
    public string name;

    [Tooltip("��� ����")]
    public string[] contexts;

    //������
    public bool isChoice;
    public string choice1;
    public string choice2;
    public string choice3;
    public string choice1_Event;
    public string choice2_Event;
    public string choice3_Event;
    public string eventKey;
    public int choice1_Next;
    public int choice2_Next;
    public int choice3_Next;
    public int skipLine;

}
[System.Serializable]
public class DialogueEvent
{
    public string csvFileName;
    public string name;

    public Vector2 line;
    public Dialogue[] dialogues;


}