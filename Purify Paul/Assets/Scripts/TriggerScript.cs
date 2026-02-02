using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerScript : MonoBehaviour
{
    public EventTrigger.TriggerEvent Trigger;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            this.Trigger.Invoke(eventData);
            Destroy(gameObject);
        }
    }
}
