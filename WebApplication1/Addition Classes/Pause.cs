using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Addition_Classes
{
   static public class Pause
    {
      static public Task<bool> Pauses()
       {
           Thread.Sleep(1000);
          return null;
       }
    }
}