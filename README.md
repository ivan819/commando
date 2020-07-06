# Објаснување на играта Commando

## Правила на игра

### Играч
Оваа проектна задача се состои од игра каде играчот управува авион. Играчот го управува движењето (со W,A,S,D) и пукањето (SPACE) на авионот.
Авионот на играчот ги има следните особини:
- Health    - ако здравјето на играчот падне под 0, играта завршува
- Damage    - колку штета им прави играчот на непријателите
- Speed     - брзина на движење
- ShootRate - интервал на пукање

### Непријатели

Од горниот дел на екранот напаѓаат различни непријателски авиони. Непријателите можат да бидат од различни типови (едни се движат странично и пукаат надоле, а други се движат право и пукаат кон моменталната позиција на играчот). На секои 60 секунди се зголемува нивото, а со тоа непријателите стануваат помоќни (добиваат повеќе енергија, прават поголема штета и побрзо се движат) 

### Појачувања (PowerUps)

PowerUps се објекти кои му даваат различни предности на играчот. За да се активираат, играчот треба да ги собере (допре) со авионот.
Типови на појачувања:
- Instakill  - Играчот добива +10000 Damage (трае 10 секунди)
- Freeze     - Сите непријатели кои се моментално на екранот се замрзнуваат
- Heal       - Играчот се лечи за +100 Health
- MultiShot  - Пукање 5 куршуми одеднаш 
- Stats      - Играчот добива +Health, +Damage, +Speed, +ShootRate
- Invincible - Играчот во наредните 10 секунди не може да биде оштетен

## Цел на играта 

Целта на играта е играчот да преживее што подолго и да собере што повеќе поени. Поени (Score) се добиваат со уништување на непријателите. Постои и механизам за Score Multiplier. За секој убиен непријател multiplier-от се зголемува за 1, а ако играчот прими damage од некој непријател multiplier-от се враќа на вредност 1. На крај играчот може да ги зачува своите поени во High Scores.
Во High Scores се чуваат најдобрите 10 поени.

# Структура и организација на код

**MenuMain** е форма за главното мени.

**MainGame** е форма за играта.

**Scene** е главната игра. Содржи информации за сите моментални објекти. Содржи листи од Enemies, Bullets, Explosions, Bullets. Содржи тајмери за генерирање на непријатели и појачувања. Има и тајмери за зголемување на тежината на играта и помошен тајмер за прикажување на порака. Оваа класа ги содржи најважните функции во прогамата. Повеќе детали за функциите има во **Опис на поважни функции**

**Utils** е помошна класа со фунционалности за полесно креирање на нови типови на елементи и функционалности на преземање на сликите, кои се ицртуваат за соодветните објекти , од ресурси.

**MovingObject** е главната класа од која наследуваат другите објекти. Класата претставува објект на екранот кој ги има следните особини:
- X позиција на објектот во панелот
- Y позиција
- Висина и ширина
- Брзина на движење
- Health 
- Слика која се исцртува на екранот

MovingObject ги содржи функциите:
- bool IsCollidingWith(MovingObject o) - функција за проверка на колизии со други објекти
- void Draw(Graphics g) - функција за цртање во панелот
- void Move() - Функција за придвижување на објектот

**Player** е класа за објекот на играчот. Наследува од MovingObject и има дополнителни функционалности како тајмери за следење на PowerUps и информации за пукањето на играчот (ShootRate, BulletSpeed, Damage). Функцијата Move() е преоптоварена бидејќи ни се потребни аргументи со информации за KeyPress од играчот. Со ова се дава контрола на движењето.
Дополнително e имплементирана функциjата Shoot(bool space, Scene scene) која му дава на играчот контрола на пукањето.

**Enemy** е класа за непријателот. Наследува од MovingObject. Слично како кај Player имплементирана е Shoot() функција. Разликата е што тука не се предава контрола на играчот туку функцијата е автоматска.

**Bullet** е класа за куршумите. Наследува од MovingObject и додадено е само променлива за чување на Damage

**Explosion** e помошна класа која се користи само за ицртување на експозиите кои настануваат кога ке уништиме некој непријател. Има тајмер кој по ~1 секунда ја брише експозијата од екранот.

**PowerUp** е класа за појачувањата. Наследува од MovingObject. Дополнително чува информација за типот на PowerUp

# Опис на поважни функции

-**void DrawUI(Graphics g)** - Го црта корисничкиот интерфејс, односно Health и Healthbar, Score и ScoreMultiplier, Stats.
-**void DrawMoveShootAllObjects(Graphics g)** - Оваа функција се повикува во секој Tick  на тајмерот. Функцијата ги контролира скоро сите аспекти на играта меѓу кои се : контрола и детекција на колизии измеѓу куршуми и непријатели, куршуми и играчот или играчот и непријател. 
Оваа функција ги повикува функциите Move(), Shoot() и Draw() на сите индивидуални објекти.
-**void PowerUp(String power)** - Функција за контрола на појавување на PowerUps.

# Screenshoots од изгледот на играта
## Gameplay :

![alt text](https://raw.githubusercontent.com/ivan819/commando/master/gameplay.PNG)

## Главно мени :

![alt text](https://raw.githubusercontent.com/ivan819/commando/master/glavnoMeni.PNG)

## HighScores :

![alt text](https://raw.githubusercontent.com/ivan819/commando/master/highscoreTable.PNG)

## Game Over форма :

![alt text](https://raw.githubusercontent.com/ivan819/commando/master/GameOverForm.PNG)
