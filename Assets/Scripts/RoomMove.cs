using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChangeMax;
    public Vector2 cameraChangeMin;

    public Vector3 playerChange;
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public TextMeshProUGUI placeText;

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.minPosition.x = cameraChangeMin.x;
            cam.minPosition.y = cameraChangeMin.y;
            cam.maxPosition.x = cameraChangeMax.x;
            cam.maxPosition.y = cameraChangeMax.y;
            other.transform.position += playerChange;
            if(needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }
}