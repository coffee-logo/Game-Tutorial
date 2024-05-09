using UnityEngine;
using UnityEngine.AI;

//Player라는 클래스를 새롭게 만든다. 그리고 MonoBehaviour라는 클래스를 상속한다
public class Player : MonoBehaviour
{
    //SpriteRenderer라는 클래스에 해당하는 객체인 spriteRenderer를 새롭게 형성한다
    private SpriteRenderer spriteRenderer;
    //게임에서 실제로 사용할 sprite 들을 모아 sprites라는 이름의 리스트를 제작한다. public으로 설정함으로써 inspector창에서 수정할 수 있게 하고 다른 곳에서도 불러올 수 있도록 한다
    public Sprite[] sprites;
    //SpriteIndex라는 새로운 변수를 만든다. 단, 본 변수는 int라는 자료형을 지닌다
    private int SpriteIndex;
    //direction 이라는 새로운 변수를 만든다. 단, 본 변수는 Vector3 클래스의 인스턴스이다. 
    private Vector3 direction;
    //gravity라는 새로운 변수를 만든다. 본 변수는 float라는 자료형을 지닌다. 기본값은 -9.8f이다
    public float gravity = -9.8f;
    //strength라는 새로운 변수를 만든다. 본 변수는 float라는 자료형을 지닌다. 기본값은 5f이다.
    public float strength = 5f;

    //Awake라는 새로운 메소드를 만든다. 플레이어가 처음 활성화됐을 때 가장 먼저 실행되는 메소드이다. 
    private void Awake()
    {
        //현재 게임 오브젝트에서 SpriteRenderer 컴포넌트를 찾아 그것을 spriteRenderer 변수에 할당
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

    }

    private void AnimateSprite()
    {
        SpriteIndex++;

        if (SpriteIndex == sprites.Length)
        {
            SpriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[SpriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
