using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeParser : MonoBehaviour
{
   public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialougeList = new List<Dialogue>(); //��ȭ ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); //csv���� �ε�

        string[] data = csvData.text.Split(new char[] { '\n' }); //���� ������ �ɰ�
        
        for(int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });       //,������ �ɰ�

            Dialogue dialogue = new Dialogue();     //��� ����Ʈ ����
            dialogue.name = row[1];     //�����ι� �̸�
            

            List<string> contextList = new List<string>();


            do
            {
                contextList.Add(row[2]);
               
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");      
            dialogue.contexts=contextList.ToArray();
            dialougeList.Add(dialogue);
           
        }
        return dialougeList.ToArray();

    }
   
}
