package net.chars
{

import flash.geom.Point;
import flash.display.*;
import flash.events.MouseEvent;
import net.chars.main;

public class charsel extends MovieClip {
	
	protected var originalPosition:Point;
	public static var player1:MovieClip, player2:MovieClip, player3:MovieClip;
	public static var charselected = 0;
	public static var scene = "abc";;
	public function charsel () {
	
		originalPosition = new Point(x, y);
		buttonMode = true;
		
		addEventListener( MouseEvent.MOUSE_DOWN, down);
		}
		protected function down( event:MouseEvent) :void{
			parent.addChild( this );
			startDrag();
			stage.addEventListener( MouseEvent.MOUSE_UP, stageUp);			
		}
		protected function stageUp( event:MouseEvent) :void{
			stage.removeEventListener(MouseEvent.MOUSE_UP, stageUp);
			stopDrag();
			var charselected = 0;
			if( dropTarget ){
				if(dropTarget.parent.name == "charslot1")
				{
				x = 556;
				y = 475.0;
				player1 = this;
				++charselected;
				}
				else if(dropTarget.parent.name == "charslot2")
				{
				x = 621;
				y = 475.0;
				player2 = this;
				++charselected;
				}
				else if(dropTarget.parent.name == "charslot3")
				{
				x = 686;
				y = 475.0;
				player3 = this;
				++charselected;
				}
				else
				{
					returntoOriginalPosition();
				}
			}
			else
				{
					returntoOriginalPosition();
				}
		}
		
		function returntoOriginalPosition():void{
			x = originalPosition.x;
			y = originalPosition.y;			
		}
		
	
	}
}
