/* 
 * This software is part of `concurrent-examples` project
 * created during students class 4AI ITIS Fabriano AS 2017-2018
 * Authors:
 * - Luca Ferroni <luca@befair.it>
 * - Marco Maramonti <...>
 * 
 * This work is released with LICENSE GPLv3, so you have the right
 * to use, copy, modify and share your modifications as long
 * as you preserve this note and you redistribute derivative versions with
 * the same LICENSE.
 */

using System;
using System.Threading;
using System.IO;

public class LettoriScrittoriSemaphoreFerroni
{
    // Implementation of the ordered write to a file
    // with the approach of readers/writers.
    // (ref. book at page 32-33)

    // Define 2 semaphores
    private static Semaphore _scrittura = new Semaphore(1,1);
    private static Semaphore _chiusura = new Semaphore(0,1);
    private static int cont_thread;
    private static StreamWriter sw;

    const int N_THREADS = 5;

    public static void Main()
    {

        sw = new StreamWriter("MyFile");

        // Create and start five numbered threads. 
        for(int i = 1; i <= N_THREADS; i++)
        {
            Thread t = new Thread(new ParameterizedThreadStart(Worker));
            // Start the thread, passing the number.
            t.Start(i);
        }

        // Avvia la procedura di chiusura solo quando 
        // il semaforo gli consente di entrare nella regione critica
        _chiusura.WaitOne();
        sw.WriteLine("Main thread write the last line.");
        sw.Close();
        _chiusura.Release(1);

        Console.WriteLine("Main thread exits.");
    }

    private static void Worker(object num)
    {
        _scrittura.WaitOne();
        sw.WriteLine("Scrivo la prima riga");
        
        // Questo sleep è solo per dimostrare la potenza
        // della sincronizzazione tramite semafori
        Thread.Sleep(1000);

        sw.WriteLine("Scrivo La seconda riga");

        cont_thread++;
        if (cont_thread == 5) {
            // Rilascia il semaforo per la chiusura
            _chiusura.Release(1);
        }
        _scrittura.Release(1);
    }
}
