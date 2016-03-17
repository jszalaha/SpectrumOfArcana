using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
    public float speed;
    public bool tired;
    public Transform linecastStart, linecastEnd;
    public RaycastHit2D rayhit;

    private DialoguePanelManager dialoguePanel;
    private InputManager inputManager;

    Animator anim;
	SpriteRenderer rend;
    Rigidbody2D rb;

    public enum Facing
    {
        FRONT,
        BACK,
        RIGHT,
        LEFT
    };
    public Facing face = Facing.FRONT;   

	// Use this for initialization
	void Start () {
        dialoguePanel = DialoguePanelManager.Instance();
        inputManager = InputManager.Instance();
        anim = GetComponent<Animator> ();
		rend = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		rend.sprite = null;
	}

    void FixedUpdate() { }

    public void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        anim.SetFloat("SpeedX", moveX);
        anim.SetFloat("SpeedY", moveY);

        if (Input.GetButton("Fire3")) // IF RUNNING
        {
            speed = 2;
            anim.SetFloat("SpeedX", moveX * 2);
            anim.SetFloat("SpeedY", moveY * 2);
        }
        else speed = 1;

        // ADD LOGIC TO PREVENT DIAGONAL MOVEMENT
        if (moveX != 0 && moveY == 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY
                | RigidbodyConstraints2D.FreezeRotation;

            if (moveX > 0)
            {
                face = Facing.RIGHT;
            }
            else
            {
                face = Facing.LEFT;
            }
        }
        if (moveX == 0 && moveY != 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX
                | RigidbodyConstraints2D.FreezeRotation;

            if (moveY > 0)
            {
                face = Facing.BACK;
            }
            else
            {
                face = Facing.FRONT;
            }
        }
        if (moveX == 0 && moveY == 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        rb.velocity = new Vector2(moveX * speed, moveY * speed);
    }

    public void Raycasting()
    {
        linecastEnd.position = linecastStart.position;
        switch (face)
        {
            case Facing.FRONT:
                linecastEnd.position = new Vector3(linecastEnd.position.x, linecastEnd.position.y - 0.15f, linecastEnd.position.z);
                break;
            case Facing.BACK:
                linecastEnd.position = new Vector3(linecastEnd.position.x, linecastEnd.position.y + 0.15f, linecastEnd.position.z);
                break;
            case Facing.RIGHT:
                linecastEnd.position = new Vector3(linecastEnd.position.x + 0.2f, linecastEnd.position.y, linecastEnd.position.z);
                break;
            case Facing.LEFT:
                linecastEnd.position = new Vector3(linecastEnd.position.x - 0.2f, linecastEnd.position.y, linecastEnd.position.z);
                break;
        }
        Debug.DrawLine(linecastStart.position, linecastEnd.position, Color.red, Time.deltaTime, false);
        rayhit = Physics2D.Linecast(linecastStart.position, linecastEnd.position, 1 << LayerMask.NameToLayer("Interactable"));
    }

    public void Interact()
    {
        if (Input.GetButtonUp("Fire1") && rayhit)
        {
            dialoguePanel.Display(rayhit.transform.gameObject.GetComponent<AttributeHandler>().description);
            anim.SetFloat("SpeedX", 0f);
            anim.SetFloat("SpeedY", 0f);
            inputManager.state = InputManager.UIState.PAUSED;
        }
    }
}
