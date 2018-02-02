#!/usr/bin/python

import threading
import time

def write_the_file(myfile):

    myfile.write("Scrivo la prima riga\n")
    time.sleep(0.2)
    myfile.write("Scrivo la seconda riga\n")


def main():

    f = open('/tmp/output-multithread.txt', 'w')

    t1 = threading.Thread(target=write_the_file, args=(f,))
    t2 = threading.Thread(target=write_the_file, args=(f,))
    t3 = threading.Thread(target=write_the_file, args=(f,))
    t1.start()
    t2.start()
    t3.start()

    time.sleep(0.5)
    f.close()



if __name__ == "__main__":

    main()
