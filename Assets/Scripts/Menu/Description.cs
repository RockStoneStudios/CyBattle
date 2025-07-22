using UnityEngine;
using UnityEngine.EventSystems;

public class Description : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] GameObject dropdown;
    public void OnPointerEnter(PointerEventData eventData)
    {
        dropdown.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dropdown.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropdown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
