#!/usr/bin/python

import threading
import time

IL_SEMAFORO = threading.Semaphore()

def write_the_file(myfile):

    thread_name = threading.current_thread().name

    IL_SEMAFORO.acquire()
    myfile.write("[%s] Scrivo la prima riga\n" % thread_name)
    time.sleep(0.2)
    myfile.write("[%s] Scrivo la seconda riga\n" % thread_name)
    IL_SEMAFORO.release()


def main():

    f = open('/tmp/output-multithread.txt', 'w')

    t1 = threading.Thread(name="t1", target=write_the_file, args=(f,))
    t2 = threading.Thread(name="t2", target=write_the_file, args=(f,))
    t3 = threading.Thread(name="t3", target=write_the_file, args=(f,))
    t1.start()
    t2.start()
    t3.start()

    time.sleep(0.5)
    IL_SEMAFORO.acquire()
    f.close()
    IL_SEMAFORO.release()



if __name__ == "__main__":

    main()
