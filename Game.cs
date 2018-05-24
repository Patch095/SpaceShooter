using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    static class Game
    {
        static List<Scene> scenes;
        public static Scene CurrentScene { get; private set; }
        public static int NumJoysticks;
        public static Window Window;
        static float gravity;
        public static float DeltaTime { get { return Window.deltaTime; } }
        public static float Gravity { get { return gravity; } }

        static Game()
        {
            Window = new Window(1280, 720, "Run!");
            gravity = 300.0f;

            string[] joysticks = Game.Window.Joysticks;
            for (int i = 0; i < joysticks.Length; i++)
            {
                if (joysticks[i] != null && joysticks[i] != "Unmapped Controller")
                    NumJoysticks++;
            }

        }

        public static void Play()
        {
            //scenesCreation
            TitleScene aivTitle = new TitleScene("Assets/aivBG.png", KeyCode.Return);
            WelcomeScene welcome = new WelcomeScene("Assets/welcomeBg.jpg", KeyCode.Return);
            PlayScene playScene = new PlayScene();
            GameOverScene gameover = new GameOverScene();
            //scenes config
            aivTitle.NextScene = welcome;
            aivTitle.ShowTime = 3;
            aivTitle.FadeIn = true;
            aivTitle.FadeOut = true;

            welcome.NextScene = playScene;
            welcome.FadeOut = false;

            playScene.NextScene = gameover;
            gameover.NextScene = playScene;

            aivTitle.Start();
            CurrentScene = aivTitle;


            //GfxManager.Load();
            while (Window.opened)
            {
                if (!CurrentScene.IsPlaying)
                {
                    //next scene
                    if (CurrentScene.NextScene != null)
                    {
                        CurrentScene.OnExit();
                        CurrentScene = CurrentScene.NextScene;
                        CurrentScene.Start();
                    }
                    else
                        return;
                }

                //Input
                if (Window.GetKey(KeyCode.Esc))
                    break;
                CurrentScene.Input();

                //Update
                CurrentScene.Update();

                //Draw
                CurrentScene.Draw();

                Window.Update();
            }
        }
    }
}