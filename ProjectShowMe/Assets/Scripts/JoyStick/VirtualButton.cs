using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public GameObject player;
    public Image ImgBg;
    public Image ImgJoystick;

    public float Speed = 10f;

    private Vector3 _inputVector;
    public Vector3 InputVector
    {
        get
        {
            return _inputVector;
        }
    }

    private Vector2 startPosition;


    // Use this for initialization
    void Start()
    {
        startPosition = this.transform.position;
        //this.transform.parent = GameObject.FindGameObjectWithTag("Boundaries").transform;
    }

    public void OnPointerDown(PointerEventData e)
    {
        OnDrag(e);
    }

    public void OnDrag(PointerEventData e)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(ImgBg.rectTransform,
                                                                    e.position,
                                                                    e.pressEventCamera,
                                                                    out pos))
        {

            pos.x = (pos.x / ImgBg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / ImgBg.rectTransform.sizeDelta.y);


            _inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

            ImgJoystick.rectTransform.anchoredPosition = new Vector3(_inputVector.x * (ImgBg.rectTransform.sizeDelta.x * .4f),
                                                                     _inputVector.z * (ImgBg.rectTransform.sizeDelta.y * .4f));

            var pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            Vector2 offset = pointB - transform.position;
            Vector2 direction = _inputVector;//Vector2.ClampMagnitude(ImgJoystick.rectTransform.anchoredPosition, 1.0f);
            player.transform.localPosition = Vector3.Lerp(transform.position, direction, Speed * Time.deltaTime);
            //player.transform.Translate(direction * Speed * Time.deltaTime);

            /*
            if (gameObject.transform.localPosition.x > 180 || gameObject.transform.localPosition.x < -180 || gameObject.transform.localPosition.y > 340 || gameObject.transform.localPosition.y < -340)
            {
                transform.position = startPosition;
            }*/
        }

    }

    public void OnPointerUp(PointerEventData e)
    {
        _inputVector = Vector3.zero;
        ImgJoystick.rectTransform.anchoredPosition = Vector3.zero;
    }


    public float Horizontal()
    {
        if (_inputVector.x != 0)
        {
            return _inputVector.x;
        }

        return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (_inputVector.z != 0)
        {
            return _inputVector.z;
        }

        return Input.GetAxis("Vertical");
    }

    public void SetActive(GameObject _activeGameObject)
    {
        //When _activeGameObject is active set to inactive
        if (_activeGameObject.activeSelf)
            _activeGameObject.SetActive(false);
        //When not set to active
        else
            _activeGameObject.SetActive(true);
    }
}
