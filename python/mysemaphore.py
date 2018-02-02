#!/usr/bin/python

import threading
import time

IL_SEMAFORO = threading.Semaphore()

def write_the_file(myfile):

    IL_SEMAFORO.acquire()
    myfile.write("Scrivo la prima riga\n")
    time.sleep(0.2)
    myfile.write("Scrivo la seconda riga\n")
    IL_SEMAFORO.release()


def main():

    f = open('/tmp/output-multithread.txt', 'w')

    t1 = threading.Thread(target=write_the_file, args=(f,))
    t2 = threading.Thread(target=write_the_file, args=(f,))
    t3 = threading.Thread(target=write_the_file, args=(f,))
    t1.start()
    t2.start()
    t3.start()

    time.sleep(0.5)
    IL_SEMAFORO.acquire()
    f.close()
    IL_SEMAFORO.release()



if __name__ == "__main__":

    main()
