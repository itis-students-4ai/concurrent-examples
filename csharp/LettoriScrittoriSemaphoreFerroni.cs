using System;
using System.Threading;

public class ExampleSemaphoreFerroni
{
    // A semaphore that simulates a limited resource pool.
    //
    private static Semaphore _chiusura;
    private static Semaphore _scrittura;

    const int N_THREADS = 5;
    private static int cont = N_THREADS;


    public static void Main()
    {
        // Create a semaphore that can satisfy up to three
        // concurrent requests. Use an initial count of zero,
        // so that the entire semaphore count is initially
        // owned by the main program thread.
        //
        _chiusura = new Semaphore(0, 0);
        _scrittura = new Semaphore(0, 1);


        // Create and start five numbered threads. 
        //
        for(int i = 1; i <= N_THREADS; i++)
        {
            Thread t = new Thread(new ParameterizedThreadStart(Worker));

            // Start the thread, passing the number.
            //
            t.Start(i);
        }

        // Wait for half a second, to allow all the
        // threads to start and to block on the semaphore.
        //
        Thread.Sleep(500);

        // The main thread starts out holding the entire
        // semaphore count. Calling Release(1) brings the 
        // semaphore count back to its maximum value, and
        // allows the waiting threads to enter the semaphore,
        // up to three at a time.
        //
        Console.WriteLine("Main thread calls Release(1).");
        _pool.Release(1);

        Console.WriteLine("Main thread exits.");
    }

    private static void Worker(object num)
    {
        // Each worker thread begins by requesting the
        // semaphore.
        Console.WriteLine("Thread {0} begins " +
            "and waits for the semaphore.", num);
        _scrittura.WaitOne();

        Console.WriteLine("Thread {0} enters the semaphore.", num);

        // The thread's "work" consists of sleeping for 
        // about a second. Each thread "works" a little 
        // longer, just to make the output more orderly.
        //

        Console.WriteLine("Thread {0} releases the semaphore.", num);
        _scrittura.Release();
        //Console.WriteLine("Thread {0} previous semaphore count: {1}",
        //    num, );
    }
}
