﻿using CSharpUtils.Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CSharpUtilsTests
{
    [TestClass]
    public class ProcessTest
    {
        [TestMethod]
        public void ProcessToTest()
        {
            var Output = new LinkedList<string>();
            var mainProcess = new MainProcess();
            var f1 = new MyProcess1();
            var f2 = new MyProcess2();

            MyProcess.DrawedHandler OnDrawed = delegate(object sender, EventArgs e)
            {
                var de = ((DrawedEventArgs)e);
				//Console.WriteLine(Output);
                Output.AddLast(Convert.ToString(de.n));
            };

            f1.Drawed += new MyProcess.DrawedHandler(OnDrawed);
            f2.Drawed += new MyProcess.DrawedHandler(OnDrawed);
			while (mainProcess.State != State.Ended)
            {
                Output.AddLast("[");
                //Output.AddLast(String.Join(",", Process.allProcesses));
                mainProcess._ExecuteProcess();
                mainProcess._DrawProcess();
                //f1._ExecuteProcess();
                Process._removeOld();
                Output.AddLast("]");
            }
            Assert.AreEqual(
                "[,1,-1,],[,2,-2,],[,3,-3,],[,4,-4,],[,3,-3,],[,2,-2,],[,1,-1,],[,0,0,],[,-1,1,],[,-2,2,],[,-3,3,],[,-4,4,],[,-4,4,],[,]",
                String.Join(",", Output)
            );
            //Console.ReadKey();
        }
    }

    class DrawedEventArgs : EventArgs
    {
        public int n;
        public DrawedEventArgs(int n)
        {
            this.n = n;
        }
    }

    abstract class MyProcess : Process
    {
        int n = 0;
        public delegate void DrawedHandler(object sender, EventArgs e);
        public event DrawedHandler Drawed;

        protected void increment()
        {
            while (n < +4)
            {
                n++;
                Yield();
            }
        }

        protected void decrement()
        {
            while (n > -4)
            {
                n--;
                Yield();
            }
        }

        override protected void Draw()
        {
            if (Drawed != null) Drawed(this, new DrawedEventArgs(n));
            //Console.WriteLine(n);
        }

    }

    class MyProcess1 : MyProcess
    {
        override protected void main()
        {
            increment();
            decrement();
        }
    }

    class MyProcess2 : MyProcess
    {
        override protected void main()
        {
            decrement();
            increment();
        }
    }
}
