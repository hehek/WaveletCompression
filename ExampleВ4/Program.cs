using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleВ4
{
    class Program
    {
        //Коэффициенты высокочастотного фильтра (HPF, high-pass filter) 
        static ArrayList hpf_coeffs(ArrayList CL)
        {
            int length = CL.Count;
            double n;            
            ArrayList CH = new ArrayList();
            for(int i=0; i< length; i++)
            {
               n = Math.Pow(-1, i) * (double)CL[length - i - 1];
                CH.Add(n);
            }            
            return CH;
        }

        //Прямое одномерное преобразование pair conv. - парная свертка
        static ArrayList pconv(ArrayList CL, ArrayList CH, ArrayList data, int delta)
        {
            int _delta = 0;
            double sL=0, sH=0;
            int CL_length = CL.Count;
            int data_length = data.Count;
            ArrayList outList = new ArrayList();  // Список с результатом
            for (int k=0; k<data_length; k+=2)  // Перебираем числа 0, 2, 4…
            {
                sL = 0; sH = 0;
                // Находим сами взвешенные суммы
                for (int i=0; i<CL_length; i++) 
                {
                    
                    sL += Convert.ToDouble(data[(k + i - _delta) % data_length] )* Convert.ToDouble(CL[i]);  // Низкочастотный коэффициент
                    sH += Convert.ToDouble(data[(k + i - _delta) % data_length]) * Convert.ToDouble(CH[i]);  // Высокочастотный коэффициент

                }
                // Добавляем коэффициенты в список
                outList.Add(sL);
                outList.Add(sH);
            }
            return outList;
        }

        static void Main(string[] args)
        {
            //Коэффициенты первой строки, относящейся к фильтру низких (L, low) частот.
            ArrayList CL = new ArrayList() { (1 + Math.Sqrt(3)) / (4 * Math.Sqrt(2)),
                                             (3 + Math.Sqrt(3)) / (4 * Math.Sqrt(2)),
                                             (3 - Math.Sqrt(3)) / (4 * Math.Sqrt(2)),
                                             (1 - Math.Sqrt(3)) / (4 * Math.Sqrt(2))};                
                  
            ArrayList data = new ArrayList() { 1, 2, 3, 4 };
            ArrayList C = new ArrayList() { 0.5, 0.5 };
            ArrayList outList = pconv(C, hpf_coeffs(C), data, 0);
            //[1.5, -0.5, 3.5, -0.5]  
            foreach (double i in outList)
            {
                Console.WriteLine(i);

            }

            Console.ReadKey();


    }
    }
}
