
public class PlayerHealth : Bolt.EntityBehaviour<ICustomCubeState>
{

	public int localHealth=3;

	public override void Attached()
	{
		state.Health=localHealth;
		state.AddCallback("Health",HealthCallback);
	}

	private void HealthCallback()  //this will be called whenever the state changed
	{
		localHealth=state.Health;
		
		if(localHealth <=0)
		{
			BoltNetwork.Destroy(gameObject);
		}
	}


	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			state.Health -=1; //whenever the state will change it calls -> state.AddCallback("Health",HealthCallback);
		}
		
	}

}