package
{
	import flash.display.MovieClip
	import playerio.*
	import flash.text.TextField;
	import flash.text.TextFieldType;
    import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	import flash.events.MouseEvent;
	import flash.events.Event;
	import flash.display.*;
	import flash.net.*;
	import flash.text.TextField;

	
	public class MyGame extends MovieClip{
		  var chatField:TextField = new TextField();
		  var displayField:TextField = new TextField();
		  var theConnection;
		  var userid:uint;

		
		function MyGame(){
			stop();
			PlayerIO.connect(
				stage,								//Referance to stage
				"testop-ds1hiqswoe2pxkmlk20duq",			//Game id (Get your own at playerio.com)
				"public",							//Connection id, default is public
				"GuestUser",						//Username
				"",									//User auth. Can be left blank if authentication is disabled on connection
				null,								//Current PartnerPay partner.
				handleConnect,						//Function executed on successful connect
				handleError							//Function executed if we recive an error
			);   
		}
		
		  function handleConnect(client:Client):void{
			trace("Sucessfully connected to player.io");
			
			//Set developmentsever (Comment out to connect to your server online)
			client.multiplayer.developmentServer = "localhost:8184";
			
			//Create pr join the room test
			client.multiplayer.createJoinRoom(
				"test",								//Room id. If set to null a random roomid is used
				"MyCode",							//The game type started on the server
				true,								//Should the room be visible in the lobby?
				{},									//Room data. This data is returned to lobby list. Variabels can be modifed on the server
				{},									//User join data
				handleJoin,							//Function executed on successful joining of the room
				handleError							//Function executed if we got a join error
			);
		}
		
		  function keyisDown(ev:KeyboardEvent){
			if(ev.keyCode == Keyboard.ENTER){
				theConnection.send("enemy1", chatField.text);
				
				chatField.text = "";
			}
			
		}
		
		
		  function handleJoin(connection:Connection):void{
			trace("Sucessfully connected to the multiplayer server");
			
			theConnection = connection;
			
			chatField.width = 200;
			chatField.height = 30;
			chatField.x = 40;
			chatField.y = 100;
			chatField.border = true;
			chatField.type = TextFieldType.INPUT;
			stage.addChild(chatField);
			chatField.addEventListener(KeyboardEvent.KEY_DOWN, keyisDown);
			chatField.border = true;

			displayField.width = 200;
			displayField.height = 100;
			displayField.x = 40;
			displayField.y = 140;
			displayField.border = true;
			stage.addChild(displayField);
			
			//Add disconnect listener
			connection.addDisconnectHandler(handleDisconnect);
					
			connection.addMessageHandler("enemy1", function(m:Message, chatMessage:String){
				displayField.appendText(chatMessage + "\n");					
			})
			
			//Add message listener for users joining the room
			connection.addMessageHandler("UserJoined", function(m:Message, userid:uint){
				trace("Player with the userid", userid, "just joined the room");
		 		userid = this.userid;
			})
			
			//Add message listener for users leaving the room
			connection.addMessageHandler("UserLeft", function(m:Message, userid:uint){
				trace("Player with the userid", userid, "just left the room");
			})
			
			//Listen to all messages using a   function
			connection.addMessageHandler("*", handleMessages)
			
		}
		
		  function handleMessages(m:Message){
			trace("Recived the message", m)
		}
		
		  function handleDisconnect():void{
			trace("Disconnected from server")
		}
		
		  function handleError(error:PlayerIOError):void{
			trace("got",error)
			gotoAndStop(3);

		}
	}	

}