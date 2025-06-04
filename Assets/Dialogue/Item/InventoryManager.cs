using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject fullInventoryUI; 
    [SerializeField] private GameObject miniSlotUI;     

    [SerializeField] private Inventory inventory;        
    [SerializeField] private Slot miniSlot;
    [SerializeField] private Button close_btn;
    [SerializeField] private Button open_btn;

    private void Update()
    {
        if (inventory.items.Count > 0)
        {
            miniSlot.item = inventory.items[0];
        }
        else
        {
            miniSlot.item = null;
        }
        inventory.FreshSlot();
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            EventManager.Instance.TriggerEvent("clickSound");
            fullInventoryUI.SetActive(!fullInventoryUI.activeSelf);
            miniSlotUI.SetActive(!miniSlotUI.activeSelf);
            open_btn.gameObject.SetActive(!open_btn.gameObject.activeSelf);
            close_btn.gameObject.SetActive(!close_btn.gameObject.activeSelf);
        }
    }
    public void OpenInventory()
    {
        EventManager.Instance.TriggerEvent("clickSound");
        fullInventoryUI.SetActive(true);
        miniSlotUI.SetActive(false);
        open_btn.gameObject.SetActive(false);
        close_btn.gameObject.SetActive(true);
       
    }

    public void CloseInventory()
    {
        EventManager.Instance.TriggerEvent("clickSound");
        fullInventoryUI.SetActive(false);
        miniSlotUI.SetActive(true);
        open_btn.gameObject.SetActive(true);
        close_btn.gameObject.SetActive(false);
       
       
        // ù ��° ������ �����ͼ� �ݿ�
        if (inventory.items.Count > 0)
        {
            miniSlot.item = inventory.items[0];
        }
        else
        {
            miniSlot.item = null;
        }
    }
}
