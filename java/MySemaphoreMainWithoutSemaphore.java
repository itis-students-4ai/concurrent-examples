
import java.io.*;

 
public class MySemaphoreMainWithoutSemaphore {
 
 public static void main(String[] args) throws InterruptedException, IOException {
 
  final BufferedWriter out = new BufferedWriter(new FileWriter("/tmp/output-multithread.txt"));

  Runnable writeToFile = new Runnable() {
 
   @Override
   public void run() {
    try {
     out.write("Scrivo la prima riga\n");
     Thread.sleep(200);
     out.write("Scrivo la seconda riga\n");
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
  out.close();
 }
 
}
