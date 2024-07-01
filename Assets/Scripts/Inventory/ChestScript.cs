using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestScript : MonoBehaviour, IPointerDownHandler
{
    private ChestInventoryManager CInventoryManager;
    private void Awake()
    {
        CInventoryManager = GameObject.Find("ChestInventoryCanvas").GetComponent<ChestInventoryManager>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("ChestPointerDown!!!");
        //CInventoryManager.getCInventory().SetActive(true);
        CInventoryManager.Activate();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
