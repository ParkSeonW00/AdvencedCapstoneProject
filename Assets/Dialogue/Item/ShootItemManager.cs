using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootItemManager : MonoBehaviour
{

   public void UseGam()
    {
        EventManager.Instance.inventory.UseItem("����");
    }
   public void UseDduck()
    {
        EventManager.Instance.inventory.UseItem("��");
    }


}
