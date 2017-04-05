package com.google.challenges;
import java.util.ArrayList;
import java.util.List;

public class Answer {  
    public static int[] answer(int area) {
     List<Integer> myList = new ArrayList<Integer>();
        int total = 0;//Number goes up.
        int max = area;//Number goes down.
        while(total != area){
          if(isPerfectSquare(max)) {          
            myList.add(max);
            total = total+max;
            max = area - total;        
          } else {
           max = --max;        
          }
        }
       
        //TODO: Turn into a method
        int size = myList.size();
        int[] Arr = new int[size];
        for(int i=0;i<myList.size();i++){
            Arr[i] = myList.get(i);
        }
       
        return Arr;  
  }
 
  //Only method I needed to cheat on...for performance reasons.
   private final static boolean isPerfectSquare(long n)
{
  if (n < 0)
    return false;

  switch((int)(n & 0x3F))
  {
  case 0x00: case 0x01: case 0x04: case 0x09: case 0x10: case 0x11:
  case 0x19: case 0x21: case 0x24: case 0x29: case 0x31: case 0x39:
    long sqrt;
    if(n < 410881L)
    {
      //John Carmack hack, converted to Java.
      // See: http://www.codemaestro.com/reviews/9
      int i;
      float x2, y;

      x2 = n * 0.5F;
      y  = n;
      i  = Float.floatToRawIntBits(y);
      i  = 0x5f3759df - ( i >> 1 );
      y  = Float.intBitsToFloat(i);
      y  = y * ( 1.5F - ( x2 * y * y ) );

      sqrt = (long)(1.0F/y);
    }
    else
    {
      //Carmack hack gives incorrect answer for n >= 410881.
      sqrt = (long)Math.sqrt(n);
    }
    return sqrt*sqrt == n;

  default:
    return false;
  }
}
}
