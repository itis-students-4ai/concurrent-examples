using System;
using System.Threading;
using System.IO;

public class ExampleSemaphore
{
    private static Semaphore semaforo;
    private static StreamWriter sw;

    public static void Main()
    {
        semaforo = new Semaphore(1, 1);

        sw = new StreamWriter("MyFile");

        //Creazione e start di 5 thread
        for (int i = 1; i <= 5; i++)
        {
            Thread t = new Thread(new ParameterizedThreadStart(Worker));

            t.Start(i);
        }

        Thread.Sleep(500);
        semaforo.WaitOne();
        sw.Close();

        semaforo.Release(1);

        
    }

    private static void Worker(object num)
    {
        semaforo.WaitOne();
        sw.WriteLine("Scrivo la prima riga\n");
    
        Thread.Sleep(1000);

        sw.WriteLine("Scrivo La seconda riga\n");
        semaforo.Release(1);
    }
}