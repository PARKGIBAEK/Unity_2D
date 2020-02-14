using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//이벤트 시스템을 상속받기 위함
public class ImageButton : MonoBehaviour, IPointerEnterHandler
{
    Button myButton = null;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("script Enter");
    }
    RawImage myRawImage = null;
    IEnumerator Start()
    {
        myRawImage = this.GetComponent<RawImage>();
        WWW myWWW = new WWW("https://pngimg.com/uploads/twitter/twitter_PNG19.png");
        yield return myWWW;//yield return이 종료될 때까지 기다린다는 것, 위의 그림을 Start함수가 끝나기전에 받아오지 못하면 그림이 로딩이 안되기 때문에 Coroutine기능 사용.
        myRawImage.texture = myWWW.texture;
        myButton = this.GetComponent<Button>();
        myButton.interactable = false;
        myButton.transition = Selectable.Transition.SpriteSwap;
        ColorBlock cb = myButton.colors;//colorTint에서 색을 바꿀 때 한번에 안됨
        cb.highlightedColor = Color.red;
    }
    float w = 1, h = 1, x = 0, y = 0;
    bool a = true;
    private void Update()
    {

        if (w <= 0 && a)
        {
            a = false;
        }
        else if (w >= 1 && !a)
        {
            a = true;
        }
        w -= a ? Time.deltaTime : -Time.deltaTime;
        h -= a ? Time.deltaTime : -Time.deltaTime;
        myRawImage.transform.localScale = new Vector2(w, h);
        //myRawImage.uvRect = new Rect(Vector2.zero, new Vector2(w, h));
    }
}

