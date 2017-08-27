var player;
var playerLives = 3;

var coins;
var countCoins = 10;

var enemies;
var countEmeny = 5;

var bomb;
var numberOfBombs = 1;
var explosion;

var score = 0;
var bestScore = 0;
var godmodeON = false;
var gameOver = true;
var firstGame = true;
var explosionSwitch = false;

function setup() {
  //zjištění velikosti okna
  createCanvas(window.innerWidth, window.innerHeight);
  //vytvoření skupin objektů
  coins = new Group();
  enemies = new Group();

  //spawnutí coinů
  for (var i = 1; i <= countCoins; i++) {
    var c = createSprite(random(100, width - 100), random(100, height - 100), 10, 10);
    c.shapeColor = color(255, 255, 0);
    coins.add(c);
  }

  //spawnutí nepřátel
  for (var i = 1; i <= countEmeny; i++) {
    var e = createSprite(random(width), random(height), random(10, 50), random(10, 30));
    e.friction = 0.01;
    e.shapeColor = color(255, 0, random(255));
    e.maxSpeed = random(1, 10);
    e.rotateToDirection = true;
    enemies.add(e);
  }

  //vytvoření spritu hráče
  player = createSprite(width / 2, height / 2, 40, 40);
  player.shapeColor = color("yellow");

  //vytvoření spritu exploze
  explosion = createSprite(0, 0, 0, 0);
  explosion.shapeColor = color("red");


}

function draw() {

  //barva pozadí
  background(0, 150, 240);

  //"menu"
  textFont("Arial Black");
  noStroke();
  textSize(40);
  fill('red');
  text("Skóre: ", 30, 60);
  textSize(25);
  text("Nejlepší skóre: ", 30, 90);

  fill(255);
  textSize(50);
  textAlign(LEFT, CENTER);
  text(score, 180, 40);
  textSize(25);
  text(bestScore, 240, 80);

  textSize(20);
  fill("orange")
  text("Počet nepřátel: ", 50, 120);
  text(enemies.length, 230, 120);
  text("Počet životů: ", 50, 150);
  text(playerLives, 200, 150);

  //konec hry
  if (playerLives <= 0) {
    gameOver = true;

    textFont("Arial Black");
    textSize(150);
    fill('red');
    text("KONEC HRY", width / 2 - 500, height / 2);

    updateSprites(false);
  }

  if (gameOver == false) {
    firstGame = false;

    //nepřátelé pronásledují myš
    for (var i = 0; i < enemies.length; i++) {
      enemies[i].attractionPoint(0.2, mouseX, mouseY);
    }

    //hráč následuje myš
    player.velocity.x = (mouseX - player.position.x) * 0.08;
    player.velocity.y = (mouseY - player.position.y) * 0.08;

    //zjištení kolize
    explosion.overlap(enemies, destroyEnemy);
    player.overlap(coins, getCoin);
    player.overlap(enemies, destroyPlayer);
    explosion.overlap(player, destroyPlayer);

    //spawnování vln
    if (enemies.length <= 0) {
      spawnEnemies();
    } else if (coins.length <= 0) {
      spawnCoins();
    }

  }

  drawSprites();
}

//----------------------------
function getCoin(player, coin) {
  coin.remove();
  score += 77;
}

function destroyEnemy(explosion, enemies) {
  enemies.remove();
  score += 123;
}

function destroyPlayer(explosion, enemies) {
  if (godmodeON == false) {
    godmodeON = true;
    setTimeout(setGodmodeOFF, 1500);
    playerLives--;
  }
}

function spawnEnemies() {
  for (var i = 1; i <= countEmeny; i++) {
    var e = createSprite(random(width), random(height), random(10, 50), random(5, 30));
    e.shapeColor = color(255, 0, random(255));
    e.maxSpeed = random(1, 10);
    e.rotateToDirection = true;
    enemies.add(e);
  }
  countEmeny++;
}

function spawnCoins() {
  for (var i = 1; i <= countCoins; i++) {
    var c = createSprite(random(100, width - 100), random(100, height - 100), 10, 10);
    c.shapeColor = color(255, 255, 0);
    coins.add(c);
  }
}

function mousePressed() {
  if (numberOfBombs > 0 && explosion.life <= 0 && gameOver == false) {
    numberOfBombs--;
    bomb = createSprite(width / 2, height / 2, 10, 10);
    bomb.shapeColor = color("red");
    bomb.position.x = player.position.x;
    bomb.position.y = player.position.y;
    explosionSwitch = true;
  }

  if (gameOver == true) {
    gameOver = false;
    if (firstGame == false) {
      restart();
    }
  }

}

function keyPressed() {

  if (keyCode == 17 && explosionSwitch == true) {
    numberOfBombs++;

    explosionSwitch = false;
    explosion = createSprite(width / 2, height / 2, 60, 60);
    explosion.shapeColor = color("red");
    explosion.position.x = bomb.position.x;
    explosion.position.y = bomb.position.y;
    explosion.life = 60;

    bomb.remove();
  }
}

function setGodmodeOFF() {
  godmodeON = false;
}

function restart()
{
  updateSprites(true);
  playerLives = 3;
  if (score > bestScore)
    bestScore = score;

  score = 0;
  countEmeny = 5;
  player.remove();
  explosion.remove();
  enemies.removeSprites();
  coins.removeSprites();
  bomb.remove();
  //----------------
  setup();
}
