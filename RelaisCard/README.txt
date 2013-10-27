Install cmake
=============

apt-get update
apt-get install cmake

Download libftdi
=================

libFTDI is an open source library to talk to FTDI chips: FT232BM, FT245BM, FT2232C, FT2232D, FT245R and FT232H 
including the popular bitbang mode. The library is linked with your program in userspace, no kernel driver required

git clone git://developer.intra2net.com/libftdi

cd libdtdi

Follow instruction from README:
mkdir build
Cd build
cmake -DCMAKE_INSTALL_PREFIX="/usr" ../
make
make install

Finally the library is at
 /usr/lib/libftdi1.so.2.0.0
 /usr/lib/libftdi1.so.2
 /usr/lib/libftdi1.so

And the header file is at:
/usr/include/libftdi1/ftdi.h

Connect the USB relais card
===========================

Verify that the relais card is recongized

lsusb

Bus 001 Device 004: ID 0403:6001 Future Technology Devices International, Ltd FT232 USB-Serial (UART) IC

cd libftdi/examples

gcc -I/usr/include/libftdi1/ -lftdi1 -o bitbang bitbang.c

Execute bitbang and watch the relais switching for a while:

./bitang

Now everything is ok. You may create your application using C/C++

Installing C# .NET Wrapper
==========================

We want to control the relais card using C#. We need a C# wrapper for native DLL  /usr/lib/libftdi1.so.2

git clone git://github.com/chmorgan/libftdinet

In file libftdinet.dll.config update shared livrary:

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<dllmap dll="libftdi" target="libftdi1.so.2"/>
</configuration>

Replace all [DllImport("libftdi")] by [DllImport("libftdi1")] 


Create a new C# Solution
========================

Option a: Add reference to libftdinet assembly

Option b: add the libftdinet project to your solution, then add reference to this project