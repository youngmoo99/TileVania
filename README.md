# Tile Vania

플레이어가 몬스터와 함정을 피해 지점까지 도달하는 2D 플랫포머 액션 게임  
A classic 2D platformer built in Unity — avoid enemies and traps, climb ladders, and collect coins to reach the goal!

<p align="center">
  <a href="#demo">🎮 플레이 영상</a> •
  <a href="#features">✨ 주요 특징</a> •
  <a href="#tech-stack">🧰 기술 스택</a> •
  <a href="#setup">⚙️ 설치/실행</a> •
  <a href="#screenshots">🖼️ 스크린샷</a>
</p>
<p>
  <img alt="Unity" src="https://img.shields.io/badge/Unity-6.0-black?logo=unity"/>
  <img alt="Platform" src="https://img.shields.io/badge/Platform-Windows%20%7C%20macOS-blue"/>
</p>

---

## TL;DR

* **장르**: 2D Platformer / Action Adventure  
* **엔진**: Unity 6.0  
* **역할(Role)**: 기획 100%, 프로그래밍 100%  
* **플레이 루프**: 이동 → 점프/사다리 → 코인 수집 → 적/함정 회피 → 출구 도달 → 다음 스테이지 진행

---

<h2 id="demo">🎮 플레이 영상</h2>

▶️ **Gameplay Video**: 준비 중  

> 점프와 사다리를 이용해 장애물을 회피하고, 적과의 접촉을 피하면서 스테이지를 완주하세요.  
> 코인을 수집하면 점수가 오르고, 생명은 제한되어 있습니다.

---

<h2 id="features">✨ 주요 특징 / Features</h2>

* 🧱 **Tilemap 기반 스테이지 구성** — 레벨을 쉽게 제작할 수 있는 2D 타일 시스템  
* 🪙 **코인 수집 시스템** — 점수 상승 및 사운드 피드백  
* 🧗 **사다리 오르기** — 중력 해제 + 애니메이션 전환으로 부드러운 등반  
* 💀 **적 및 함정 충돌 판정** — 충돌 시 사망 애니메이션 + 반동 + 색상 변경  
* 💥 **총알 발사 시스템** — 발사체가 적과 충돌 시 제거  
* 🌀 **적 순찰 로직** — 경계 트리거에서 방향 전환 및 Flip 처리  
* ❤️ **목숨/점수 UI** — 게임 세션 전역 관리 (GameSession Singleton)  
* 🔄 **씬 유지 시스템** — ScenePersist로 스테이지 전환 시 오브젝트 유지  
* 🏁 **레벨 종료 처리** — 출구 트리거 시 다음 씬 전환, 마지막 스테이지 시 결과 씬 이동  
* 🎵 **배경음 싱글톤 관리** — 씬 전환 시 음악 중단 없이 유지  

---

<h2 id="tech-stack">🧰 기술 스택 / Tech Stack</h2>

**엔진**: Unity 6.0  
**언어**: C#  
**패키지/툴**: TextMeshPro, Rigidbody2D Physics, Tilemap, Animator, Input System, Git, VS Code

**핵심 시스템 구성**

| 시스템 | 설명 |
|--------|------|
| **PlayerMovement** | 이동, 점프, 사다리, 사망, 애니메이션 제어 |
| **Bullet** | 발사체 이동 및 충돌 판정 (Enemy 제거) |
| **EnemyMovement** | 좌우 순찰 및 Flip 처리 |
| **CoinPickUp** | 코인 수집 시 점수 증가 및 사운드 재생 |
| **GameSession** | 목숨/점수 관리 (Singleton 유지) |
| **LevelExit** | 스테이지 완료 후 다음 씬 전환 및 GameClear 처리 |
| **ScenePersist** | 스테이지 전환 시 특정 오브젝트 유지 |
| **BackgroundMusic** | 배경음 싱글톤 관리 |
| **UIGameOver / UIResult** | 결과 및 게임오버 UI 처리 |
| **GameManager** | 메인 메뉴, 씬 전환, 초기 로드 제어 |

---

<h2 id="architecture">🏗️ 프로젝트 구조 / Architecture</h2>

```
Assets/
 TileVania/
  Player/
   PlayerMovement.cs
   Bullet.cs
  Enemy/
   EnemyMovement.cs
  Collectibles/
   CoinPickUp.cs
  Managers/
   GameManager.cs
   GameSession.cs
   ScenePersist.cs
  UI/
   UIGameOver.cs
   UIResult.cs
  System/
   LevelExit.cs
   BackgroundMusic.cs
  Scenes/
   MainMenu.unity
   Level 1.unity
   Level 2.unity
   GameOver.unity
   GameClear.unity
```

**설계 포인트**  
* 각 시스템은 단일 책임 원칙(SRP)을 따름  
* GameSession, ScenePersist를 통한 세션 관리로 중복 방지  
* 씬 전환 시 `FindObjectOfType` 기반의 동적 참조로 UI 안정화  
* LayerMask로 충돌 레이어(Ground, Enemies, Hazards, Climbing) 관리


---

<h2 id="setup">⚙️ 설치 및 실행 / Setup</h2>

저장소 클론

git clone https://github.com/<YOUR_ID>/RoyalRun.git


Unity Hub에서 프로젝트 열기

Packages 자동 복구 후, Assets/Scenes/Demo.unity 실행

▶️ Play

에셋 의존성이 있는 경우, Readme/팝업 안내에 따라 의존 패키지를 함께 설치하세요.

---

<h2 id="controls">🎮 조작법 / Controls</h2>

| 동작    | 조작         | 설명 |
| ----- | ---------- | ------- |
| 이동    | A / D 혹은  ← / → | 좌우 이동 |
| 점프   | Space | 점프 | 
| 사다리 오르기 | W / S 혹은 ↑ / ↓ | 사다리 위/아래 이동|
| 발사 | Mouse Left | 총알 발사 |


이동은 x/z 평면에서 경계(Clamp)로 제한되어 레인 이탈을 방지합니다.

---

<h2 id="screenshots">🖼️ 스크린샷 / Screenshots</h2> <p align="center"> <img src="Royal Run Main.png" width="720" alt="Royal Run Gameplay"/> <img src="Royal Run Main2.png" width="720" alt="Royal Run Gameplay"/> </p>

적을 피하고 코인을 모으며 사다리를 오르고 점프해 스테이지를 탈출하세요.
클래식 감성과 현대적인 입력 시스템이 결합된 Unity 2D 플랫포머입니다.

---

<h2 id="roadmap">🚀 향후 계획 / Roadmap</h2>

 Checkpoint 시스템 추가

 적 AI 확장 (추적 / 투사체 공격)

 체력 기반 데미지 시스템

 아이템/무기 업그레이드 시스템

 모바일 터치 대응 및 패드 지원

 ---

<h2 id="credits">👤 제작자 / Credits</h2>

* **기획·개발**: 김영무 (Kim YoungMoo)

* **아트 리소스**: Low-poly 리소스(상용/프리믹스)

* **사운드**: 자체 제작 & 무료 라이브러리 활용
  
* **참고 강의**: [강의 링크](https://www.udemy.com/course/best-c-unity-2d/?kw=c%23&src=sac&couponCode=MT251110G1)


 ---
 
<h2 id="contact">📬 연락처 / Contact</h2>

* **이메일**: [rladuan612@gmail.com](mailto:rladuan612@gmail.com)
* **포트폴리오**: [포트폴리오](https://www.naver.com)
