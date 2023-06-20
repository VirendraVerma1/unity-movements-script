//event system

public class PlayerJoined : Bolt.EntityBehaviour<ICustomCubeState>
{
	public override void Attached()
	{
		var event=PlayerJoinedEvent.Create();
		event.Message="Hello there!";
		event.Send();
	}

}

public class ServerEventListner : Bolt.GlobalEvemtListner
{
	public override void OnEvent(PlayerJoinedEvent event)
	{
		print(event.Message);
	}

}