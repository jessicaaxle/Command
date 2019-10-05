using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    static Queue<ICommand> commonBuffer;

    static List<ICommand> commandHistory;
    static int counter;

    private void Awake()
    {
        commonBuffer = new Queue<ICommand>();
        commandHistory = new List<ICommand>();
    }

    public static void AddCommand(ICommand command)
    {
            while (commandHistory.Count > counter)
            {
            commandHistory.RemoveAt(counter);
            }
        commonBuffer.Enqueue(command);
    }

    void Update()
    {
        if (commonBuffer.Count > 0)
        {
            ICommand c = commonBuffer.Dequeue();
            c.Execute();

            commandHistory.Add(c);
            counter++;
        }
        else
            if (Input.GetKeyDown(KeyCode.Z))
        {
            if (counter > 0)
            {
                counter--;
                commandHistory[counter].Undo();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (counter < commandHistory.Count)
            {
                commandHistory[counter].Execute();
                counter++;
            }
        }
       
    }
}
