
//Shoot ->trigger property
public class Shooting : Bolt.EntityBehaviour<ICustomCubeState>
{
	public Rigidbody bulletPrefab;
	public float bulletSpeed;
	public GameObject muzzle;
	
	public override void Attached()
	{
		state.OnShoot = Shoot;
	}
	
	private void Shoot()
	{
		Rigidbody bulletClone=Instantite(bulletPrefab,muzzle.transform.position,this.transform.rotation);
		bulletClone.velocity=transform.TransformDirection(new Vector3(0,0,bulletSpeed));
	}
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space)&&entity.isOwner)
		{
			state.Shoot();
		}
	}
}