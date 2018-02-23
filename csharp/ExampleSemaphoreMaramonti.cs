using System;
using System.Threading;
using System.IO;

public class ExampleSemaphore
{
    private static Semaphore semaforo;
    private static Semaphore chiusura;
    private static int cont_thread;
    private static StreamWriter sw;

    public static void Main()
    {
        semaforo = new Semaphore(1, 1);
        chiusura = new Semaphore(0, 1);

        sw = new StreamWriter("MyFile");

        //Creazione e start di 5 thread
        for (int i = 1; i <= 5; i++)
        {
            Thread t = new Thread(new ParameterizedThreadStart(Worker));

            t.Start(i);
        }

        chiusura.WaitOne();
        sw.Close();
        chiusura.Release(1);
    }

    private static void Worker(object num)
    {
        semaforo.WaitOne();
        sw.WriteLine("Scrivo la prima riga\n");
    
        Thread.Sleep(1000);

        sw.WriteLine("Scrivo La seconda riga\n");
        cont_thread++;
        semaforo.Release(1);
        
        if (cont_thread == 5) {
            chiusura.Release(1);
        }

    }
}