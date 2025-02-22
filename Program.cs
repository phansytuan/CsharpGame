using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using SplashKitSDK;

namespace ForestEscape
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow = new Window("Forest Escape", 1000, 600);
            SplashKitSDK.Timer timer = new SplashKitSDK.Timer("gameTimer");
            Bitmap background = new Bitmap("background", "background.jpg");
            StartScreen startscreen = new StartScreen();
            Music startMusic = SplashKit.LoadMusic("startSound", "startMusic.mp3");
            bool gameOver = false;
            bool gameStart = false;
            bool gamePaused = false;
            bool gameLoaded = false;
            uint obstacleSpawn = 0;
            uint FlyingObstacleSpawn = 0;
            Random rand = new Random();
            Player player = new Player();
            GameObject tile1 = new Tile(0, 0,false);
            GameObject tile2 = new Tile(0, 570, true);
            Score score = new Score();
            List<GameObject> gameObjects = new List<GameObject>();
            gameObjects.Add(tile1);
            gameObjects.Add(tile2);
            gameObjects.Add(player);
            gameObjects.Add(score);
            SplashKit.StartTimer("gameTimer");
            SplashKit.PlayMusic(startMusic, 100);

            do
            {
                SplashKit.ClearScreen();
                SplashKit.ProcessEvents();
                background.Draw(0, 0);
                
                
                //Spawn the obstacle
                if (timer.Ticks - obstacleSpawn >= 2000)
                {
                    switch (rand.Next(0, 2))
                    {
                        case 0:
                            GameObstacle obstacle = new GameObstacle(rand.Next(1000, 1250), 37);
                            obstacle.SetDrawingStrategy(new GroundObstacleDrawing());
                            gameObjects.Add(obstacle);
                            break;
                        case 1:
                            GameObstacle obstacle2 = new GameObstacle(rand.Next(1000, 1250), 480);
                            obstacle2.SetDrawingStrategy(new GroundObstacleDrawing());
                            gameObjects.Add(obstacle2);
                            break;
                    }
                    obstacleSpawn = timer.Ticks;
                    if (score.Point > 20)
                    {
                        obstacleSpawn = timer.Ticks - 1000;
                    }
                    score.Point += 1;
                }

                if (timer.Ticks - FlyingObstacleSpawn >= 1500 && score.Point > 10)
                {
                    GameObstacle obstacle = new GameObstacle(rand.Next(1000, 1250), rand.Next(200,450));
                    obstacle.SetDrawingStrategy(new FlyingObstacleDrawing());
                    gameObjects.Add(obstacle);
                    FlyingObstacleSpawn = timer.Ticks;
                    if (score.Point > 20)
                    {
                        FlyingObstacleSpawn = timer.Ticks - 500;
                    }
                }

                    // Draw and update the game object to the screen
                foreach (GameObject gameObject in gameObjects)
                    {
                    if(gameObject.GetType() == typeof(GameObstacle))
                    {
                        //check if the player touch the obstacle
                        if (SplashKit.RectanglesIntersect(gameObject.Bounds, player.Bounds))
                        {
                                gameOver = true;
                                timer.Reset();
                                timer.Pause();
                        }


                    }
                        gameObject.Draw();
                        if (!gamePaused)
                        {
                            gameObject.Update(gameOver);
                        }
                    
                    }

                //reset the game
                if (gameStart)
                {
                    gameStart = false;
                    gameOver = false;
                    // Remove all obstacle in GameobjectCopy
                    gameObjects.RemoveAll(s => s is GameObstacle);

                    foreach (GameObject gameObject in gameObjects)
                    {
                        gameObject.Restart();
                    }
                    timer.Resume();
                }


                //increase the game speed if player exceed 10 points
                if(score.Point > 20)
                {
                    player.Speed = 0.5;
                    foreach (GameObject gameObject in gameObjects)
                    {
                        if(gameObject is GameObstacle)
                        {
                            gameObject.Speed = - 0.5;
                        }
                    }
                }
                else if(score.Point > 40)
                {
                    player.Speed = 0.6;
                    foreach (GameObject gameObject in gameObjects)
                    {
                        if (gameObject is GameObstacle)
                        {
                            gameObject.Speed = -0.7;
                        }
                    }
                }
                else
                {
                    player.Speed = 0.3;
                    foreach (GameObject gameObject in gameObjects)
                    {
                        if (gameObject is GameObstacle)
                        {
                            gameObject.Speed = -0.35;
                        }
                    }
                }



                //check if the R key is pressed to reset the game
                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    gameStart = true;
                }

                //check if space key is pressed to pause the game
                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    if (!gamePaused)
                    {
                        gamePaused = true;
                        timer.Pause();
                    }
                    else 
                    {
                        gamePaused = false;
                        timer.Resume();
                    }

                }

                //save the game with the S key
                if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    gamePaused = true;
                    timer.Pause();
                    StreamWriter writer = new StreamWriter("D:\\OOP\\data\\gameData.txt");
                    writer.WriteLine(gameObjects.Count - 2);
                    foreach (GameObject gameObject in gameObjects)
                    {
                        if (gameObject is not Tile)
                        {
                            gameObject.SaveTo(writer);
                        }
                    }
                    writer.Close();

                } 

                //Load the game with L key
                if (SplashKit.KeyTyped(KeyCode.LKey) && !gameLoaded)
                {
                    gameLoaded = true;
                    gameObjects.RemoveAll(s => s is not Tile);
                    StreamReader reader = new StreamReader("D:\\OOP\\data\\gameData.txt");
                    int count = reader.ReadInteger();
                    GameObject g;

                    for(int i = 0; i < count; i++)
                    {
                        string type = reader.ReadLine();
                        switch (type)
                        {
                            case "Player":
                                g = new Player();
                                player = g as Player;
                                break;
                            case "Score":
                                g = new Score();
                                score = g as Score;
                                break;
                            case "Obstacle":
                                g = new GameObstacle();    
                                break;
                            default:
                                throw new InvalidDataException("Error at object: " + type);
                        }
                        g.LoadFrom(reader);
                        gameObjects.Add(g);
                    }
                    reader.Close();
                    
                }

                if (gamePaused)
                {
                    startscreen.Draw();
                }
                SplashKit.RefreshScreen();

            } while (!SplashKit.WindowCloseRequested("Forest Escape"));

            
        }
    }
}
