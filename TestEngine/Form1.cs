using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestEngine
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      unsafe
      public struct array_information
      {
         public double* a;
         public double* b;
         public double* c;
      };

      [DllImport("Engine_CPP.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = true)]
      unsafe public extern static double* AddTwoArrayWIthLenth3_CPP(ref array_information array_info);

      

      private void Form1_Load(object sender, EventArgs e)
      {
         array_information arrinfo = new array_information();
         double[] a = { 3, 3, 3 };
         double[] b = { 4, 5, 6 };

         unsafe
         {
            double* c;
            double* d;
            fixed (double* a_ptr = a)
            {
               fixed (double* b_ptr = b)
               {
                  arrinfo.a = a_ptr;
                  arrinfo.b = b_ptr;
                  arrinfo.c = null;
                  d = AddTwoArrayWIthLenth3_CPP(ref arrinfo);
                  Console.WriteLine("Done");
               }
            }
            
         }
       }
   }
}
