# ZepProject# Unity 2D 미니게임 - 연출 기반 이동 및 점프

ZEP 스타일의 2D 캐릭터 이동 및 점프 연출을 중심으로 제작된 미니게임입니다.  
플레이어는 자연스럽게 맵을 이동하며, 위 방향으로 점프하는 연출과 카메라 추적 기능이 포함되어 있습니다.

---

## 주요 기능

### 캐릭터 이동
- WASD 또는 방향키로 상하좌우 및 대각선 이동
- Rigidbody2D + MovePosition을 활용한 이동 처리

### 연출 기반 점프
- 스페이스바 입력 시 Y축 기준으로 점프 연출
- Mathf.Sin() 곡선을 활용한 부드러운 점프 구현
- 점프 중 이동 가능
- 중력을 사용하지 않음 (gravityScale = 0)

### 카메라 추적
- 메인 카메라는 플레이어를 따라 이동
- Vector3.Lerp() 기반의 부드러운 추적
- 카메라 Z축 고정 (-10)로 2D 화면 유지

---

## 프로젝트 구조
MainScene/
├── Player (빈 오브젝트)

│ └── MainSprite (스프라이트 및 애니메이션)

├── Grid (타일맵: Floor, Collision)

└── Main Camera (FollowCamera.cs)
## 사용 기술

- Unity 2022.3.17f
- Rigidbody2D, SpriteRenderer, Tilemap, Animator
- C# 기반 입력 및 이동 처리

---

## 트러블슈팅

### 화면이 하얘지는 문제
- 원인: 카메라가 Z축 0 또는 Skybox 배경 사용
- 해결: 
  - Main Camera의 Z 위치를 -10으로 설정
  - Clear Flags = Solid Color, 배경색을 어두운 색으로 설정
  - FollowCamera.cs에서 desiredPosition.z = -10f 로 고정

### 점프 후 내려오지 않음
- 원인: 기준 Y 위치가 이동하며 변함
- 해결: 점프 시작 시 jumpBaseY를 저장하고, jumpBaseY + Sin 곡선으로 Y 위치를 계산

### 점프 후 제자리로 돌아감
- 원인: jumpStartPos 기준으로 다시 이동시키는 코드 존재
- 해결: 점프 중 offset만 사용하고, 점프 종료 시 복귀하지 않음

---

## 향후 개발 예정 기능

- UI 및 미니게임 스코어 시스템

