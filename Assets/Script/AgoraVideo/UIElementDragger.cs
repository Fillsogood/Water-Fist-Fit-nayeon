﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace agora_utilities
{
    //드래그 앤 드랍
    public class UIElementDragger : EventTrigger
    {

        public override void OnDrag(PointerEventData eventData)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            base.OnDrag(eventData);
        }
    }
}