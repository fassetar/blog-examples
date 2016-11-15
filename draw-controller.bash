 #!/bin/bash
pkill xpaint #kills any xpain that was already started#
xpaint -nowarn &
xwininfo -root|grep 'geometry'|sed -e 's/+.*$//g'|sed -e 's/^.*-geometry //g'>filea
WIDTH=`cat filea|cut -d 'x' -f1`
HEIGHT=`cat filea|cut -d 'x' -f2`
sleep 3
echo "entering xpaint"
Version=`xwit -all -print| grep -i xpaint | sed -e 's/^.*\([0-9][0-9]*\.[0-9][0-9]*\.[0-9][0-9]*\).*$/\1/'`
echo "Version is: $Version"
pkill xpaint 
dVersion=`echo $Version | sed 's/\.//g'`
Version=$dVersion
if [ $Version -eq 2813 ] 
then
Version="-canvas"
else 
Version="-popped"
fi
xpaint -24 -size ${WIDTH}x${HEIGHT} -nowarn -nomenubar $Version & #"{" lets you know when the variable ends
sleep 3
toolID=`xwit -all -print | grep -i xpaint | sed -e 's/:.*//g'` 
winID=`xwit -all -print | grep -i untitled| sed -e 's/:.*//g'`
focusWin=`xwit -id $winID -focus -raise -move 0 0`
focusTools=`xwit -id $toolID -focus -raise -move 0 0`
echo $toolID
echo $winID
echo "Width is $WIDTH"
echo "Height is $HEIGHT"
sleep 4
xpos=`echo $[($RANDOM % $WIDTH)]`
ypos=`echo $[($RANDOM % $HEIGHT)]`
size=`echo $[($RANDOM % 30)]`
echo $xpos
echo $ypos
echo "done"
shape=`echo $[($RANDOM % 2)]`
echo $shape
 if [ $shape -eq 0 ] #Draw a triangle
            then
	    xte 'mousemove 500 500'
            xte 'mousedown 1'
            xte 'mousemove 600 600'
            xte 'mousemove 500 600'
            xte 'mousemove 500 500'
            xte 'mouseup 1'
            fi
 if [ $shape -eq 1 ] #Draw a Square
            then
            xte 'mousemove  600 500'
            xte 'mousedown 1'
            xte 'mousemove 600 600'
	    xte 'mousemove 500 600'
	    xte 'mousemove 500 500'
	    xte 'mousemove 600 500'
            #xte 'mousermove ${xpos} ${size}'
        xte 'mouseup 1'
        fi
 


sleep 10
pkill xpaint #same as before but this time making sure that I've finished
rm -f filea
#xwit  -lower -raise##
