using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPad : MonoBehaviour
{
    // touchpad object
    private RectTransform _touchPad;

    // 터치 입력 중 방향 컨트롤러의 영역 안에있는 입력 구분 아이디
    private int _touchId = -1;

    // 입력 시작 좌표
    private Vector3 _startPos = Vector3.zero;

    // 방향 컨트롤러가 원으로 움직이는 반지름
    public float _dragRadius = 60f;

    // 플레이어의 움직임을 관리하는 PlayerMovement 스크립트와 연결
    public PlayerMovement _player;

    // 버튼이 눌렸는지 체크
    private bool _buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        // 터치패드 RectTransform
        _touchPad = GetComponent<RectTransform>();

        // 터치패드 좌표
        _startPos = _touchPad.position;    
    }

    // 버튼 누름 여부 확인
    public void ButtonDown() {
        _buttonPressed = true;
    }

    public void ButtonUp() {
        _buttonPressed = false;
        HandleInput(_startPos); // 터치패드, 좌표 원위치
    }

    void FixedUpdate() {
        // 모바일 -> 터치패드 입력
        HandleTouchInput();

        // 모바일 외에는 터치로 이동
        #if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
            HandleInput(Input.mousePosition);

        #endif
    }

    void HandleTouchInput() {
        // 터치 아이디
        int i = 0;

        if(Input.touchCount > 0) {
            // 모든 터치에 대해 조회
            foreach(Touch touch in Input.touches) {
                i++;

                // 해당 좌표 구하기
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);

                // 터치 입력이 시작되었다면
                if(touch.phase == TouchPhase.Began) {
                    if(touch.position.x <= (_startPos.x + _dragRadius)) {
                        // 터치의 좌표가 현재 방향키 안이면
                        // 터치 아이디 기준으로 컨트롤러 조작
                        _touchId = i;
                    }
                }

                // 터치 입력이 움직이거나, 가만히 있을 때
                if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                    if(_touchId == i) { // 아이디가 지정된 경우
                        HandleInput(touchPos);
                    }
                }

                // 터치 입력이 끝났을 때
                if(touch.phase == TouchPhase.Ended) {
                    if(_touchId == i) { // 입력받으려 했던거면 해제
                        _touchId = -1;
                    }
                }
            }
        }
    }

    void HandleInput(Vector3 input) {
        if(_buttonPressed) {
            // 버튼과 기준좌표 거리 구하기
            Vector3 diffVector = (input - _startPos);

            // 거리 비교 후, 최대치보다 크면
            if(diffVector.sqrMagnitude > _dragRadius * _dragRadius) {
                diffVector.Normalize(); // 방향벡터 거리 1로 조정

                // 방향 컨트롤러는 최대치만큼만 움직이게 함
                _touchPad.position = _startPos + diffVector * _dragRadius;
            }

            else {  // 최대치보다 작을시 
                // 입력좌표에 방향키 이동
                _touchPad.position = input;
            }
        }

        // 방향키에서 손 떼지면
        else {
            _touchPad.position = _startPos; // 처음 값으로 돌림
        }

        // 방향키 기준지점 차이
        Vector3 diff = _touchPad.position - _startPos;

        // 방향 유지한 채로, 거리를 나누어 방향 구하기
        Vector2 normDiff = new Vector3(diff.x / _dragRadius, diff.y / _dragRadius);

        if(_player != null) {
            // 플레이어와 연결되어 있으면 변경된 좌표 전달
            _player.OnStickChanged(normDiff);
        }
    }
}
