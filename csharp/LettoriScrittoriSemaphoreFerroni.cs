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
    private static Semaphore _lettura = new Semaphore(1,1);
    private static Semaphore _scrittura = new Semaphore(0,1);
    private static int cont_thread;

    const int FILENAME = "MyFile";
    const int N_THREADS = 5;
    const string EXIT_COMMAND = "exit";

    public static void Main()
    {

        StreamWriter sw = new StreamWriter(FILENAME);
        string command = "";

        // Create and start five numbered threads. 
        for(int i = 1; i <= N_THREADS; i++)
        {
            Thread t = new Thread(new ParameterizedThreadStart(Worker));
            // Start the thread, passing the number.
            t.Start(i);
        }

        while (command != EXIT_COMMAND) {
            // Legge un comando da standard input
            // e lo scrive nel file appena il semaforo gli consente di passare
            command = Console.ReadLine("Inserire comando ["+ EXIT_COMMAND +" per uscire] > ");
            Console.WriteLine("Attendo l'accesso al semaforo di scrittura sul file...");
            _scrittura.WaitOne();
            sw.WriteLine(command);
            Console.WriteLine("Ho scritto il nuovo comando");
            _scrittura.Release(1);
        }

        Console.WriteLine("Main thread exits.");
    }

    private static void Worker(object num)
    {
        // Metodo per i Thread lettori
        // adattato da esempio progetto 3, pag. 33
        while (true) {

            _lettura.WaitOne();
            cont_thread++;
            if (cont_thread == 1) {
                _scrittura.WaitOne();
            }
            _lettura.Release();

            string[] commands = File.ReadAllLines(@FILENAME);

            Console.WriteLine("["+ num + "]: comandi da effettuare");
            foreach (string command in commands) {
                Console.WriteLine("[" + num + "]: " + command);
                if (command == EXIT_COMMAND) {
                    break
                }
            }
            
            _lettura.WaitOne();
            cont_thread--;
            if (cont_thread == 0) {
                _scrittura.Release();
            }
            _lettura.Release(1);
        }

        Console.WriteLine("[" + num + "]: exiting...");
    }
}
