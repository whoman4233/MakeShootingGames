# 프로젝트 개요
패턴 기반 구조 설계와 객체 풀링 최적화를 적용한 2D 슈팅 게임입니다. 현재 프로토타입단계까지 구현되어있습니다.

## 주요 구현 기능
- 전략 패턴 기반의 적 공격/이동/패턴 구현
- 풀링 시스템 (총알, 적, 플레이어)
- 상태 머신 기반 게임 흐름 (로비 → 전투 → 종료)
- Google Sheet 기반 데이터 연동

## 사용 디자인 패턴
- 전략 패턴 (적, 플레이어, 보스등의 이동/공격)
- 팩토리 패턴(적과 보스를 팩토리패턴을 이용해 생성)
- 상태 패턴 (플레이어와 게임의 상태를 구현)
- 커맨드 패턴 (입력 처리)
- 싱글톤 패턴 (GameManager, PoolManager)
- 오브젝트 풀 패턴

## 시연 영상
https://github.com/whoman4233/MakeShootingGames/issues/1#issue-3030861390

## 스크린샷
- 로비 화면
- 인게임 전투
- 보스 등장
- 게임오버 화면

## 실행 방법
1. Unity 2022.3.X 이상 설치
2. 이 프로젝트를 Clone
3. `/Scenes/LobbyScene 1` 실행 후 Play

## 추가 개발 아이디어
- 스테이지 시스템 추가
- 추가적인 공격 전략 생성
- 전체적인 퀄리티 증가
- 사운드 및 이펙트 추가
