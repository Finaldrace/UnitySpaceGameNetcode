using System;
using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class CommandLineArgsExample : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    public NetworkManagerHud hud;

    public AudioListener audioListener;

    /*
     * 
     * ./MyUnityProgram --param1 value1 --param2 value2
     * 
     * --server true --ip "127.0.0.1 --port "0000"
     * .\2DSpaceShooter.exe --mode "client" --ip "127.0.0.1" --port "7777"
     * .\2DSpaceShooter.exe --mode "host"
     * .\2DSpaceShooter.exe --mode "server"
     * 
     * C:\Repositorys\com.unity.multiplayer.samples.bitesize-main\Basic\2DSpaceShooter\build\Client
     * server = bool.TryParse(s,out server);
    */
    public string[] args;


    public string mode = "";
    public string ip = "127.0.0.1";
    public string port = "7777";
    public string audio = "";
    void Start()
    {
        // Print the command line arguments to the console
        args = System.Environment.GetCommandLineArgs();
        foreach (string arg in args)
        {
            textMesh.text += arg + Environment.NewLine;
            Debug.Log(arg);
            Console.WriteLine(arg);
        }

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--mode")
            {
                mode = args[i + 1];
            }
            else if (args[i] == "ip")
            {
                ip = args[i + 1];
                hud.m_ConnectAddress = ip;
            }
            else if (args[i] == "port")
            {
                port = args[i + 1];
                hud.m_PortString = port;
            }
            else if (args[i] == "audio")
            {
                audio = args[i + 1];
                if (audio == "mute")
                {
                    audioListener.enabled = false;
                }
            }
        }

        switch (mode)
        {
            case "client":
                startClient();
                break;
            case "host":
                startHost();
                break;
            case "server":
                startServer();
                break;
            default:
                break;
        }
    }
    public void startClient()
    {
        hud.m_NetworkManager.StartClient();
        StopAllCoroutines();
        StartCoroutine(hud.ShowConnectingStatus());
    }
    public void startHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void startServer()
    {
        hud.m_NetworkManager.StartServer();
        hud.ShowMainMenuUI(false);
        hud.ShowInGameUI(true);
    }
}
