using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using libftdinet;

namespace RelaisConsole
{
    class relais
    {

        private static FTDIContext device;
        byte relaisSetting = 0;
        byte[] buf = new byte[10];

        /// <summary>
        /// workaround for Wheezy Hardfloat : this is power of two
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>

        static int powerOfTwo(uint x)
        {
            int rc = 1;
            for (int k = 0; k < x; k++)
            {
                rc = rc * 2;
            }
            return (rc);
        }

        /// <summary>
        /// read current relais setting
        /// </summary>
        /// <returns></returns>

        public byte readRelaisPort()
        {
            byte inputs = 0;

            if (device == null)
            {
                Console.WriteLine(" USB relay : open device first ");
                Exception ex = new Exception(" USB relay : open device first ");
                throw (ex);
            }

            inputs = device.GetPins();
            Console.WriteLine(" Device Inputs are " + inputs.ToString("X2"));

            return (inputs);
        }


        /// <summary>
        /// set relay TRUE or FALSE
        /// </summary>
        /// <param name="nr"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public byte setRelay(uint nr, Boolean value)
        {

            if (device == null)
            {
                Console.WriteLine(" USB relay : open device first ");
                Exception ex = new Exception(" USB relay : open device first ");
                throw (ex);
            }

            byte setting = (byte) powerOfTwo(nr);

            relaisSetting = readRelaisPort();

            if (value == true) {
                relaisSetting = (byte)(relaisSetting | setting);
            }
            else {
                relaisSetting = (byte)(relaisSetting & (255 - setting));
            }

            Console.WriteLine("Relais " + nr.ToString() + " " + setting.ToString("X2") + " setting : " + relaisSetting.ToString("X2"));

            buf[0] = relaisSetting;
            device.WriteData(buf, 1);

            return (relaisSetting);

        }

        /// <summary>
        /// for test purpose : set relais ON then set relais OFF
        /// </summary>

        public void relaisTest()
        {
            for (uint k = 0; k < 8; k++)
            {
                setRelay(k, true);
                Thread.Sleep(1000);
            }

            for (uint k = 0; k < 8; k++)
            {
                setRelay(k, false);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// init FTDI device
        /// </summary>

        public void init()
        {
            device = new FTDIContext(0x0403, 0x6001);

            if (device == null)
            {
                Console.WriteLine(" FTDI devive not found ");
                return;
            }

            device.SetBitMode(255, 4);  // setting bit bang mode
        }

        public void close()
        {
            device.Close();
        }

    }

}
