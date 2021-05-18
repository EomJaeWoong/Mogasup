using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 클래스 선언 위에 RequireComponent(typeof(Animator))라는 구문은
// 이 게임 오브젝트에 Animator가 없으면 안 된다고 명시하는 역할을 합니다.
// 이 스크립트가 붙어있는 게임 오브젝트에 Animator 컴포넌트가 없으면 게임이 실행되지 않습니다.
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    // 게임 오브젝트에 붙어있는 Animator 컴포넌트를 가져옴
    protected Animator avatar;

    // 플레이어 근처의 오브젝트
    GameObject nearObject;

    // Start is called before the first frame update
    void Start()
    {
        avatar = GetComponent<Animator>();    
    }

    // h: Horizontal 방향 컨트롤러 가로 방향
    // v: Vertical 방향 컨트롤러의 세로 방향
    float h, v;

    // 방향 컨트롤러에서 컨트롤러 변경시 호출
    public void OnStickChanged(Vector2 stickPos) {
        h = stickPos.x;
        v = stickPos.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(avatar) {
            float back = 1f;

            if(v < 0f)  back = -1f;

            // 애니메이터에 속도 전달
            avatar.SetFloat("Speed", (h*h+v*v));

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if(rigidbody) {
                Vector3 speed = rigidbody.velocity;
                speed.x = 8 * h;
                speed.z = 8 * v;
                rigidbody.velocity = speed;
                if(h != 0f && v != 0f) {
                    // 방향 전환
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
                }
            }
        }        
    }

    // Trigger Event
    void OnTriggerEnter(Collider other) {
        if(other.tag == "photo") {
            nearObject = other.gameObject;

            // photolist script 불러오기
            PhotoList photoList = nearObject.GetComponent<PhotoList>();
            photoList.Enter(this);
        } else if(other.tag == "board") {
            nearObject = other.gameObject;

            // boardlist script 불러오기
            BoardList boardList = nearObject.GetComponent<BoardList>();
            boardList.Enter(this);
        }
        else if (other.tag == "game")
        {
            nearObject = other.gameObject;

            // gamelist script 불러오기
            GameList gameList = nearObject.GetComponent<GameList>();
            gameList.Enter(this);
        }
    }

    void OntriggerExit(Collider other) {
        if(other.tag == "photo") {
            // photolist script 불러오기
            PhotoList photoList = nearObject.GetComponent<PhotoList>();
            photoList.Exit();
            nearObject = null;
        } else if(other.tag == "board") {
            // boardlist script 불러오기
            BoardList boardList = nearObject.GetComponent<BoardList>();
            boardList.Exit();
            nearObject = null;
        }
        else if (other.tag == "game")
        {
            // gamelist script 불러오기
            GameList gameList = nearObject.GetComponent<GameList>();
            gameList.Exit();
            nearObject = null;
        }
    }
}
