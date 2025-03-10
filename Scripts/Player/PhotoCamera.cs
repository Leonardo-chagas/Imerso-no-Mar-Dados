using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PhotoCamera : MonoBehaviour
{
    [SerializeField] private InputActionReference rightHandActivate;
    [SerializeField] private Animator maskAnimator;

    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private Camera cam;
    [SerializeField] private Camera otherCam;
    [SerializeField] private Transform point;
    [SerializeField] private GameObject CameraRange;
    [SerializeField] private RenderTexture screenTexture;
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private Sprite testSprite;
    private Sprite photoSprite;
    private AudioSource audioSource;
    private TMP_Text photoText;
    private Texture2D screenCapture;
    [HideInInspector] public bool viewingPhoto;
    private List<(GameObject, string)> fishesInView = new List<(GameObject, string)>();
    private bool canTakePhoto = false;
    GameObject fishObject;
    private RecordPhotoEvent photoEvent;

    [Header("Events")]
    public GameEvent photoShot;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        photoEvent = GetComponent<RecordPhotoEvent>();

        rightHandActivate.action.performed += _ => TakePhoto();
        
        //screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenCapture = new Texture2D(screenTexture.width, screenTexture.height, TextureFormat.RGB24, false, true);

        GameObject canvas = GameObject.Find("Canvas- Canvas");
        photoFrame = canvas.transform.GetChild(0).gameObject;
        photoDisplayArea = canvas.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Image>();
        photoText = canvas.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<TMP_Text>();

        cam.gameObject.SetActive(false);
    }

    private void OnDisable(){
        canTakePhoto = false;
    }

    private void OnEnable(){
        canTakePhoto = true;
    }

    private void TakePhoto(){
        if(!canTakePhoto) return;
        canTakePhoto = false;
        audioSource.Play();
        fishesInView.Clear();
        photoText.text = "";
        StartCoroutine(CapturePhoto());
        //maskAnimator.SetTrigger("cutin");
        print("photo taken");
    }

    IEnumerator CapturePhoto(){
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        //cam.gameObject.SetActive(true);
        //RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        /* screenTexture.width = Screen.width;
        screenTexture.height = Screen.height; */
        //screenTexture.depth = 16;
        //screenTexture.depthStencilFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.D16_UNorm;
        //screenTexture.graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
        //transmite a imagem da câmera para a variável screenTexture
        //cam.targetTexture = screenTexture;
        //passa essa textura para a renderização principal
        otherCam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        //cam.Render();
        otherCam.Render();

        //Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        Rect regionToRead = new Rect(0, 0, screenTexture.width, screenTexture.height);

        //le os pixels da textura
        //screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.ReadPixels(regionToRead, 0, 0);
        screenCapture.Apply();
        
        //reinicia a renderização principal
        //RenderTexture.active = null;

        //Comunica o ínicio do evento de foto
        photoShot.Raise(this, cam);

        /* RaycastHit[] boxCastHit = Physics.BoxCastAll(point.position, new Vector3(1, 1, 2.5f), point.forward, layerMask = );
        foreach */
        
        cam.gameObject.SetActive(false);
        ShowPhoto();
    }

    private void ShowPhoto(){
        /* Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite; */
        photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
        //photoDisplayArea.texture = screenCapture;
        //photoDisplayArea.sprite = testSprite;
        otherCam.targetTexture = renderTexture;
        RenderTexture.active = null;

        photoFrame.SetActive(true);

        StartCoroutine(PhotoAway());
        StartCoroutine(WaitToFindFish());
    }

    IEnumerator WaitToFindFish(){
        yield return new WaitForSeconds(0.5f);
        DetectFish();
    }

    //-----terceiro passo da detecção de peixe-----
    //detectar o peixe na foto
    /* 
    - Peixe mais próximo da camera?
    - Peixe mais próximo do centro da camera?
    - maior peixe na foto?
     */
    private void DetectFish(){
        //GameObject fishObject;
        string fishClosestToCenter = "";
        float closestDistance = 100;
        
        foreach((GameObject, string) fish in fishesInView){
            //print(fish.Item2);
            Vector2 point = cam.WorldToViewportPoint(fish.Item1.transform.position);

            float dx = point.x - 0.5f;
            float dy = point.y - 0.5f;

            float distanceSquared = dx*dx + dy*dy;

            float newClosestDistance = Mathf.Min(distanceSquared, closestDistance);

            if(newClosestDistance != closestDistance){
                closestDistance = newClosestDistance;
                fishClosestToCenter = fish.Item2;
                fishObject = fish.Item1;
            }
        }
        if(fishesInView.Count > 0){
            if(fishObject.CompareTag("Small fish") && Vector3.Distance(fishObject.transform.position, transform.position) > 15 && Vector3.Distance(fishObject.transform.position, transform.position) < 25){
                photoText.text = "<color=red>Você está muito longe do peixe</color>";
                photoEvent.RecordPhotoTaken("Você está muito longe do peixe");
            }
            else if(fishObject.CompareTag("Big fish") && Vector3.Distance(fishObject.transform.position, transform.position) > 25 && Vector3.Distance(fishObject.transform.position, transform.position) < 40){
                photoText.text = "<color=red>Você está muito longe do peixe</color>";
                photoEvent.RecordPhotoTaken("Você está muito longe do peixe");
            }
            else if((fishObject.CompareTag("Small fish") && Vector3.Distance(fishObject.transform.position, transform.position) <= 15) || (fishObject.CompareTag("Big fish") && Vector3.Distance(fishObject.transform.position, transform.position) <= 25)){
                photoText.text = "<color=green>" + fishClosestToCenter + "</color>";
                FishDataHolder.instance.Save(fishClosestToCenter);
                photoEvent.RecordPhotoTaken(fishClosestToCenter);
            }
            else{
                photoText.text = "<color=red>Nenhum peixe foi detectado</color>";
                photoEvent.RecordPhotoTaken("Nenhum peixe foi detectado");
            }
        }
        else{
            photoText.text = "<color=red>Nenhum peixe foi detectado</color>";
            photoEvent.RecordPhotoTaken("Nenhum peixe foi detectado");
        }
    }

    IEnumerator PhotoAway(){
        yield return new WaitForSeconds(3.0f);
        RemovePhoto();
    }

    private void RemovePhoto(){
        canTakePhoto = true;
        viewingPhoto = false;
        photoFrame.SetActive(false);

    }

    public void FishInView(GameObject fish, string fishName){
        fishesInView.Add((fish, fishName));
    }

    public void CancelPhoto(Component sender, object data){
        StopAllCoroutines();
        photoFrame.SetActive(false);
    }
}
