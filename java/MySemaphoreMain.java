
import java.util.concurrent.Semaphore;
import java.io.*;

 
public class MySemaphoreMain {
 
 public static void main(String[] args) throws InterruptedException, IOException {
 
  final BufferedWriter out = new BufferedWriter(new FileWriter("/tmp/output-multithread.txt"));
  final Semaphore semaphore = new Semaphore(1);

  Runnable writeToFile = new Runnable() {
 
   @Override
   public void run() {
    try {
     semaphore.acquire();
     out.write("Scrivo la prima riga\n");
     Thread.sleep(200);
     out.write("Scrivo la seconda riga\n");
     semaphore.release();
    } catch (InterruptedException ie) {
    } catch (IOException ie) {
    }
   }
  };
 
  Thread t1 = new Thread(writeToFile);
  Thread t2 = new Thread(writeToFile);
  Thread t3 = new Thread(writeToFile);
  t1.start();
  t2.start();
  t3.start();
  Thread.sleep(500);
  semaphore.acquire();
  out.close();
  semaphore.release();
 }
 
}
